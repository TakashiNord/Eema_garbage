// edget.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <winsock.h>
#include <windows.h>
#include <time.h>

#include <string.h>
#include <stdlib.h>

#pragma comment(lib,"ws2_32.lib")

/*  WARNING! ALL INCLUDES MUST BE PLACED BEFORE THIS INCLUDE!!! */
#if defined(__IC286__)
#pragma noalign
#elif defined(__IC386__)
#pragma noalign
#elif defined(__BORLANDC__)
#pragma option -a-
#elif defined(__WATCOMC__)
#pragma pack(1)
#elif defined(_MSC_VER)
#pragma pack(1)
#else
#pragma pack(1)
#endif

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


/* ************************************************* */
/*      Constants for data difinition from sys_gtyp  */
/* ************************************************* */
#define GLOBAL_TYPE_NOTDEFINED  (uint16_t)0 // not defined type
#define GLOBAL_TYPE_ANALOG      (uint16_t)1 /*Непрерывные (аналоговые) данные*/
#define GLOBAL_TYPE_DIGIT       (uint16_t)2 /*Цифровые данные*/
#define GLOBAL_TYPE_BOOL        (uint16_t)3 /*Данные состояния (булевые)*/
#define GLOBAL_TYPE_DANALOG     (uint16_t)4 /*Двойные аналоговые*/
#define GLOBAL_TYPE_DDIGIT      (uint16_t)5 /*Двойные цифровые*/
#define GLOBAL_TYPE_DBOOL       (uint16_t)6 /*Двойные булевые*/
#define GLOBAL_TYPE_BLOB        (uint16_t)7 /*Большие двоичные*/

/* Команды серверам данных */
#define CMD_DCP_GET             (uint16_t)8               /* Получить значение параметра */
#define CMD_DCP_SETNEWVAL       (uint16_t)9               /* Установить новое значение параметра */
#define CMD_DCP_GETSRC          (uint16_t)10              /* Получить текущий источник */
#define CMD_DCP_SETSRC          (uint16_t)11              /* Установить текущий источник */

#define CMD_DCP_SETVAL          (uint16_t)15              /* Установить значение параметра ручного воода*/
#define CMD_DCP_SETLEVEL        (uint16_t)16              /* Set new value (global digit)*/
#define CMD_DCP_GETVAL          (uint16_t)17              /* Получить значение параметра ручного воода */

/* Уведомления и ошибки */
#define DCP_OK                  (int16_t)0
#define DCP_FAILED              (int16_t)-1
#define DCP_REGFAILED           (int16_t)-2                    /* Ошибка регистрации параметров */
#define DCP_TYPENOPRESENT       (int16_t)-3
#define DCP_OUTOFMEMORY         (int16_t)-4
#define DCP_PROTONOCORRECT      (int16_t)-5
#define DCP_TIMEOUT             (int16_t)-6
#define DCP_SRCNOTPRESENT       (int16_t)-7
#define DCP_COMMNOTSUPPORT      (int16_t)-8                   /* Команда не поддерживатеся */
#define DCP_NULLDATANUMBER      (int16_t)-9
#define DCP_DATAERR             (int16_t)-10                  /* Ошибка при операциях с параметрами */
#define DCP_NOLAWS              (int16_t)-11                  /* Отсутствуют права на операцию */
#define DCP_NOTAVAILABLE        (int16_t)-12                  // Not available in the current state

/* Структуры DCP - протокола */
/* Основная структура протокола - Регистрация параметров и пр. */

typedef struct _DCP_REG {
    uint32_t tbl_id;    /* Идентификатор таблицы списка */
    uint32_t id;       /* Идентификатор параметра в таблице списке */
    uint32_t status;   /* статус (возвращает сервер)*/
} DCP_REG;


/* Аналоговые значения */
typedef struct DCP_ASTRUCT
{
    uint32_t tbl_id;
    uint32_t id;
    double   value;
    uint32_t status;
} DCP_ASTRUCT;

/* Булевые значения */
typedef struct DCP_BSTRUCT
{
    uint32_t tbl_id;
    uint32_t id;
    uint32_t value;
    uint32_t status;
} DCP_BSTRUCT;

/* Права пользователя */
typedef struct KEY_LAWS {
    uint32_t  laws;
    char      key[16];
} KEY_LAWS;

/* Универсальный заголовок пакетов для передачи данных в комплексе РСДУ */
typedef struct UNITRANS_HEADER {
    uint32_t  command;
    uint32_t  src_uid;
    uint32_t  dst_uid;
    uint32_t  param1;    /*  Как правило, количество передаваемых параметров */
    uint32_t  param2;    /*  Как правило, глобальный тип данных */
    uint32_t  param3;    /*  Бывает, что здесь передается какое-либо время(например последнего запроса, диспетчерской ведомости и т.д. */
    KEY_LAWS  src_laws;
    uint32_t  data_len;
    int16_t   status;
    uint32_t  time1970;
} UNITRANS_HEADER;/* Универсальный заголовок пакетов для передачи данных в комплексе РСДУ */

#if defined(__IC286__)
#if defined(__NOALIGN__)
#pragma noalign
#else
#pragma align
#endif /* __NOALIGN__ */

#elif defined(__IC386__)
#if defined(__NOALIGN__)
#pragma noalign
#else
#pragma align
#endif /* __NOALIGN__ */

#elif defined(__BORLANDC__)
#pragma option -a.

#elif defined(__WATCOMC__)
#pragma pack()

#elif defined(_MSC_VER)
#pragma pack()

#else
#pragma pack()

#endif    /* __IC286__, __IC386__, __BORLANDC__, __WATCOMC__, _MSC_VER */


// from rsdutime.c
time_t RSDURTGUtils_Time70(void)
{
    time_t t;
    t = time(0);
    return(t);
}

#define MAX_UDP_PACKET_LENGTH   1024  /* Максимальная длина пакета UDP */
#define MAX_HOST_NAME_LEN       64    /* Максимальная длина имени хоста */



#define PORT 2012 // 2132 2012 ADV_PROTO_UDP

// argv[1]=ip argv[2]=port argv[3]=GET argv[4]=id argv[5]=tbl_id
static void show_usage(char* name)
{
    printf( "Usage: %s <option(s)>\n", name);
    printf( "Options:\n" );
    printf( "\t:ip port GET id ?tbl_id(=29)?\n" );
	printf( "Output: value or NONE or ERR\n " );
}

int main(int argc, char* argv[])
{
  int tmp_val = 0;
  char buff[1014];
  int err ;
  char   UDPpacket[MAX_UDP_PACKET_LENGTH];

  UNITRANS_HEADER hd;
  DCP_REG rparam[10] ; // [1];
  
  DCP_ASTRUCT as ;
  DCP_BSTRUCT bs ;  

  char ip[80];
  int id = 0;
  int tbl_id = 29; // def
  int param2 = GLOBAL_TYPE_ANALOG ; // def
  int data_len = 0 ;
  unsigned char  *Sbuf = NULL;
  int32_t         sndsize = 0;
  uint16_t port_id = 0;

  if(argc<5) {
    show_usage(argv[0]);
    return(0);
  }

  id = atoi(argv[4]);
  if (id<=0) {
	  printf("ERRID");
      return 0;
  }

  if (argc>5) {
	  tmp_val = atoi(argv[5]);
	  switch (tmp_val) {
		case 116 : tbl_id = 116 ; break ;
		case 33  : tbl_id = 33; param2 = GLOBAL_TYPE_BOOL ; break ;
		case 35  : tbl_id = 35; param2 = GLOBAL_TYPE_BOOL ; break ;
		default  : tbl_id = 29;
	  }
  }
  
  port_id = atoi(argv[2]);
  if (port_id<=1024 || port_id>65535) {
	  printf("ERRPORT");
      return 0;
  }

  sprintf(ip,"%s\0",argv[1]);
  printf("(%d) %s:%d %d %d \n",strlen(ip),ip,port_id,id,tbl_id );

  memset(&as, 0, sizeof(DCP_ASTRUCT));
  memset(&bs, 0, sizeof(DCP_BSTRUCT));

  memset(&rparam, 0, sizeof(DCP_REG));
  rparam[0].tbl_id = tbl_id ; // tbl_id 0
  rparam[0].id     = id;
  rparam[0].status = DCP_OK;
  
  memset(&hd, 0, sizeof(UNITRANS_HEADER));
  hd.command = CMD_DCP_GET;
  hd.src_uid = 0xFFFF ; // 0x0 0xffff  env->UserID
  hd.dst_uid = 0xFFFF ; // 0xffff 0  ADM_SYSMONUSERID;
  hd.param1  = 1 ; // 0xffff
  hd.param2  = param2 ;
//  hd.param3  = 0 ;
  hd.src_laws.laws = 0xFFFF; // 0xffffffff
  hd.data_len = sizeof(DCP_REG) ;
  hd.status = DCP_OK ;
  hd.time1970 = (uint32_t)RSDURTGUtils_Time70() ;

  sndsize = sizeof (UNITRANS_HEADER);
  //printf("hd.data_len=%d sndsize=%d\n",hd.data_len,sndsize);

  if (hd.data_len > 0)
  {
    if((Sbuf = (unsigned char *)malloc((uint32_t)(hd.data_len + sndsize) )) == (unsigned char *)0)
	//if((Sbuf = new char[sizeof(hd.data_len + sndsize)]) == (char *)0)
    {
       printf("E_MEM");
       return 0;
    }
	ZeroMemory(Sbuf, sizeof(hd.data_len + sndsize));
    memcpy (Sbuf, (unsigned char *) &hd, (size_t)sndsize);
    memcpy (Sbuf + sndsize, (unsigned char *) &rparam ,(size_t)hd.data_len);
    sndsize+=hd.data_len;
  }
  else Sbuf = (unsigned char *) &hd; // Sbuf = reinterpret_cast<char*>(&hd);
  
 // for(int i = 0 ; i<sndsize; i++) // sizeof sndsize
 //	  printf("%02x ", Sbuf[i]);
 // printf("\n");

// ---------------------------------------------------------------

  // Шаг 1 - иницилизация библиотеки Winsocks
  if (WSAStartup(0x202,(WSADATA *)&buff[0]))
  {
    //printf("WSAStartup error: %d\n",  WSAGetLastError());
	printf("ERR");
    return 0;
  }

  // Шаг 2 - открытие сокета
  SOCKET sockfd=socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP); // IPPROTO_UDP
  if (sockfd==INVALID_SOCKET)
  {
    //printf("socket() error: %d\n",WSAGetLastError());
	printf("ERR");
    WSACleanup();
    return 0;
  }

  // Шаг 2.1 - установка свойств сокета
  int snd_timeout = 1000;
  int rcv_timeout = 1000;
  BOOL  fBroadcast = TRUE;

  err = setsockopt ( sockfd, SOL_SOCKET, SO_BROADCAST, (CHAR *) &fBroadcast, sizeof ( BOOL ));
  if (err == SOCKET_ERROR) { closesocket ( sockfd ); return err; }
  err = setsockopt ( sockfd, SOL_SOCKET , SO_RCVTIMEO, (const char *)&rcv_timeout, sizeof(int));
  if (err == SOCKET_ERROR) { closesocket ( sockfd ); return err; }
  err = setsockopt ( sockfd, SOL_SOCKET , SO_SNDTIMEO, (const char *)&snd_timeout, sizeof(int));
  if (err == SOCKET_ERROR) { closesocket ( sockfd ); return err; }

  // Шаг 3 - обмен сообщений с сервером
  HOSTENT *hst;
  sockaddr_in dest_addr;

  dest_addr.sin_family=AF_INET;
  dest_addr.sin_port=htons(port_id);

  // определение IP-адреса узла
  if (inet_addr(ip))
    dest_addr.sin_addr.s_addr=inet_addr(ip);
  else
    if (hst=gethostbyname(ip))
      dest_addr.sin_addr.s_addr=((unsigned long **)hst->h_addr_list)[0][0];
    else
    {
      //printf("Unknown host: %d\n",WSAGetLastError());
	  printf("ERRIP");
      closesocket(sockfd);
      WSACleanup();
      return 0;
    }


  sockaddr_in server_addr;
  /*
  * We will bind this socket to a wildcard name
  */

  int length = 0;

  server_addr.sin_family = AF_INET;
  server_addr.sin_addr.s_addr = INADDR_ANY ; // htonl(INADDR_ANY);
  server_addr.sin_port = 0; //

  length = sizeof(server_addr);
  err = bind (sockfd, (struct sockaddr *) &server_addr, length);
  if ( err == SOCKET_ERROR )
  {
    closesocket ( sockfd );
    return err;
  }
  
//unsigned long iMode = 1;
//if (NO_ERROR!=ioctlsocket(sockfd, FIONBIO, &iMode))
//  printf("ioctlsocket failed with error: \n");  

  // Передача сообщений на сервер
  int sent_bytes  = sendto(sockfd,  (char *)Sbuf, sndsize ,0,(sockaddr *) &dest_addr,sizeof(dest_addr));
  if ( sent_bytes  == SOCKET_ERROR ) // if ( sent_bytes  != sndsize )
  {
    printf("sendto: err=%d GetLastError() = %d\n",sent_bytes ,GetLastError());
	printf("ERRSEND");
    closesocket ( sockfd );
    return 0;
  }

  // Прием сообщения с сервера
  int server_addr_size=sizeof(server_addr);

  int n=recvfrom(sockfd, UDPpacket, MAX_UDP_PACKET_LENGTH ,0,(sockaddr *) &server_addr, &server_addr_size);

  if (n==SOCKET_ERROR)
  {
      printf("recvfrom() error: %d\n",WSAGetLastError());
	  printf("ERRRECV");
  }

  // шаг последний - выход
  closesocket(sockfd);
  WSACleanup();

  // Some info on the sender side

   getpeername(sockfd, (SOCKADDR *)&server_addr, &server_addr_size);
   printf("Server: Sending IP used: %s\n", inet_ntoa(server_addr.sin_addr));
   printf("Server: Sending port used: %d\n", htons(server_addr.sin_port));

// ---------------------------------------------------------------
  free (Sbuf);

  if (n<=0) return(0);

  memset(&hd, 0, sizeof (UNITRANS_HEADER));
  memcpy(&hd, UDPpacket, sizeof(UNITRANS_HEADER));
  if (hd.data_len > MAX_UDP_PACKET_LENGTH - sizeof(UNITRANS_HEADER))
  {
    memset(&hd, 0, sizeof (UNITRANS_HEADER));
    printf("ERRRECVMEM1");
    return 0;
  } else {
    if (hd.data_len == 0) {
      memset(&as, 0, sizeof(DCP_ASTRUCT));
      memset(&bs, 0, sizeof(DCP_BSTRUCT));		
	}
    else {
	  switch (tbl_id) {
		case 33 : 
		case 35 : 
		  memcpy (&bs, UDPpacket + sizeof(UNITRANS_HEADER), (size_t)hd.data_len);
		break ;
		default : 
		  memcpy (&as, UDPpacket + sizeof(UNITRANS_HEADER), (size_t)hd.data_len);
	  }	
     }
  }

  printf("Status : %d\n", hd.status);
  printf("Data length : %d\n", hd.data_len);
  
  switch (tbl_id) {
	case 33 : 
	case 35 : 
      printf("Status buf: %d\n", bs.status);
      printf("Value  : %d\n", bs.value);
	break ;
	default : 
      printf("Status buf: %d\n", as.status);
      printf("Value  : %f\n", as.value);
  } 

  return 0;
}

