

1. Обновление справочников, вопрос сильно затянулся.

2. От  СО "Тип (марка) должна быть описана только у бака трансформатора (TransformerTank.Asset.ProductAssetModel). 
Необходимо убрать тип (марку) у имущественного объекта трансформатора (AssetContainer.ProductAssetModel)"

3. От СО "У объекта класса Asset заполнено свойство ДатыЭтаповВвода (inUseDate). 
Информация должна быть записана в свойство ДатыЖизненногоЦикла (lifeCycleDate)." (Дата ввода в эксплуатацию")


===================================================================== SQLITE
CREATE TABLE IF NOT EXISTS PSRType 
(
    ABOUT TEXT, NAME TEXT, AssetTypes TEXT
);

CREATE TABLE IF NOT EXISTS ProductAssetModel
(
    ABOUT TEXT, Manufacturer TEXT, NAME TEXT, AssetTypes TEXT
);
CREATE TABLE IF NOT EXISTS AssetDataSource  
(
    ABOUT TEXT, NAME TEXT, OrganisationRoleObjects TEXT
);
CREATE TABLE IF NOT EXISTS AssetOwner
( 
  ABOUT TEXT, NAME TEXT, OrganisationRoleObjects TEXT 
);
CREATE TABLE IF NOT EXISTS AssetType
( 
  ABOUT TEXT, NAME TEXT, AssetTypePSRTypes TEXT
);  
CREATE TABLE IF NOT EXISTS Manufacturer
(
  ABOUT TEXT,NAME TEXT,OrganisationRoleOrganisation TEXT
); 
CREATE TABLE IF NOT EXISTS Organisation
(
  ABOUT TEXT,NAME TEXT,oGRN TEXT,ChildOrganisations TEXT,Roles TEXT
);  
CREATE TABLE IF NOT EXISTS OrganisationRole
(
  ABOUT TEXT,NAME TEXT,OrganisationRoleObjects TEXT
);
CREATE TABLE IF NOT EXISTS OperationalLimitType
( 
  ABOUT TEXT,NAME TEXT,acceptableDuration TEXT,direction TEXT
);
CREATE TABLE IF NOT EXISTS OverheadWireInfo
( 
  ABOUT TEXT,NAME TEXT,sizeDescription TEXT,ratedCurrent TEXT,rDC20 TEXT,material TEXT,radius TEXT
);
CREATE TABLE IF NOT EXISTS TapeShieldCableInfo
(  
  ABOUT TEXT,NAME TEXT,sizeDescription TEXT,material TEXT,radius TEXT,diameterOverJacket TEXT,diameterOverCore TEXT, diameterOverInsulation TEXT,tapeThickness TEXT,sheathThickness TEXT,constructionKind TEXT,outerJacketKind TEXT, insulationEr TEXT,insulationErShield TEXT,shieldMaterial TEXT,shieldIsTransposed TEXT,shieldCrossSection TEXT, shieldGrounding TEXT,crossSection TEXT,underShieldScreenThickness TEXT,radialMoistureBarrierThicknes TEXT
) ;

CREATE TABLE IF NOT EXISTS ConcentricNeutralCableInfo 
(
 ABOUT TEXT,NAME TEXT,sizeDescription TEXT,material TEXT,radius TEXT,diameterOverJacket TEXT,diameterOverCore TEXT, diameterOverInsulation TEXT,diameterOverNeutral TEXT,tapeThickness TEXT,sheathThickness TEXT,constructionKind TEXT,outerJacketKind TEXT, insulationEr TEXT,insulationErShield TEXT,shieldMaterial TEXT,shieldIsTransposed TEXT,shieldCrossSection TEXT, shieldGrounding TEXT,crossSection TEXT,underShieldScreenThickness TEXT,radialMoistureBarrierThicknes TEXT 
);  



=====================================================================

1) Организации-tree.xml
  = 
CREATE TABLE RSDUADMIN.AST_ORG
(
  ID          NUMBER(11),
  GUID        RAW(16),
  ID_PARENT   NUMBER(11),
  ID_TYPE     NUMBER(11) CONSTRAINT AST_ORG_ID_TYPE_NN NOT NULL,
  NAME        VARCHAR2(255 CHAR),
  ALIAS       VARCHAR2(255 CHAR),
  ID_FILEWAV  NUMBER(11),
  DATE_MOD    NUMBER(11)
)

GUID = rdf:about
NAME = cim:IdentifiedObject.name

<cim:Organisation rdf:about="#_5e0ff38c-aea1-4769-9a72-9cb45f812946">
 
 <cim:Organisation.Roles rdf:resource="#_15d4e6b2-8a53-4c49-bae9-c585be4e779e"/>
 
 <cim:IdentifiedObject.name>Энергосила</cim:IdentifiedObject.name>
</cim:Organisation>

2507	20F67C618FCD4B9E9C54FC53270D8526	2	3	НПП "Август"	НПП "Август"		1713146400





2) Организации-производители оборудования-tree.xml



CREATE TABLE RSDUADMIN.OBJ_MANUFACTS
(
  ID      NUMBER(11),
  GUID    RAW(16) CONSTRAINT OBJ_MANUFACTS_GUID_NN NOT NULL,
  ID_ORG  NUMBER(11),
  NAME    VARCHAR2(255 BYTE),
  ALIAS   VARCHAR2(255 BYTE)
)

COMMENT ON TABLE RSDUADMIN.OBJ_MANUFACTS IS 'Производители оборудования';

COMMENT ON COLUMN RSDUADMIN.OBJ_MANUFACTS.ID IS 'Идентификатор производителя оборудования';

COMMENT ON COLUMN RSDUADMIN.OBJ_MANUFACTS.GUID IS 'Глобальный идентификатор производителя оборудования';

COMMENT ON COLUMN RSDUADMIN.OBJ_MANUFACTS.ID_ORG IS 'Ссылка на организацию производителя модели';

COMMENT ON COLUMN RSDUADMIN.OBJ_MANUFACTS.NAME IS 'Наименование производителя оборудования';

COMMENT ON COLUMN RSDUADMIN.OBJ_MANUFACTS.ALIAS IS 'Краткое наименование производителя оборудования';










3) Типы эксплуатационных ограничений_пределов-tree.xml

CREATE TABLE RSDUADMIN.OBJ_LIMIT_TYPE
(
  ID         NUMBER(11),
  GUID       RAW(16) CONSTRAINT OBJ_LIMIT_TYPE_GUID_NN NOT NULL,
  DIRECTION  NUMBER(11),
  DURATION   NUMBER,
  NAME       VARCHAR2(255 CHAR)
)

21	D822C9E6DB214414BB585A20B0F9A03C	1	240	сверху (4мин)

  <cim:OperationalLimitType rdf:about="#_d822c9e6-db21-4414-bb58-5a20b0f9a03c">

<cim:IdentifiedObject.name>сверху (4мин)</cim:IdentifiedObject.name>
 
<cim:OperationalLimitType.acceptableDuration>240</cim:OperationalLimitType.acceptableDuration>

<cim:OperationalLimitType.direction rdf:resource="cim:OperationalLimitDirectionKind.high"/>

</cim:OperationalLimitType>

COMMENT ON TABLE RSDUADMIN.OBJ_LIMIT_TYPE IS 'Справочник типов эксплуатационных ограничений';

COMMENT ON COLUMN RSDUADMIN.OBJ_LIMIT_TYPE.ID IS 'Идентификатор типа эксплуатационного ограничения';

COMMENT ON COLUMN RSDUADMIN.OBJ_LIMIT_TYPE.GUID IS 'Глобальный идентификатор типа эксплуатационного ограничения';

COMMENT ON COLUMN RSDUADMIN.OBJ_LIMIT_TYPE.DIRECTION IS 'Направление нарушения для типа эксплуатационного ограничения';

COMMENT ON COLUMN RSDUADMIN.OBJ_LIMIT_TYPE.DURATION IS 'Допустимая длительность нарушения для типа эксплуатационного ограничения';

COMMENT ON COLUMN RSDUADMIN.OBJ_LIMIT_TYPE.NAME IS 'Наименование типа эксплуатационного ограничения';


связана с CREATE TABLE RSDUADMIN.OBJ_LIMIT
(
  ID             NUMBER(11),
  GUID           RAW(16) CONSTRAINT OBJ_LIMIT_GUID_NN NOT NULL,
  ID_LIMIT_SET   NUMBER(11),
  ID_LIMIT_TYPE  NUMBER(11),
  ID_PTYPE       NUMBER(11),
  NORMAL_VALUE   NUMBER,
  VAL            NUMBER,
  NAME           VARCHAR2(255 CHAR)
)



3)

CREATE TABLE RSDUADMIN.OBJ_MODEL
(
  ID           NUMBER(11),
  NAME         VARCHAR2(255 BYTE) CONSTRAINT OBJ_MODEL_NAME_NN NOT NULL,
  ID_TYPE      NUMBER(11),
  GUID         RAW(16),
  ID_MANUFACT  NUMBER(11)                       DEFAULT null
)

COMMENT ON TABLE RSDUADMIN.OBJ_MODEL IS 'Модели оборудования';

COMMENT ON COLUMN RSDUADMIN.OBJ_MODEL.NAME IS 'Наименование модели';

COMMENT ON COLUMN RSDUADMIN.OBJ_MODEL.ID_TYPE IS 'Ссылка на тип объекта (оборудования)';










CREATE TABLE RSDUADMIN.SYS_PTYP
(
  ID            NUMBER(11),
  ID_NODE       NUMBER(11),
  NAME          VARCHAR2(255 BYTE),
  ALIAS         VARCHAR2(255 BYTE),
  ID_ICON       NUMBER(11),
  CIM_CLASS     VARCHAR2(63 BYTE),
  DEFINE_ALIAS  VARCHAR2(63 BYTE),
  ID_GTYP       NUMBER(11) CONSTRAINT SYS_PTYP_ID_GTYP_NN NOT NULL,
  DESCRIPTION   VARCHAR2(255 BYTE),
  ID_FILEWAV    NUMBER(11)                      DEFAULT NULL,
  ID_TYPE       NUMBER(11)
)

1008	1	Напряжение	U	118	Voltage 	PTYP_VOLTAGE	1	Значение напряжения в кВ  (kV)		1
1022	4	Температура	T	43	Temperature 	PTYP_TEMPERATURE	1	Значение температуры в температурных единицах		1
1014	1	Проводимость реактивная	B	239	Susceptance 	PTYP_SUSCEPTANCE	1	Мнимая часть полной проводимости		1
1004	1	Сопротивление активное	R	131	Resistance 	PTYP_RESISTANCE	1	Активное сопротивление (реальная часть полного сопротивления) в Омах		1
1002	1	Мощность реактивная	Q	122	ReactivePower	PTYP_REACTIVE_POWER	1	Произведение среднеквадратичного значения напряжения и среднеквадратичного значения реактивной составляющей тока (в МВар)		1
1005	1	Сопротивление реактивное	X	125	Reactance 	PTYP_REACTANCE	1	Реактивное сопротивление (мнимая часть полного сопротивления) в Омах (Ohm) при номинальной частоте		1
1041	6	Процент	%	240	PerCent	PTYP_PERCENT	1	Обычно принимает значения от 0 до 100		1




CREATE TABLE RSDUADMIN.SYS_PARAM_TYPES
(
  ID             NUMBER(11),
  ID_TYPE        NUMBER(11) CONSTRAINT SYS_PARAM_TYPES_ID_TYPE_NN NOT NULL,
  ID_UNIT        NUMBER(11),
  ID_ENUM        NUMBER(11),
  NAME           VARCHAR2(255 BYTE),
  CIM_ATTR_NAME  VARCHAR2(15 BYTE),
  DEFINE_ALIAS   VARCHAR2(63 BYTE),
  ALIAS          VARCHAR2(255 BYTE),
  GUID           RAW(16)                        DEFAULT null
)


2250	1027		75	Положительное направление измерений перетоков		PARAM_POSITIVE_DIRECT	Положительное направление измерений перетоков (по умолчанию "к шинам")
1794	1026	76		Расстояние между проводниками		PARAM_PHASE_WIRE_SPACING	Расстояние между проводниками
1793	1042	79		Количество проводников в симметричном пучке		PARAM_PHASE_WIRE_COUNT	Количество проводников в симметричном пучке
1792	1027		45	Проводник является кабелем"		PARAM_IS_CABLE	Проводник является кабелем
1790	1024	67		Время изменения нагрузки между границами диапазона		PARAM_RATED_LOAD_REGULATING_TIME	Время изменения нагрузки между границами диапазона

