/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 27.02.2024
 * Time: 13:16
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
	/// Description of FormVariable.
	/// </summary>
	public partial class FormVariable : Form
	{
		public FormVariable()
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
    public string stSchema="";


    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormVariable(OdbcConnection conn, String name )
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      _conn = conn ;
      stSchema = name ;
    }
	
    public void VAR_TABLE(object sender)
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      dataSet1.Clear();

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

      string sl1 = "SELECT asi.ID,ad.NAME as DESC,ad.ALIAS, rip.NAME,asi.VALUE,rip.DESCRIPTION " +
" FROM "+stSchema+"AD_SINFO_INI asi, "+stSchema+"AD_DIR ad, "+stSchema+"RSDU_INI_PARAM rip " +
" WHERE asi.ID_SERVER_NODE=ad.id and asi.ID_INI_PARAM=rip.id "+
" ORDER BY asi.ID_SERVER_NODE  ";
 
      cmd0.Connection=this._conn;
	  cmd0.CommandText=sl1;
      
      adapter.SelectCommand = cmd0; // Указываем запрос для выполнения

      int a = 0;
      // Заполняем объект источника данных
      try {
        a = adapter.Fill(dataSet1);
      }
      catch (Exception ex1)
      {
        MessageBox.Show("Error 2 =\n" + " result =" + a + "\n" +ex1.Message);
      }

      try {
        // Запрет удаления данных
        dataSet1.Tables[0].DefaultView.AllowDelete = false;
        // Запрет модификации данных
        dataSet1.Tables[0].DefaultView.AllowEdit = false;
        // Запрет добавления данных
        dataSet1.Tables[0].DefaultView.AllowNew = false;

        // (с этого момента она будет отображать его содержимое)
        dataGridViewList.DataSource = dataSet1.Tables[0].DefaultView;
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

        void FormVariableLoad(object sender, EventArgs e)
		{
			//
			this.Text = "  :  " ;
			VAR_TABLE(sender) ;
		}
				
		
		
	}
}
