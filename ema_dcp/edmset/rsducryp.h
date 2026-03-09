
#pragma once

/************************************************************************
| RSDUCryp.h - библиотека защиты информации                            |         
| Яковлев А.В.                                                         |
| 20.11.2003г.,  версия 1.0                                            |
************************************************************************/
#ifndef __RSDUCRYP_H
#define __RSDUCRYP_H

#if (!defined(_RMX_C) && !defined(RMX))
#include <stdint.h>
#else
#include <rmxint.h>
#endif

/************* Типы данных **************/
#ifdef WIN32 /* ОС Windows*/
   #define RSDUCRYPTOTYPE
    //#ifdef RSDUCRYPTO_LIB
    //    #define RSDUCRYPTOTYPE __declspec(dllexport)
    //#else
    //    #define RSDUCRYPTOTYPE __declspec(dllimport)
    //#endif
    typedef uint32_t word32; /* 32 int */
#else        /* ОС iRMX */
    #define RSDUCRYPTOTYPE
    #define word32 uint32_t
#endif

/* Коды возвращаемых функциями значений */
#define RSDUCRYPTO_OK       0 /* Функция завершена успешно */
#define RSDUCRYPTO_ERROR    1 /* Возникла ошибка */

#define RSDUCRYPTO_KEY_LEN  32 /* Длина ключа */

#ifdef __cplusplus
extern "C"
{
#endif

/*** Основные функции библиотеки ***/
/* Функция получения сеансового ключа */
RSDUCRYPTOTYPE int32_t RSDUCrypto_GetSessionKey(
                                            void* pvSessionKey,  /* [out] указатель на ключ - блок данных 32 байта */
                                            int32_t lTime           /* [in] полная дата генерации ключа в секундах с 00:00:00 1 января 1970 г. (GMT) */
                                           );
/* Функция шифрования информации */
/* В случае шифровки текстовой информации не стоит забывать 
   про использование завершающего символа строки - нулевого байта.
   То есть дополнительно к шифруемой строке добавляется еще один  пустой байт
*/
RSDUCRYPTOTYPE int32_t RSDUCrypto_Encrypt(
                                      void* pvDataBeforeEncrypt, /* [in] указатель на шифруемый блок данных */
                                      void* pvDataAfterEncrypt,  /* [out] указатель на зашифрованный блок данных */
                                      uint32_t dwLength,    /* [in] длина (в байтах) блока данных */
                                      void* pvKey                /* [in] указатель на ключ, при помощи которого шифруются данные - блок данных 32 байта */
                                     );
/* Функция дешифрования информации */
RSDUCRYPTOTYPE int32_t RSDUCrypto_Decrypt(
                                      void* pvDataBeforeDecrypt, /* [in] указатель на зашифрованный блок данных */
                                      void* pvDataAfterDecrypt,  /* [out] указатель на расшифрованный блок данных */
                                      uint32_t dwLength,    /* [in] длина (в байтах) блока данных */
                                      void* pvKey                /* [in] указатель на ключ, при помощи которого дешифруются данные - блок данных 32 байта */
                                     );
/* Функция кодирования прав */
RSDUCRYPTOTYPE int32_t RSDUCrypto_Encoding(
                                       uint32_t* pdwLaws /* [in, out] указатель на права */
                                      );
/* Функция декодирования прав */
RSDUCRYPTOTYPE int32_t RSDUCrypto_Decoding(
                                       uint32_t* pdwLaws /* [in, out] указатель на права */
                                      );

/*** Вспомогательные функции библиотеки ***/
/* Функция получения текстового описания ошибки по ее коду */
RSDUCRYPTOTYPE char * RSDUCrypto_GetErrorMsg(
                                             int32_t nError     /* [in] код ошибки */
                                            );

#ifdef __cplusplus
}
#endif

#endif /* #ifndef __RSDUCRYP_H */


