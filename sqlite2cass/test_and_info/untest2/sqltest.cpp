// sqltest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "string.h"
#include "time.h"
#include "stdlib.h"


#pragma comment(lib, "sqlite3.lib")
#include <sqlite3.h>


static int callback(void *count, int argc, char **argv, char **azColName) {
    int *c = (int *)count;
    *c = atoi(argv[0]);
    return 0;
}

sqlite3* db;
sqlite3_stmt  *ppStmt = NULL;
int ReturnCode = 0 ;
char *pErrMsg = 0;
char QueryMsg[1024];
size_t MaxDataCountinResult = 86400;


int main(int argc, char* argv[])
{
  printf("Hello World!\n");

  char *zErrMsg = 0;
  int rc;
  int count = 0;

// sqlite3_open("D:\\MEAS_ARC_29_70.db", &db)
  if (sqlite3_open_v2("D:\\MEAS_ARC_29_70.db", &db, SQLITE_OPEN_READONLY, NULL) != SQLITE_OK) {
    puts( "Could not open database.\n" );
    fprintf(stderr, "Can't open database: %s\n", sqlite3_errmsg(db));
    return 1;
  }

  sqlite3_busy_timeout(db,1000);

/*
  sqlite3_column_blob > BLOB result
sqlite3_column_double > REAL result
sqlite3_column_int  > 32-bit INTEGER result
sqlite3_column_int64  > 64-bit INTEGER result
sqlite3_column_text > UTF-8 TEXT result
sqlite3_column_text16 > UTF-16 TEXT result
sqlite3_column_value  > The result as an unprotected sqlite3_value object.

sqlite3_column_bytes  > Size of a BLOB or a UTF-8 TEXT result in bytes
sqlite3_column_bytes16    >   Size of UTF-16 TEXT in bytes
sqlite3_column_type > Default datatype of the result
*/

  sqlite3_exec(db, "PRAGMA synchronous=OFF", NULL, NULL, NULL);
  sqlite3_exec(db, "PRAGMA journal_mode = OFF", NULL, NULL, NULL);

  sprintf(QueryMsg, "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name");
  ReturnCode = sqlite3_prepare_v2( db, QueryMsg , -1, &ppStmt, 0 );
  if ( ReturnCode == SQLITE_OK )
  {
      // get the first row of the result set
      while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
      {
          puts( (char *)sqlite3_column_text( ppStmt, 0 ) );
      }
  } else {

      printf(  "Get TableName from sqlite_master find error = %s" , sqlite3_errmsg(db));
      sqlite3_free(pErrMsg);
      pErrMsg = NULL;
      sqlite3_finalize( ppStmt );
      sqlite3_close( db );
      return (1);
  }
  // close the statement
  sqlite3_finalize( ppStmt );

  QueryMsg[0]='\0';
  sprintf(QueryMsg, "PRAGMA table_info( %s )","MEAS_ARC_29_70");
  ReturnCode = sqlite3_prepare_v2( db, QueryMsg , -1, &ppStmt, 0 );
  if ( ReturnCode == SQLITE_OK )
  {
      //ReturnCode = sqlite3_bind_text(ppStmt, 1, "MEAS_ARC_33_27", -1, SQLITE_STATIC );
      //if ( ReturnCode != SQLITE_OK ) break ;
      while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
      {
         printf( "%s , %s \n" , (char *)sqlite3_column_text( ppStmt, 0 ) ,
     (char *)sqlite3_column_text( ppStmt, 1 ) ) ;
      }
  } else {
  printf(  " find error = %s" , sqlite3_errmsg(db));
      sqlite3_free(pErrMsg);
      pErrMsg = NULL;
      sqlite3_finalize( ppStmt );
      sqlite3_close( db );
      return (0);
  }
  sqlite3_finalize( ppStmt );

  time_t ltime;
  time(&ltime);
  printf("0=%s \n",ctime(&ltime) );


  time_t input_start_time = time(NULL)-3272593; //convert_input_datetime("27052023010101");

  time_t tSplitDateFrom = input_start_time;
  time_t tSplitDateTo = tSplitDateFrom + 300 * MaxDataCountinResult;

  //printf("%s \n",ctime((const time_t *)tSplitDateFrom));
  printf("%u \n",(unsigned)tSplitDateFrom);


  char sql1[256] = "SELECT count(ID) FROM MEAS_ARC_29_70 WHERE TIME1970>=? and TIME1970<=? ;" ;

  ReturnCode = sqlite3_prepare_v2( db, sql1 , strlen(sql1), &ppStmt, NULL );

  if ( ReturnCode == SQLITE_OK )
  {
     ReturnCode = sqlite3_bind_int(ppStmt, 1, (unsigned)tSplitDateFrom);
     //if( ReturnCode != SQLITE_OK ) break;
     ReturnCode = sqlite3_bind_int(ppStmt, 2, (unsigned)tSplitDateTo);
     //if( ReturnCode != SQLITE_OK ) break;

  } else {
     puts("sqlite3_prepare_v2");
     printf(  " count find error = %s" , sqlite3_errmsg(db));
               sqlite3_free(pErrMsg);
               pErrMsg = NULL;
   }

  while ( sqlite3_step( ppStmt ) == SQLITE_ROW ) // SQLITE_DONE
  {
    printf(" -- cnt=%s \n",(char*)sqlite3_column_text(ppStmt, 0));
  }//while

  sqlite3_finalize( ppStmt );

  time_t ltime4;
  time(&ltime4);
  printf("144=%s \n",ctime(&ltime4) );

  char sql2[256] = "SELECT ID , TIME1970, VAL, STATE FROM MEAS_ARC_29_70 WHERE TIME1970>=? and TIME1970<=? ;" ;
  ReturnCode = sqlite3_prepare_v2( db, sql2 , strlen(sql2), &ppStmt, NULL );

  if ( ReturnCode == SQLITE_OK )
  {
     ReturnCode = sqlite3_bind_int(ppStmt, 1, (unsigned)tSplitDateFrom);
     //if( ReturnCode != SQLITE_OK ) break;
     ReturnCode = sqlite3_bind_int(ppStmt, 2, (unsigned)tSplitDateTo);
     //if( ReturnCode != SQLITE_OK ) break;
  } else {
     puts("sqlite3_prepare_v2");
     printf(  " count find error = %s" , sqlite3_errmsg(db));
               sqlite3_free(pErrMsg);
               pErrMsg = NULL;
   }

  while ( sqlite3_step( ppStmt ) == SQLITE_ROW ) // SQLITE_DONE
  {
    printf(" -- id=%s %s %s %s \n",(char*)sqlite3_column_text(ppStmt, 0),
             (char*)sqlite3_column_text(ppStmt, 1) ,
             ( char*)sqlite3_column_text(ppStmt, 2),
             ( char*)sqlite3_column_text(ppStmt, 3) );
  }//while
  sqlite3_finalize( ppStmt );

  time_t ltime1;
  time(&ltime1);
  printf("1=%s \n",ctime(&ltime1) );

  rc = sqlite3_exec(db, "SELECT count(ID) FROM MEAS_ARC_29_70", callback, &count, &zErrMsg);
  if (rc != SQLITE_OK) {
       fprintf(stderr, "SQL error: %s\n", zErrMsg);
       sqlite3_free(zErrMsg);
  } else {
       printf("count: %d\n", count);
  }

  time_t ltime2;
  time(&ltime2);
  printf("2=%s \n",ctime(&ltime2) );

  //Records records = select_stmt("SELECT * FROM test");
  sqlite3_close(db);

  return 0;
}

