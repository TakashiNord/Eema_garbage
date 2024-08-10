// emaqvit.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <winsock2.h>
#include <windows.h>
#include <time.h>

#include <stdlib.h>

#include <iostream>
using namespace std;

#pragma comment(lib,"ws2_32.lib")

#define WIN32 1

#ifdef WIN32
typedef signed char   int8_t;
typedef short int   int16_t;
typedef int       int32_t;
typedef long int  int64_t;

typedef unsigned char     uint8_t;
typedef unsigned short int    uint16_t;
typedef unsigned int      uint32_t;
/* typedef unsigned short   uint16_t; */
typedef unsigned long int uint64_t;
#endif // WIN32

/*������� �������� ������*/
#define GCMD_GLOBAL_STATE_STOPPED       (uint16_t)0xf00f      /* ������� ��������� - ���������� */
#define GCMD_GET_GLOBAL_STATE           (uint16_t)0xf008      /* �������� ������ ������� (SYSTEMMASTER ��� SYSTEMSLAVE) */
#define GCMD_GLOBAL_STATE_MASTER        (uint16_t)0xf009      /* ����� ������� SYSTEMMASTER */
#define GCMD_GLOBAL_STATE_SLAVE         (uint16_t)0xf00a      /* ����� ������� SYSTEMSLAVE */
#define GCMD_SET_GLOBAL_STATE_MASTER    (uint16_t)0xf00b      /* Set SYSTEMMASTER */
#define GCMD_SET_GLOBAL_STATE_SLAVE     (uint16_t)0xf00c      /* Set SYSTEMSLAVE */
#define GCMD_SEND_TO_SLAVE              (uint16_t)0xf00d      /* ��������� ������ ���������� ��������� */
#define GCMD_ECHO                       (uint16_t)0xfffe      /* ��� - ������*/
#define E_DELETE_YOURSELF               (uint16_t)0xFFED      /* Unload JOB */
#define SYS_DELETE_YOURSELF             (uint16_t)0xFFDD      /* Unload JOB */


/* ----------------------------------- Start for SH Interface --------------------------------------------*/

#define CMD_ADCP_SH_KVIT             (uint16_t)74   /* ������������ ���� */
#define CMD_ADCP_SH_LOGGROUPON       (uint16_t)75   /* �������� ���������� ������ �� */
#define CMD_ADCP_SH_LOGGROUPOFF      (uint16_t)76   /* ��������� ���������� ������ �� */
#define CMD_ADCP_SH_ALLTSON          (uint16_t)77   /* �������� ��� �� �� ���� */
#define CMD_ADCP_SH_ALLTSOFF         (uint16_t)78   /* ��������� ��� �� �� ���� */

#define CMD_ADCP_SH_SETCHANINFO      (uint16_t)101  /* ���������� ���������� � ������ ������ */





#ifdef WIN32
#define E_TIME       WSAETIMEDOUT
#define E_PARAM      WSAEINVAL
#else    /* Linux */
#define E_TIME       EWOULDBLOCK
#define E_PARAM      EINVAL
#endif
#define E_MEM        ENOMEM
#define E_EXIST      EIO
#define E_OK         0
#define E_SQLEXCEPT  200
#define E_ALERTRCV   201

#define WAIT_FOREVER   0xFFFF
#define NO_WAIT        0

#pragma pack(push,1)
/* ����� ������������ */
typedef struct KEY_LAWS {
    uint32_t  laws;
    char      key[16];
} KEY_LAWS;
#pragma pack(pop)

#pragma pack(push,1)
/* ������������� ��������� ������� ��� �������� ������ � ��������� ���� */
typedef struct UNITRANS_HEADER {
    uint32_t  command;
    uint32_t  src_uid;
    uint32_t  dst_uid;
    uint32_t  param1;    /*  ��� �������, ���������� ������������ ���������� */
    uint32_t  param2;    /*  ��� �������, ���������� ��� ������ */
    uint32_t  param3;    /*  ������, ��� ����� ���������� �����-���� �����(�������� ���������� �������, ������������� ��������� � �.�. */
    KEY_LAWS  src_laws;
    uint32_t  data_len;
    int16_t   status;
    uint32_t  time1970;
} UNITRANS_HEADER;/* ������������� ��������� ������� ��� �������� ������ � ��������� ���� */
#pragma pack(pop)


// from rsdutime.c
time_t RSDURTGUtils_Time70(void)
{
    uint32_t t;
    t = time(0);
    return(t);
}


// sysmon 2003
//#define PORT 2003

// acsrv 2005
//#define PORT 2005

// 2006 shit40
#define PORT 2006

char *SERVERADDR[]={"10.168.100.214","10.168.100.215"};



int main(int argc, char* argv[])
{
 
  if(argc<2) {
    printf("String runnig: exe ip\n");
	printf("Output: -------------\n");
    return(0);
  }	


  char buff[1014];
  #pragma pack(push,1)
  UNITRANS_HEADER hd,hd1;
  #pragma pack(pop)
  INT err;

  memset(&hd1, 0, sizeof(UNITRANS_HEADER)); // ��������� ��� ��������

  memset(&hd, 0, sizeof(UNITRANS_HEADER));
  hd.src_uid = 0xffff ; // 0x0 0xffff  env->UserID
  hd.param1  = 0xffff ; // 0xffff
  hd.param2  = 0xffff ;// 0xffff  0 ADM_GROUPID;
  hd.src_laws.laws = 0xffffffff;
  hd.dst_uid = 0xffff ;// 0xffff 0  ADM_SYSMONUSERID;
  hd.command = CMD_ADCP_SH_KVIT ; // GCMD_GET_GLOBAL_STATE  CMD_ADCP_SH_KVIT
  hd.data_len = 0 ;
  hd.time1970 = RSDURTGUtils_Time70();
  hd.status = E_OK ;  // E_OK = 0


  // ��� 1 - ������������ ���������� Winsocks
  if (WSAStartup(0x202,(WSADATA *)&buff[0]))
  {
    printf("WSAStartup error: %d\n",  WSAGetLastError());
    return 0;
  }

  // ��� 2 - �������� ������
  SOCKET my_sock=socket(AF_INET, SOCK_DGRAM, 0);
  if (my_sock==INVALID_SOCKET)
  {
    printf("socket() error: %d\n",WSAGetLastError());
    WSACleanup();
    return 0;
  }

  // ��� 2.1 - ��������� ������� ������
  int snd_timeout = 1000;
  int rcv_timeout = 1000;
  BOOL  fBroadcast = TRUE;

  err = setsockopt ( my_sock, SOL_SOCKET, SO_BROADCAST, (CHAR *) &fBroadcast, sizeof ( BOOL ));
  if (err == SOCKET_ERROR) { closesocket ( my_sock ); return err; }
  err = setsockopt ( my_sock, SOL_SOCKET , SO_RCVTIMEO, (const char *)&rcv_timeout, sizeof(int));
  if (err == SOCKET_ERROR) { closesocket ( my_sock ); return err; }
  err = setsockopt ( my_sock, SOL_SOCKET , SO_SNDTIMEO, (const char *)&snd_timeout, sizeof(int));
  if (err == SOCKET_ERROR) { closesocket ( my_sock ); return err; }

  // ��� 3 - ����� ��������� � ��������
  HOSTENT *hst;
  sockaddr_in dest_addr;

  dest_addr.sin_family=AF_INET;
  dest_addr.sin_port=htons(PORT);

  // ����������� IP-������ ����
  if (inet_addr(argv[1]))
    dest_addr.sin_addr.s_addr=inet_addr(argv[1]);
  else
    if (hst=gethostbyname(argv[1]))
      dest_addr.sin_addr.s_addr=((unsigned long **)hst->h_addr_list)[0][0];
    else
    {
      printf("Unknown host: %d\n",WSAGetLastError());
      closesocket(my_sock);
      WSACleanup();
      return 0;
    }


  sockaddr_in server_addr;
  /*
  * We will bind this socket to a wildcard name
  */

  int length = 0;

  server_addr.sin_family = AF_INET;
  server_addr.sin_addr.s_addr = INADDR_ANY;
  server_addr.sin_port = 0;

  length = sizeof(server_addr);
  err = bind (my_sock, (struct sockaddr *) &server_addr, length);
  if ( err == SOCKET_ERROR )
  {
    closesocket ( my_sock );
    return err;
  }


  // �������� ��������� �� ������
  err = sendto(my_sock,(char *)&hd, sizeof(hd),0,
      (sockaddr *) &dest_addr,sizeof(dest_addr));
  if ( err == SOCKET_ERROR )
  {
    printf("sendto: err=%d GetLastError() = %d\n",err,GetLastError());
	//printf("ERR");
    closesocket ( my_sock );
    return err;
  }

  // ����� ��������� � �������
  int server_addr_size=sizeof(server_addr);

  int n=recvfrom(my_sock,(char *)&hd1, sizeof(hd1),0,
      (sockaddr *) &server_addr, &server_addr_size);

  if (n==SOCKET_ERROR)
  {
      printf("recvfrom() error: %d\n",WSAGetLastError());
	  //printf("ERR");
      closesocket(my_sock);
      WSACleanup();
      return 0;
  }


  printf("\n" );
  printf("\n %s : %d ", argv[1] , PORT);
  printf("\nhd.command = %x", hd1.command );
  printf("\nhd.src_uid = %x", hd1.src_uid );
  printf("\nhd.param1 = %x", hd1.param1 );
  printf("\nhd.status = %x", hd1.status );
  printf("\n" );

  if (hd1.command==GCMD_GLOBAL_STATE_MASTER)
	  printf("MASTER" );
  else if (hd1.command==GCMD_GLOBAL_STATE_SLAVE)
	  printf("SLAVE" );
  else printf("NONE" ); 


  // ��� ��������� - �����
  closesocket(my_sock);
  WSACleanup();

  return 0;
}



