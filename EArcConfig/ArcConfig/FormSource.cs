/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 21.04.2021
 * Time: 18:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.Collections;
using System.ComponentModel;
using System.Data.Odbc;
using System.IO;
using System.Text;

namespace ArcConfig
{
	/// <summary>
	/// Description of FormSource.
	/// </summary>
	public partial class FormSource : Form
	{
		public FormSource()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		public FormSource(OdbcConnection conn)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			 _conn = conn ;
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
                  
    /*  if (stype=="DECIMAL") ret = reader.GetValue(i).ToString(); 
        if (stype=="NUMBER") ret = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
        if (stype=="VARCHAR2") ret = reader.GetString(i);
        if (stype=="NVARCHAR") ret = reader.GetString(i);
        if (stype=="WVARCHAR") ret = reader.GetString(i);
        if (stype=="TEXT") ret = reader.GetString(i);
        if (stype=="INTEGER") ret = reader.GetValue(i).ToString();
        if (stype=="CHAR") ret = reader.GetString(i);
        if (stype=="NCHAR") ret = reader.GetString(i);
        if (stype=="DATE") ret = reader.GetString(i);
        if (stype=="TIME") ret = reader.GetString(i); 
        if (stype=="DOUBLE PRECISION") ret = reader.GetValue(i).ToString(); 
     */
       
     }
     return(ret);
   }


    public OdbcConnection _conn;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }		

//    
// MEAS_SOURCE
// DG_SOURCE
// DA_SOURCE
// CALC_SOURCE
// EA_SOURCE
//
//  ID        NUMBER(11),
//  ALIAS     VARCHAR2(255 BYTE),
//  ID_GTOPT  NUMBER(11)
//  PORT_NUM  NUMBER(11),
//  PRIORITY  NUMBER(11)	
//

	}
}
