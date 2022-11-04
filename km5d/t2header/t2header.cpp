// t2header.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <math.h>

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


typedef struct
{
    char value[4];
} BCD_Number;

BCD_Number  val ;

// source
char source2[] = "63430200080000000000000000002AB0";
char source[] = "63430200044A3A0A0A0035000000000000170D5E630111D0F083F53950523983";
char source1[] = "634302007B30EC9A44D8A1FE4500000000D0F1AA4180EBB341000016434079B9410000044200000442728182C07C3B5C44F052B3411D3975C3E045C741000000000000000082D37B" ;
// dest
char dest[256]="\0";


uint32_t get_dcba(const uint16_t *src)
{
    float f;
    uint32_t i;
    uint8_t a, b, c, d;

    a = (src[0] >> 8) & 0xFF;
    b = (src[0] >> 0) & 0xFF;
    c = (src[1] >> 8) & 0xFF;
    d = (src[1] >> 0) & 0xFF;

  //printf("a=%d b=%d c=%d d=%d\n",a,b,c,d);
  //printf("d=%02x c=%02x b=%02x a=%02x\n",d,c,b,a);
  sprintf(val.value,"%02x%02x%02x%02x\n",d,c,b,a);

    i = (d << 24) |
        (c << 16) |
        (b << 8)  |
        (a << 0);
    memcpy(&f, &i, 4);

    return i;
}


int main(int argc, char* argv[])
{
  printf("Start!\n\n");

    // длина строки 4 байта = 8 символов в строке

  uint16_t bcd[] = {0x6343, 0x0200}; //Lo Hi

    printf("bcd(little)=%04x%04x\n",bcd[0],bcd[1]);
  uint32_t r1=get_dcba(bcd) ;
  printf("real=%d\n",r1);
  printf("number=%s\n",val.value);

  printf("\n");

    //BCD_Number bcd_result;
    //bcd_result = bin2bcd(value);
    /* Assuming an int is 4 bytes */
    //printf("result=0x%08x\n", *((int *)bcd_result.value));

  printf("\n\nEnd!\n");
  return 0;
}


//=============================================================================

uint32_t Convert(uint32_t value, const uint32_t base1, const uint32_t base2)
{
    uint32_t result = 0;
    for (int i = 0; value > 0; i++)
    {
        result += value % base1 * pow(base2, i);
        value /= base1;
    }
    return result;
}

uint32_t FromBCD(uint32_t value)
{
    return Convert(value, 16, 10);
}

uint32_t ToBCD(uint32_t value)
{
    return Convert(value, 10, 16);
}

//=============================================================================

//typedef struct
//{
//    char value[4];
//} BCD_Number;

BCD_Number bin2bcd(int bin_number)
{
    BCD_Number bcd_number;

    for(int i = 0; i < sizeof(bcd_number.value); i++)
    {
        bcd_number.value[i] = bin_number % 10;
        bin_number /= 10;
        bcd_number.value[i] |= bin_number % 10 << 4;
        bin_number /= 10;
    }

    return bcd_number;
}


//=============================================================================

uint64_t uint32_to_bcd(uint32_t usi) {

    uint64_t shift = 16;
    uint64_t result = (usi % 10);

    while (usi = (usi/10))  {
        result += (usi % 10) * shift;
        shift *= 16; // weirdly, it's not possible to left shift more than 32 bits
    }
    return result;
}
//
uint32_t bcd_to_ui32(uint64_t bcd)  {

    uint64_t mask = 0x000f;
    uint64_t pwr = 1;

    uint64_t i = (bcd & mask);
    while (bcd = (bcd >> 4))    {
        pwr *= 10;
        i += (bcd & mask) * pwr;
    }
    return (uint32_t)i;
}



//=============================================================================






//=============================================================================