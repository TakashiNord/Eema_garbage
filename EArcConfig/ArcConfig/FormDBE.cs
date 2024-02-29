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

    public List<String> a  = new List<String>();
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

    public void DBE_COMBO(object sender)
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      // Объект для связи между базой данных и источником данных
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }

      string sl1 = "SELECT dj.ID_STORAGE,ds.NAME AS STORAGE,dj.ID,dj.NAME " +
                   " FROM {0}DBE_JOB dj , {0}DBE_STORAGE ds " +
                   " WHERE ds.ID=dj.ID_STORAGE " +
                   " ORDER by dj.ID_STORAGE " ;

      sl1 = String.Format(sl1,stSchema);

      cmd0.CommandText=sl1;
      try
      {
         reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.Message);
        return ;
      }

      Application.DoEvents();

      if (reader.HasRows) {
        int ii = 0 ;
        while (reader.Read())
        {
            string s0 = GetTypeValue(ref reader, 0) ;
            string s1 = GetTypeValue(ref reader, 1) ;
            string s2 = GetTypeValue(ref reader, 2) ;
            string s3 = GetTypeValue(ref reader, 3) ;
            comboBox1.Items.Insert( ii, "[ "+s1+" ] - "+s3+" ("+s2+")" );
            a.Insert( ii, s2 );
            ii++;
        } // while
      }
      reader.Close();

      cmd0.Dispose();
      Application.DoEvents();

    }

    public void DBE_TABLE(string ind)
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }

      dataSet1.Clear();
      dataSet1.Tables.Clear();

      try {
        dataGridViewList.DataSource = null ;
        dataGridViewList.Rows.Clear();
        Application.DoEvents();
        dataGridViewList.Columns.Clear();
      }
      catch (Exception ex1)
      {
        MessageBox.Show("Error 1 ="+ex1.Message);
      }

      string sl1 = "" +
"SELECT dd.ID_ACTION,da.NAME,dd.ID_TABLE,dd.ID_PARAM,dd.ID_GTOPT " +
"FROM {0}DBE_ACTION da, {0}DBE_DESTINATION dd " +
"WHERE dd.ID_ACTION=da.ID and da.ID_JOB={1} " +
"ORDER by dd.ID_ACTION " ;

      sl1=String.Format(sl1,stSchema,ind);

      cmd0.Connection=this._conn;
      cmd0.CommandText=sl1;

      adapter.SelectCommand = cmd0; // Указываем запрос для выполнения

      int ai = 0;
      // Заполняем объект источника данных
      try {
        ai = adapter.Fill(dataSet1);
      }
      catch (Exception ex1)
      {
        MessageBox.Show("Error 2 =\n" + " result =" + ai + "\n" +ex1.Message);
      }

      try {
        // Запрет удаления данных
        dataSet1.Tables[0].DefaultView.AllowDelete = false;
        // Запрет модификации данных
        dataSet1.Tables[0].DefaultView.AllowEdit = false;
        // Запрет добавления данных
        dataSet1.Tables[0].DefaultView.AllowNew = false;

        // (с этого момента она будет отображать его содержимое)
        dataGridViewList.DataSource = dataSet1.Tables[0];
      }
      catch (Exception ex1)
      {
        MessageBox.Show("Error 3 ="+ex1.Message);
      }

      // Set up the data source.
      dataGridViewList.Update();

      for (int ii = 0; ii < dataGridViewList.RowCount ; ii++) {
        // нумерация
        dataGridViewList.Rows[ii].HeaderCell.Value = (ii + 1).ToString();
      } //for


      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridViewList.AutoResizeColumns();

      cmd0.Dispose();
      Application.DoEvents();

    }

    void FormDBELoad(object sender, EventArgs e)
    {
      //
      this.Text = " DBE :  " ;
      a.Clear();
      comboBox1.Items.Clear();
      DBE_COMBO(sender) ;
    }
    void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
    {
      int ind=comboBox1.SelectedIndex;
      if (ind<0) return ;
      String nm = a[ind] ;
      DBE_TABLE(nm);
    }




  }
}
