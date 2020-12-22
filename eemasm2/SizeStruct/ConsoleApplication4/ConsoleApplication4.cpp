// ConsoleApplication4.cpp: определяет точку входа для консольного приложения.
//
#pragma once

#include "stdafx.h"
#include "time.h"
#include "string.h"
#include <iostream>

using namespace std;

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

#pragma pack(push, 1)


/*------- Используется транспортом Ethernet под RMX ------------------*/
#define MAX_UDP_PACKET_LENGTH   1024  /* Максимальная длина пакета UDP */
#define MAX_HOST_NAME_LEN       64    /* Максимальная длина имени хоста */

typedef struct sysinfo_type {
    char        boot_dev[15];
    char        file_driver;
    char        boot_file[30];
    unsigned char   reserved1[4];
    unsigned char   monitor;
    uint32_t        sdm_intcpt_offs;
    uint16_t    sdm_intcpt_base;
    uint16_t    nuc_tick_interval;
    uint16_t    kn_tick_ratio;
    unsigned char   m_xface_vers;   /* Non-zero if DOSRMX 2.0b or */
    unsigned char   sscope_active;  /* beyond (RMK-based).        */
    unsigned char   no_video_reset;
    unsigned char   reserved2[17];
    unsigned char   board_type;
    uint16_t    delay_const;
    uint32_t    sysinfo_offs;
    uint16_t    sysinfo_base;
    unsigned char   bustype;
    unsigned char   cputype;
    uint32_t        debuginfo;
    unsigned char   time_zone;
    uint32_t    physical_memory;
    uint32_t    rom_memory_base;
    uint32_t    rom_memory_size;
    unsigned char   cpu_model;
    uint32_t        cpu_features;
    unsigned char   intel_reserved[14];
    unsigned char   user_reserved[32];
} SYSINFO_TYPE;

/* Информация о системе */
typedef struct SYS_INFO {
    uint16_t        HostID;
    unsigned char   boot_dev[15];
    unsigned char   file_driver;
    unsigned char   boot_file[30];
    uint16_t        nuc_tick_interval;
    uint16_t        kn_tick_ratio;
    unsigned char   board_type;
    uint16_t        delay_const;
    unsigned char   bustype;
    unsigned char   cputype;
    uint32_t        debuginfo;
    unsigned char   time_zone;
    uint32_t        physical_memory;
    uint32_t        rom_memory_base;
    uint32_t        rom_memory_size;
    unsigned char   cpu_model;
    uint32_t        cpu_features;
    uint16_t        JobNumber;
    uint16_t        TaskNumber;
    uint32_t        sd_device_size; /* Размер (в байтах) системного диска (:SD:) */
    uint32_t        sd_bytes_free;  /* Свободное место (в байтах) системного диска (:SD:) */
    uint32_t        sd_files_free;  /* Количество доступных файлов для создания на системном диске (:SD:) */
} SYS_INFO;


#define MAX_SYS_JOB_PER_HOST    64
typedef struct RMX_STR {
    unsigned char   len;
    unsigned char   str[81];
} RMX_STR;

typedef struct JOB_INFO {
    uint16_t        user_id;
    uint16_t        JobID;     /* Zolotov M.Yu. Подставил тип unsigned short вместо типа SELECTOR */
    int16_t         JobStatus;
    unsigned char   JobName[80];
    unsigned char   LoadName[80];
    uint16_t        TaskNumber;
    unsigned char   MaxPriority;
    unsigned char   ExceptMode;
    uint32_t        UsedMem;
    uint32_t        AvailMem;
} JOB_INFO;

#define MAX_JOB_PER_HOST        32  /* Максимальное число одновременно запущенных заданий РВ РСДУ на хосте */
#define MAX_PROCESSES_PER_HOST  256 /* Максимальное число когда-либо регистрированных заданий РВ РСДУ на хосте */
typedef struct JOBS_INFO {
    uint16_t    JobNumber;
    JOB_INFO    JobInfo[MAX_JOB_PER_HOST];
} JOBS_INFO;


typedef struct SYS_JOB {
    uint16_t    level;    /* Level ierarchy job */
    uint32_t    MemUsed;
    uint32_t    MemAvail;
    int32_t     JobToken;
    RMX_STR     JobName;
} SYS_JOB;

typedef struct SYS_JOB_INFO{
    uint16_t    hid;  /* Host ID (slot number) */
    uint16_t    sjn;  /* Sytem job number */
    SYS_JOB     sj[MAX_SYS_JOB_PER_HOST];   /* System job info array */
} SYS_JOB_INFO;

#define MAX_JOB_DIR_SIZE        256

typedef struct JOBDIRITEM {
    unsigned char   len;
    unsigned char   name[15];
    uint16_t        token_type;
    int32_t         token;
} JOBDIRITEM;

typedef struct JOB_DIR_INFO {
    uint16_t    dir_size;
    uint16_t    entire_use;
    JOBDIRITEM  dir_item[MAX_JOB_DIR_SIZE];
} JOB_DIR_INFO;


typedef struct HOST_INFO {
    uint16_t  host_id;
    unsigned char       host_name_len;
    unsigned char       host_name[MAX_HOST_NAME_LEN];
} HOST_INFO;

typedef struct /*Эта структура из RMX2.2 nucleus.h*/
{
    int32_t     owner_job;
    int32_t     next_task;
    /*    rmx_ptrdef(void far, exception_handler);*/
    char        exception_handler[6];
    uint8_t     exception_mode;
    uint8_t     fill0;
    uint8_t     static_priority;
    uint8_t     dynamic_priority;
    uint8_t     task_flags;
    uint8_t     interrupt_task;
    uint8_t     pending_interrupts;
    uint8_t     max_interrupts;
    uint16_t    int_level;
    uint8_t     task_state;
    uint8_t     suspend_depth;
    uint16_t    delay_request;
    int32_t     last_exchange;
} TASK_INFO_STRUCT;

#define MAX_TASK_PER_HOST   300 /* 26 byte info per task = 7800 byte */
typedef struct TASK_INFO {
    uint16_t            TaskNumber;
    TASK_INFO_STRUCT    TaskInfoStr[MAX_TASK_PER_HOST];
} TASK_INFO;


#define MAX_ADM_INFO_LEN    32768   /* For send/receive to PSB (no limit) */

typedef union ADM_INFO {
    char        GenericInfo[MAX_ADM_INFO_LEN];
    JOBS_INFO   JobsInfo;
    SYSINFO_TYPE    SysInfoType;
    SYS_INFO        SysInfo;
    SYS_JOB_INFO    SysJobInfo;
    TASK_INFO       TaskInfo;
    JOB_DIR_INFO    DirInfo;
    HOST_INFO       HostInfo;
} ADM_INFO;

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
    uint16_t type; /* тип протокола - описаны в db2const.h */
    union
    {
        NCS_ADDR addr_ncs;
        GENADDR  addr_eth;
        IP_ADDR  addr_ip;
    }addr;
} REM_ADDR;

/*Информация об отдельном хосте*/
typedef struct MB_HOST_INFO {
    int16_t     SessionErr;
    uint16_t    SlotStatus;
    uint16_t    user_id;
    REM_ADDR    host_addr;
    unsigned char   HostName[MAX_HOST_NAME_LEN];
    uint16_t        JobNumber;
    JOB_INFO        JobInfo[MAX_JOB_PER_HOST];
} MB_HOST_INFO;

typedef struct SET_TIME_STRUCT
{
    unsigned char   seconds;
    unsigned char   minutes;
    unsigned char   hours;
    unsigned char   days;
    unsigned char   months;
    uint16_t        years;
} SET_TIME_STRUCT;

typedef struct SYSMON_TCP
{
    uint16_t    user_id;
    uint16_t    job_id;
    uint16_t    host_count;
    uint16_t    host;
    union
    {
        ADM_INFO        adm_i;
        MB_HOST_INFO    mbh_i;
        SET_TIME_STRUCT sg_time;
    } data;
} SYSMON_TCP;

typedef struct SYSMON_TCP1
{
    uint16_t    user_id;
    uint16_t    job_id;
    uint16_t    host_count;
    uint16_t    host;
} SYSMON_TCP1;
  
typedef struct SYSMON_TCP2
{
    uint16_t    user_id;
    uint16_t    job_id;
    uint16_t    host_count;
    uint16_t    host;
	char        s[32768];
} SYSMON_TCP2;

typedef struct SYSMON_TCP3
{
    uint16_t    user_id;
    uint16_t    job_id;
    uint16_t    host_count;
    uint16_t    host;
	//ADM_INFO        adm_i;
    //SYS_INFO        SysInfo;
    uint16_t        HostID;
    unsigned char   boot_dev[15];
    unsigned char   file_driver;
    unsigned char   boot_file[30];
    uint16_t        nuc_tick_interval;
    uint16_t        kn_tick_ratio;
    unsigned char   board_type;
    uint16_t        delay_const;
    unsigned char   bustype;
    unsigned char   cputype;
    uint32_t        debuginfo;
    unsigned char   time_zone;
    uint32_t        physical_memory;
    uint32_t        rom_memory_base;
    uint32_t        rom_memory_size;
    unsigned char   cpu_model;
    uint32_t        cpu_features;
    uint16_t        JobNumber;
    uint16_t        TaskNumber;
    uint32_t        sd_device_size; /* Размер (в байтах) системного диска (:SD:) */
    uint32_t        sd_bytes_free;  /* Свободное место (в байтах) системного диска (:SD:) */
    uint32_t        sd_files_free;  /* Количество доступных файлов для создания на системном диске (:SD:) */
} SYSMON_TCP3;

typedef struct SYSMON_TCP5
{
    uint16_t    user_id;
    uint16_t    job_id;
    uint16_t    host_count;
    uint16_t    host;
	//MB_HOST_INFO    mbh_i;
    int16_t     SessionErr;
    uint16_t    SlotStatus;
    uint16_t    user_id2222222222;
    struct {
		uint16_t type; /* тип протокола - описаны в db2const.h */
      union
      {
        struct {
    unsigned char    addrlength;
    unsigned char    unused;
    uint16_t         portid;
    unsigned char    address[28];
} addr_ncs;
        struct 
{
    uint32_t r_sock;
}  addr_eth;
        struct 
{
    uint32_t ip_addr;
    uint16_t       ip_port;
}   addr_ip;
	}addr; } host_addr;
    unsigned char   HostName[MAX_HOST_NAME_LEN];
    uint16_t        JobNumber;
    //JOB_INFO        JobInfo[MAX_JOB_PER_HOST];
 struct JOB_INFO {
    uint16_t        user_id;
    uint16_t        JobID;     /* Zolotov M.Yu. Подставил тип unsigned short вместо типа SELECTOR */
    int16_t         JobStatus;
    unsigned char   JobName[80];
    unsigned char   LoadName[80];
    uint16_t        TaskNumber;
    unsigned char   MaxPriority;
    unsigned char   ExceptMode;
    uint32_t        UsedMem;
    uint32_t        AvailMem;
} JobInfo[MAX_JOB_PER_HOST];
} SYSMON_TCP5;


typedef  struct SYSMON_DATA
{
    unsigned short      user_id;
    unsigned short      job_id;
    unsigned short      host_count;
    unsigned short      host;
    union {
    ADM_INFO            adm_i;
    MB_HOST_INFO        mbh_i;
    SET_TIME_STRUCT     sg_time;
    } data;
}SYSMON_DATA33;

#define MAX_HOSTS 20

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

#pragma pack(pop)

// from rsdutime.c
time_t RSDURTGUtils_Time70(void)
{
    uint32_t t;
    t = time(0);
    return(t);
}

#define GCMD_DATASERVICEINIT            (uint16_t)0xf006      /* Этой командой отвечает dbcpd*/
#define GCMD_DATASERVICEEXIT            (uint16_t)0xf007      /* Эта команда используется для завершения сеанса с dbcpd*/


#define SMC_MAKE_RESERVE_RSDU_JOB   (uint16_t)112
#define SMC_MAKE_RESERVE_RSDU_JOBS  (uint16_t)113 

int _tmain(int argc, _TCHAR* argv[])
{
    UNITRANS_HEADER     cd, cd_init, cd_close;
    SYSMON_TCP          SysmonData;

	SYSMON_TCP  sm_hdr;
	SYSMON_DATA33 *p_sm_data[MAX_HOSTS];

	memset(&SysmonData, 0, sizeof(SysmonData));
    SysmonData.host_count = 1;
    SysmonData.host = 0;
    SysmonData.user_id = 1024;
    SysmonData.job_id  = 5;


    cd.command  = SMC_MAKE_RESERVE_RSDU_JOB;
    cd.data_len = sizeof(SysmonData);
    cd.dst_uid  = 0;
    cd.src_uid  = 1;
    cd.src_laws.laws = 0xFFFFFFFF;
    cd.status   = 0;
    cd.time1970 = RSDURTGUtils_Time70();
    cd.param1   = 1;
    cd.param2   = 0;
    cd.param3   = 0;

    cd_close.command  = GCMD_DATASERVICEEXIT;
    cd_close.data_len = 0;
    cd_close.dst_uid  = 0;
    cd_close.src_uid  = 1;
    cd_close.status   = 0;
    cd_close.time1970 = RSDURTGUtils_Time70();
    cd_close.param1   = 0;
    cd_close.param2   = 0;
    cd_close.param3   = 0;


	std::cout << "sizeof(SysmonData) =" << sizeof(SysmonData) << endl;
	std::cout << "sizeof(cd) =" << sizeof(cd) << endl;
	std::cout << "sizeof(cd_close) =" << sizeof(cd_close) << endl;

	std::cout << "sizeof(SYSMON_TCP2) =" << sizeof(SYSMON_TCP2) << endl;
	std::cout << "sizeof(SYSMON_TCP1) =" << sizeof(SYSMON_TCP1) << endl;
	std::cout << "sizeof(SYSMON_TCP) =" << sizeof(SYSMON_TCP) << endl;
	std::cout << "sizeof(ADM_INFO) =" << sizeof(ADM_INFO) << endl;
	std::cout << "sizeof(MB_HOST_INFO) =" << sizeof(MB_HOST_INFO) << endl;
	std::cout << "sizeof(SET_TIME_STRUCT) =" << sizeof(SET_TIME_STRUCT) << endl;
	std::cout << "sizeof(SYSMON_TCP3) =" << sizeof(SYSMON_TCP3) << endl;
	std::cout << "sizeof(SYSMON_TCP5) =" << sizeof(SYSMON_TCP5) << endl;

	std::cout << "sizeof(SYSMON_DATA33) =" << sizeof(SYSMON_DATA33) << endl;
	std::cout << "sizeof(p_sm_data) =" << sizeof(p_sm_data) << endl;
	const int numOfAnswers =0;
 const int len= 5810;
			p_sm_data[numOfAnswers] = (SYSMON_DATA33 *)malloc(len);
			memset(p_sm_data[numOfAnswers], 0, len);
			std::cout << "sizeof(p_sm_data) =" << sizeof(p_sm_data[numOfAnswers]) << endl;

  std::cout << "sizeof(REM_ADDR) =" << sizeof(REM_ADDR) << endl;
  std::cout << "sizeof(JOB_INFO) =" << sizeof(JOB_INFO) << endl;



	return 0;
}

