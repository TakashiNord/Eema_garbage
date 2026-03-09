
#pragma once

#ifndef __RSDULOGS_H__INCLUDED__
#define __RSDULOGS_H__INCLUDED__

/************************************************************************/
/* Функции для работы с логами                                            */
/************************************************************************/

#include "utilsapi.h"

/* Ефимов М.Г. 09.08.2011 */
/* Определения источника и ключа вывода для всех логов. */

#define LOG_SRC LOG_LOCAL0
#define LOG_KEY LOG_PID | LOG_NDELAY
#define LOG_MAXFILENAME  256
#define LOG_MAXSLHEADER  256

#define LOG_MODE_FILE    0
#define LOG_MODE_SYSLOG  1

/* UPDATE 15.08.2011 - заголовки для серверов */
#define LOG_CAPT_LSMAGENT    "LSMAGENT"
#define LOG_CAPT_SSBSD       "SSBSD"
#define LOG_CAPT_SYSMON      "SYSMON"
#define LOG_CAPT_BRIDGE      "BRIDGE"
#define LOG_CAPT_ACSRVD      "ACSRVD"
#define LOG_CAPT_DPLOAD      "DPLOAD"
#define LOG_CAPT_SBRIDGE     "SBRIDGE"
#define LOG_CAPT_OICDCPD     "OICDCPD"
#define LOG_CAPT_ELREG       "ELREG"
#define LOG_CAPT_PHREG       "PHREG"
#define LOG_CAPT_PWS         "PWS"
#define LOG_CAPT_AUTOMAT     "AUTOMAT"
#define LOG_CAPT_DCS         "DCS"

/**
 * @brief Log threads information defines.
 **/
#define LSI_MASK_ALL            0x00000001 // Mask for all flags
#define LSI_NONE                0x00000000 // None output thread information
#define LSI_SHOW_THREADID       0x00000001 // show thread id information


#ifdef LINUX
#define LOG_CONTAINER    LOG_HEADER
typedef struct LOG_HEADER
{
    LOG_HEADER() : AppLogFile(NULL), LogFile(NULL), MsgBuffer(NULL), GlobalDebug(0), mode(LOG_MODE_FILE), timemask(0), flShowInfo(LSI_NONE), IsInitialized(0) {}
    FILE       *AppLogFile; // Application log file descriptor
    FILE       *LogFile; // Local log file descriptor
    int         fdAppLogFile;
    int         fdLogFile;
    char        AppLogFileName[LOG_MAXFILENAME]; // Application log file name
    char        FileName[LOG_MAXFILENAME]; // File name for local log
    char        SyslogHeader[LOG_MAXSLHEADER];
    char       *MsgBuffer;
    int32_t     DebugMask;
    int32_t     GlobalDebug;
    int32_t     mode; // log type File/Syslog
    int32_t     timemask;
    uint32_t    flShowInfo; // Flags for put to log some information
    int         IsInitialized;
} LOG_HEADER;
#else
#define LOG_CONTAINER    FILE
#define LOG_HEADER    FILE
#endif

#define LOG_NOTIMESTAMP    8

// Log messages types from syslog, for correct comile for windows.
#ifdef WIN32
#define LOG_EMERG   0   /* system is unusable */
#define LOG_ALERT   1   /* action must be taken immediately */
#define LOG_CRIT    2   /* critical conditions */
#define LOG_ERR     3   /* error conditions */
#define LOG_WARNING 4   /* warning conditions */
#define LOG_NOTICE  5   /* normal but signification condition */
#define LOG_INFO    6   /* informational */
#define LOG_DEBUG   7   /* debug-level messages */
#define LOG_MAX   8


#define LOG_PRIMASK 0x0007  /* mask to extract priority part (internal) */
#define LOG_PRI(p)  ((p) & LOG_PRIMASK) /* extract priority */
#endif // WIN32

/**
 * @brief Log levels list
 *
 * @warning Сделано как временный вариант. Лучше в будующем полностью отказаться
 *          от использования системных уровней логирования и использовать свои
 *          и просто мапить их в системные.
 **/
typedef enum LLTypes
{
    llEmerg = LOG_EMERG,
    llAlert = LOG_ALERT,
    llCrit  = LOG_CRIT,
    llError = LOG_ERR,
    llWarn  = LOG_WARNING,
    llNotic = LOG_NOTICE,
    llInfo  = LOG_INFO,
    llDebug = LOG_DEBUG,
    llMax   = LOG_MAX
} LLTypes_t;

/**
 * @brief Log level names. Definition located in the logmanager.c
 **/
extern const char *LLNames[llMax];

int RSDURTGUtils_UnilogMessage(int32_t priority, LOG_CONTAINER* LogContainer, const char *message, ...);

/* Инициализация логера */
int RSDURTGUtils_LogInit(LOG_CONTAINER* LogHeader,
                         int32_t        DebugMask,
                         const char    *AppLogFileName,
                         const char    *LogFileName,
                         const char    *CallFunctionName,
                         uint32_t       flagShowInfo = LSI_NONE);

/* Деинициализация логера */
int RSDURTGUtils_LogClose(LOG_CONTAINER* LogHeader);


/* Изменение уровня отладки. */
int RSDURTGUtils_ChangeDebug(int new_debug, char BinName[]);


/* Проверка состояния отладки */
int RSDURTGUtils_CheckDebug(LOG_CONTAINER* LogContainer);

/* Получение текущего уровня отладки */
int32_t RSDURTGUtils_GetDebug(char BinName[]);


#ifdef LINUX
/* Включить отображение времени(по умолчанию) */
void RSDURTGUtils_LogTimeOn(LOG_CONTAINER* LogHeader);

/* Выключить отображение времени */
void RSDURTGUtils_LogTimeOff(LOG_CONTAINER* LogHeader);
#endif

/* Обновление GlobalDebug в контейнере файла */
int RSDURTGUtils_CopyDebugModeFromSharedMemory(LOG_CONTAINER* LogContainer);

#define TIMESTRING_SIZE 64


#endif // __RSDULOGS_H__INCLUDED__
