// edget.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#define _CRT_SECURE_NO_WARNINGS 1

#include <winsock.h>
#include <windows.h>

#include <string.h>
#include <stdlib.h>
#include <time.h>

#pragma comment(lib,"ws2_32.lib")

#include "rdef.h"
#include "rsdutime.h"
#include "errlist.h"
#include "rsdulogs.h"

#define LOG_CONTAINER    FILE
#define RSDUUTILS_API

int32_t tcp_cycle_receive(int sock, char* buf, uint32_t dlength, uint16_t timeout, uint16_t *status) ;
int32_t timed_recvfrom(int sock, uint16_t time_out, char *dgram, int32_t length, int32_t flag, struct sockaddr *r_addr, size_t *r_addr_len, uint16_t *status) ;


RSDUUTILS_API void RSDURTGUtils_SFree( void * ptr)
{
    if (ptr == (void *)0) return;

    free (ptr);
}

RSDUUTILS_API void * RSDURTGUtils_SMalloc(uint32_t size)
{
    void *buf = NULL;

    if (size == 0) return ((void *)0);

    buf = (void*)malloc((size_t)size);
    return ((void*)buf);
}

RSDUUTILS_API void * RSDURTGUtils_SCalloc(uint32_t n, uint32_t size)
{
    char *b;

    if ((size == 0) || (n == 0)) return ((void *)0);
    b = (char*)calloc((size_t)n, (size_t)size);
    return((void *)b);
}

RSDUUTILS_API void * RSDURTGUtils_SRealloc(void *old, uint32_t size)
{
    char *buf;

    buf = (char*)realloc (old, (size_t)size);
    return((void*)buf);
}

uint16_t RSDURTGUtils_GetCommonProto()
{
    return (ADV_PROTO_UDP);
}

int32_t RSDURTGUtils_AddressSetPort(REM_ADDR * address, uint16_t srvc_port, uint16_t id_proto)
{
    if (address == (REM_ADDR *)0)
        return(-1);
    if (id_proto == ADV_PROTO_COMMON)
        (*address).type = RSDURTGUtils_GetCommonProto();
    else
        (*address).type = id_proto;

    switch ((*address).type)
    {
    case ADV_PROTO_ETH:
        (*address).addr.addr_eth.portid = srvc_port;
        break;

    case ADV_PROTO_NCS:
        (*address).addr.addr_ncs.r_sock = (uint32_t)(((srvc_port << 16) & 0xFFFF0000) | ((*address).addr.addr_ncs.r_sock & 0x0000FFFF));
        break;

    case ADV_PROTO_UDP_CLEAR:
    case ADV_PROTO_UDP:
    case ADV_PROTO_TCP:
        (*address).addr.addr_ip.ip_port = srvc_port;
        break;

    default:
        return(-1);
    }

    return(0);
}

/* 
*****************************************************************************************
* RSDURTGUtils_bound_stream_socket - returns a bound stream socket & port for a specified address.
*
* RETURNS - socket handle if successful, -1 if unsuccessful.
*****************************************************************************************
*/
int RSDURTGUtils_bound_stream_socket(uint16_t *port, uint32_t timeout, LOG_CONTAINER *lfp)
{
    struct sockaddr_in  sock_addr;
    int32_t             length = 0;

    int                 sock = 0;
    char                err_string[64];
    const int32_t       on = 1;

    /* Generate a network address for the command socket */
    sock_addr.sin_family = AF_INET;
    sock_addr.sin_addr.s_addr = INADDR_ANY;
    sock_addr.sin_port = htons(*port);

    /* Create a new socket */
    sock = socket(AF_INET,SOCK_STREAM,0);
    if (sock < 0)
    {
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE, "stream-socket creation failed");
        sock = -1;
    }
    /* Set option SO_REUSEADDR for socket
       It's allows to ignore TIME_WAIT behavior of TCP */
    else if (setsockopt(sock, SOL_SOCKET, SO_REUSEADDR, (char*)&on, (socklen_t)sizeof(on)) < 0)
    {
        sprintf(err_string, "setsockopt failed, sock=%d, port=%u\n", sock, *port);
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE, err_string);
        shutdown(sock,2);
        closesocket(sock);

        sock = -1;
    }

    /* Bind the new socket.
    */
    else if ((bind(sock, (struct sockaddr *)&sock_addr, (socklen_t)sizeof(struct sockaddr_in))) < 0)
    {
        sprintf(err_string, "bind failed, sock=%d, port=%u\n", sock, *port);
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE, err_string);
        shutdown(sock,2);
        closesocket(sock);
        sock = -1;
    }
    else
    {
        length = sizeof(struct sockaddr_in);
        if ((getsockname(sock, (struct sockaddr*)&sock_addr, &length)) < 0 )
        {
            //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE, "getsockname failed");
            shutdown(sock,2);
            closesocket(sock);
            sock = -1;
        }
        else
        {
            *port = ntohs(sock_addr.sin_port);
        }
    }
    return(sock);
}

/* 
*****************************************************************************
* RSDURTGUtils_bound_dgram_socket - returns a bound datagram socket for a specified address.
*
* RETURNS - socket handle if successful, -1 if unsuccessful.
*****************************************************************************
*/

int RSDURTGUtils_bound_dgram_socket (uint16_t *port, int32_t broadcast, uint32_t timeout, LOG_CONTAINER *lfp)
{
    struct sockaddr_in  sock_addr;
    int         sock = 0;

    int32_t     length;

    int32_t     setBC;
    int32_t     setReuseAddr = 0;
    char        err_string[64];

    // Do not set SO_REUSEADDR in case if port-id is unknown
    if ((port != NULL) && (*port != 0))
        setReuseAddr = 1;
    else
        setReuseAddr = 0;
    /* Generate a network address for the command socket.
    */
    sock_addr.sin_family = AF_INET;
    sock_addr.sin_addr.s_addr = INADDR_ANY;
    sock_addr.sin_port = htons(*port);
    setBC = 1;//broadcast;


    // Declare and initialize variables
    WSADATA wsaData = {0};
    int iResult = 0;

    // Initialize Winsock
    iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (iResult != 0) {
        wprintf(L"WSAStartup failed: %d\n", iResult);
        return -1;
    }


    /* Create a new socket */
    sock = socket(AF_INET, SOCK_DGRAM, 0);

    if (sock == INVALID_SOCKET)
    {
        wprintf(L"socket function failed with error = %d\n", WSAGetLastError() );
        sock = -1;
    }


    if (sock < 0)
    {
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE,"socket creation failed");
        sock = -1;
    }
    else if (setsockopt(sock, SOL_SOCKET, SO_BROADCAST, (char *) &setBC, (socklen_t)sizeof(setBC)) < 0)
    {
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE,"setsockopt() failed\n" );
        shutdown(sock,2);

        closesocket(sock);

        sock = -1;
    }
    else if (setsockopt(sock, SOL_SOCKET, SO_REUSEADDR, (char *) &setReuseAddr, (socklen_t)sizeof(setReuseAddr)) < 0)
    {
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE,"setsockopt() failed\n" );
        shutdown(sock,2);

        closesocket(sock);

        sock = -1;
    }

    else if ((bind(sock, (struct sockaddr *) &sock_addr,(socklen_t)sizeof(struct sockaddr_in))) < 0)
    {
        sprintf(err_string, "bind failed, sock=%d, port=%u\n", sock, *port);
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE,err_string);
        shutdown(sock,2);

        closesocket(sock);

        sock = -1;
    }
    else
    {
        length = sizeof(struct sockaddr_in);
        if ((getsockname(sock, (struct sockaddr *)&sock_addr, &length)) < 0 )
        {
            //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE, "getsockname failed");
            shutdown(sock,2);

            closesocket(sock);

            sock = -1;
        }
        else
        {
            *port = ntohs(sock_addr.sin_port);
        }
    }
    return(sock);
}




void ncs_delete_port(COMM_PORT_NCS *p_del_port, LOG_CONTAINER* lfp)
{

}
void ncs_clear_port(COMM_PORT_NCS * s_port)
{
}
int32_t ncs_get_port (uint16_t  port_id,
                      COMM_PORT_NCS *p_ncs_port,
                      uint16_t  buf_init,
                      uint16_t  bufsize,
                      uint16_t     *state,
                      LOG_CONTAINER* lfp)
{
    *state = E_PARAM;
    return (-1);
}
int32_t ncs_receive(COMM_PORT_NCS * p_rcv_port,
                    NCS_ADDR * rcv_addr,
                    UNITRANS_HEADER *theader,
                    unsigned char **data,
                    uint16_t time_out,
                    uint16_t *p_state,
                    LOG_CONTAINER* lfp)
{
    *p_state = E_PARAM;
    return (-1);
}
int32_t ncs_set_opt(COMM_PORT_NCS *p_port,
                    int32_t optname,
                    const char *optval,
                    int32_t optlen,
                    uint16_t *p_state,
                    LOG_CONTAINER *lfp)
{
    return -1;
}
int32_t ncs_send(COMM_PORT_NCS *p_port,
                 NCS_ADDR *p_addr,
                 UNITRANS_HEADER *header,
                 unsigned char *data,
                 uint16_t time_out,
                 uint16_t *p_state,
                 LOG_CONTAINER *lfp)
{
    *p_state = E_PARAM;
    return (-1);
}

void eth_delete_port(SERVICE_PORT *s_port,
                     LOG_CONTAINER *lfp)
{
}
void eth_clear_port(SERVICE_PORT * s_port)
{
}
int32_t eth_receive(SERVICE_PORT *s_port,
                    GENADDR *rcv_addr,
                    UNITRANS_HEADER *theader,
                    unsigned char **data,
                    uint16_t time_out,
                    uint16_t *state,
                    LOG_CONTAINER *lfp)
{
    *state = E_PARAM;
    return (-1);
}
int32_t eth_send( SERVICE_PORT *s_port,
                 GENADDR *snd_addr,
                 UNITRANS_HEADER *theader,
                 unsigned char *data,
                 uint16_t time_out,
                 uint16_t *state,
                 LOG_CONTAINER *lfp)
{
    *state = E_PARAM;
    return (-1);
}



int32_t eth_set_opt(SERVICE_PORT *s_port,
                    int32_t optname,
                    const char *optval,
                    int32_t optlen,
                    uint16_t *state,
                    LOG_CONTAINER *lfp)
{
    return -1;
}

void eth_delete_port(SERVICE_PORT *s_port,
                     LOG_CONTAINER *lfp);

#define TS_ONLY_MAIN_INTERFACE  (uint16_t)1
#define TS_FOR_ANY_EXCHANGE     (uint16_t)0
#define NUM_TRANS               (uint16_t)15       /*     Max number outstanding trans at port */
#define MAXBUFS                 (uint16_t)30       /*     Maximum # buffers in pool */
#define DEFAULT_INITIAL          20

int32_t eth_get_port(SERVICE_PORT *s_port,
                     uint16_t *state,
                     LOG_CONTAINER *lfp)
{
    *state = E_PARAM;
    return (-1);
}

int32_t RSDURTGUtils_TCPReceive(int sock, char *buf, int32_t len, int32_t timeout, LOG_CONTAINER *lfp, uint32_t dm)
{
    int32_t     rval = 0;
    uint16_t    status = E_OK;


    /* Проверка на корректность сокета */
    if (sock <= 0)
    {
        //RSDURTGUtils_UnilogMessage(LOG_ERR,lfp, "TCPReceive: err, sock is incorrect: sock=%d", sock);
        return(-1);
    }

    if ((buf == (char *)0) || (len <= 0))
    {
        //RSDURTGUtils_UnilogMessage(LOG_ERR,lfp, "TCPReceive: err, ptr rcv data buf - %p,  rcv data len - %d", buf, len);
        return(-1);
    }

    timeout *= 100;

    /*    24.01.2007 Вагин А.А. ставлю сюда tcp_cycle_receive. Чтобы не было дублирования кода в библиотеке    */
    rval = tcp_cycle_receive(sock, buf, len, timeout, &status);
    if (rval < 0)
    {
        if (((EWOULDBLOCK != status) && (EINTR != status)) || dm)
           { ; }// RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)status, ERR_CONTINUE, "TCPReceive: recv() failed");
    }

    if (rval >= 0 && dm)
    {
        //RSDURTGUtils_UnilogMessage(LOG_DEBUG,lfp, "TCPReceive:data len is %d", rval);
    }

    return(rval);
}

int32_t RSDURTGUtils_TCPSend(int sock, char * buf, int32_t len, int32_t timeout, LOG_CONTAINER* fp, uint32_t dm)
{
    int32_t         sval = 0;
    struct timeval  tv;
    fd_set          rfds;
    int32_t         rv;


    /* Проверка на корректность сокета */
    if (sock <= 0)
    {
        //RSDURTGUtils_UnilogMessage(LOG_ERR,fp, "RSDURTGUtils_TCPSend: err, sock is incorrect: sock=%d", sock);
        return(-1);
    }

    /* Проверка на превышение FD_SETSIZE */
    if (sock >= FD_SETSIZE)
    {
        //RSDURTGUtils_UnilogMessage(LOG_ERR,fp, "RSDURTGUtils_TCPSend: Socket %d more FD_SETSIZE(%d)", sock, FD_SETSIZE);
    }


    FD_ZERO(&rfds);
    FD_SET(sock, &rfds);

    if (timeout != WAIT_FOREVER)
    {
        tv.tv_sec = timeout;
        tv.tv_usec = 0;
        rv = select(sock + 1, NULL, &rfds, NULL, &tv);
    }
    else
    {
        rv = select(sock + 1, NULL, &rfds, NULL, NULL);
    }

    if (rv == 0)
    {
        errno = E_TIME;
        return(-1);
    }
    else if (rv < 0)
    {
        if (errno == 0)
            errno = EPIPE;
        if (errno != EINTR)
            { ; }//RSDURTGUtils_ErrCheck( fp, ERR_TCP, __LINE__, __FILE__, (int32_t)errno, ERR_CONTINUE, "TCPSend: select() failed");
        return(-1);
    }

    if((sval = send(sock, buf, len, 0)) <= 0)
    {
        if (errno == 0)
            errno = EPIPE;
        if (errno != EWOULDBLOCK)
        {
            if (errno != EINTR)
               { ; }// RSDURTGUtils_ErrCheck(fp, ERR_TCP, __LINE__, __FILE__, (int32_t)errno, ERR_CONTINUE,
                //"TCPSend: Error while sending data to remote node.\n");
        }
        return (-1);
    }
    if (dm)
    {
        ;//RSDURTGUtils_UnilogMessage(LOG_DEBUG,fp, "RSDURTGUtils_TCPSend:data len is %d", sval);
    }
    return(sval);
}

int32_t RSDURTGUtils_UDPReceive(int sock, char * buf, int32_t len, int32_t timeout, void * rhost_address)
{
    size_t    rhost_address_len = sizeof (struct sockaddr_in);
    uint16_t  status = E_OK;

    if (sock <= 0)
    {
        errno = ENOTSOCK; /* Invalid socket */
        return(-1);
    }

    if (rhost_address == (void*)0)
    {
        errno = EBADF; /* Remote address buffer null */
        return(-1);
    }

    if (buf == (char*)0 || (len == 0))
    {
        errno = E_EXIST; /* No buffer to receive */
        return(-1);
    }

    /*    24.01.2007 Вагин А.А. ставлю сюда timed_recvfrom. Чтобы не было дублирования кода в библиотеке    */
    return (timed_recvfrom(sock,(uint16_t)timeout*100,buf,len,0,(struct sockaddr*)rhost_address,&rhost_address_len, &status));
}



int32_t RSDURTGUtils_UDPSend(int sock, char * buf, int32_t len, int32_t timeout, void * dest_address)
{
    if (sock <= 0)
    {
        errno = E_EXIST; /* Invalid socket */
        return(-1);
    }

    if (dest_address == (void*)0)
    {
        errno = E_EXIST; /* Destination address fail */
        return(-1);
    }

    if (buf == (char*)0 || (len == 0))
    {
        errno = E_EXIST; /* No object to send */
        return(-1);
    }
	
	;

    return(sendto(sock, buf, len, 0, (struct sockaddr*)dest_address, (int32_t)sizeof(struct sockaddr_in)));
}


int udp_clear_sock (int sock)
{
    char    dgram[MAX_UDP_PACKET_LENGTH];
    struct  sockaddr_in  rem_addr;
    fd_set  rfds;
    int32_t rval = 1;
    int32_t count = 0;

    int32_t         rhost_address_len = sizeof (struct sockaddr_in);
    struct timeval  tv;



    while (rval > 0)
    {
        FD_ZERO(&rfds);
        FD_SET(sock, &rfds);
        tv.tv_sec = 0;
        tv.tv_usec = 0;
        rval = select(sock + 1, &rfds, NULL, NULL, &tv);
        if (rval > 0)
        {
            if (FD_ISSET(sock, &rfds))
                rval = recvfrom(sock, dgram, 1, 0, (struct sockaddr*)&rem_addr,  (socklen_t*)&rhost_address_len);
            else
                rval = 0;
            if (rval > 0)
                count += rval;
        }
    }
	return count;
}

int udpex_clear_port (COMM_PORT_IP * s_port)
{
	int count = 0;

	count += udp_clear_sock((*s_port).sock);
	count += udp_clear_sock((*s_port).return_sock);

	return count;
}


void udpex_delete_port(COMM_PORT_IP *s_port, LOG_CONTAINER *lfp)
{
    PACKET_ITEM *pPacketItem = NULL;
    uint32_t     i = 0;
    int          sock = (*s_port).sock;
    int          return_sock = (*s_port).return_sock;


    for (i = 0; i < (*s_port).num_bufstr; i++)
    {
        pPacketItem = &(*s_port).bufstr[i];
        if ((*pPacketItem).rcv_stamp.tv_sec != 0)
        {
            (*pPacketItem).rcv_stamp.tv_sec = 0;
            RSDURTGUtils_SFree((*pPacketItem).databuf);
            (*pPacketItem).databuf = (char*)0;
        }
    }

    if (sock > 0)
    {
        (*s_port).sock = 0;
        shutdown(sock, 2);

        closesocket(sock);


    }
    if (return_sock > 0)
    {
        (*s_port).return_sock = 0;
        shutdown(return_sock, 2);

        closesocket(return_sock);


    }
    memset (s_port, 0, sizeof (COMM_PORT_IP));
}



int32_t udpex_get_port (uint16_t port_id, COMM_PORT_IP *p_udp_port, uint16_t *state, LOG_CONTAINER *lfp)
{
    memset (p_udp_port, 0, sizeof (COMM_PORT_IP));
    /* Создаем основной порт */
    (*p_udp_port).sock = RSDURTGUtils_bound_dgram_socket ((uint16_t *)&port_id, 0, (uint32_t)WAIT_FOREVER, lfp);
    if ((*p_udp_port).sock <= 0)
    {
        if (errno == 0)
            *state = E_EXIST;
        else
            *state = errno;
        (*p_udp_port).sock = 0;
        return(-1);
    }
    (*p_udp_port).port_id = port_id;

#ifdef _DEBUGUDP
    //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_get_port: Create main port %d", port_id);
#endif

    /* Создаем порт для обмена квитанциями, номер порта выделяется динамически */
    port_id = 0;
    (*p_udp_port).return_sock = RSDURTGUtils_bound_dgram_socket ((uint16_t *)&port_id, 0, (uint32_t)WAIT_FOREVER, lfp);
    if ((*p_udp_port).return_sock <= 0)
    {
        if (errno == 0)
            *state = E_EXIST;
        else
            *state = errno;
        udpex_delete_port(p_udp_port, lfp);
        (*p_udp_port).return_sock = 0;
        return(-1);
    }
    (*p_udp_port).accept_port_id = port_id;

#ifdef _DEBUGUDP
    //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_get_port: Create accept port %d", port_id);
#endif

    return(0);
}


int32_t header_list_comp(const void *pm, const void *pd)
{
    UDP_HEADER *m;
    UDP_HEADER *d;

    m = (UDP_HEADER *)pm;
    d = (UDP_HEADER *)pd;
    if ((*m).dg_num > (*d).dg_num) return(1);
    else if((*m).dg_num < (*d).dg_num) return(-1);
    else return(0);
}

int32_t header_list_key_comp(const void *key, const void *p_hdr)
{
    uint32_t   *k;
    UDP_HEADER *hdr;

    k = (uint32_t *)key;
    hdr = (UDP_HEADER *)p_hdr;
    if (*k > (*hdr).dg_num) return(1);
    else if(*k < (*hdr).dg_num) return(-1);
    else return(0);
}

int32_t packet_list_comp(const void *pm, const void *pd)
{
    PACKET_ITEM *m;
    PACKET_ITEM *d;

    m = (PACKET_ITEM *)pm;
    d = (PACKET_ITEM *)pd;
    if ((*m).rcv_stamp.tv_sec > (*d).rcv_stamp.tv_sec) return(-1);
    else if((*m).rcv_stamp.tv_sec < (*d).rcv_stamp.tv_sec) return(1);
    else return(0);
}

int32_t timed_recvfrom(int sock, uint16_t time_out, char *dgram, int32_t length, int32_t flag, struct sockaddr *r_addr, size_t *r_addr_len, uint16_t *status)
{
    int32_t rval = 0;
    fd_set  rfds;


    struct  timeval tv;
    int32_t *pr_addr_len = (int32_t*)r_addr_len;


    *status = E_OK;

    FD_ZERO(&rfds);
    FD_SET(sock, &rfds);

    if (time_out != WAIT_FOREVER)
    {
        tv.tv_sec = time_out/100;
        tv.tv_usec = (time_out - tv.tv_sec * 100) * 10000; /* to microseconds */
        rval = select(sock + 1, &rfds, NULL, NULL, &tv);
    }
    else
    {
        /* wait forever */
        rval = select(sock + 1, &rfds, NULL, NULL, NULL);
    }

    if (rval == 0)
    {
        errno = E_TIME;
        *status = E_TIME;
        return (-1);
    }
    else if (rval < 0)
    {
        if (errno == 0)
        {
            *status = E_TIME;
            errno = E_TIME;
        }
        else
        {
            *status = errno;
        }
        return (-1);
    }

    memset(dgram, 0, (size_t)length);
    rval = recvfrom(sock, dgram, length, 0, (struct sockaddr*)r_addr, (socklen_t*)pr_addr_len);
    if (rval <= 0)
    {
        if (errno == 0)
        {
            *status = E_TIME;
            errno = E_TIME;
        }
        else
        {
            *status = errno;
        }
        return(-1);
    }
    return (rval);
}

int32_t udpex_receive (COMM_PORT_IP    *s_port,
                       IP_ADDR         *rcv_addr,
                       UNITRANS_HEADER *theader,
                       unsigned char  **data,
                       uint16_t         time_out,
                       uint16_t        *state,
                       LOG_CONTAINER   *lfp)
{
    UDP_HEADER      *pUdpHeader = NULL, *pSearchUdpHeader = NULL;
    PACKET_ITEM     *pPacketItem = NULL, *pInsertPacket = NULL;
    char             dgram[MAX_UDP_PACKET_LENGTH];
    size_t           theader_size = sizeof (UNITRANS_HEADER);
    size_t           dgram_headlen = sizeof (UDP_HEADER);
    size_t           dgram_datalen = MAX_UDP_PACKET_LENGTH - dgram_headlen;
    uint32_t         rcv_len = 0;
    struct           sockaddr_in rcv_sockaddr;
    size_t           sockaddr_size = sizeof (struct sockaddr_in);
    uint16_t         i;
    IP_ADDR          ip_addr;
    int32_t          r_size;
    struct           timeval tv;
    char            *resize_buf = (char*)0;
    const size_t     cszAddressSize = MAX_HOST_NAME_LEN + 6;
    char             sz_address[cszAddressSize];
    REM_ADDR         rem_addr;


    if (theader == (UNITRANS_HEADER *)0)
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck(lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "udpex_receive: UNITRANS_HEADER buffer is NULL");
        return(-1);
    }
    *state = 0;
    *data = NULL;


    tv = RSDURTGUtils_TimeTick();

    for (i = 0; i < (*s_port).num_bufstr; i++)
    {
        pPacketItem = &(*s_port).bufstr[i];
        if ((*pPacketItem).rcv_stamp.tv_sec != 0)
        {
            if (tv.tv_sec > (*pPacketItem).dead_stamp.tv_sec + 1)    /*    Защита +1 секунда. На случай разрешения таймера 1 секунда    */
            {
                (*pPacketItem).rcv_stamp.tv_sec = 0;
                RSDURTGUtils_SFree((*pPacketItem).databuf);
                (*pPacketItem).databuf = (char*)0;
#ifdef _DEBUGUDP
                rem_addr.type = ADV_PROTO_UDP;
                rem_addr.addr.addr_ip = (*pPacketItem).ip_addr;
                //RSDURTGUtils_UnilogMessage(LOG_ERR,lfp, "udpex_receive: Drop dead packet - trans_id %d, %d bytes received out of %d, address %s", 
                //    (*pPacketItem).trans_id, 
                //    (*pPacketItem).length - (*pPacketItem).rcv_size, 
                //    (*pPacketItem).length, 
                //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif
            }
        }
    }

    /*    Сжатие буфера    */
    if ((*s_port).num_bufstr > 0)
    {
        qsort((*s_port).bufstr, (size_t)(*s_port).num_bufstr, sizeof(PACKET_ITEM), packet_list_comp);
        for (i = 0; i < (*s_port).num_bufstr; i++)
            if ((*s_port).bufstr[i].rcv_stamp.tv_sec == 0)
                break;
        (*s_port).num_bufstr = i;
    }

    while (1)
    {
        /* Пытаемся получить очередную дейтаграмму */
        r_size = timed_recvfrom((*s_port).sock, time_out, dgram, MAX_UDP_PACKET_LENGTH, 0, (struct sockaddr*)&rcv_sockaddr, &sockaddr_size, state);
        
        ip_addr.ip_addr = rcv_sockaddr.sin_addr.s_addr;
        ip_addr.ip_port = ntohs(rcv_sockaddr.sin_port);

        rem_addr.type = ADV_PROTO_UDP;
        rem_addr.addr.addr_ip = ip_addr;

        if (r_size < 0)
        {
            if (*state != EWOULDBLOCK)
            {
                if (*state != EINTR)    /*    Как правило, завершение процесса - ошибку подавляем    */
                {
                    ;//RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_receive: recvfrom() failed, address %s", RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
                }
            }
            break;
        }

        pUdpHeader = (UDP_HEADER*)dgram;

#ifdef _DEBUGUDP
        //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_receive: timed_recvfrom: OK, trans_id %d, length %d, dg_num %d, address %s", 
        //    pUdpHeader->trans_id,
        //    pUdpHeader->length,
        //    pUdpHeader->dg_num,
        //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif

        /* Контроль длины дейтаграммы. Длина дейтаграммы является фиксированной! */
        if (r_size < MAX_UDP_PACKET_LENGTH)
        {
#ifdef _DEBUGUDP
            //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_receive: we will not accept datagram of %d bytes, which is less than %d, address %s",
            //    r_size,
            //    MAX_UDP_PACKET_LENGTH,
            //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif
            continue;
        }

        /* Контроль версии транспорта */
        if (((*pUdpHeader).version != UDP_VERSION) || 
            ((*pUdpHeader).cntl > (CNTL_COMMON | CNTL_BROADCAST | CNTL_ACCEPT | CNTL_CLOCLSYNC | CNTL_PRIORITY)) || 
            ((*pUdpHeader).status > STATE_KVIT_FALSE))
        {
            //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_receive: the datagram has an unknown format, version=%d cntl=%d status=%d, address %s",
            //    (*pUdpHeader).version,
            //    (*pUdpHeader).cntl,
            //    (*pUdpHeader).status,
            //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
            continue;
        }

        pInsertPacket   = (PACKET_ITEM*)0;
        pPacketItem     = (PACKET_ITEM*)0;

        /* Поиск структуры пакета для принятой дейтаграммы */
        for (i = 0; i < (*s_port).num_bufstr; i++)
        {
            if ((*s_port).bufstr[i].rcv_stamp.tv_sec == 0)
            {
                /* Возможно, это дейтаграмма является первым пакетом от новой открытой сессии? */
                if (pInsertPacket == (PACKET_ITEM*)0)
                    pInsertPacket = &(*s_port).bufstr[i];
            }
            else if (ip_addr.ip_addr == (*s_port).bufstr[i].ip_addr.ip_addr
                && ip_addr.ip_port == (*s_port).bufstr[i].ip_addr.ip_port
                && (*pUdpHeader).trans_id == (*s_port).bufstr[i].trans_id)
            {
                /* Обнаружили уже начатую сессию. Добавим этот пакет к этой сессии */
#ifdef _DEBUGUDP
                //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_receive: Found packet buffer[%d]: dg_num %d, trans_id %d, address %s", 
                //    i, 
                //    (*pUdpHeader).dg_num, 
                //    (*pUdpHeader).trans_id, 
                //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif
                pPacketItem = &(*s_port).bufstr[i];
                break;
            }
            else if (ip_addr.ip_port == (*s_port).bufstr[i].ip_addr.ip_port
                && (*pUdpHeader).trans_id == (*s_port).bufstr[i].trans_id)
            {
                /* Напечатаем сообщение в лог о том что найдена существующая сессия (на случай, если пакет придет с другого адреса) */
                //RSDURTGUtils_UnilogMessage(LOG_WARNING, lfp, "udpex_receive: Found packet buffer[%d], bit with not the same address: dg_num %d, trans_id %d, address %s", 
                //    i, 
                //    (*pUdpHeader).dg_num, 
                //    (*pUdpHeader).trans_id, 
                //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
            }

        }

        /* Создаем структуру для нового пакета */
        if (pPacketItem == (PACKET_ITEM*)0)
        {
            if (pInsertPacket == (PACKET_ITEM*)0)
            {
                if ((*s_port).num_bufstr >= MAX_SESSION_TO_PORT)
                {
                    *state = E_MEM;
                    //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_receive: Session number overflow (maximum %d), address %s", 
                    //    MAX_SESSION_TO_PORT,
                    //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
                    break;
                }
                pInsertPacket = &(*s_port).bufstr[(*s_port).num_bufstr];
                (*s_port).num_bufstr++;
            }
            pPacketItem = pInsertPacket;
            (*pPacketItem).databuf = (char*)RSDURTGUtils_SCalloc((uint32_t)(*pUdpHeader).length, sizeof (char));
            if ((*pPacketItem).databuf == (char*)0)
            {
                *state = E_MEM;
                //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_receive: RSDURTGUtils_SCalloc(%u bytes) failed, address %s", 
                //    (*pUdpHeader).length,
                //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
                break;
            }
            (*pPacketItem).ip_addr = ip_addr;
            (*pPacketItem).rcv_stamp = timetv2rsdutv( RSDURTGUtils_TimeTick() );
            tv.tv_sec = (*pUdpHeader).snd_timeout/100;
            tv.tv_usec = ((*pUdpHeader).snd_timeout - tv.tv_sec*100)*10000;
            (*pPacketItem).dead_stamp.tv_sec = (*pPacketItem).rcv_stamp.tv_sec + tv.tv_sec;
            (*pPacketItem).dead_stamp.tv_usec = (*pPacketItem).rcv_stamp.tv_usec + tv.tv_usec;
            (*pPacketItem).length = (*pUdpHeader).length;
            (*pPacketItem).rcv_size = (*pUdpHeader).length;
            (*pPacketItem).num_hdr = 0;
            (*pPacketItem).top_dg_num = -1;
            (*pPacketItem).trans_id = (*pUdpHeader).trans_id;

#ifdef _DEBUGUDP
        //    RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_receive: Create packet buffer: dg_num %d, trans_id %d, length %d, address %s",
        //        (*pUdpHeader).dg_num,
        //        (*pUdpHeader).trans_id,
        //        (*pPacketItem).length,
        //        RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif
        }

        /*    Проверяем - может пришела квитанция - признак конца пакета    */
        if ((*pPacketItem).rcv_size == 0 
            && (*pUdpHeader).cntl == CNTL_ACCEPT
            && (*pUdpHeader).status == STATE_KVIT_TRUE)
        {
#ifdef _DEBUGUDP
        //    RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_receive: Receive common packet accept for trans_id %d, address %s", 
        //        (*pUdpHeader).trans_id,
        //        RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif
            break;
        }

        pSearchUdpHeader = (UDP_HEADER*)0;
        if ((*pPacketItem).num_hdr != 0)
            pSearchUdpHeader = (UDP_HEADER*)bsearch(&(*pUdpHeader).dg_num, (*pPacketItem).hdr, (size_t)(*pPacketItem).num_hdr, sizeof (UDP_HEADER), header_list_key_comp);

        if (pSearchUdpHeader != (UDP_HEADER*)0)    /* Может, эту дейтаграмму уже приняли */
        {
#ifdef _DEBUGUDP
        //    RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_receive: trans_is %d, received dg_num %d is already in buffer, address %s", 
        //        (*pUdpHeader).trans_id,
        //        (*pUdpHeader).dg_num,
        //        RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif
        }
        else
        {
            (*pPacketItem).hdr[(*pPacketItem).num_hdr] = *pUdpHeader;
            (*pPacketItem).num_hdr++;
            qsort((*pPacketItem).hdr, (size_t)(*pPacketItem).num_hdr, sizeof (UDP_HEADER), header_list_comp);
            if ((int32_t)(*pUdpHeader).dg_num > (*pPacketItem).top_dg_num)    /*    Дейтаграмма еще не принималась    */
            {
                if ((*pUdpHeader).dg_num == (*pPacketItem).length / dgram_datalen)
                    rcv_len = (*pPacketItem).length - (*pUdpHeader).dg_num * dgram_datalen;
                else
                    rcv_len = dgram_datalen;
                /* Вставляем содержимое дейтаграммы в общий пакет */
                memcpy ((*pPacketItem).databuf + (*pUdpHeader).dg_num * dgram_datalen, dgram + dgram_headlen, (size_t)rcv_len);
                (*pPacketItem).rcv_size -= rcv_len;
            }
        }

#ifdef _DEBUGUDP
        //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_receive: trans_is %d, need to receive: %d bytes, num_hdr %d, address %s",
        //    (*pUdpHeader).trans_id,
        //    (*pPacketItem).rcv_size,
        //    (*pPacketItem).num_hdr,
        //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif

        /* Еще не конец пакета, и окно не принято, и это не широковещательный пакет    */
        if (((*pPacketItem).num_hdr != DEF_WINDOW_SIZE && (*pPacketItem).rcv_size != 0)
            || (((*pUdpHeader).cntl & CNTL_BROADCAST) == CNTL_BROADCAST))
            continue;

        /* Отправка квитанции */
        pSearchUdpHeader = &(*pPacketItem).hdr[(*pPacketItem).num_hdr - 1];
        if ((int32_t)(*pSearchUdpHeader).dg_num > (*pPacketItem).top_dg_num)
            (*pPacketItem).top_dg_num = (*pSearchUdpHeader).dg_num;

        (*pPacketItem).num_hdr = 0;

        (*pUdpHeader).cntl = CNTL_ACCEPT;
        (*pUdpHeader).length = 0;
        (*pUdpHeader).version = UDP_VERSION;
        (*pUdpHeader).status = STATE_KVIT_TRUE;

#ifdef _DEBUGUDP
        //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_receive: Send accept for dg_num %d, trans_id %d, to port %d", 
        //    (*pUdpHeader).dg_num,
        //    (*pUdpHeader).trans_id, 
        //    (*pUdpHeader).return_port);
#endif

        rcv_sockaddr.sin_port = htons((uint16_t)(*pUdpHeader).return_port);
        if (sendto((*s_port).sock, (char *)dgram, MAX_UDP_PACKET_LENGTH, 0, (struct sockaddr*)&rcv_sockaddr, (int32_t)sockaddr_size) < 0)
        {
            (*pPacketItem).rcv_stamp.tv_sec = 0;
            RSDURTGUtils_SFree((*pPacketItem).databuf);
            (*pPacketItem).databuf = (char*)0;
            if (errno == 0)
                *state = E_TIME;
            else
                *state = errno;
            if (*state != EWOULDBLOCK)
            {
                if (*state != EINTR)
				{ ; }//  RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "udpex_receive: sendto() CNTL_ACCEPT failed");
            }
            break;
        }
    }

    if (*state != 0)
    {
        memset (theader, 0, sizeof (UNITRANS_HEADER));
        return(-1);
    }

    /* Получение данных прикладного уровня */
    if (pPacketItem !=  (PACKET_ITEM*)0)
    {
        /* Копируем заголовок UNITRANS_HEADER */
        memcpy (theader, (*pPacketItem).databuf, theader_size);
        if ((*theader).data_len > 0)
        {
            /* Выделяем кусок с данными, который следует за заголовком UNITRANS_HEADER */
            memmove((*pPacketItem).databuf, (*pPacketItem).databuf + theader_size, (size_t)(*theader).data_len);
            resize_buf = (char*)RSDURTGUtils_SRealloc((*pPacketItem).databuf, (uint32_t)(*theader).data_len);
            if (resize_buf == (char*)0)
            {
                *state = E_MEM;
                RSDURTGUtils_SFree((*pPacketItem).databuf);
                (*pPacketItem).databuf = NULL;
                memset (theader, 0, sizeof (UNITRANS_HEADER));
                //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_receive: RSDURTGUtils_SRealloc() for PACKET_ITEM failed, address %s",
                //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
                return (-1);
            }
            else
                *data = (unsigned char*)resize_buf;
        }
        else
        {
            RSDURTGUtils_SFree((*pPacketItem).databuf);
        }
    }
    else
    {
        memset (theader, 0, sizeof (UNITRANS_HEADER));
        *state = E_MEM;
        //RSDURTGUtils_ErrCheck(lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_MEM, ERR_CONTINUE, "udpex_receive: Failed buffer for PACKET_ITEM");
        return(-1);
    }

    (*pPacketItem).databuf = (char*)0;
    (*pPacketItem).rcv_stamp.tv_sec = 0;
    *rcv_addr = ip_addr;
    return (0);
}

int32_t udp_bcflag(int sock, IP_ADDR *snd_addr, uint16_t *state, LOG_CONTAINER *lfp)
{
    int32_t SetBroadCastFlag = 0;

    SetBroadCastFlag = 1;
    if (setsockopt(sock, SOL_SOCKET, SO_BROADCAST, (char *)&SetBroadCastFlag, (socklen_t)sizeof(SetBroadCastFlag)) < 0)
    {
        if (errno == 0)
            *state = E_PARAM;
        else
            *state = errno;
        //RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "udp_bcflag: setsockopt() failed");
        return (-4);
    }
    return(0);
}


int32_t udpex_send (COMM_PORT_IP    *s_port,
                    IP_ADDR         *snd_addr,
                    UNITRANS_HEADER *theader,
                    unsigned char   *data,
                    uint16_t         time_out,
                    uint16_t        *state,
                    LOG_CONTAINER   *lfp)
{
    UDP_HEADER  udp_header, *pUdpHeader = NULL;
    char        dgram[MAX_UDP_PACKET_LENGTH];
    char       *sbuf = (char*)0;
    size_t      theader_size = sizeof (UNITRANS_HEADER);
    size_t      dgram_headlen = sizeof (UDP_HEADER);
    size_t      dgram_datalen = MAX_UDP_PACKET_LENGTH - dgram_headlen;
    int32_t     snd_size = 0;
    uint32_t    snd_len = 0;
    uint32_t    slen = 0;
    uint32_t    dg_num = 0, window_dg_num = 0;
    uint32_t    ip_addr_broadcust;
    struct      sockaddr_in  snd_sockaddr, rcv_addr;
    size_t      sockaddr_size = sizeof (struct sockaddr_in);
    unsigned char   retry;
#ifdef _DEBUGUDP
    unsigned char   last_retry;
    const size_t    cszAddressSize = MAX_HOST_NAME_LEN + 6;
    char            sz_address[cszAddressSize];
#endif
    REM_ADDR        rem_addr;
    

	int clear_cnt = 0;

    if (theader == (UNITRANS_HEADER *)0)
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck(lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "udpex_send: UNITRANS_HEADER buffer is NULL");
        return(-1);
    }

    if (((*theader).data_len > 0) && (data == (unsigned char *)0))
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck(lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "udpex_send: data buffer is NULL");
        return(-1);
    }

    slen = (*theader).data_len + theader_size;

    (*s_port).trans_id++;
    *state = E_OK;
    if (udp_bcflag((*s_port).sock, snd_addr, state, lfp) != 0) return (-1);

    snd_sockaddr.sin_addr.s_addr = (*snd_addr).ip_addr;
    snd_sockaddr.sin_port = htons((*snd_addr).ip_port);
    snd_sockaddr.sin_family = AF_INET;

    sbuf = (char*)RSDURTGUtils_SMalloc((uint32_t)slen);
    if (sbuf == (char*)0)
    {
        *state = E_MEM;
        //RSDURTGUtils_ErrCheck(lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "udpex_send: RSDURTGUtils_SMalloc() for sbuf failed");
        return (-1);
    }

    memcpy (sbuf, theader, theader_size);
    if ((*theader).data_len > 0)
        memcpy (sbuf + theader_size, data, (size_t)(*theader).data_len);

    snd_size = slen;
    window_dg_num = 0;
    retry = 0;

#ifdef _DEBUGUDP
    last_retry = 0;
#endif

    while(snd_size > 0)
    {
        if (retry > DEF_RETRY_NUM)
        {
#ifdef _DEBUGUDP
            //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_send: Retry count %d, trans_id %d, exit", DEF_RETRY_NUM, (*s_port).trans_id);
#endif
            break;
        }

#ifdef _DEBUGUDP
        //else if (retry > last_retry)
        //{
        //    last_retry = retry;
        //    RSDURTGUtils_UnilogMessage(LOG_WARNING, lfp, "udpex_send: Retry %d, trans_id %d", retry, (*s_port).trans_id);
        //}
#endif

        if ((size_t)snd_size > dgram_datalen)
            snd_len = dgram_datalen;
        else
        {
            snd_len = snd_size;
            memset (dgram + dgram_headlen + snd_len, 0, (size_t)(dgram_datalen - snd_len));
        }

        if ((*snd_addr).ip_addr == INADDR_BROADCAST)    /* Широковещательная рассылка */

            udp_header.cntl = CNTL_BROADCAST;
        else
            udp_header.cntl = CNTL_COMMON;

        udp_header.length = slen;
        udp_header.priority = 0;
        udp_header.return_port = (*s_port).accept_port_id;
        udp_header.version = UDP_VERSION;
        udp_header.trans_id = (*s_port).trans_id;
        udp_header.snd_timeout = time_out;

        if (retry != 0)
            udp_header.status = STATE_RETRY;
        else
            udp_header.status = STATE_OK;
        udp_header.dg_num = dg_num;

        memcpy (dgram, &udp_header, dgram_headlen);
        memcpy (dgram + dgram_headlen, (void *)(sbuf + udp_header.dg_num * dgram_datalen), (size_t)snd_len);

        rem_addr.type = ADV_PROTO_UDP;
        rem_addr.addr.addr_ip = *snd_addr;

#ifdef _DEBUGUDP
        //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_send: Send packet dg_num %d, trans_id %d, to address %s", 
        //    udp_header.dg_num,
        //    udp_header.trans_id,
        //    RSDURTGUtils_AddressToStr_r(&rem_addr, sz_address, cszAddressSize));
#endif

        clear_cnt = udp_clear_sock ((*s_port).return_sock);
        if (clear_cnt > 0)
		{ ; } //RSDURTGUtils_UnilogMessage(LOG_WARNING, lfp, "udpex_send: return_sock was cleared from %d bytes", clear_cnt);
		
        if (sendto((*s_port).sock, (char *)dgram, MAX_UDP_PACKET_LENGTH, 0, (struct sockaddr*)&snd_sockaddr, (int32_t)sockaddr_size) < 0)
        {
            if (errno == 0)
                *state = E_TIME;
            else
                *state = errno;

            if (*state != EWOULDBLOCK)
            {
                if (*state != EINTR)
                   { ; }//RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "udpex_send: sendto() failed");
            }
            break;
        }

        window_dg_num++;
        dg_num++;
        snd_size = slen - dg_num * dgram_datalen;

        if ((snd_size > 0 && window_dg_num != DEF_WINDOW_SIZE) || ((udp_header.cntl & CNTL_BROADCAST) == CNTL_BROADCAST))
            continue;

        /* Прием квитанций */
        if (timed_recvfrom ((*s_port).return_sock, time_out, dgram, MAX_UDP_PACKET_LENGTH, 0, (struct sockaddr *)&rcv_addr, &sockaddr_size, state) < 0)
        {
#ifdef _DEBUGUDP
            //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_send: Error receive accept packet");
#endif
            retry++;
            if (dg_num < DEF_WINDOW_SIZE)
                dg_num = 0;
            else
                dg_num -= DEF_WINDOW_SIZE;
        }
        else
        {
            pUdpHeader = (UDP_HEADER*)dgram;
            if ((*pUdpHeader).cntl == CNTL_ACCEPT
                && (*pUdpHeader).version  == UDP_VERSION
                && (*pUdpHeader).status   == STATE_KVIT_TRUE
                && (*pUdpHeader).trans_id == udp_header.trans_id
              //&& rcv_addr.sin_addr.s_addr == (*snd_addr).ip_addr
                && (*snd_addr).ip_port == ntohs (rcv_addr.sin_port))
            {
                retry = 0;
                *state = E_OK;
#ifdef _DEBUGUDP
                //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_send: Received accept trans_id %d", udp_header.trans_id);
#endif
            }
            else
            {
#ifdef _DEBUGUDP
                //RSDURTGUtils_UnilogMessage(LOG_ERR, lfp, "udpex_send: Received a broken (or invalid) accept, trans_id %d", (*pUdpHeader).trans_id);
#endif
                retry++;
                *state = EINVAL;
                if (dg_num < DEF_WINDOW_SIZE)
                    dg_num = 0;
                else
                    dg_num -= DEF_WINDOW_SIZE;
            }
        }
        snd_size = slen - dg_num * dgram_datalen;
        window_dg_num = 0;
    }

    if (snd_size <= 0)
    {
        /* Отправка квитанции */
        udp_header.cntl = CNTL_ACCEPT;
        udp_header.length = 0;
        udp_header.version = UDP_VERSION;
        udp_header.status = STATE_KVIT_TRUE;

#ifdef _DEBUGUDP
        //RSDURTGUtils_UnilogMessage(LOG_INFO, lfp, "udpex_send: Send common packet accept for trans_id %d", udp_header.trans_id);
#endif

        memcpy (dgram, &udp_header, dgram_headlen);
        memset (dgram + dgram_headlen, 0, dgram_datalen);
        if (sendto((*s_port).sock, (char *)dgram, MAX_UDP_PACKET_LENGTH, 0, (struct sockaddr*)&snd_sockaddr, (int32_t)sockaddr_size) < 0)
        {
            if (errno == 0)
                *state = E_TIME;
            else
                *state = errno;
            if (*state != EWOULDBLOCK)
            {
                if (*state != EINTR)
                   { ; }// RSDURTGUtils_ErrCheck(lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "udpex_send: sendto() failed");
            }
        }
    }

    if (sbuf != NULL)
        RSDURTGUtils_SFree(sbuf);

    if (snd_size > 0 || *state != E_OK)
    {
        return(-1);
    }

    return(0);
}



void _udpex_clear_port (COMM_PORT *s_port) { udpex_clear_port(&(*s_port).service.port_ip); }
void _udpex_delete_port(COMM_PORT *s_port, LOG_CONTAINER *lfp) { udpex_delete_port(&(*s_port).service.port_ip, lfp); }
int32_t  _udpex_send      (COMM_PORT *s_port,
                           REM_ADDR *snd_addr,
                           UNITRANS_HEADER *theader,
                           unsigned char *data,
                           uint16_t time_out,
                           uint16_t *state,
                           LOG_CONTAINER *lfp)
{
    return (udpex_send(&(*s_port).service.port_ip, &(*snd_addr).addr.addr_ip, theader, data, time_out, state, lfp));
}

int32_t _udpex_receive    (COMM_PORT *s_port,
                           REM_ADDR *rcv_addr,
                           UNITRANS_HEADER *theader,
                           unsigned char **data,
                           uint16_t time_out,
                           uint16_t *state,
                           LOG_CONTAINER *lfp)
{
    return (udpex_receive    (&(*s_port).service.port_ip, &(*rcv_addr).addr.addr_ip, theader, data, time_out, state, lfp));
}


void _eth_clear_port (COMM_PORT *s_port) { eth_clear_port(&(*s_port).service.port_eth); }
void _eth_delete_port(COMM_PORT *s_port, LOG_CONTAINER *lfp) { eth_delete_port(&(*s_port).service.port_eth, lfp); }
int32_t  _eth_send      (COMM_PORT *s_port,
                         REM_ADDR *snd_addr,
                         UNITRANS_HEADER *theader,
                         unsigned char *data,
                         uint16_t time_out,
                         uint16_t *state,
                         LOG_CONTAINER *lfp)
{
    return (eth_send(&(*s_port).service.port_eth, &(*snd_addr).addr.addr_eth, theader, data, time_out, state, lfp));
}

int32_t _eth_receive    (COMM_PORT *s_port,
                         REM_ADDR *rcv_addr,
                         UNITRANS_HEADER *theader,
                         unsigned char **data,
                         uint16_t time_out,
                         uint16_t *state,
                         LOG_CONTAINER *lfp)
{
    return (eth_receive    (&(*s_port).service.port_eth, &(*rcv_addr).addr.addr_eth, theader, data, time_out, state, lfp));
}

int32_t _eth_set_opt    (COMM_PORT *s_port,
                         int32_t optname,
                         const char *optval,
                         int32_t optlen,
                         uint16_t *p_state,
                         LOG_CONTAINER *lfp)
{
    return (eth_set_opt    (&(*s_port).service.port_eth, optname, optval, optlen, p_state, lfp));
}

void _ncs_clear_port (COMM_PORT * s_port) { ncs_clear_port(&(*s_port).service.port_ncs); }
void _ncs_delete_port(COMM_PORT *s_port, LOG_CONTAINER* lfp) { ncs_delete_port(&(*s_port).service.port_ncs, lfp); }
int32_t  _ncs_send     (COMM_PORT *s_port,
                        REM_ADDR *snd_addr,
                        UNITRANS_HEADER *theader,
                        unsigned char *data,
                        uint16_t time_out,
                        uint16_t *state,
                        LOG_CONTAINER *lfp)
{
    return (ncs_send(&(*s_port).service.port_ncs, &(*snd_addr).addr.addr_ncs, theader, data, time_out, state, lfp));
}
int32_t _ncs_receive    (COMM_PORT *s_port,
                         REM_ADDR *rcv_addr,
                         UNITRANS_HEADER *theader,
                         unsigned char **data,
                         uint16_t time_out,
                         uint16_t *state,
                         LOG_CONTAINER *lfp)
{
    return (ncs_receive    (&(*s_port).service.port_ncs, &(*rcv_addr).addr.addr_ncs, theader, data, time_out, state, lfp));
}
int32_t _ncs_set_opt    (COMM_PORT* s_port,
                         int32_t optname,
                         const char *optval,
                         int32_t optlen,
                         uint16_t *p_state,
                         LOG_CONTAINER *lfp)
{
    return(ncs_set_opt    (&(*s_port).service.port_ncs, optname, optval, optlen, p_state, lfp));
}

void _ip_delete_port(COMM_PORT * s_port, LOG_CONTAINER* lfp)
{
    int sock = (*s_port).service.port_ip.sock;
    
    if (sock > 0)
    {
        memset (&(*s_port).service.port_ip, 0, sizeof (COMM_PORT_IP));
        shutdown(sock,2);

        closesocket(sock);

    }
}


int32_t  _tcp_send (COMM_PORT *s_port,
                        REM_ADDR *snd_addr,
                        UNITRANS_HEADER *theader,
                        unsigned char *data,
                        uint16_t time_out,
                        uint16_t *state,
                        LOG_CONTAINER *lfp)
{
    unsigned char  *Sbuf = NULL;
    int32_t         rv;
    int32_t         sndsize = 0;
    int             sock = (*s_port).service.port_ip.sock;
    struct timeval  tv;
    struct fd_set          rfds;


    *state = E_OK;

    if (sock <= 0)
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck( lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "_tcp_send: not connected socket");
        return(-1);
    }

    if (theader == (UNITRANS_HEADER *)0)
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck( lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "_tcp_send: Failed UNITRANS_HEADER buffer");
        return(-1);
    }

    if (((*theader).data_len > 0) && (data == (unsigned char *)0))
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck( lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "_tcp_send: Failed data buffer");
        return(-1);
    }

    if ((*s_port).service.port_ip.remote_address.ip_port == 0) /* Is socket connected */
    {
        *state = E_EXIST;
        //RSDURTGUtils_UnilogMessage(LOG_ERR,lfp,"_tcp_send: not connected socket");
        return (-1);
    }

    sndsize = sizeof (UNITRANS_HEADER);
    if ((*theader).data_len > 0)
    {
        if((Sbuf = (unsigned char *)RSDURTGUtils_SMalloc((uint32_t)(*theader).data_len + sndsize)) == (unsigned char *)0)
        {
            *state = E_MEM;
            //RSDURTGUtils_ErrCheck( lfp, ERR_RMX, __LINE__, __FILE__, (int32_t)E_MEM, ERR_CONTINUE, "_tcp_send: RSDURTGUtils_SMalloc() failed");
            return (-1);
        }
        memcpy (Sbuf, theader, (size_t)sndsize);
        memcpy (Sbuf + sndsize,data,(size_t)(*theader).data_len);
        sndsize+=(*theader).data_len;
    }
    else Sbuf = (unsigned char *)theader;

    FD_ZERO(&rfds);
    FD_SET(sock, &rfds);

    if (time_out != WAIT_FOREVER)
    {
        tv.tv_sec = time_out/100;
        tv.tv_usec = (time_out - tv.tv_sec * 100) * 10000;
        rv = select(sock + 1, NULL, &rfds, NULL, &tv);
    }
    else
    {
        rv = select(sock + 1, NULL, &rfds, NULL, NULL);
    }

    if (rv == 0)
    {
        errno = E_TIME;
        *state = E_TIME;
        return(-1);
    }
    else if (rv < 0)
    {
        if (errno == 0)
            *state = EPIPE;
        else
            *state = errno;
        if (*state != EINTR)
            { ; }//RSDURTGUtils_ErrCheck( lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "_tcp_send: select() failed");
        return(-1);
    }

    rv = send(sock, (char *)Sbuf, (int32_t)sndsize, 0);

    if (Sbuf != (unsigned char *)theader)
        RSDURTGUtils_SFree(Sbuf);

    if (rv <= 0)
    {
        if (errno == 0)
        {
            *state = EPIPE;
            errno = EPIPE;
        }
        else
            *state = errno;
        if (*state != EWOULDBLOCK)
        {
            if (*state != EINTR)
              { ; }//  RSDURTGUtils_ErrCheck( lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "_tcp_send: send() failed");
        }
        return(-1);
    }

    if (rv != sndsize)
    {
        *state = EINVAL;
        errno = EINVAL;
        //RSDURTGUtils_UnilogMessage(LOG_ERR,lfp,"_tcp_send: sent %d bytes, but set %d", rv, sndsize);
        return(-1);
    }
    return(0);
}

int32_t tcp_cycle_receive(int sock, char* buf, uint32_t dlength, uint16_t timeout, uint16_t *status)
{
    int32_t         rv, rd, rval;
    struct timeval  tv;
    fd_set          rfds;

    rval = 0;
    rd = dlength;

    *status = E_OK;

    while (rd > 0)
    {
        FD_ZERO(&rfds);
        FD_SET(sock, &rfds);

        if (timeout != WAIT_FOREVER)
        {
            tv.tv_sec = timeout/100;
            tv.tv_usec = (timeout - tv.tv_sec * 100) * 10000;
            rv = select(sock + 1, &rfds, NULL, NULL, &tv);
        }
        else
        {
            /* wait forever */
            rv = select(sock + 1, &rfds, NULL, NULL, NULL);
        }

        if (rv == 0)
        {
            errno = E_TIME;
            *status = E_TIME;
            return(-1);
        }
        else if (rv < 0)
            break;

        rv = recv(sock, (char *)buf + (dlength - rd), (int32_t)rd, 0);
        if (rv <= 0)
            break;
        else 
            rval += rv;
        rd = rd - rv;
    }
    if (rv <= 0)
    {
        if (errno == 0)
        {
            *status = EPIPE;
            errno = EPIPE;
        }
        else
            *status = errno;
        return(-1);
    }
    return(rval);
}

int32_t _tcp_receive    (COMM_PORT *s_port,
                         REM_ADDR *rcv_addr,
                         UNITRANS_HEADER *theader,
                         unsigned char **data,
                         uint16_t time_out,
                         uint16_t *state,
                         LOG_CONTAINER *lfp)
{
    unsigned char *Sbuf = NULL;
    int sock = (*s_port).service.port_ip.sock;
    *state = E_OK;
    *data = NULL;

    if (sock <= 0) /* Is socket connected */
    {
        *state = E_EXIST;
        //RSDURTGUtils_UnilogMessage(LOG_ERR,lfp,"_tcp_receive: not connected socket");
        return (-1);
    }

    if ((*s_port).service.port_ip.remote_address.ip_port == 0) /* Is socket connected */
    {
        *state = E_EXIST;
        //RSDURTGUtils_UnilogMessage(LOG_ERR,lfp,"_tcp_receive: not connected socket");
        return (-1);
    }

    if (theader == (UNITRANS_HEADER *)0)
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck( lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "_tcp_receive: Failed UNITRANS_HEADER buffer");
        return(-1);
    }

    if (tcp_cycle_receive(sock, (char*)theader, (uint32_t)sizeof(UNITRANS_HEADER), time_out, state) <= 0)
    {
        memset (theader, 0, sizeof (UNITRANS_HEADER));
        /*        if (*state != EWOULDBLOCK)
        {
        if (*state != EINTR)
        RSDURTGUtils_ErrCheck( lfp, ERR_TCP, __LINE__, __FILE__, (int)*state, ERR_CONTINUE, "_tcp_receive: tcp_cycle_receive() failed");
        }    */
        return(-1);
    }

    if((*theader).data_len > 0)
    {
        if((Sbuf = (unsigned char *)RSDURTGUtils_SMalloc((uint32_t)(*theader).data_len)) == (unsigned char *)0)
        {
            memset (theader, 0, sizeof (UNITRANS_HEADER));
            *state = E_MEM;
            //RSDURTGUtils_ErrCheck( lfp, ERR_RMX, __LINE__, __FILE__, (int32_t)E_MEM, ERR_CONTINUE, "_tcp_receive: RSDURTGUtils_SMalloc");
            return(-1);
        }
        else if (tcp_cycle_receive(sock, (char*)Sbuf, (uint32_t)(*theader).data_len, time_out, state) <= 0)
        {
            memset (theader, 0, sizeof (UNITRANS_HEADER));
            /*            if (*state != EWOULDBLOCK)
            {
            if (*state != EINTR)
            RSDURTGUtils_ErrCheck( lfp, ERR_TCP, __LINE__, __FILE__, (int)*state, ERR_CONTINUE, "_tcp_receive: tcp_cycle_receive() failed");
            }    */
            return(-1);
        }
    }
    else 
        Sbuf = (unsigned char *)0;

    *data = Sbuf;

    memset (rcv_addr, 0, sizeof (REM_ADDR));
    (*rcv_addr).addr.addr_ip = (*s_port).service.port_ip.remote_address;
    (*rcv_addr).type = (*s_port).type;
    return(0);
}

int32_t _udp_receive    (COMM_PORT *s_port,
                         REM_ADDR *rcv_addr,
                         UNITRANS_HEADER *theader,
                         unsigned char **data,
                         uint16_t time_out,
                         uint16_t *state,
                         LOG_CONTAINER *lfp)
{
    char        UDPpacket[MAX_UDP_PACKET_LENGTH];
    char       *Sbuf = NULL;
    int32_t     rval = 0;
    size_t      rhost_address_len;
    int         sock = (*s_port).service.port_ip.sock;
    struct sockaddr_in  r_addr;


    *state = E_OK;
    *data = NULL;

    if (theader == (UNITRANS_HEADER *)0)
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck( lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "_udp_receive: Failed UNITRANS_HEADER buffer");
        return(-1);
    }

    rhost_address_len = sizeof (r_addr);
    rval = timed_recvfrom (sock, time_out, UDPpacket, MAX_UDP_PACKET_LENGTH, 0, (struct sockaddr*)&r_addr, &rhost_address_len, state);
    if (rval <= 0)
    {
        memset(theader, 0, sizeof (UNITRANS_HEADER));
        /*        if (*state != EWOULDBLOCK)
        {
        if (*state != EINTR)
        RSDURTGUtils_ErrCheck( lfp, ERR_TCP, __LINE__, __FILE__, (int)*state, ERR_CONTINUE, "_udp_receive: recvfrom() failedn");
        }    */
        return(-1);
    }

    memcpy(theader, UDPpacket, sizeof(UNITRANS_HEADER));
    if ((*theader).data_len > MAX_UDP_PACKET_LENGTH - sizeof(UNITRANS_HEADER))
    {
        memset(theader, 0, sizeof (UNITRANS_HEADER));
        *state = E_MEM;
        //RSDURTGUtils_ErrCheck( lfp, ERR_RMX, __LINE__, __FILE__, (int32_t)E_MEM, ERR_CONTINUE, "_udp_receive: packet size in data_len very long");
        return (-1);
    }
    else
    {
        if ((*theader).data_len == 0)
            Sbuf = (char *)0;
        else
        {
            if((Sbuf = (char *)RSDURTGUtils_SMalloc((uint32_t)(*theader).data_len)) == (char *)0)
            {
                memset(theader, 0, sizeof (UNITRANS_HEADER));
                *state = E_MEM;
                //RSDURTGUtils_ErrCheck( lfp, ERR_RMX, __LINE__, __FILE__, (int32_t)E_MEM, ERR_CONTINUE, "_udp_receive: RSDURTGUtils_SMalloc");
                return(-1);
            }
            memcpy (Sbuf, UDPpacket + sizeof(UNITRANS_HEADER), (size_t)(*theader).data_len);
        }
    }

    *data = (unsigned char*)Sbuf;
    (*rcv_addr).addr.addr_ip.ip_addr = r_addr.sin_addr.s_addr;
    (*rcv_addr).addr.addr_ip.ip_port = ntohs(r_addr.sin_port);
    (*rcv_addr).type = (*s_port).type;
    return(0);
}


int32_t  _udp_send     (COMM_PORT *s_port,
                        REM_ADDR *snd_addr,
                        UNITRANS_HEADER *theader,
                        unsigned char *data,
                        uint16_t time_out,
                        uint16_t *state,
                        LOG_CONTAINER *lfp)
{
    int32_t        rv;
    unsigned char *Sbuf = NULL;
    int32_t        sndsize = 0;
    int            sock = (*s_port).service.port_ip.sock;
    struct sockaddr_in addr;


    *state = E_OK;

    if (theader == (UNITRANS_HEADER *)0)
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck( lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "_udp_send: Failed UNITRANS_HEADER buffer");
        return(-1);
    }

    if (((*theader).data_len > 0) && (data == (unsigned char *)0))
    {
        *state = E_EXIST;
        //RSDURTGUtils_ErrCheck( lfp, ERR_CLIB, __LINE__, __FILE__, (int32_t)E_EXIST, ERR_CONTINUE, "_udp_send: Failed data buffer");
        return(-1);
    }

    if (udp_bcflag(sock, &(*snd_addr).addr.addr_ip, state, lfp) != 0)
        return (-1);

    sndsize = sizeof (UNITRANS_HEADER);
    if ((*theader).data_len > 0)
    {
        if((Sbuf = (unsigned char *)RSDURTGUtils_SMalloc((uint32_t)(*theader).data_len + sndsize)) == (unsigned char *)0)
        {
            *state = E_MEM;
            //RSDURTGUtils_ErrCheck( lfp, ERR_RMX, __LINE__, __FILE__, (int32_t)E_MEM, ERR_CONTINUE, "_udp_send: RSDURTGUtils_SMalloc() failed");
            return (-1);
        }
        memcpy (Sbuf, theader, (size_t)sndsize);
        memcpy (Sbuf + sndsize,data,(size_t)(*theader).data_len);
        sndsize+=(*theader).data_len;
    }
    else Sbuf = (unsigned char *)theader;

    addr.sin_addr.s_addr = (*snd_addr).addr.addr_ip.ip_addr;
    addr.sin_port = htons((*snd_addr).addr.addr_ip.ip_port);
    addr.sin_family = AF_INET;


    /* Для проверки раскомментирован блок выставления параметров broadcast */
    int32_t setBC = 1;
    if (setsockopt(sock, SOL_SOCKET, SO_BROADCAST, (char *)&setBC, (socklen_t)sizeof(setBC)) < 0)
    {
        if (errno == 0)
            *state = E_PARAM;
        else
            *state = errno;
        //RSDURTGUtils_ErrCheck( lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "_udp_send: setsockopt() failed");
        return (-1);
    }

    rv = sendto(sock,(char *)Sbuf,sndsize,0,(struct sockaddr*)&addr,(int32_t)sizeof (struct sockaddr_in));

    if (Sbuf != (unsigned char *)theader)
        RSDURTGUtils_SFree(Sbuf);

    if (rv < 0)
    {
        if (errno == 0)
            *state = E_TIME;
        else
            *state = errno;
        if (*state != EWOULDBLOCK)
        {
            if (*state != EINTR)
                { ; }//RSDURTGUtils_ErrCheck( lfp, ERR_TCP, __LINE__, __FILE__, (int32_t)*state, ERR_CONTINUE, "_udp_send: sendto() failed");
        }
        return(-1);
    }
    return(0);
}

void  _udp_clear_port(COMM_PORT * s_port)
{
    udp_clear_sock((*s_port).service.port_ip.sock);
}




COMM_PORT* RSDURTGUtils_GetCommPort(uint16_t type, uint16_t* p_port_id, uint16_t* p_status, LOG_CONTAINER* lfp)
{
    COMM_PORT* CommPort = (COMM_PORT*)NULL;
    *p_status = E_OK;
    errno = 0;

    if (type == ADV_PROTO_COMMON)
        type = RSDURTGUtils_GetCommonProto();

    if ((CommPort = (COMM_PORT *)RSDURTGUtils_SMalloc(sizeof(COMM_PORT))) == (COMM_PORT *)0)
    {
        *p_status = E_MEM;
        return(CommPort);
    }
    memset (CommPort,0,sizeof (COMM_PORT));
    CommPort->type = type;
    switch (type)
    {
    case ADV_PROTO_NCS:
        if(ncs_get_port (*p_port_id, (COMM_PORT_NCS *)&(CommPort->service.port_ncs),(uint16_t)0,(uint16_t)0,p_status, lfp)<0)
        {
            RSDURTGUtils_SFree(CommPort);
            CommPort = (COMM_PORT*)NULL;
            return(CommPort);
        }
        (*CommPort).port_delete =  &_ncs_delete_port;
        (*CommPort).port_send =    &_ncs_send;
        (*CommPort).port_receive = &_ncs_receive;
        (*CommPort).port_set_opt = &_ncs_set_opt;
        (*CommPort).port_clear   = &_ncs_clear_port;
        break;

    case ADV_PROTO_ETH:
        (*CommPort).service.port_eth.port_id = *p_port_id;
        if(eth_get_port((SERVICE_PORT *)&(*CommPort).service.port_eth, p_status, lfp)<0)
        {
            RSDURTGUtils_SFree(CommPort);
            CommPort = (COMM_PORT*)NULL;
            return(CommPort);
        }
        (*CommPort).port_delete =  &_eth_delete_port;
        (*CommPort).port_send =    &_eth_send;
        (*CommPort).port_receive = &_eth_receive;
        (*CommPort).port_set_opt = &_eth_set_opt;
        (*CommPort).port_clear =   &_eth_clear_port;
        break;

    case ADV_PROTO_TCP:
        (*CommPort).service.port_ip.sock = RSDURTGUtils_bound_stream_socket ((uint16_t *)p_port_id, (uint32_t)WAIT_FOREVER, lfp);
        if ((*CommPort).service.port_ip.sock <= 0)
        {
            *p_status = errno;
            RSDURTGUtils_SFree(CommPort);
            CommPort = (COMM_PORT*)NULL;
            return(CommPort);
        }
        (*CommPort).service.port_ip.port_id = *p_port_id;
        (*CommPort).port_delete =   &_ip_delete_port;
        (*CommPort).port_receive =  &_tcp_receive;
        (*CommPort).port_send =     &_tcp_send;
        break;

    case ADV_PROTO_UDP_CLEAR:
        (*CommPort).service.port_ip.sock = RSDURTGUtils_bound_dgram_socket ((uint16_t *)p_port_id,0,
            (uint32_t)WAIT_FOREVER, lfp);
        if ((*CommPort).service.port_ip.sock <= 0)
        {
            *p_status = errno;
            RSDURTGUtils_SFree(CommPort);
            CommPort = (COMM_PORT*)NULL;
            return(CommPort);
        }
        (*CommPort).service.port_ip.port_id = *p_port_id;
        (*CommPort).port_delete =   &_ip_delete_port;
        (*CommPort).port_receive =  &_udp_receive;
        (*CommPort).port_send =     &_udp_send;
        (*CommPort).port_clear =    &_udp_clear_port;
        break;

    case ADV_PROTO_UDP:

        if(udpex_get_port (*p_port_id, (COMM_PORT_IP *)&(CommPort->service.port_ip), p_status, lfp)<0)
        {
            RSDURTGUtils_SFree(CommPort);
            CommPort = (COMM_PORT*)NULL;
            return(CommPort);
        }
        (*CommPort).port_delete =   &_udpex_delete_port;
        (*CommPort).port_receive =  &_udpex_receive;
        (*CommPort).port_send =     &_udpex_send;
        (*CommPort).port_clear =    &_udpex_clear_port;
        break;


        break;
    default:
        RSDURTGUtils_SFree(CommPort);
        CommPort = (COMM_PORT*)NULL;
        *p_status = E_PARAM;
    }
    return (CommPort);
}

void RSDURTGUtils_DelCommPort(COMM_PORT *&comm_port, LOG_CONTAINER*lfp)
{
    if (comm_port != (COMM_PORT *)0 && (*comm_port).port_delete != NULL)
    {
        (*comm_port).port_delete(comm_port,lfp);
        RSDURTGUtils_SFree(comm_port);
    }
}

int32_t RSDURTGUtils_ReceiveCommPort(COMM_PORT *comm_port,
                                     REM_ADDR *src_addr,
                                     UNITRANS_HEADER *header,
                                     unsigned char **data,
                                     uint16_t time_out,
                                     uint16_t *p_state,
                                     LOG_CONTAINER*lfp)
{
    int32_t res = -1;
    if (comm_port == (COMM_PORT *)0 || src_addr == (REM_ADDR*)0 || (*comm_port).port_receive == NULL)
    {
        *p_state = E_EXIST;
    }
    else
    {
        *p_state = E_OK;
        res = (*comm_port).port_receive(comm_port, src_addr, header, data, time_out, p_state, lfp);
    }
    if (res == 0 && *p_state == E_OK)
        (*src_addr).type = (*comm_port).type;
    return(res);
}

int32_t RSDURTGUtils_SendCommPort(COMM_PORT *comm_port,
                                  REM_ADDR *rcv_addr,
                                  UNITRANS_HEADER *header,
                                  unsigned char *data,
                                  uint16_t time_out,
                                  uint16_t *p_state,
                                  LOG_CONTAINER *lfp)
{
    int32_t res = -1;
    if (comm_port == (COMM_PORT *)0 || rcv_addr == (REM_ADDR*)0 || (*comm_port).port_send == NULL)
        *p_state = E_EXIST;
    else
    {
        *p_state = E_OK;
        res = (*comm_port).port_send(comm_port, rcv_addr, header, data, time_out, p_state, lfp);
    }
    return(res);
}


int32_t RSDURTGUtils_StrToHex(uint8_t * in_buf, uint32_t buf_len, uint8_t * out_buf, uint32_t max_out)
{
    uint32_t i, ii = 0, j = 0, sc = 3;
    char h, l;

    for(i = 0; i < buf_len; i++)
    {
        ii++;
        if(ii%2 == 0) sc=0;
        else sc = 1;
        switch(in_buf[i])
        {
        case '0':
            if(sc == 0) l = 0x0;
            else h = 0x0;
            break;
        case '1':
            if(sc == 0) l = 0x1;
            else h = 0x1;
            break;
        case '2':
            if(sc == 0) l = 0x2;
            else h = 0x2;
            break;
        case '3':
            if(sc == 0) l=0x3;
            else h = 0x3;
            break;
        case '4':
            if(sc == 0) l=0x4;
            else h = 0x4;
            break;
        case '5':
            if(sc == 0) l=0x5;
            else h = 0x5;
            break;
        case '6':
            if(sc == 0) l=0x6;
            else h = 0x6;
            break;
        case '7':
            if(sc == 0) l=0x7;
            else h = 0x7;
            break;
        case '8':
            if(sc == 0) l=0x8;
            else h = 0x8;
            break;
        case '9':
            if(sc == 0) l=0x9;
            else h = 0x9;
            break;
        case 'A':
        case 'a':
            if(sc == 0) l=0xa;
            else h = 0xa;
            break;
        case 'B':
        case 'b':
            if(sc == 0) l=0xb;
            else h = 0xb;
            break;
        case 'C':
        case 'c':
            if(sc == 0) l=0xc;
            else h = 0xc;
            break;
        case 'D':
        case 'd':
            if(sc == 0) l=0xd;
            else h = 0xd;
            break;
        case 'E':
        case 'e':
            if(sc == 0) l=0xe;
            else h = 0xe;
            break;
        case 'F':
        case 'f':
            if(sc == 0) l=0xf;
            else h = 0xf;
            break;
        default:
            sc = 3;
            ii--;
            break;
        }
        if(sc > 0) continue;
        out_buf[j] = (l & 0x0F) | ((h << 4) & 0xF0);
        j++;
        if (j >= max_out) return(j - 1);
    }
    return(j - 1);
}


int32_t RSDURTGUtils_AddressSet(REM_ADDR * address, uint16_t srvc_port, uint16_t *hostid, char* hostaddr, uint16_t id_proto)
{
    struct hostent *host;

    if (address == (REM_ADDR *)0)
        return(-1);
    if (id_proto == ADV_PROTO_COMMON)
        (*address).type = RSDURTGUtils_GetCommonProto();
    else
        (*address).type = id_proto;

    switch ((*address).type)
    {
    case ADV_PROTO_ETH:
        if (hostaddr == (char*)0)
            return(-1);
        RSDURTGUtils_StrToHex((uint8_t*)hostaddr,(uint32_t) 16,(uint8_t*) (*address).addr.addr_eth.address, (uint32_t)ETHERNET_ADDR_SIZE);
        (*address).addr.addr_eth.portid = srvc_port;
        (*address).addr.addr_eth.addrlength = ETHERNET_ADDR_SIZE;
        break;

    case ADV_PROTO_NCS:
        if (hostid == (uint16_t*)0)
            return(-1);
        (*address).addr.addr_ncs.r_sock = (uint32_t)(((srvc_port << 16) & 0xFFFF0000) | (*hostid & 0x0000FFFF));
        break;

    case ADV_PROTO_UDP:
    case ADV_PROTO_UDP_CLEAR:
    case ADV_PROTO_TCP:

        (*address).addr.addr_ip.ip_addr = inet_addr((char *)hostaddr);
        if ((*address).addr.addr_ip.ip_addr == INADDR_NONE)
        {
            if ((host = gethostbyname((char *)hostaddr)) != NULL)
                memcpy (&(*address).addr.addr_ip.ip_addr, host->h_addr, sizeof (uint32_t));
            else
                return(-1);
        }
        (*address).addr.addr_ip.ip_port = srvc_port;
        break;

    default:
        return(-1);
    }

    return(0);
}



#define COMM_PORT_TIMEOUT  5 /* Тайм-аут для отправки/получения данных по универсальному транспорту, в секундах */

int test_send()
{
    LOG_HEADER Logger;
    COMM_PORT *cp = NULL;
    uint16_t port_id = 0;
    uint16_t status = DCP_OK;
    uint16_t hostid = 0;
    UNITRANS_HEADER msgheader;
    REM_ADDR raddr;
    DCP_REG rparam[10];
    DCP_ASTRUCT *rbuf = NULL;


    RSDURTGUtils_AddressSet(&raddr, (uint16_t)2132, &hostid, (char*)"10.5.165.20", ADV_PROTO_UDP);
    port_id = raddr.addr.addr_ip.ip_port;
    cp = RSDURTGUtils_GetCommPort(ADV_PROTO_UDP, &port_id, &status, &Logger);

    if( cp == NULL || status != E_OK )
    {
        printf("RSDURTGUtils_GetCommPort() failed, err %04hx\n", status);
        
    }
    else
    {
        msgheader.command = CMD_DCP_GET;
        msgheader.status = DCP_OK;
        msgheader.time1970 = (uint32_t)RSDURTGUtils_Time70();
        msgheader.src_uid = 0xFFFF;
        msgheader.dst_uid = 0xFFFF;
        msgheader.src_laws.laws = 0xFFFF;
        msgheader.param1 = 1;
        msgheader.param2 = GLOBAL_TYPE_ANALOG;
        msgheader.data_len = sizeof(DCP_REG);

        rparam[0].id = 2024446;
        rparam[0].tbl_id = 0;
        rparam[0].status = DCP_OK;

        rbuf =(DCP_ASTRUCT *) RSDURTGUtils_SMalloc(10*sizeof(DCP_ASTRUCT));

        RSDURTGUtils_SendCommPort(cp, &raddr, &msgheader, (unsigned char *)&rparam, COMM_PORT_TIMEOUT*100, &status, &Logger);

        RSDURTGUtils_ReceiveCommPort(cp, &raddr, &msgheader, (unsigned char **)&rbuf, COMM_PORT_TIMEOUT*100, &status, &Logger);

//        if( blDebugMode )
//        {
            printf("Status : %d\n", msgheader.status);
            printf("Data length : %d\n", msgheader.data_len);
            printf("Status buf: %d\n", rbuf[0].status);
            printf("Value  : %f\n", rbuf->value);
//        }

        RSDURTGUtils_SFree(rbuf);
    }

    RSDURTGUtils_DelCommPort(cp, &Logger);

    return 0;
}


int getV(char *ip, int16_t portid, uint32_t gtype, uint32_t id, uint32_t tbl_id, int blDebugMode)
{
    int32_t err = 0 ;
	LOG_HEADER Logger;
    COMM_PORT *cp = NULL;
    uint16_t port_id = 0;
    uint16_t status = DCP_OK;
    uint16_t hostid = 0;
    UNITRANS_HEADER msgheader;
    REM_ADDR raddr;
    DCP_REG rparam[10];
    DCP_ASTRUCT *rbufA = NULL;
	DCP_BSTRUCT *rbufB = NULL;

    err=RSDURTGUtils_AddressSet(&raddr, portid, &hostid, ip, ADV_PROTO_UDP);
	if (err<0) {
		printf("ERRADDR");
		return(0);
	}
    port_id = raddr.addr.addr_ip.ip_port;
    cp = RSDURTGUtils_GetCommPort(ADV_PROTO_UDP, &port_id, &status, &Logger);

    if( cp == NULL || status != E_OK )
    {
       if (blDebugMode==1) 
		   printf("RSDURTGUtils_GetCommPort() failed, err %04hx\n", status);
	   printf("NONE");
    }
    else
    {
        msgheader.command = CMD_DCP_GET;
        msgheader.status = DCP_OK;
        msgheader.time1970 = (uint32_t)RSDURTGUtils_Time70();
        msgheader.src_uid = 0xFFFF;
        msgheader.dst_uid = 0xFFFF;
        msgheader.src_laws.laws = 0xFFFF;
        msgheader.param1 = 1;
        msgheader.param2 = gtype;
        msgheader.data_len = sizeof(DCP_REG);

        rparam[0].id = id;
        rparam[0].tbl_id = 0; // tbl_id
        rparam[0].status = DCP_OK;
		
		rbufA =(DCP_ASTRUCT *) RSDURTGUtils_SMalloc(10*sizeof(DCP_ASTRUCT));
		rbufB =(DCP_BSTRUCT *) RSDURTGUtils_SMalloc(10*sizeof(DCP_BSTRUCT));

        err = 0 ;
		if( RSDURTGUtils_SendCommPort(cp, &raddr, &msgheader, (unsigned char *)&rparam, COMM_PORT_TIMEOUT*100, &status, &Logger)!= E_OK ) {
			err++;
			printf("ERRSEND");
		} else {
			if (gtype==GLOBAL_TYPE_ANALOG)
             if(RSDURTGUtils_ReceiveCommPort(cp, &raddr, &msgheader, (unsigned char **)&rbufA, COMM_PORT_TIMEOUT*100, &status, &Logger)!= E_OK ) {
               err++;
			 }
		    if (gtype==GLOBAL_TYPE_BOOL)
		     if(RSDURTGUtils_ReceiveCommPort(cp, &raddr, &msgheader, (unsigned char **)&rbufB, COMM_PORT_TIMEOUT*100, &status, &Logger)!= E_OK ) {
               err++;
			 }

			// If no errors then show data from received buffer.
			if (err==0) {
			  for(uint32_t i=0; i<msgheader.param1; ++i)
              {
			    if (blDebugMode==1) printf("Status : %d\n", msgheader.status);
                if (blDebugMode==1) printf("Data length : %d\n", msgheader.data_len);
                switch( msgheader.param2 )
                {
                   case GLOBAL_TYPE_ANALOG:
					   //printf("Status: %d\n", rbufA[0].status);
                       //printf("Value : %f\n", rbufA->value);
					   if (blDebugMode==0) printf("%f", rbufA->value);
					   if (blDebugMode>0) printf("%f:%d", rbufA->value,rbufA[0].status);
                       break;
			       case GLOBAL_TYPE_BOOL:
                       //printf("Status: %d\n", rbufB[0].status);
                       //printf("Value : %d\n", rbufB->value);
					   if (blDebugMode==0) printf("%d", rbufB->value);
					   if (blDebugMode>0) printf("%d:%d", rbufB->value,rbufB[0].status);
                       break;
		         default:
                       break;
                 }
              } // for
			} // if err
			else {
				printf("ERRRES");
			}

		} 

        RSDURTGUtils_SFree(rbufA);
		RSDURTGUtils_SFree(rbufB);
    }

    RSDURTGUtils_DelCommPort(cp, &Logger);

    return 0;
}


// argv[1]=ip argv[2]=port argv[3]=id argv[4]=tbl_id argv[5]=d|s
static void show_usage(char* name)
{
    printf( "Usage: %s <option(s)>\n", name);
    printf( "Options:\n" );
    printf( "\t:ip port id ?tbl_id(=29)? ?-s?\n" );
	printf( "Output: value or NONE or ERR\n " );
}


int main(int argc, char* argv[])
{
  char buff[1014];
  // Шаг 1 - иницилизация библиотеки Winsocks
  if (WSAStartup(0x202,(WSADATA *)&buff[0]))
  {
    //printf("WSAStartup error: %d\n",  WSAGetLastError());
	printf("ERR");
    return 0;
  }	

  int tmp_val = 0;
  int err ;
  char ip[16]; //xxx.xxx.xxx.xxx
  int id = 0;
  int tbl_id = 29; // def
  int param2 = GLOBAL_TYPE_ANALOG ; // def
  int blDebugMode = 0 ; // debug mode
  int data_len = 0 ;
  uint16_t port_id = 0;

  if(argc<4) {
    show_usage(argv[0]);
    return(0);
  }

  //for (int i = 0; i < argc; i++){
  //  printf("%d %s \n", i,argv[i]);
  //}


  if (strlen(argv[1])>=16) {
	  printf("ERRIP");
      return 0;
  }
  sprintf(ip,"%s\0",argv[1]);

  port_id = atoi(argv[2]);
  if (port_id<=1024 || port_id>65535) {
	  printf("ERRPORT");
      return 0;
  }

  id = atoi(argv[3]);
  if (id<=0) {
	  printf("ERRID");
      return 0;
  }


  if (argc==5) {

    if(strcmp(argv[4], "-d")==0)
    {
      blDebugMode = 1;
    } 
	if(strcmp(argv[4], "-s")==0)
    {
      blDebugMode = 2;
    }
	if(blDebugMode==0) {
	  tmp_val = atoi(argv[4]);
	  switch (tmp_val) {
		case 116 : tbl_id = 116 ; break ;
		case 33  : tbl_id = 33; param2 = GLOBAL_TYPE_BOOL ; break ;
		case 35  : tbl_id = 35; param2 = GLOBAL_TYPE_BOOL ; break ;
		default  : tbl_id = 29;
	  }
	}
  }

  if (argc==6) {
	  tmp_val = atoi(argv[4]);
	  switch (tmp_val) {
		case 116 : tbl_id = 116 ; break ;
		case 33  : tbl_id = 33; param2 = GLOBAL_TYPE_BOOL ; break ;
		case 35  : tbl_id = 35; param2 = GLOBAL_TYPE_BOOL ; break ;
		default  : tbl_id = 29;
	  }
	if(strcmp(argv[5], "-d")==0)
    {
        blDebugMode = 1;
    }
    if(strcmp(argv[5], "-s")==0)
    {
        blDebugMode = 2;
    }
  }

  if (blDebugMode==1) {
    printf("(%d)%s:%d gtype=%d id=%d tbl=%d mode=%d\n",strlen(ip),ip,port_id,param2,id,tbl_id, blDebugMode);
  }


  getV( ip , port_id , param2 , id , tbl_id , blDebugMode );


	WSACleanup();
	return 0;
}


