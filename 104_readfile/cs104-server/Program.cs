using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using lib60870;
using lib60870.CS101;
using lib60870.CS104;

using System.IO;
using System.Collections.Generic;

namespace cs104_server
{

  class MainClass
  {

     public static List<Adress> listOfAdress = new List<Adress>() ;

     public static double ParseToDouble(string value)
     {
         double result = Double.NaN;
         value = value.Trim();
         if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out result))
         {
             if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
             {
                 return Double.NaN;
             }
         }
         return result;
     }

    public static int rfile ( )
    {
       string path= @"out_u_g.txt";
       String[] keyPair = null;
       String value = null;
       String Key = null;
       String s1 = null;
       double vl =0;

       if (!File.Exists(path)) {
         Console.WriteLine("\nFile = " + path + " not exists\n");
         return (1);
       }

       try
       {
          string[] lines = File.ReadAllLines(path);
          foreach (string s in lines)
          {
            // что-нибудь делаем с прочитанной строкой s
            //Console.WriteLine(s);
            s1= s.Trim().ToUpper();
            if (s1== "") continue;
            keyPair = s1.Split(new char[] { '=' }, 2);
            Key = keyPair[0].Trim();
            if (keyPair.Length > 1) {
              value = keyPair[1].Trim();
              vl=ParseToDouble(value);
              //Console.WriteLine("Key=" + Key + "=" + vl.ToString());
              for(int i=0;i<listOfAdress.Count;i++)
              {
                 if(listOfAdress[i].Name.Contains(Key))
                  listOfAdress[i].Value = (float)vl;
              }
            }

          }
       }
       catch (Exception ex)
       {
          Console.WriteLine(ex.Message);
       }

       return(0);
    }


     public static int rfiletable()
     {
       string path= @"out_u_g_param.txt";
       String[] keyPair = null;
       String value = null;
       String Key = null;
       String s1 = null;
       int vl =0;

        if (!File.Exists(path)) {
          Console.WriteLine("\nFile = " + path + " not exists\n");
          return (1);
        }

        try
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string s in lines)
            {
               s1= s.Trim().ToUpper();
               if (s1== "") continue ;
               keyPair = s1.Split(new char[] { ';' }, 2);
               Key = keyPair[0].Trim();
               if (keyPair.Length > 1) {
                 value = keyPair[1].Trim();
                 //Console.WriteLine("Key1=" + Key + "=" + value);
                 if (int.TryParse(value, out vl)) {
                   //Console.WriteLine("Key1=" + Key + "=" + vl.ToString());
                   listOfAdress.Add(new Adress() { Name = Key, Cvalif = vl, Value=float.NaN });
                 }
               }
            }

        }
        catch (Exception ex)
        {
             Console.WriteLine(ex.Message);
             return (1);
        }

        return(0);
     }

     public static int ToServer(ref ASDU newAsdu)
     {
        string key;
        float vl ;
        int adr ;
        for(int i=0;i<listOfAdress.Count;i++)
        {
           //key=listOfAdress[i].Name;
           adr=listOfAdress[i].Cvalif;
           vl=listOfAdress[i].Value;
           //Console.WriteLine("ToServer=" + key + "=" + vl.ToString());
           //newAsdu.AddInformationObject(new MeasuredValueShortWithCP56Time2a(adr, (float)vl, new QualityDescriptor(), new CP56Time2a(DateTime.Now)));
		   newAsdu.AddInformationObject(new MeasuredValueShort(adr, (float)vl, new QualityDescriptor() ));
        }
        return(0);
     }


    private static bool interrogationHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qoi)
    {
      Console.WriteLine ("Interrogation for group " + qoi);

      ApplicationLayerParameters cp = connection.GetApplicationLayerParameters ();

      connection.SendACT_CON (asdu, false);

      // send sequence of information objects
      ASDU newAsdu = new ASDU (cp, CauseOfTransmission.INTERROGATED_BY_STATION, false, false, 1, 1, true);

      rfile( );
      ToServer(ref newAsdu);

      connection.SendASDU (newAsdu);

      connection.SendACT_TERM (asdu);

      return true;
    }

    private static bool asduHandler(object parameter, IMasterConnection connection, ASDU asdu)
    {

      if (asdu.TypeId == TypeID.C_SC_NA_1) {
        Console.WriteLine ("Single command");

        SingleCommand sc = (SingleCommand)asdu.GetElement (0);

        Console.WriteLine (sc.ToString ());
      }
      else if (asdu.TypeId == TypeID.C_CS_NA_1){

        ClockSynchronizationCommand qsc = (ClockSynchronizationCommand)asdu.GetElement (0);

        Console.WriteLine ("Received clock sync command with time " + qsc.NewTime.ToString());
      }

      if (asdu.TypeId == TypeID.M_ME_TF_1) {
        MeasuredValueShortWithCP56Time2a sc = (MeasuredValueShortWithCP56Time2a)asdu.GetElement (0);
        Console.WriteLine (sc.ToString ());
      }
      if (asdu.TypeId == TypeID.M_ME_NC_1) {
        MeasuredValueShort sc = (MeasuredValueShort)asdu.GetElement (0);
        Console.WriteLine (sc.ToString ());
      }
      return true;
    }

    public static void Main (string[] args)
    {

      bool running = true;

      Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e) {
        e.Cancel = true;
        running = false;
      };

      Server server = new Server ();

      server.DebugOutput = true;

      server.MaxQueueSize = 10;
	  
	  //server.SetLocalPort(2405);

      server.SetInterrogationHandler (interrogationHandler, null);

      server.SetASDUHandler (asduHandler, null);

      if (1==rfiletable()) return ;

      server.Start ();

      ASDU newAsdu = new ASDU(server.GetApplicationLayerParameters(), CauseOfTransmission.INITIALIZED, false, false, 0, 1, false);
      EndOfInitialization eoi = new EndOfInitialization(0);
      newAsdu.AddInformationObject(eoi);
      server.EnqueueASDU(newAsdu);

      int waitTime = 1000;

      while (running) {
        Thread.Sleep(1000);

        if (waitTime > 0)
          waitTime -= 100;
        else {

          newAsdu = new ASDU (server.GetApplicationLayerParameters(), CauseOfTransmission.PERIODIC, false, false, 1, 1, true);

          ToServer(ref newAsdu);
          rfile( );

          server.EnqueueASDU (newAsdu);

          waitTime = 1000;
        }
      }

      Console.WriteLine ("Stop server");
      server.Stop ();
    }

  }


   public class Adress
   {
      public string Name { get; set; }
      public int Cvalif { get; set; }
      public float Value { get; set; }
   }


}
