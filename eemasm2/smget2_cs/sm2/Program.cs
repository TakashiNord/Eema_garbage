/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 13.11.2017
 * Time: 14:19
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Net ;
using System.Net.Sockets ;


using int8_t = System.Byte;
using int16_t = System.Int16;
using int32_t = System.Int32;
using int64_t = System.Int64;
using uint8_t = System.SByte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;

//[StructLayout(LayoutKind.Sequential, Pack = 0)]

/* Права пользователя */
public struct KEY_LAWS {
    public uint32_t  laws;
    public string      key;
} ;

/* Универсальный заголовок пакетов для передачи данных в комплексе РСДУ */
public struct UNITRANS_HEADER {
    public uint32_t  command;
    public uint32_t  src_uid;
    public uint32_t  dst_uid;
    public uint32_t  param1;    /*  Как правило, количество передаваемых параметров */
    public uint32_t  param2;    /*  Как правило, глобальный тип данных */
    public uint32_t  param3;    /*  Бывает, что здесь передается какое-либо время(например последнего запроса, диспетчерской ведомости и т.д. */
    public KEY_LAWS  src_laws;
    public uint32_t  data_len;
    public int16_t   status;
    public uint32_t  time1970;
}



namespace sm2
{
  class Program
  {

    public static void Main(string[] args)
    {

      Console.WriteLine("Hello World!");

      uint16_t GCMD_GLOBAL_STATE_STOPPED       = 0xf00f ;     /* Текущее состояние - остановлен */
      uint16_t GCMD_GET_GLOBAL_STATE           = 0xf008 ;     /* Получить статус сервера (SYSTEMMASTER или SYSTEMSLAVE) */
      uint16_t GCMD_GLOBAL_STATE_MASTER        = 0xf009 ;     /* Ответ сервера SYSTEMMASTER */
      uint16_t GCMD_GLOBAL_STATE_SLAVE         = 0xf00a ;     /* Ответ сервера SYSTEMSLAVE */

      UNITRANS_HEADER hd = new UNITRANS_HEADER();

      hd.src_uid = 0xffff ; // 0x0 0xffff  env->UserID
      hd.param1  = 0xffff ; // 0xffff
      hd.param2  = 0xffff ;// 0xffff  0 ADM_GROUPID;
      hd.src_laws.laws = 0xffffffff;
      hd.dst_uid = 0xffff ;// 0xffff 0  ADM_SYSMONUSERID;
      hd.command = GCMD_GET_GLOBAL_STATE;
      hd.data_len = 0 ;
      hd.time1970 = 0; // RSDURTGUtils_Time70() static_cast<UINT>(time(NULL))
      hd.status = 0 ;  // E_OK = 0

      IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(hd));
      byte[] byteArray = new byte[Marshal.SizeOf(hd)];

      //Now copy structure to pointer
      Marshal.StructureToPtr(hd, ptr, false);
      Marshal.Copy(ptr, byteArray, 0, Marshal.SizeOf(hd));
      //Now use ByteArray ready for use

      // TODO: Implement Functionality Here
      // This constructor arbitrarily assigns the local port number.
      UdpClient udpClient = new UdpClient();
      try{
           udpClient.Connect("192.168.120.21", 2003);

           // Sends a message to the host to which you have connected.
           udpClient.Send(byteArray, byteArray.Length);

           //IPEndPoint object will allow us to read datagrams sent from any source.
           IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

           // Blocks until a message returns on this socket from a remote host.
           Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
           string returnData = Encoding.Unicode.GetString(receiveBytes);

           UNITRANS_HEADER hd2 = new UNITRANS_HEADER();
           Marshal.Copy(receiveBytes, 0, ptr, receiveBytes.Length);
           hd2=(UNITRANS_HEADER)Marshal.PtrToStructure(ptr,typeof(UNITRANS_HEADER));

           // Uses the IPEndPoint object to determine which of these two hosts responded.
           Console.WriteLine("hd.command= " +
                                      hd2.command.ToString());
           Console.WriteLine("from Ip=" + RemoteIpEndPoint.Address.ToString() +
                                       " : " + RemoteIpEndPoint.Port.ToString());

           udpClient.Close();
      }
      catch (Exception e ) {
                    Console.WriteLine(e.ToString());
      }

      //Marshal.FreeHGlobal(ptr);

      Console.Write("Press any key to continue . . . ");
      Console.ReadKey(true);
    }


  }




}