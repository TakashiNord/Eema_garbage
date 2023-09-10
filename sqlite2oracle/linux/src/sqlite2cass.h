#pragma once

// Запрос информации о профиле с заданным ID
constexpr char SQLSelectProfile[] = 
"SELECT y.ID id_tbllst, x.table_name arc_tblname, sch.SCHEMA_NAME arc_schema_name, arc_ginfo.DEPTH, arc_ginfo.STATE, sys_gtopt.INTERVAL, SYS_GTOPT.ID_GTYPE "
"   FROM sys_tbllst x, sys_tbllst y, sys_tbllnk st, sys_otyp ot, arc_db_schema sch, arc_services_info si, arc_subsyst_profile, arc_ginfo, sys_gtopt "
"  WHERE y.ID = st.id_lsttbl "
"    AND x.ID = st.id_dsttbl "
"    AND x.id_type = ot.ID "
"    AND ot.define_alias = 'ARH'"
"    AND si.ID_DB_SCHEMA = sch.ID "
"    AND si.ID_LSTTBL = y.ID "
"    AND arc_subsyst_profile.ID_TBLLST = y.ID "
"    AND arc_subsyst_profile.ID_GINFO = arc_ginfo.ID "
"    AND sys_gtopt.ID = arc_ginfo.ID_GTOPT "
"    AND arc_ginfo.ID = %d "
" ORDER BY y.ID ";
constexpr char SQLSelectProfileFMT[] = "%u %63c %63c %u %u %u %u";
constexpr unsigned SQLSelectProfileNUM = 7;
// Информация об архивных таблицах
typedef struct _ARCHIVE_INFO
{
    unsigned ParamID{ 0 };
    char TblName[MAX_ALIAS_FIELD_LEN] = { 0 };
} ARCHIVE_INFO;

// Информация о профиле
typedef struct _PROFILE_INFO
{
    unsigned LstTblID{ 0 };
    char ArcTblName[MAX_ALIAS_FIELD_LEN] = { 0 };
    char SchemaName[MAX_ALIAS_FIELD_LEN] = { 0 };
    unsigned Depth{ 0 };
    unsigned State{ 0 };
    unsigned Interval{ 0 };
    unsigned GtypeID{ 0 };
    std::vector<ARCHIVE_INFO> listArchives;
} PROFILE_INFO;

// Запрос списка архивных таблиц
constexpr char SQLSelectArcTables[] =
"SELECT id_param, retfname "
"   FROM %s, ALL_TABLES "
"  WHERE id_ginfo = %u "
"  AND upper(%s.RETFNAME) = upper(ALL_TABLES.TABLE_NAME) "
"  AND upper(ALL_TABLES.OWNER) = upper('%s') "
" ORDER BY id_param ASC";
constexpr char SQLSelectArcTablesRangeParams[] =
"SELECT id_param, retfname "
"   FROM %s, ALL_TABLES "
"  WHERE id_ginfo = %u "
"  AND upper(%s.RETFNAME) = upper(ALL_TABLES.TABLE_NAME) "
"  AND upper(ALL_TABLES.OWNER) = upper('%s') "
"  AND id_param >= %u "
"  AND id_param <= %u "
" ORDER BY id_param ASC";
constexpr char SQLSelectArcTablesFMT[] = "%u %63c";
constexpr unsigned SQLSelectArcTablesNUM = 2;

// Запросы из сервера rdarchd по чтению различных видов архивов:
#define RDA_ARCROWS_SQL     "select time1970,0,state,val,0,0 from %s where time1970 between %u and %u"
// Архив по изменению (с мкс.)
#define RDA_ARCROWS_CHANGE_SQL "select time1970,time_mks,state,val,0,0 from %s where time1970 between %u and %u"
// Архив с min_val и max_val (только периодические)
#define RDA_ARCROWS_MINMAX_SQL "select time1970,0,state,val,min_val,max_val from %s where time1970 between %u and %u"
// Общий запрос для обычных архивов и по изменению после унификации функции ARC_GET_INSTANT (DBUpdate 2017_06_21_ARC_GET_INSTANT_TIME_MKS)
#define RDA_ARCROWS_INST_SQL   "select time1970,time_mks,state,val,0,0 FROM table(ARC_GET_INSTANT(%u, %u, %u, %u, %u))"

#define RDA_ADD_ORDER_SQL   " order by time1970"
#define RDA_ADD_ORDER_MKS_SQL " ,time_mks"
#define RDA_ADD_TYPE_SQL    " and id_gtopt=%u"

#define RDA_ARCROW_AFMT     "%u %u %u %lf %lf %lf"
#define RDA_ARCROW_BFMT     "%u %u %u %u %lf %lf"
#define RDA_ARCROW_NUM      6


constexpr size_t MaxDataCountinResult = 86400; // Maximum count of data in the result. (24*60*60 sec = a day)
constexpr unsigned DBTimeout = 6000;           // DB timeout in 10msec.

constexpr unsigned CassWriteTimeout = 30000000;// Таймаут для вставки в Cassandra по умолчанию (мкс.)
constexpr unsigned CassBatchSize = 45;         // Объём пакета при пакетной (batch) вставки в Cassandra
