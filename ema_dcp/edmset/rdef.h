#pragma once

#ifndef __H1__INCLUDED__
#define __H1__INCLUDED__


#include "winint.h"
#include "rsdulogs.h"
#include "rsdutime.h"

//#define LOG_CONTAINER FILE


#define UINT_8  unsigned char
#define UINT_16 uint16_t
#define UINT_32 uint32_t
#ifndef _MAX_STRING
#define _MAX_STRING 255
#endif
typedef struct {
    uint8_t             length;
    uint8_t             text[_MAX_STRING];    /* adjust # of bytes in string */
} RMX_STRING;

#ifdef WIN32
    typedef int socklen_t;
#endif /* WIN32 */


#ifdef WIN32

//#define WSAETIMEDOUT 10060L
//#define WSAEINVAL    10022L
#define E_TIME       WSAETIMEDOUT
#define E_PARAM      WSAEINVAL

#define E_MEM        ENOMEM
#define E_EXIST      EIO
#define E_OK         0
#define E_SQLEXCEPT  200
#define E_ALERTRCV   201

#define WAIT_FOREVER   0xFFFF
#define NO_WAIT        0
#endif

/*Команды серверам данных*/
#define GCMD_GLOBAL_STATE_STOPPED       (uint16_t)0xf00f      /* Текущее состояние - остановлен */
#define GCMD_GET_GLOBAL_STATE           (uint16_t)0xf008      /* Получить статус сервера (SYSTEMMASTER или SYSTEMSLAVE) */
#define GCMD_GLOBAL_STATE_MASTER        (uint16_t)0xf009      /* Ответ сервера SYSTEMMASTER */
#define GCMD_GLOBAL_STATE_SLAVE         (uint16_t)0xf00a      /* Ответ сервера SYSTEMSLAVE */
#define GCMD_SET_GLOBAL_STATE_MASTER    (uint16_t)0xf00b      /* Set SYSTEMMASTER */
#define GCMD_SET_GLOBAL_STATE_SLAVE     (uint16_t)0xf00c      /* Set SYSTEMSLAVE */
#define GCMD_SEND_TO_SLAVE              (uint16_t)0xf00d      /* Отправить данные резервному комплексу */
#define GCMD_ECHO                       (uint16_t)0xfffe      /* Эхо - запрос*/
#define E_DELETE_YOURSELF               (uint16_t)0xFFED      /* Unload JOB */
#define SYS_DELETE_YOURSELF             (uint16_t)0xFFDD      /* Unload JOB */

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

#define DCP_USERCOMMENT_MAX_SIZE 256 /* Максимальный размер комментария пользователя с учетом кодировки и символа конца строки - 255 символов */


#ifndef MAX_LOGIN_LEN
#define MAX_LOGIN_LEN   16      /* Максимальная длина логина БД процесса (15 символов) */
#endif
#ifndef MAX_PASSWD_LEN
#define MAX_PASSWD_LEN  33      /* Максимальная длина пароля (32 символа) */
#endif
#ifndef MAX_PATH_LEN
#define MAX_PATH_LEN    256    /* Максимальная длина пути к файлу в файловой системе */
#endif
/* 2.02.2004. Яковлев А.В. */
/* Добавление ACCESS_DATA */
/* Яковлев А.В. 2.02.2004 */
/* Структура для посылки в DCP-командах, */
/* требующих изменений в БД от имени пользователя-человека */
typedef struct ACCESS_DATA
{
    int8_t login[MAX_LOGIN_LEN];     /* Логин пользователя БДТИ */
    int8_t password[MAX_PASSWD_LEN]; /* Пароль пользователя БДТИ */
} ACCESS_DATA;

/* 17 марта 2003г., Макаренко Е.А.: значения ручного ввода */ 
typedef struct DCP_ASTRUCT_MNL
{
    uint32_t tbl_id;
    uint32_t id;
    uint32_t id_channel;
    double   value;
    uint32_t status;
    ACCESS_DATA access_data; /* Данные пользователя для подключения к БД */
} DCP_ASTRUCT_MNL;

typedef struct DCP_ASTRUCT_MNL_COMMENT
{
    uint32_t tbl_id;         /* Идентификатор таблицы списка */
    uint32_t id;             /* Идентификатор параметра в таблице списке */
    uint32_t id_channel;     /* Идентификатор канала источника "Оператор" */
    double   value;          /* Значение ручного ввода */
    uint32_t status;         /* Статус */
    ACCESS_DATA access_data; /* Данные пользователя для подключения к БД */
    int8_t   comment[DCP_USERCOMMENT_MAX_SIZE]; /* Комментарий пользователя (СИ-строка, кодировка ASCII Windows-1251) */
} DCP_ASTRUCT_MNL_COMMENT;

typedef struct DCP_DSTRUCT_MNL
{
    uint32_t tbl_id;
    uint32_t id;
    uint32_t id_channel;
    uint32_t value;
    uint32_t status;
    ACCESS_DATA access_data; /* Данные пользователя для подключения к БД */
} DCP_DSTRUCT_MNL;

typedef struct DCP_DSTRUCT_MNL_COMMENT
{
    uint32_t tbl_id;         /* Идентификатор таблицы списка */
    uint32_t id;             /* Идентификатор параметра в таблице списке */
    uint32_t id_channel;     /* Идентификатор канала источника "Оператор" */
    uint32_t value;          /* Значение ручного ввода */
    uint32_t status;         /* Статус */
    ACCESS_DATA access_data; /* Данные пользователя для подключения к БД */
    int8_t   comment[DCP_USERCOMMENT_MAX_SIZE]; /* Комментарий пользователя (СИ-строка, кодировка ASCII Windows-1251) */
} DCP_DSTRUCT_MNL_COMMENT;

typedef struct DCP_BSTRUCT_MNL
{
    uint32_t tbl_id;
    uint32_t id;
    uint32_t id_channel;
    uint32_t value;
    uint32_t status;
    ACCESS_DATA access_data; /* Данные пользователя для подключения к БД */
} DCP_BSTRUCT_MNL;

typedef struct DCP_BSTRUCT_MNL_COMMENT
{
    uint32_t tbl_id;         /* Идентификатор таблицы списка */
    uint32_t id;             /* Идентификатор параметра в таблице списке */
    uint32_t id_channel;     /* Идентификатор канала источника "Оператор" */
    uint32_t value;          /* Значение ручного ввода */
    uint32_t status;         /* Статус */
    ACCESS_DATA access_data; /* Данные пользователя для подключения к БД */
    int8_t   comment[DCP_USERCOMMENT_MAX_SIZE]; /* Комментарий пользователя (СИ-строка, кодировка ASCII Windows-1251) */
} DCP_BSTRUCT_MNL_COMMENT;


typedef    union  DCP_INT_DATALIST {
    DCP_ASTRUCT_MNL          an_mnl[1];            /* для новых команд получения/записи значений ручного ввода */ 
    DCP_DSTRUCT_MNL          dg_mnl[1];
    DCP_BSTRUCT_MNL          bi_mnl[1];
    DCP_ASTRUCT_MNL_COMMENT  an_mnl_comment[1];    /* для новых команд получения/записи значений ручного ввода (+комментарий пользователя) */
    DCP_DSTRUCT_MNL_COMMENT  dg_mnl_comment[1];
    DCP_BSTRUCT_MNL_COMMENT  bi_mnl_comment[1];
} DCP_INT_DATALIST;

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


#define MAX_UDP_PACKET_LENGTH   1024  /* Максимальная длина пакета UDP */
#define MAX_HOST_NAME_LEN       64    /* Максимальная длина имени хоста */


typedef struct GENADDR {
    unsigned char    addrlength;
    unsigned char    unused;
    uint16_t         portid;
    unsigned char    address[28];
} GENADDR;


typedef struct NCS_ADDR
{
    uint32_t r_sock;
} NCS_ADDR;


#define IP_ADDR_ALL (uint32_t)0x00000000

typedef struct IP_ADDR
{
    uint32_t ip_addr;
    uint16_t       ip_port;
} IP_ADDR;

typedef struct REM_ADDR
{
    uint16_t type; /*  db2const.h */
    union
    {
        NCS_ADDR addr_ncs;
        GENADDR  addr_eth;
        IP_ADDR  addr_ip;
    }addr;
} REM_ADDR;

/* Справочник поддерживаемых протоколов */

#define ADV_PROTO_COMMON                 0 /* Протокол агента, брать значение из RSDU_GLOBAL */
#define ADV_PROTO_TCP                    1 /* TCP протокол            */
#define ADV_PROTO_UDP                    2 /* User Datagram protocol  */
#define ADV_PROTO_NCS                    3 /* Nucleus Communication Services */
#define ADV_PROTO_OPERATOR               4 /* Ручной ввод оператора   */
#define ADV_PROTO_CALC                   5 /* Математический расчет   */
#define ADV_PROTO_FIFO                   6 /* Именованный программный канал */
#define ADV_PROTO_DDA                    7 /* Непосредственный доступ к данным */
#define ADV_PROTO_ETH                    8 /* Ethernet Services       */
#define ADV_PROTO_UDP_CLEAR              9 /* Чистый UDP без подтверждений    */

typedef enum TRANSPORT_PROTO_
{
  TransportProto_Common        = 0,
  TransportProto_TCP           = 1,
  TransportProto_UDP           = 2,
  TransportProto_NCS           = 3,
  TransportProto_OPERATOR      = 4,
  TransportProto_CALC          = 5,
  TransportProto_FIFO          = 6,
  TransportProto_DDA           = 7,
  TransportProto_ETH           = 8,
  TransportProto_UDP_CLEAR     = 9
} TRANSPORT_PROTO_t;


#ifndef RMX
#if _LONG64_
#define NATIVE_WORD uint32_t
#else
#ifdef __INT16__
#define NATIVE_WORD uint16_t
#else    /* __INT32__ */
#define NATIVE_WORD uint32_t
#endif
#endif
#endif


#define ETHERNET_ADDR_SIZE          6
#define LOOPBACK_INTERFACE_NAME     {3,"lo0"}
#define ETHERNET_INTERFACE_NAME1    {4,"eth1"}
#define ETHERNET_INTERFACE_NAME0    {4,"eth0"}
#define ETHERNET_ADDR_LOOPBACK      {(char)0x00,(char)0x00,(char)0x00,(char)0x00,(char)0x00,(char)0x00}
#define ETHERNET_ADDR_BROADCAST     {(char)0xFF,(char)0xFF,(char)0xFF,(char)0xFF,(char)0xFF,(char)0xFF}

/* Минимально возможное число - 0. (защита от деления на ноль) */
#define EPS_VALUE         1.0e-7
/* Проверка числа на ноль */
#define RSDUMathUtils_IsZero(x)     (fabs((double)(x))<EPS_VALUE?TRUE:FALSE)

typedef struct RECEIVE_REPLY {
    uint16_t  port_id;
    uint16_t  sync_port_d;
    uint32_t  frag_len;
    int32_t   reply_status;
    char      addr[ETHERNET_ADDR_SIZE];
    uint16_t  trans_id;
    uint16_t  crc16;
} RECEIVE_REPLY;

#define TRANS_TIMEOUT   (uint16_t)1000/*400-было мало*/ /* В десятках милисекуннд */
/*#define MAX_SESSION_TO_PORT (uint16_t)20*/ /* А почему 20?? Этого мало... */
#define MAX_SESSION_TO_PORT (uint16_t)64

typedef struct SESSION_TYPE {
    int32_t      s_port; /*  Not used in linux projects now. */
    time_t       insert_time;
    char far    *msg;
} SESSION_TYPE;

typedef struct SERVICE_PORT {
    int32_t         main_port; /*  Not used in linux projects now. */
    GENADDR         main_addr;
    RMX_STRING      main_ifname;
    int32_t         loop_port; /*  Not used in linux projects now. */
    GENADDR         loop_addr;
    RMX_STRING      loop_ifname;
    int32_t         sink_port; /*  Not used in linux projects now. */
    int32_t         sync_port_main; /*  Not used in linux projects now. */
    int32_t         sync_port_loop; /*  Not used in linux projects now. */
    int32_t         buff_pool; /*  Not used in linux projects now. */
    int32_t         current_receive_channel; /*  Not used in linux projects now. */
    uint16_t        port_id;
    uint16_t        sync_port_id_main;
    uint16_t        sync_port_id_loop;
    uint16_t        ctr_msg_size;
    uint16_t        current_channel;
    uint16_t        session_num;
    SESSION_TYPE    ses_port[MAX_SESSION_TO_PORT];
    uint16_t        trans_id;
} SERVICE_PORT;
/*#endif*/

/*************************************************************
* Прототипы новых транспортных функций, не зависящих от     *
* протокола                                                 *
*************************************************************/
typedef struct NCS_SOCKET {
    uint16_t      HostID;
    uint16_t      PortID;
} NCS_SOCKET;

#define MAX_ADM_MESSAGE_LEN     16      /* NCS port format limit*/

typedef struct COMM_PORT_NCS
{
    uint16_t   port_id;

    int32_t    serv_port; /*  Not used in linux projects now. */

    uint32_t  *clock_source;
} COMM_PORT_NCS;




#define UDP_VERSION         1

/*    Статусы пакета    */
#define STATE_OK            0x0    /* При передаче дейтаграмм с заголовком и данными от источника к приемнику */
#define STATE_RETRY         0x1 /* При повторной передаче дейтаграммы с заголовком и данными от источника к приемнику */
#define STATE_KVIT_TRUE     0x2    /* Квитанция: положительное подтверждение приема всех дейтаграмм окна */
#define STATE_KVIT_FALSE    0x3    /* Квитанция: отрицательное подтверждение. Не все дейтаграммы приняты правильно */

/*    Биты управляющей информации    */
#define CNTL_COMMON         0x0    /*    Обычный пакет с данными    */
#define CNTL_BROADCAST      0x1    /*    Широковещательное сообщение    */
#define CNTL_ACCEPT         0x2    /*    Квитанция    */
#define CNTL_CLOCLSYNC      0x4    /*    Флаг синхронизации часов    */
#define CNTL_PRIORITY       0x8    /*    Установлен приоритет    */

#define DEF_WINDOW_SIZE     5
#define DEF_RETRY_NUM       1

/*    Структура заголовка дейтаграммы    */
typedef struct UDP_HEADER
{
    unsigned char   version;    /*    Версия транспорта    */
    unsigned char   cntl;       /*    Управляющая информация    */
    uint32_t        trans_id;   /*    Идентификатор транзакции    */
    uint32_t        length;     /*    Длина пакета    */
    uint32_t        dg_num;     /*    Номер дейтаграммы    */
    unsigned char   priority;   /*    Приоритет    */
    uint16_t        snd_timeout;/*    Таймаут передатчика    */
    int32_t         return_port;/*    Номер порта (для квитанций)    */
    unsigned char   status;     /*    Статус пакета    */
}UDP_HEADER;

/*    Буфер пакета    */
typedef struct PACKET_ITEM
{
    struct IP_ADDR      ip_addr;     /*    Адрес отправителя пакета    */
    struct UDP_HEADER   hdr[DEF_WINDOW_SIZE];        /*    Заголовки принятых дейтаграмм окна    */
    uint32_t            num_hdr;     /*    Число принятых дейтаграмм пакета */
    char               *databuf;     /*    Область данных пакета    */
    struct RSDU_timeval rcv_stamp;   /*    Время приема первой дейтаграммы пакета */
    struct RSDU_timeval dead_stamp;  /*    Время приема первой дейтаграммы пакета + таймаут передатчика    */
    uint32_t            trans_id;    /*    Идентификатор транзакции    */
    uint32_t            length;      /*    Полная длина пакета    */
    uint32_t            rcv_size;    /*    Сколько байт осталось принять    */
    int32_t             top_dg_num;  /*    Все дейтаграммы до этой включительно приняты    */
}PACKET_ITEM;



typedef struct COMM_PORT_IP
{
    uint16_t port_id;    /* Свой порт */
    uint16_t accept_port_id;    /* Свой порт */
    int      sock;       /* Сокет */
    int      return_sock; /* Сокет для квитанций */
    IP_ADDR  remote_address;
    uint16_t flags;
    uint32_t trans_id;
    PACKET_ITEM    bufstr[MAX_SESSION_TO_PORT];
    uint32_t num_bufstr;
    int32_t  err_sock;
    int32_t  err_return_sock;
} COMM_PORT_IP;

typedef struct COMM_PORT
{
    /* Методы для работы с портом, перегрузка в создании порта */
    void far (*port_delete)(struct COMM_PORT far *s_port,
        LOG_CONTAINER *lfp);
    void far (*port_clear) (struct COMM_PORT far *s_port);
    int32_t  far (*port_send) (struct COMM_PORT far *s_port,
        REM_ADDR far *snd_addr,
        UNITRANS_HEADER far *theader,
        unsigned char far *data,
        uint16_t time_out,
        uint16_t far *state,
        LOG_CONTAINER *lfp);
    int32_t far (*port_receive)(struct COMM_PORT far *s_port,
        REM_ADDR far *rcv_addr,
        UNITRANS_HEADER far *theader,
        unsigned char far **data,
        uint16_t time_out,
        uint16_t far *state,
        LOG_CONTAINER *lfp);
    int32_t far (*port_set_opt)(struct COMM_PORT far *s_port,
        int32_t optname,
        const char far *optval,
        int32_t optlen,
        uint16_t far *p_state,
        LOG_CONTAINER *lfp);
    uint16_t type; /*тип протокола ADV_PROTO_ETH или ADV_PROTO_NCS - описаны в db2const.h*/
    union
    {
        COMM_PORT_NCS port_ncs;
        SERVICE_PORT  port_eth;
        COMM_PORT_IP  port_ip;
    } service;
} COMM_PORT;




#endif // __H1__INCLUDED__