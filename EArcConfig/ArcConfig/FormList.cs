/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 02.02.2024
 * Time: 20:04
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
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Threading.Tasks ;

namespace ArcConfig
{
	/// <summary>
	/// Description of FormList.
	/// </summary>
	public partial class FormList : Form
	{
		public FormList()
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
    public String _id;
    public String _name;
	public String _tmCol;

    public int _OptionSchemaName = 0;
    public string OptionSchemaMain = "RSDUADMIN";


    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormList(OdbcConnection conn, String name , String tmCol  )
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      _conn = conn ;
      _name = name ;
	  _tmCol = tmCol ;

    }
		
		
    //Unix -> DateTime
    public  DateTime UnixTimestampToDateTime(double unixTime)
    {
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long unixTimeStampInTicks = (long) (unixTime * TimeSpan.TicksPerSecond);
        return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
    }

    //DateTime -> Unix
    public  double DateTimeToUnixTimestamp(DateTime dateTime)
    {
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
        return (double) unixTimeStampInTicks / TimeSpan.TicksPerSecond;
    }		

    public void ARC_TABLE(object sender)
    {
      //
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      cmd0.Connection=this._conn;
 
      try {
        dataGridViewList.DataSource = null ;
        dataGridViewList.Rows.Clear();
        Application.DoEvents();
        dataGridViewList.Columns.Clear();
      }
      catch (Exception ex1)
      {
        ; //MessageBox.Show("Error ="+ex1.Message);
      }

      
      string sl1 = "SELECT * FROM " + _name ;
	  if (_tmCol!="") sl1 = "SELECT * , '' AS FROMDT1970 FROM " + _name ;
 
      cmd0.CommandText=sl1;

      dataSet1.Clear();

      // Указываем запрос для выполнения
      adapter.SelectCommand = cmd0;

      int a = 0;
      // Заполняем объект источника данных
      try {
        a = adapter.Fill(dataSet1);
      }
      catch (Exception ex1)
      {
        ;
      }

      // Запрет удаления данных
      dataSet1.Tables[0].DefaultView.AllowDelete = false;
      // Запрет модификации данных
      dataSet1.Tables[0].DefaultView.AllowEdit = false;
      // Запрет добавления данных
      dataSet1.Tables[0].DefaultView.AllowNew = false;

      // (с этого момента она будет отображать его содержимое)
      dataGridViewList.DataSource = dataSet1.Tables[0].DefaultView;;

      // Set up the data source.
      dataGridViewList.Update();

      DateTime t0 ;
      double vl1 = 0 ;
      for (int ii = 0; ii < dataGridViewList.RowCount ; ii++) {

        // нумерация
        dataGridViewList.Rows[ii].HeaderCell.Value = (ii + 1).ToString();

        if (_tmCol!="") {
          //Unix -> DateTime DT_START = 6 - > 9
		    try {
              vl1 = Convert.ToDouble(dataGridViewList.Rows[ii].Cells[_tmCol].Value);
            }
            catch (Exception ex1)
            {
              vl1 = 0;
			  continue ;
            }
          t0 = UnixTimestampToDateTime(vl1) ;
          //t0=t0.ToUniversalTime() ;
          t0=t0.ToLocalTime();
          dataGridViewList.Rows[ii].Cells["FROMDT1970"].Value=t0.ToString("u"); // u s o
        }


      } //for


      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridViewList.AutoResizeColumns();

      cmd0.Dispose();
      Application.DoEvents();

    }
		void FormListLoad(object sender, EventArgs e)
		{
			ARC_TABLE(sender) ;
		}    
		
	}
}
