// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"

#include <stdio.h> 

#include <iostream>
#include <sys/stat.h>

#include <string>
#include <sstream>
#include <iomanip>
#include <vector>
#include <chrono>
#include <cmath>
#include <algorithm>

#include <ctime>
#include <iostream>
#include <iterator>
#include <locale>


int _tmain(int argc, _TCHAR* argv[])
{
    // массив имен таблиц
    std::vector<std::string> CacheTableName;

    CacheTableName.reserve(10);

	CacheTableName.push_back( "MEAS_ARC_33_27" );
	CacheTableName.push_back( "MEAS_ARC_29_4.db" );

//  выделение из имени таблицы id раздела и архива
    std::string sp=CacheTableName[0];

	std::cout <<  " CacheTableName = " << sp ;

	//return (0);

    std::string TmpCacheTableName (sp.length()+1, ' ') ;
    //TmpCacheTableName.reserve(sp.length()+1);
    for(int i = 0, l = sp.length(); i < l; ++i)
      if (sp[i] <= '9' && sp[i] >= '0')  TmpCacheTableName[i] = sp[i];
      else TmpCacheTableName[i] = ' ';

    int LstTblID =-1;
    int ARCGINFOID =-1;
	if (2!=sscanf(TmpCacheTableName.c_str(),"%d%d",&LstTblID,&ARCGINFOID)) {
       ;
    }
    int ID_profile = ARCGINFOID ;

	std::cout <<  " ID_profile = " << ID_profile << " , LstTblID=" << LstTblID  << std::endl;



    // массив имен колонок таблицы
    std::vector<std::string> CacheTableNameCol;

    CacheTableNameCol.reserve(20);
	
	CacheTableNameCol.push_back( "ID");
	CacheTableNameCol.push_back( "TIME1970");
	CacheTableNameCol.push_back( "TIME_MKS");
	CacheTableNameCol.push_back( "STATE");
	CacheTableNameCol.push_back( "VAL");
	CacheTableNameCol.push_back( "MIN_VAL");
	CacheTableNameCol.push_back( "MAX_VAL");


	std::string QCol = "" ;
    std::string QColPunc = "," ;

    if ( std::find(CacheTableNameCol.begin(), CacheTableNameCol.end(), "TIME1970") != CacheTableNameCol.end() )
       QCol="TIME1970";
    else
       QCol="0";
    if ( std::find(CacheTableNameCol.begin(), CacheTableNameCol.end(), "TIME_MKS") != CacheTableNameCol.end() )
       QCol=QCol+QColPunc+"TIME_MKS";
    else
       QCol=QCol+QColPunc+"0";
    if ( std::find(CacheTableNameCol.begin(), CacheTableNameCol.end(), "STATE") != CacheTableNameCol.end() )
       QCol=QCol+QColPunc+"STATE";
    else
       QCol=QCol+QColPunc+"0";
    if ( std::find(CacheTableNameCol.begin(), CacheTableNameCol.end(), "VAL") != CacheTableNameCol.end() )
       QCol=QCol+QColPunc+"VAL";
    else
       QCol=QCol+QColPunc+"0";
    if ( std::find(CacheTableNameCol.begin(), CacheTableNameCol.end(), "MIN_VAL") != CacheTableNameCol.end() )
       QCol=QCol+QColPunc+"MIN_VAL";
    else
       QCol=QCol+QColPunc+"0";
    if ( std::find(CacheTableNameCol.begin(), CacheTableNameCol.end(), "MAX_VAL") != CacheTableNameCol.end() )
       QCol=QCol+QColPunc+"MAX_VAL";
    else
       QCol=QCol+QColPunc+"0";


    std::string sql1 = "SELECT " + QCol + " FROM " + CacheTableName[0] + " WHERE TIME1970>=? and TIME1970<=? and ID=? " ;
    std::string sql2 = "SELECT " + QCol + " FROM " + CacheTableName[0] + " WHERE TIME1970>=? and TIME1970<=? and ID>=? and ID<=? ;" ;

    std::cout <<  "Select command = " << sql1 << std::endl;


    // Format time, "ddd yyyy-mm-dd hh:mm:ss zzz"
    std::time_t t = 1685368800 ; // std::time(nullptr);
    char mbstr[100];

	struct tm  ts;
    ts = *localtime(&t);
	ts = *gmtime(&t);

    if (std::strftime(mbstr, sizeof(mbstr), "%Y-%m-%d %H:%M:%S.000+0000", std::gmtime(&t)))
        std::cout << mbstr << '\n';

	// std::strftime(std::data(timeString), std::size(timeString),  "yyyy-mm-ddThh:mm:ssZ", std::gmtime(&time));


	return 0;
}

