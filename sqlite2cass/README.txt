# sqlite2cass
 sqlite2cass
 
------------------------------------------------------------------------------------------
 
Restore data from SQLite to Cassandra 
 
------------------------------------------------------------------------------------------ 

HOW INSTALL.

Как установить:
1.	Скопировать sqlite2cass.cpp в /root/LinuxRT/trunk/Others/oracle2cass/linux/src
2.	Скопировать Makefile.sqlite2cass.in в /root/LinuxRT/trunk/Others/oracle2cass/linux 
3.	Добавить в файл  /root/LinuxRT/trunk/configure
Инфу о новом проекте и где Makefile.sqlite2cass.in файл
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
)
4.	Добавить в файл /root/LinuxRT/trunk/Makefile
Новый путь
…..
#SUBDIRS_OTHERS += Others/cid2profile/linux/Makefile
SUBDIRS_OTHERS += Others/oracle2cass/linux/Makefile
SUBDIRS_OTHERS += Others/oracle2cass/linux/Makefile.sqlite2cass

5.	Добавить в rsdu.config новый модуль

[modules]
…..
DebugManage=yes
oracle2cass=yes
sqlite2cass=yes
….

6.	Все. Дальше как обычно
make clean
./configure
make

В скрытой папке /root/LinuxRT/trunk/Others/oracle2cass/linux/.build_sqlite2cass_SUSE/bin
Соберется выполняемый-файл 

------------------

Включение индексации:
 CREATE INDEX if not exists ID_TIME1970_INDX ON MEAS_ARC_29_70 (ID ASC, TIME1970 ASC);
 
Импорт csv
cqlsh 172.12.0.113 9042
use arc ;
DESC TABLES ;
COPY arc.profile_70 FROM '/root/MEAS_ARC_29_70.csv' WITH HEADER=TRUE

NOTE:

Imports data from a comma-separated values (CSV) file or a delimited text file into an existing table. 
Each line in the source file is imported as a row. 
All rows in the dataset must contain the same number of fields and have values in the PRIMARY KEY fields.

The process verifies the PRIMARY KEY and updates existing records. 
If HEADER = false and no column names are specified, the fields are imported in deterministic order. 
When HEADER = true, the first row of a file is a header row.
Note: Only use COPY FROM to import datasets that have less than two million rows. 
To import large datasets, use sstableloader.