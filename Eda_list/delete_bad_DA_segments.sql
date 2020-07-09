/*
���������:
  ������ � �� ������������ ������ ������������� ��� ��������� �����, ������� �� ����� ���� �� ����� 
  (��� �������, � �������� ���� 6-7-8-9-� �.�.)

��� ��� ����������, ����� ������ ���� ������������������ �������� ����� :

select * from sys_tbllst where table_name like 'DA_V_LST_%';

  � ��� � �������� ������� ����� "����������"  dpload:

Warning - Scheme name of archive tables is empty for subsystem with ID: 1262 and list-table: DA_V_LST_7.
Archives will not write for this subsystem.
Warning - Scheme name of archive tables is empty for subsystem with ID: 2082 and list-table: DA_V_LST_9.
Archives will not write for this subsystem.
Warning - Scheme name of archive tables is empty for subsystem with ID: 2084 and list-table: DA_V_LST_8.

  ����� - ������� ����������, ��� ������� ������������� �� �����  (�� sys_tree21 - ���� ��� ��� ������ ID_LSTTBL, ������, �� ������������)
  ��� (�������) ����� ������� (�.�. ������� ��� ���  ��� �� ���. � ����������� �������� �� ��������� ������ ����)

  ��� ����� � ������������ ��������� ������. ��������� ����� � sqlplus

  � ���� ������� �� ����� ������� �������� ������������� ��� �������� � 8  (DA_V_CAT_8, DA_V_LST_8)
   ������� ��������� ������ �� �����, ������� ��������� 
   � ����� ������� � DROP ��� ����� ������ ��� ������ ������
*/
--------------------------------------------------------------
set echo off 
-- ������� ������ ��� ���:

define DA_LST_NUMBER=19
-- �.�. � ���� ������� �� ����� ������� �������� ������������� ��� �������� � 8  (DA_V_CAT_8, DA_V_LST_8)

-- �������� , ��� ��� ��� � sys_tree21 
select count (id) "����/��� � SYS_TREE21" from sys_tree21 where id_lsttbl in (select id from sys_tbllst where upper(table_name) like 'DA_V_LST_&DA_LST_NUMBER');


-- ��� ��� ����� �������� (��� ����� ��� � ������  - �� ��� � sys_tree21 ���, �� ��� �� ������������)
select * from DA_V_CAT_&DA_LST_NUMBER;
select *  from sys_tbllst where id_node in (select id_node from sys_tbllst where table_name like 'DA_V_CAT_&DA_LST_NUMBER');



--------------------------------------------------------------
-- ���� ��� (������ 0), �� ������� ����� ������� 
-- ����� ��� ���� ������ ��� ���� ���������


drop public synonym DA_V_CAT_&DA_LST_NUMBER;
drop public synonym DA_V_LST_&DA_LST_NUMBER;
drop public synonym ARC_DA_V_LST_&DA_LST_NUMBER;
drop view DA_V_CAT_&DA_LST_NUMBER;
drop view DA_V_LST_&DA_LST_NUMBER;
drop view ARC_DA_V_LST_&DA_LST_NUMBER;

delete from sys_tbllnk where id_lsttbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER' 
                                              and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));
delete from sys_tblref where id_tbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER'
                                              and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));
delete from sys_tblref where id_tbl in (select id from sys_tbllst t where table_name like 'DA_V_CAT_&DA_LST_NUMBER'
                                              and not exists (select 1 from sys_tree21 where id_lsttbl IN (SELECT id_lsttbl FROM sys_tabl_v WHERE ID = t.id)));

delete from ARC_SERVICES_TUNE where id_sprofile in (select id from arc_subsyst_profile
    where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER'
                        AND not exists (select 1 from sys_tree21 where id_lsttbl = t.id)));

delete from ARC_SERVICES_ACCESS where id_sprofile in (select id from arc_subsyst_profile
    where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER'
                        AND not exists (select 1 from sys_tree21 where id_lsttbl = t.id)));

delete from ARC_SERVICES_INFO where id_lsttbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER'
                        AND not exists (select 1 from sys_tree21 where id_lsttbl = t.id));

delete from arc_subsyst_profile where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER'
  and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));

delete from sys_app_serv_lst where id_lsttbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER'
  and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));

delete from ARC_READ_DEFAULTS  where id_tbllst in (select id from sys_tbllst t where table_name like '%DA_V_LST_&DA_LST_NUMBER'
  and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));

delete from ARC_READ_DEFAULTS  where id_tbllst in (select id from sys_tbllst t where table_name like 'ARC_DA_V_LST_&DA_LST_NUMBER'
  and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));

delete  from ARC_READ_VIEW  where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_&DA_LST_NUMBER'
  and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));

delete from ad_sinfo where id_lsttbl  in (select id from sys_tbllst t where table_name like 'DA_V_CAT_&DA_LST_NUMBER'
   and not exists (select 1 from sys_tree21 where id_lsttbl = t.id)); 



set serveroutput on
declare 
 nID number ;
 tname varchar2(15) := 'DA_V_LST_&DA_LST_NUMBER';
 begin 
  select count (id) into nID from sys_tree21 where id_lsttbl in (select id from sys_tbllst where table_name like tname);  
  if nId = 0 then 
    select id_node  into nID from sys_tbllst where table_name like tname;

delete from sys_tblref where id_tbl in (select id from sys_tbllst where id_node = nID);
delete from sys_tbllnk where id_lsttbl in (select id from sys_tbllst where id_node = nID);
delete from sys_tbllnk where id_dsttbl in (select id from sys_tbllst where id_node = nID);
    delete from sys_tbllst where id_node = nID;

    delete from sys_tbllst where table_name like tname;
    delete from sys_db_part where id = nID;
    dbms_output.put_line ('������� ��� '||tname );
  else
    dbms_output.put_line ('�� ������� ��� '||tname || ': ������������ � ����������!');
  end if;

exception when no_data_found then 
    dbms_output.put_line ('��� ��� �������� '|| tname||' �� ���������������� - ������ �������');
end;
/

-- � ����� �������� ������� ������
--commit;

set serveroutput on
declare 
 nID number ;
 tname varchar2(15) := 'ARC_DA_V_LST_&DA_LST_NUMBER';
 begin 
  select count (id) into nID from sys_tree21 where id_lsttbl in (select id from sys_tbllst where table_name like tname);  
  if nId = 0 then 
    select id_node  into nID from sys_tbllst where table_name like tname;

delete from sys_tblref where id_tbl in (select id from sys_tbllst where id_node = nID);
delete from sys_tbllnk where id_lsttbl in (select id from sys_tbllst where id_node = nID);
delete from sys_tbllnk where id_dsttbl in (select id from sys_tbllst where id_node = nID);
    delete from sys_tbllst where id_node = nID;

    delete from sys_tbllst where table_name like tname;
    delete from sys_db_part where id = nID;
    dbms_output.put_line ('������� ��� '||tname );
  else
    dbms_output.put_line ('�� ������� ��� '||tname || ': ������������ � ����������!');
  end if;

exception when no_data_found then 
    dbms_output.put_line ('��� ��� ������� �������� '|| tname||' �� ���������������� - ������ �������');
end;
/

-- � ����� �������� ������� ������
--commit;



