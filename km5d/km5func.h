#ifndef __KM5FUNC_H

ListType KM5TCPOpenDriverCmd(DCS_ENV *Env, PORT_PROP  *PtrPortProp, LOG_HEADER* LogFile);

void KM5TCPCloseDriverCmd(ListType TCPDriverParameters, DCS_ENV *Env, LOG_HEADER* LogFile);

void KM5TCPReadParameterCmd(DCS_ENV *Env,
                               DRIVER_EXCHANGE_STRUCT * DriverSendExchange,
                               ListType TCPDriverParameters,
                               LOG_HEADER* LogFile);

void KM5ReadParameter(DRIVER_EXCHANGE_STRUCT *  DriverSendExchange, uint32_t IfaceType,
                      void * DriverParameters, LOG_HEADER* LogFile);

void KM5TCPReadArchiveCmd(DCS_ENV *Env, DRIVER_EXCHANGE_STRUCT  *DriverSendExchange,
                           ListType DriverParameters, LOG_HEADER* LogFile);

void KM5TCPWriteTimeCmd(DRIVER_EXCHANGE_STRUCT *DriverSendExchange,
                           ListType TCPDriverParameters, LOG_HEADER* LogFile);

void KM5TCPWriteParameterCmd(DRIVER_EXCHANGE_STRUCT * DriverSendExchange,
        ListType TCPDriverParameters ,LOG_HEADER* LogFile);

int32_t KM5TCPCheckDeviceCmd(DRIVER_EXCHANGE_STRUCT  * DriverSendExchange,
                             ListType DriverParameters,LOG_HEADER* LogFile, DCS_ENV *Env);

void KM5TCPReadTimeCmd(DRIVER_EXCHANGE_STRUCT *DriverSendExchange,
                          ListType TCPDriverParameters, LOG_HEADER* LogFile);

void *km5_cyclic_task(void *pStartParams);

#endif /* __KM5FUNC_H */
