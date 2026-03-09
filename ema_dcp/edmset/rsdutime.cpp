
//#include <winsock.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <time.h>

//#include <winsock2.h>
#include <windows.h>
#include <time.h>

#include "winint.h"
#include "rsdutime.h"




int32_t gettimeofday (struct timeval *tv, struct timezone *tz);

#define DELTA_EPCH_IN_USEC 11644473600000000ULL
LONGLONG filetime_to_unix_epoch (const FILETIME *ft)
{
    LONGLONG res = (LONGLONG) ft->dwHighDateTime << 32;
    res |= ft->dwLowDateTime;
    res /= 10; /* from 100 nano-sec periods to usec */
    res -= DELTA_EPCH_IN_USEC; /* from Win epoch to Unix epoch */
    return (res);
}

int32_t gettimeofday (struct timeval *tv, struct timezone *tz)
{
    FILETIME ft;
    LONGLONG tim;

    if (!tv) return (-1);
    GetSystemTimeAsFileTime (&ft);
    tim = filetime_to_unix_epoch (&ft);
    tv->tv_sec = (int32_t) (tim / 1000000L);
    tv->tv_usec = (int32_t) (tim % 1000000L);
    return (0);
}

/**
* @brief Converted timeval struct to RSDU_timeval struct
*
* @param tv[in] Time value in system struct 'timeval'
* @param rsdutv[out] Time value in the struct for transmite.
*
* @returns 0 on successful
*          Negative in error case.
**/
int timetv2rsdutv(struct timeval &tv, struct RSDU_timeval &rsdutv)
{
    rsdutv.tv_sec = (int32_t)tv.tv_sec;
    rsdutv.tv_usec = (int32_t)tv.tv_usec;
    return 0;
}

/**
* @brief Converted RSDU_timeval struct to timeval struct
*
* @param rsdutv[in] Time value in the struct for transmite.
* @param tv[out] Time value in system struct 'timeval'
*
* @returns 0 on successful
*          Negative in error case.
**/
int rsdutv2timetv(struct RSDU_timeval &rsdutv, struct timeval &tv)
{
    tv.tv_sec = rsdutv.tv_sec;
    tv.tv_usec = rsdutv.tv_usec;
    return 0;
}

///**
// * @brief Converted timeval struct to RSDU_timeval struct
// *
// * @param tv[in] Time value in system struct 'timeval'
// *
// * @returns Time value in the struct 'RSDU_timeval' for transmite
// **/
//RSDU_timeval timetv2rsdutv(struct timeval tv)
//{
//    struct RSDU_timeval rsdutv;
//    rsdutv.tv_sec = (int32_t)tv.tv_sec;
//    rsdutv.tv_usec = (int32_t)tv.tv_usec;
//    return rsdutv;
//}

///**
// * @brief Converted RSDU_timeval struct to timeval struct
// *
// * @param rsdutv[in] Time value in the struct for transmite.
// *
// * @returns Time value in system struct 'timeval'
// **/
//timeval rsdutv2timetv(struct RSDU_timeval rsdutv)
//{
//    struct timeval tv;
//    tv.tv_sec = rsdutv.tv_sec;
//    tv.tv_usec = rsdutv.tv_usec;
//    return tv;
//}


time_t RSDURTGUtils_Time70(void)
{
    //uint32_t t;
    //t = time(0);
    return(time(0));
}

struct timeval RSDURTGUtils_TimeTick(void)
{
    struct timeval tv;

    /* System timer value */
    gettimeofday(&tv, 0);
    return(tv);
}

RSDU_timestamp RSDURTGUtils_TimeStamp(void)
{
    struct timeval tv;
    RSDU_timestamp ts;

    /* System timer value */
    gettimeofday(&tv, 0);
    
    ts.tv_sec = tv.tv_sec;
    ts.tv_usec = tv.tv_usec;
    /* TODO: реализовать механизм контроля синхронизации времени на хосте, тогда раскомментировать:
    if (RSDURTGUtils_GetTimeSyncStatus())
        ts.status = kTimestampOk;
    else
        ts.status = kTimestampNoSource; // Источник точного времени отсутствует
    */
    // Необходимо продумать механизм контроля синхронизации времени на хосте, в том числе по протоколам сбора/передачи (МЭК101/104).
    // А пока что считаем время на хосте всегда достоверным.
    ts.status = kTimestampOk;

    return(ts);
}

// Конвертирование структуры времени в форматированную строку с переводом на следующую строку
char * ctick(struct timeval *tv)
{
    struct tm tms;
    static char time_str[CTICK_BUFFER_SIZE];

    localtime_s(&tms, (time_t*)&(*tv).tv_sec);

    sprintf (time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d\n", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
        tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)(tv->tv_usec/1000));
    return(time_str);
}

/**
* @brief This function output RSDU_timeval struct into the formated string with line feed on end string.
*
* @param rsdutv[in] Time value for output to string
*
* @return Pointer to local static variable, contained time in string format
**/
char * ctick(struct RSDU_timeval *rsdutv)
{
    struct tm tms;
    static char time_str[CTICK_BUFFER_SIZE];
    time_t rsdutime = (*rsdutv).tv_sec;


    localtime_s(&tms, &rsdutime);


    sprintf (time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d\n", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
        tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsdutv).tv_usec/1000));
    return(time_str);
}

/**
* @brief This function output RSDU_timestamp struct into the formated string with line feed on end string.
*
* @param rsduts[in] Time value for output to string
*
* @return Pointer to local static variable, contained time in string format
**/
char * ctick(struct RSDU_timestamp *rsduts)
{
    struct tm tms;
    static char time_str[CTICK_BUFFER_SIZE];
    time_t rsdutime = (*rsduts).tv_sec;


    localtime_s(&tms, &rsdutime);


    sprintf(time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d (%1d)\n", tms.tm_mday, tms.tm_mon + 1, tms.tm_year + 1900,
        tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsduts).tv_usec / 1000), ((*rsduts).status % 10));
    return(time_str);
}

// Конвертирование структуры времени в форматированную строку с переводом на следующую строку
// Буфер time_str не менее 32 символов!
char * ctick_r(struct timeval *tv, char *time_str)
{
    struct tm tms;
    time_t timetv = (*tv).tv_sec;

    if (time_str != NULL)
    {

        __int64 time = (*tv).tv_sec;
        if (localtime_s(&tms, &time) == 0)
        {
            sprintf (time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d\n", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
                tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*tv).tv_usec/1000));
        }

    }
    return(time_str);
}

/**
* @brief This function outputs RSDU_timeval struct into the formated string
*        with line feed on end string. String for output pointed in the
*        'time_str' parameter.
*
* @param rsdutv[in] Time value for output to string
* @param time_str[out] String for output time value in it.
*
* @return On successful - Pointer to time_str
**/
char * ctick_r(struct RSDU_timeval *rsdutv, char *time_str)
{
    struct tm tms;
    time_t rsdutime = (*rsdutv).tv_sec;

    if (time_str != NULL)
    {

        if (localtime_s(&tms, &rsdutime) == 0)
        {
            sprintf(time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d\n", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
                tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsdutv).tv_usec/1000));
        }

    }
    return(time_str);
}

/**
* @brief This function outputs RSDU_timestamp struct into the formated string
*        with line feed on end string. String for output pointed in the
*        'time_str' parameter.
*
* @param rsdutv[in] Time value for output to string
* @param time_str[out] String for output time value in it.
*
* @return On successful - Pointer to time_str
**/
char * ctick_r(struct RSDU_timestamp *rsduts, char *time_str)
{
    struct tm tms;
    time_t rsdutime = (*rsduts).tv_sec;

    if (time_str != NULL)
    {

        if (localtime_s(&tms, &rsdutime) == 0)
        {
            sprintf(time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d (%1d)\n", tms.tm_mday, tms.tm_mon + 1, tms.tm_year + 1900,
                tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsduts).tv_usec / 1000), ((*rsduts).status % 10));
        }

    }
    return(time_str);
}

// Конвертирование структуры времени в форматированную строку без перевода на следующую строку
char* ctick_line(struct timeval *tv)
{
    struct tm tms;
    static char time_str[CTICK_BUFFER_SIZE];

    localtime_s(&tms, (time_t*)&(*tv).tv_sec);

    sprintf(time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
        tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*tv).tv_usec/1000));
    return(time_str);
}

/**
* @brief This function output RSDU_timeval struct into the formated string without line feed.
*
* @param rsdutv[in] Time value for output to string
*
* @return Pointer to local static variable, contained time in string format
**/
char * ctick_line(struct RSDU_timeval *rsdutv)
{
    struct tm tms;
    static char time_str[CTICK_BUFFER_SIZE];
    time_t rsdutime = (*rsdutv).tv_sec;

    localtime_s(&tms, &rsdutime);

    sprintf (time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
        tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsdutv).tv_usec/1000));
    return(time_str);
}

/**
* @brief This function output RSDU_timestamp struct into the formated string without line feed.
*
* @param rsdutv[in] Time value for output to string
*
* @return Pointer to local static variable, contained time in string format
**/
char * ctick_line(struct RSDU_timestamp *rsduts)
{
    struct tm tms;
    static char time_str[CTICK_BUFFER_SIZE];
    time_t rsdutime = (*rsduts).tv_sec;

    localtime_s(&tms, &rsdutime);

    sprintf(time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d (%1d)", tms.tm_mday, tms.tm_mon + 1, tms.tm_year + 1900,
        tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsduts).tv_usec / 1000), ((*rsduts).status % 10));
    return(time_str);
}

// Конвертирование структуры времени в форматированную строку без перевода на следующую строку
// Буфер time_str не менее 32 символов!
char* ctick_line_r(struct timeval *tv, char *time_str)
{
    struct tm *ptms = NULL, tms;
    if (time_str != NULL)
    {

        __int64 time = (*tv).tv_sec;
        if (localtime_s(&tms, &time) == 0)
        {
            sprintf (time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d\n", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
                tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*tv).tv_usec/1000));
        }

    }
    return(time_str);
}

/**
* @brief This function outputs RSDU_timeval struct into the formated string
*        without line feed on end string. String for output pointed in the
*        'time_str' parameter.
*
* @param rsdutv[in] Time value for output to string
* @param time_str[out] String for output time value in it.
*
* @return On successful - Pointer to time_str
**/
char * ctick_line_r(struct RSDU_timeval *rsdutv, char *time_str)
{
    struct tm tms;
    time_t rsdutime = (*rsdutv).tv_sec;

    if (time_str != NULL)
    {

        if (localtime_s(&tms, &rsdutime) == 0)
        {
            sprintf (time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d", tms.tm_mday, tms.tm_mon+1, tms.tm_year+1900,
                tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsdutv).tv_usec/1000));
        }

    }
    return(time_str);
}

/**
* @brief This function outputs RSDU_timestamp struct into the formated string
*        without line feed on end string. String for output pointed in the
*        'time_str' parameter.
*
* @param rsdutv[in] Time value for output to string
* @param time_str[out] String for output time value in it.
*
* @return On successful - Pointer to time_str
**/
char * ctick_line_r(struct RSDU_timestamp *rsduts, char *time_str)
{
    struct tm tms;
    time_t rsdutime = (*rsduts).tv_sec;

    if (time_str != NULL)
    {

        if (localtime_s(&tms, &rsdutime) == 0)
        {
            sprintf(time_str, "%02d.%02d.%d %02d:%02d:%02d.%03d (%1d)", tms.tm_mday, tms.tm_mon + 1, tms.tm_year + 1900,
                tms.tm_hour, tms.tm_min, tms.tm_sec, (int32_t)((*rsduts).tv_usec / 1000), ((*rsduts).status % 10));
        }

    }
    return(time_str);
}


void RSDURTGUtils_SLocaltime(time_t *utc, struct tm *ltm)
{

    __int64 time = *utc;
    localtime_s(ltm, &time);

    return;
}

/*    Сравнение двух меток времени, tv1 > tv2    */
int32_t RSDURTGUtils_TickCompareG(struct timeval *tv1, struct timeval *tv2)
{
    if ((*tv1).tv_sec > (*tv2).tv_sec
        || ((*tv1).tv_sec == (*tv2).tv_sec && (*tv1).tv_usec > (*tv2).tv_usec))
        return(1);
    return(0);
}

/**
* @brief Compare two time stamp (rsdutv1 > rsdutv2)
*
* @param rsdutv1[in] Time stamp for comparison
* @param rsdutv2[in] Time stamp for comparison
*
* @return 1 -- in case rsdutv1 > rsdutv2
*         0 -- in case rsdutv1 <= rsdutv2
**/
int32_t RSDURTGUtils_TickCompareG(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2)
{
    if (rsdutv1.tv_sec > rsdutv2.tv_sec
        || (rsdutv1.tv_sec == rsdutv2.tv_sec && rsdutv1.tv_usec > rsdutv2.tv_usec))
        return(1);
    return(0);
}

/**
* @brief Compare two time stamp (rsdutv1 > rsdutv2)
*
* @param rsdutv1[in] Time stamp for comparison
* @param rsdutv2[in] Time stamp for comparison
*
* @return 1 -- in case rsdutv1 > rsdutv2
*         0 -- in case rsdutv1 <= rsdutv2
**/
int32_t RSDURTGUtils_TickCompareG(const RSDU_timestamp &rsdutv1, const RSDU_timestamp &rsdutv2)
{
    if (rsdutv1.tv_sec > rsdutv2.tv_sec
        || (rsdutv1.tv_sec == rsdutv2.tv_sec && rsdutv1.tv_usec > rsdutv2.tv_usec))
        return(1);
    return(0);
}


/*    Сравнение двух меток времени, tv1 == tv2    */
int32_t RSDURTGUtils_TickCompareE(struct timeval *tv1, struct timeval *tv2)
{
    if ((*tv1).tv_sec == (*tv2).tv_sec && (*tv1).tv_usec == (*tv2).tv_usec)
        return(1);
    return(0);
}

/**
* @brief Compare two time stamp (rsdutv1 == rsdutv2)
*
* @param rsdutv1[in] Time stamp for comparison
* @param rsdutv2[in] Time stamp for comparison
*
* @return 1 -- in case rsdutv1 == rsdutv2
*         0 -- in case rsdutv1 <> rsdutv2
**/
int32_t RSDURTGUtils_TickCompareE(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2)
{
    if (rsdutv1.tv_sec == rsdutv2.tv_sec && rsdutv1.tv_usec == rsdutv2.tv_usec)
        return(1);
    return(0);
}

/**
* @brief Compare two time stamp (rsdutv1 == rsdutv2)
*
* @param rsdutv1[in] Time stamp for comparison
* @param rsdutv2[in] Time stamp for comparison
*
* @return 1 -- in case rsdutv1 == rsdutv2
*         0 -- in case rsdutv1 <> rsdutv2
**/
int32_t RSDURTGUtils_TickCompareE(const RSDU_timestamp &rsdutv1, const RSDU_timestamp &rsdutv2)
{
    if (rsdutv1.tv_sec == rsdutv2.tv_sec && rsdutv1.tv_usec == rsdutv2.tv_usec)
        return(1);
    return(0);
}

/**
 * @brief Compare two time stamp (tv1 == tv2)
 *
 * @param tv1[in] Time stamp for comparison
 * @param tv2[in] Time stamp for comparison
 *
 * @return 1 -- in case tv1 > tv2
 *         0 -- in case tv1 = tv2
 *         -1 -- in case tv1 < tv2
 **/
int32_t RSDURTGUtils_TickCompare(struct timeval *tv1, struct timeval *tv2)
{
    if (RSDURTGUtils_TickCompareE(tv1, tv2)) return(0);
    else if (RSDURTGUtils_TickCompareG(tv1, tv2)) return(1);
    return(-1);
}

/**
* @brief Compare two time stamp
*
* @param rsdutv1[in] Time stamp for comparison
* @param rsdutv2[in] Time stamp for comparison
*
* @return 1 -- in case rsdutv1 > rsdutv2
*         0 -- in case rsdutv1 == rsdutv2
*         -1 -- in case rsdutv1 < rsdutv2
**/
int32_t RSDURTGUtils_TickCompare(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2)
{
    if (RSDURTGUtils_TickCompareE(rsdutv1, rsdutv2)) return(0);
    else if (RSDURTGUtils_TickCompareG(rsdutv1, rsdutv2)) return(1);
    return(-1);
}

/**
* @brief Compare two time stamp
*
* @param rsdutv1[in] Time stamp for comparison
* @param rsdutv2[in] Time stamp for comparison
*
* @return 1 -- in case rsdutv1 > rsdutv2
*         0 -- in case rsdutv1 == rsdutv2
*         -1 -- in case rsdutv1 < rsdutv2
**/
int32_t RSDURTGUtils_TickCompare(const RSDU_timestamp &rsdutv1, const RSDU_timestamp &rsdutv2)
{
    if (RSDURTGUtils_TickCompareE(rsdutv1, rsdutv2)) return(0);
    else if (RSDURTGUtils_TickCompareG(rsdutv1, rsdutv2)) return(1);
    return(-1);
}

/*    Сравнивает и возвращает старшую метоку времени    */
struct timeval RSDURTGUtils_GetOlderTick(struct timeval *tv1, struct timeval *tv2)
{
    if (RSDURTGUtils_TickCompareG(tv1, tv2)) return(*tv1);
    return(*tv2);
}

/**
* @brief Compare two time stamp and return maximum of them
*
* @param rsdutv1[in] Time stamp for comparison
* @param rsdutv2[in] Time stamp for comparison
*
* @return maximum from rsdutv1 and rsdutv2
**/
struct RSDU_timeval RSDURTGUtils_GetOlderTick(const struct RSDU_timeval &rsdutv1, const struct RSDU_timeval &rsdutv2)
{
    if (RSDURTGUtils_TickCompareG(rsdutv1, rsdutv2)) return(rsdutv1);
    return(rsdutv2);
}

/**
* @brief Compare two time stamp and return maximum of them
*
* @param rsduts1[in] Time stamp for comparison
* @param rsduts1[in] Time stamp for comparison
*
* @return maximum from rsduts1 and rsduts1
**/
RSDU_timestamp RSDURTGUtils_GetOlderTick(const RSDU_timestamp &rsdutv1, const RSDU_timestamp &rsdutv2)
{
    if (RSDURTGUtils_TickCompareG(rsdutv1, rsdutv2)) return(rsdutv1);
    return(rsdutv2);
}

/*    Возвращает разницу в десятках мс. меток времени tv1 - tv2    */
int32_t RSDURTGUtils_GetTickDifference(struct timeval *tv1, struct timeval *tv2)
{
    int32_t ct_diff;
    int32_t ms_tv1 = (int32_t)(*tv1).tv_usec/10000;
    int32_t ms_tv2 = (int32_t)(*tv2).tv_usec/10000;

    ct_diff = ((*tv1).tv_sec - (*tv2).tv_sec)*100;
    if (ms_tv2 > ms_tv1)
        ct_diff = ct_diff - ms_tv2 + ms_tv1;
    else
        ct_diff = ct_diff + ms_tv1 - ms_tv2;

    return(ct_diff);
}

int32_t RSDURTGUtils_SetTime70(time_t *utc)
{
    int result = 0;


    return result;

}

