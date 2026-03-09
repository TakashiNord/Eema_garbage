/* RSDUCryp.c */
/* Функции библиотеки шифрования данных*/
#define RSDUCRYPTO_LIB
#define KEYSIZE_WORD32 (RSDUCRYPTO_KEY_LEN/sizeof(word32))

#include <rsducryp.h>
#include "gost.h"

/* Основные функции библиотеки */
/*****************************************************************/
/* Функция получения сеансового ключа */
RSDUCRYPTOTYPE int32_t RSDUCrypto_GetSessionKey(void* pvSessionKey, int32_t lTime)
{
    /************* Предопределенная ключевая информация **************/
    /* Рабочий ключ */
    const word32 WorkKey[KEYSIZE_WORD32] = {0x76391A31, 0xDCFBF748, 0x86E71C12, 0x946371BF, 0x4A1EA1FF, 0x24646DD6, 0xF96586DE, 0x6EDE89AB};
    /* Базовая часть для сеансового ключа (первые 7 байт) */
    word32 key[KEYSIZE_WORD32] = {0x41C951CB, 0xDDED618A, 0x78EA9B4B, 0x41D21EA7, 0x2FC1151E, 0xE7783135, 0xB8A17F5, 0x0};
    word32 iv[2]; /* синхропослыка - начальное заполнение гаммы */

    /* Конкатенация базовой части и переменной информации - времени */
    key[KEYSIZE_WORD32-1] = (word32)lTime;

    /* синхропосылка (по идее должна вырабатывться каждый раз новая, но сейчас просто зашита) */
    iv[0] = 0x946371BF;
    iv[1] = 0x4A1EA1FF;

    /* шифрования полученного ключа рабочим ключом */
    gostofb(key, key, (uint32_t)KEYSIZE_WORD32/2, iv, WorkKey);
    memcpy(pvSessionKey,(void *)key,(uint32_t)KEYSIZE_WORD32*sizeof(word32));

    return RSDUCRYPTO_OK;
}

/*****************************************************************/
/* Функция шифрования информации */
RSDUCRYPTOTYPE int32_t RSDUCrypto_Encrypt(void *pvDataBeforeEncrypt, void *pvDataAfterEncrypt, uint32_t dwLength, void* pvKey)
{
    uint32_t lengthDW,lengthEnd;
    word32 *in, *out, *key;
    word32 iv[2],               /* синхропослыка - начальное заполнение гаммы */
        in_end[2] = {0,0},   /* последний блок входных данных (для неполного 64-разрядного блока) */
        out_end[2] = {0,0};  /* последний блок выходных данных (для неполного 64-разрядного блока) */

    /* синхропосылка (по идее должна вырабатывться каждый раз новая, но сейчас просто зашита) */
    iv[0] = 0x946371BF;
    iv[1] = 0x4A1EA1FF;

    in = (word32 *)pvDataBeforeEncrypt;
    out = (word32 *)pvDataAfterEncrypt;
    key = (word32 *)pvKey;

    /* количество 64-разрядных блоков в массивах данных */
    lengthDW = dwLength/(2*sizeof(word32));
    lengthEnd = dwLength - 2*sizeof(word32)*lengthDW;
    memcpy(in_end,in+2*lengthDW,(uint32_t)lengthEnd);

    if (lengthDW > 0)
    {
        /*шифрование данных*/
        gostofb(in, out, lengthDW, iv, key);
    }

    /* шифрование последних (неполных) блоков данных */
    gostofb(in_end, out_end, (uint32_t)1, iv, key);
    memcpy(out+2*lengthDW,out_end,(uint32_t)lengthEnd);
    return RSDUCRYPTO_OK;
}

/*****************************************************************/
/* Функция дешифрования информации */
RSDUCRYPTOTYPE int32_t RSDUCrypto_Decrypt(void *pvDataBeforeDecrypt, void *pvDataAfterDecrypt, uint32_t dwLength, void* pvKey)
{
    uint32_t lengthDW,lengthEnd;
    word32 *in, *out, *key;
    word32 iv[2],               /* синхропослыка - начальное заполнение гаммы */
        in_end[2] = {0,0},   /* последний блок входных данных (для неполного 64-разрядного блока) */
        out_end[2] = {0,0};  /* последний блок выходных данных (для неполного 64-разрядного блока) */

    /* синхропосылка (по идее должна вырабатывться каждый раз новая, но сейчас просто зашита) */
    iv[0] = 0x946371BF;
    iv[1] = 0x4A1EA1FF;

    in = (word32 *)pvDataBeforeDecrypt;
    out = (word32 *)pvDataAfterDecrypt;
    key = (word32 *)pvKey;

    /* количество 64-разрядных блоков в массивах данных */
    lengthDW = dwLength/(2*sizeof(word32));
    lengthEnd = dwLength - 2*sizeof(word32)*lengthDW;
    memcpy(in_end,in+2*lengthDW,(uint32_t)lengthEnd);

    if (lengthDW > 0)
    {
        /* дешифрование данных */
        gostofb(in, out, lengthDW, iv, key);
    }

    /* дешифрование последних (неполных) блоков данных */
    gostofb(in_end, out_end, (uint32_t)1, iv, key);
    memcpy(out+2*lengthDW,out_end,(uint32_t)lengthEnd);

    return RSDUCRYPTO_OK;
}

/*****************************************************************/
/* Функция кодирования прав */
RSDUCRYPTOTYPE int32_t RSDUCrypto_Encoding(uint32_t* pdwLaws)
{
    const uint32_t skr = 0xE5CB972E; /* посчитанная кодирующая последовательность (скремблер 011b, начальный ключ - 111b) 11100101110010111001011100101110b */

    *pdwLaws ^= skr;

    return RSDUCRYPTO_OK;
}

/*****************************************************************/
/* Функция кодирования прав */
RSDUCRYPTOTYPE int32_t RSDUCrypto_Decoding(uint32_t* pdwLaws)
{
    const uint32_t skr = 0xE5CB972E; /* посчитанная кодирующая последовательность (скремблер 011b, начальный ключ - 111b) 11100101110010111001011100101110b */

    *pdwLaws ^= skr;

    return RSDUCRYPTO_OK;
}

/* Вспомогательные функции библиотеки */
/*****************************************************************/
/* Функция получения текстового описания ошибки по ее коду */
RSDUCRYPTOTYPE char * RSDUCrypto_GetErrorMsg(int32_t nError)
{
    switch(nError)
    {
    case RSDUCRYPTO_OK:
        return (char *)"No errors";
        break;
    case RSDUCRYPTO_ERROR:
        return (char *)"Error!";
        break;
    default:
        return (char *)"Unknow error!";
    }
}
