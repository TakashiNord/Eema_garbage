/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 07.06.2020
 * Time: 15:51
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.IO;
using System.Threading.Tasks;
using System.Threading;

using System.Collections.Generic;

namespace read_file1
{
  class Program
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
                   Console.WriteLine("Key1=" + Key + "=" + vl.ToString());
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

     public static int ToServer()
     {
        string key;
        float vl ;
        for(int i=0;i<listOfAdress.Count;i++)
        {
           key=listOfAdress[i].Name;
           vl=listOfAdress[i].Value;
           Console.WriteLine("ToServer=" + key + "=" + vl.ToString());
        }
        return(0);
     }


    public static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      bool running = true;

      Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e) {
        e.Cancel = true;
        running = false;
      };

      // TODO: Implement Functionality Here
      rfiletable ( );
      rfile ( );
      Console.Write(DateTime.Now);

      int waitTime = 1000;

      while (running) {
        Thread.Sleep(2000);

        if (waitTime > 0)
          waitTime -= 100;
        else {

          ToServer();
          Console.Write(DateTime.Now);
          rfile ( );

          waitTime = 1000;
        }
      }

      Console.Write("Press any key to continue . . . ");
      Console.ReadKey(true);
    }


    public static int rfile1 ( )
    {
       string path= @"out_u_g.txt";

       if (!File.Exists(path)) {
         Console.WriteLine("\nFile = " + path + " not exists\n");
         return (1);
       }

       Console.WriteLine("\n1------------\n");
       using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
       {
         string line;
         while ((line = sr.ReadLine()) != null)
         {
            Console.WriteLine(line);
          }
       }

       Console.WriteLine("\n2------------\n");
       StreamReader f = new StreamReader(path);
       while (!f.EndOfStream)
       {
         string s = f.ReadLine();
         // что-нибудь делаем с прочитанной строкой s
         Console.WriteLine(s);
       }
       f.Close();

       Console.WriteLine("\n3------------\n");
       string fileContent = string.Empty;
       try
       {
         using (TextReader tr = File.OpenText(path))
         {
           fileContent = tr.ReadToEnd();
           Console.WriteLine(fileContent);
          }
       }
       catch (Exception ex)
       {
            Console.WriteLine(ex.Message);
       }

       Console.WriteLine("\n4------------\n");
       string[] lines = File.ReadAllLines(path);
       foreach (string s in lines)
       {
         // что-нибудь делаем с прочитанной строкой s
         Console.WriteLine(s);
       }


       return(0);
    }



  }

   public class Adress
   {
      public string Name { get; set; }
      public int Cvalif { get; set; }
      public float Value { get; set; }
   }


}