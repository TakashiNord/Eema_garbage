// pr2000.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#define uint32_t int

typedef struct REG_SET
{
    uint32_t    id;
    uint32_t    id_param;
    uint32_t    id_level;
    uint32_t    id_src;
    uint32_t    id_srci;
    uint32_t    notify_period;
    uint32_t    hyst_period;    /* ��������� ���������� ��� ������������ �������*/
    uint32_t    firstbegintime; /* ����� ��������������� ����������� ������ �� ������� */
    uint32_t    priority;
    uint32_t    start_signal_id;
    uint32_t    cont_signal_id;
    uint32_t    sets_type;
    /*    double              m_min_val;*/      /* �������� ������� �� ������� ����� */
    /*    double              m_max_val;  */
    double      min_val;        /* �������� �������, ���������� �� ��������� */
    double      max_val;
    double      min_val_final;  /* ������������ ����������� �������� �������, ������������ ��� �������� (��������, ��� ��������� �������) */
    double      max_val_final;
    uint32_t    val_is_get;     /* �������� ������� ���� �������� �� ��������� (�)*/
    uint32_t    begintime;      /* ����� ����������� ������ �� �������  */
    double      extrem;         /* ������������� �������� ��������� ��� ������ ��� �� ������� */
    uint32_t    user_id;
    uint32_t    time1970;
} REG_SET;

typedef struct REG_BASE
{
    uint32_t    id;
    uint32_t    id_node;
    uint32_t    cur_src;        /* ������� �������� �������� */
    uint32_t    cur_srci;       /* ������� ����� ��������� �������� */
    uint32_t    c_ft;           /* Current parameter state and feature */
    uint32_t    c_state;        /* Calculate state */
    uint32_t    state;          /* Parameter main feature */
    uint32_t    novalid;        /* 0 - �������� ��������� ���������, 1 - ����������� (�������� �� ���� � ���� ���������� ��������������� ����������) */
    uint32_t    set_level_number;   /* Number of set level */
    uint32_t    last_change_time;   /* Last value change time */
    uint32_t    last_valid_value_time;
    double      last_valid_value;   /* No comment */
    double      c_disper;       /* Calculate accuracy */
    double      disper;         /* Current parameter accuracy */
//    EXTREME_VALUE   extreme_value[RETRO_EXTREME_WIDTH]; /* ������������� �������� ���������� */
//    REG_VAL     rv[RETRO_WIDTH];
//    ListType    ivl;   /* ������ ListType �������������� ������������ �������� REG_INT, ������������� �� ��������� */
//    ListType    avr;   /* ������ ListType �������������� ������� �������� REG_AVR, ������������� �� ��������� */
//    REG_MVL     mvl;
    REG_SET   (*set)[];
//    struct ELRSRV_ENV *pEnv;
}REG_BASE;
  
REG_BASE s ;
REG_SET ss ;
struct REG_SET set, *pset;

int f1 ( ) 
{
	s.id=2000 ;
	s.last_change_time=98878868;
	s.set_level_number = 1 ;
	
	ss.begintime=9087768 ;
	ss.id=32455;
	ss.max_val=768;
	ss.min_val=654;

	pset=&set;

	pset->begintime=23333 ;
	pset->id=32455;
	pset->max_val=768;
	pset->min_val=654;
	
	//s.set=&pset;	
	
	return 0 ;
}

int f2 ( ) 
{
    int j ;
    int dt = 0 ;
	
	struct REG_BASE        RegBase ;
	struct REG_SET         RegSet ;
	
	
	REG_BASE        *pRegBase = &RegBase;
	struct REG_SET         *pRegSet =&RegSet ; //= NULL;	
	
	(*pRegBase).id=2000;
	(*pRegBase).last_change_time=98878868;
	(*pRegBase).set_level_number = 1 ;
	
	(*(*pRegBase).set)[0].id=55;
	(*(*pRegBase).set)[0].id_param=1000;
	(*(*pRegBase).set)[0].id_level=123;
	(*(*pRegBase).set)[0].id_src=10023;
	(*(*pRegBase).set)[0].id_srci=1023;
	(*(*pRegBase).set)[0].notify_period=60;
	(*(*pRegBase).set)[0].hyst_period=40;
	(*(*pRegBase).set)[0].firstbegintime=4512464;
	(*(*pRegBase).set)[0].priority=142;
	(*(*pRegBase).set)[0].start_signal_id=445131;
	(*(*pRegBase).set)[0].cont_signal_id=745;
	(*(*pRegBase).set)[0].sets_type=7845;
	(*(*pRegBase).set)[0].max_val=999999.0;
	(*(*pRegBase).set)[0].min_val=-999999.23;
	(*(*pRegBase).set)[0].min_val_final=999.0;
	(*(*pRegBase).set)[0].max_val_final=-9.23;
	(*(*pRegBase).set)[0].val_is_get=0;
	(*(*pRegBase).set)[0].begintime=123;
    (*(*pRegBase).set)[0].extrem=56842.235;
    (*(*pRegBase).set)[0].user_id=1;
    (*(*pRegBase).set)[0].time1970=145236879;

	//pRegSet = &(*(*pRegBase).set)[0];

    for (j = 0; j < (*pRegBase).set_level_number; j++)
    {
        dt=(*(*pRegBase).set)[j].begintime ;
		printf("begintime=%d",dt);
     }






	return 0 ;
}


int main(int argc, char* argv[])
{
   f2 ( ) ;
	
	
	
	printf("Hello World!\n");
	return 0;
}

/*

struct point *pp;

��������, ��� pp - ��� ��������� �� ��������� ���� struct point. ���� pp ��������� �� ��������� point, �� *pp - ��� ���� ���������, � (*pp).x � (*pp).y - �� ��������. ��������� ��������� pp, �� ����� �� ��������

struct point origin, *pp;

pp = &origin;
printf("origin: (%d,%d)\n", (*pp).x, (*pp).y);

 ���� �� ��������� �������. (�������� -> ������� �� ����� -, �� ������� ����� ������� ���� >.) ������� printf ����� ���������� � ����

printf("origin: (%d,%d)\n", pp->�, pp->y);

��������� . � -> ����������� ����� �������. ����� �������, ��� ������� ����������

struct rect r, *rp = &r;

��������� ������ ��������� ����� ������������:

r.pt1.x
rp->pt1.x
(r.pt1).x
(rp->pt1).x

��������� ������� � ��������� ��������� . � -> ������ � ����������� ������ ������� () � ���������� ������� [] �������� ����� ������� ��������� � �������� ����������� � ����������� ������ ����� ������ ����������. ��������, ���� ������ ����������


#define NKEYS (sizeof keytab / sizeof(struct key))

���� �� ��������� ����� �������� ������ �������� - �������� ������ ������� �� ������ ������-�� ��� ����������� ��������:

#define NKEYS (sizeof keytab / sizeof keytab[0])

*/