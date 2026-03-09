/* Gost.h */
/* описание основных функций и структур алгоритма ГОСТа */

#include "rsducryp.h"
#include <string.h>

/* случайное число */
#define RAND32 ((word32)rand() << 17 ^ (word32)rand() << 9 ^ rand())
/* константы для алгоритма гаммирования*/
#define C1 0x01010104
#define C2 0x01010101

/* таблица замен */
extern const unsigned char k8[16];
extern const unsigned char k7[16];
extern const unsigned char k6[16];
extern const unsigned char k5[16];
extern const unsigned char k4[16];
extern const unsigned char k3[16];
extern const unsigned char k2[16];
extern const unsigned char k1[16];

/* дополнительная таблица замен (для более быстрого алгоритма) */
extern unsigned char k87[256];
extern unsigned char k65[256];
extern unsigned char k43[256];
extern unsigned char k21[256];

/* функции */
int32_t f(word32 x); /* преобразование по таблице замен */
void kboxinit(void);
void gostcrypt(word32 const in[2], word32 out[2], word32 const key[8]);
void gostdecrypt(word32 const in[2], word32 out[2], word32 const key[8]);
void gostmac(word32 const *in, uint32_t len, word32 out[2], word32 const key[8]);
void gostofb(word32 *in, word32 *out, uint32_t len, word32 iv[2], word32 const key[8]);
