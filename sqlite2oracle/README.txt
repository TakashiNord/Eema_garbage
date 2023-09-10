# sqlite2oracle
 sqlite2oracle

------------------------------------------------------------------------------------------
 
Restore data from SQLite to Oracle
 
------------------------------------------------------------------------------------------ 

Алгоритм :
        1/ Открываем соединение
        2/ Устанавливаем роли
        3/ Вызываем arc_writer_pkg.prepare_table
        5/  вставляем записи insert.
        6/ Вызываем перенос (arc_writer_pkg.MOVE_DATA) из таблицы стэка
        7/ завершаем 
 
------------------------------------------------------------------------------------------  
 
HOW INSTALL.
Как установить:
1.	Скопировать sqlite2oracle.cpp в /root/LinuxRT/trunk/Others/oracle2cass/linux/src
2.	Скопировать Makefile.sqlite2oracle.in в /root/LinuxRT/trunk/Others/oracle2cass/linux 
3.	Добавить в файл  /root/LinuxRT/trunk/configure
Инфу о новом проекте и где Makefile.sqlite2oracle.in файл
others_names=(
Filegen
mbadmin
inpsendsign
sgtest
sqltest
sysutil
rtquery
rtsetcontr
DebugManage
cid2profile
oracle2cass
sqlite2cass
sqlite2oracle
)

others_makefiles=(
Others/FileGen_Linux/linux/Makefile
Others/mbadmin/linux/Makefile
Others/inpsendsign/linux/Makefile
Others/sgtest/linux/Makefile
Others/sqltest/linux/Makefile
Others/sysutil/linux/Makefile
Others/rtquery/linux/Makefile
Others/rtsetcontr/linux/Makefile
Others/DebugManage/linux/Makefile
Others/cid2profile/linux/Makefile
Others/oracle2cass/linux/Makefile
Others/oracle2cass/linux/Makefile.sqlite2cass
Others/oracle2cass/linux/Makefile.sqlite2oracle
)
4.	Добавить в файл /root/LinuxRT/trunk/Makefile
Новый путь
…..
#SUBDIRS_OTHERS += Others/cid2profile/linux/Makefile
SUBDIRS_OTHERS += Others/oracle2cass/linux/Makefile
SUBDIRS_OTHERS += Others/oracle2cass/linux/Makefile.sqlite2cass
SUBDIRS_OTHERS += Others/oracle2cass/linux/Makefile.sqlite2oracle

5.	Добавить в rsdu.config новый модуль

[modules]
…..
DebugManage=yes
oracle2cass=yes
sqlite2cass=yes
sqlite2oracle=yes
….

6.	Все. Дальше как обычно
make clean
./configure
make

В скрытой папке /root/LinuxRT/trunk/Others/oracle2cass/linux/.build_sqlite2oracle_SUSE/bin
Соберется выполняемый-файл 

------------------