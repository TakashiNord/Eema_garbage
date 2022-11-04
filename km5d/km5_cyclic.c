/*
*  NAME:               km5_cyclic.c
*  BEGIN:              01.09.2022
*  COMPANY:            EMA Ltd.
*  DESCRIPTION:        Задача циклического опроса КМ5
*/


#include <netinet/in.h>
#include <pthread.h>
#include <stddef.h>
#include <string.h>
#include <errno.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <rsdu2def.h>
#include <sys_sign.h>
#include <db2const.h>
#include <rmxtbox.h>
#include <utils.h>

#include "dcstree.h"
#include "dcsdrv.h"
//#include <modbus.h>


/************************************************************************/
void *km5_cyclic_task(void *pStartParams)
{
    time_t          CurTaskTime = 0, NextTaskTime = 0;
    int32_t         NumberParamOnDevice = 0;
    int32_t         i = 0;
    uint16_t        status = E_OK;
    POINT_PROP    * (*pPointsOnDevice)[] = NULL;
    DEVICE_PROP   * pDeviceProp = NULL;
    uint32_t        ExchangeStructSize = 0;
    DRIVER_EXCHANGE_STRUCT *  DriverExchange = NULL;
    uint32_t        par_fail = 0, par_not_get = 0;
    DCS_ENV       * Env = NULL;
    LOG_HEADER      LogStruct;
    LOG_HEADER    * LogFile = &LogStruct;
	ListType        QueryList = NULL, WriteList = NULL;
    char            path[MAX_PATH_LEN];
    PORT_PROP     * pPortProp = (PORT_PROP*)0;
    int32_t         GoMimo = 0;
    sem_t         * ExchangeSyncSem = NULL;  /* Семафор для синхронизации работы с драйвером устройства */
    uint32_t        BaseExchangePriority = DRIVERPRIORITY_CYCLIC;
    uint32_t        TaskPeriod = GLT_TIMEOUT_0;/* Период работы */
    uint32_t        FlushFlag; /* Флаг объявления параметров устройства недостоверными */
    uint32_t        ReqDataDelay  = 0; /* Minimal query period, 10*ms */

    Env = (DCS_ENV*)((DCS_START_ENV*)pStartParams)->pEnv;
    pDeviceProp = (DEVICE_PROP*)((DCS_START_ENV*)pStartParams)->pParam;

    /* Если драйвер создавать не для кого, то ругнемся и завершимся */
    if ((pDeviceProp == NULL) || (GetCyclicTaskState(pDeviceProp, pDeviceProp->TaskPeriod) != TASK_STATE_CREATED))
        RSDURTGUtils_ErrCheck(LogFile, ERR_RMX, __LINE__, __FILE__, E_PARAM, ERR_DELETE, "km5_cyclic_task: Unknown cyclic task to start ");
    else
        TaskPeriod = pDeviceProp->TaskPeriod;

    sprintf(path, "%skm5_cyclic_task_%04u.log", Env->LogFilePath, (*pDeviceProp).id);
    RSDURTGUtils_LogInit(LogFile, DCS_KM5, Env->LogFileName, path, __FUNCTION__);

    pPortProp = pDeviceProp->PtrPort;
    if (NULL == pPortProp)
        RSDURTGUtils_ErrCheck(LogFile, ERR_RMX, __LINE__, __FILE__, E_PARAM, ERR_DELETE, "km5_cyclic_task: pPortProp is NULL! ");

    RSDURTGUtils_CreateSemaphore((uint16_t) 0, (void*)&ExchangeSyncSem, (uint16_t) 4, FIFO_QUEUING , &status);
    RSDURTGUtils_ErrCheck(LogFile, ERR_RMX, __LINE__, __FILE__, (int32_t)status, ERR_DELETE, (const char *) "km5_cyclic_task: RSDURTGUtils_CreateSemaphore ExchangeSyncSem! ");

    SetCyclicTaskState(pDeviceProp, TaskPeriod, TASK_STATE_READY);
    /* Инициализация состояния передачи - циклов обмена не было */
    pDeviceProp->TransState = TRANS_STATE_EMPTY;

    RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "Start KM% cyclic task(TaskPeriod=%u), iface %u, port = %u, id = %u, DevNumber = %u",
        TaskPeriod, (*(*pPortProp).PtrIface).IfaceType, (*pPortProp).PortNumber,
        pDeviceProp->id, pDeviceProp->DevNumber);

    /* Signal that the shared data has been read */
    RSDURTGUtils_SendUnits((void*)&(*pPortProp).DrvTaskSyncSem, (uint16_t)1, &status);
    RSDURTGUtils_ErrCheck(LogFile, ERR_RMX, __LINE__, __FILE__, (int32_t)status, ERR_CONTINUE, "km5_cyclic_task: Send units to SyncSem ");

	QueryList = ListCreate((int32_t)sizeof(POINT_PROP *));
    WriteList = ListCreate((int32_t)sizeof(POINT_PROP *));
    if (QueryList == (ListType)0 || WriteList == (ListType)0)
    {
        RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "km5_cyclic_task: ListCreate at line %d ", __LINE__);
        GoMimo = 1;
    }
    else
    {
        NumberParamOnDevice = ListNumItems(pDeviceProp->DevPoint);
        pPointsOnDevice = (POINT_PROP * (*)[])ListGetDataPtr(pDeviceProp->DevPoint);
    }
    
/* Формируем структуру для отправки драйверу */
    ExchangeStructSize = DRIVER_EXCHANGE_STRUCT_MAIN_LENGTH + sizeof(KM5_DATA) * NumberParamOnDevice;
    DriverExchange = (DRIVER_EXCHANGE_STRUCT  *)RSDURTGUtils_SMalloc(ExchangeStructSize);
    if (NULL == DriverExchange)
    {
        RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "km5_cyclic_task: RSDURTGUtils_SMalloc FAIL for Device %u, DevNumber = %u! ",
                                   pDeviceProp->id, pDeviceProp->DevNumber);
        GoMimo = 0;
    }

    /************************************************************************/
    /* Основной цикл работы */
    CurTaskTime = RSDURTGUtils_Time70();
    /* Первый цикл обмена должен обязательно состояться, вне зависимости от текущего времени чтобы получить текущие измерения */
    NextTaskTime = CurTaskTime;

    FlushFlag = TRUE; /* Флаг сброса статуса параметров устройства */
	ReqDataDelay = (*pDeviceProp).Option.IECReqDataDelay.Value;
    if (ReqDataDelay < DCS_DRIVER_MINSLEEP) ReqDataDelay = DCS_DRIVER_MINSLEEP;

    while (0 == GoMimo)
    {
        if (GetCyclicTaskState(pDeviceProp, TaskPeriod) == TASK_STATE_ABORT)
            break;

// В режиме SLAVE мы не должны производить опрос устройства (исключение -- активный резерв)
        if( RSDURTGUtils_GetGlobalStatus() == SYSTEMSLAVE &&  ((*pDeviceProp).PtrPort->State & PORT_ACTIVE_RESERVE) == 0 )
        {
            /* На случай если перешли в резервный режим, сбрасываем счетчик опроса значений, чтобы при переходе в основной режим повторить опрос и не ожидать очередного цикла*/
            NextTaskTime = 0;
            RSDURTGUtils_Sleep(DCS_CHECK_DRIVERS, &status); // Ожидаем 1 секунду перед повторной проверкой режима работы.
            continue;
        }
        status = E_OK;
        RSDURTGUtils_Sleep(ReqDataDelay, &status);

        /* Проверяем, что устройство не выведено из работы */
        if ((((*pDeviceProp).State & ADCP_DST_NOWORK) == ADCP_DST_NOWORK)||
            (((*pDeviceProp).PtrPort->State & ADCP_DST_NOWORK) == ADCP_DST_NOWORK) ||
            (((*pDeviceProp).PtrIface->State & ADCP_DST_NOWORK) == ADCP_DST_NOWORK))
        {
            if (FlushFlag == FALSE)
            {
                /* После вывода устройства из работы необходимо объявить параметры недостоверными
                причем не имеет смысла выполнять эту процедуру много раз.
                */
                SetDevicePointStatusFail (Env, pDeviceProp, OIC_DST_NOPRESENT);
                FlushFlag = TRUE;
            }
            continue;
        }
        /* Проверяем, что устройство исправно */
        if ((((*pDeviceProp).State & DEVICE_DST_FAILED) == DEVICE_DST_FAILED))
        {
            continue;
        }

        /* Проверяем пора ли нам работать в зависимости от заданного интервала */
        CurTaskTime = RSDURTGUtils_Time70();
        if(CurTaskTime < NextTaskTime)
            continue;
        if (0 == TaskPeriod)
            NextTaskTime = CurTaskTime;
        else
            NextTaskTime = CurTaskTime - CurTaskTime % TaskPeriod + TaskPeriod + DCS_CYCLIC_SHIFT; /* Задаем следующее время опроса */

        FlushFlag = FALSE;

		ListClear(QueryList);
        ListClear(WriteList);
        par_fail = 0;
        par_not_get = 0;

/* Параметры с устройства получаем одним запросом*/
		DriverExchange->command     = CMD_READ_PARAMETER;
        DriverExchange->time        = RSDURTGUtils_Time70();
        DriverExchange->NumberParam = NumberParamOnDevice;
        DriverExchange->DevNumber   = pDeviceProp->DevNumber;
        DriverExchange->device_id   = pDeviceProp->id;
        DriverExchange->ResponseSem = ExchangeSyncSem;
        DriverExchange->status      = DRIVER_OK;

        if (pPortProp->PtrIface->IfaceType == IFACE_ETHERGATE)
            BaseExchangePriority = DRIVERPRIORITY_CHECK;
        else
            BaseExchangePriority = DRIVERPRIORITY_CYCLIC;
        DriverExchange->Priority = BaseExchangePriority;

        /* Инициализируем запрос */
		/* Формируем список параметров для запроса с устройства */
        for(i=0; i<NumberParamOnDevice; i++)
        {
            if(((*(*pPointsOnDevice)[i]).State & DCP_DST_NOWORK) == DCP_DST_NOWORK)
                continue;

            if(0 == CheckGtOptTime(Env, (*pPointsOnDevice)[i], TaskPeriod))
                continue;

			
			DriverExchange->data.km5[i].DeviceNumber = DriverExchange->DevNumber;
			DriverExchange->data.km5[i].Addr = (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr;
			DriverExchange->data.km5[i].status = DRIVER_DST_PARAM_NOTGET;
		}
		if ((GetCyclicTaskState(pDeviceProp, TaskPeriod) == TASK_STATE_ABORT) || pPortProp->DriverQueue == NULL)
            break;
/* Отправляем команду в очередь драйверу */
        DCSDrQInsertAtom(pPortProp->DriverQueue, DriverExchange, DriverExchange->Priority, &status);
        RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "km5_cyclic_task: DCSDrQInsertAtom, priority %u, trans_id %u",
                                   DriverExchange->Priority, DriverExchange->TransID);

		/* дожидаемся ответа от драйверной задачи в течение N секунд */
        while (TRUE)
        {
            status = E_OK;
            RSDURTGUtils_ReceiveUnits((void*)&ExchangeSyncSem, 1, DCS_DRIVER_EXCHANGE_WAIT, &status);
            if (status != E_OK)
            {
                /* Удаляем команду из очереди  */
                DCSDrQDeleteItem(pPortProp->DriverQueue, DriverExchange, &status);
                if (status == E_EXIST)
                {
                   /* Наша команда в очереди сообщений не найдена:
                      она принята на обработку драйверной задачей, пытаемся выполнить ReceiveUnits еще раз.
                    */
                   continue;
                }
                /* Повышаем приоритет команды и заново пытаемся выполнить ее */
                if (BaseExchangePriority > 0)
                {
                    BaseExchangePriority--;
                }
                DriverExchange->status = DRIVER_EXCHANGE_ERROR;
                break;
            }
            else
            {
                /* Получен ответ от драйверной задачи. Устанавливаем базовый приоритет команды.  */
                if (pPortProp->PtrIface->IfaceType == IFACE_ETHERGATE)
                    BaseExchangePriority = DRIVERPRIORITY_CHECK;
                else
                    BaseExchangePriority = DRIVERPRIORITY_CYCLIC;
                break;
            }
        }/* while (TRUE) */
        RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "km5_cyclic_task: Receive response, status=%u, DriverExchange->status=%u",
                                   status, DriverExchange->status);

            /* Сохраняем статус обмена с устройством для последующего его использования
            в задаче checkdevice - при восстановлении устройства */
        if (pDeviceProp->TransState != TRANS_STATE_OK)
            pDeviceProp->TransState = TRANS_STATE_OK;

        RSDURTGUtils_AutoSem asPointSem((*Env).PointSem);
        asPointSem.Receive(DCS_COMMON_WAIT, &status);
		if (status != E_OK)
        {
            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "km5_cyclic_task: ReceiveUnits(PointSem) Failed (status=%d)", status);
			if (status != E_TIME)
            {
                RSDURTGUtils_Sleep(DCS_COMMON_WAIT, &status);
            }
        }
        else
        {
			for(i = 0; i < NumberParamOnDevice; i++)
            {
				if(DriverExchange->status == DRIVER_OK)
				{
					(*(*pPointsOnDevice)[i]).CurrentVal.status &= ~OIC_DST_VALUENOCORRECT;
					(*(*pPointsOnDevice)[i]).CurrentVal.status &= ~OIC_DST_NOPRESENT;
					(*(*pPointsOnDevice)[i]).CurrentVal.TimeTickUpdate = RSDURTGUtils_TimeStamp();

					/* А в TimeTick должно пойти время с прибора */
					(*(*pPointsOnDevice)[i]).CurrentVal.TimeTick = (*(*pPointsOnDevice)[i]).CurrentVal.TimeTickUpdate;
					if (TaskPeriod > 0)
					{
						/* Приводим текущее время к границе интервала */
						(*(*pPointsOnDevice)[i]).CurrentVal.TimeTick.tv_sec -= (*(*pPointsOnDevice)[i]).CurrentVal.TimeTick.tv_sec % TaskPeriod;
						(*(*pPointsOnDevice)[i]).CurrentVal.TimeTick.tv_usec = 0;
					}
					if (DriverExchange->data.km5[i].status == DRIVER_DST_PARAM_OK)
                    {
                        switch((*(*pPointsOnDevice)[i]).Gtype)
                        {
                        case GLOBAL_TYPE_ANALOG:
                            if (((*(*pPointsOnDevice)[i]).State & DCP_DST_VALUESCALED) == DCP_DST_VALUESCALED)  /* Пересчет из квантов */
                            {
                                (*(*pPointsOnDevice)[i]).CurrentVal.Value.aval = ((( (*(*pPointsOnDevice)[i]).ScaleMax - (*(*pPointsOnDevice)[i]).ScaleMin)
                                    / (*(*pPointsOnDevice)[i]).Scale)* DriverExchange->data.irt[0].value.aval)
                                    + (*(*pPointsOnDevice)[i]).ScaleMin;
                            }
                            else
                                (*(*pPointsOnDevice)[i]).CurrentVal.Value.aval = (*(*pPointsOnDevice)[i]).Scale * DriverExchange->data.km5[i].value.aval; /* Масштабирование ТИ */
                            /* Сохраняем немасштабированное значение */
                            (*(*pPointsOnDevice)[i]).NoScaledCurrentVal.Value.aval = DriverExchange->data.km5[0].value.aval;

                            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "km5_cyclic_task: Command = %u Parameter = %u val=%lf status=%d (id_param %u)",
                                (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.CommandNum, (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.ParameterNum,(*(*pPointsOnDevice)[i]).CurrentVal.Value.aval,
                                DriverExchange->data.km5[i].status,(*(*pPointsOnDevice)[i]).id);
                            break;

                        default:
                            (*(*pPointsOnDevice)[i]).CurrentVal.status |= OIC_DST_VALUENOCORRECT;
                            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "km5_cyclic_task: UNKNOWN TYPE! Command = %u Parameter = %u type = %d (id_param %u)",
													  (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.CommandNum,
													  (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.ParameterNum,
													  (*(*pPointsOnDevice)[i]).Gtype, (*(*pPointsOnDevice)[i]).id);
                            break;
                        }
                    }/* if (DriverExchange->data.irt[0].status == DRIVER_DST_PARAM_OK) */
                    else
                    {
                        if(DriverExchange->data.km5[i].status == DRIVER_DST_PARAM_NOTGET)
                        {
                            par_not_get++;
                            (*(*pPointsOnDevice)[i]).CurrentVal.status |= OIC_DST_VALUENOCORRECT;
                            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "km5_cyclic_task: Command = %u Parameter = %u (id_param %u) NOTGET",
													  (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.CommandNum,
													  (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.ParameterNum,
													  (*(*pPointsOnDevice)[i]).id);
                        }
                        else
                        if(DriverExchange->data.km5[i].status == DRIVER_DST_PARAM_NOCORRECT)
                        {
                            par_fail++;
                            (*(*pPointsOnDevice)[i]).CurrentVal.status |= OIC_DST_VALUENOCORRECT;
                            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "km5_cyclic_task: Command = %u Parameter = %u (id_param %u) NOCORRECT",
										              (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.CommandNum,
													  (*(*pPointsOnDevice)[i]).PointAddr.KM5_Addr.ParameterNum,
													  (*(*pPointsOnDevice)[i]).id);
                        }
                    }
                }/* if(DriverExchange->status == DRIVER_OK) */
                else
                {
                    /* Если драйвер не смог обработать текущий запрос значений, пробуем следующий цикл опросить */
                    NextTaskTime = 0;
                    /* Драйвер вернул не ОК - запрашиваемый параметр объявляем недостоверным */
                    par_not_get++;
                    (*(*pPointsOnDevice)[i]).CurrentVal.status |= OIC_DST_VALUENOCORRECT;

                }
                /* Сохраняем статус и время немасштабированного значения */
                (*(*pPointsOnDevice)[i]).NoScaledCurrentVal.status = (*(*pPointsOnDevice)[i]).CurrentVal.status;
                (*(*pPointsOnDevice)[i]).NoScaledCurrentVal.TimeTick = (*(*pPointsOnDevice)[i]).CurrentVal.TimeTick;

                /* Для немгновенных параметров обновляем ретроспективу */
                if (TaskPeriod > 0)
                {
                    UpdateRetroVal((*pPointsOnDevice)[i]);
                }
                if (CheckPointUpdated((*pPointsOnDevice)[i]))  /* Значение параметра изменилось ? */
                {
                    (*(*pPointsOnDevice)[i]).LastSentVal = (*(*pPointsOnDevice)[i]).CurrentVal;
                    (*(*pPointsOnDevice)[i]).NoScaledLastSentVal = (*(*pPointsOnDevice)[i]).NoScaledCurrentVal;
                    //Перенесено в объект PointValue
                    //SendToADCPUpdatedValue(Env, (*pPointsOnDevice)[i]);
                }
		    }/* for(i=0; i<NumberParamOnDevice; i++) */
			asPointSem.Send(&status);
			if (status != E_OK)
			{
				RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "km5_cyclic_task: SendUnits(PointSem) Failed (status=%d)", status);
			}
        }/* else if (status != E_OK) */
    }/* while (0 == GoMimo) */

    if (DriverExchange != NULL)
    {
        /* Удаляем команду из очереди, если она там осталась */
        RSDURTGUtils_SFree(DriverExchange);
        DriverExchange = NULL;
    }
    if (ExchangeSyncSem != NULL)
    {
        status = E_OK;
        RSDURTGUtils_DropSemaphore((void*)&ExchangeSyncSem, &status);
        if (status != E_OK)
        {
            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "km5_cyclic_task: RSDURTGUtils_DropSemaphore error, errno=%d, status=%d ", errno, status);
        }
    }

    RSDURTGUtils_UnilogMessage(LOG_INFO, LogFile, "End KM5 cyclic task((TaskPeriod=%u, GoMimo=%d) for id = %u, DevNumber = %u",
        TaskPeriod, GoMimo, (*pDeviceProp).id, pDeviceProp->DevNumber);

    RSDURTGUtils_LogClose(LogFile);

    SetCyclicTaskState(pDeviceProp, TaskPeriod, TASK_STATE_EMPTY);
    RSDURTGUtils_ThrExit(0, NULL, &status);
    return NULL; /* Dummy exit */
}
