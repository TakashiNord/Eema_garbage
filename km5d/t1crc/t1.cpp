// t1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

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


// source
char source2[] = "63430200080000000000000000002AB0";
char source[] = "63430200044A3A0A0A0035000000000000170D5E630111D0F083F53950523983";
char source1[] = "634302007B30EC9A44D8A1FE4500000000D0F1AA4180EBB341000016434079B9410000044200000442728182C07C3B5C44F052B3411D3975C3E045C741000000000000000082D37B" ;
// dest
char dest[256]="\0";


int hex_to_int(char c)
{
    if (c >= 97)
        c = c - 32;
    int first = c / 16 - 3;
    int second = c % 16;
    int result = first * 10 + second;
    if (result > 9) result--;
    return result;
}

int hex_to_ascii(char c, char d){
    int high = hex_to_int(c) * 16;
    int low = hex_to_int(d);
    return high+low;
}

long hexToAscii(char first, char second)
{
   char hex[5], *stop;
   hex[0] = '0';
   hex[1] = 'x';
   hex[2] = first;
   hex[3] = second;
   hex[4] = 0;
   return strtol(hex, &stop, 16);
}

// Crc for KM5
int CrcKM5(char * pSrc, int length) {
  uint8_t sum1=0;
  uint8_t sum2=0;
  uint8_t d ;

  uint8_t buf = 0;
  int len = strlen(pSrc);

  // Two hex digits are combined into one byte
  for(int i = 0; i < length; i++) {

    if(i % 2 != 0){
      d = hex_to_ascii(buf, pSrc[i]) ;
      sum1 ^=d; // XOR
      sum2 +=d; // mod 256
      printf("%c %d / CRC sum1(XOR)=%d, sum2(256)=%d\n",d,d,sum1,sum2);
    }else{
    buf = pSrc[i];
      }

  }

  return 0;  // normal exit
}


// line modbus
unsigned char Lrc(unsigned char * pSrc, int length) {
  unsigned char locLrc=0;
  for(int i=0;i<length;i++)
    locLrc += *(pSrc+i);
    return locLrc = ~locLrc + 1;
}


int main(int argc, char* argv[])
{
  printf("Start!\n\n");

  int l1 = strlen(source);
  printf("Len=%d\n",l1);
  printf("Len=%s\n",source);

  printf("\n");

  printf("\nCRC\n");
  // длина провер€ймой строки должна быть на 2 байта меньше
  int l3 = strlen(source); // - 4 ;
  CrcKM5(source, l3);

  printf("\n");
  printf("\n\nEnd!\n");
  return 0;
}

