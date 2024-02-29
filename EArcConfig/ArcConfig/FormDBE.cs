/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 28.02.2024
 * Time: 16:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Data.Odbc;
using System.Reflection;
using System.Resources;

namespace ArcConfig
{
	/// <summary>
	/// Description of FormDBE.
	/// </summary>
	public partial class FormDBE : Form
	{
		public FormDBE()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
   public string GetTypeValue(ref OdbcDataReader reader, int i)
   {
     object obj ;
     string ret="";
     if (reader.IsDBNull(i)) {
          ;
     } else {
       obj = reader.GetValue(i) ;
       string stype= reader.GetDataTypeName(i).ToUpper();
       //AddLogString("reader.GetDataTypeName = " + stype);
       ret = obj.ToString();
     }
     return(ret);
   }

    public OdbcConnection _conn;
    public int _OptionSchemaName = 0;
    public string OptionSchemaMain = "RSDUADMIN";

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormDBE(OdbcConnection conn, int OptionSchemaName )
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      _conn = conn ;
      _OptionSchemaName = OptionSchemaName ;
    }
		
    public void DBE_TABLE(object sender)
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }      
      


      cmd0.Dispose();
      Application.DoEvents();

    }
		void FormDBELoad(object sender, EventArgs e)
		{
			//
			this.Text = " DBE :  " ;
			DBE_TABLE(sender) ;
		}		
		
		
		
		
	}
}
