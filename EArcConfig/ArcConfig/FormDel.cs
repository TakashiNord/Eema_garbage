/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 24.03.2021
 * Time: 11:38
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
  /// Description of FormDel.
  /// </summary>
  public partial class FormDel : Form
  {
    public FormDel()
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


    List<MEAS1> dp = new List<MEAS1>();
    public DataGridViewCellMouseEventArgs /*DataGridViewCellEventArgs*/ mouseLocation;
    public int PCellValueChanged = 0;

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

    public int valueBefore = 0;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormDel(OdbcConnection conn, String id, String name, String ginfo,String gpt_name, String tbl, int FullName, int SchemaName, int SaveFormat)
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
      _id_gpt = ginfo ;
      _gpt_name = gpt_name ;
      _id_tbl = tbl ;
      TABLE_NAME = "" ;
      OptionFullName = FullName ;
      _OptionSchemaName = SchemaName ;
      OptionSaveFormat = SaveFormat ;
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

    // public bool IsClearingNow(int lsttbl)
    public string test_if_job( string pID_tbllst )
    {
      /*
       test_if_job:
         внешняя процедура для проверки завершились ли все джоб предыдущей чистки
        FUNCTION test_if_job( pID_tbllst NUMBER)
        RETURN NUMBER
      */

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();

      cmd0.Connection=this._conn;

      string vRetVal = "0";

      cmd0.CommandType = System.Data.CommandType.StoredProcedure;
      cmd0.Parameters.Clear();

      cmd0.CommandText = "{ ? = call arc_ctrl_pkg.test_if_job(?) }";

      OdbcParameter parOut = new OdbcParameter();
      parOut.Direction = System.Data.ParameterDirection.ReturnValue;
      parOut.OdbcType = OdbcType.Decimal;
      parOut.ParameterName = "retval";

      cmd0.Parameters.Add(parOut);

      OdbcParameter param1 = new OdbcParameter();
      param1.Direction = System.Data.ParameterDirection.Input;
      param1.OdbcType = OdbcType.Decimal;
      param1.ParameterName = "pID_tbllst";
      param1.Value = Convert.ToDecimal(pID_tbllst);

      cmd0.Parameters.Add(param1);

      cmd0.CommandTimeout = 120;

      try
      {
         cmd0.ExecuteNonQuery();
         vRetVal = cmd0.Parameters["retval"].Value.ToString() ;
      }
      catch (Exception )
      {
        vRetVal = "-1" ;
        //throw;
      }

      return (vRetVal);
    }

    // public void ClearHist(int idTable, IEnumerable<int> idGinfos, DateTime endTime, bool toFile)
    // endTime.ToString("dd.MM.yyyy")
    public string clear_one_schema_job( string pID_tbllst , string pID_ginfo , string pEndDate , int pToFile )
    {
      /*
      -- clear_one_schema_job:
      --   внешняя процедура для вызова из приложения
      --   Создает джобы чистки для указанного раздела (по Id_tbllst) и указанных через запятую ID_GINFO
        PROCEDURE clear_one_schema_job( pID_tbllst NUMBER, pID_ginfo VARCHAR2, pEndDate VARCHAR2, pToFile
      */

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      cmd0.Connection=this._conn;

      string vRetVal = "0";

      cmd0.CommandType = System.Data.CommandType.StoredProcedure;
      cmd0.Parameters.Clear();

      cmd0.CommandText = "{ call arc_ctrl_pkg.clear_one_schema_job(?, ?, ?, ?) }";

      OdbcParameter param1 = new OdbcParameter();
      param1.Direction = System.Data.ParameterDirection.Input;
      param1.OdbcType = OdbcType.Decimal;
      param1.ParameterName = "pID_tbllst";
      param1.Value = Convert.ToDecimal(pID_tbllst);

      cmd0.Parameters.Add(param1);

      OdbcParameter param2 = new OdbcParameter();
      param2.Direction = System.Data.ParameterDirection.Input;
      param2.OdbcType = OdbcType.NText;
      param2.ParameterName = "pID_ginfo";
      param2.Value = Convert.ToString (pID_ginfo);

      cmd0.Parameters.Add(param2);

      OdbcParameter param3 = new OdbcParameter();
      param3.Direction = System.Data.ParameterDirection.Input;
      param3.OdbcType = OdbcType.NText;
      param3.ParameterName = "pEndDate";
      param3.Value = Convert.ToString (pEndDate);

      cmd0.Parameters.Add(param3);

      OdbcParameter param4 = new OdbcParameter();
      param4.Direction = System.Data.ParameterDirection.Input;
      param4.OdbcType = OdbcType.Int ;
      param4.ParameterName = "pToFile";
      param4.Value = Convert.ToInt32 (pToFile);

      cmd0.Parameters.Add(param4);

      cmd0.CommandTimeout = 120;

      try
      {
         cmd0.ExecuteNonQuery();
      }
      catch (Exception )
      {
        vRetVal = "-1" ;
        //throw;
      }

      return (vRetVal);
    }

    public void DelShown(object sender)
    {
        //
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;
        cmd0.Connection=this._conn;

        PCellValueChanged = 0;

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

        var column0 = new DataGridViewColumn();
        column0.HeaderText = "Группа";
        column0.Name = "Group";
        column0.ReadOnly = true;
        column0.CellTemplate = new DataGridViewTextBoxCell();

        var column1 = new DataGridViewColumn();
        column1.HeaderText = "id"; //текст в шапке
        //column1.Width = 100; //ширина колонки
        column1.ReadOnly = true; //значение в этой колонке нельзя править
        column1.Name = "id"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
        //column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
        column1.SortMode = DataGridViewColumnSortMode.Automatic;
        column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

        var column2 = new DataGridViewColumn();
        column2.HeaderText = "Наименование";
        column2.Name = "Name";
        column2.SortMode = DataGridViewColumnSortMode.Automatic;
        column2.CellTemplate = new DataGridViewTextBoxCell();

        var column3 = new DataGridViewColumn();
        column3.HeaderText = "Тип";
        column3.Name = "type";
        column3.SortMode = DataGridViewColumnSortMode.Automatic;
        column3.CellTemplate = new DataGridViewTextBoxCell();

        dataGridView1.Columns.Add(column0);
        dataGridView1.Columns.Add(column1);
        dataGridView1.Columns.Add(column2);
        dataGridView1.Columns.Add(column3);

        dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки

        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        string sl1= r.GetString("ARC_SUBSYST_PROFILE3");
        sl1 = String.Format(sl1,_id_tbl,_id);

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

        string[] arr = new string[5];

        while (reader.Read())
        {
          for ( int i = 0; i<reader.FieldCount; i++)
          {
             arr[i]= GetTypeValue(ref reader, i);
          }

         //ARC_SUBSYST_PROFILE.ID, ARC_SUBSYST_PROFILE.ID_GINFO, ARC_SUBSYST_PROFILE.IS_WRITEON,

          DataGridViewCheckBoxColumn column9 = new DataGridViewCheckBoxColumn();
          column9.HeaderText = "Status" ;
          column9.TrueValue = true;
          column9.FalseValue = false;
          column9.Name = arr[1];
          column9.SortMode = DataGridViewColumnSortMode.Automatic;
          DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
          // auto profile select  another color
          if (arr[2]!="0") {
             cellStyle.BackColor = Color.LightGray;
             //cellStyle.SelectionBackColor = Color.Red;
          }
          cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          column9.DefaultCellStyle = cellStyle;

          dataGridView1.Columns.Add(column9);

        } // while
        reader.Close();

        var column4 = new DataGridViewColumn();
        column4.HeaderText = "Min Date";
        column4.Name = "MinDate";
        column4.SortMode = DataGridViewColumnSortMode.Automatic;
        column4.CellTemplate = new DataGridViewTextBoxCell();
        column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

        var column5 = new DataGridViewColumn();
        column5.HeaderText = "Max Date";
        column5.Name = "MaxDate";
        column5.SortMode = DataGridViewColumnSortMode.Automatic;
        column5.CellTemplate = new DataGridViewTextBoxCell();
        column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

        var column6 = new DataGridViewColumn();
        column6.HeaderText = "Count";
        column6.Name = "cnt";
        column6.SortMode = DataGridViewColumnSortMode.Automatic;
        column6.CellTemplate = new DataGridViewTextBoxCell();
        column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

        var column7 = new DataGridViewColumn();
        column7.HeaderText = "RETFNAME";
        column7.Name = "RETFNAME";
        column7.SortMode = DataGridViewColumnSortMode.Automatic;
        column7.CellTemplate = new DataGridViewTextBoxCell();
        column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;


        dataGridView1.Columns.Add(column4);
        dataGridView1.Columns.Add(column5);
        dataGridView1.Columns.Add(column6);
        dataGridView1.Columns.Add(column7);


        // get table
        string TABLE_NAME = "" ;

        sl1= "SELECT upper(lst.TABLE_NAME) FROM sys_tbllst lst WHERE lst.ID=" + _id_tbl;
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

        dp.Clear();
        Application.DoEvents();

        sl1="SELECT 0,0,0,0,0,0,0 FROM DUAL" ;

        string TableTree = "OBJ_TREE" ;

        // определяем поле
        //ERROR [42S22] [Oracle][ODBC][Ora]ORA-00904: "IS_EXDATA": недопустимый идентификатор
        int is_exdata = 1 ;
        sl1="SELECT is_exdata FROM meas_list" ;

        cmd0.CommandText=sl1;
        try
        {
          reader = cmd0.ExecuteReader();
        }
        catch (Exception )
        {
          is_exdata = 0 ;
        }
        reader.Close();


        if (TABLE_NAME.IndexOf("PHREG_LIST_V")>=0) {
            sl1 = r.GetString("PHREG_LIST_V");
            if (is_exdata == 0 ) sl1 = r.GetString("PHREG_LIST_V1");
        }
        if (TABLE_NAME.IndexOf("ELREG_LIST_V")>=0) {
            sl1 = r.GetString("ELREG_LIST_V");
            if (is_exdata == 0 ) sl1 = r.GetString("ELREG_LIST_V1");
        }
        if (TABLE_NAME.IndexOf("PSWT_LIST_V")>=0) {
            sl1 = r.GetString("PSWT_LIST_V");
            if (is_exdata == 0 ) sl1 = r.GetString("PSWT_LIST_V1");
        }
        if (TABLE_NAME.IndexOf("AUTO_LIST_V")>=0) {
            sl1 = r.GetString("AUTO_LIST_V");
            if (is_exdata == 0 ) sl1 = r.GetString("AUTO_LIST_V1");
        }
        if (TABLE_NAME.IndexOf("EA_CHANNELS")>=0) {
            sl1 = r.GetString("EA_CHANNELS");
        }
        if (TABLE_NAME.IndexOf("EA_V_CONSUMER_POINTS")>=0) {
            sl1 = r.GetString("EA_V_CONSUMER_POINTS");
        }
        if (TABLE_NAME.IndexOf("CALC_LIST")>=0) {
            sl1 = r.GetString("CALC_LIST");
        }
        if (TABLE_NAME.IndexOf("DG_LIST")>=0) {
            sl1 = r.GetString("DG_LIST");
            TableTree = "DG_GROUPS" ;
        }
        if (TABLE_NAME.IndexOf("EXDATA_LIST_V")>=0) {
            sl1 = r.GetString("EXDATA_LIST_V");     ;
        }
        if (TABLE_NAME.IndexOf("DA_V_LST")>=0) {
            sl1 = r.GetString("DA_V_LST");
            sl1 = String.Format(sl1,TABLE_NAME);
        }

        if  (TABLE_NAME.Length<=1) {
          return ;
        }

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

        //string[]
        arr = new string[6];

        while (reader.Read())
        {
          for ( int i = 0; i<6; i++)
          {
            arr[i]= GetTypeValue(ref reader, i);
          }

          //  .ID_NODE, DA_CAT.NAME, .ID, .ID_TYPE, DA_V_LST_1.NAME, ID_GINFO
          if (TABLE_NAME.IndexOf("DA_V_LST")>=0) {
             int _id1 = Convert.ToInt32(arr[2]);
             int _idn = Convert.ToInt32(arr[0]);
             string _idt = arr[3].ToString();
             int _ginfo = Convert.ToInt32(arr[5]);
             MEAS1 item1 = new MEAS1(_id1,_idn,_idt,arr[4],arr[1],_ginfo);
             dp.Add(item1);
           } else {
             //SELECT ID, id_obj, id_meas_type, NAME, alias , ID_GINFO
             int _id2 = Convert.ToInt32(arr[0]);
             int _idn = Convert.ToInt32(arr[1]);
             string _idt = arr[2].ToString();
             int _ginfo = Convert.ToInt32(arr[5]);
             MEAS1 item1 = new MEAS1(_id2,_idn, _idt,arr[3],arr[4],_ginfo);
             dp.Add(item1);
          }

        } // while
        reader.Close();

        int CntArchive = 0 ;

        int iFindNo =-1;
        int iRowIndex = 0;
        int prevFindNo = -1;
        foreach (MEAS1 p in dp)
        {
         iFindNo = p.ID;
         if (prevFindNo != iFindNo ) {
           iRowIndex=dataGridView1.Rows.Add();

           dataGridView1.Rows[iRowIndex].HeaderCell.Value = (iRowIndex + 1).ToString();

           if (TABLE_NAME.IndexOf("DA_V_LST")>=0) {
              dataGridView1.Rows[iRowIndex].Cells[0].Value = p.NAME2;
           } else {
            String nd = "" ;
            if (OptionFullName>0) nd=GetFullName(p.ID_NODE.ToString(),TableTree) ;
            dataGridView1.Rows[iRowIndex].Cells[0].Value = nd;
           }
           dataGridView1.Rows[iRowIndex].Cells[1].Value = p.ID.ToString();
           dataGridView1.Rows[iRowIndex].Cells[2].Value = p.NAME1;
           dataGridView1.Rows[iRowIndex].Cells[3].Value = p.ID_TYPE.ToString();
           // dataGridViewP.Rows[iRowIndex].HeaderCell.Value = iRowIndex.ToString();
           for (int jj = 4; jj < dataGridView1.Columns.Count; ++jj)
           {
              dataGridView1.Rows[iRowIndex].Cells[jj].Value = CheckState.Unchecked ;
              if ( dataGridView1.Columns[jj].Name==p.ID_GINFO.ToString() )
              {
                 dataGridView1.Rows[iRowIndex].Cells[jj].Value = CheckState.Checked;
                 CntArchive ++ ;
              }
              break ; // 1 столбец
            }

         } else {
            for (int jj = 4; jj < dataGridView1.Columns.Count; ++jj)
            {
              if ( dataGridView1.Columns[jj].Name==p.ID_GINFO.ToString() )
              {
                dataGridView1.Rows[iRowIndex].Cells[jj].Value = CheckState.Checked;
                CntArchive ++;
              }
              break ; // 1 столбец
            }

          }
         prevFindNo = iFindNo ;
        }

        // отключаем сортировку для каждого столбца
        foreach (DataGridViewColumn col in dataGridView1.Columns)
        {
          //col.SortMode = DataGridViewColumnSortMode.NotSortable;
          col.ReadOnly = true ;
        }

        // Resize the master DataGridView columns to fit the newly loaded data.
        dataGridView1.AutoResizeColumns();

        //dataGridView1.Update();

        PCellValueChanged = 1;

        Application.DoEvents();

        textBox1.Text = "00:00" ;
        textBox2.Text = "00:00" ;

        textBox3.Text = CntArchive.ToString();

        textBox4.Text = dataGridView1.Rows.Count.ToString() ;

        Application.DoEvents();

    }



    public void DelCreate(object sender)
    {

        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;

        cmd0.Connection=this._conn;

        string sl1 = r.GetString("ARH_SYSTBLLST1");
        sl1 = String.Format(sl1,_id_tbl);

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

        String ARC_NAME = "" ;
        if (reader.HasRows) {
          while (reader.Read())
          {
            ARC_NAME = GetTypeValue(ref reader, 0).ToUpper() ;
            break ;
          } // while
        }
        reader.Close();

        if (ARC_NAME=="") { MessageBox.Show("Архивная Таблица не известна!"); return ; }

        Application.DoEvents();


        String TABLE_NAME = "" ;

        double dtmin = 999999999999 ;
        double dtmax = 0 ;

        for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
        {
          statusStrip1.Items[1].Text = (jj+1).ToString() + " of " +  dataGridView1.Rows.Count.ToString();


          dataGridView1.Rows[jj].Cells[5].Value = "" ;
          dataGridView1.Rows[jj].Cells[6].Value = "" ;
          dataGridView1.Rows[jj].Cells[7].Value = "" ;
          dataGridView1.Rows[jj].Cells[8].Value = "" ;

          dataGridView1.Rows[jj].DefaultCellStyle.BackColor = Color.White ;

          DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridView1.Rows[jj].Cells[4] ;

          //dataGridView1.Rows[jj].Cells[4].Value != CheckState.Unchecked ;

          if ( Convert.ToBoolean(chk.Value) == true )
          {
            string id_param=dataGridView1.Rows[jj].Cells[1].Value.ToString();
            sl1 = "SELECT RETFNAME FROM " + ARC_NAME + " WHERE ID_PARAM=" +id_param + " AND ID_GINFO=" +_id ;

            cmd0.CommandText=sl1;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception )
            {
              reader.Close();
              continue ;
            }

            if (reader.HasRows) {
              while (reader.Read())
              {
                TABLE_NAME = GetTypeValue(ref reader, 0).ToUpper() ;
                break ;
              } // while
            }
            reader.Close();

            if (TABLE_NAME=="") continue ;
            Application.DoEvents();

            // min max cnt
            string[] ext1 = new string[3];

            sl1 = r.GetString("TABLEOPTION");
            sl1 = String.Format(sl1,TABLE_NAME );
            cmd0.CommandText=sl1;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception )
            {
              reader.Close();
              continue ;
            }

            if (reader.HasRows) {
              while (reader.Read())
              {
                string[] arr2 = new string[2];
                for (int i = 0;i<2;i++) {
                  arr2[i] = GetTypeValue(ref reader, i).ToUpper() ;
                }
                if (arr2[0]=="MIN") ext1[0]=arr2[1];
                if (arr2[0]=="MAX") ext1[1]=arr2[1];
                if (arr2[0]=="CNT") ext1[2]=arr2[1];
              } // while
            }
            reader.Close();

            //Unix -> DateTime
            double d1 = Convert.ToDouble(ext1[0]) ;
            double d2 = Convert.ToDouble(ext1[1]) ;

            // не учитываем 0 значения
            if (d1>0.1) if (dtmin>=d1) dtmin = d1 ;
            if (d2>0.1) if (dtmax<=d2) dtmax = d2 ;

            DateTime t1 = UnixTimestampToDateTime(d1);
            DateTime t2 = UnixTimestampToDateTime(d2);

            dataGridView1.Rows[jj].Cells[5].Value = t1.ToString();
            dataGridView1.Rows[jj].Cells[6].Value = t2.ToString();
            dataGridView1.Rows[jj].Cells[7].Value = ext1[2];
            dataGridView1.Rows[jj].Cells[8].Value = TABLE_NAME ;

          } //check
        } // for

        DateTime t11 = UnixTimestampToDateTime(dtmin);
        DateTime t22 = UnixTimestampToDateTime(dtmax);

        textBox1.Text = t11.ToString();
        textBox2.Text = t22.ToString();

        // 157766400 = 5 лет
        dtmin = dtmin - 157766400 ;

        // если в таблице 0
        if (dtmin<0.1) {
          dtmin = 0 ;
          t11 = UnixTimestampToDateTime(dtmin);
        }

        dateTimePicker1.Value = t11 ;

    }


    public void DelProcess(object sender)
    {

       DateTime dt0 = dateTimePicker1.Value ;
       double dtd=DateTimeToUnixTimestamp(dt0);

       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();

       cmd0.Connection=this._conn;

       string RETFNAME = "" ;
       int res1 = 0 ;
       int err = 0 ;
       int sm = 0 ;

       for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
       {
          statusStrip1.Items[1].Text = (jj+1).ToString() + " of " +  dataGridView1.Rows.Count.ToString();

          DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridView1.Rows[jj].Cells[4] ;

          if ( Convert.ToBoolean(chk.Value) == true )
          {
            RETFNAME = dataGridView1.Rows[jj].Cells[8].Value.ToString()  ;
            if (RETFNAME=="") { err ++ ; continue ; }

            cmd0.CommandText="DELETE FROM " + RETFNAME + " WHERE TIME1970 < " +dtd.ToString() ;
            try
            {
              res1 = cmd0.ExecuteNonQuery();
            }
            catch (Exception )
            {
              err ++ ;
              continue ;
            }

            Application.DoEvents();
            if (res1!=0)
            {  dataGridView1.Rows[jj].DefaultCellStyle.BackColor = Color.Magenta ;
              sm ++ ;
            }

            //cmd0.Dispose();

          } //check
        } // for

       MessageBox.Show (" Процесс удаления данных до T= " + dt0.ToString()
         + " \n завершен.\n Обработано =" + sm.ToString() + " параметров\n" + "Число ошибок =  " + err.ToString()
        ,"Удаление данных из таблиц", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );

    }

    public void DelJob(object sender)
    {

       DialogResult result1;
       String res2 = test_if_job( _id_tbl ) ;
       if (res2=="0") {

         DateTime dt0 = dateTimePicker1.Value ;

         result1 = MessageBox.Show (" Создать Задачу для удаления данных до T= " + dt0.ToString()
           + " \n ? \n - процесс удаления стартует незамедлительно после создания Job. " ,
           "Удаление данных из таблиц", MessageBoxButtons.YesNo,
           MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 );
         if (result1 == DialogResult.No)
         {
            return ;
         }

         res2 = clear_one_schema_job( _id_tbl , _id , dt0.ToString("dd.MM.yyyy") , 0 ) ;
         if (res2=="0") {
           string  s1 = " Задача создана ! " ;
           MessageBox.Show (s1, "DelJob" , MessageBoxButtons.OK,  MessageBoxIcon.Warning );
         } else {
          MessageBox.Show ("Ошибка создания Задачи. \n", "DelJob" , MessageBoxButtons.OK,  MessageBoxIcon.Warning );
         }

       } else {
         MessageBox.Show ("Job уже создан и работает для данного СПИСКА-ПАРАМЕТРОВ! = " + res2, "DelJob" , MessageBoxButtons.OK,  MessageBoxIcon.Warning );
       }

    }

    void FormDelLoad(object sender, EventArgs e)
    {
       //
       this.Text = this.Text + " : " +  _name + " , " + _gpt_name + ".";

       button11.Enabled=false ;
       button12.Enabled=false ;
       button3.Enabled=false ;
       button1.Enabled=false ;
       button4.Enabled=false ; // list
    }

    void Button2Click(object sender, EventArgs e)
    {
       DelShown(sender);
       button11.Enabled=true ;
       button12.Enabled=false ;
       button3.Enabled=true ;
       button1.Enabled=true ;
    }
    void Button3Click(object sender, EventArgs e)
    {

       button11.Enabled=false ;
       button12.Enabled=false ;
       button2.Enabled=false ;
       button3.Enabled=false ;
       button1.Enabled=false ;

       //DelCreateAsync().GetAwaiter();
       try
       {
         DelCreate( sender ) ;
         // Resize the master DataGridView columns to fit the newly loaded data.
         dataGridView1.AutoResizeColumns();
       }
       catch (Exception )
       {
        ;
       }

       button11.Enabled=true ;
       button12.Enabled=true ;
       button2.Enabled=true ;
       button3.Enabled=true ;
       button1.Enabled=true ;

       button4.Enabled=true ; // list

    }
    void Button12Click(object sender, EventArgs e)
    {
       DateTime dt0 = dateTimePicker1.Value ;
       double dtd=DateTimeToUnixTimestamp(dt0);

       DialogResult result = MessageBox.Show (" Начать процесс удаления данных до T= " + dt0.ToString()
         + "\n ? \n - процесс удаления использует цикл по всем параметрам и команду DDL DELETE" +
         "\n и может занять много времени. После потребуется перестроить Индексы. "  ,
         "Удаление данных из таблиц",
         MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 );
       if (result == DialogResult.No)
       {
          return ;
       }

       button11.Enabled=false ;
       button12.Enabled=false ;
       button2.Enabled=false ;
       button3.Enabled=false ;
       button1.Enabled=false ;
       button4.Enabled=true ; // list

       DelProcess( sender) ;

       button11.Enabled=true ;
       button12.Enabled=true ;
       button2.Enabled=true ;
       button3.Enabled=true ;
       button1.Enabled=true ;
       button4.Enabled=true ; // list

    }
    void Button11Click(object sender, EventArgs e)
    {
       DelJob(sender);
    }
    void Button1Click(object sender, EventArgs e)
    {
      // export
      var  dataGridView = (DataGridView)dataGridView1; ;

      //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Filter = "CSV|*.csv|Text|*.txt";
      saveFileDialog1.FilterIndex = 1;

      saveFileDialog1.Title = "Export to CSV-TXT";   //define the name of openfileDialog
      saveFileDialog1.InitialDirectory = @"Desktop"; //define the initial directory
      saveFileDialog1.FileName="";

      if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
      // получаем выбранный файл
      string filename = saveFileDialog1.FileName;
      // сохраняем текст в файл
      switch(saveFileDialog1.FilterIndex)
      {
         case 2 :
            IDataObject objectSave = Clipboard.GetDataObject();
            dataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridView.SelectAll();
            Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            string pattern = @"^;(.*)$";
            string str = (Clipboard.GetText(TextDataFormat.Text)).Replace(" ", ";");
            str=Regex.Replace(str, pattern, "$1",RegexOptions.Multiline);
            File.WriteAllText(filename, str, Encoding.UTF8);
            if (objectSave != null)
            {
                Clipboard.SetDataObject(objectSave);
            }
            dataGridView.ClearSelection();
         break;
         default :
            string fileCSV = "";
            for (int f = 0; f < dataGridView.ColumnCount; f++)
                fileCSV += (dataGridView.Columns[f].HeaderText + ";");
            fileCSV += "\t\n";
            string st , tst;

            using (StreamWriter wr = new StreamWriter(filename, false, Encoding.UTF8) )  // Encoding.GetEncoding("windows-1251"))
            {

                for (int i = 0; i < dataGridView.RowCount ; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                    {
                      st = "" ;
					  try
                      {
                        st = (dataGridView[j, i].Value).ToString() ;
                      }
                      catch (Exception ex1)
                      {
                        st = "" ;
                      }

                      tst = "" ;
					  try
                      {
					  	tst = (dataGridView[j, i].Value).GetType().ToString() ;
                      }
                      catch (Exception ex2)
                      {
                        tst = "" ;
                      }

                      if ("System.Windows.Forms.CheckState"==tst) {
                        if (OptionSaveFormat>0) {
						    if (st=="Checked") st="1";
                            if (st=="Unchecked") st="0";
			            } else {
				            if (st=="Checked") st="x";
                            if (st=="Unchecked") st="";
				        }
                      }
                      fileCSV += st + ";";
                    }
                    //fileCSV += "\t\n";
                    wr.WriteLine(fileCSV);
                    fileCSV="";
                }

			} // using
			//    StreamWriter wr = new StreamWriter(filename, false, Encoding.UTF8); // Encoding.GetEncoding("windows-1251")
            //    wr.Write(fileCSV);
            //    wr.Close();
          break ;
      }
      String s1="filename="+filename + " -> Save";
      toolStripStatusLabel1.Text = s1;
      //MessageBox.Show("Export to \n " + filename + " \n done");
    }
    void DataGridView1Sorted(object sender, EventArgs e)
    {
        // после сортировки заново перенумеровать
        dataGridView1.RowHeadersWidth = 90;
        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        {
           dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
        }
    }
    void Button4Click(object sender, EventArgs e)
    {
        // вывести лист

        DateTime dt0 = dateTimePicker1.Value ;
        double dtd=DateTimeToUnixTimestamp(dt0);

        FormDelInfo1 fdi1 = new FormDelInfo1();


        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;

        cmd0.Connection=this._conn;

        string sl1 = r.GetString("ARH_SYSTBLLST2");
        sl1 = String.Format(sl1,_id_tbl);

        cmd0.CommandText=sl1;
        try
        {
          reader = cmd0.ExecuteReader();
        }
        catch (Exception ex1)
        {
          MessageBox.Show(ex1.Message);
        }

        String ARC_NAME = "" ;
        String _SCHEME_NAME = "" ;
        if (reader.HasRows) {
          while (reader.Read())
          {
            ARC_NAME = GetTypeValue(ref reader, 2).ToUpper() ;
            _SCHEME_NAME = GetTypeValue(ref reader, 3).ToUpper() ;
            break ;
          } // while
        }
        reader.Close();

        Application.DoEvents();

        string RETFNAME = "" ;
        //int res1 = 0 ;
        int err = 0 ;
        //int sm = 0 ;
        String str = "" ;

        for (int jj = 0; jj < dataGridView1.Rows.Count; ++jj)
        {
           statusStrip1.Items[1].Text = (jj+1).ToString() + " of " +  dataGridView1.Rows.Count.ToString();

           DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridView1.Rows[jj].Cells[4] ;

           if ( Convert.ToBoolean(chk.Value) == true )
           {
             RETFNAME = dataGridView1.Rows[jj].Cells[8].Value.ToString()  ;
             if (RETFNAME=="") { err ++ ; continue ; }

             fdi1.Add("--");

             if (_SCHEME_NAME!="") {
               RETFNAME= _SCHEME_NAME+"."+RETFNAME ;
             }

             str="-- -- -- DELETE FROM " + RETFNAME + " WHERE TIME1970 < " +dtd.ToString() + " ;" ;

             fdi1.Add(str);

             //-- усечение таблицы
             //-- способ 1 - освободить неиспользуемое место - обычно не в конце файла
             str= "alter table "+ RETFNAME + " enable row movement" + " ;" ;
             fdi1.Add(str);

             str= "alter table "+ RETFNAME + " shrink space cascade" + " ;" ; // вместе с индексом
             fdi1.Add(str);

             //-- Cause: Cannot shrink the segment because it is not in auto segment space managed tablespace
             //-- возможно, проблема в индексе, попробуем то же самое без него:
             str= "-- -- alter table "+ RETFNAME  + " shrink space" + " ;" ;
             fdi1.Add(str);

             str= "-- -- alter index "+ RETFNAME + "_I DEALLOCATE UNUSED" + " ;" ;
             fdi1.Add(str);

             Application.DoEvents();

           } //check
        } // for

        fdi1.ShowDialog();

    }

  }

}
