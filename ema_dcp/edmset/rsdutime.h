#pragma once

#ifndef __RSDUTIME_H
#define __RSDUTIME_H

#include "winint.h"
#include "utilsapi.h"

#define far

#include "stnoalg.h" /* WARNING! ALL INCLUDES MUST BE PLACED BEFORE THIS INCLUDE!!!*/

#define CTIME_BUFFER_SIZE 32
#define CTICK_BUFFER_SIZE 64

/**
* @brief New timeval struct with fixed size of fields for x86 and x86_64 systems
**/
typedef struct RSDU_timeval
{
    int32_t tv_sec;  /* < seconds */
    int32_t tv_usec; /* < microseconds */
} RSDU_timeval;

#if ((defined(WIN32) && !defined(_WINSOCKAPI_)) || (!defined(WIN32) && !defined(_SYS_TIME_H)) && !defined(LINUX))
struct timeval
{
    int32_t tv_sec;  /* < seconds */
    int32_t tv_usec; /* < microseconds */
};
#endif

/**
* @brief Статусы меток времени 
**/
typedef enum TimestampStatus
{
    kTimestampOk = 0,       /* OK *//* = OIC_DST_OK */
    kTimestampNoCorrect = 1,/* Недостоверная метка *//* = OIC_DST_VALUENOCORRECT */
    kTimestampNoSource = 2  /* Отсутствует источник точного времени *//* = OIC_DST_NOPRESENT */
} TimestampStatus;

/**
* @brief New timeval struct with fixed size of fields for x86 and x86_64 systems and status of timestamp
**/
typedef struct RSDU_timestamp
{
    int32_t tv_sec;  /* < seconds */
    int32_t tv_usec; /* < microseconds */
    int32_t status;  /* < status */
    RSDU_timestamp() : tv_sec(0), tv_usec(0), status(kTimestampOk) {}
    RSDU_timestamp(const RSDU_timeval & rsdutv) : tv_sec(rsdutv.tv_sec), tv_usec(rsdutv.tv_usec), status(kTimestampOk) {}
    RSDU_timestamp(const struct timeval & tv) : tv_sec(tv.tv_sec), tv_usec(tv.tv_usec), status(kTimestampOk) {}
} RSDU_timestamp;


inline int timetv2rsdutv(struct timeval &tv, struct RSDU_timeval &rsdutv); /* Converted timeval struct to RSDU_timeval struct */
inline int rsdutv2timetv(struct RSDU_timeval &rsdutv, struct timeval &tv); /* Converted RSDU_timeval struct to timeval struct */

/**
* @brief Converted timeval struct to RSDU_timeval struct
*
* @param tv[in] Time value in system struct 'timeval'
*
* @returns Time value in the struct 'RSDU_timeval' for transmite
**/
inline RSDU_timeval timetv2rsdutv(struct timeval tv)
{
    struct RSDU_timeval rsdutv;
    rsdutv.tv_sec = (int32_t)tv.tv_sec;
    rsdutv.tv_usec = (int32_t)tv.tv_usec;
    return rsdutv;
}

/**
* @brief Converted RSDU_timeval struct to timeval struct
*
* @param rsdutv[in] Time value in the struct for transmite.
*
* @returns Time value in system struct 'timeval'
**/
inline timeval rsdutv2timetv(struct RSDU_timeval rsdutv)
{
    struct timeval tv;
    tv.tv_sec = rsdutv.tv_sec;
    tv.tv_usec = rsdutv.tv_usec;
    return tv;
}

/**
* @brief Converted timeval struct to RSDU_timestamp struct
*
* @param tv[in] Time value in system struct 'timeval'
*
* @returns Time value in the struct 'RSDU_timestamp' for transmite
**/
inline RSDU_timestamp timetv2rsduts(struct timeval tv)
{
    struct RSDU_timestamp rsduts;
    rsduts.tv_sec = (int32_t)tv.tv_sec;
    rsduts.tv_usec = (int32_t)tv.tv_usec;
    rsduts.status = kTimestampOk;
    return rsduts;
}

/**
* @brief Converted RSDU_timestamp struct to timeval struct
*
* @param rsduts[in] Time value in the struct for transmite.
*
* @returns Time value in system struct 'timeval'
**/
inline timeval rsduts2timetv(struct RSDU_timestamp rsduts)
{
    struct timeval tv;
    tv.tv_sec = rsduts.tv_sec;
    tv.tv_usec = rsduts.tv_usec;
    return tv;
}

/**
* @brief Converted RSDU_timeval struct to RSDU_timestamp struct
*
* @param tv[in] Time value in system struct 'timeval'
*
* @returns Time value in the struct 'RSDU_timestamp' for transmite
**/
inline RSDU_timestamp rsdutv2rsduts(struct RSDU_timeval rsdutv)
{
    struct RSDU_timestamp rsduts;
    rsduts.tv_sec = rsdutv.tv_sec;
    rsduts.tv_usec = rsdutv.tv_usec;
    rsduts.status = kTimestampOk;
    return rsduts;
}

/**
* @brief Converted RSDU_timestamp struct to RSDU_timeval struct
*
* @param rsduts[in] Time value in the struct for transmite.
*
* @returns Time value in system struct 'timeval'
**/
inline struct RSDU_timeval rsduts2rsdutv(struct RSDU_timestamp rsduts)
{
    struct RSDU_timeval rsdutv;
    rsdutv.tv_sec = rsduts.tv_sec;
    rsdutv.tv_usec = rsduts.tv_usec;
    return rsdutv;
}

/* Конвертирование структуры времени в строку с переводом на новую строку */
char* ctick(struct timeval *tv);
char* ctick_r(struct timeval *tv, char *time_str);
char* ctick(struct RSDU_timeval *tv);
char* ctick_r(struct RSDU_timeval *tv, char *time_str);
char* ctick(struct RSDU_timestamp *rsduts);
char* ctick_r(struct RSDU_timestamp *rsduts, char *time_str);

/* Конвертирование структуры времени в строку без перевода на новую строку */
char* ctick_line(struct timeval *tv);
char* ctick_line_r(struct timeval *tv, char *time_str);
char* ctick_line(struct RSDU_timeval *tv);
char* ctick_line_r(struct RSDU_timeval *tv, char *time_str);
char* ctick_line(struct RSDU_timestamp *rsduts);
char* ctick_line_r(struct RSDU_timestamp *rsduts, char *time_str);

int32_t RSDURTGUtils_TickCompareG(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2);
int32_t RSDURTGUtils_TickCompareE(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2);
int32_t RSDURTGUtils_TickCompare(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2);
struct RSDU_timeval RSDURTGUtils_GetOlderTick(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2);
int32_t RSDURTGUtils_TickCompareG(const RSDU_timestamp &rsdutv1, const struct RSDU_timestamp &rsdutv2);
int32_t RSDURTGUtils_TickCompareE(const RSDU_timestamp &rsdutv1, const struct RSDU_timestamp &rsdutv2);
int32_t RSDURTGUtils_TickCompare(const RSDU_timestamp &rsdutv1, const struct RSDU_timestamp &rsdutv2);
RSDU_timestamp RSDURTGUtils_GetOlderTick(const RSDU_timestamp &rsdutv1, const RSDU_timestamp &rsdutv2);

//#define RSDU_timeval timeval


#define DIFF_78_70  (uint32_t)252460800

/* Получение текущего времени */
RSDUUTILS_API time_t far RSDURTGUtils_Time70(void);
struct timeval far RSDURTGUtils_TimeTick(void);
RSDU_timestamp RSDURTGUtils_TimeStamp(void);


unsigned long far RSDURTGUtils_Time78(void);

time_t RSDURTGUtils_Mktime70 (struct tm *tim_p);
time_t RSDURTGUtils_SMktime  (struct tm *tp);

void RSDURTGUtils_SLocaltime(time_t *utc, struct tm *ltm);

/*    Сравнение двух меток времени, tv1 > tv2    */
int32_t RSDURTGUtils_TickCompareG(struct timeval *tv1, struct timeval *tv2);

/*    Сравнение двух меток времени, tv1 == tv2    */
int32_t RSDURTGUtils_TickCompareE(struct timeval *tv1, struct timeval *tv2);

/*    Сравнение двух меток времени    */
int32_t RSDURTGUtils_TickCompare(struct timeval *tv1, struct timeval *tv2);

/*    Сравнивает и возвращает старшую метоку времени    */
struct timeval RSDURTGUtils_GetOlderTick(struct timeval *tv1, struct timeval *tv2);

/*    Возвращает разницу в десятках мс. меток времени tv1 - tv2    */
int32_t RSDURTGUtils_GetTickDifference(struct timeval *tv1, struct timeval *tv2);

/*    Устанавливает системное время на хосте    */
int32_t RSDURTGUtils_SetTime70(time_t *utc);

struct timeval RSDURTGUtils_MonotonicTimeTick();
time_t RSDURTGUtils_MonotonicTime(void);

#include "stalgrst.h"

#endif
