// sqltest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "string.h"

//SOURCE=.\sqlite\sqlite3.h
#pragma comment(lib, "sqlite3.lib")

#include <sqlite3.h>

sqlite3* db;
    sqlite3_stmt  *ppStmt = NULL;
    int ReturnCode = 0 ;
    char *pErrMsg = 0;
	char QueryMsg[1024];

int main(int argc, char* argv[])
{
	printf("Hello World!\n");

// sqlite3_open("D:\\MEAS_ARC_29_70.db", &db)
  if (sqlite3_open_v2("D:\\MEAS_ARC_29_70.db", &db, SQLITE_OPEN_READONLY, NULL) != SQLITE_OK) {
	  puts( "Could not open database.\n" );
    return 1;
  }

  sqlite3_busy_timeout(db,1000);
  
/*
  sqlite3_column_blob	>	BLOB result
sqlite3_column_double	>	REAL result
sqlite3_column_int	>	32-bit INTEGER result
sqlite3_column_int64	>	64-bit INTEGER result
sqlite3_column_text	>	UTF-8 TEXT result
sqlite3_column_text16	>	UTF-16 TEXT result
sqlite3_column_value	>	The result as an unprotected sqlite3_value object.
 	 	 
sqlite3_column_bytes	>	Size of a BLOB or a UTF-8 TEXT result in bytes
sqlite3_column_bytes16   	>  	Size of UTF-16 TEXT in bytes
sqlite3_column_type	>	Default datatype of the result 
*/
  
  sqlite3_exec(db, "PRAGMA synchronous=OFF", NULL, NULL, NULL);

    sprintf(QueryMsg, "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name");
    ReturnCode = sqlite3_prepare( db, QueryMsg , -1, &ppStmt, 0 );
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


QueryMsg[0]='\0';
    sprintf(QueryMsg, "Select count(*) from %s ","MEAS_ARC_29_70");
    ReturnCode = sqlite3_prepare_v2( db, QueryMsg , -1, &ppStmt, 0 );
    if ( ReturnCode == SQLITE_OK )
    {
        //ReturnCode = sqlite3_bind_text(ppStmt, 1, "MEAS_ARC_33_27", -1, SQLITE_STATIC );
        //if ( ReturnCode != SQLITE_OK ) break ;
        while ( sqlite3_step( ppStmt ) == SQLITE_ROW )
        {
           printf( " Count = %s \n" , (char *)sqlite3_column_text( ppStmt, 0 ) ) ;
        }
    } else {
		printf(  " count find error = %s" , sqlite3_errmsg(db));
        sqlite3_free(pErrMsg);
        pErrMsg = NULL;
        sqlite3_finalize( ppStmt );
        sqlite3_close( db );
        return (0);
    }
    sqlite3_finalize( ppStmt );


  //Records records = select_stmt("SELECT * FROM test");
  sqlite3_close(db);

 // for (auto& record : records) {
    // do something with your records
 // }


	return 0;
}

