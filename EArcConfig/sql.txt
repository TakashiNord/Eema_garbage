

  Фаза 1.
   отработка логики и интерфейсов


 0) Иерархия

select * from sys_db_part where define_alias like 'DA_SUBSYST'

DA_SUBSYST
ARC_READ
MODEL_SUBSYST

select id,id_parent, name, id_lsttbl 
from sys_tree21 
where id_parent in (
select distinct id_parent from sys_tree21 where id_lsttbl in 
(select id from sys_tbllst where id_type in
(select id from sys_otyp where define_alias like 'LST') 
and id_node in 
(select id from sys_db_part where id_parent in 
(select id from sys_db_part where define_alias like 'MODEL_SUBSYST' or define_alias like 'DA_SUBSYST' ))))

select id, NVL(id_parent,0), name, NVL(id_lsttbl,0) 
from sys_tree21 
start with ID in 
(select distinct id_parent from sys_tree21 where id_lsttbl in 
(select id from sys_tbllst where id_type in
(select id from sys_otyp where define_alias like 'LST') 
and id_node in 
(select id from sys_db_part where id_parent in 
(select id from sys_db_part where define_alias like 'MODEL_SUBSYST')))) connect by prior id=id_parent order by id

select id, NVL(id_parent,0), name, NVL(id_lsttbl,0) 
from sys_tree21 
start with ID in 
(select distinct id_parent from sys_tree21 where id_lsttbl in 
(select id from sys_tbllst where id_type in
(select id from sys_otyp where define_alias like 'LST') 
and id_node in 
(select id from sys_db_part where id_parent in 
(select id from sys_db_part where define_alias like 'DA_SUBSYST')))) connect by prior id=id_parent order by id


 Ограничения

#define ARCH_MIN_DEPTH 24 // Minimal depth. Hours. (Default is 1 day)
#define ARCH_MAX_DEPTH 100000 // Maximal depth. Hours. (Default is about 10 years)

#define ARCH_MIN_PART_DEPTH 10 // Minimal partition depth. Minutes. (Default is 10 minutes)
#define ARCH_MAX_PART_DEPTH 525600 // Maximal partition depth. Minutes. (Default is 1 year)

#define ARCH_MIN_PART_COUNT 1 // Minimal partitions count
#define ARCH_MAX_PART_COUNT 63 // Maximal partitions count
#define ARCH_DEFAULT_PARTITION_COUNT 12 // Default partitions count


 Работа с табл SYS_GTOPT

SELECT 
  SYS_GTOPT.ID      , 
  SYS_GTOPT.NAME    , 
  SYS_GTYP.DEFINE_ALIAS as "DataType",
  SYS_GTOPT.INTERVAL as "Interval (sec)"
  , SYS_ATYP.NAME as "Archive Type"
FROM SYS_GTOPT, SYS_GTYP, SYS_ATYP
WHERE SYS_GTYP.id=SYS_GTOPT.ID_GTYPE
AND SYS_GTOPT.ID_ATYPE=SYS_ATYP.ID


SELECT 
  SYS_GTOPT.ID
  ,SYS_GTOPT.NAME
  ,SYS_GTYP.DEFINE_ALIAS as "DataType"
  ,SYS_GTOPT.INTERVAL as "Interval (sec)"
  ,'0' as "Archive Type"
FROM SYS_GTOPT, SYS_GTYP
WHERE SYS_GTOPT.ID_ATYPE is null and SYS_GTYP.id=SYS_GTOPT.ID_GTYPE
union
SELECT 
  SYS_GTOPT.ID
  ,SYS_GTOPT.NAME
  ,SYS_GTYP.DEFINE_ALIAS as "DataType"
  ,SYS_GTOPT.INTERVAL as "Interval (sec)"
  ,SYS_ATYP.NAME as "Archive Type"
FROM SYS_GTOPT , SYS_ATYP, SYS_GTYP
WHERE SYS_GTOPT.ID_ATYPE=SYS_ATYP.ID  and SYS_GTYP.id=SYS_GTOPT.ID_GTYPE


- Биты масок 

 MASK

1	Запись по изменению	ARC_FTR_WRITE_ON_CHANGE	1
2	Запись экстремумов на интервале	ARC_FTR_WRITE_MINMAX	2
3	Использование партиций для доступа к архивам	ARC_FTR_USE_VIEW	4
4	Тип архива в архивной таблице	ARC_FTR_GTOPT_IN_ARCHIVE	8
5	Обновление данных в архиве	ARC_FTR_ARC_MERGE	16
6	Автоматическое восстановление архива	ARC_FTR_AUTORESTR	32
7	Использовать интерфейс прямой загрузки	ARC_FTR_DPLOAD_ON	64
8	Использование представлений на базовых архивах	ARC_FTR_USE_VIEW_ON_BASE	128
9	Усреднение на границе интервала	ARC_FTR_AVERAGE	256
10	В расчете участвуют величины с учетом исходного знака	ARC_FTR_CALC_SIGN	512
11	В расчете участвуют величины взятые по модулю	ARC_FTR_CALC_ABS	1024
12	В расчете участвуют только положительные величины	ARC_FTR_CALC_INPUT	2048
13	В расчете участвуют только отрицательные величины	ARC_FTR_CALC_OUTPUT	4096
14	Использовать метод трапеций для расчёта интеграла	ARC_FTR_CALC_TRAPEZOID	8192
15	Использование представлений на журналах	ARC_FTR_USE_VIEW_ON_JOUR	16384


 Табл ARC_GINFO

COMMENT ON COLUMN ARC_GINFO.ID IS 'Идентификатор профиля архива';
COMMENT ON COLUMN ARC_GINFO.ID_GTOPT IS 'Тип архива';
COMMENT ON COLUMN ARC_GINFO.ID_TYPE IS 'Ссылка на справочник ARC_TYPE';
COMMENT ON COLUMN ARC_GINFO.DEPTH IS 'Глубина архива в БД архивов, в часах. Значение 0 - архив неограниченной глубины';
COMMENT ON COLUMN ARC_GINFO.DEPTH_LOCAL IS 'Глубина архива в локальной БД, в часах. Значение 0 - архив с неограниченной глубиной';
COMMENT ON COLUMN ARC_GINFO.CACHE_SIZE IS 'Объем данных ("строк"), при котором сервер записи архивов отправляет данные на вставку серверу прямого доступа';
COMMENT ON COLUMN ARC_GINFO.CACHE_TIMEOUT IS 'Промежуток времени (сек), через который сервер записи архивов отправляет данные на вставку серверу прямого доступа';
COMMENT ON COLUMN ARC_GINFO.FLUSH_INTERVAL IS 'Период (сек) чистки устаревших данных. Значение 0 - не исполняется';
COMMENT ON COLUMN ARC_GINFO.RESTORE_INTERVAL IS 'Период (сек) контроля целостности архивных данных. Значение 0 - непрерывное исполнение';
COMMENT ON COLUMN ARC_GINFO.STACK_INTERVAL IS 'Период (сек) разбора таблицы - стека. Значение 0 - не исполняется';
COMMENT ON COLUMN ARC_GINFO.WRITE_MINMAX IS 'Записывать минимальное и максимальное значение на интервале (1 - записывать, 0 - не записывать)';
COMMENT ON COLUMN ARC_GINFO.RESTORE_TIME IS 'Период, по истечении которого прекращаются попытки восстановить архив из внешней подсистемы';
COMMENT ON COLUMN ARC_GINFO.NAME IS 'Наименование профиля архива';
COMMENT ON COLUMN ARC_GINFO.DEPTH_PARTITION IS 'Глубина хранения среза в разделе (в часах). По умолчанию - 3 часа';
COMMENT ON COLUMN ARC_GINFO.RESTORE_TIME_LOCAL IS 'Время, в течение которого возможно восстановление параметра из локального архива';

CREATE TABLE ARC_GINFO
(
  ID                  NUMBER(11),
  ID_GTOPT            NUMBER(11),
  ID_TYPE             NUMBER(11)                DEFAULT 1,
  DEPTH               NUMBER(11)                DEFAULT 168,
  DEPTH_LOCAL         NUMBER(11)                DEFAULT 168,
  CACHE_SIZE          NUMBER(11)                DEFAULT 200000,
  CACHE_TIMEOUT       NUMBER(11)                DEFAULT 5,
  FLUSH_INTERVAL      NUMBER(11)                DEFAULT 86400,
  RESTORE_INTERVAL    NUMBER(11)                DEFAULT 3600,
  STACK_INTERVAL      NUMBER(11)                DEFAULT 60,
  WRITE_MINMAX        NUMBER(11)                DEFAULT 0,
  RESTORE_TIME        NUMBER(11)                DEFAULT 168,
  NAME                VARCHAR2(255 BYTE),
  STATE               NUMBER(11)                DEFAULT 0,
  DEPTH_PARTITION     NUMBER(11)                DEFAULT 3,
  RESTORE_TIME_LOCAL  NUMBER(11)                DEFAULT 0
)


// delete

ARC_SUBSYST_PROFILE
  ID

ARC_SERVICES_TUNE
  ID_SPROFILE = ID

ARC_SERVICES_ACCESS
  ID_SPROFILE=ID
  
  
// create

-- определения id заданной таблицы-списка сбора

select id into nID_TBLLST from RSDUADMIN.SYS_TBLLST where table_name = '&TblLstName';

  pARC_PROFILE := 'MEAS_ARC_' || nID_TBLLST || '_' || nId_ARCGINFO;
>>> Вставка в ARC_SUBSYST_PROFILE нового профиля архивов: '|| pARC_PROFILE);
Insert into RSDUADMIN.ARC_SUBSYST_PROFILE (ID, ID_TBLLST, ID_GINFO, IS_WRITEON, STACK_NAME, LAST_UPDATE, IS_VIEWABLE)
 Values (nID_SPROFILE, nID_TBLLST, nID_ARCGINFO, 0, pARC_PROFILE, 0, 1);


select * from ad_service where define_alias like 'ADV_SRVC_DPLOADADCP_ACCESPORT%' order by id asc

>>> Вставка в ARC_SERVICES_TUNE настроек для записи нового профиля архивов:');

  select id from ad_service where define_alias like 'ADV_SRVC_DPLOADADCP_ACCESPORT%' order by id asc
  nID_PRIORITY := nID_PRIORITY + 1;

  Insert into RSDUADMIN.ARC_SERVICES_TUNE(ID_SPROFILE, ID_SERVICE, PRIORITY)
           Values (nID_SPROFILE, rec.id, nID_PRIORITY);
  end loop;

-- Триггер для автоматического формирования имени табл-стека для профиля

     SELECT upper(table_name) INTO vArcDescName
       FROM sys_tbllst lst, sys_tbllnk lnk, sys_otyp t
      WHERE lnk.id_lsttbl = :NEW.id_tbllst
        AND lnk.id_dsttbl = lst.ID
        AND lst.id_type = t.ID
        AND t.define_alias LIKE 'ARH';

   :NEW.Stack_Name := vArcDescName || '_' || :NEW.id_tbllst|| '_' || :NEW.id_ginfo;

UPPER(TABLE_NAME) ID_LSTTBL

CALC_ARC  1140
DG_ARC  1320
MEAS_ARC  2001
MEAS_ARC  2002
MEAS_ARC  2003
DA_ARC  2005
DA_ARC  2006
DA_ARC  2007
DA_ARC  2008
DA_ARC  2009
DA_ARC  1000443
DA_ARC  1000483
MEAS_ARC  1000522
MEAS_ARC  1000532
EA_ARC  64
MEAS_ARC  116
REG_ARC 843
EA_ARC  881
DA_ARC  893
DA_ARC  896
MEAS_ARC  29
MEAS_ARC  33
MEAS_ARC  35
DA_ARC  54
DA_ARC  57
DA_ARC  59



SELECT 
   ad_pinfo.portnumber,ad_pinfo.id_proto,ad_sinfo.id_lsttbl,ad_list.id_type,
   sys_otyp.alias,ad_service.NAME,ad_service.define_alias 
      FROM ad_dir,
           ad_dir dir1,
           ad_list,
           ad_pinfo,
           ad_ncard,
           ad_sinfo,
           sys_otyp,
           ad_service,
           ad_hosts
     WHERE ad_sinfo.id_server_node = ad_dir.ID
       AND ad_list.id_node = ad_dir.ID
       AND ad_pinfo.id_param = ad_list.ID
       AND ad_pinfo.id_intrface_node = ad_ncard.id_node
       AND ad_list.id_type = sys_otyp.ID
       AND ad_pinfo.portnumber = ad_service.ID
       AND ad_pinfo.id_intrface_node = dir1.ID
       AND dir1.id_parent = ad_hosts.id_host_node
       AND ad_pinfo.id_proto > 2
       AND ad_pinfo.id_proto <> 9
       AND ad_dir.ID IN (SELECT ad_dir.ID FROM ad_dir WHERE ad_dir.id_type > 1000)
	   AND ad_sinfo.ID_LSTTBL=
    UNION
    SELECT 
      ad_pinfo.portnumber,ad_pinfo.id_proto,ad_sinfo.id_lsttbl,ad_list.id_type,
      sys_otyp.alias,ad_service.NAME,ad_service.define_alias 
      FROM ad_dir,
           ad_dir dir1,
           ad_list,
           ad_pinfo,
           ad_ipinfo,
           ad_sinfo,
           sys_otyp,
           ad_service,
           ad_hosts
     WHERE ad_sinfo.id_server_node = ad_dir.ID
       AND ad_list.id_node = ad_dir.ID
       AND ad_pinfo.id_param = ad_list.ID
       AND ad_pinfo.id_intrface_node = ad_ipinfo.id_node
       AND ad_list.id_type = sys_otyp.ID
       AND ad_pinfo.portnumber = ad_service.ID
       AND ad_pinfo.id_intrface_node = dir1.ID
       AND dir1.id_parent = ad_hosts.id_host_node
       AND ((ad_pinfo.id_proto <= 2) OR (ad_pinfo.id_proto = 9))
       AND ad_dir.ID IN (SELECT ad_dir.ID  FROM ad_dir WHERE ad_dir.id_type > 1000)
	   AND ad_sinfo.ID_LSTTBL=




 1\   SELECT upper(lst.TABLE_NAME) FROM sys_tbllst lst WHERE lst.ID  =  ======29

DA_V_LST_      ADV_SRVC_DCSOICTCP_ACCESSPORT  ADV_SRVC_DCSOIC_ACCESPORT
PHREG_LIST_V   ADV_SRVC_PHROIC_ACCESPORT%
ELREG_LIST_V   ADV_SRVC_ELROIC_ACCESPORT%

PSWT_LIST_V    ADV_SRVC_PWSOIC_ACCESPORT
AUTO_LIST_V    ADV_SRVC_SSWOIC_ACCESPORT      dpload --
EA_CHANNELS    --------- ADV_SRVC_RDAADCP_ACCESPORT
EA_V_CONSUMER_POINTS  -----------
CALC_LIST       --------
DG_LIST         ADV_SRVC_RDAADCP_ACCESPORT   dpload --
EXDATA_LIST_V   ADV_SRVC_RDAADCP_ACCESPORT   dpload --


if DA_V_LST_
  select * from ad_service where define_alias like 'ADV_SRVC_DCSOICTCP_ACCESSPORT%' order by id asc
ADV_SRVC_RDAADCP_ACCESPORT++++


-+ADV_SRVC_RDAOIC_ACCESPORT

-ADV_SRVC_RDAELOIC_ACCESPORT
-ADV_SRVC_RDAPHOIC_ACCESPORT
-ADV_SRVC_RDAPSOIC_ACCESPORT
-ADV_SRVC_RDADAOIC_ACCESPORT
-ADV_SRVC_RDAEAOIC_ACCESPORT

-- Вставка в ARC_SERVICES_ACCESS настроек для чтения нового профиля архивов:');
for rec in (
     select id from ad_service where define_alias like 'ADV_SRVC_PHROIC_ACCESPORT%' order by id asc
 )
        Insert into RSDUADMIN.ARC_SERVICES_ACCESS(ID_SPROFILE, ID_SERVICE, RETRO_DEPTH)
           Values (nID_SPROFILE, rec.id, 86400);
  end loop;
