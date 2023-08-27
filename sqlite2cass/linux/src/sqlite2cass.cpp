/*
 project: sqlile2cass v2


*/

#include <stdint.h>
#include <string.h>

#include <csignal>
#include <fstream>
#include <iostream>
#include <sys/stat.h>

#include <unistd.h> // for Linux : stat

#include <string>
#include <sstream>
#include <iomanip>
#include <vector>
#include <chrono>
#include <cmath>
#include <algorithm>
#include <ctime>

#include <sqlite3.h>

#include <db2const.h>
#include <utils.h>
#include <rsdudbapi.h>
#include <rsduoracle.h>
#include <rsdusql.h>
#include <rsducass.h>
#include <rsducpp/log.h>

#include "oracle2cass.h"

//using namespace std;

// Запрос информации о профиле с заданным ID
constexpr char SQLSelectProfileSQLITE[] =
"SELECT y.ID id_tbllst, x.table_name arc_tblname, sch.SCHEMA_NAME arc_schema_name, "
"       arc_ginfo.DEPTH, arc_ginfo.STATE, sys_gtopt.INTERVAL, SYS_GTOPT.ID_GTYPE "
"   FROM sys_tbllst x, sys_tbllst y, sys_tbllnk st, sys_otyp ot, "
"   arc_db_schema sch, arc_services_info si, "
"   arc_subsyst_profile, arc_ginfo, sys_gtopt "
"  WHERE y.id = st.id_lsttbl "
"    AND x.ID = st.id_dsttbl "
"    AND x.id_type = ot.ID "
"    AND ot.define_alias = 'ARH' "
"    AND si.ID_DB_SCHEMA = sch.ID "
"    AND si.ID_LSTTBL = y.id "
"    AND arc_subsyst_profile.ID_TBLLST = y.id "
"    AND arc_subsyst_profile.ID_GINFO = arc_ginfo.ID "
"    AND sys_gtopt.ID = arc_ginfo.ID_GTOPT "
"    AND arc_ginfo.ID = %d "
"    AND arc_subsyst_profile.ID_TBLLST = %d "
" ORDER BY y.ID ";

// Запрос списка архивных таблиц
constexpr char SQLSelectArcTablesSQLITE[] =
"SELECT id_param, retfname "
"   FROM %s "
"  WHERE id_ginfo = %u "
" ORDER BY id_param ASC";
constexpr char SQLSelectArcTablesRangeParamsSQLITE[] =
"SELECT id_param, retfname "
"   FROM %s "
"  WHERE id_ginfo = %u "
"  AND id_param >= %u "
"  AND id_param <= %u "
" ORDER BY id_param ASC";

bool FileExists( const std::string &Filename )
{
    return access( Filename.c_str(), F_OK ) == 0; // != -1
}



// Static variable
std::string   JobName = "SQLite utility";
sqlite3       *db_handle = NULL;
std::ofstream outFile ;
//DB_ACCES_INFO *ui ;


int CsvSqlite( std::string bdname, time_t input_start_time, time_t input_end_time, int start_id_param, int end_id_param)
{
    //sqlite3       *db_handle = NULL; // !!!!! перемещено в static
    sqlite3_stmt  *ppStmt = NULL;
    int rc = 0 ;
    char QuerySql[MAX_QUERY_LEN] = {0}; // SQL query to retrieve CSV data
    time_t ltime0;

    std::string delimiter = ",";  // delimiter (default comma)
    int header = 1 ; // output header
    int header_flag = 0 ; // output header
    int ID_profile = -1 ; // profile
    int LstTblID= -1 ; // razdel

    int partfile = 0 ; // output index part file
    //std::ofstream outFile ; // !!!!! перемещено в static
    std::string QSQLite ="";
    long long int sizeFile = 0 ; // calc file size
    long long int rows = 0; // calc count rows
    char strpartfile[10] ;

    char mbstr[100]; // for strftime
    time_t t ;

    // массив имен таблиц
    std::vector<std::string> TableNames;
    // массив имен колонок таблицы
    std::vector<std::string> TableNameCols;

    TableNames.reserve(10);
    TableNameCols.reserve(20);


// ---------------------------------------------------------------------------

// корректировка на +-12 часов = 43200 сек
   if (input_start_time>43200) input_start_time-=43200;
   input_end_time+=43200;

// ---------------------------------------------------------------------------

    // проверка на существование файла
    struct stat sb;
    // If the file/directory exists at the path returns 0
    // If block executes if path exists
    if (stat(bdname.c_str(), &sb) == 0 && !(sb.st_mode & S_IFDIR))
        std::cout << "The path " << bdname << " is valid." << std::endl;
    else {
        std::cout << "The path " << bdname << " is invalid !" << std::endl;
        return 0;
    }

/* Инициализируем соединение с БД */
    rc = sqlite3_open_v2(bdname.c_str(), &db_handle,SQLITE_OPEN_READWRITE, NULL); // SQLITE_OPEN_READONLY - filesystem OS - lock
    if ( rc != SQLITE_OK )
    {
        std::cout << "Can't open SQLite database file :" << bdname << " -" << sqlite3_errmsg(db_handle);

        sqlite3_close(db_handle);
        /* В случае ошибки обращения к файлу завершаем с ним работу и завершаем задачу */
        return(0);
    }

    sqlite3_busy_timeout(db_handle,3000);

    rc = sqlite3_exec(db_handle, "PRAGMA journal_mode=OFF", NULL, NULL, NULL);
    if ( rc != SQLITE_OK )
    {
        std::cout <<  "'PRAGMA journal_mode=OFF' error = " << sqlite3_errmsg(db_handle) << std::endl;
    }

    rc = sqlite3_exec(db_handle, "PRAGMA synchronous=OFF", NULL, NULL, NULL);
    //rc = sqlite3_exec(db_handle, "PRAGMA encoding = 'UTF-8'", NULL, NULL, NULL);

    std::cout << "....database file opened successfully!" << std::endl;

    std::cout <<  " Time period = [ " << input_start_time << " , " << input_end_time  << " ] " << std::endl;

// получение имен таблиц в файле
    std::cout << std::endl << " Tables :" << std::endl;
    QuerySql[0]='\0';
    sprintf(QuerySql, "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name");
    rc = sqlite3_prepare_v2( db_handle, QuerySql , -1, &ppStmt, NULL );
    if ( rc == SQLITE_OK )
    {
        while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
        {
            std::cout << "\t" << (const char *)sqlite3_column_text( ppStmt, 0 ) << std::endl;

            TableNames.push_back( (char *)sqlite3_column_text( ppStmt, 0 ) );
        }
    } else {
        std::cout <<  "Get TableName from sqlite_master find error = " << sqlite3_errmsg(db_handle) << std::endl;

        sqlite3_finalize( ppStmt );
        sqlite3_close( db_handle );
        return (0);
    }
    // close the statement
    sqlite3_finalize( ppStmt );


    for (auto &tb : TableNames)
    {
       TableNameCols.clear();

       // выделение из имени таблицы - id раздела и id архива
       std::string TmpCacheTableName (tb.length()+1, ' ') ;

       for(int i = 0, l = tb.length(); i < l; ++i)
         if (tb[i] <= '9' && tb[i] >= '0')  TmpCacheTableName[i] = tb[i];
         else TmpCacheTableName[i] = ' ';

       if (2!=sscanf(TmpCacheTableName.c_str(),"%d%d",&LstTblID,&ID_profile)) {
          ;
       }

       if (ID_profile<=0 || LstTblID <=0) {
           std::cout <<  "Error! ID_profile=" << ID_profile << " , LstTblID=" << LstTblID  << std::endl;
           break ;
       }

       std::cout <<  " ID_profile = " << ID_profile << " , LstTblID = " << LstTblID  << std::endl;

       time(&ltime0);

       // колонки из табл
       std::cout << std::endl << " Table Cols: "  << tb <<  " , "  << ctime(&ltime0) << std::endl;
       QuerySql[0]='\0';
       sprintf(QuerySql, "PRAGMA table_info( %s )",tb.c_str());
       rc = sqlite3_prepare_v2( db_handle, QuerySql , -1, &ppStmt, NULL );
       if ( rc == SQLITE_OK )
       {
           while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
           {
               std::cout << (const char *)sqlite3_column_text( ppStmt, 0 ) << " , "
                         << (const char *)sqlite3_column_text( ppStmt, 1 ) << " , "
                         << (const char *)sqlite3_column_text( ppStmt, 2 ) << std::endl;

               TableNameCols.push_back( (const char *)sqlite3_column_text( ppStmt, 1 ) );
           }
       } else {
           std::cout <<  "PRAGMA table_info error = " << sqlite3_errmsg(db_handle) ;
           break ;
       }
       sqlite3_finalize( ppStmt );

       // for select from DB
       //  порядок ID,TIME1970,TIME_MKS,STATE,VAL,MIN_VAL,MAX_VAL

       std::string QCol = "" ;
       std::string QColPunc = "," ;

       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "ID") != TableNameCols.end() )
          QCol="ID";
       else
          QCol="0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "TIME1970") != TableNameCols.end() )
          QCol=QCol+QColPunc+"TIME1970";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "TIME_MKS") != TableNameCols.end() )
          QCol=QCol+QColPunc+"TIME_MKS";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "STATE") != TableNameCols.end() )
          QCol=QCol+QColPunc+"STATE";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "VAL") != TableNameCols.end() )
          QCol=QCol+QColPunc+"VAL";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MIN_VAL") != TableNameCols.end() )
          QCol=QCol+QColPunc+"MIN_VAL";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MAX_VAL") != TableNameCols.end() )
          QCol=QCol+QColPunc+"MAX_VAL";
       else
          QCol=QCol+QColPunc+"0";

       std::string sqls = "";
       std::string sql0 = "SELECT " + QCol + " FROM " + tb ;
       std::string sql1 = sql0 + " WHERE time1970 between ? and ? ORDER BY ID asc " ; // LIMIT 5000 OFFSET 0+i
       std::string sql2 = sql0 + " WHERE time1970 between ? and ? and ID>= ? and ID<= ? ORDER BY ID asc " ; // LIMIT 5000 OFFSET 0+i
       std::string sql3 = sql0 + " WHERE time1970 between ? and ? and ID>= ? ORDER BY ID asc " ; // LIMIT 5000 OFFSET 0+i
       std::string sql4 = sql0 + " WHERE time1970 between ? and ? and ID<= ? ORDER BY ID asc " ; // LIMIT 5000 OFFSET 0+i

       int tsql = 0 ;
       sqls = sql1 ;
       if (start_id_param>0) {
         sqls = sql3 ;
         tsql = 3 ;
       }
       if (end_id_param>0) {
         sqls = sql4 ;
         tsql = 4 ;
       }
       if (start_id_param>0 && end_id_param>0) {
         sqls = sql2 ;
         tsql = 2 ;
       }

       std::cout << std::endl << "Query command = " << sqls << std::endl;

       time(&ltime0);

       QSQLite ="";
       sizeFile = 0 ;
       rows = 0;
       strpartfile[0]='\0';sprintf(strpartfile,"%04d",partfile);
       std::string tbfile=tb+"_" + strpartfile + ".csv" ; // partfile
       outFile.open(tbfile.c_str(), std::ios::out | std::ios::trunc );
       if (outFile.fail())
       {
          std::cout << "The file [ " << tbfile << " ] was not successfully opened for saving" << std::endl;
          return(1);
       }
       outFile << std::setprecision(12) ; // точность 12 знаков
       std::cout << "Create file = [ " << tbfile << " ] ...." << ctime(&ltime0) << std::endl;

       //Прогресс
       unsigned progressDot = 0;
       header_flag = 0 ;


       time_t tSplitDateFrom = input_start_time;
       time_t tSplitDateTo = input_end_time;

/*
       time_t tSplitDateFrom = input_start_time;
       time_t tSplitDateTo = tSplitDateFrom + 1 * MaxDataCountinResult;
       time_t tDateTo = input_end_time;

       while (tSplitDateFrom <= tDateTo)
       {
           if (tSplitDateTo > tDateTo)
               tSplitDateTo = input_end_time;

*/

           QuerySql[0]='\0';
           sprintf(QuerySql, "%s ",sqls.c_str());
           rc = sqlite3_prepare_v2( db_handle, QuerySql , strlen(QuerySql), &ppStmt, NULL );
           if ( rc == SQLITE_OK )
           {
              sqlite3_reset(ppStmt);
              sqlite3_clear_bindings(ppStmt);

              rc = sqlite3_bind_int64(ppStmt, 1, tSplitDateFrom);
              if( rc != SQLITE_OK ) {
                std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                break;
              }
              rc = sqlite3_bind_int64(ppStmt, 2, tSplitDateTo);
              if( rc != SQLITE_OK ) {
                std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                break;
              }

              if (tsql==3) {
                rc = sqlite3_bind_int(ppStmt, 3, start_id_param);
                if( rc != SQLITE_OK ) {
                  std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                  break;
                }
              }
              if (tsql==4) {
                rc = sqlite3_bind_int(ppStmt, 3, end_id_param);
                if( rc != SQLITE_OK ) {
                  std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                  break;
                }
              }
              if (tsql==2) {
                rc = sqlite3_bind_int(ppStmt, 3, start_id_param);
                if( rc != SQLITE_OK ) {
                   std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                   break;
                }
                rc = sqlite3_bind_int(ppStmt, 4, end_id_param);
                if( rc != SQLITE_OK ) {
                   std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                   break;
                }
              }

           } else {
               std::cout <<  "Prepare to exec = " << sqlite3_errmsg(db_handle) << std::endl;
               break;
           }


           // output header
/*(
  id_tbllst int,
  id int,
  time1970 timestamp,
  val double,
  state bigint,
  min_val double,
  max_val double,
  PRIMARY KEY ((id_tbllst, id), time1970)
  ) WITH CLUSTERING ORDER BY (time1970 DESC)
*/
           if (header && header_flag==0) {
             QSQLite = "ID_TBLLST" ;
             if ( std::find(TableNameCols.begin(), TableNameCols.end(), "ID") != TableNameCols.end() )
                QSQLite=QSQLite+delimiter+"ID";
             if ( std::find(TableNameCols.begin(), TableNameCols.end(), "TIME1970") != TableNameCols.end() )
                QSQLite=QSQLite+delimiter+"TIME1970";
             if ( std::find(TableNameCols.begin(), TableNameCols.end(), "VAL") != TableNameCols.end() )
                QSQLite=QSQLite+delimiter+"VAL";
             if ( std::find(TableNameCols.begin(), TableNameCols.end(), "STATE") != TableNameCols.end() )
                QSQLite=QSQLite+delimiter+"STATE";
             if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MIN_VAL") != TableNameCols.end() )
                QSQLite=QSQLite+delimiter+"MIN_VAL";
             if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MAX_VAL") != TableNameCols.end() )
                QSQLite=QSQLite+delimiter+"MAX_VAL";

             std::cout <<  "CSV Header = " << QSQLite << std::endl;

             if (outFile.is_open()) { /* ok, proceed with output */
               outFile <<  QSQLite << std::endl;
               sizeFile = sizeFile + QSQLite.size() + 1;
               header_flag = 1 ;
             }
           }


           while ( sqlite3_step( ppStmt ) == SQLITE_ROW ) // SQLITE_DONE
           {
               unsigned id = 0, time1970 = 0, time_mks = 0, state = 0;
               unsigned bval = 0;
               double   aval = 0, min_val = 0, max_val = 0;

               id = sqlite3_column_int64(ppStmt, 0);
               time1970 = sqlite3_column_int64(ppStmt, 1);
               time_mks = sqlite3_column_int(ppStmt, 2);
               state = sqlite3_column_int64(ppStmt, 3);
               aval = sqlite3_column_double(ppStmt, 4);
               min_val = sqlite3_column_double(ppStmt, 5);
               max_val = sqlite3_column_double(ppStmt, 6);

               mbstr[0]='\0';
               t = static_cast<time_t>(time1970) ;
               if (std::strftime(mbstr, sizeof(mbstr), "%Y-%m-%d %H:%M:%S", std::gmtime(&t))) // std::gmtime(&t) std::localtime(&t)
                 ;
               else
                 sprintf(mbstr,"%u",time1970 );

               // cannot make
               std::string tmstr(mbstr);
               if (time_mks>0) {
                  char buf_time_mks[40];
                  buf_time_mks[0]='\0';
                  sprintf(buf_time_mks,"%03d", (int)time_mks/1000 );
                  std::string str_time_mks(buf_time_mks);
                  tmstr = tmstr + "." + str_time_mks + "+0000" ;
               } else {
                  tmstr = tmstr + ".000+0000" ;
               }

               std::string fstr = "" ;
               fstr = "" + std::to_string(LstTblID) ;
               if ( std::find(TableNameCols.begin(), TableNameCols.end(), "ID") != TableNameCols.end() )
                  fstr = fstr + delimiter + std::to_string(id) ;
               if ( std::find(TableNameCols.begin(), TableNameCols.end(), "TIME1970") != TableNameCols.end() )
                  fstr = fstr + delimiter + tmstr ;
               if ( std::find(TableNameCols.begin(), TableNameCols.end(), "VAL") != TableNameCols.end() )
                  fstr = fstr + delimiter + std::to_string(aval) ;
               if ( std::find(TableNameCols.begin(), TableNameCols.end(), "STATE") != TableNameCols.end() )
                  fstr = fstr + delimiter + std::to_string(static_cast<int64_t>(state)) ;
               if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MIN_VAL") != TableNameCols.end() )
                  fstr = fstr + delimiter + std::to_string(min_val) ;
               if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MAX_VAL") != TableNameCols.end() )
                  fstr = fstr + delimiter + std::to_string(max_val) ;

               if (outFile.is_open()) { /* ok, proceed with output */
                 if (outFile.fail())
                 {
                   std::cout << ".....Error write to file" << std::endl;
                 } else {
                   outFile << fstr << std::endl;
                   sizeFile = sizeFile + fstr.size()+ 1;
                   rows++;
                 }
               }

               // число строк около 2 млн строк
               if ( rows > 1999990 ) // (sizeFile/(1024*1024)) > 1024  // 1024 mb
               {
                 outFile.flush();
                 outFile.close();
                 std::cout << std::endl << "Rows = " << rows << " , FileSize=" << sizeFile << " bytes" << std::endl;

                 partfile++;

                 sizeFile = 0 ;
                 rows = 0 ;
                 header_flag = 0 ;

                 strpartfile[0]='\0';sprintf(strpartfile,"%04d",partfile);
                 std::string tbfile=tb+"_" + strpartfile + ".csv" ; // partfile
                 outFile.open(tbfile.c_str(), std::ios::out | std::ios::trunc );
                 if (outFile.fail())
                 {
                    std::cout << "The file [ " << tbfile << " ] was not successfully opened for saving" << std::endl;
                    sqlite3_finalize( ppStmt );
                    sqlite3_close( db_handle );
                    return(1); // break ;
                 }
                 outFile << std::setprecision(12) ; // точность 12 знаков
                 time(&ltime0);
                 std::cout << "Create file = [ " << tbfile << " ] ...." << ctime(&ltime0) << std::endl;

                 if (outFile.is_open()) {
                   outFile <<  QSQLite << std::endl;
                   sizeFile = sizeFile + QSQLite.size() + 1;
                   header_flag = 1 ;
                 }
               }

               if (++progressDot % 10000 == 0)
                       std::cout << "." << std::flush;
           }//while
           sqlite3_finalize( ppStmt );

/*
           // Calculate RestoreDate from and to.
           tSplitDateFrom = tSplitDateTo + 1;
           tSplitDateTo = tSplitDateFrom + 1 * MaxDataCountinResult;
       }// while (tSplitDateFrom <= tDateTo)
*/

       outFile.flush();
       outFile.close();
       std::cout << std::endl << "Rows = " << rows << " , FileSize=" << sizeFile << " bytes" << std::endl;

       time(&ltime0);
       std::cout << std::endl << "End=" << ctime(&ltime0) << std::endl;

    } // for (tb)

    sqlite3_close( db_handle );

    std::cout << " .... csv finished.." <<std::endl;

    return (0);
}



int InfoSqlite( std::string bdname , int create_index)
{
    //sqlite3       *db_handle = NULL;
    sqlite3_stmt  *ppStmt = NULL;
    int rc = 0 ;
    char *pErrMsg = 0 ;
    char QuerySql[MAX_QUERY_LEN] = {0};
    time_t ltime0;

    char mbstr[80]; // for strftime
    time_t t ;

    // массив имен таблиц
    std::vector<std::string> TableNames;
    // массив имен колонок таблицы
    std::vector<std::string> TableNameCols;

    TableNames.reserve(10);
    TableNameCols.reserve(20);

// --------------------------------------------------------------------------

    // проверка на существование файла
    struct stat sb;
    // If the file/directory exists at the path returns 0
    // If block executes if path exists
    if (stat(bdname.c_str(), &sb) == 0 && !(sb.st_mode & S_IFDIR))
        std::cout << "The path " << bdname << " is valid." << std::endl;
    else {
        std::cout << "The path " << bdname << " is invalid !" << std::endl;
        return 0;
    }

    // Init
    /*
    if (sqlite3_initialize() != SQLITE_OK) {
        std::cerr <<  "Err. Unable to initialize the library." << std::endl;
        exit(0);
    }
    */

/* Инициализируем соединение с БД */
    rc = sqlite3_open_v2(bdname.c_str(), &db_handle,SQLITE_OPEN_READWRITE, NULL); // SQLITE_OPEN_READONLY - filesystem OS - lock
    if ( rc != SQLITE_OK )
    {
        std::cout << "Can't open SQLite database file :" << bdname << " -" << sqlite3_errmsg(db_handle);

        sqlite3_close(db_handle);
        /* В случае ошибки обращения к файлу завершаем с ним работу и завершаем задачу */
        return(0);
    }

    sqlite3_busy_timeout(db_handle,3000);

    rc = sqlite3_exec(db_handle, "PRAGMA journal_mode=OFF", NULL, NULL, NULL);
    if ( rc != SQLITE_OK )
    {
        std::cout <<  "'PRAGMA journal_mode=OFF' error = " << sqlite3_errmsg(db_handle) << std::endl;
    }

    rc = sqlite3_exec(db_handle, "PRAGMA synchronous=OFF", NULL, NULL, NULL);
    //rc = sqlite3_exec(db_handle, "PRAGMA encoding = 'UTF-8'", NULL, NULL, NULL);

    std::cout << "....database file opened successfully!" << std::endl;

// получение имен таблиц в файле
    std::cout << std::endl << " Tables :" << std::endl;
    QuerySql[0]='\0';
    sprintf(QuerySql, "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name");
    rc = sqlite3_prepare_v2( db_handle, QuerySql , -1, &ppStmt, NULL );
    if ( rc == SQLITE_OK )
    {
        while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
        {
            std::cout << "\t" << (const char *)sqlite3_column_text( ppStmt, 0 ) << std::endl;

            TableNames.push_back( (char *)sqlite3_column_text( ppStmt, 0 ) );
        }
    } else {
        std::cout <<  "Get TableName from sqlite_master find error = " << sqlite3_errmsg(db_handle) << std::endl;

        sqlite3_finalize( ppStmt );
        sqlite3_close( db_handle );
        return (0);
    }
    // close the statement
    sqlite3_finalize( ppStmt );


   /* get statistics - слишком много ресурсов:
    - время выполнения до 1 часа, если файл > 1 GB
    - если файл < 1 Gb, то время выполнения от 2.5 сек до 2 мин
   */

    double sz = sb.st_size/ (1024 * 1024) ; // in MB

    if (sz>1024.) {
    //  char d='a';
      std::cout << std::endl;
      std::cout<<"Расчет min|max|count для файлов > 1 Gb может занять от 20 мин до 70 мин !" << std::endl;
      std::cout<<"Необходимо включить индексацию для таблиц !" << std::endl;
    /*
      std::cout<<"Продолжить? [Y\\n] =";
      // бесконечный ввод
      int fl=1 ;
      while(fl) {
        d=getchar();
        switch(d) {
          case 'n': case'N':
            sqlite3_close( db_handle );
            std::cout << " Exit program, good bye." <<std::endl;
            return(0);
          break;
          case 'y': case'Y': case '\r': fl=0;
          break;
        }
      }
    */
      std::cout << std::endl;
    }

    // включение индексации для файлов
    if (create_index>0) {
      std::cout << std::endl;
      std::cout<<"Активация индексации для таблиц:" << std::endl;
      std::cout<<"\t\t - Процесс может несколько часов!" << std::endl;
      std::cout<<"\t\t - Увеличение размера файла!" << std::endl;
      std::cout<<"   Ожидайте .......!" << std::endl;
      for (auto &tbi : TableNames)
      {
         time(&ltime0);
         std::cout << std::endl << "\t Table name = "  << tbi <<  " , "  << ctime(&ltime0) << std::endl;
         QuerySql[0]='\0';
         sprintf(QuerySql, "CREATE INDEX if not exists ID_TIME1970_INDX ON %s (ID ASC, TIME1970 ASC)",tbi.c_str());
         rc = sqlite3_exec(db_handle, QuerySql, NULL, NULL, &pErrMsg );
         if ( rc != SQLITE_OK )
         {
           std::cout <<  QuerySql << " error = " << pErrMsg << std::endl;
           sqlite3_free(pErrMsg);
         }
         time(&ltime0);
         std::cout << std::endl << "\t End indexing = "  << ctime(&ltime0) << std::endl;
      }
      std::cout << std::endl;
    }

    for (auto &tb : TableNames)
    {
       TableNameCols.clear();

       time(&ltime0);

       // колонки из табл = cid  name  type  notnull  dflt_value  pk
       std::cout << std::endl << " Table Cols: "  << tb <<  " , "  << ctime(&ltime0) << std::endl;
       QuerySql[0]='\0';
       sprintf(QuerySql, "PRAGMA table_info( %s )",tb.c_str());
       rc = sqlite3_prepare_v2( db_handle, QuerySql , -1, &ppStmt, NULL );
       if ( rc == SQLITE_OK )
       {
           while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
           {
               std::cout << (const char *)sqlite3_column_text( ppStmt, 0 ) << " , "
                         << (const char *)sqlite3_column_text( ppStmt, 1 ) << " , "
                         << (const char *)sqlite3_column_text( ppStmt, 2 ) << std::endl;

               TableNameCols.push_back( (char *)sqlite3_column_text( ppStmt, 1 ) );
           }
       } else {
           std::cout <<  "PRAGMA table_info error = " << sqlite3_errmsg(db_handle) ;
       }
       sqlite3_finalize( ppStmt );

       time(&ltime0);

       std::cout << std::endl;
       std::cout << " Table IDs: "  << tb <<  " , "  << ctime(&ltime0) << std::endl;
       QuerySql[0]='\0';
       sprintf(QuerySql, "SELECT min(ID),max(ID),COUNT( DISTINCT ID ),count(ID),min(TIME1970),max(TIME1970) FROM %s ",tb.c_str());
       rc = sqlite3_prepare_v2( db_handle, QuerySql, strlen(QuerySql), &ppStmt, NULL );
       if ( rc == SQLITE_OK )
       {
           while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
           {
               std::cout << " min(ID) = " << (const char *)sqlite3_column_text( ppStmt, 0 ) << std::endl;
               std::cout << " max(ID) = " << (const char *)sqlite3_column_text( ppStmt, 1 ) << std::endl;
               std::cout << " Number parameters = " << (const char *)sqlite3_column_text( ppStmt, 2 ) << std::endl;
               std::cout << " Total records = " << (const char *)sqlite3_column_text( ppStmt, 3 ) << std::endl;

               std::cout << " min(TIME1970) = " << (const char *)sqlite3_column_text( ppStmt, 4 ) ;

               mbstr[0]='\0';
               t = static_cast<time_t>( sqlite3_column_int64( ppStmt, 4 ) ) ;
               if (std::strftime(mbstr, sizeof(mbstr), "%Y-%m-%d %H:%M:%S", std::gmtime(&t)))
                  std::cout << " = " << mbstr << std::endl;
               else  std::cout << std::endl;

               std::cout << " max(TIME1970) = " << (const char *)sqlite3_column_text( ppStmt, 5 ) ;

               mbstr[0]='\0';
               t = static_cast<time_t>( sqlite3_column_int64( ppStmt, 5 ) ) ;
               if (std::strftime(mbstr, sizeof(mbstr), "%Y-%m-%d %H:%M:%S", std::gmtime(&t)))
                  std::cout << " = " << mbstr << std::endl;
               else  std::cout << std::endl;
           }
       } else {
           std::cout <<  QuerySql << "   error = " << sqlite3_errmsg(db_handle) ;
       }
       sqlite3_finalize( ppStmt );

       time(&ltime0);

       std::cout << std::endl << " List IDs: "  << tb <<  " , "  << ctime(&ltime0) << std::endl;
       QuerySql[0]='\0';
       sprintf(QuerySql, "SELECT DISTINCT ID FROM %s order by ID",tb.c_str());
       rc = sqlite3_prepare_v2( db_handle, QuerySql, strlen(QuerySql), &ppStmt, NULL );
       if (rc != SQLITE_OK) {
         std::cout <<  "SELECT DISTINCT ID = " << sqlite3_errmsg(db_handle) ;
       } else {
          int j = 0 ;
          while ( sqlite3_step( ppStmt ) == SQLITE_ROW ) // SQLITE_DONE
          {
            j++ ;
            std::cout << " " << (char*)sqlite3_column_text(ppStmt, 0) ;
            if (j%16==0) std::cout << std::endl;
          }//while
          std::cout << std::endl;
       }
       sqlite3_finalize( ppStmt );

       time(&ltime0);
       std::cout << std::endl << "End=" << ctime(&ltime0) << std::endl;

    } // for

    sqlite3_close( db_handle );

    std::cout << " .... finished program." <<std::endl;

    return (0);
}


/*
*  Обработка сигнала завершения приложения
*/
void OnExit(int sig) // C++17
{
    if (outFile.is_open()) { /* ok, proceed with output */
      outFile.flush();
      outFile.close();
  }

    if (db_handle!=NULL) sqlite3_close( db_handle );

  //if (ui!=NULL) RSDURTGUtils_DBClose(ui);

  std::cout << std::endl << " Program break ! " << std::endl;
}


time_t convert_input_datetime(const std::string input_date_str)
{
    time_t converted_time = -1;

    try
    {
        long long input_date = std::stoll(input_date_str);

        struct tm timeinfo{0};
        timeinfo.tm_year = (input_date / 1000000ll) % 10000ll - 1900;
        timeinfo.tm_mon  = (input_date / 10000000000ll) % 100ll - 1;
        timeinfo.tm_mday = (input_date / 1000000000000ll) % 100ll;
        timeinfo.tm_hour = (input_date / 10000ll) % 100ll;
        timeinfo.tm_min  = (input_date / 100ll) % 100ll;
        timeinfo.tm_sec  =  input_date % 100ll;

        converted_time = RSDURTGUtils_SMktime(&timeinfo);
    }
    catch (std::exception &e)
    {
        std::cout << "Error Convert input date/time : " << e.what() << std::endl;
    }

    return converted_time;
}

inline void rtrim(char* str, const size_t str_size)
{
    str[str_size - 1] = '\0';
    if (strchr(str, ' ') != (char*)0)
        *strchr(str, ' ') = '\0';
}


int main(int argc, char* argv[])
{
    // обработка на прерывания программы от пользователя или в искл ситуациях. CTRL-Z
    signal(SIGINT,  OnExit);
    signal(SIGQUIT, OnExit); // --
    signal(SIGKILL, OnExit); // --
    signal(SIGTERM, OnExit);

    //Check input parameters from command line
    auto print_right_parameters = [](std::string program)
    {
        std::cout << "Usage: \n";
        std::cout << '\t' << program << " DBfile start_datetime end_datetime login password TNSDB [start_id_param end_id_param]\n";
        std::cout << "where: \n";
        std::cout << "\tDBfile         - SQLite DB of real time RSDU\n";
        std::cout << "\tstart_datetime - begin time restore, format: DDMMYYYYHHMISS (local timezone)\n";
        std::cout << "\tend_datetime   - end time restore, format: DDMMYYYYHHMISS (local timezone)\n";
        std::cout << "\tlogin password - login and password to connect to Oracle DB RSDU\n";
        std::cout << "\tTNSDB          - RSDU database TNS with configured Cassandra cluster\n";
        std::cout << "\nexamples:\n";
        std::cout << "\t to Cassandra:\n";
        std::cout << '\t' << program << " MEAS_ARC_33_27.db 01012019000000 01062019000000 rsduadmin passme RSDU\n";
        std::cout << '\t' << program << " ./MEAS_ARC_29_70.db 01012019000000 01062019000000 rsduadmin passme RSDU 10000 99999" << std::endl;
        std::cout << "\t info about SQLite:\n";
        std::cout << '\t' << program << " ./MEAS_ARC_116_3.db" << std::endl;
        std::cout << "\t to CSV from SQLite:\n";
        std::cout << '\t' << program << " ./MEAS_ARC_29_93.db 01012019000000 01062019000000 rsduadmin passme CSV " << std::endl;
        std::cout << '\t' << program << " ./MEAS_ARC_116_3.db 01012019000000 01062019000000 rsduadmin passme CSV 10000 99999" << std::endl;
    };

    if (argc == 2) // output info from sqlite
    {
        InfoSqlite( argv[1] , 0 ) ;
        return 0;
    }
    if (argc == 3) // output info from sqlite + create index
    {
        int _index = 0 ;
        std::string create_index = argv[2];
        if (create_index=="i"||create_index=="-i"||create_index=="I"||create_index=="-I") _index = 1 ;
        InfoSqlite( argv[1] , _index ) ;
        return 0;
    }

    if (argc < 6)
    {
        std::cout << "Not enough parameters!" << std::endl;
        print_right_parameters(argv[0]);
        return 0;
    }


    int ID_profile = -1, start_id_param = -1, end_id_param = -1;
    bool bParamRange = false;

    if (argc >= 8)
    {
        bParamRange = true;
        try
        {
            start_id_param = std::stoi(argv[7]);
        }
        catch (std::exception &e)
        {
            std::cout << "ERROR convert input start_id_param : " << e.what();
        }
    }
    if (argc >= 9)
    {
        bParamRange = true;
        try
        {
            end_id_param = std::stoi(argv[8]);
        }
        catch (std::exception &e)
        {
            std::cout << "ERROR convert input end_id_param : " << e.what();
        }

        if (end_id_param<=0) end_id_param=2147483646 ; // тип int-1 или 999999999;
        if (start_id_param>end_id_param) {
          // swap
          end_id_param = start_id_param + end_id_param;
          start_id_param = end_id_param - start_id_param;
          end_id_param = end_id_param - start_id_param;
        }
    }

    time_t input_start_time = convert_input_datetime(argv[2]);
    time_t input_end_time = convert_input_datetime(argv[3]);

    if ( input_start_time < 0 || input_end_time < 0 || (bParamRange && (start_id_param < 0 || end_id_param < 0)))
    {
        std::cout << "Input data format error!" << std::endl;
        print_right_parameters(argv[0]);
        return -1;
    }
    else
    {
      struct RSDU_timeval time_tick {0};
      time_tick.tv_sec = input_start_time;
      char time_str[CTICK_BUFFER_SIZE] = {0};
      std::cout << "decode_start_time: <" << ctick_line_r(&time_tick, time_str) << ">" ;

      std::cout << "   [ UTC: " << std::put_time(std::gmtime(&input_start_time), "%c %Z") << " , local: " << std::put_time(std::localtime(&input_start_time), "%c %Z") << " ] "<< std::endl;

      time_tick.tv_sec = input_end_time;
      std::cout << "decode_end_time:   <" << ctick_line_r(&time_tick, time_str) << ">" ;

      std::cout << "   [ UTC: " << std::put_time(std::gmtime(&input_end_time), "%c %Z") << " , local: " << std::put_time(std::localtime(&input_end_time), "%c %Z") << " ] "<< std::endl;

      if (bParamRange)
        std::cout << "set parameters range ID: [ " << start_id_param << " - " << end_id_param  << " ]"<< std::endl;
      else
        std::cout << "set parameters range ID: [ ALL ]" << std::endl;
    }

    std::string loginDBOracle = argv[4];
    std::string passwordDBOracle = argv[5];
    std::string newTNS = argv[6];

    std::string fileDBSQLite(argv[1]);

    //////////////////////////////////////////////////////////////////////////
    std::string tns1 = newTNS;
    std::transform(tns1.begin(),tns1.end(),tns1.begin(),::tolower);
    std::string csv[] {"csv", "xls", "xlsx", "txt"};
    for (unsigned ii {0}; ii < std::size(csv); ii++)
    {
        if ( 0==csv[ii].compare(tns1) )
        {
          CsvSqlite( fileDBSQLite, input_start_time, input_end_time, start_id_param, end_id_param ) ;// output to csv from sqlite
          return 0;
        }
    }

    //////////////////////////////////////////////////////////////////////////

    //Read input parameters from the Sqlite DB
    sqlite3       *db_handle = NULL;
    sqlite3_stmt  *ppStmt = NULL;
    int ReturnCode = 0 ;
    char *pErrMsg = 0 ;
    char QuerySql[MAX_QUERY_LEN] = {0};
    int LstTblID =-1 ;

    // массив имен таблиц
    std::vector<std::string> TableNames;
    // массив имен колонок таблицы
    std::vector<std::string> TableNameCols;

    TableNames.reserve(10);
    TableNameCols.reserve(20);

    //////////////////////////////////////////////////////////////////////////

    // проверка на существование файла
    struct stat sb;
    // If the file/directory exists at the path returns 0
    // If block executes if path exists
    if (stat(argv[1], &sb) == 0 && !(sb.st_mode & S_IFDIR))
        std::cout << "The path " << argv[1]  << " is valid." << std::endl;
    else {
        std::cout << "The path " << argv[1]  << " is invalid !" << std::endl;
        return 0;
    }

    std::cout <<  "Open Sqlite DB file : " << fileDBSQLite << std::endl;

/* Инициализируем соединение с БД */
    ReturnCode = sqlite3_open_v2(fileDBSQLite.c_str(), &db_handle, SQLITE_OPEN_READWRITE, NULL);
    if ( ReturnCode != SQLITE_OK )
    {
        std::cout << "Can't open SQLite database file :" << fileDBSQLite << " -" << sqlite3_errmsg(db_handle);

        sqlite3_close(db_handle);
        /* В случае ошибки обращения к файлу завершаем с ним работу и завершаем задачу */
        exit(0);
    }

    std::cout << "....database file opened successfully!" << std::endl;

    sqlite3_busy_timeout(db_handle,6000);

    ReturnCode = sqlite3_exec(db_handle, "PRAGMA journal_mode=OFF", NULL, NULL, NULL);
    if ( ReturnCode != SQLITE_OK )
    {
        std::cout <<  "'PRAGMA journal_mode=OFF' error = " << sqlite3_errmsg(db_handle) << std::endl;
    }

// получение имен таблиц в файле
    QuerySql[0]='\0';
    sprintf(QuerySql, "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name");
    ReturnCode = sqlite3_prepare_v2( db_handle, QuerySql , strlen(QuerySql), &ppStmt, NULL );
    if ( ReturnCode == SQLITE_OK )
    {
        while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
        {
            TableNames.push_back( (const char *)sqlite3_column_text( ppStmt, 0 ) );
        }
    } else {
        std::cout <<  "Get TableName from sqlite_master find error = " << sqlite3_errmsg(db_handle);

        sqlite3_finalize( ppStmt );
        sqlite3_close( db_handle );
        return (0);
    }
    // close the statement
    sqlite3_finalize( ppStmt );



 // обход всех таблиц в sqlite БД
    for (auto &tb : TableNames)
    {
       TableNameCols.clear();

       //  выделение из имени таблицы - id раздела и id архива
       // 1 ФАЙЛ = 1 ТАБЛИЦА
       std::string TmpCacheTableName (tb.length()+1, ' ') ;

       for(int i = 0, l = tb.length(); i < l; ++i)
         if (tb[i] <= '9' && tb[i] >= '0')  TmpCacheTableName[i] = tb[i];
         else TmpCacheTableName[i] = ' ';

       if (2!=sscanf(TmpCacheTableName.c_str(),"%d%d",&LstTblID,&ID_profile)) {
          ;
       }

       if (ID_profile<=0 || LstTblID <=0) {
           std::cout <<  "Error! ID_profile=" << ID_profile << " , LstTblID=" << LstTblID  << std::endl;
           break ;
       }

       std::cout <<  " ID_profile = " << ID_profile << " , LstTblID = " << LstTblID  << std::endl;


       // получение наименований колонок из табл
       QuerySql[0]='\0';
       sprintf(QuerySql, "PRAGMA table_info( %s )",tb.c_str());
       ReturnCode = sqlite3_prepare_v2( db_handle, QuerySql , strlen(QuerySql), &ppStmt, NULL );
       if ( ReturnCode == SQLITE_OK )
       {
           while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
           {
               TableNameCols.push_back( (const char *)sqlite3_column_text( ppStmt, 1 ) );
           }
       } else {
           std::cout <<  "PRAGMA table_info error = " << sqlite3_errmsg(db_handle) ;
           sqlite3_finalize( ppStmt );
           break ;
       }
       sqlite3_finalize( ppStmt );


       // (ID, TIME1970, VAL, STATE) values($1, $2, $3, $4)"
       // (ID, TIME1970, VAL, MIN_VAL, MAX_VAL, STATE) values($1, $2, $3, $4, $5, $6)"
       // (ID, TIME1970, TIME_MKS, VAL, STATE) values($1, $2, $3, $4, $5)"

       //  select  ID,TIME1970,TIME_MKS,STATE,VAL,MIN_VAL,MAX_VAL
       std::string QCol = "" ;
       std::string QColPunc = "," ;

       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "ID") != TableNameCols.end() )
          QCol="ID";
       else
          QCol="0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "TIME1970") != TableNameCols.end() )
          QCol=QCol+QColPunc+"TIME1970";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "TIME_MKS") != TableNameCols.end() )
          QCol=QCol+QColPunc+"TIME_MKS";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "STATE") != TableNameCols.end() )
          QCol=QCol+QColPunc+"STATE";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "VAL") != TableNameCols.end() )
          QCol=QCol+QColPunc+"VAL";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MIN_VAL") != TableNameCols.end() )
          QCol=QCol+QColPunc+"MIN_VAL";
       else
          QCol=QCol+QColPunc+"0";
       if ( std::find(TableNameCols.begin(), TableNameCols.end(), "MAX_VAL") != TableNameCols.end() )
          QCol=QCol+QColPunc+"MAX_VAL";
       else
          QCol=QCol+QColPunc+"0";

       std::string sql0 = "SELECT " + QCol + " FROM " + tb ;
       std::string sql1 = "SELECT " + QCol + " FROM " + tb + " WHERE time1970 between ? and ? ORDER BY ID asc" ;
       std::string sql2 = "SELECT " + QCol + " FROM " + tb + " WHERE time1970 between ? and ? and ID>= ? and ID<= ? ORDER BY ID asc" ;
       std::string sql22 = "SELECT " + QCol + " FROM " + tb + " WHERE time1970>= ? and time1970<= ? and ID>= ? and ID<= ? ORDER BY ID asc" ;

       std::cout << "Query command = " << sql0 << std::endl;

       //////////////////////////////////////////////////////////////////////////
       //Read input parameters from the DB
       Logger log;
       log.Info("Reading the DB start");

       int db_err = 0;
       std::vector<CASS_HOST_INFO> listCassHosts;
       CASS_HOST_INFO   cass_host;
       unsigned         port_num = 0;
       char             host_addr[64] = {0};
       char             sql[MAX_QUERY_LEN] = {0};

       //Подключение к БД
       DB_ACCES_INFO *ui = RSDURTGUtils_DBOpenConnectionWithTns(loginDBOracle.c_str(), passwordDBOracle.c_str(), newTNS.c_str(), DBTimeout, log.getPointer());
       if (ui == NULL)
       {
         std::cerr << "Error open connection with DB '" << newTNS << "' with User Login '" << loginDBOracle <<"' and Password '" << passwordDBOracle << "'" << std::endl;
         ++db_err;
       }

       //Чтение информации о хостах кластера Cassandra
       if (0 == db_err)
       {
           std::cout << "Reading Cassandra cluster hosts' addresses:" << std::endl;
           sprintf(sql, SRV_CASSHOSTS_SQL);
           int cn = RSDURTGUtils_DBSQLExec(ui, sql, DBTimeout, log.getPointer());
           if (cn > 0)
           {
               listCassHosts.reserve(cn);
               int i = 0;
               char *psz = NULL;
               while ((psz = RSDURTGUtils_DBFetch(ui)) != NULL)
               {
                   if (sscanf((const char *)psz, SRV_CASSHOSTS_FMT, host_addr, &port_num) != SRV_CASSHOSTS_NUM)
                   {
                       std::cerr << "Error scan DB row" << std::endl;
                       ++db_err;
                   }
                   else
                   {
                       rtrim(host_addr, sizeof(host_addr));
                       strcpy(cass_host.host_addr, host_addr);
                       cass_host.host_port = port_num;
                       listCassHosts.push_back(cass_host);
                       std::cout << "Cassandra host #"<< i++ <<": " << cass_host.host_addr << ":" << cass_host.host_port << std::endl;
                   }
               }
           }
           else
           if (cn < 0)
           {
              std::cerr << "DB error SQL: <" << sql <<  ">, Error message: " << RSDURTGUtils_DBGetError(ui) << std::endl;
              ++db_err;
           }
           RSDURTGUtils_DBDataDispose(ui);
       }//if (0 == db_err)

       RsduCassCluster cass_cluster;
       if (0 == db_err)
       {
           if (listCassHosts.empty()) //Нет информации о хостах Cassandra - серверу некуда подключаться
           {
               std::cerr << "Error: No CASSANDRA hosts!" << std::endl;
               ++db_err;
           }
           else
           try
           {
               cass_cluster.Init(listCassHosts.data(), listCassHosts.size(), log.getPointer(), 0);
               RsduCassSession cass_session(&cass_cluster, CassWriteTimeout);
               std::string versioninfostr;
               stringmap mapCassVersionInfo;
               cass_session.CheckConnection(versioninfostr, mapCassVersionInfo);
               std::cout << "CASSANDRA test connection successful, system info:\n" << versioninfostr.c_str() << std::endl;
           }
           catch (RsduCassException &e)
           {
               std::cerr << "CASSANDRA test connection failed, error: " << e.what() << std::endl;
               ++db_err;
           }
       }

       std::vector<PROFILE_INFO> listProfiles;
       //Чтение информации о таблицах профиля из БД Oracle
       if (0 == db_err)
       {
           //Чтение id_tbllst, id_gtopt, type? из ARC_GINFO
           //--Чтение соответствующего RSDUXXARH
           //Чтение соответствующего %_ARC
           std::cout << "Reading Profile "<< ID_profile <<" info:" << std::endl;
           sprintf(sql, SQLSelectProfileSQLITE, ID_profile, LstTblID); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
           int cn = RSDURTGUtils_DBSQLExec(ui, sql, DBTimeout, log.getPointer());
           if (cn > 0)
           {
               listProfiles.reserve(cn);
               int i = 0;
               char *psz = NULL;
               while ((psz = RSDURTGUtils_DBFetch(ui)) != NULL)
               {
                   PROFILE_INFO profile;
                   if (sscanf((const char *)psz, SQLSelectProfileFMT,
                              &profile.LstTblID,profile.ArcTblName, profile.SchemaName,
                              &profile.Depth, &profile.State, &profile.Interval, &profile.GtypeID) != SQLSelectProfileNUM)
                   {
                       std::cerr << "Error scan DB row" << std::endl;
                       ++db_err;
                   }
                   else
                   {
                       rtrim(profile.ArcTblName, sizeof(profile.ArcTblName));
                       rtrim(profile.SchemaName, sizeof(profile.SchemaName));
                       profile.Depth *= 3600; /* Переводим в секунды глубину записи архивов, заданную в БД (arc_ginfo.depth) в часах */
                       listProfiles.push_back(profile);
                       std::cout << "Profile ID " << ID_profile << " info #" << i++ << ": LstTblID: " << profile.LstTblID << " ArcTblName: " << profile.ArcTblName << " SchemaName: " << profile.SchemaName
                                 << " Depth: " << profile.Depth << "(sec.)/" << profile.Depth/3600 <<"(h.) State: " << profile.State << " Interval: " << profile.Interval << " GtypeID: " << profile.GtypeID << std::endl;
                   }
               }
           }
           else
           if (cn < 0)
           {
               std::cerr << "DB error SQL: <" << sql << ">, Error message: " << RSDURTGUtils_DBGetError(ui) << std::endl;
               ++db_err;
           }
           RSDURTGUtils_DBDataDispose(ui);
       }

       if (0 == db_err)
       {
           if (listProfiles.empty())
           {
               std::cerr << "Error: No information about profile with ID " << ID_profile << std::endl;
               ++db_err;
           }
       }


       size_t totalArchives = 0;

       log.Info("Reading the DB end, db_err = %d", db_err);
       if (db_err != 0)
       {
           std::cerr << "ERROR Initialization! Exiting..." << std::endl;
           RSDURTGUtils_DBClose(ui);
           continue ; // return -1;
       }



       RSDURTGUtils_DBClose(ui); // прочитали все из Oracle



       //////////////////////////////////////////////////////////////////////////
       //Перенос архивов в Cassandra
       log.Info("Starting Data reading and loading to Cassandra");

       auto beginLoad = std::chrono::high_resolution_clock::now();
       try
       {
           RsduCassSession cass_session(&cass_cluster, CassWriteTimeout);
           cass_session.Connect();
           size_t NumProfiles = listProfiles.size();
           size_t iProfile = 0;
           //Обход по профилю
           for (const auto &profile : listProfiles)
           {

               ++iProfile;
               std::cout << "===Proceed profile=== [" << iProfile << "/" << NumProfiles << "] [" << 100 * iProfile / NumProfiles << "%] : ( " << profile.LstTblID << " , " << ID_profile << " )" << std::endl;

               size_t processedArchive = 0;

               //Прогресс
               unsigned progressDot = 0;
               auto start = std::chrono::high_resolution_clock::now();

//============================================================================
               time_t cur_time = RSDURTGUtils_Time70();

               std::cout << "R" << std::flush;

               QuerySql[0]='\0';
               sprintf(QuerySql, "%s",sql1.c_str() );
               if (bParamRange) sprintf(QuerySql, "%s",sql2.c_str() );

               ReturnCode = sqlite3_prepare_v2( db_handle, QuerySql , strlen(QuerySql), &ppStmt, NULL );

               if ( ReturnCode == SQLITE_OK )
               {
                  sqlite3_reset(ppStmt);
                  sqlite3_clear_bindings(ppStmt);

                  ReturnCode = sqlite3_bind_int64(ppStmt, 1, input_start_time);
                  if( ReturnCode != SQLITE_OK ) {
                    std::cout << " sqlite3_bind_int64 error = " << sqlite3_errmsg(db_handle) << std::endl;
                    break;
                  }
                  ReturnCode = sqlite3_bind_int64(ppStmt, 2, input_end_time);
                  if( ReturnCode != SQLITE_OK ) {
                    std::cout << " sqlite3_bind_int64 error = " << sqlite3_errmsg(db_handle) << std::endl;
                    break;
                  }

                  if (bParamRange) {
                    ReturnCode = sqlite3_bind_int(ppStmt, 3, start_id_param);
                    if( ReturnCode != SQLITE_OK ) {
                      std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                      break;
                    }
                    ReturnCode = sqlite3_bind_int(ppStmt, 4, end_id_param);
                    if( ReturnCode != SQLITE_OK ) {
                      std::cout << " sqlite3_bind_int error = " << sqlite3_errmsg(db_handle) << std::endl;
                      break;
                    }
                  }

               } else {
                   std::cout <<  "Prepare to exec = " << sqlite3_errmsg(db_handle) << std::endl;
                   break;
               }



               // int max=2147483646
               //int REC_MAX = (input_end_time-input_start_time)/(profile.Interval ? profile.Interval : 1) * MaxDataCountinResult;
               int REC_MAX = 100 * MaxDataCountinResult; //8640000
               REC_MAX = REC_MAX + 2 ;



               std::vector<ARC_STRUCT> oracle_archive; // Значения текущего считываемого/записываемого архива Oracle
               oracle_archive.reserve(REC_MAX);
               oracle_archive.clear();
               int count_rec = 0 ; // подсчет числа вставленных записей

               int ncolumns = sqlite3_column_count(ppStmt); // число колонок в запросе
               std::cout << "A(REC_MAX=" <<REC_MAX<<", col="<<ncolumns<<")"<< std::flush;
               std::cout << "..prepare.. wait...=>"<< std::flush << std::endl;

               int step_flag = 0 ;

               while ( 1 ) // sqlite3_step( ppStmt ) == SQLITE_ROW       ---- SQLITE_DONE
               {
                   step_flag = sqlite3_step( ppStmt ) ;

                   if (step_flag==SQLITE_ROW) {

                       ++processedArchive;

                       ARC_STRUCT arcstruct;
                       unsigned id = 0, time1970 = 0, time_mks = 0, state = 0;
                       unsigned bval = 0;
                       double   aval = 0, min_val = 0, max_val = 0;
                       db_err = 0;

                       id = sqlite3_column_int(ppStmt, 0);
                       time1970 = sqlite3_column_int64(ppStmt, 1);
                       time_mks = sqlite3_column_int(ppStmt, 2);
                       state = sqlite3_column_int64(ppStmt, 3);
                       if (GLOBAL_TYPE_ANALOG == profile.GtypeID)
                       {
                           // SQLITE_INTEGER  SQLITE_FLOAT  SQLITE_NULL  SQLITE_TEXT
                           switch (sqlite3_column_type(ppStmt, 4)) {
                           case SQLITE_NULL:
                               aval = 0 ;
                               break;
                           default :
                               aval = sqlite3_column_double(ppStmt, 4);
                               break;
                           }
                           arcstruct.val = aval;
                       }
                       else if(GLOBAL_TYPE_BOOL == profile.GtypeID)
                       {
                           // SQLITE_INTEGER  SQLITE_FLOAT  SQLITE_NULL  SQLITE_TEXT
                           switch (sqlite3_column_type(ppStmt, 4)) {
                           case SQLITE_NULL:
                               bval = 0 ;
                               break;
                           default :
                               bval = sqlite3_column_int(ppStmt, 4);
                               break;
                           }
                           arcstruct.val = bval;
                       }
                       min_val = sqlite3_column_double(ppStmt, 5);
                       max_val = sqlite3_column_double(ppStmt, 6);

                       //std::cout << "(" << count_rec << ")= " << id << " , " << time1970 << " , " << time_mks << " , "
                       //            << state << " , " << arcstruct.val << " , " << min_val << " , " << max_val << std::endl;

                       if (id<=0) db_err++ ;

                       if (0 == db_err)
                       {
                           arcstruct.id_tbllst = static_cast<int32_t>(profile.LstTblID);
                           arcstruct.id_ginfo = static_cast<int32_t>(ID_profile);
                           arcstruct.id = static_cast<int32_t>(id);
                           arcstruct.time1970 = static_cast<int64_t>(time1970) * 1000LL + static_cast<int64_t>(time_mks) / 1000LL;
                           arcstruct.state = static_cast<int64_t>(state);
                           arcstruct.min_val = min_val;
                           arcstruct.max_val = max_val;
                           if (profile.Depth)//Если глубина хранения не ноль (0 = хранить неограниченное время)
                           {
                               //Расчёт сдвига для глубины хранения (относительно текущего времени) на случай, если метка времени у данных за прошлое (восстановление данных)
                               cass_int32_t time_diff = static_cast<cass_int32_t>(cur_time) - static_cast<cass_int32_t>(time1970);
                               arcstruct.depth = static_cast<cass_int32_t>(profile.Depth) - time_diff;
                           }

                           oracle_archive.push_back(arcstruct);

                           count_rec ++ ; // подсчет числа вставленных записей

                       }//if (0 == db_err)

                   } // step_flag==SQLITE_ROW

                   if (count_rec==REC_MAX || step_flag!=SQLITE_ROW) {
                       //Вставка в Cassandra
                       //////////////////////////////////////////////////////////////////////////
                       RsduCassPreparedStatement cass_statement(cass_session, "", CassWriteTimeout);
                       cass_statement.PrepareArcInsert(ID_profile);
                       auto finish = std::chrono::high_resolution_clock::now();
                       std::chrono::duration<double> elapsed = finish - start;
                       size_t NumRecords = oracle_archive.size();
                       std::cout << " : " << NumRecords << " rows\t(" << std::setprecision(6) << elapsed.count() << " sec. elapsed), inserting..." << std::flush;
                       size_t toSplitStart = 0, toSplitEnd = 0;
                       uint32_t num_errs = 0;

                       start = std::chrono::high_resolution_clock::now();
                       progressDot = 0;

                       while (toSplitEnd < NumRecords && num_errs < 2 /* max 2 attemps */)
                       {
                           toSplitEnd = toSplitStart + CassBatchSize;
                           if (toSplitEnd > NumRecords)
                               toSplitEnd = NumRecords;
                           std::vector<ARC_STRUCT>::const_iterator first = oracle_archive.begin() + toSplitStart;
                           std::vector<ARC_STRUCT>::const_iterator last = oracle_archive.begin() + toSplitEnd;
                           std::vector<ARC_STRUCT> SplitArcVec(first, last);
                           try
                           {
                               //std::cout << "Prepare to load to Cassandra from " << toSplitStart << " to " << toSplitEnd << " from " << NumRecords << std::endl;
                               cass_statement.InsertArcIntoBatchWithPrepared(SplitArcVec.data(), SplitArcVec.size());
                           }
                           catch (RsduCassException &e)
                           {
                               if (0 == num_errs) //Выводим сообщение только о первой ошибке
                               {
                                   std::stringstream errMessage;
                                   errMessage << "Error: CASSANDRA InsertArcIntoBatchWithPrepared error message: " << e.what() << std::endl;
                                   std::cerr << errMessage.str();
                                   log.Error(errMessage.str().c_str());
                               }
                               ++num_errs;
                           }
                           toSplitStart = toSplitEnd;
                           if (++progressDot % 1000 == 0)
                               std::cout << "." << std::flush;
                       }//while (toSplitEnd < NumRecords && num_errs < 2)

                       cass_statement.FreePrepared();

                       finish = std::chrono::high_resolution_clock::now();
                       elapsed = finish - start;
                       std::cout << " (" << elapsed.count() << " sec. elapsed),\t" << num_errs << " errors" << std::endl;
                       //////////////////////////////////////////////////////////////////////////

                       count_rec = 0 ;
                       oracle_archive.clear();
                   }

                   if (step_flag!=SQLITE_ROW) break ; // выход из цикла

               } // while
               sqlite3_finalize( ppStmt );


           }//for (const auto &profile : listProfiles)
           cass_session.Close();
       }//try
       catch (RsduCassException &e)
       {
           std::cerr << "DoCassLoad: CassException : " << e.what() << std::endl;
       }
       catch (std::exception &e)
       {
           std::cerr << "DoCassLoad: Standard exception : " << e.what() << std::endl;
       }
       log.Info("End loading to Cassandra");

    } // for tb

    sqlite3_close( db_handle );

    std::cout << " .... finished.." << std::endl;

    return 0;
}

