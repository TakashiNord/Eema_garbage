/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 14.03.2024
 * Time: 19:07
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
	/// Description of FormMeas30.
	/// </summary>
	public partial class FormMeas30 : Form
	{
		public FormMeas30()
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

    public FormMeas30(OdbcConnection conn, int OptionSchemaName )
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
		
		
		
		
		
		
	}
}
