#!/usr/bin/env python
# -*- coding: utf-8 -*-

import sys
import time
import socket
import struct
import os, string, binascii, datetime


SMC_CLEARL_RSDU_JOB_INFO = 118
SMC_GET_HOST_SYS_INFO = 101 # (sizeof(uint16_t)*4) + sizeof(SYS_INFO);
SMC_DEL_HOST_SYS_JOB = 103
SMC_GET_HOST_JOB_INFO = 102 # (sizeof(uint16_t)*4) + sizeof(SYS_INFO);
GCMD_DATASERVICEINIT = 0xf006 # Этой командой отвечает dbcpd
GCMD_DATASERVICEEXIT = 0xf007 # Эта команда используется для завершения сеанса с dbcpd
GCMD_ECHO = 0xfffe   # Эхо - запрос
GCMD_GET_GLOBAL_STATE = 0xf008 # Получить статус сервера (SYSTEMMASTER или SYSTEMSLAVE)
SMC_REINIT_DB = 124
SMC_REINIT_RSDU_JOB = 108
SMC_GET_MBII_HOST_INFO = 100
SMC_RESET_MBII_SYSTEM = 119
SMC_SHUTDOWN_RSDU = 120
SMC_SHUTDOWN_ALL_HOSTS = 107
SMC_MAKE_RESERVE_RSDU_JOB = 112
SMC_RESTART_MODULE = 128
SMC_RESTART_ALL_MODULES_ON_HOST = 129
SMC_RESTART_ALL_MODULES_ON_CLASTER = 130
GCMD_SET_GLOBAL_STATE_SLAVE = 0xf00c
GCMD_SET_GLOBAL_STATE_MASTER = 0xf00b

# Команды состояния
GCMD_GLOBAL_STATE_STOPPED = 0xf00f   # Текущее состояние - остановлен
GCMD_GLOBAL_STATE_MASTER = 0xf009   # Ответ сервера SYSTEMMASTER
GCMD_GLOBAL_STATE_SLAVE = 0xf00a    # Ответ сервера SYSTEMSLAVE


def smconnu(addr0):
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    try:
        s.setsockopt(socket.SOL_SOCKET, socket.SO_RCVTIMEO, 1000)
        s.setsockopt(socket.SOL_SOCKET, socket.SO_SNDTIMEO, 1000)
    except Exception as e:
        s.settimeout(1)
        pass

    s.connect(addr0)

    #This is needed to join a multicast group
    ip = addr0[0]
    mreq = struct.pack("4sl", socket.inet_aton(ip), socket.INADDR_ANY)

    return s


def smsendu(s, d):
    #print ('sending "%s" ' % d) # binascii.hexlify(d)
    s.sendall(d)
    return 0


def smreceiveu(s, ln):
    data = ("", "")
    data = s.recvfrom(ln)
    return data


def uh(fm):
    fm1 = fm # 'IIIIIII16sIhI'
    l1 = struct.calcsize(fm1)
    bo = sys.byteorder
    #print(bo)
    if bo == 'little': fm1 = '<' + fm
    if bo == 'big': fm1 = '!' + fm
    l2 = struct.calcsize(fm1)
    pass
    return fm1


def unpackVal(tr, frm):
    spack = struct.pack('>' + frm, tr)
    sunpack = struct.unpack('<' + frm, spack)
    ret = ''
    if len(sunpack) > 0 : ret = sunpack[0]
    return ret


"""
input
  command = 
SMC_CLEARL_RSDU_JOB_INFO
GCMD_ECHO
GCMD_GET_GLOBAL_STATE
SMC_REINIT_DB
SMC_REINIT_RSDU_JOB
SMC_RESET_MBII_SYSTEM
SMC_SHUTDOWN_RSDU
SMC_SHUTDOWN_ALL_HOSTS
SMC_RESTART_ALL_MODULES_ON_HOST
SMC_RESTART_ALL_MODULES_ON_CLASTER
  UNITRANS_HEADER { command, src_uid, dst_uid, param1, param2, param3, laws, key, data_len, status, time1970 }
return
  UNITRANS_HEADER
"""
def SendCommand(snd_addr, UNITRANS_HEADER):
    reply = ''
    ret = (0, 0, 0, 0, 0, 0, 0, b'\0', 0, 0, 0)
    fm = 'IIIIIII16sIhI'
    fm1 = uh(fm)
    #
    packer = struct.Struct(fm1)
    packed_data = packer.pack(*UNITRANS_HEADER)
    #
    adr , port = snd_addr
    s = smconnu(snd_addr)
    try:
        smsendu(s, packed_data)
        (reply, addr0) = smreceiveu(s, 56)
    except Exception as e:
        pass
    finally:
        pass
    s.close()

    if len(reply) <= 0 : return ret

    if len(reply) == struct.calcsize('!' + fm):
        fm1 = '!' + fm

    try:
        fd = struct.unpack(fm1, reply)
    except Exception as e:
        fd = ret
    finally:
        pass

    pass
    return fd


"""
input 
  snd_addr = ('10.100.1.20', 2003) # sysmon
cmd =
  SMC_GET_MBII_HOST_INFO
  (sizeof(uint16_t)*4) + sizeof(MB_HOST_INFO);
  UNITRANS_HEADER { command, src_uid, dst_uid, param1, param2, param3, laws, key, data_len, status, time1970 }
return
  <tuple SYSMON_TCP> or empty
"""
def GetJobList(addr_send):
    """
    --
    """
    ret = ()
    tm = time.time()
    snd_addr = addr_send #('10.100.1.20', 2003)
    cm = SMC_GET_MBII_HOST_INFO
    theader = (cm, 0xffff, 0xffff, 0xffff, 0xffff, 0, 0xfffffff, b'\0', 2*4, 0, int(time.time()), 0, 0, 0, 0)
    fm = 'IIIIIII16sIhI'
    fm1 = uh(fm+'HHHH')

    packer = struct.Struct(fm1)
    packed_data = packer.pack(*theader)

    reply = ''
    bo = 'little'

    s = smconnu(snd_addr)
    try:
        # Send data
        smsendu(s, packed_data)
        time.sleep(.01)
        (reply, addr0) = smreceiveu(s,12000)
    except Exception as e:
        pass
    finally:
        pass
    #print(reply, addr0)
    if len(reply) <= 0 : return ret

    # get SYSMON_TCP= ( MB_HOST_INFO (addr_eth) ) = 5810 chars = 8+5802 = if host=1 because len=12000
    # UNITRANS_HEADER[0..10]+SYSMON_TCP[0..3+MB_HOST_INFO[0..3+4..6+7..11+12.13+array(14..23)+]]
    # REM_ADDR=34  JOB_INFO=178   178 * 32 = 5696  + 34 = 5730

    # size answer c+SYSMON_TCP
    lans = len(reply)
    # from UNITRANS_HEADER+SYSMON_TCP get SYSMON_TCP
    luni = struct.calcsize('!' + fm )
    lsysmon = lans-luni
    # if size <> SYSMON_TCP= ( MB_HOST_INFO (addr_eth) )
    if lsysmon != 5810 : return ret
    #print('lsysmon=',lsysmon)
    slsysmon = str(lsysmon)
    fm1 = fm + slsysmon + 's'
    if lans == struct.calcsize('!' + fm1):
        fm1 = '!' + fm1
        bo = 'big'
    fd1 = struct.unpack(fm1, reply)

    strSYS = fd1[11]
    if bo == 'big':
        strSYS = unpackVal(fd1[11], slsysmon+'s')
    # can get size from UNITRANS_HEADER[0..10].data_len <tuple[8]> = 5810
    sizeSYS = fd1[8]
    if bo == 'big':
        sizeSYS = unpackVal(fd1[8], 'I')
    # MB_HOST_INFO (addr_eth), fm12= struct JOB_INFO
    fm12 = 'HHh80s80sHBBII'*32
    fm22 = 'hHH' + 'HBBH28s' + '64sH' + fm12
    # 5810s chars unpack to HHHH+hHH+HBBH28s+64sH+HHh80s80sHBBII*32
    retSYS = struct.unpack('!'+'HHHH'+fm22, strSYS)

    ## close connection
    #cm = GCMD_DATASERVICEEXIT
    #theader = (cm, 0xffff, 0xffff, 0xffff, 0xffff,0,0xfffffff,b'\0',0,0,int(time.time()))
    #CmUNITRANSHEADER(s,theader)
    #

    s.close()
    return retSYS


def JobListPrint(data):
    #
    ll_b = len(data)
    if ll_b <= 0:
        print("not data")
        return 0
    #
    host = unpackVal(data[3], 'H')
    print('host=', host)
    #
    HostName = unpackVal(data[12], '64s')
    HostName = HostName.decode('utf-8')
    H1 = HostName.strip('\x00')
    H2 = H1.split('\x00')[0]
    print('HostName=', H2)
    #
    JobNumber = unpackVal(data[13], 'H')
    print('JobNumber=', JobNumber)
    #
    i = 14
    num_jobs = 0
    while i < ll_b:
        user_id = unpackVal(data[i], 'H')
        print('user_id=', user_id)
        JobID=unpackVal(data[i+1], 'H')
        print('JobID=', JobID)
        JobStatus = unpackVal(data[i+2], 'h')
        print('JobStatus=', JobStatus)
        JobName = unpackVal(data[i+3], '80s')
        JobName = JobName.decode('utf-8')
        print('JobName=', JobName)
        LoadName = unpackVal(data[i+4], '80s')
        LoadName = LoadName.decode('utf-8')
        print('LoadName=', LoadName)
        pass
        i+= 10
        num_jobs+= 1
        if num_jobs >= JobNumber : break
        pass
    pass
    return 0


"""
 get Bridged
 return =
    MasterBridged = ( host,JobID,user_id,JobStatus )
"""
def JobListBridge(data):
    #
    MasterBridged = ()
    ll_b = len(data)
    if ll_b <= 0:
        print("not data")
        return MasterBridged
    #
    host = unpackVal(data[3], 'H')
    #print('host=', host)
    JobNumber = unpackVal(data[13], 'H')
    #print('JobNumber=', JobNumber)
    #
    ll_b = len(data)
    i = 14
    num_jobs = 0
    while i < ll_b:
        JobName = unpackVal(data[i+3], '80s')
        JobName = JobName.decode('utf-8')
        #print('JobName=', JobName)
        LoadName = unpackVal(data[i+4], '80s')
        LoadName = LoadName.decode('utf-8')
        #print('LoadName=', LoadName)
        f1 = LoadName.find('/bridged')
        f2 = JobName.find('TCP Bridge')
        if (f1 >= 0 and f2 >= 0) :
            user_id = unpackVal(data[i], 'H')
            # print('user_id=', user_id)
            JobID = unpackVal(data[i + 1], 'H')
            # print('JobID=', JobID)
            JobStatus = unpackVal(data[i + 2], 'h')
            # print('JobStatus=', JobStatus)
            pass  # MasterBridged = hostid jobid id JobStatus
            MasterBridged = (host, JobID, user_id, JobStatus)
            break
        pass
        i+= 10
        num_jobs+= 1
        if num_jobs >= JobNumber : break
        pass
    pass
    print(MasterBridged)
    return MasterBridged


"""
 Send Change Status
 in =
   cm = SMC_MAKE_RESERVE_RSDU_JOB
   serv = MasterBridged = ( host,JobID,user_id,JobStatus )
 return =
    0
    
 Send Restart Module
 in =
   cm = SMC_RESTART_MODULE
   serv = ( host,JobID,user_id,JobStatus )
 return =
    0    

 Send Del Module
 in =
   cm = SMC_DEL_HOST_SYS_JOB
   serv = ( host,JobID,user_id,JobStatus )
 return =
    0
    
"""
def SendCommandA(addr_send, cm, serv):
    #
    tm = time.time()
    snd_addr = addr_send # ('10.100.1.20', 2003)

    if len(serv) <= 0 : return -1

    JobID = serv[1]
    JobStatus = serv[3]

    if cm == SMC_MAKE_RESERVE_RSDU_JOB:
        if JobStatus != 1:
            print("bridge - no running")
            return -2

    if JobID != 0 :
        theader = (cm, 0xffff, 0xffff, 0xffff, 0xffff, 0, 0xfffffff, b'\0', 2*4, 0, int(time.time()), serv[2], serv[1], 0, serv[0])
        fm = 'IIIIIII16sIhI'
        fm1 = uh(fm+'HHHH')

        packer = struct.Struct(fm1)
        packed_data = packer.pack(*theader)

        s = smconnu(snd_addr)
        time.sleep(.01)
        try:
            # Send data
            smsendu(s, packed_data)
            smreceiveu(s, 56)
        except Exception as e:
            pass
        finally:
            pass

    ## close connection
    #cm = GCMD_DATASERVICEEXIT
    #theader = (cm , 0xffff, 0xffff, 0xffff, 0xffff,0,0xfffffff,b'\0',0,0,int(time.time()))
    #CmUNITRANSHEADER(s, theader)

    s.close()
    return 0


#-----------------------------------------------------
if __name__ == '__main__':
    str1 = ""
    pass
    print("time=" + time.strftime("%Y-%m-%d %H:%M:%S"))
    print("time utc=" + time.strftime("%Y-%m-%d %H:%M:%S", time.gmtime()))
    #print("datetime=", unicode(datetime.datetime.now()) )
    print("-----------------------")

    #addr1 = ('10.100.1.20', 2003) # sysmon
    #addr2 = ('10.100.1.20', 2013)
    #addr3 = ('10.100.1.20', 2005) # acsrvd

    snd_addr = ('10.100.1.20', 2003) # sysmon
    cm = GCMD_GET_GLOBAL_STATE
    # SMC_CLEARL_RSDU_JOB_INFO  GCMD_ECHO  GCMD_GET_GLOBAL_STATE
    # SMC_REINIT_DB  SMC_REINIT_RSDU_JOB
    # SMC_RESET_MBII_SYSTEM
    # SMC_SHUTDOWN_RSDU  SMC_SHUTDOWN_ALL_HOSTS
    # SMC_RESTART_ALL_MODULES_ON_HOST  SMC_RESTART_ALL_MODULES_ON_CLASTER
    theader = (cm, 0xffff, 0xffff, 0xffff, 0xffff, 0, 0xfffffff, b'\0', 0, 0, int(time.time()))
    retcom = SendCommand(snd_addr, theader)
    cm1 = unpackVal(retcom[0], 'I')
    print(f'Command = {cm1}')
    pass
    data1 = GetJobList(('10.100.1.20', 2003))
    JobListPrint(data1)
    MasterBridged = JobListBridge(data1)
    #Send Change Status
    #SendCommandA( ('10.100.1.20', 2003) , SMC_MAKE_RESERVE_RSDU_JOB, MasterBridged )
    #Send Restart Module
    #SendCommandA( ('10.100.1.20', 2003) , SMC_RESTART_MODULE, MasterBridged )
    #Send Del Module
    #SendCommandA( ('10.100.1.20', 2003) , SMC_DEL_HOST_SYS_JOB, MasterBridged )
    pass
    pass

