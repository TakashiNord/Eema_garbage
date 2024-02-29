/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 12.02.2024
 * Time: 15:56
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
  /// Description of FormParamChange.
  /// </summary>
  public partial class FormParamChange : Form
  {
    public FormParamChange()
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

/*
 формирование полного пути (имени) параметра с учетом родителя
*/
   public string GetFullName(String id,  String TableTree )
   {
     // Объект для выполнения запросов к базе данных
     OdbcCommand cmd0 = new OdbcCommand();
     cmd0.Connection=this._conn;
     OdbcDataReader reader=null ;
     String NAME = "";
     String ID = id ;

     string[] arr = new string[3];
     int fl = 0;
     while (fl==0)
     {
       // ORA 1000 !!!!!!
       cmd0.CommandText="SELECT ID, ID_PARENT, NAME FROM " + TableTree + " WHERE ID="+ID ;
       try
       {
         reader = cmd0.ExecuteReader();
       }
       catch (Exception )
       {
         fl=1;
         reader.Close();
         //MessageBox.Show("Error ="+ex1.Message + "  id=" + id);
         continue ;
       }

       if (reader.HasRows) {
         while (reader.Read())
         {
           for ( int i = 0; i<reader.FieldCount; i++)
           {
             if (reader.IsDBNull(i)) {
                arr[i]="" ;
             } else {
                object obj = reader.GetValue(i) ;
                arr[i] = obj.ToString();
             }
           }
         }

         ID = arr[1]; // ID_PARENT
         if (ID=="") fl = 1 ;

         NAME=arr[2]+ "\\" + NAME ;
       }
       reader.Close();

     } // while

     reader=null ;
     cmd0.Dispose(); // ORA 1000 !!!!!!

     return(NAME);
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
    public String _id;
    public String _name;
    public String _id_gpt;
    public String _gpt_name;
    public String _id_tbl;
    public String TABLE_NAME ;
    public int OptionFullName ;
    public int _OptionSchemaName = 0;
    public int OptionSaveFormat = 0;
    public string OptionSchemaMain = "RSDUADMIN";
    List<String> _idTO = new List<String>();
    List<String> _idTO_GTOPT = new List<String>();

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormParamChange(OdbcConnection conn, String id, String name, String gopt,String gpt_name, String tbl, int FullName, int SchemaName )
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      _conn = conn ;
      _id = id ;
      _name = name ;
      _id_gpt = gopt ;
      _gpt_name = gpt_name ;
      _id_tbl = tbl ;
      TABLE_NAME = "" ;
      OptionFullName = FullName ;
      _OptionSchemaName = SchemaName ;
    }


    public void ShownBase(object sender)
    {
        //
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;
        cmd0.Connection=this._conn;

        string stSchema="";
        if (_OptionSchemaName>0) {
          stSchema=OptionSchemaMain + "." ;
        }

        try {
          dataGridView1.DataSource = null ;
          dataGridView1.Rows.Clear();
          Application.DoEvents();
          dataGridView1.Columns.Clear();
        }
        catch (Exception ex1)
        {
          MessageBox.Show("Error ="+ex1.Message);
        }
        Application.DoEvents();

        DataGridViewCheckBoxColumn column00 = new DataGridViewCheckBoxColumn();
        column00.HeaderText = "Status" ;
        column00.TrueValue = true;
        column00.FalseValue = false;
        column00.Name = "ST";
        column00.SortMode = DataGridViewColumnSortMode.Automatic;
        DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
        cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        column00.DefaultCellStyle = cellStyle;

        dataGridView1.Columns.Add(column00);

        var column0 = new DataGridViewColumn();
        column0.HeaderText = "id"; //текст в шапке
        //column1.Width = 100; //ширина колонки
        column0.ReadOnly = true; //значение в этой колонке нельзя править
        column0.Name = "id"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
        //column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
        column0.SortMode = DataGridViewColumnSortMode.Automatic;
        column0.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

        dataGridView1.Columns.Add(column0);

        var column1 = new DataGridViewColumn();
        column1.HeaderText = "Группа";
        column1.Name = "Group";
        column1.ReadOnly = true;
        column1.CellTemplate = new DataGridViewTextBoxCell();

        dataGridView1.Columns.Add(column1);

        var column2 = new DataGridViewColumn();
        column2.HeaderText = "Наименование";
        column2.Name = "Name";
        column2.SortMode = DataGridViewColumnSortMode.Automatic;
        column2.CellTemplate = new DataGridViewTextBoxCell();

        dataGridView1.Columns.Add(column2);

        dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки



        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        string sl1 ;

        // get table параметров
        string TABLE_NAME = "" ;

        sl1= "SELECT UPPER(TABLE_NAME) FROM "+stSchema+"sys_tbllst WHERE ID=" + _id_tbl;
        cmd0.CommandText=sl1;
        try
        {
          reader = cmd0.ExecuteReader();
        }
        catch (Exception ex1)
        {
          MessageBox.Show(ex1.ToString() );
          return ;
        }

        if (reader.HasRows) {
          while (reader.Read())
          {
            TABLE_NAME = GetTypeValue(ref reader, 0);
            TABLE_NAME = TABLE_NAME.ToUpper() ;
            break ;
          } // while
        }
        reader.Close();


        Application.DoEvents();


        // определяем поле
        int is_exdata = 1 ;
        if (0==_checkCol( "IS_EXDATA" , stSchema+"MEAS_LIST" )) is_exdata = 0 ;


        int ex_run = 1 ;

        string TableTree = stSchema+"OBJ_TREE" ;

        if (TABLE_NAME.IndexOf("PHREG_LIST_V")>=0) {
            sl1 = r.GetString("PHREG_LIST_V0");
            if (0==_checkCol( "ID" , "PHREG_LIST_V" )) {
              sl1 = r.GetString("PHREG_LIST_V00");
              if (is_exdata == 1 ) sl1 = sl1 + " AND is_exdata = 0 ";
            }
        }
        if (TABLE_NAME.IndexOf("ELREG_LIST_V")>=0) {
            sl1 = r.GetString("ELREG_LIST_V0");
            if (0==_checkCol( "ID" , "ELREG_LIST_V" )) {
              sl1 = r.GetString("ELREG_LIST_V00");
              if (is_exdata == 1 ) sl1 = sl1 + " AND is_exdata = 0 ";
            }
        }
        if (TABLE_NAME.IndexOf("PSWT_LIST_V")>=0) {
            ex_run = 0 ;
        }
        if (TABLE_NAME.IndexOf("AUTO_LIST_V")>=0) {
            ex_run = 0 ;
        }
        if (TABLE_NAME.IndexOf("EA_CHANNELS")>=0) {
            ex_run = 0 ;
        }
        if (TABLE_NAME.IndexOf("EA_V_CONSUMER_POINTS")>=0) {
            ex_run = 0 ;
        }
        if (TABLE_NAME.IndexOf("CALC_LIST")>=0) {
            ex_run = 0 ;
        }
        if (TABLE_NAME.IndexOf("DG_LIST")>=0) {
            ex_run = 0 ;
        }
        if (TABLE_NAME.IndexOf("EXDATA_LIST_V")>=0) {
        	if (is_exdata == 1 ) {
              sl1 = r.GetString("EXDATA_LIST_V0");
              if (0==_checkCol( "ID" , "EXDATA_LIST_V" )) {
                sl1 = r.GetString("EXDATA_LIST_V00");
              }
        	}
        }
        if (TABLE_NAME.IndexOf("DA_V_LST")>=0) {
            ex_run = 0 ;
        }

        
        if (TABLE_NAME.Length<=1) {
          MessageBox.Show(" Не задана таблица раздела = " + TABLE_NAME + " .." );
          return ;
        }

        if (ex_run==0) {
          MessageBox.Show (" Для раздела " + TABLE_NAME + "\n перенос параметров не осуществим.."
              ,"Перенос параметров в другой архив", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
          return ;
        }


        string s="SELECT v.ID,v.ID_NODE,v.NAME FROM "+stSchema+"MEAS_ARC mc, ( {0} ) v WHERE mc.ID_PARAM=v.ID AND mc.ID_GINFO={1}" ;
        sl1 = String.Format(s,sl1,_id );
        cmd0.CommandText=sl1;
        try
        {
          reader = cmd0.ExecuteReader();
        }
        catch (Exception ex1)
        {
          MessageBox.Show(" выполнение алгоритма  - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
          return ;
        }

        if (reader.HasRows==false) {
          reader.Close();
          return;
        }


        String sV1 ;
        int iRowIndex = 0 ;
        while (reader.Read())
        {
          iRowIndex=dataGridView1.Rows.Add();

          dataGridView1.Rows[iRowIndex].HeaderCell.Value = (iRowIndex + 1).ToString();

          dataGridView1.Rows[iRowIndex].Cells[0].Value = CheckState.Unchecked ;
          sV1 = GetTypeValue(ref reader, 0) ;
          dataGridView1.Rows[iRowIndex].Cells[1].Value = sV1.ToString();

          String nd = "" ;
          if (OptionFullName>0) nd=GetFullName(GetTypeValue(ref reader, 1),TableTree) ;
          dataGridView1.Rows[iRowIndex].Cells[2].Value = nd.ToString();

          sV1 = GetTypeValue(ref reader, 2) ;
          dataGridView1.Rows[iRowIndex].Cells[3].Value = sV1.ToString();

        } // while
        reader.Close();

         // Resize the master DataGridView columns to fit the newly loaded data.
        dataGridView1.AutoResizeColumns();

        //dataGridView1.Update();

        Application.DoEvents();

    }

    public void ShownArc(object sender)
    {
        //
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;
        cmd0.Connection=this._conn;

        string stSchema="";
        if (_OptionSchemaName>0) {
          stSchema=OptionSchemaMain + "." ;
        }

        comboBox1.Items.Clear();
        _idTO.Clear();
        _idTO_GTOPT.Clear();

        Application.DoEvents();

        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        string sl1= r.GetString("ARC_SUBSYST_PROFILE5");
        sl1 = String.Format(sl1,_id_tbl);

        cmd0.CommandText=sl1;
        try
        {
          reader = cmd0.ExecuteReader();
        }
        catch (Exception ex1)
        {
          MessageBox.Show(ex1.ToString() );
          return ;
        }

        if (!reader.HasRows) {
          reader.Close();
          return ;
        }

        int rI=0;
        string st = "" ;
        string[] arr = new string[5];

        while (reader.Read())
        {
          for ( int i = 0; i<5; i++)
          {
             arr[i]= GetTypeValue(ref reader, i);
          }

          Application.DoEvents();

          if (arr[1]==_id) continue ;

          //ARC_SUBSYST_PROFILE.ID, ARC_SUBSYST_PROFILE.ID_GINFO, ARC_GINFO.NAME,ARC_GINFO.ID_GTOPT, SYS_GTOPT.NAME
          st = "[" +arr[1]+"]" + "  '" +arr[2]+"'" + "  [" +arr[4]+"]" ;
          comboBox1.Items.Insert(rI,st);
          _idTO.Insert(rI,arr[1]);
          _idTO_GTOPT.Insert(rI,arr[3]);

        } // while
        reader.Close();


        Application.DoEvents();

    }


    void FormParamChangeLoad(object sender, EventArgs e)
    {
      //
      this.Text = this._gpt_name + " : " + this._name ;
      ShownArc(sender);
    }
    void ButtonPARAMClick(object sender, EventArgs e)
    {
      ShownBase(sender) ;
      //ShownArc(sender);
    }
    void ButtonSELClick(object sender, EventArgs e)
    {
      // select all
      for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
      {
        dataGridView1.Rows[jj].Cells[0].Value = CheckState.Checked ;
      } // for
    }
    void ButtonUSClick(object sender, EventArgs e)
    {
      //unselect
      for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
      {
        dataGridView1.Rows[jj].Cells[0].Value = CheckState.Unchecked ;
      } // for
    }
    void ButtonSTARTClick(object sender, EventArgs e)
    {
      DialogResult result1;
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      cmd0.Connection=this._conn;

      // перенос архивов в другой архив
      if (dataGridView1.Rows.Count<=0) return;

      int iT = comboBox1.SelectedIndex ;
      if (iT<0) return ;

      int cnt = 0;
      for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
      {
        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridView1.Rows[jj].Cells[0] ;
        if ( Convert.ToBoolean(chk.Value) == true ) cnt++ ;
      } // for
      if (cnt<=0) return ;

      string sT = comboBox1.SelectedItem.ToString();

      result1 = MessageBox.Show (" Перенести " + cnt.ToString() + " параметра(-ов) "
           + "\n - архива : '" + _name + "' [ " + _gpt_name + " ] "
           + "\n-------------------------------------------\n в архив :" + sT ,
           "Перенос параметров в другой архив", MessageBoxButtons.YesNo,
           MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 );
      if (result1 == DialogResult.No)
      {
        return ;
      }

      //MessageBox.Show( _idTO[iT].ToString() + " , " + _idTO_GTOPT[iT].ToString() );

      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }

      string s0="SELECT count(ID_PARAM) FROM "+stSchema+"MEAS_ARC "
             + " WHERE ID_PARAM={0} AND ID_GINFO={1}" ;
      string s1="UPDATE "+stSchema+"MEAS_ARC SET ID_GINFO={0} , ID_GTOPT={1} "
             + " WHERE ID_PARAM={2} AND ID_GINFO={3}" ;

      int err = 0 ;
      int res1 = 0 ; int res2 = 0 ;
      object res0 ;
      int Total_Records = 0;
      for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
      {
        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridView1.Rows[jj].Cells[0] ;
        if ( Convert.ToBoolean(chk.Value) == true ) {

          String id_param = dataGridView1.Rows[jj].Cells[1].Value.ToString() ; // ID

          // проверяем, что архива уже не существует
          res0 = DBNull.Value ;
          cmd0.CommandText=String.Format(s0,id_param, _idTO[iT] );
          try
          {
            res0 = cmd0.ExecuteScalar();
          }
          catch (Exception )
          {
            err ++ ;
          }

          if (res0 == DBNull.Value)
            Total_Records = 0;
          else
            Total_Records = System.Convert.ToInt32(res0);
          if (Total_Records!=0) res2++;

          if (Total_Records==0) {
            cmd0.CommandText=String.Format(s1, _idTO[iT],_idTO_GTOPT[iT], id_param, _id );
            try
            {
              res1+= cmd0.ExecuteNonQuery();
            }
            catch (Exception )
            {
              err ++ ;
              //continue ;
            }
          }

          Application.DoEvents();

        }
      } // for

      cmd0.Dispose();

      MessageBox.Show (" Процесс переноса завершен."
         + "\n перенесено =  " +res1.ToString() + " параметр(-ов)"
         + "\n ошибок (количество) =  " +err.ToString() + " "
         + "\n не перенесено (уже существуют) =  " +res2.ToString() + " "
      ,"Перенос параметров в другой архив", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );

      this.Close();

    }

  }
}
