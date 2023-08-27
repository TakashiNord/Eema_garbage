/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 07.06.2021
 * Time: 8:45
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using System.ComponentModel ;
using System.Net.NetworkInformation;

using System.Runtime.InteropServices;
using System.Net.Sockets ;


using int8_t = System.Byte;
using int16_t = System.Int16;
using int32_t = System.Int32;
using int64_t = System.Int64;
using uint8_t = System.SByte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;

namespace ScanIP
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MainForm : Form
  {

  	
// ***************************************************

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
};

public uint16_t GCMD_GLOBAL_STATE_STOPPED       = 0xf00f ;     /* Текущее состояние - остановлен */
public uint16_t GCMD_GET_GLOBAL_STATE           = 0xf008 ;     /* Получить статус сервера (SYSTEMMASTER или SYSTEMSLAVE) */
public uint16_t GCMD_GLOBAL_STATE_MASTER        = 0xf009 ;     /* Ответ сервера SYSTEMMASTER */
public uint16_t GCMD_GLOBAL_STATE_SLAVE         = 0xf00a ;     /* Ответ сервера SYSTEMSLAVE */
// ***************************************************  

  	public MainForm()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      backgroundWorker1.WorkerReportsProgress = true;
      backgroundWorker1.WorkerSupportsCancellation = true;

    }


    public bool IsValidIP (string address)
    {
        if (!Regex.IsMatch (address, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
            return false;

        IPAddress dummy;
        return IPAddress.TryParse (address, out dummy);
    }


public uint32_t udpPing(string arg, int port)
{
  uint32_t cm = 0x0 ;


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
       udpClient.Connect(arg, port); // ip 2003

       // Sends a message to the host to which you have connected.
       udpClient.Send(byteArray, byteArray.Length);

       //IPEndPoint object will allow us to read datagrams sent from any source.
       IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

       // Blocks until a message returns on this socket from a remote host.
       Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
       string returnData = Encoding.Unicode.GetString(receiveBytes);

       udpClient.Close();

       UNITRANS_HEADER hd2 = new UNITRANS_HEADER();
       Marshal.Copy(receiveBytes, 0, ptr, receiveBytes.Length);
       hd2=(UNITRANS_HEADER)Marshal.PtrToStructure(ptr,typeof(UNITRANS_HEADER));

       // Uses the IPEndPoint object to determine which of these two hosts responded.
       cm = hd2.command ;

       //Console.WriteLine("hd.command= " +
       //                           hd2.command.ToString());
       //Console.WriteLine("from Ip=" + RemoteIpEndPoint.Address.ToString() +
       //                            " : " + RemoteIpEndPoint.Port.ToString());

  }
  catch (Exception e ) {
       //Console.WriteLine(e.ToString());
       cm = 0xffff ;
  }

  Marshal.FreeHGlobal(ptr);

  return(cm);
}



public static bool HostOnline(string arg)
{
    Ping pingSender = new Ping();
    PingOptions options = new PingOptions();

    options.DontFragment = true;

    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    byte[] buffer = Encoding.ASCII.GetBytes(data);
    int timeout = 2;
    try
    {
        PingReply reply = pingSender.Send(arg, timeout, buffer, options);
        if (reply.Status == IPStatus.Success) { return true; }
        else { return false; }
    }
    catch
    {
        return false;
    }
}


    void MainFormLoad(object sender, EventArgs e)
    {
      checkBoxSysmon.Checked = true ;
      checkBoxAcsrvd.Checked = true ;

      String strHostName = "" ;

      // Getting Ip address of local machine...
      // First get the host name of local machine.
      strHostName = System.Net.Dns.GetHostName ();
      toolStripStatusLabel1.Text = "Local Machine's Host Name: " +  strHostName;
      richTextOut.AppendText(toolStripStatusLabel1.Text + "\n\n");

      // Then using host name, get the IP address list..

      IPHostEntry ipEntry = System.Net.Dns.GetHostEntry (strHostName);
      IPAddress [] addr = ipEntry.AddressList;

      for (int i = 0; i < addr.Length; i++)
      {
         richTextOut.AppendText("IP Address " + i + ") = " + addr[i].ToString ());
         richTextOut.AppendText("\n");
      }

      maskedTextBoxIP.Text = "192.168.1.1" ;
      maskedTextBoxMask.Text = "255.255.255.000" ;
      maskedTextBoxEndIP.Text = maskedTextBoxIP.Text ;

      button5.Enabled =false ;// run

    }
    void Button1Click(object sender, EventArgs e)
    {
         // кнопка a
         maskedTextBoxMask.Text = "255.0.0.0" ;
    }
    void Button2Click(object sender, EventArgs e)
    {
          // кнопка b
          maskedTextBoxMask.Text = "255.255.0.0" ;
    }
    void Button3Click(object sender, EventArgs e)
    {
          // кнопка c
          maskedTextBoxMask.Text = "255.255.255.0" ;
    }
    void Button4Click(object sender, EventArgs e)
    {
          // кнопка i
          FormI fi = new FormI();
          var result = fi.ShowDialog();

          if (result == DialogResult.OK)
          {
               maskedTextBoxMask.Text = fi._RetMask ;
          }
          fi.Dispose();
    }
    void ButtonCalcClick(object sender, EventArgs e)
    {
        // calc

        if (IsValidIP (maskedTextBoxIP.Text)==false) {
          MessageBox.Show("Неправильный IP");
          return ;
        }
        if (IsValidIP (maskedTextBoxMask.Text)==false) {
          MessageBox.Show("Неправильная MASK-а");
          return ;
        }

        IPSegment ip = new IPSegment(maskedTextBoxIP.Text,maskedTextBoxMask.Text);

        textBox5.Text = ip.NumberOfHosts.ToString() ;
        textBox1.Text = ip.NetworkAddress.ToIpString() ;
        textBox4.Text = ip.BroadcastAddress.ToIpString() ;
        textBox2.Text = ip.startIP.ToIpString() ;
        textBox3.Text = ip.endIP.ToIpString() ;

        maskedTextBoxEndIP.Text =  textBox3.Text ;

        richTextOut.AppendText("\n");

        if (ip.NumberOfHosts<=10) {
          richTextOut.AppendText("=================\n");
          foreach (var host in ip.Hosts()) {
              richTextOut.AppendText(host.ToIpString()+ "\n");
          }
          richTextOut.AppendText("=================\n");
        }

        button5.Enabled = true ;

    }
    void Button5Click(object sender, EventArgs e)
    {
        // run ping
        
        if (button5.Text == "Stop") return ;

        IPSegment ip1 = new IPSegment(maskedTextBoxIP.Text,maskedTextBoxMask.Text);
        String EndIP = maskedTextBoxEndIP.Text.Trim() ;

        if (IsValidIP (EndIP)==false) {
          MessageBox.Show("Неправильный конечный IP");
          return ;
        }

        // Конечный IP должен быть в диапазоне
        int find_ip = 0 ;
        foreach (var host in ip1.Hosts()) {
          String arg1 = host.ToIpString();
          if (arg1==EndIP) { find_ip = 1 ;  break ; }
        }
        if (find_ip == 0) {
        	 MessageBox.Show("Конечный IP не в диапазоне." + Environment.NewLine + " Ping прекращен.");
        	 return ;
        }
        
        button5.Text = "Stop";

        string sq1 = String.Format("{0,-20} | {1,4} | {2,4} | {3,4}\n\n","IP", "Ping","2003", "2005");
        richTextOut.AppendText(sq1);
        // find ping and port
        foreach (var host in ip1.Hosts()) {
           String arg1 = host.ToIpString();
           //bool st = HostOnline(arg1) ;
           string sq2 = String.Format("{0,-20} | {1,4} | {2,4} | {3,4}"+ Environment.NewLine ,arg1,"+","+","+");

           richTextOut.AppendText(sq2);

           //if (backgroundWorker1.IsBusy != true)
           //{
              // Start the asynchronous operation.
           //    backgroundWorker1.RunWorkerAsync(arg1);
           // }

           if (arg1==EndIP) break ;
        }
        
        button5.Text = "Start";
    }
    void BackgroundWorker1DoWork(object sender, DoWorkEventArgs e)
    {
        BackgroundWorker worker = sender as BackgroundWorker;

        if ((worker.CancellationPending == true))
        {
            e.Cancel = true;
            return;
        }
        else
        {
            worker.ReportProgress(1);
            e.Result = HostOnline((string)e.Argument);
        }

        // Perform a time consuming operation and report progress.
        //System.Threading.Thread.Sleep(500);
        //worker.ReportProgress(i * 10);
    }
    void BackgroundWorker1ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
    }
    void BackgroundWorker1RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled == true)
        {
            //resultLabel.Text = "Canceled!";
        }
        else if (e.Error != null)
        {
            //resultLabel.Text = "Error: " + e.Error.Message;
        }
        else
        {
            //resultLabel.Text = "Done!";
        }
    }

    

    
    
  }
}
