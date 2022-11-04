/*
*  NAME:               km5func.c
*  BEGIN:              01.09.2022
*  COMPANY:            EMA Ltd.
*  DESCRIPTION:
*  AUTHOR:             
*  LAST MODIFICATIONS:
*/

#include <string.h>
#include <errno.h>
#include <stdio.h>
#include <strings.h>
#include <unistd.h>
#include <ctype.h>

#include <math.h>
#include <stdlib.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <rsdu2def.h>
#include <oicdcpi.h>
#include <rmxtbox.h>
#include <utils.h>
#include <dcs.h>
#include <dcsdrv.h>
#include <sys_sign.h>

#include "km5proto.h"

uint16_t KM5_check_sum(char *msg, int32_t len) {
  int i, l;
  uint16_t sum;
//TODO необходимо реализовать функцию
  return sum;
}

ListType KM5TCPOpenDriverCmd(DCS_ENV *Env, PORT_PROP  *PtrPortProp, LOG_HEADER* LogFile)
{
    TCP_PORT_PARAMETERS  (*TCPDriverParameters)[] = (TCP_PORT_PARAMETERS (*)[])0;
    DEVICE_PROP         *(*PtrDevices)[] = NULL;

    uint32_t    NumConnections = 0, i = 0;
    int         sock = 0;
    uint16_t    port = 0;
    ListType    TCPList = NULL;
    time_t      ct = 0;

    RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "KM5TCPOpenDriverCmd");

    if (PtrPortProp == (PORT_PROP*)0)
    {
        RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPOpenDriverCmd: Fatal error PtrPortProp is NULL! ");
        return NULL;
    }

    PtrDevices = (DEVICE_PROP *(*)[])ListGetDataPtr(PtrPortProp->PortDev);
    if (PtrPortProp->PtrIface == NULL)
    {
        RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"KM5TCPOpenDriverCmd: Fatal error PtrPortProp->PtrIface is NULL!");
        return TCPList;
    }

    TCPList = ListCreate((int32_t)sizeof(TCP_PORT_PARAMETERS));
    if (TCPList == NULL)
    {
        RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"KM5TCPOpenDriverCmd: Fatal error E_MEM!");
        return TCPList;
    }

    if (PtrPortProp->PtrIface->IfaceType == IFACE_ETHERNET)
    {
        NumConnections = ListNumItems(PtrPortProp->PortDev);
        if (NumConnections <= 0)
        {
            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPOpenDriverCmd: ListNumItems(PtrPortProp->PortDev)=%d ", NumConnections);
            if (TCPList != (ListType)0)
            {
                ListDispose(TCPList);
                TCPList = NULL;
            }
            return NULL;
        }
        TCPDriverParameters = (TCP_PORT_PARAMETERS (*)[])RSDURTGUtils_SMalloc(sizeof(TCP_PORT_PARAMETERS) * NumConnections);
        if (TCPDriverParameters == (TCP_PORT_PARAMETERS (*)[])0)
        {
            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPOpenDriverCmd: RSDURTGUtils_SMalloc(TCPDriverParameters) error, %d bytes, at %d line",
                (int32_t)sizeof(TCP_PORT_PARAMETERS) * NumConnections, __LINE__);
            if (TCPList != (ListType)0)
            {
                ListDispose(TCPList);
                TCPList = NULL;
            }
            return NULL;
        }
        for (i=0; i<NumConnections; i++)
        {
            /* Заполняем настройки */
            port = 0;
            (*TCPDriverParameters)[i].MsgID = 0;
            (*TCPDriverParameters)[i].PortState = PORT_STATE_EMPTY;
            (*TCPDriverParameters)[i].FailedTime = 0;
            (*TCPDriverParameters)[i].ProtoType = IFACE_ETHERNET;

            (*TCPDriverParameters)[i].PortNumber = (*(*PtrDevices)[i]).PortNumber;
            memcpy(&(*TCPDriverParameters)[i].HostAddr[0],&(*(*PtrDevices)[i]).HostAddr[0],16);
            (*TCPDriverParameters)[i].BaudRate = PtrPortProp->BaudRate;

            (*TCPDriverParameters)[i].DeviceID = (*(*PtrDevices)[i]).DevNumber;
            (*TCPDriverParameters)[i].device_addr.sin_family = AF_INET;
            (*TCPDriverParameters)[i].device_addr.sin_port = htons((uint16_t)(*(*PtrDevices)[i]).PortNumber);
            (*TCPDriverParameters)[i].device_addr.sin_addr.s_addr = inet_addr((char *)&(*(*PtrDevices)[i]).HostAddr[0]);
            (*TCPDriverParameters)[i].sock = 0;

            if ((((*(*PtrDevices)[i]).State & ADCP_DST_NOWORK)== ADCP_DST_NOWORK) ||
                (((*(*PtrDevices)[i]).PtrPort->State & ADCP_DST_NOWORK) == ADCP_DST_NOWORK) ||
                (((*(*PtrDevices)[i]).PtrIface->State & ADCP_DST_NOWORK) == ADCP_DST_NOWORK))
                continue;

            sock = RSDURTGUtils_bound_stream_socket ((uint16_t *)&port, (uint32_t)0xffff, LogFile);
            if(sock < 0)
            {
                RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"KM5TCPOpenDriverCmd: Can't create SOCKET! ");
                continue;
            }

            /* Пытаемся приконнектиться к устройству */

            RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "KM5TCPOpenDriverCmd: connect %s:%d",
            		&(*(*PtrDevices)[i]).HostAddr[0], (*TCPDriverParameters)[i].device_addr.sin_port);

            if (connect(sock,(struct sockaddr *)&(*TCPDriverParameters)[i].device_addr, (socklen_t)sizeof(struct sockaddr)) < 0)
            {
                // REVIEW <Вагин А.А.><29.01.2008> Дублирование кода. Есть функции типа SetPortDeviceStatusFail
                // которые как раз посылают сигнал 1 раз. Об этом больше не пишу, но применяется ко всем драйверам из DAD.
                /* Сигнал отправляем лишь единожды */
                if (((*(*PtrDevices)[i]).State & DEVICE_DST_FAILED) != DEVICE_DST_FAILED)
                {
                    RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPOpenDriverCmd: Device failed! id=%u addr=%s port=%u ",
                        (*(*PtrDevices)[i]).id,(*(*PtrDevices)[i]).HostAddr,(*(*PtrDevices)[i]).PortNumber);
                    (*(*PtrDevices)[i]).State |= DEVICE_DST_FAILED;
                    SS_SendDaDevState((COMM_PORT *)0, (*Env).MyDirTableID,(*(*PtrDevices)[i]).id,
                        DRIVER_EXCHANGE_ERROR,(uint32_t)SS_DADEVFAILED,SS_ALARM, LogFile, Env->FullPathName, Env->UserID);
                }

                shutdown(sock,2);
                close(sock);
                sock = 0;
                (*TCPDriverParameters)[i].FailedTime = ct;

                RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "KM5TCPOpenDriverCmd: connection error");

                continue;
            }
            (*TCPDriverParameters)[i].PortState = PORT_STATE_CONNECTED;
            (*TCPDriverParameters)[i].sock = sock;
        }/* for (i=0; i<NumConnections; i++) */
    }/* if (PtrPortProp->PtrIface->IfaceType == IFACE_ETHERNET) */
   else if(PtrPortProp->PtrIface->IfaceType == IFACE_ETHERGATE ||
		   PtrPortProp->PtrIface->IfaceType == IFACE_MODEM_OVERTCP)
   {
	   /* Открываем одно соединение с устройством, адрес которого описан на порту */
	   NumConnections = 1;

	   TCPDriverParameters = (TCP_PORT_PARAMETERS (*)[])RSDURTGUtils_SMalloc(sizeof(TCP_PORT_PARAMETERS)*NumConnections);
	   if (NULL == TCPDriverParameters)
	   {
		   RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"KM5TCPOpenDriverCmd: Fatal error TCPDriverParameters1 E_MEM!");
		   ListDispose(TCPList);
		   TCPList = (ListType)0;
		   return TCPList;
	   }

	   (*TCPDriverParameters)[0].MsgID = 0;
	   (*TCPDriverParameters)[i].ProtoType = IFACE_ETHERGATE;
	   (*TCPDriverParameters)[i].BaudRate = PtrPortProp->BaudRate;
	   (*TCPDriverParameters)[0].PortNumber = PtrPortProp->PortNumber;
	   memcpy(&(*TCPDriverParameters)[0].HostAddr[0], &PtrPortProp->PortHostAddr[0], 16);

	   (*TCPDriverParameters)[0].PortState  = PORT_STATE_EMPTY;
	   (*TCPDriverParameters)[0].FailedTime = 0;
	   strcpy((*TCPDriverParameters)[0].DialNumber, PtrPortProp->DialNumber);

	   (*TCPDriverParameters)[0].device_addr.sin_family = AF_INET;
	   (*TCPDriverParameters)[0].device_addr.sin_port = htons((uint16_t)(*TCPDriverParameters)[0].PortNumber);
	   (*TCPDriverParameters)[0].device_addr.sin_addr.s_addr = inet_addr((char *)(*TCPDriverParameters)[0].HostAddr);
	   (*TCPDriverParameters)[0].sock = 0;

	   if (((PtrPortProp->State & ADCP_DST_NOWORK) != ADCP_DST_NOWORK) &&
		   ((PtrPortProp->PtrIface->State & ADCP_DST_NOWORK) != ADCP_DST_NOWORK))
	   {
		   sock = RSDURTGUtils_bound_stream_socket ((uint16_t *)&port, (uint32_t)0xffff, LogFile);
		   if(sock < 0)
		   {
			   RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"KM5TCPOpenDriverCmd: Can't create SOCKET! ");
		   }
		   else
		   {
			   RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile,"KM5TCPOpenDriverCmd: connect to %s port %d ",
				   (*TCPDriverParameters)[0].HostAddr, (*TCPDriverParameters)[0].PortNumber);

			   int32_t retConnect = connect(sock,(struct sockaddr *)&(*TCPDriverParameters)[0].device_addr, (socklen_t)sizeof(struct sockaddr));
			   if (retConnect < 0)
			   {
				   RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"KM5TCPOpenDriverCmd: EtherGate connect to %s port %d failed! errno=%d ",
					   (*TCPDriverParameters)[0].HostAddr,(*TCPDriverParameters)[0].PortNumber, errno);
				   RSDURTGUtils_ErrCheck(LogFile, ERR_TCP, __LINE__, __FILE__, errno, ERR_CONTINUE, "KM5TCPOpenDriverCmd: connect failed ");
			   }

			   if (retConnect < 0)
			   {
				   /* TODO: Выполнять проверку порта а не устройства! */
				   if (((*(*PtrDevices)[0]).State & DEVICE_DST_FAILED) != DEVICE_DST_FAILED)
				   {
					   RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile,"KM5TCPOpenDriverCmd: EtherGate device failed! id=%u addr=%s ",
						   PtrPortProp->id,PtrPortProp->PortHostAddr);
					   SS_SendDaDevState((COMM_PORT *)0, (*Env).MyDirTableID,PtrPortProp->id,
						   DRIVER_EXCHANGE_ERROR,(uint32_t)SS_DADEVFAILED,SS_ALARM, LogFile, Env->FullPathName, Env->UserID);
				   }

				   shutdown(sock,2);
				   close(sock);

				   for(i=0; i<(uint32_t)ListNumItems(PtrPortProp->PortDev);i++)
					   (*(*PtrDevices)[i]).State |= DEVICE_DST_FAILED;
			   }
			   else
			   {
				   (*TCPDriverParameters)[0].PortState = PORT_STATE_CONNECTED;
				   (*TCPDriverParameters)[0].sock = sock;
			   }
		   }
	   }
   }

   else
   {
	   ListDispose(TCPList);
	   TCPList = (ListType)0;
   }

   if(NumConnections > 0)
   {
	   ListInsertItems(TCPList, (void*)TCPDriverParameters, FRONT_OF_LIST, (int32_t)NumConnections);
	   qsort(ListGetDataPtr(TCPList), (size_t)ListNumItems(TCPList), sizeof(TCP_PORT_PARAMETERS), CompTCPParam);
	   RSDURTGUtils_SFree(TCPDriverParameters);
   }

   return(TCPList);
}

void KM5TCPCloseDriverCmd(ListType TCPDriverParameters, DCS_ENV *Env, LOG_HEADER* LogFile)
{
    TCP_PORT_PARAMETERS   (*PtrTCPParameters)[] = NULL;
    int32_t                 NumConnections = 0, i = 0;


    if (TCPDriverParameters == NULL)
    {
        RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPCloseDriverCmd: TCPDriverParameters is NULL! ");
        return;
    }
    NumConnections = ListNumItems(TCPDriverParameters);
    PtrTCPParameters = (TCP_PORT_PARAMETERS (*)[])ListGetDataPtr(TCPDriverParameters);
    if (NULL == PtrTCPParameters)
        return;

    if (NumConnections > 0)
    {
        for (i = 0; i<NumConnections; i++)
        {
            if ((*PtrTCPParameters)[i].PortState == PORT_STATE_CONNECTED)
            {
                if ((*PtrTCPParameters)[i].sock != 0)
                {
                    shutdown((*PtrTCPParameters)[i].sock, 2);
                    close((*PtrTCPParameters)[i].sock);
                    (*PtrTCPParameters)[i].sock = 0;
                }
                (*PtrTCPParameters)[i].PortState = PORT_STATE_EMPTY;
            }
        }/* for (i=0; i<NumConnections;i++) */
    }/* if (NumConnections > 0) */
    ListDispose(TCPDriverParameters);
}

void KM5ReadParameter(DCS_ENV *Env, DRIVER_EXCHANGE_STRUCT *  DriverSendExchange, uint32_t IfaceType,
                      void * DriverParameters, LOG_HEADER* LogFile)
{
    TCP_PORT_PARAMETERS     *PtrTCPParameter = NULL;
    RMXSER_PORT_PARAMETERS  *PtrSerialParameters = NULL;
    int32_t         dev_num = 0;
    KM5_msg_t       km5_msg;
    ADCP_UMBOX_LOG_ITEM LogBoxItem;
    DEVICE_PROP    *pDevProp = NULL;
    uint16_t        status = E_OK;
    int32_t         i = 0;

    switch (IfaceType)
    {
		case IFACE_ETHERNET:
			PtrTCPParameter = (TCP_PORT_PARAMETERS *)DriverParameters;
			break;
		case IFACE_COM_PORTS:
			PtrSerialParameters = (RMXSER_PORT_PARAMETERS *)DriverParameters;
			break;
		default:
			RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5ReadParameter: unknown iface type %d ", IfaceType);
			DriverSendExchange->status = DRIVER_EXCHANGE_ERROR;
			return;
    }

    memset(&km5_msg, 0x00, sizeof(km5_msg));

    dev_num = ListNumItems(Env->Device);
    
    for ( i = 0; i < dev_num; i++)
    {
    	pDevProp = (DEVICE_PROP*)ListGetPtrToItem(Env->Device, i + 1);
    	if (DriverSendExchange->device_id == pDevProp->id)
    	{
			LogBoxItem.type = DA_TYPE_DRIVER;
			LogBoxItem.id = pDevProp->id;
			LogBoxItem.counter = 0;
			LogBoxItem.priority = 120;

    		break;
    	}
    	else
    		pDevProp = NULL;
    }

//TODO  необходимо реализовать функцию
  
}

void KM5TCPReadParameterCmd(DCS_ENV *Env,
                               DRIVER_EXCHANGE_STRUCT * DriverSendExchange,
                               ListType TCPDriverParameters,
                               LOG_HEADER* LogFile)
{
    TCP_PORT_PARAMETERS *PtrTCPParameter = NULL;

    int32_t     i = 0;
    uint32_t    IfaceType;

    uint32_t        device_id = (*DriverSendExchange).data.modbus[0].DeviceNumber;


    /* Инициализируем статусы параметров */
    for (i=0; i < (int32_t)(*DriverSendExchange).NumberParam; i++)
        (*DriverSendExchange).data.irt[i].status = DRIVER_DST_PARAM_NOCORRECT;

    PtrTCPParameter = (TCP_PORT_PARAMETERS*)ListGetPtrToItem(TCPDriverParameters,1);
    if (NULL == PtrTCPParameter)
    {
        RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPReadParameterCmd: Cannot ListGetPtrToItem(TCPDriverParameters,1) for device number %u!",
            (*DriverSendExchange).data.modbus[0].DeviceNumber);
        (*DriverSendExchange).status = DRIVER_EXCHANGE_ERROR;
        return;
    }
    if (PtrTCPParameter->ProtoType == IFACE_ETHERNET)
    {
        PtrTCPParameter = (TCP_PORT_PARAMETERS *)bsearch(&device_id, ListGetDataPtr(TCPDriverParameters),(size_t)ListNumItems(TCPDriverParameters), sizeof(TCP_PORT_PARAMETERS),CompTCPParamDeviceKey);
        if (PtrTCPParameter == (TCP_PORT_PARAMETERS *)0)
        {
            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPReadParameterCmd: Not found device number %u!",
                (*DriverSendExchange).data.irt[0].DeviceNumber);
            (*DriverSendExchange).status = DRIVER_EXCHANGE_ERROR;
            return;
        }
    }
    else
    {
        PtrTCPParameter = (TCP_PORT_PARAMETERS*)ListGetPtrToItem(TCPDriverParameters,1);
        if (NULL == PtrTCPParameter)
        {
            RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPReadParameterCmd: Cannot ListGetPtrToItem(TCPDriverParameters,1) for device number %u!",
                (*DriverSendExchange).data.irt[0].DeviceNumber);
            (*DriverSendExchange).status = DRIVER_EXCHANGE_ERROR;
            return;
        }
    }

    IfaceType = IFACE_ETHERNET;

    if (PtrTCPParameter->PortState != PORT_STATE_CONNECTED)
    {
        (*DriverSendExchange).status = DRIVER_EXCHANGE_ERROR;
        return;
    }
    KM5ReadParameter(Env, DriverSendExchange, IfaceType, PtrTCPParameter, LogFile);
}

void KM5TCPReadArchiveCmd(DCS_ENV *Env, DRIVER_EXCHANGE_STRUCT  *DriverSendExchange,
                           ListType DriverParameters, LOG_HEADER* LogFile)
{
    (*DriverSendExchange).status = DRIVER_UNSUPPORT_COMMAND;
    return;
}

void KM5TCPWriteTimeCmd(DRIVER_EXCHANGE_STRUCT *DriverSendExchange,
                           ListType TCPDriverParameters, LOG_HEADER* LogFile)
{
    (*DriverSendExchange).status = DRIVER_UNSUPPORT_COMMAND;
    return;
}

void KM5TCPWriteParameterCmd(DRIVER_EXCHANGE_STRUCT * DriverSendExchange,
        ListType TCPDriverParameters ,LOG_HEADER* LogFile)
{
    (*DriverSendExchange).status = DRIVER_UNSUPPORT_COMMAND;
    return;
}

int32_t KM5CheckDeviceCmd(DRIVER_EXCHANGE_STRUCT * DriverSendExchange,
                          void *DriverParameters, LOG_HEADER* LogFile)
{
    TCP_PORT_PARAMETERS     *PtrTCPParameter = (TCP_PORT_PARAMETERS *)DriverParameters;

    KM5_msg_t       km5_msg;
    int32_t         ret = DRIVER_OK;

    memset(&km5_msg, 0x00, sizeof(km5_msg));

//TODO  необходимо реализовать функцию
    return ret;
}

int32_t KM5TCPCheckDeviceCmd(DRIVER_EXCHANGE_STRUCT *DriverSendExchange,
                             ListType TCPDriverParameters, LOG_HEADER* LogFile, DCS_ENV * Env)
{
    TCP_PORT_PARAMETERS  *PtrTCPParameter = NULL;
    TCP_PORT_PARAMETERS   TCPParameter;
    int32_t               ret = DRIVER_OK;

    RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "KM5TCPCheckDeviceCmd");


    if (NULL == DriverSendExchange ||
        NULL == TCPDriverParameters ||
        NULL == LogFile ||
        NULL == Env)
    {
        return DRIVER_SYSTEM_ERROR;
    }

    if (ListNumItems(TCPDriverParameters) <=0)
        return DRIVER_SYSTEM_ERROR;

    PtrTCPParameter = (TCP_PORT_PARAMETERS*)ListGetPtrToItem(TCPDriverParameters,1);
    if(PtrTCPParameter == (TCP_PORT_PARAMETERS *)0)
        return DRIVER_SYSTEM_ERROR;

    TCPParameter.DeviceID = (*DriverSendExchange).DevNumber;

    if(PtrTCPParameter->ProtoType == IFACE_ETHERNET)
        PtrTCPParameter = (TCP_PORT_PARAMETERS *)bsearch(&TCPParameter.DeviceID,ListGetDataPtr(TCPDriverParameters),(size_t)ListNumItems(TCPDriverParameters), sizeof(TCP_PORT_PARAMETERS),CompTCPParam);

    if(PtrTCPParameter == (TCP_PORT_PARAMETERS *)0)
    {
        RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "KM5TCPCheckDeviceCmd: Not found device! ");
        ret = DRIVER_EXCHANGE_ERROR;
    }
    else
    {
		RSDURTGUtils_UnilogMessage(LOG_ERR, LogFile, "KM5TCPCheckDeviceCmd: PORT_STATE_CONNECTED ");

		if (DRIVER_OK == ret)
		{
			ret = KM5CheckDeviceCmd(DriverSendExchange, PtrTCPParameter, LogFile);
			if (DRIVER_OK == ret)
			{
				RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "KM5TCPCheckDeviceCmd: Node come to live! id=%u addr=%u",
					(*DriverSendExchange).device_id, (*DriverSendExchange).DevNumber);
			}
		}
    }

    RSDURTGUtils_UnilogMessage(LOG_DEBUG, LogFile, "KM5TCPCheckDeviceCmd %d", ret);

    return ret;
}

void KM5TCPReadTimeCmd(DRIVER_EXCHANGE_STRUCT *DriverSendExchange,
                          ListType TCPDriverParameters, LOG_HEADER* LogFile)
{
    (*DriverSendExchange).status = DRIVER_UNSUPPORT_COMMAND;
    return;
}
