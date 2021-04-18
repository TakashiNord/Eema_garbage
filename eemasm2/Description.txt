
ac_serv = acsrvd
 udp:10.100.1.20:2005
 модуль удерживает статус экземпляра.
    switch (cd.command)
        case GCMD_GET_GLOBAL_STATE
 возвращает статус
       case GCMD_GETDATASERVICEINFO
возвращает   UNITRANS_HEADER+DATA_SRV_INFO
#ifdef LIGHT
                ds.tcp_port = ADV_SRVC_DCPOICL;
#else
                ds.tcp_port = ADV_SRVC_DCPDATA;
#endif
                ds.current_state = DSST_LOAD|DSST_INIT|DSST_LISTEN;
                ds.id_user = (*Env).UserID;
                ds.id_server = (*Env).MyServerID;
                ds.id_group = (*Env).GroupID;
                ds.current_connection_number = 0;
                ds.all_connection_number = 0;
                Sbuf = (char*)&ds;
                cd.data_len = sizeof (DATA_SRV_INFO);


Bridge
 модуль управляет статусами, контроль, и репликацией.
 udp:10.100.1.20:2013  - прием команд +  контроль статуса
 tcp:10.100.1.20:2013   - контроль статуса

 udp_service_task
       port_id = ADV_SRVC_BRIDGEEXT;
       ADV_PROTO_UDP_CLEAR
        switch (cd.command)
            case GCMD_GET_GLOBAL_STATE
            case GCMD_ECHO

forvard_task
/*
Действия в данной задаче выполняет мост,
являющийся в текущий момент МАСТЕРом.
Описание функций задачи:
- контроль доступности SLAVE (РЕЗЕРВа) - периодически щлёт запросы и ожидает ответа на них;
- прием команд на выполнение РЕЗЕРВИРОВАНИЯ и перенаправление их на выполнение к SLAVE
*/

back_task
 GCMD_SEND_TO_SLAVE
 GCMD_ECHO
 GCMD_SET_GLOBAL_STATE_MASTER
/*
Действия в данной задаче выполняет мост,
являющийся в текущий момент РЕЗЕРВом.
Описание функций задачи:
Подключение и контроль связи с MASTERом.
Контроль одновременного присутствия двух MASTERов - в этом случае,
тот который по дефолту не является основным, переходит в режим SLAVE.
*

smagent
 udp:10.100.1.20:2050  - прием команд
 tcp:10.100.1.20:2050   - контроль статуса
 - обработка внутренних команд + получение только  (UNITRANS_HEADER*)&header
   отправка (UNITRANS_HEADER*)&header + TR_Buff = (char *)&TAdmInf;

        SendAnswer = header.status;
        header.data_len = 0;
        header.status = E_OK;
        MsgJob.Time = header.time1970;

        switch (header.command)
        {
        case GCMD_ECHO:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "%s: Case GCMD_ECHO",__FUNCTION__);
            header.dst_uid = header.src_uid;
            header.param2 = header.param1;
            header.src_uid = Env->UserID;
            header.param1 = Env->GroupID;
            header.status = 0;
            break;

        case ADC_CHECKLINK:
            header.dst_uid = header.src_uid;
            header.param2 = header.param1;
            header.src_uid = Env->UserID;
            header.status = 0;

            header.data_len = sizeof(HOST_INFO);
            TR_Buff = (char *)&TAdmInf;
            break;

        case ADC_CHECK:
                header.status = ADERR_JOBSABSENT;
            break;

        case ADC_RESERVE:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_RESERVE", __FUNCTION__);
                    header.status = ADERR_JOBIDNOTVALID;
                header.status = ADERR_JOBSABSENT;
            break;

        case GCMD_SET_GLOBAL_STATE_MASTER:
        case GCMD_SET_GLOBAL_STATE_SLAVE:
            if (header.command == GCMD_SET_GLOBAL_STATE_MASTER)
                for (i=0; i<MAX_JOB_PER_HOST; i++)
                {
                    /* Команду перехода отправляем всем модулям кроме моста */
                    if ((Env->JobReg[i].JobTok != 0) && (strstr(Env->JobReg[i].LoadName, "bridge") == NULL))
                    {
                        pid = Env->JobReg[i].JobTok;
                        get_proc_path(procfile, (int32_t*)&pid);
                        if (strlen(procfile) > 0)
                        {
                            MsgJob.Command = JBC_RESERVE;
                            MsgJob.Data.ObjToken[0] = 0;
                            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "%s: JBC_RESERVE to Env->JobReg[%d].JobTok=%d",__FUNCTION__,i,Env->JobReg[i].JobTok);
                            retcode = RSDURTGUtils_MsgBoxSend(&(*Env).MsgBox, &MsgJob, sizeof(MsgJob), Env->JobReg[i].ExitMB, 0, &status);
                        }
                    }
                }/* of for (...) */
                if (JobNumberCheck > 0)
                    header.status = ADERR_JOBIDNOTVALID;
                else
                    header.status = ADERR_OK;
            }/* if Env->JobNumber != 0 */
            else
                header.status = ADERR_JOBSABSENT;
            break;

        case ADC_SYNCSIGNAL:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_SYNCSIGNAL unsupported on current service", __FUNCTION__);
            header.status = ADERR_UNSUPPORTCOMMAND;
            break;

        case ADC_REINITDB:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_REINITDB", __FUNCTION__);

        case ADC_RINIT:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_RINIT", __FUNCTION__);
            if (Env->JobNumber != 0)
            {
                for (i=0; i<MAX_JOB_PER_HOST; i++)
                {
                    if (((Env->JobReg[i].JobInfo != (RSDU_INFO*)0) && (header.param2 != 0) && (Env->JobReg[i].JobInfo->user_id == (uint16_t)header.param2))
                        || (Env->JobReg[i].JobTok == (int32_t)header.dst_uid)) break;
                }
 
        case ADC_RINITALL:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_RINITALL", __FUNCTION__);
 
        case ADC_SHUTDOWN:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_SHUTDOWN", __FUNCTION__);
            execres = rsdu_system((char *)"/usr/bin/ema softstop &", LogFile);
            if (execres != 0)
                RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "%s: Error system ADC_SHUTDOWN. Errno=%u (%s)", __FUNCTION__, errno, strerror(errno));
            header.status = ADERR_OK;
            break;

        case ADC_SHUTDOWNJOB:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_SHUTDOWNJOB ProcID %u (UserID %u)", __FUNCTION__, header.dst_uid, header.param2);
 **/
        case ADC_CLEAR:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_CLEAR", __FUNCTION__);
 

        case ADC_RELOADJOB:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_RELOADJOB ProcID %u (UserID %u)", __FUNCTION__, header.dst_uid, header.param2);
            if (Env->JobNumber != 0)
            {
                for (i=0; i<MAX_JOB_PER_HOST; i++)
                {
                    if (((Env->JobReg[i].JobInfo != (RSDU_INFO*)0) && (header.param2 != 0) && (Env->JobReg[i].JobInfo->user_id == (uint16_t)header.param2))
                        || (Env->JobReg[i].JobTok == (int32_t)header.dst_uid)) break;
                }
                    strcpy (RLJobName, pt);
                    strncpy(RLJobLogin, (const char *)Env->JobReg[i].JobInfo->login, MAX_LOGIN_LEN);
                    snprintf(RLEmaArgs, 128, "/usr/bin/ema restart-force %s now &", RLJobName);
                    int execres = rsdu_system(RLEmaArgs, LogFile);
                }
                else
                    header.status = ADERR_LOADNAMENOTVALID;
            }
            else
                header.status = ADERR_JOBSABSENT;
            break;

        case ADC_DELETEJOB:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_DELETEJOB ProcID %u", __FUNCTION__, header.dst_uid);
            for (i=0; i<MAX_JOB_PER_HOST; i++)
                if (Env->JobReg[i].JobTok == (int32_t)header.dst_uid) break;

                header.status = ADERR_JOBIDNOTVALID;
            break;

        case ADC_DELETEJOBFORCE:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_DELETEJOBFORCE", __FUNCTION__);
            for (i=0; i<MAX_JOB_PER_HOST; i++)
                if (Env->JobReg[i].JobTok == (int32_t)header.dst_uid) break;
            if (i == MAX_JOB_PER_HOST)
            {
                RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"%s: ADERR_JOBIDNOTVALID (%u)",__FUNCTION__,header.dst_uid);
                header.status = ADERR_JOBIDNOTVALID;
            }
            else
            {
                RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"%s: SIGKILL to dst_uid = %u",__FUNCTION__,header.dst_uid);
                RSDUUnix_delete_job(header.dst_uid);
            }
            break;

        case ADC_SYSTEMSHUTDOWN:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_SYSTEMSHUTDOWN", __FUNCTION__);
            sync(); /* Flush all system stacks */
            if (rsdu_system((char *)"init 0", 10) != 0) /* Force power off */
            {
                retcode = reboot(LINUX_REBOOT_CMD_POWER_OFF);
 

        case ADC_SYSTEMREBOOT:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_SYSTEMREBOOT", __FUNCTION__);
            LSMAgentRebootHost(Env, LogFile);
            break;

        case ADC_RINFO:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_RINFO", __FUNCTION__);
                TAdmInf.JobsInfo.JobNumber = Index;
                TR_Buff = (char *)&TAdmInf.GenericInfo[0];
            break;

        case ADC_SETTIMESOURCE:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_SETTIMESOURCE", __FUNCTION__);
            Env->rg->clock_source = header.time1970;
            break;

        case ADC_USETSENDERBRIDGE:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_USETSENDERBRIDGE", __FUNCTION__);
            memset(&Env->rg->send_bridge_sock, 0, sizeof(Env->rg->send_bridge_sock));
            break;

        case ADC_SETSENDERBRIDGE:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_SETSENDERBRIDGE", __FUNCTION__);
            Env->rg->send_bridge_sock = rem_address;
            break;

        case ADC_USETRECEIVERBRIDGE:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_USETRECEIVERBRIDGE", __FUNCTION__);
            memset(&Env->rg->receive_bridge_sock, 0, sizeof(Env->rg->receive_bridge_sock));
            break;

        case ADC_SETRECEIVERBRIDGE:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_SETRECEIVERBRIDGE", __FUNCTION__);
            Env->rg->receive_bridge_sock = rem_address;
            break;

        case ADC_GETJOBDIR:
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Command ADC_GETJOBDIR", __FUNCTION__);
            for (i=0; i<MAX_JOB_PER_HOST; i++)
                if (Env->JobReg[i].JobTok == (int32_t)header.dst_uid) break;

            TR_Buff = (char *)&TAdmInf;
            header.data_len = ((sizeof(JOBDIRITEM)*TAdmInf.DirInfo.entire_use) + (sizeof(int16_t)*2));
            break;

        case ADC_RESTART_JOB:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_RESTART_JOB ProcID %u (UserID %u)", __FUNCTION__, header.dst_uid, header.param2);
                    snprintf(RLEmaArgs, 128, "/usr/bin/ema restart-force %s now &", RLJobName);

        case ADC_RESTART_ALLJOBS:
            RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile,"%s: Command ADC_RESTART_ALLJOBS", __FUNCTION__);
            execres = rsdu_system((char *)"/usr/bin/ema softrestart-force all now &", LogFile);

        default:
            SendAnswer = 1;
            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"%s: Unknown command=%u",__FUNCTION__,header.command);
        }

        if(SendAnswer == 0)
        {
            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"%s: Sending answer to %s", __FUNCTION__, RSDURTGUtils_AddressToStr_r(&rem_address, sz_address, cszAddressSize));
            header.time1970 = time(NULL);/* TODO: RSDURTGUtils_Time70(NULL);*/
            header.src_uid = Env->UserID;
            header.param1 = Env->GroupID;
            if (RSDURTGUtils_SendCommPort((COMM_PORT *)pRcvPort,
                (REM_ADDR *) &rem_address,
                (UNITRANS_HEADER *) &header,
                (unsigned char *)TR_Buff,
                (uint16_t)250 /* 2,5 секунды, иначе возможно закидывание агентов системным монитором (команды раз в 5 секунд) */,
                &status,
                LogFile) < 0)


sysmon
 - управляет экземпляром через Клиент
 udp:10.100.1.20:2003  - прием команд
 tcp:10.100.1.20:2003   - отправка
adcp_service_task
   ADV_PROTO_TCP 
  paccet = (SYSMON_TCP *)RSDURTGUtils_SMalloc(sizeof(SYSMON_TCP));
  отсылает (UNITRANS_HEADER *) &cd + (unsigned char *)paccet (структура заполняется в зависимости от типа инфы)
            switch (cd.command)
            {
            case SMC_CLEARL_RSDU_JOB_INFO:
                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "adcp_service_task: Command SMC_CLEARL_RSDU_JOB_INFO from user %u", cd.src_uid);
                header.command = ADC_CLEAR;
                header.status = ADERR_OK;
                send_to = SEND_TO_ALL;
                int_var = 0;
                cd.data_len = 0;
                break;

            case SMC_GET_HOST_SYS_INFO:
                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "adcp_service_task: Command SMC_GET_HOST_SYS_INFO from user %u", cd.src_uid);
                header.command = ADC_SYSTEMINFO;
                header.status = ADERR_OK;
                cd.status = 0;
                send_to = (*paccet).host;
                int_var = 1;    /*    Эмуляция исполнения команды */
                cd.data_len = (sizeof(uint16_t)*4) + sizeof(SYS_INFO);
                memset (&(*paccet).data.adm_i.SysInfo, 0, sizeof (SYS_INFO));
                RSDURTGUtils_ReceiveUnits((void*)&Env->PointSem, (uint16_t)1, (uint16_t)WAIT_FOREVER, &status);
                (*paccet).data.adm_i.SysInfo.JobNumber = (*(*Env).MBInfo).MBHostInfo[(*paccet).host].JobNumber;
                (*paccet).data.adm_i.SysInfo.HostID = (*paccet).host;
                strcpy ((char*)&(*paccet).data.adm_i.SysInfo.boot_file[1], "/boot/vmlinuz");
                (*paccet).data.adm_i.SysInfo.boot_file[0] = strlen ((char*)&(*paccet).data.adm_i.SysInfo.boot_file[1]);
                (*paccet).data.adm_i.SysInfo.debuginfo = (*Env).DebugMode;
                strcpy ((char*)&(*paccet).data.adm_i.SysInfo.boot_dev[1], "Linux");
                (*paccet).data.adm_i.SysInfo.boot_dev[0] = strlen ((char*)&(*paccet).data.adm_i.SysInfo.boot_dev[1]);
                (*paccet).user_id = (*(*Env).MBInfo).MBHostInfo[(*paccet).host].user_id;
                (*paccet).host = (*paccet).host;
                (*paccet).host_count = (*(*Env).MBInfo).ActivDevice;
                (*paccet).job_id = 0;
                RSDURTGUtils_SendUnits ((void*)&Env->PointSem, (uint16_t)1, &status);
                break;

            case SMC_DEL_HOST_SYS_JOB:
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Command SMC_DEL_HOST_SYS_JOB from user %u for process %u (server %u)", cd.data_len, (*paccet).job_id, (*paccet).user_id);
                header.command = ADC_SHUTDOWNJOB;
                header.status = ADERR_OK;
                send_to = (*paccet).host;
                int_var = 0;
                cd.data_len = 0;
                break;

            case SMC_GET_HOST_JOB_INFO:
                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "adcp_service_task: Command SMC_GET_HOST_JOB_INFO from user %u", cd.src_uid);
                header.command = ADC_SYSJOBINFO;
                header.status = ADERR_OK;
                cd.status = 0;
                send_to = (*paccet).host;
                int_var = 1;    /*    Эмуляция исполнения команды */
                cd.data_len = (sizeof(uint16_t)*4) + sizeof(SYS_JOB_INFO);
                memset (&(*paccet).data.adm_i.SysJobInfo, 0, sizeof (SYS_JOB_INFO));
                (*paccet).data.adm_i.SysJobInfo.hid = (*paccet).host;
                (*paccet).data.adm_i.SysJobInfo.sjn = 0;
                break;

            case GCMD_DATASERVICEEXIT:
                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "adcp_service_task: Case GCMD_DATASERVICEEXIT");
                GoMimo = 0;
                if (is_signal_sent)
                    RSDURTGUtils_SS_SendTaskState(NULL, (uint32_t) SS_CLOSESESSION, SS_MESSAGE, LogFile, (*Env).BinFilePath);
                break;

            case GCMD_ECHO:
                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "adcp_service_task: Case GCMD_ECHO, uid %u", cd.src_uid);
                cd.status = DCP_OK;
                cd.param1 = 0;
                cd.data_len = 0;
                int_var = 1;
                break;

            case GCMD_GET_GLOBAL_STATE:
                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "adcp_service_task: Receive command GCMD_GET_GLOBAL_STATE from %u, %d", cd.src_uid,(*Env).rsdu_stop_flag);
                if ((*Env).rsdu_stop_flag)
                {
                    cd.status = E_OK;
                    cd.command = GCMD_GLOBAL_STATE_STOPPED;
                    cd.command = GCMD_GLOBAL_STATE_MASTER;
                    cd.command = GCMD_GLOBAL_STATE_SLAVE;
                }
                cd.data_len = 0;
                int_var = 1;
                break;

            case SMC_REINIT_DB:
            case SMC_REINIT_RSDU_JOB:
                header.command = ADC_RINIT;
                header.data_len = 0;
                send_to = (*paccet).host;
                int_var = 0;
                break;

            case SMC_GET_MBII_HOST_INFO:
                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "adcp_service_task: Command SMC_GET_MBII_HOST_INFO from user %u", cd.src_uid);
                header.command = ADC_RINFO;
                send_to = SEND_TO_ALL;
                int_var = 1;
                cd.data_len = (sizeof(uint16_t)*4) + sizeof(MB_HOST_INFO);
                cd.status = E_OK;
                break;

            case SMC_RESET_MBII_SYSTEM:
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Command SMC_RESET_MBII_SYSTEM from user %u", cd.src_uid);
                cd.time1970 = RSDURTGUtils_Time70();
                header.command = ADC_SYSTEMREBOOT;
                send_to = SEND_TO_ALL;
                int_var = 0;
                cd.data_len = 0;
                (*Env).rsdu_stop_flag = 0xFF;
                break;

            case SMC_SHUTDOWN_RSDU:
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Receive command SMC_SHUTDOWN_RSDU from %u", cd.src_uid);
                RSDURTGUtils_SS_SendProcState((COMM_PORT *)0, (uint32_t) SS_RSDUSTOP, SS_MESSAGE, cd.src_uid, LogFile, (*Env).BinFilePath);
                header.command = ADC_SHUTDOWN;
                send_to = SEND_TO_ALL;
                int_var = 0;
                cd.data_len = 0;
                (*Env).rsdu_stop_flag = 0xFF;
                break;

            case SMC_SHUTDOWN_ALL_HOSTS:
                cd.dst_uid = (uint16_t)0xffff;
                cd.param2  = (uint16_t)0xffff;
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Receive command SMC_SHUTDOWN_ALL_HOSTS from %u", cd.src_uid);
                RSDURTGUtils_SS_SendProcState((COMM_PORT *)0, (uint32_t) SS_SHUTDOWN, SS_MESSAGE, cd.src_uid, LogFile, (*Env).BinFilePath);
                header.command = ADC_SYSTEMSHUTDOWN;
                send_to = SEND_TO_ALL;
                int_var = 0;
                cd.data_len = 0;
                (*Env).rsdu_stop_flag = 0xFF;
                break;

            case SMC_MAKE_RESERVE_RSDU_JOB:
                /* 09.09.2008 Zolotov M.Yu Обработка команды смены статуса MASTER/SLAVE */
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Command SMC_MAKE_RESERVE_RSDU_JOB from user %u", cd.src_uid);
                header.command = ADC_RESERVE;
                header.param2 = (*paccet).user_id;
                header.dst_uid = (*paccet).job_id;
                header.data_len = 0;
                send_to = (*paccet).host;
                int_var = 0;
                break;

// Restart one module at the host
            case SMC_RESTART_MODULE:
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Command SMC_RESTART_MODULE from user %u for process %u (server %u)", cd.src_uid, (*paccet).job_id, (*paccet).user_id);
                header.command = ADC_RESTART_JOB;
                header.param2 = (*paccet).user_id;
                header.dst_uid = (*paccet).job_id;
                header.data_len = 0;
                cd.status = E_OK;
                send_to = (*paccet).host;
                int_var = 0;
                break;

// Restart all modules at the host
            case SMC_RESTART_ALL_MODULES_ON_HOST:
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Command SMC_RESTART_ALL_MODULES_ON_HOST from user %u", cd.src_uid);
                header.command = ADC_RESTART_ALLJOBS;
                cd.dst_uid = (uint16_t)0xffff;
                cd.param2  = (uint16_t)0xffff;
                header.data_len = 0;
                cd.status = E_OK;
                send_to = (*paccet).host;
                int_var = 0;
                break;

// Restart all module at the claster
            case SMC_RESTART_ALL_MODULES_ON_CLASTER:
                RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "adcp_service_task: Command SMC_RESTART_ALL_MODULES_ON_CLASTER from user %u", cd.src_uid);
                header.command = ADC_RESTART_ALLJOBS;
                cd.dst_uid = (uint16_t)0xffff;
                cd.param2  = (uint16_t)0xffff;
                header.data_len = 0;
                cd.status = E_OK;
               /* It is supposed all modulues to work with a clean state */
                (*Env).rsdu_stop_flag = 0;
                send_to = SEND_TO_ALL;
                int_var = 0;
                break;

            default:
                RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "adcp_service_task: Case default: Command is %u, uid %u", cd.command, cd.src_uid);
                cd.param1 = 0;
                cd.data_len = 0;
                cd.status = DCP_PROTONOCORRECT;
                int_var = 1;
                break;
            }


========================================================================================

SELECT sinfo.id_user, HOST.host_num, sini.VALUE , dir3.id, dir1.NAME
FROM sys_appl a,
ad_sinfo sinfo,
ad_dir dir1,
ad_dir dir2,
ad_dir dir3,
ad_hosts HOST,
ad_sinfo_ini sini,
rsdu_ini_param ini
WHERE LOWER(a.alias) = 'bridged'
AND sinfo.id_appl = a.ID
AND dir1.ID = sinfo.id_server_node
AND dir2.ID = dir1.id_parent
AND dir3.ID = dir2.id_parent
AND dir3.ID = HOST.id_host_node
AND sini.id_server_node = sinfo.id_server_node
AND sini.id_ini_param = ini.ID
AND ini.NAME = 'MASTER_HOST'  ;


ID_USER	HOST_NUM	VALUE	ID	NAME

10870	0	10.100.1.30	3000730	Мост для взаимодействия комлексов ОИК№1
10880	0	10.100.1.20	3000731	Мост для взаимодействия комлексов ОИК№2
10889	0	10.100.1.100	3000015	Мост для взаимодействия комлексов БДТИ№1
10899	0	10.100.1.102	3000016	Мост для взаимодействия комлексов БДТИ№2

select ip.IP_ADDR from ad_ipinfo ip , ad_dir dir where ip.ID_NODE = dir.ID and dir.id_parent = %ld

select ip.IP_ADDR from ad_ipinfo ip , ad_dir dir where ip.ID_NODE = dir.ID and dir.id_parent = 3000730     -- 10.100.1.20
select ip.IP_ADDR from ad_ipinfo ip , ad_dir dir where ip.ID_NODE = dir.ID and dir.id_parent = 3000731     -- 10.100.1.30
select ip.IP_ADDR from ad_ipinfo ip , ad_dir dir where ip.ID_NODE = dir.ID and dir.id_parent = 3000015     -- 10.100.1.100
select ip.IP_ADDR from ad_ipinfo ip , ad_dir dir where ip.ID_NODE = dir.ID and dir.id_parent = 3000016     -- none



uint16_t unsigned short int  = H
short int   int16_t  h
unsigned int      uint32_t; I
unsigned char B
char[] s

typedef struct SYSMON_TCP
{
    H    user_id;
    H    job_id;
    H    host_count;
    H    host;
    union
    {
      {
        h     SessionErr;
        H    SlotStatus;
        H    user_id;
        {
          uint16_t type;
          {
            I ip_addr;
            H ip_port;
          }  addr_ip;
         }addr;
       } host_addr;
       s64   HostName[64];
       H        JobNumber;
       {
         H        user_id;
         H        JobID; 
         h         JobStatus;
         s80   JobName[80];
         s80   LoadName[80];
         H        TaskNumber;
         s80   MaxPriority;
         s80   ExceptMode;
         I        UsedMem;
         I        AvailMem;
        } JobInfo[32];
       } mbh_i;
     } data;
} SYSMON_TCP;


fm2="HHhs80s80Hs80s80II"
fm232=fm*32

fm1="HHHHhHHIHs64H"
fm=fm1+fm232

sending
08f00000ffff0000ffff0000ffff0000ffff000000000000ffffffff00000000000000000000000000000000000000000000000000000000
08f00000ffff0000ffff0000ffff0000ffff000000000000ffffff0f00000000000000000000000000000000000000000000000000000000

ret
06f000008e2a0000ffff000004000000ffff000000000000ffffffff0000000000000000000000000000000000000000000000000000
06f000008e2a0000ffff000004000000ffff000000000000ffffffff0000000000000000000000000000000000000000000000000000


0000f0080000ffff0000ffff0000ffff0000ffff0000000000000000ffffffff00000000000000000000000000000000000000000000


/* Справочник поддерживаемых протоколов */

#define ADV_PROTO_COMMON                 0 /* Протокол агента, брать значение из RSDU_GLOBAL */
#define ADV_PROTO_TCP                    1 /* TCP протокол            */
#define ADV_PROTO_UDP                    2 /* User Datagram protocol  */
#define ADV_PROTO_NCS                    3 /* Nucleus Communication Services */
#define ADV_PROTO_OPERATOR               4 /* Ручной ввод оператора   */
#define ADV_PROTO_CALC                   5 /* Математический расчет   */
#define ADV_PROTO_FIFO                   6 /* Именованный программный канал */
#define ADV_PROTO_DDA                    7 /* Непосредственный доступ к данным */
#define ADV_PROTO_ETH                    8 /* Ethernet Services       */
#define ADV_PROTO_UDP_CLEAR              9 /* Чистый UDP без подтверждений    */

typedef enum TRANSPORT_PROTO_
{
  TransportProto_Common        = 0,
  TransportProto_TCP           = 1,
  TransportProto_UDP           = 2,
  TransportProto_NCS           = 3,
  TransportProto_OPERATOR      = 4,
  TransportProto_CALC          = 5,
  TransportProto_FIFO          = 6,
  TransportProto_DDA           = 7,
  TransportProto_ETH           = 8,
  TransportProto_UDP_CLEAR     = 9
} TRANSPORT_PROTO_t;

#define IP_ADDR_ALL (uint32_t)0x00000000

----------------------------------------------------------------------------------------------------------------

  //Посылаем серверу сменить статус на резервный
  if(MasterBridged.jobid != 0) //Если не 0, значит нашли его
  {
    sm_hdr.tcp_h.src_uid = (unsigned short)_userInfo.ulUID;
    sm_hdr.tcp_h.param1 = (unsigned short)_userInfo.ulGID;
    sm_hdr.tcp_h.src_laws.laws = _laws;
    sm_hdr.tcp_h.dst_uid = _smUid;
    sm_hdr.tcp_h.param2 = _smGid;
    sm_hdr.tcp_h.param3 = 0;
    sm_hdr.tcp_h.command = SMC_MAKE_RESERVE_RSDU_JOB;
    sm_hdr.tcp_h.data_len = sizeof(unsigned short) * 4;
    sm_hdr.tcp_h.status = DCP_OK;
    sm_hdr.tcp_h.time1970 = (unsigned int)time(NULL);

    sm_hdr.host = (USHORT)MasterBridged.hostid;
    sm_hdr.job_id = (USHORT)MasterBridged.jobid;
    sm_hdr.user_id =  (USHORT)MasterBridged.id;

    if(Send(&sm_hdr, sizeof(UNITRANS_HEADER) + sizeof(unsigned short)*4) == SOCKET_ERROR)
      return -1;

    if(Receive(&hdr, sizeof(hdr)) == SOCKET_ERROR)
      return -1;
  }
  //Закрываем соединение
  hdr.src_uid = (unsigned short)_userInfo.ulUID;
  hdr.param1 = (unsigned short)_userInfo.ulGID;
  hdr.src_laws.laws = _laws;
  hdr.dst_uid = _smUid;
  hdr.param2 = _smGid;
  hdr.param3 = 0;
  hdr.command = GCMD_DATASERVICEEXIT;
  hdr.data_len = 0;
  hdr.status = E_OK;
  hdr.time1970 = (unsigned int)time(NULL);

  Send(&hdr, sizeof(hdr));


/*Команды серверам данных*/
#define GCMD_GLOBAL_STATE_STOPPED       (uint16_t)0xf00f      /* Текущее состояние - остановлен */
#define GCMD_GET_GLOBAL_STATE           (uint16_t)0xf008      /* Получить статус сервера (SYSTEMMASTER или SYSTEMSLAVE) */
#define GCMD_GLOBAL_STATE_MASTER        (uint16_t)0xf009      /* Ответ сервера SYSTEMMASTER */
#define GCMD_GLOBAL_STATE_SLAVE         (uint16_t)0xf00a      /* Ответ сервера SYSTEMSLAVE */
#define GCMD_SET_GLOBAL_STATE_MASTER    (uint16_t)0xf00b      /* Set SYSTEMMASTER */
#define GCMD_SET_GLOBAL_STATE_SLAVE     (uint16_t)0xf00c      /* Set SYSTEMSLAVE */
#define GCMD_SEND_TO_SLAVE              (uint16_t)0xf00d      /* Отправить данные резервному комплексу */
#define GCMD_ECHO                       (uint16_t)0xfffe      /* Эхо - запрос*/
#define GCMD_DATASERVICEINIT            (uint16_t)0xf006      /* Этой командой отвечает dbcpd*/
#define GCMD_DATASERVICEEXIT            (uint16_t)0xf007      /* Эта команда используется для завершения сеанса с dbcpd*/
#define E_DELETE_YOURSELF               (uint16_t)0xFFED      /* Unload JOB */
#define SYS_DELETE_YOURSELF             (uint16_t)0xFFDD      /* Unload JOB */
#define SYS23_DELETE_YOURSELF           (uint16_t)0x00FF      /* iRMX III 2.3 */
/* необходимо для сервера доступа  / добавил Кранчев Д.Ф. 27.01.04 */
#define GCMD_GETDATASERVICEINFO         (uint16_t)0xf003
#define GCMD_GETDATASERVICESTAT         (uint16_t)0xf004
#define GCMD_DATASERVICEDISPOSE         (uint16_t)0xf005
/* необходимо для щита  / добавил Кранчев Д.Ф. 20.04.04 */
#define GERR_UNKNOWNCOMMAND             0x2105



/* Права пользователя */
typedef struct KEY_LAWS {
    uint32_t  laws;
    char      key[16];
} KEY_LAWS;

/* Универсальный заголовок пакетов для передачи данных в комплексе РСДУ */
typedef struct UNITRANS_HEADER {
    uint32_t  command;
    uint32_t  src_uid;
    uint32_t  dst_uid;
    uint32_t  param1;    /*  Как правило, количество передаваемых параметров */
    uint32_t  param2;    /*  Как правило, глобальный тип данных */
    uint32_t  param3;    /*  Бывает, что здесь передается какое-либо время(например последнего запроса, диспетчерской ведомости и т.д. */
    KEY_LAWS  src_laws;
    uint32_t  data_len;
    int16_t   status;
    uint32_t  time1970;
} UNITRANS_HEADER;/* Универсальный заголовок пакетов для передачи данных в комплексе РСДУ */


/*Состояние комплекса - основной/резервный*/
#define SYSTEMMASTER            (uint32_t)0                 /* For RSDU2STATE var. & RSDU_GLOBAL.SystemMode */
#define SYSTEMSLAVE             (uint32_t)1                 /* For RSDU2STATE var. & RSDU_GLOBAL.SystemMode */

#define SMC_GET_MBII_HOST_INFO      (uint16_t)100
SMC_GET_HOST_SYS_INFO       (uint16_t)101
SMC_GET_HOST_JOB_INFO       (uint16_t)102
SMC_DEL_HOST_SYS_JOB        (uint16_t)103
#define SMC_DEL_HOST_SYS_JOB_FORCE  (uint16_t)104
#define SMC_REBOOT_ANY_HOST         (uint16_t)105
#define SMC_SHUTDOWN_HOST           (uint16_t)106
SMC_SHUTDOWN_ALL_HOSTS      (uint16_t)107
SMC_REINIT_RSDU_JOB         (uint16_t)108
#define SMC_REINIT_RSDU_JOBS        (uint16_t)109
#define SMC_REINIT_ALL_RSDU_JOBS    (uint16_t)110
#define SMC_RELOAD_RSDU_JOB         (uint16_t)111
#define SMC_MAKE_RESERVE_RSDU_JOB   (uint16_t)112
#define SMC_MAKE_RESERVE_RSDU_JOBS  (uint16_t)113
#define SMC_TEST_RSDU_JOBS          (uint16_t)114
#define SMC_SET_TIME_ON_HOST        (uint16_t)115
#define SMC_SET_TIME_ON_ALL_HOST    (uint16_t)116
#define SMC_SET_GLOBAL_TIME         (uint16_t)117
SMC_CLEARL_RSDU_JOB_INFO    (uint16_t)118
#define SMC_RESET_MBII_SYSTEM       (uint16_t)119
#define SMC_SHUTDOWN_RSDU           (uint16_t)120
#define SMC_SEND_TICK_SIGNAL        (uint16_t)121
#define SMC_GET_TASK_INFO           (uint16_t)122
#define SMC_SET_TIME_SOURCE         (uint16_t)123
SMC_REINIT_DB               (uint16_t)124
#define SMC_GET_JOB_DIR             (uint16_t)126
#define SMC_GET_SERVER_ID           (uint16_t)127 /* Get server ID from table dv_dir type_header.src_uid */

SMC_RESTART_MODULE          (uint16_t)128 // restart one modulte on the host
SMC_RESTART_ALL_MODULES_ON_HOST (uint16_t)129 // restart all modules no the host, exclude bridged, sysmond, lsmagentd
SMC_RESTART_ALL_MODULES_ON_CLASTER (uint16_t)130 // restart all modules on claster, exclude bridged, sysmond, lsmagentd

/*Команды серверам данных*/
#define GCMD_GLOBAL_STATE_STOPPED       (uint16_t)0xf00f      /* Текущее состояние - остановлен */
#define GCMD_GET_GLOBAL_STATE           (uint16_t)0xf008      /* Получить статус сервера (SYSTEMMASTER или SYSTEMSLAVE) */
#define GCMD_GLOBAL_STATE_MASTER        (uint16_t)0xf009      /* Ответ сервера SYSTEMMASTER */
#define GCMD_GLOBAL_STATE_SLAVE         (uint16_t)0xf00a      /* Ответ сервера SYSTEMSLAVE */
#define GCMD_SET_GLOBAL_STATE_MASTER    (uint16_t)0xf00b      /* Set SYSTEMMASTER */
#define GCMD_SET_GLOBAL_STATE_SLAVE     (uint16_t)0xf00c      /* Set SYSTEMSLAVE */
#define GCMD_SEND_TO_SLAVE              (uint16_t)0xf00d      /* Отправить данные резервному комплексу */
#define GCMD_ECHO                       (uint16_t)0xfffe      /* Эхо - запрос*/
#define GCMD_DATASERVICEINIT            (uint16_t)0xf006      /* Этой командой отвечает dbcpd*/
#define GCMD_DATASERVICEEXIT            (uint16_t)0xf007      /* Эта команда используется для завершения сеанса с dbcpd*/
#define E_DELETE_YOURSELF               (uint16_t)0xFFED      /* Unload JOB */
#define SYS_DELETE_YOURSELF             (uint16_t)0xFFDD      /* Unload JOB */
#define SYS23_DELETE_YOURSELF           (uint16_t)0x00FF      /* iRMX III 2.3 */
/* необходимо для сервера доступа  / добавил Кранчев Д.Ф. 27.01.04 */
#define GCMD_GETDATASERVICEINFO         (uint16_t)0xf003
#define GCMD_GETDATASERVICESTAT         (uint16_t)0xf004
#define GCMD_DATASERVICEDISPOSE         (uint16_t)0xf005
/* необходимо для щита  / добавил Кранчев Д.Ф. 20.04.04 */
#define GERR_UNKNOWNCOMMAND             0x2105

/* Функция бродкастом рассылает на порт ADMCPORT (порт, который занимает агент системного монитора,
на который посылаются команды администрирования) команду cmd.
Кроме этого внутри функции определена посылка сигнала  signal о состоянии задачи.
*/
void SendBroadcast(BRIDGE_ENV  *Env, COMM_PORT * pSrv_port, uint16_t cmd, uint32_t signal, LOG_HEADER*  fp)
{
    UNITRANS_HEADER type_header;
    REM_ADDR        gen_addr;
    uint16_t        status = E_OK;
    int32_t         SetBroadcast = 0;
    int32_t         NeedToSendBroadcast = 0;

    switch(cmd)
    {
    case GCMD_SET_GLOBAL_STATE_SLAVE:
        Env->GlobalState = SYSTEMSLAVE;
        NeedToSendBroadcast = 1;
        break;

    case GCMD_SET_GLOBAL_STATE_MASTER:
        Env->GlobalState = SYSTEMMASTER;
        NeedToSendBroadcast = 1;
        break;

    default:
        NeedToSendBroadcast = 1;
        break;
    }

    if (NeedToSendBroadcast == 1)
    {
        if (pSrv_port != NULL)
        {
            SetBroadcast = 1;
            setsockopt((*pSrv_port).service.port_ip.sock, SOL_SOCKET, SO_BROADCAST, (char*)&SetBroadcast, (socklen_t)sizeof(SetBroadcast));

            RSDURTGUtils_ToBcAddress(&gen_addr,ADMCPORT,ADV_PROTO_COMMON);
            memset(&type_header, 0, sizeof(type_header));
            type_header.command = cmd;
            type_header.status = ADERR_NOTANSWER;
            type_header.dst_uid = 0xffff;
            type_header.src_uid = (*Env).UserID;
            type_header.data_len = 0;
            type_header.param2 = 1010;
            type_header.param1 = (*Env).GroupID;
            type_header.src_laws.laws = 0x0;
            type_header.time1970 = RSDURTGUtils_Time70();
            RSDURTGUtils_SendCommPort( (COMM_PORT  *)pSrv_port,
                (REM_ADDR  *)&gen_addr,
                (UNITRANS_HEADER  *)&type_header,
                (unsigned char  *)0,
                (uint16_t)100,
                &status,
                fp);

            SetBroadcast = 0;
            setsockopt((*pSrv_port).service.port_ip.sock, SOL_SOCKET, SO_BROADCAST, (char*)&SetBroadcast, (socklen_t)sizeof(SetBroadcast));
        }
    }


///////////////////////////////////////////////////////////////////////////////////////////////
int StatusChanger::SendChangeStatus()
{
	UNITRANS_HEADER hdr;
	SYSMON_TCP  sm_hdr;

	if(Connect(MasterBridged.IpAddress) == FALSE)
		return -1;

	if(Receive(&hdr, sizeof(hdr)) == SOCKET_ERROR)
	{
		Message msg("");
		strErrorMessage = CString("Неудачная попытка соединения с системным монитором!\n")  + msg.GetError();
		return -1;
	}

	if(hdr.command == GCMD_DATASERVICEINIT)
	{
		if(hdr.status == 0)
		{
			_smUid = (unsigned short)hdr.src_uid;
			_smGid = (unsigned short)hdr.param1;
		}
		else
		{
			strErrorMessage = "Получен ответ с ошибкой, неудачное соединение с системным монитором!";
			return -1;
		}
	}

	Sleep(100);

	//Посылаем серверу сменить статус на резервный
	if(MasterBridged.jobid != 0) //Если не 0, значит нашли его
	{
		sm_hdr.tcp_h.src_uid = (unsigned short)_userInfo.ulUID;
		sm_hdr.tcp_h.param1 = (unsigned short)_userInfo.ulGID;
		sm_hdr.tcp_h.src_laws.laws = _laws;
		sm_hdr.tcp_h.dst_uid = _smUid;
		sm_hdr.tcp_h.param2 = _smGid;
		sm_hdr.tcp_h.param3 = 0;
		sm_hdr.tcp_h.command = SMC_MAKE_RESERVE_RSDU_JOB;
		sm_hdr.tcp_h.data_len = sizeof(unsigned short) * 4;
		sm_hdr.tcp_h.status = DCP_OK;
		sm_hdr.tcp_h.time1970 = (unsigned int)time(NULL);

		sm_hdr.host = (USHORT)MasterBridged.hostid;
		sm_hdr.job_id = (USHORT)MasterBridged.jobid;
		sm_hdr.user_id =  (USHORT)MasterBridged.id;

		if(Send(&sm_hdr, sizeof(UNITRANS_HEADER) + sizeof(unsigned short)*4) == SOCKET_ERROR)
			return -1;

		if(Receive(&hdr, sizeof(hdr)) == SOCKET_ERROR)
			return -1;
	}
	//Закрываем соединение
	hdr.src_uid = (unsigned short)_userInfo.ulUID;
	hdr.param1 = (unsigned short)_userInfo.ulGID;
	hdr.src_laws.laws = _laws;
	hdr.dst_uid = _smUid;
	hdr.param2 = _smGid;
	hdr.param3 = 0;
	hdr.command = GCMD_DATASERVICEEXIT;
	hdr.data_len = 0;
	hdr.status = E_OK;
	hdr.time1970 = (unsigned int)time(NULL);

	Send(&hdr, sizeof(hdr));

	sockSM.Close();

	return 0;
}

size = 5580

data.adm_i.SysInfo
p_sm_data[j]->data.mbh_i.JobInfo[i]

data.mbh_i.JobNumber


HHHHhHHHIH64BH  HHh80B80BHBBII


typedef struct SYSMON_TCP5
{
    H uint16_t    user_id;
    H uint16_t    job_id;
    H uint16_t    host_count;
    H uint16_t    host;
	//MB_HOST_INFO    mbh_i;
    h int16_t     SessionErr;
    H uint16_t    SlotStatus;
    H uint16_t    user_id2222222222;
    struct {
	 H	uint16_t type; /* тип протокола - описаны в db2const.h */
      union
      {

        struct {
   B unsigned char    addrlength;
   B  unsigned char    unused;
   H    uint16_t         portid;
  28B   unsigned char    address[28];
} addr_ncs;

        struct 
{
    uint32_t r_sock;
}  addr_eth;

        struct 
{
    I uint32_t ip_addr;
   H uint16_t       ip_port;
}   addr_ip;
	}addr; 

} host_addr;

   B64  unsigned char   HostName[MAX_HOST_NAME_LEN];
   H   uint16_t        JobNumber;
    //JOB_INFO        JobInfo[MAX_JOB_PER_HOST];
 struct JOB_INFO {
  H uint16_t        user_id;
  H  uint16_t        JobID;     /* Zolotov M.Yu. Подставил тип unsigned short вместо типа SELECTOR */
  h  int16_t         JobStatus;
  B80  unsigned char   JobName[80];
  B80  unsigned char   LoadName[80];
  H  uint16_t        TaskNumber;
  B   unsigned char   MaxPriority;
  B   unsigned char   ExceptMode;
  I  uint32_t        UsedMem;
  I  uint32_t        AvailMem;
} JobInfo[MAX_JOB_PER_HOST];
} SYSMON_TCP5;


5864 = длина
5802 = MB_HOST_INFO
REM_ADDR=34
JOB_INFO=178
178 * 32 = 5696
+ 34 = 5730

