#!/usr/bin/python
import sqlite3
from sys import argv

#script, start_dt, end_dt = argv
script = argv

start_dt = "20230527012500"
end_dt = "20230527013000"


def parse():
    try:
        sqlite_connection = sqlite3.connect('MEAS_ARC_29_70.db')
        cursor = sqlite_connection.cursor()
        sql_base = "SELECT ID, TIME1970, VAL, STATE, MIN_VAL, MAX_VAL, strftime('%Y-%m-%d %H:%M:%S.000+0000', TIME1970, 'unixepoch') from MEAS_ARC_29_70 "

        #sqlite_connection = sqlite3.connect('MEAS_ARC_33_27.db')
        #cursor = sqlite_connection.cursor()
        #sql_base = "SELECT ID, TIME1970, VAL, STATE, strftime('%Y-%m-%d %H:%M:%S.000+0000', TIME1970, 'unixepoch') from MEAS_ARC_33_27"

        sql_where = " WHERE  strftime('%Y%m%d%H%M%S', TIME1970, 'unixepoch') >= '" + start_dt + "' and strftime('%Y%m%d%H%M%S', TIME1970, 'unixepoch') <= '" + end_dt + "'"
        sqlite_select_query = sql_base + sql_where
        print( sqlite_select_query )
        cursor.execute(sqlite_select_query)
        #records = cursor.fetchall()
        #for row in records:
        #    print(29, row[0], row[6].encode('ascii', 'ignore'), row[5], row[4], row[3], row[2])
        #    print(33, row[0], row[4].encode('ascii', 'ignore'), 0, 0, row[3], row[2])
        #with open("MEAS_ARC.csv", "w+") as f:
        cnt = 0
		row = cursor.fetchone()
        while row is not None:
            #print("%d,%d,%s,%f,%f,%ld,%f" % (29, row[0], row[6].encode('ascii', 'ignore'), row[5], row[4], row[3], row[2]),sep='', end='\n', file=sys.stdout, flush=True )
            print(29, row[0], row[6], row[5], row[4], row[3], row[2], sep=',', flush=True)
            
            #print("%d,%d,%s,%f,%f,%ld,%f" % (33, row[0], row[4].encode('ascii', 'ignore'), 0, 0, row[3], row[2]) )
            print(33, row[0], row[4].encode('ascii', 'ignore'), 0, 0, row[3], row[2], sep=',', flush=True)            
            
            #f.write(s)
            row = cursor.fetchone()
            cnt = cnt + 1
            #if (cnt>200) : break ;
        cursor.close()

    except sqlite3.Error as error:
        print("Error SQLite", error)
    finally:
        if sqlite_connection:
            sqlite_connection.close()



parse()

