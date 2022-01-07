/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 21.04.2021
 * Time: 18:23
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
  /// Description of FormStatus.
  /// </summary>
  public partial class FormStatus : Form
  {
    public FormStatus()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
    }
    public FormStatus(OdbcConnection conn)
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

    public OdbcConnection _conn;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
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

    //Unix -> DateTime
    public  DateTime UnixTimestampToDateTime(double unixTime)
    {
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long unixTimeStampInTicks = (long) (unixTime * TimeSpan.TicksPerSecond);
        return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
    }

    void StatusLoad( )
    {

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcCommand cmd1 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;
      cmd1.Connection=this._conn;

      // re-read
      //dataGridViewA.Rows.Clear();
      Application.DoEvents();

      dataSet1.Clear();


      cmd0.CommandText= "SELECT id, name, DEFINE_ALIAS,  CAST(LAST_UPDATE as CHAR(40)) as LAST_UPDATE, CAST(LAST_RELINK as CHAR(40)) as LAST_RELINK,REINIT_TYPE FROM SYS_DB_PART";

      // Указываем запрос для выполнения
      adapter.SelectCommand = cmd0;
      // Заполняем объект источника данных
      //adapter.Fill(dataSet1,"SYS_DB_PART");
      adapter.Fill(dataSet1);

      // Запрет удаления данных
      dataSet1.Tables[0].DefaultView.AllowDelete = false;
      // Запрет модификации данных
      dataSet1.Tables[0].DefaultView.AllowEdit = false;
      // Запрет добавления данных
      dataSet1.Tables[0].DefaultView.AllowNew = false;

      dataSet1.Tables[0].DefaultView.RowFilter= "DEFINE_ALIAS Like '%EL_REG_DBPART%' or DEFINE_ALIAS Like '%PH_REG_DBPART%'  or DEFINE_ALIAS Like '%AUTOMAT_DBPART%'  or DEFINE_ALIAS Like '%PWSWITCH_DBPART%' or DEFINE_ALIAS Like 'OBJECT' " ;

      // (с этого момента она будет отображать его содержимое)
      dataGridView1.DataSource = dataSet1.Tables[0].DefaultView;;
      //dataGridViewA.Columns["ID"].ReadOnly = true;
      //dataGridViewA.Columns["ID_NODE"].ReadOnly = true;

      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridView1.AutoResizeColumns();

      // Configure the details DataGridView so that its columns automatically
      // adjust their widths when the data changes.
      dataGridView1.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.AllCells;

      DateTime t0 ;
      double vl1 = 0 , vl2 = 0;
      for (int ii = 0; ii < dataGridView1.RowCount ; ii++) {
        //Unix -> DateTime
        vl1 = Convert.ToDouble(dataGridView1.Rows[ii].Cells["LAST_UPDATE"].Value);
        if (vl1>0) {
          t0 = UnixTimestampToDateTime(vl1) ;
          //t0=t0.ToUniversalTime() ;
          t0=t0.ToLocalTime();
          dataGridView1.Rows[ii].Cells["LAST_UPDATE"].Value=t0.ToString("u"); // u s o
        }


        vl1 = Convert.ToDouble(dataGridView1.Rows[ii].Cells["LAST_RELINK"].Value);
        if (vl1>0) {
          t0 = UnixTimestampToDateTime(vl1) ;
          //t0=t0.ToUniversalTime() ;
          t0=t0.ToLocalTime();
          dataGridView1.Rows[ii].Cells["LAST_RELINK"].Value=t0.ToString("u"); // u s o
        }

      }

    }


    void FormStatusLoad(object sender, EventArgs e)
    {
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
        checkBoxColumn.HeaderText = "";
        checkBoxColumn.Width = 30;
        checkBoxColumn.TrueValue = true;
        checkBoxColumn.FalseValue = false;
        checkBoxColumn.Name = "checkonoff";
        dataGridView1.Columns.Insert(0, checkBoxColumn);

        StatusLoad( ) ;
    }
    void Button1Click(object sender, EventArgs e)
    {
        //
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;
        
        cmd0.Connection=this._conn;
        
        for (int ii = 0; ii < dataGridView1.RowCount ; ii++) {
           //
           var isChecked = Convert.ToBoolean(dataGridView1.Rows[ii].Cells[0].Value) ;
           if (isChecked==false) continue ;
        
           string id = Convert.ToString (dataGridView1.Rows[ii].Cells["ID"].Value);
        
           string vl1 = Convert.ToString (dataGridView1.Rows[ii].Cells["LAST_UPDATE"].Value);
           if (vl1!="0") {
               cmd0.CommandText=" UPDATE SYS_DB_PART SET LAST_UPDATE = 0 WHERE ID=" + id;
               try
               {
                 reader = cmd0.ExecuteReader();
               }
               catch (Exception ex1)
               {
                 MessageBox.Show(ex1.ToString() );
               }
               reader.Close();
           }
        
        
           vl1 = Convert.ToString(dataGridView1.Rows[ii].Cells["LAST_RELINK"].Value);
           if (vl1!="0") {
               cmd0.CommandText=" UPDATE SYS_DB_PART SET LAST_RELINK = 0 WHERE ID=" + id;
               try
               {
                 reader = cmd0.ExecuteReader();
               }
               catch (Exception ex1)
               {
                 MessageBox.Show(ex1.ToString() );
               }
               reader.Close();
           }
        
        }
        StatusLoad( ) ;

    }
    void Button2Click(object sender, EventArgs e)
    {
        this.Close();
    }


  }
}
