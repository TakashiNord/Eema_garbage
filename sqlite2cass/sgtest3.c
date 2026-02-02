/*
Zolotov M 5.10.06
Testing the server of the signal system
*/
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <errno.h>
#include <sys/time.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <sys/socket.h>

#include <rsdu2def.h>
#include <rmxinifl.h>
#include <rmxtbox.h>
#include <utils.h>
#include <db2const.h>
#include <rsdujrn.h>

#include "rsduss.h"
#include "sssendext.h"

#define S_GROUPS  2
#define S_USERS   1

COMM_PORT      *CommPort = NULL;
uint16_t        port_id = 0;
uint16_t        status = E_OK, trans_id;
J_HWSTATE_STR   jhs;
J_PSTATE_STR    jps;
J_LOEVLOG_STR   jls;
char            text_msg[1023];
char           *msg = "MSG_TEST_TO_APPBAR";
unsigned char  *trb = NULL;
uint32_t        l;
uint32_t        dirt_id, dirr_id, lstt_id, lstr_id;
uint32_t        user_id, signal_id, signal_type, signal_class, jrnl_id, ss_user_id;

char     *sql = "SELECT DISTINCT SYS_SIGN.ID_SCLASS, SYS_SIGN.ID_TYPE,\
                LOWER(RTRIM(NVL(SYS_TABL.R_TABLE, 'mail or message')))\
                FROM SYS_SIGN, SYS_TABL, SYS_JRNL\
                WHERE SYS_SIGN.ID = %u\
                AND  ((SYS_JRNL.ID_TBL=SYS_TABL.ID(+))\
                AND (SYS_SIGN.ID_NODE=SYS_JRNL.ID))";

				
RSDUSS_API int32_t SS_SendSignal_HWSTATE_ToSS(const SSInfo *ssInfo,
                                             uint32_t signalId,
                                             uint32_t signalType,
                                             uint32_t userTableId,
                                             uint32_t userId,
                                             uint32_t groupTableId,
                                             uint32_t groupId,
                                             const char *message);				
				
				
				
int32_t main(int32_t argc, const char *argv[])
{
    char        FullBinName[MAX_PROCFILE_LEN];
    char        achBinName[MAX_PROCFILE_LEN];
    LOG_HEADER  LogFile;

    strcpy(FullBinName, RSDURTGUtils_GetBinName_r(achBinName, MAX_PROCFILE_LEN));

    if (argc < 5)
    {
        printf("Usage: %s <SS_user_ID> <user ID> <signal ID> <signal type> [<\"message\"> [<dirt_id> <dirr_id> <lstt_id> <lstr_id>]]\n", argv[0]);
        printf("signal type = 99999 for j_loevlog journal\n");
        return -1;
    }

    sscanf(argv[1],"%u", &ss_user_id);
    sscanf(argv[2],"%u", &user_id);
    sscanf(argv[3],"%u", &signal_id);
    sscanf(argv[4],"%u", &signal_type);

    signal_class = !SS_CONTROLSYSTEM;
    if (argc >= 6)
        sprintf(text_msg, "%s", argv[5]);

    if (argc >=10)
    {
        sscanf(argv[6],"%u", &dirt_id);
        sscanf(argv[7],"%u", &dirr_id);
        sscanf(argv[8],"%u", &lstt_id);
        sscanf(argv[9],"%u", &lstr_id);
    }

    if ((signal_type == SS_MAIL) || (signal_type == SS_TEXT_MESSAGE) || (signal_type == SS_TEXTMESS_ACT))
    {
        if (argc >=6 )
            sprintf(text_msg, "%s", argv[5]);
        else
            sprintf(text_msg, "%s", msg);
        printf ("argc=%u,text_msg=%s\n",argc,text_msg);

    }

// NEW INIT
    SSInfo      ssDest;
    SSInfoList *ssList = NULL;
    int32_t     ssres = E_SS_OK;

    SS_InfoList_Create(ssList);

    ssres = SS_Init_ByUserId(&ssDest, ss_user_id, NULL);
    if( ssres == E_SS_OK )
    {
        ssres = SS_InfoList_AddItem(ssList, &ssDest);
        if( ssres != E_SS_OK )
        {
            printf("Error in AddItem1 !!! [0x%08x]\n", ssres);
        }
    }
    else
    {
        printf("Error in init SS1!!! [0x%08x]\n", ssres);
        return -1;
    }

/*
    //find Signal System server
    if (RSDURTGUtils_SS_Init(ss_user_id, user_id, (uint32_t)1001, (char*)0, &LogFile, FullBinName) < 0)
    {
        printf("SS_Init() failed\n");
        return -1;
    }
    else
    {
        printf("Init is OK\n");
    }
*/

    port_id = 0;
    CommPort = RSDURTGUtils_GetCommPort((uint16_t)ADV_PROTO_UDP, &port_id,&status, &LogFile);
	if (signal_id==250) CommPort = RSDURTGUtils_GetCommPort((uint16_t)ADV_PROTO_COMMON, &port_id, &status, &LogFile);

    if(CommPort == NULL)
    {
        printf("rq_create_port() failed, err %04hx\n", status);
        return -1;
    }

    switch (signal_type)
    {
    case SS_MAIL:
        printf("Send mail signal %u to signal server\n", signal_id);
        printf("Send mail: msg==<%s> success!\n",text_msg);
        RSDURTGUtils_SS_SendMailMessage((COMM_PORT*)CommPort, signal_id, text_msg, &LogFile, FullBinName);
        break;

    case SS_TEXT_MESSAGE:
    case SS_TEXTMESS_ACT:
        printf("Send text message %u to signal server\n", signal_id);
        RSDURTGUtils_SS_SendTextMessage((COMM_PORT*)CommPort, signal_id, signal_class, signal_type, text_msg, &LogFile, FullBinName);
        printf("Send text: msg==<%s> success!\n",text_msg);
        break;

    case 99999:
        bzero(&jls, sizeof(J_LOEVLOG_STR));
        jls.sh.signal = signal_id;
        jls.sh.id_src_user = (uint16_t)user_id;
        jls.sh.id_cause_user = (uint16_t) user_id;
        jls.sh.lctime = RSDURTGUtils_Time70();

        jls.dirt_id = dirt_id;//81;// TableID
        jls.dirr_id = dirr_id;//26;// ParameterID
        jls.lstt_id = lstt_id;//81;// TableID
        jls.lstr_id = lstr_id;//29;// ParameterID
        if (strlen(text_msg)<=0)
        {
            strcpy(jls.text, "test string");
        }
        strncpy(jls.text, text_msg, strlen(text_msg));
        l = sizeof(J_LOEVLOG_STR);
        trb = (unsigned char *)RSDURTGUtils_SMalloc((uint32_t)l); 
        bzero(trb, l);
        if (trb == (unsigned char *)0)
        {
            printf("Out of memory: %d\n", l);
            return -1;
        }
        memmove(trb, (char *)&jls, l);
        printf("Send signal %u to signal server (j_loevlog journal)\n", signal_id);
        if (RSDURTGUtils_SS_SendSignal((uint32_t) signal_class,
            signal_type,
            (char*) trb,
            l,
            (COMM_PORT *)CommPort,
            &LogFile,
            FullBinName) < 0)
            printf("SS_SendSignal() failed\n");
        RSDURTGUtils_SFree(trb);
        break;

    default:
        /*
        jps.sh.signal = signal_id;
        jps.sh.id_src_user = (uint16_t)user_id;
        jps.sh.id_cause_user = (uint16_t)user_id;
        jps.sh.lctime = RSDURTGUtils_Time70();
        jps.dirt_id = S_GROUPS;
        jps.dirr_id = 1001;
        jps.lstt_id = S_USERS;
        jps.lstr_id = user_id;
        l = strlen(text_msg);
        strncpy(jps.description, text_msg, J_PSTATE_STR_DESC_SIZE);
        jps.description[(l < J_PSTATE_STR_DESC_SIZE-1)? l : J_PSTATE_STR_DESC_SIZE-1] = '\0';

        l = sizeof(J_PSTATE_STR);
        trb = (unsigned char *)RSDURTGUtils_SMalloc((uint32_t)l);
        if (trb == (unsigned char *)0)
        {
            printf("Out of memory: %d\n", l);
            return -1;
        }
        memmove(trb, (char *)&jps, sizeof(J_PSTATE_STR));
        printf("Send signal %u to signal server\n", signal_id);

//        ssres = SS_SendSignal_ToSS(&ssDest, signal_id, signal_type, user_id, trb, l);
        ssres = SS_SendSignal_ToMaster(ssList, signal_id, signal_type, user_id, trb, l);
        if( ssres != E_SS_OK )
        {
            printf("Error in sending signal!!! [0x%08x]\n", ssres);
        }
*/
/*
        if (RSDURTGUtils_SS_SendSignal((uint32_t) signal_class,
            signal_type,
            (char*) trb,
            l,      
            (COMM_PORT *)CommPort,
            &LogFile,
            FullBinName) < 0)
            printf("SS_SendSignal() failed\n");
*/
//        RSDURTGUtils_SFree(trb);

        if (signal_id==250) {
          ssres = SS_SendSignal_HWSTATE_ToSS(&ssDest, signal_id, signal_type, S_USERS, user_id, S_GROUPS, 1001, text_msg);
          if( ssres != E_SS_OK )
          {
            printf("Error in sending signal!!! [0x%08x]\n", ssres);
          }
		  break ;
		}

        ssres = SS_SendSignal_PSTATE_ToSS(&ssDest, signal_id, signal_type, S_USERS, user_id, S_GROUPS, 1001, text_msg);
        if( ssres != E_SS_OK )
        {
            printf("Error in sending signal!!! [0x%08x]\n", ssres);
        }
        break;
    }

    RSDURTGUtils_DelCommPort(CommPort, &LogFile);

    return 0;
}


RSDUSS_API int32_t SS_SendSignal_HWSTATE_ToSS(const SSInfo *ssInfo,
                                             uint32_t signalId,
                                             uint32_t signalType,
                                             uint32_t userTableId,
                                             uint32_t userId,
                                             uint32_t groupTableId,
                                             uint32_t groupId,
                                             const char *message)
{
    J_HWSTATE_STR j_hwstate ;

    int32_t         ssres = E_SS_OK;
    size_t          len = 0;
	
	int state = 0;

    memset(&j_hwstate, 0, sizeof(J_HWSTATE_STR));

    j_hwstate.sh.signal = signalId;
    j_hwstate.sh.id_src_user = (uint16_t)userId;
    j_hwstate.sh.id_cause_user = (uint16_t)userId;
    j_hwstate.sh.lctime = RSDURTGUtils_Time70();
    j_hwstate.dirt_id = groupTableId;  /* filed ID table SYS_TABL */
    j_hwstate.dirr_id = groupId;
    j_hwstate.state = state;
    j_hwstate.timetick = timetv2rsdutv( RSDURTGUtils_TimeTick() );

    len = strlen(message);

    ssres = SS_SendMessage_ToSS(ssInfo, signalType, userId, (const uint8_t *)&j_hwstate, sizeof(J_HWSTATE_STR));
	
    return ssres;
}
