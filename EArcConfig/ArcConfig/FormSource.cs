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

using System.Collections.Generic;
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

    public FormSource(OdbcConnection conn, int SchemaName)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
       _conn = conn ;
       _OptionSchemaName = SchemaName ;
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

    public List<String> aa = new List<String>();
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



    public void SOURCE_TABLE(String _name)
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      dataSet1.Clear();
      dataSet1.Tables.Clear();

      try {
        dataGridView1.DataSource = null ;
        dataGridView1.Rows.Clear();
        Application.DoEvents();
        dataGridView1.Columns.Clear();
      }
      catch (Exception ex1)
      {
        MessageBox.Show("Error 1 ="+ex1.Message);
      }

      string sl1 = "SELECT * FROM " + _name + " ORDER BY ID ASC " ;

      cmd0.Connection=this._conn;
      cmd0.CommandText=sl1;

      adapter.SelectCommand = cmd0; // Указываем запрос для выполнения

      int ai = 0;
      // Заполняем объект источника данных
      try {
        ai = adapter.Fill(dataSet1,_name);
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
        dataGridView1.DataSource = dataSet1.Tables[0];
      }
      catch (Exception ex1)
      {
        MessageBox.Show("Error 3 ="+ex1.Message);
      }

      // Set up the data source.
      dataGridView1.Update();

      for (int ii = 0; ii < dataGridView1.RowCount ; ii++) {
        // нумерация
        dataGridView1.Rows[ii].HeaderCell.Value = (ii + 1).ToString();
      } //for


      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridView1.AutoResizeColumns();

      cmd0.Dispose();
      Application.DoEvents();

    }


    void FormSourceLoad(object sender, EventArgs e)
    {
      //
      aa.Clear();
      aa.Insert(0,"Получения значений параметров (CALC_SOURCE)");
      aa.Insert(1,"Для рапределенной системы сбора (DA_SOURCE)");
      aa.Insert(2,"Получения значений параметров ДГ (DG_SOURCE)");
      aa.Insert(3,"Для параметров учета электроэнергии (EA_SOURCE)");
      aa.Insert(4,"Получения значений параметров (MEAS_SOURCE)");

      a.Clear();
      a.Insert(0,"CALC_SOURCE");
      a.Insert(1,"DA_SOURCE");
      a.Insert(2,"DG_SOURCE");
      a.Insert(3,"EA_SOURCE");
      a.Insert(4,"MEAS_SOURCE");

      comboBox1.Items.Clear();

      for (int ii = 0; ii < aa.Count ; ii++)
        comboBox1.Items.Insert( ii,aa[ii] );

    }
    void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
    {
      int ind=comboBox1.SelectedIndex;
      if (ind<0) return ;

      String nm = a[ind] ;

      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }

      this.Text = "  :  " + stSchema+nm ;
      SOURCE_TABLE(stSchema+nm);
    }


  }
}
