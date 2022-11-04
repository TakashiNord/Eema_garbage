// tread.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <math.h>
#include <ctype.h>

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




/* Get a float from 4 bytes (Modbus) without any conversion (ABCD) */
float modbus_get_float_abcd(const uint16_t *src)
{
    float f;
    uint32_t i;
    uint8_t a, b, c, d;

    a = (src[0] >> 8) & 0xFF;
    b = (src[0] >> 0) & 0xFF;
    c = (src[1] >> 8) & 0xFF;
    d = (src[1] >> 0) & 0xFF;

    i = (a << 24) |
        (b << 16) |
        (c << 8) |
        (d << 0);
    memcpy(&f, &i, 4);

    return f;
}

/* Get a float from 4 bytes (Modbus) in inversed format (DCBA) */
float modbus_get_float_dcba(const uint16_t *src)
{
    float f;
    uint32_t i;
    uint8_t a, b, c, d;

    a = (src[0] >> 8) & 0xFF;
    b = (src[0] >> 0) & 0xFF;
    c = (src[1] >> 8) & 0xFF;
    d = (src[1] >> 0) & 0xFF;

    i = (d << 24) |
        (c << 16) |
        (b << 8) |
        (a << 0);
    memcpy(&f, &i, 4);

    return f;
}

/* Get a float from 4 bytes (Modbus) with swapped bytes (BADC) */
float modbus_get_float_badc(const uint16_t *src)
{
    float f;
    uint32_t i;
    uint8_t a, b, c, d;

    a = (src[0] >> 8) & 0xFF;
    b = (src[0] >> 0) & 0xFF;
    c = (src[1] >> 8) & 0xFF;
    d = (src[1] >> 0) & 0xFF;

    i = (b << 24) |
        (a << 16) |
        (d << 8) |
        (c << 0);
    memcpy(&f, &i, 4);

    return f;
}

/* Get a float from 4 bytes (Modbus) with swapped words (CDAB) */
float modbus_get_float_cdab(const uint16_t *src)
{
    float f;
    uint32_t i;
    uint8_t a, b, c, d;

    a = (src[0] >> 8) & 0xFF;
    b = (src[0] >> 0) & 0xFF;
    c = (src[1] >> 8) & 0xFF;
    d = (src[1] >> 0) & 0xFF;

    i = (c << 24) |
        (d << 16) |
        (a << 8) |
        (b << 0);
    memcpy(&f, &i, 4);

    return f;
}

/* DEPRECATED - Get a float from 4 bytes in sort of Modbus format */
float modbus_get_float(const uint16_t *src)
{
    float f;
    uint32_t i;

    i = (((uint32_t)src[1]) << 16) + src[0];
    memcpy(&f, &i, sizeof(float));

    return f;
}




typedef struct
{
    char value[4];
} BCD_Number;

BCD_Number  val ;

// source
char source2[] = "63430200080000000000000000002AB0";
char source1[] = "63430200044A3A0A0A0035000000000000170D5E630111D0F083F53950523983";
char source[] = "634302007B30EC9A44D8A1FE4500000000D0F1AA4180EBB341000016434079B9410000044200000442728182C07C3B5C44F052B3411D3975C3E045C741000000000000000082D37B" ;
// dest
char dest[256]="\0";


// проверка CRC
uint32_t CRCKM5(const char * src , uint32_t dstlen)
{
    uint32_t err = 0; // ошибка проверки CRC
    uint8_t u=0;
    uint8_t sum1 = 0 ;
    uint8_t sum2 = 0 ;
    uint8_t cnt = 0 ; // считаем байты

    while (src && sscanf(src, "%2x", &u) == 1)
    {
      cnt++;
      // последние 2 байта не считаем (72-2)=>cnt
      if ( (dstlen-2)>=cnt ) {
        sum1^=u;
        sum2+=u;
      } else {
        if ( (dstlen-1)==cnt ) if ((sum1-u)!=0) err=-1; // проверяем xor
        if ( dstlen==cnt ) if ((sum2-u)!=0) err=-2;  // проверяем 256
      }
      src += 2;
      //printf("%c %d | CRC sum1(XOR)=%d, sum2(256)=%d\n",u,u, sum1,sum2);
    }

    return err;
}



float value [ 20 ] ; // массив принимаемых значений? можно структуру

// разбор принимаемой строки
uint32_t KM5(const char * src, uint32_t dstlen)
{
  char hvalue[5];
  float f ;
  uint8_t a, b, c, d;
  uint8_t fun;
  int i ;
  uint16_t val[] = {0x2000, 0x47F1}; //Lo Hi


  src += 0; // сдвиг
  // header BCD - формат
  sscanf(src, "%02x%02x%02x%02x", &a,&b,&c,&d);
  sprintf(hvalue,"%02x%02x%02x%02x\0", d,c,b,a);
  // проверка, что Header=заданному
  // if () { }
  printf("header=%02x%02x%02x%02x | %s\n",a,b,c,d, hvalue);


  src += 8; // сдвиг
  // number function
  sscanf(src, "%2x", &fun);

  printf("function=%d\n",fun);


  // values
  switch(fun)
  {
       case 123:
        // читаем 16 элементов по 4 байта + 1 элемент=2 байта
        src += 2; // сдвиг
        for(i=0;i<16 && src && sscanf(src, "%02x%02x%02x%02x", &a,&b,&c,&d) == 4;i++, src += 8) {

            val[0] = a; val[0] = b ; val[1] = c; val[1] = d;
            f = modbus_get_float_dcba(val) ;
            value[i] = f ;

        }

        if (src && sscanf(src, "%02x", &f)==1) {
            value[16] = f ;
            src += 2; // сдвиг
        }

        printf("CRC=%s\n",src); // остались 2 байта CRC

       break;
       default:

       break;
  }


  return 0;
}


int main(int argc, char* argv[])
{
   int i ;

   // init arr
   for(i=0;i<20;i++) {
     value[i]=0.0;
   }

   // считаем длину полученной строки
   uint32_t len = strlen(source) ;
   // количество байтов (приблизительное)
   uint32_t lenb=int(len/2) ;
   printf("\n%s\nlength = %d\nbyte = %d\n",source,len, lenb);

   int crc=CRCKM5( source , lenb ) ;
   printf("\nFlag CRC = %d\n",crc);

   if (crc<0) printf("\nFlag CRC = %d - no correct\n",crc);

   // принимаем, что func=123 , длина принимаемой строки 72 байта=4+1+65+1+1 65=4*16+1
   KM5(&source[0],lenb);

   printf("\nValues:\n");
   for(i=0;i<20;i++) {
     printf("[%02d]=%f\n",i,value[i]);
   }

   printf("\n------\n");
   return 0;
}





