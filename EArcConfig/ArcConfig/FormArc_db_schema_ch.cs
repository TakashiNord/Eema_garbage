/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 01.03.2024
 * Time: 12:21
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
  /// Description of FormArc_db_schema_ch.
  /// </summary>
  public partial class FormArc_db_schema_ch : Form
  {
    public FormArc_db_schema_ch()
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

    int _checkCol( string nameCol , string nameTbl )
    {
       int is_exists = 1 ;
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd = new OdbcCommand();
       OdbcDataReader reader1 = null ;

       cmd.Connection = this._conn; // уже созданное и открытое соединение

       // определяем поле
       //ERROR [42S22] [Oracle][ODBC][Ora]ORA-00904: недопустимый идентификатор

       cmd.CommandText="SELECT " + nameCol + " FROM "+nameTbl ;
       try
       {
          reader1 = cmd.ExecuteReader();
          if (!reader1.IsClosed ) reader1.Close();
       }
       catch (Exception ex1)
       {
          is_exists = 0 ;
       }
       //finally
       //{
       //   is_exists = 0 ;
       //}
       //if (!reader1.IsClosed ) reader1.Close();
       reader1 = null ;
       cmd.Dispose();
       return (is_exists) ;
    }

    public OdbcConnection _conn;
    //public string stSchema="";
    public int _OptionSchemaName = 0;
    public string OptionSchemaMain = "RSDUADMIN";

    List<String> b = new List<String>();
    List<String> bi = new List<String>();

    public class asi {
      public string ID_LSTTBL {get; set;}
      public string ID_DB_SCHEMA {get; set;}
      public asi(string sa , string sb) {
        ID_LSTTBL = sa ;
        ID_DB_SCHEMA = sb ;
      }
    }  ;
    List<asi> lasi = new List<asi>();

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormArc_db_schema_ch(OdbcConnection conn, int  OptionSchemaName )
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


    void Select0(object sender)
    {
       // Объект для связи между базой данных и источником данных
       OdbcDataAdapter adapter = new OdbcDataAdapter();
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();
       OdbcDataReader reader = null ;

       cmd0.Connection=this._conn;

       Application.DoEvents();

       string stSchema="";
       if (_OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
       }

       cmd0.CommandText="SELECT ID_LSTTBL,ID_DB_SCHEMA " +
                        " FROM " + stSchema + "ARC_SERVICES_INFO " ;
       try
       {
          reader = cmd0.ExecuteReader();
       }
       catch (Exception ex1)
       {
         MessageBox.Show("Error : " + cmd0.CommandText + " " + ex1.Message);
         return ;
       }
       if (reader.HasRows) {
         while (reader.Read())
         {
             String vS0 = GetTypeValue(ref reader, 0) ;
             String vS1 = GetTypeValue(ref reader, 1) ;
             lasi.Add(new asi(vS0,vS1) );
         } // while
       }
       reader.Close();

    }

    void Select3(object sender)
    {
       // Объект для связи между базой данных и источником данных
       OdbcDataAdapter adapter = new OdbcDataAdapter();
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();
       OdbcDataReader reader = null ;

       cmd0.Connection=this._conn;

       Application.DoEvents();

       var column1 = new DataGridViewColumn();
       column1.HeaderText = "Наименование";
       column1.Name = "Name";
       column1.ReadOnly = true; //значение в этой колонке нельзя править
       column1.CellTemplate = new DataGridViewTextBoxCell();

       var column2 = new DataGridViewColumn();
       column2.HeaderText = "id"; //текст в шапке
       column2.ReadOnly = true; //значение в этой колонке нельзя править
       column2.Name = "id"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
       //column2.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
       column2.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

       DataGridViewCheckBoxColumn column3 = new DataGridViewCheckBoxColumn();
       column3.HeaderText = "Bind" ;
       column3.TrueValue = true;
       column3.FalseValue = false;
       column3.Name = "Status";
       column3.SortMode = DataGridViewColumnSortMode.Automatic;
       DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
          // auto profile select  another color
          //cellStyle.BackColor = Color.LightGray;
          //cellStyle.SelectionBackColor = Color.Red;
       cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
       column3.DefaultCellStyle = cellStyle;

       DataGridViewComboBoxColumn column4 = new DataGridViewComboBoxColumn();
       column4.HeaderText = "Archive";
       column4.Name = "arc";
       column4.MaxDropDownItems=12;
       column4.Width=300 ;
       column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
       column4.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton ;

       //column4.DropDownStyle = ComboBoxStyle.DropDownList;

       string stSchema="";
       if (_OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
       }

       /*
        * проверка на существование ARC_DB_SCHEMA.ID_STORAGE_TYPE
        * получение типа бд хранилища
        */

       if (0==_checkCol( "ID_STORAGE_TYPE" , stSchema+"ARC_DB_SCHEMA" ))
       cmd0.CommandText="select ads.ID, ads.NAME, ads.SCHEMA_NAME , '' as STORAGE " +
       "from " + stSchema + "ARC_DB_SCHEMA ads " +
       "order by ads.ID asc " ;
       else
       cmd0.CommandText="select ads.ID, ads.NAME, ads.SCHEMA_NAME , ast.DEFINE_ALIAS as STORAGE " +
       "from " + stSchema + "ARC_DB_SCHEMA ads, " + stSchema + "ARC_STORAGE_TYPE ast " +
       "where ads.ID_STORAGE_TYPE=ast.ID " +
       "order by ads.ID asc " ;

       try
       {
          reader = cmd0.ExecuteReader();
       }
       catch (Exception ex1)
       {
         MessageBox.Show("Error : " + cmd0.CommandText + " " + ex1.Message);
         return ;
       }
       if (reader.HasRows) {
         int iSa = 0 ;
         while (reader.Read())
         {
           String vS0 = GetTypeValue(ref reader, 0) ;
           String vS1 = GetTypeValue(ref reader, 1) ;
           String vS2 = GetTypeValue(ref reader, 2) ;
           String vS3 = GetTypeValue(ref reader, 3) ;
           bi.Insert(iSa,vS0);
           if (vS3.Trim()==String.Empty)
             b.Insert(iSa,vS1  + "|[" + vS2 + "]|" + vS3);
           else
             b.Insert(iSa,vS1  + "|[" + vS2 + "]");

           column4.Items.Insert(iSa,b[iSa]);

           iSa++;
         } // while
       }
       reader.Close();

       dataGridView1.Columns.Add(column1);
       dataGridView1.Columns.Add(column2);
       dataGridView1.Columns.Add(column3);
       dataGridView1.Columns.Add(column4);

       dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки

       dataGridView1.Update();

    }

    void Select5(object sender)
    {
       // Объект для связи между базой данных и источником данных
       OdbcDataAdapter adapter = new OdbcDataAdapter();
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();
       OdbcDataReader reader = null ;

       cmd0.Connection=this._conn;

       ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

       string stSchema="";
       if (_OptionSchemaName>0) {
         stSchema=OptionSchemaMain + "." ;
       }

       Application.DoEvents();

       cmd0.CommandText=r.GetString("FormArc_db_schema_ch1");
       try
       {
          reader = cmd0.ExecuteReader();
       }
       catch (Exception ex1)
       {
         MessageBox.Show("Error : " + cmd0.CommandText + " " + ex1.Message);
         return ;
       }
       if (reader.HasRows) {
         int iRowIndex = 0 ;
         while (reader.Read())
         {
           dataGridView1.Rows.Add();

           // id,id_parent, name, id_lsttbl

           String vS2 = GetTypeValue(ref reader, 2) ;
           String vS3 = GetTypeValue(ref reader, 3) ;

           dataGridView1.Rows[iRowIndex].Cells[0].Value = vS2;
           dataGridView1.Rows[iRowIndex].Cells[1].Value = vS3;
           dataGridView1.Rows[iRowIndex].Cells[2].Value = CheckState.Unchecked;

           dataGridView1.Rows[iRowIndex].HeaderCell.Value = (iRowIndex + 1).ToString();
           iRowIndex++;
         } // while
       }
       reader.Close();

       dataGridView1.Update();

       // Resize the master DataGridView columns to fit the newly loaded data.
       dataGridView1.AutoResizeColumns();

       for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
       {
         for (int ii = 0; ii < lasi.Count; ++ii)
         {
           string slst = lasi[ii].ID_LSTTBL ;
           string sdbs = lasi[ii].ID_DB_SCHEMA ;
           if (slst==dataGridView1.Rows[jj].Cells[1].Value.ToString() ) {
             dataGridView1.Rows[jj].Cells[2].Value = CheckState.Checked;
             DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
             cell = dataGridView1.Rows[jj].Cells[3] as DataGridViewComboBoxCell;
             for (int ik = 0; ik < bi.Count; ++ik)
             {
               if (bi[ik]==sdbs) {
                 cell.Value = cell.Items[ik];
                 try
                 {
                   dataGridView1.Rows[jj].Cells[3] = cell as DataGridViewCell;
                 }
                 catch (Exception ex1)
                 {
                  ;
                 }
                 break ;
               }
             }
             cell.Dispose();
             continue ;
           }
         }
       }
       dataGridView1.Update();

       // Resize the master DataGridView columns to fit the newly loaded data.
       dataGridView1.AutoResizeColumns();
    }


    void FormArc_db_schema_chLoad(object sender, EventArgs e)
    {
      //
      bi.Clear();
      b.Clear();
      Select0(sender) ; // ARC_SERVICES_INFO
      Select3(sender) ; // grid + ARC_DB_SCHEMA
      Select5(sender) ; // systblst
    }
    void DataGridView1DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      e.Cancel = true ; //false;
    }
    void ButCancelClick(object sender, EventArgs e)
    {
      this.Close();
    }
    void ButOkClick(object sender, EventArgs e)
    {
      // ok
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string sl1 = "" ;

      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }

      // delete
      sl1="DELETE FROM "+stSchema+"ARC_SERVICES_INFO ";
      int crec1 = 0 ;
      cmd0.CommandText=sl1;
      try
      {
        crec1=cmd0.ExecuteNonQuery();
      }
      catch (Exception ex1)
      {
        MessageBox.Show("Error : " + cmd0.CommandText + " " + ex1.Message);
        return ;
      }

      // insert
      for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
      {
        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridView1.Rows[jj].Cells[2] ;
      	if (Convert.ToBoolean(chk.Value) == true) {
          string slst = dataGridView1.Rows[jj].Cells[1].Value.ToString() ;
          DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
          cell = dataGridView1.Rows[jj].Cells[3] as DataGridViewComboBoxCell;
          for (int ik = 0; ik < bi.Count; ++ik)
          {
          	if (b[ik]==cell.Value.ToString()) {
              string sdbs = bi[ik] ;
              sl1="INSERT INTO  "+stSchema+"ARC_SERVICES_INFO (ID_LSTTBL,ID_DB_SCHEMA) VALUES " +
                  " ( " + slst + " , "+ sdbs + " ) ;";
              crec1 = 0 ;
              cmd0.CommandText=sl1;
              try
              {
                crec1=cmd0.ExecuteNonQuery();
              }
              catch (Exception ex1)
              {
                 MessageBox.Show("Error : " + cmd0.CommandText + " " + ex1.Message);
                 return ;
              }

              break ;
            }
          }
          cell.Dispose();
        }
      }

      this.Close();
    }


  }
}
