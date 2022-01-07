/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 25.10.2021
 * Time: 7:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Data.Odbc;

using System.Collections.Generic;

namespace checkrowtable
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!\n");
			
			// TODO: Implement Functionality Here

			string txDsn = "RSDU2";
			string txLogin = "rsduadmin";
			string txPassword= "passme";

			
			
   	OdbcConnection odbcConnection = new OdbcConnection("DSN=" + txDsn + ";UID=" + txLogin + ";PWD=" + txPassword+ "; Pooling=False;");
      if (odbcConnection != null)
      {
        try
        {
          odbcConnection.Open();
          
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=odbcConnection;

      // "select * from ARC_FTR";
      cmd0.CommandText="SELECT * FROM meas_list "  ;

      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        Console.Write(ex1.ToString() );
        return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
                       for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string field = reader.GetName(i).ToUpper();
                            var specificType = reader.GetProviderSpecificFieldType(i);
                      
                            Console.Write("типа данных:{0} = {1} = {2} \n" ,i, reader.GetFieldType(i).ToString(),  specificType );

                        }
                       break ;
        } // while
      }
      reader.Close();         
          
          
          
          
        }
        catch (Exception ex)
        {
          Console.Write("Ошибка подключения к БД\n" + ex.Message);
          odbcConnection.Dispose();
          odbcConnection = (OdbcConnection) null;
        }
      }			
			
			
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}