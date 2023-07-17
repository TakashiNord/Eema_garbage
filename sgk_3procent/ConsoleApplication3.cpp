// ConsoleApplication3.cpp:
//

#include "stdafx.h"

#include <iostream>
//#include <array>
//#include <vector>
//#include <cmath>
//#include <algorithm>


using namespace std;

double tec2_3procent ( double val, int mode ) // mode = 1 ( первая колонка) mode = 2 (вторая колонка)
{
   double ret = 0;
   double arr[][3] =
   {
      {-39, 130, 54},
      {-38, 130, 54},
      {-37, 130, 54},
      {-36, 130, 54},
      {-35, 130, 55},
      {-34, 130, 55},
      {-33, 130, 56},
      {-32, 130, 56},
      {-31, 130, 57},
      {-30, 130, 58},
      {-29, 130, 58},
      {-28, 130, 59},
      {-27, 130, 59},
      {-26, 130, 60},
      {-25, 130, 60},
      {-24, 130, 61},
      {-23, 128, 60},
      {-22, 126, 60},
      {-21, 124, 59},
      {-20, 121, 58},
      {-19, 119, 58},
      {-18, 117, 57},
      {-17, 115, 56},
      {-16, 112, 55},
      {-15, 110, 55},
      {-14, 108, 54},
      {-13, 105, 53},
      {-12, 103, 52},
      {-11, 101, 52},
      {-10,  98, 51},
      { -9,  96, 50},
      { -8,  93, 49},
      { -7,  91, 48},
      { -6,  89, 48},
      { -5,  86, 47},
      { -4,  84, 46},
      { -3,  82, 45},
      { -2,  79, 44},
      { -1,  77, 44},
      {  0,  75, 43},
      {  1,  75, 44},
      {  2,  75, 44},
      {  3,  75, 45},
      {  4,  75, 45},
      {  5,  75, 46},
      {  6,  75, 46},
      {  7,  75, 47},
      {  8,  75, 48}
   } ;

   size_t larr = sizeof arr / sizeof arr[0]; // 48

   for (int ii=0 ; ii<larr; ii++ ) {
    if (arr[ii][0]==val) {
      if (mode==1) { ret=arr[ii][1] ; break ; }
      if (mode==2) { ret=arr[ii][2] ; break ; }
    }
   }

   return (ret);
}


int _tmain(int argc, _TCHAR* argv[])
{
  double temperature = -25 ;
  std::cout << "T="<<temperature << std::endl;
  double v1 = tec2_3procent ( temperature, 1 ) ;
  std::cout << "T(before)="<<v1 << std::endl;
  double v2 = tec2_3procent ( temperature, 2 ) ;
  std::cout << "T(after)=" <<v2 << std::endl;

  return 0;
}

