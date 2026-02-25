// eemaunit.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <winsock2.h>
#include <windows.h>
#include <time.h>

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
typedef unsigned long int uint64_t;
#endif // WIN32

// from rsdu2def.h

/*Команды серверам данных*/
#define GCMD_GLOBAL_STATE_STOPPED       (uint16_t)0xf00f      /* Текущее состояние - остановлен */
#define GCMD_GET_GLOBAL_STATE           (uint16_t)0xf008      /* Получить статус сервера (SYSTEMMASTER или SYSTEMSLAVE) */
#define GCMD_GLOBAL_STATE_MASTER        (uint16_t)0xf009      /* Ответ сервера SYSTEMMASTER */
#define GCMD_GLOBAL_STATE_SLAVE         (uint16_t)0xf00a      /* Ответ сервера SYSTEMSLAVE */
#define GCMD_SET_GLOBAL_STATE_MASTER    (uint16_t)0xf00b      /* Set SYSTEMMASTER */
#define GCMD_SET_GLOBAL_STATE_SLAVE     (uint16_t)0xf00c      /* Set SYSTEMSLAVE */
#define GCMD_SEND_TO_SLAVE              (uint16_t)0xf00d      /* Отправить данные резервному комплексу */
#define GCMD_ECHO                       (uint16_t)0xfffe      /* Эхо - запрос*/
#define GCMD_DATASERVICEINIT            (uint16_t)0xf006      /* Этой командой отвечает dbcpd*/
#define GCMD_DATASERVICEEXIT            (uint16_t)0xf007      /* Эта команда используется для завершения сеанса с dbcpd*/
#define E_DELETE_YOURSELF               (uint16_t)0xFFED      /* Unload JOB */
#define SYS_DELETE_YOURSELF             (uint16_t)0xFFDD      /* Unload JOB */
#define SYS23_DELETE_YOURSELF           (uint16_t)0x00FF      /* iRMX III 2.3 */
/* необходимо для сервера доступа  / добавил Кранчев Д.Ф. 27.01.04 */
#define GCMD_GETDATASERVICEINFO         (uint16_t)0xf003
#define GCMD_GETDATASERVICESTAT         (uint16_t)0xf004
#define GCMD_DATASERVICEDISPOSE         (uint16_t)0xf005
/* необходимо для щита  / добавил Кранчев Д.Ф. 20.04.04 */
#define GERR_UNKNOWNCOMMAND             0x2105


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
/* Права пользователя */
typedef struct KEY_LAWS {
    uint32_t  laws;
    char      key[16];
} KEY_LAWS;
#pragma pack(pop)

#pragma pack(push,1)
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
#pragma pack(pop)

// from rsdutime.c
time_t RSDURTGUtils_Time70(void)
{
    uint32_t t;
    t = time(0);
    return(t);
}


#define PORT 2003
#define SERVERADDR "192.168.120.31"


int main(int argc, char* argv[])
{
  char buff[1014];

  UNITRANS_HEADER hd, hdr;
  INT err;

  memset(&hd, 0, sizeof(UNITRANS_HEADER));
  
  hd.command = GCMD_GET_GLOBAL_STATE;
  hd.src_uid = 0xffff ; // 0x0 0xffff  env->UserID
  hd.dst_uid = 0xffff ;// 0xffff 0  ADM_SYSMONUSERID;
  hd.param1  = 0xffff ; // 0xffff
  hd.param2  = 0xffff ;// 0xffff  0 ADM_GROUPID;
  //hd.param3
  hd.src_laws.laws = 0xffffffff;
  hd.data_len = 0 ;
  hd.status = E_OK ;  // E_OK = 0
  hd.time1970 = RSDURTGUtils_Time70(); // RSDURTGUtils_Time70() static_cast<UINT>(time(NULL))

//8fffffff000ffffffffffffffff00ffffffffffffffff00ffffffffffffffff00ffffffffffffffff000000ffffffffffffffffffffffffffffffff000000000000000000000053ffffff81ffffffce5d

  // Шаг 1 - иницилизация библиотеки Winsocks
  if (WSAStartup(0x202,(WSADATA *)&buff[0]))
  {
    printf("WSAStartup error: %d\n",  WSAGetLastError());
    return -1;
  }

  // Шаг 2 - открытие сокета
  SOCKET my_sock=socket(AF_INET, SOCK_DGRAM, 0);
  if (my_sock==INVALID_SOCKET)
  {
    printf("socket() error: %d\n",WSAGetLastError());
    WSACleanup();
    return -1;
  }

  // Шаг 2.1 - установка свойств сокета
  int snd_timeout = 1000;
  int rcv_timeout = 1000;
  BOOL  fBroadcast = TRUE;

  err = setsockopt ( my_sock, SOL_SOCKET, SO_BROADCAST, (CHAR *) &fBroadcast, sizeof ( BOOL ));
  if (err == SOCKET_ERROR) { closesocket ( my_sock ); return err; }
  err = setsockopt ( my_sock, SOL_SOCKET , SO_RCVTIMEO, (const char *)&rcv_timeout, sizeof(int));
  if (err == SOCKET_ERROR) { closesocket ( my_sock ); return err; }
  err = setsockopt ( my_sock, SOL_SOCKET , SO_SNDTIMEO, (const char *)&snd_timeout, sizeof(int));
  if (err == SOCKET_ERROR) { closesocket ( my_sock ); return err; }

  // Шаг 3 - обмен сообщений с сервером
  HOSTENT *hst;
  sockaddr_in dest_addr;

  dest_addr.sin_family=AF_INET;
  dest_addr.sin_port=htons(PORT);

  // определение IP-адреса узла
  if (inet_addr(SERVERADDR))
    dest_addr.sin_addr.s_addr=inet_addr(SERVERADDR);
  else
    if (hst=gethostbyname(SERVERADDR))
      dest_addr.sin_addr.s_addr=((unsigned long **)hst->h_addr_list)[0][0];
    else
    {
      printf("Unknown host: %d\n",WSAGetLastError());
      closesocket(my_sock);
      WSACleanup();
      return -1;
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

  char b[sizeof(UNITRANS_HEADER)+1];
  memcpy(b,(char *) &hd, sizeof(hd));
  for(size_t i=0; i<sizeof(hd); i++) printf("%x",b[i]);
  // printf("%x",static_cast<unsigned char>(b[i]));

  // Передача сообщений на сервер
  err = sendto(my_sock,(char *)&hd, sizeof(hd),0,
      (sockaddr *) &dest_addr,sizeof(dest_addr));
  if ( err == SOCKET_ERROR )
  {
    printf("sendto: err=%d GetLastError() = %d\n",err,GetLastError());
    closesocket ( my_sock );
    return err;
  }

  memset(&hdr, 0, sizeof(UNITRANS_HEADER));
  
  // Прием сообщения с сервера
  int server_addr_size=sizeof(server_addr);

  int n=recvfrom(my_sock,(char *)&hdr, sizeof(hdr),0,
      (sockaddr *) &server_addr, &server_addr_size);

  if (n==SOCKET_ERROR)
  {
      printf("recvfrom() error: %d\n",WSAGetLastError());
      closesocket(my_sock);
      WSACleanup();
      return -1;
  }
  
  b[0]='\0';
  memcpy(b,(char *) &hdr, sizeof(hdr));
  for(size_t i=0; i<sizeof(hdr); i++) printf("%x",b[i]);
  //printf("%x",static_cast<unsigned char>(b[i]));
//9fffffff000ca00ffffffffffffffff004000ffffffffffffffff000000ffffffffffffffffffffffffffffffff000000000000000000000053ffffff81ffffffce5d

  printf("\n" );
  printf("\n %s : %d ", SERVERADDR , PORT);
  printf("\nhd.command = %x", hdr.command );
  printf("\nhd.src_uid = %x", hdr.src_uid );
  printf("\nhd.param1 = %x", hdr.param1 );
  printf("\nhd.status = %x", hdr.status );
  printf("\n" );

  // шаг последний - выход
  closesocket(my_sock);
  WSACleanup();

  return 0;
}

