/*
 ������� ������ ������� ����������� �������. ���� � ��������� ���������
     ������� ��������� ����� ������ ����������� (min(t1,t2,...)
 ������ : AccidentTimeDate($4003748, 0 ��� 1) ;
 ���������� : ����
    0 - ������� delta
    1 - ������ delta
    2 - ����� ������ yyyy-mm-dd delta  � Unix �������
*/
REG_VAL AccidentTimeDate(REG_BASE *pCurVal, int type)
{
    REG_VAL        RegVal;
    uint32_t       j ;
    uint32_t       ELRF ;
    uint32_t       first_time ;
    uint32_t       min_time ;
    uint32_t       curr_time ;
    uint32_t       dt;
    struct tm      *st;
    const time_t   *timep ;
    uint32_t       notify_period ;
 
    min_time =0; // ��� ������ ������������ ���������� �������
                 // (*pCurVal).last_valid_value_time ;
                 
    /* ���� �� ���� ������� ������� j ��� �������� ��������� */
    for (j = 0; j < (*pCurVal).set_level_number; j++)
    {
        //   
        notify_period = (*(*pCurVal).set)[j].notify_period ; // max = 0..3600
        if (notify_period<=3600)  (*(*pCurVal).set)[j].notify_period = 3600*24 ; 

        first_time = (*(*pCurVal).set)[j].begintime ; //firstbegintime
        if (j==0) min_time = first_time ;
        else if (min_time>first_time) min_time = first_time ;
    }
    first_time = min_time ;

    curr_time = RSDURTGUtils_Time70(); // time(0)

    // ��������� �������
    //dt =(uint32_t)difftime(curr_time,first_time) ;
    dt = curr_time - first_time; // ���������� ������� delta
    if (dt<0) dt = 0 ; // (*pCurVal).last_change_time

    ELRF = ELRF_ADERATING ; // ELRF_DERATING;

    if (first_time<=0) {
        dt = 0;
        ELRF = ELRF_ALLCORRECT;
    }


    if (type==1) {

       // �������� ����� �����
       dt = dt/60 + ((dt%60)?1:0) ;

    }

    if (type==2) {

       // ���� ������: �������� �� ������� ����-MM-DD
       // ������������� � time_t � ��������� dt

       ttime=(time_t)curr_time ;
       timep=&ttime;
       //st=gmtime(&timep) ; // Convert time_t to tm as UTC time (function )
       st=localtime (timep) ; // Convert time_t to tm as local time (function )

       //st->tm_year =
       //st->tm_mon =
       //st->tm_mday =
       st->tm_hour = 0;
       st->tm_min = 0;
       st->tm_sec = 0;

       dt = mktime(st) + dt ;
    }

    RegVal.vl = dt;
    RegVal.ft = ELRF;

    return RegVal;
}
