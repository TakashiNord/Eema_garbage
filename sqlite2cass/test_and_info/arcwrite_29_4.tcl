
#
package require tclodbc
package require sqlite3

# ===============================================
# option

# аутенфикация для Oracle
 set tns "rsdu2"
 set usr "rsduadmin" ; #  admin  nov_ema
 set pwd "passme" ; # passme  qwertyqaz

# файл SQLite
 set sqlname "D:\\MEAS_ARC_29_4.db"

# Запросы SQLite
# с 27 по 29 мая
 set strSQL1 "SELECT ID,TIME1970,VAL,STATE FROM MEAS_ARC_29_4 WHERE TIME1970>=1685125590 AND TIME1970<=1685471190 ORDER BY ID asc"
# 01 по 05 июня
# set strSQL1 "SELECT ID,TIME1970,VAL,STATE FROM MEAS_ARC_29_4 WHERE TIME1970>=1685525190 AND TIME1970<=1686043590 ORDER BY ID asc"

# время хранения в часах профиля в Oracle
 set profile_Depth 43848 ;

# шаблон строки для вставки в Cassandra
 set sout "INSERT INTO arc.profile_4  (id_tbllst,id,time1970,max_val,min_val,state,val) values (29, %s , %s  , 0.0 , 0.0 , %s , %s ) IF NOT EXISTS USING TTL %s ;"

# SELECT id_tbllst, id, time1970, max_val, min_val, state, val, TTL(state) FROM arc.profile_4 where id_tbllst=29 and id=10016
# id_tbllst int, id int, time1970 timestamp, val double, state bigint, min_val double, max_val double,

# ===============================================


proc Cass { } {
  global tns usr pwd

# Устанавливаем соединение к БД Oracle
  #database db2 $tns $usr $pwd
  #db2 set autocommit off


  global sqlname
  global strSQL1
  global profile_Depth
  global sout

  set profile_Depth [ expr $profile_Depth * 60 * 60 ] ; # перевод в секунды

# открываем или создаем бд SQLite
  sqlite3 db1 $sqlname ; # associate the SQLite database with the object

  db1 eval {PRAGMA synchronous=OFF}
  db1 eval {PRAGMA journal_mode=OFF}

# ===============================================
  # формируем строки, копируем данные - последовательно.
  set cnt 0 ; # количество

  set cur_time [ clock seconds ] ; # текущее время для расчета TTL

  LogWrite "-- BEGIN BATCH"

  db1 eval $strSQL1 values {
    set id $values(ID)
    set time1970 $values(TIME1970)
    set state $values(STATE)
    set val $values(VAL)

    set time_diff [ expr $cur_time - $time1970 ] ;
    set depth  [ expr $profile_Depth - $time_diff ] ; # TTL

    set time1970 [ expr  int($time1970)*1000 ]

    set s2 [ format $sout $id $time1970 $state $val $depth ]
    LogWrite $s2

    incr cnt ;
    puts ""
  }

  LogWrite "-- APPLY BATCH;"

# ===============================================
  #db1 eval {VACUUM} ;
  db1 close ; # Закрываем соединение к БД SQLite

# Закрываем соединение к БД Oracle
#  db2 commit
#  db2 disconnect

}

proc LogWrite  { s } {
  global rf
  if {![info exists rf]} { return }
  if {$rf==""} { return }
  puts $rf $s
}

proc main {  } {
  global rf

  set t1 [ clock format [ clock seconds ] -format "%Y:%m:%d %H:%M:%S" ]
  puts "\nstart = $t1\n"

# выходной файл или лог
  set tf [ clock format [ clock seconds ] -format "%Y%m%d_%H%M%S" ]
  set lgname arcwrite_${tf}.cql
  global sqlname
  if {[info exists sqlname]} {
    set tclname [info script]
    set lgname [file rootname $sqlname ]_${tf}.cql
  }
  set rf [ open $lgname "w+"  ]
  #fconfigure $rf -buffering none

  Cass

  close $rf

  set t1 [ clock format [ clock seconds ] -format "%Y:%m:%d %H:%M:%S" ]
  puts "\nend = $t1\n"

  return (0)
}


main




