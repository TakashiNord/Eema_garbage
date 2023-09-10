

FUNCTION prepare_table (pTname VARCHAR2) RETURN VARCHAR2 IS
  nID_TblLst NUMBER (11);
  nID_Ginfo  NUMBER (11);
  nSTATE_DPLOAD NUMBER (11);
  nErrID1    NUMBER (11);
  nErrID2    NUMBER (11);
  vARC_OWNER VARCHAR2 (63);
  vSQL       VARCHAR2 (1024) := 'begin OWNER.stack_ctrl_pkg.truncate_table@DBLINK(:1); end;';
  vRetVal    VARCHAR2 (1024);
BEGIN

  get_arc_profile (upper(pTName), nID_TblLst, nID_Ginfo);
  get_arc_owner (nID_TblLst, vARC_OWNER);

  nSTATE_DPLOAD := check_ginfo_state (nID_Ginfo);
  nErrID1 := check_err_id;
  IF NOT (nID_TblLst IS NULL OR nID_Ginfo IS NULL OR vARC_OWNER IS NULL)
   AND nSTATE_DPLOAD = 0
  THEN
    -- ��������� ��� ������������� �������� �� ����-����
    check_stack (pTname, vARC_OWNER);
    --  �������� ����-�����
    vSQL := REPLACE (vSQL, 'OWNER', vARC_OWNER);
    vSQL := REPLACE (vSQL, '@DBLINK', vARC_SRV_DBLINK);
    execute immediate vSQL USING pTname;
    -- ������� �������-����
    nErrID2 := check_err_id;
    IF nErrID2 > nErrID1 THEN
      vRetVal := check_err_name (nErrID1, nErrID2);
    END IF;
  ELSIF  nSTATE_DPLOAD != 0 then
    vRetVal := '��� �������, ������� ������� ����� ����� ������ ��������, �������������� ����������';
  ELSE
    -- ������ �� ������� ������� �� �������
    vRetVal := '�� ������� ������ �� ������� ������ ��� '||pTname;
  END IF;

  RETURN vRetVal;

EXCEPTION WHEN OTHERS THEN
  vRetVal := '������ ���������� �������� �������-����� ��� '||pTname||': '||SQLERRM;
  RETURN vRetVal;
END;



FUNCTION move_data (pTname VARCHAR2) RETURN VARCHAR2 IS
  nID_TblLst NUMBER (11);
  nID_Ginfo  NUMBER (11);
  nSTATE_DPLOAD NUMBER (11);
  nErrID1    NUMBER (11);
  nErrID2    NUMBER (11);
  vARC_OWNER VARCHAR2 (63);
  vSQL VARCHAR2 (1024) := 'begin OWNER.stack_ctrl_pkg.arc_move_table_stack@DBLINK (:1, :2, :3); end;';
  vRetVal    VARCHAR2 (1024);
BEGIN

  get_arc_profile (upper(pTName), nID_TblLst, nID_Ginfo);
  get_arc_owner (nID_TblLst, vARC_OWNER);

  nSTATE_DPLOAD := check_ginfo_state (nID_Ginfo);
  nErrID1 := check_err_id;
  IF NOT (nID_TblLst IS NULL OR nID_Ginfo IS NULL OR vARC_OWNER IS NULL)
   AND nSTATE_DPLOAD = 0
  THEN
    --  ������ ����-�����
    vSQL := REPLACE (vSQL, 'OWNER', vARC_OWNER);
    vSQL := REPLACE (vSQL, '@DBLINK', vARC_SRV_DBLINK);
    execute immediate vSQL USING pTname, nID_TblLst, nID_Ginfo;
    -- ��������� �������-����
    nErrID2 := check_err_id;
    IF nErrID2 > nErrID1 THEN
      vRetVal := check_err_name (nErrID1, nErrID2);
    END IF;
   ELSIF  nSTATE_DPLOAD != 0 then
    vRetVal := '��� �������, ������� ������� ����� ����� ������ ��������, �������������� ����������';
   ELSE
    -- ������ �� ������� ������� �� �������
    vRetVal := '�� ������� ������ �� ������� ������ ��� '||pTname;
   END IF;
  RETURN vRetVal;

EXCEPTION WHEN OTHERS THEN
  vRetVal := '������ ������� �������� �������-����� ��� '||pTname||': '||SQLERRM;
  RETURN vRetVal;
END;

