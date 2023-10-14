/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 07.01.2021
 * Time: 9:09
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data.Odbc;

using System.Reflection;
using System.Resources;

using System.Diagnostics;

namespace ArcConfig
{
  /// <summary>
  /// Description of FormArcGinfo.
  /// </summary>
  ///

  public partial class FormArcGinfo : Form
  {

    public string[] VL = new string[16];

    public FormArcGinfo()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
    }

    public FormArcGinfo(OdbcConnection conn, String id, String id_tbl,int SchemaName)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      _conn = conn ;
      id_arcginfo = id ;
      _id_tbl = id_tbl ;
      ARC_NAME = "" ;
      _OptionSchemaName = SchemaName ;
      //
    }

    int ErrInput ;
    public string StrToIntVal (string text)
    {
       ErrInput = -1;
       int number = 0 ;
       String ret = number.ToString();
       text = text.Trim();
       if (int.TryParse(text, out number))
       {
          ret = number.ToString();
          ErrInput = 0 ;
       }
       return (ret);
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
    public String id_arcginfo;
    public int _OptionSchemaName = 0;
    public String _id_tbl ;
    public string OptionSchemaMain = "RSDUADMIN";
    public String ARC_NAME ;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }


    void FormArcGinfoLoad(object sender, EventArgs e)
    {
      textBoxID.Text = id_arcginfo ;
      //List<ArcGinfo> a1 = new List<ArcGinfo>();


      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;


      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }


      checkedListBoxSTATE.Items.Clear();

      // get arc_name table
      ARC_NAME = "LIST_ARC" ;
      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

      string sl2 = r.GetString("ARH_SYSTBLLST1");
      sl2 = String.Format(sl2,_id_tbl);

      cmd0.CommandText=sl2;
      try
      {
          reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        ;
      }

      if (reader.HasRows) {
          while (reader.Read())
          {
            ARC_NAME = GetTypeValue(ref reader, 0).ToUpper() ;
            break ;
          } // while
      }
      reader.Close();

      Application.DoEvents();


      // "select * from ARC_FTR";
      cmd0.CommandText="SELECT  ID, NAME, DEFINE_ALIAS, MASK FROM "+stSchema+"ARC_FTR ORDER BY ID"  ;

      Application.DoEvents();

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
          string[] arr = new string[4];
          for ( int i = 0; i<4; i++)
          {
            arr[i]= GetTypeValue(ref reader, i);
          }
          checkedListBoxSTATE.Items.Add(arr[1]+" ("+arr[3]+")",CheckState.Unchecked);
        } // while
      }
      reader.Close();

      Application.DoEvents();

      // "select * from ARC_GINFO";
      cmd0.CommandText="" +
"SELECT " +
" ID,  " +
" ID_GTOPT, " +
" ID_TYPE, " +
" DEPTH, " +
" DEPTH_LOCAL, " +
" CACHE_SIZE, " +
" CACHE_TIMEOUT, " +
" FLUSH_INTERVAL, " +
" RESTORE_INTERVAL, " +
" STACK_INTERVAL, " +
" WRITE_MINMAX, " +
" RESTORE_TIME, " +
" NAME, " +
" STATE, " +
" DEPTH_PARTITION, " +
" RESTORE_TIME_LOCAL " +
" FROM "+stSchema+"ARC_GINFO " +
" WHERE ID=" + id_arcginfo ;

      Application.DoEvents();

      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.ToString() );
        return ;
      }

      // default value
      String ID_GTOPT="1";
      textBoxID.Text="9999" ;
      textBoxDEPTH.Text="168" ;
      textBoxDEPTH_LOCAL.Text="168" ;
      textBoxCACHE_SIZE.Text="200000" ;
      textBoxCACHE_TIMEOUT.Text="5" ;
      textBoxFLUSH_INTERVAL.Text="86400" ;
      textBoxSTACK_INTERVAL.Text="60" ;
      textBoxRESTORE_INTERVAL.Text="3600" ;
      textBoxRESTORE_TIME.Text="168" ;
      textBoxSTATE.Text="0" ; //!!!!!!!!!!!!!!!!
      checkBoxWRITE_MINMAX.Checked=false;
      textBoxRESTORE_TIME_LOCAL.Text="0" ;
      textBoxName.Text="Archive Name" ;
      textBoxDEPTH_PARTITION.Text="180" ;
      comboBoxID_TYPE.SelectedIndex=0; // -----------
      textBoxN.Text="12" ;

      if (reader.HasRows) {
       while (reader.Read())
       {
        //string[] arr = new string[16];
        for ( int i = 0; i<16; i++)
        {
           VL[i]= GetTypeValue(ref reader, i);
        }

        textBoxID.Text=VL[0] ;
        ID_GTOPT=VL[1];
        textBoxDEPTH.Text=VL[3] ;
        textBoxDEPTH_LOCAL.Text=VL[4] ;
        textBoxCACHE_SIZE.Text=VL[5] ;
        textBoxCACHE_TIMEOUT.Text=VL[6] ;
        textBoxFLUSH_INTERVAL.Text=VL[7] ;
        textBoxRESTORE_INTERVAL.Text=VL[8] ;
        textBoxSTACK_INTERVAL.Text=VL[9] ;
        if (VL[10]=="1") checkBoxWRITE_MINMAX.Checked=true;
        else checkBoxWRITE_MINMAX.Checked=false;
        textBoxSTATE.Text=VL[13] ; //------
        textBoxRESTORE_TIME.Text=VL[11] ;
        textBoxName.Text=VL[12] ;
        //checkedListBoxSTATE
        if (VL[2]=="1") comboBoxID_TYPE.SelectedIndex=0;
        else comboBoxID_TYPE.SelectedIndex=1;
        textBoxDEPTH_PARTITION.Text=VL[14] ;
        textBoxRESTORE_TIME_LOCAL.Text=VL[15] ;

        int p = 12;
        int dp1 = Convert.ToInt32(textBoxDEPTH_PARTITION.Text);
        int dp2 = Convert.ToInt32(textBoxDEPTH.Text);
        if (dp1>0 && dp2>0)
          p=Convert.ToInt32(textBoxDEPTH.Text)*60/Convert.ToInt32(textBoxDEPTH_PARTITION.Text);
        if (p>63)
          textBoxN.ForeColor = Color.Red;
          //textBoxN.BackColor = System.Drawing.Color.Red;
        textBoxN.Text=p.ToString();

        break ;

       } // while
      }
      reader.Close();

      Application.DoEvents();

      int val1 = Convert.ToInt32(textBoxSTATE.Text);
      for (int i = 0; i < checkedListBoxSTATE.Items.Count; i++)
      {
        string curItem = checkedListBoxSTATE.GetItemText(checkedListBoxSTATE.Items[i]);
        int ip1 = curItem.IndexOf('(');
        string text1 = curItem.Substring(ip1+1) ;
        int ip2 = text1.IndexOf(')');
        text1 = text1.Substring(0,ip2) ;
        int val2 = Convert.ToInt32(text1);
        if ((val1 & val2)!=0)  checkedListBoxSTATE.SetItemChecked(i, true);
      }



      DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
      checkBoxColumn.HeaderText = "";
      checkBoxColumn.Width = 30;
      checkBoxColumn.TrueValue = true;
      checkBoxColumn.FalseValue = false;
      checkBoxColumn.Name = "checkTurn";

      dataGridView1.Columns.Insert(0, checkBoxColumn);

      // re-read
      //dataGridView1.Rows.Clear();
      Application.DoEvents();

      dataSet1.Clear();

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();


string  sl1="" +
"SELECT "+
"  sg1.ID,sg1.NAME"+
"  ,sg2.DEFINE_ALIAS as \"DataType\" "+
"  ,sg1.INTERVAL as \"Interval (sec)\" "+
"  ,'0' as \"Archive Type\" "+
" FROM "+stSchema+"SYS_GTOPT sg1, "+stSchema+"SYS_GTYP sg2"+
" WHERE sg1.ID_ATYPE is null and sg2.id=sg1.ID_GTYPE"+
" union "+
"SELECT "+
"  sg1.ID,sg1.NAME"+
"  ,sg2.DEFINE_ALIAS as \"DataType\" "+
"  ,sg1.INTERVAL as \"Interval (sec)\" "+
"  ,sa.NAME as \"Archive Type\" "+
" FROM "+stSchema+"SYS_GTOPT sg1, "+stSchema+"SYS_ATYP sa, "+stSchema+"SYS_GTYP sg2"+
" WHERE sg1.ID_ATYPE=sa.ID and sg2.id=sg1.ID_GTYPE" ;

      sl1 = r.GetString("SYS_GTOPT_LIST_V");

      cmd0.CommandText=sl1;

      // Указываем запрос для выполнения
      adapter.SelectCommand = cmd0;
      // Заполняем объект источника данных
      adapter.Fill(dataSet1);

      // Запрет удаления данных
      dataSet1.Tables[0].DefaultView.AllowDelete = false;
      // Запрет модификации данных
      dataSet1.Tables[0].DefaultView.AllowEdit = false;
      // Запрет добавления данных
      dataSet1.Tables[0].DefaultView.AllowNew = false;

      // (с этого момента она будет отображать его содержимое)
      dataGridView1.DataSource = dataSet1.Tables[0].DefaultView;;
      //dataGridView1.Columns["ID"].ReadOnly = true;
      //dataGridView1.Columns["ID_NODE"].ReadOnly = true;

      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridView1.AutoResizeColumns();

      // Configure the details DataGridView so that its columns automatically
      // adjust their widths when the data changes.
      dataGridView1.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.AllCells;

      for (int ii = 0; ii < dataGridView1.RowCount ; ii++) {
        dataGridView1.Rows[ii].Cells[0].Value = false ;
        dataGridView1.Rows[ii].HeaderCell.Style.BackColor = Color.White ;
        dataGridView1.Rows[ii].DefaultCellStyle.BackColor = Color.White ;
      }

      dataGridView1.EnableHeadersVisualStyles = false;
      dataGridView1.AlternatingRowsDefaultCellStyle.BackColor =Color.LightGray;
      dataGridView1.Columns[0].HeaderCell.Style.BackColor = Color.SpringGreen ;

      //dataGridView1.Columns[0].HeaderCell.ToolTipText = "Подсказка11";

      for (int ii = 0; ii < dataGridView1.RowCount ; ii++) {
        if (dataGridView1.Rows[ii].Cells[1].Value.ToString() == ID_GTOPT) {
          dataGridView1.Rows[ii].Cells[0].Value = true ;
          //dataGridView1.Rows[ii].Selected=true;
          dataGridView1.Rows[ii].HeaderCell.Style.BackColor = Color.LightGreen ;
          dataGridView1.Rows[ii].DefaultCellStyle.BackColor = Color.LightGreen ;
          dataGridView1.FirstDisplayedScrollingRowIndex=ii;

          dataGridView1.CurrentCell = dataGridView1.Rows[ii].Cells[0] ;
          dataGridView1.Rows[ii].Selected=true ;
        }

      }

      Application.DoEvents();

      dataGridView1.Update();

      button3.Focus();
    }

    void TextBoxIDMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxID, "Идентификатор профиля архива в БД");
    }
    void TextBoxNameMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxName, "Наименование профиля архива");
    }
    void ComboBoxID_TYPEMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(comboBoxID_TYPE, "Вид архива");
    }
    void TextBoxDEPTHMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxDEPTH, "Глубина архива в БД архивов, в часах. Значение 0 - архив неограниченной глубины");
    }
    void TextBoxDEPTH_LOCALMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxDEPTH_LOCAL, "Глубина архива в локальной БД, в часах. Значение 0 - архив с неограниченной глубиной");
    }
    void TextBoxCACHE_SIZEMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxCACHE_SIZE, "Объем данных ('строк'), при котором сервер записи архивов отправляет данные на вставку серверу прямого доступа");
    }
    void TextBoxCACHE_TIMEOUTMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxCACHE_TIMEOUT, "Промежуток времени (сек), через который сервер записи архивов отправляет данные на вставку серверу прямого доступа");
    }
    void TextBoxFLUSH_INTERVALMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxFLUSH_INTERVAL, "Период (сек) чистки устаревших данных. Значение 0 - не исполняется");
    }
    void TextBoxRESTORE_INTERVALMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxRESTORE_INTERVAL, "Период (сек) контроля целостности архивных данных. Значение 0 - непрерывное исполнение");
    }
    void TextBoxSTACK_INTERVALMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxSTACK_INTERVAL, "Период (сек) разбора таблицы - стека. Значение 0 - не исполняется");
    }
    void CheckBoxWRITE_MINMAXMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(checkBoxWRITE_MINMAX, "Записывать минимальное и максимальное значение на интервале");
    }
    void TextBoxRESTORE_TIMEMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxRESTORE_TIME, "Период, по истечении которого прекращаются попытки восстановить архив из внешней подсистемы");
    }
    void TextBoxDEPTH_PARTITIONMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxDEPTH_PARTITION, "Глубина хранения среза в разделе (в минутах). По умолчанию - 180 мин = 3 часа");
    }
    void TextBoxRESTORE_TIME_LOCALMouseHover(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(textBoxRESTORE_TIME_LOCAL, "Время, в течение которого возможно восстановление параметра из локального архива");
    }
    void Button4Click(object sender, EventArgs e)
    {
       // default
       //textBoxID.Text="" ;
       textBoxDEPTH.Text="168" ;
       textBoxDEPTH_LOCAL.Text="168" ;
       textBoxCACHE_SIZE.Text="200000" ;
       textBoxCACHE_TIMEOUT.Text="5" ;
       textBoxFLUSH_INTERVAL.Text="86400" ;
       textBoxSTACK_INTERVAL.Text="60" ;
       textBoxRESTORE_INTERVAL.Text="3600" ;
       textBoxRESTORE_TIME.Text="168" ;
       checkBoxWRITE_MINMAX.Checked=false;
       textBoxRESTORE_TIME_LOCAL.Text="0" ;
       //textBoxName.Text="name" ;
       //checkedListBoxSTATE
       textBoxDEPTH_PARTITION.Text="180" ;
       //comboBoxID_GTOPT;
       textBoxSTATE.Text="0" ; //----------------
       comboBoxID_TYPE.SelectedIndex=0; // !!!!!!!
       textBoxN.Text="12" ;
    }
    void CheckedListBoxSTATESelectedIndexChanged(object sender, EventArgs e)
    {
       // Old variant
     /*
       string curItem = checkedListBoxSTATE.SelectedItem.ToString();
       int i = checkedListBoxSTATE.SelectedIndex ;
       int ip1 = curItem.IndexOf('(');
       string text1 = curItem.Substring(ip1+1) ;
       int ip2 = text1.IndexOf(')');
       text1 = text1.Substring(0,ip2) ;

       int val1 = Convert.ToInt32(textBoxSTATE.Text);
       int val2 = Convert.ToInt32(text1);

       bool chk = checkedListBoxSTATE.GetItemChecked(i);
       if (chk==true) val1=val1+val2;
       else val1=val1-val2;
       if (val1<0) val1=0;

       if (text1=="2") {
         if (chk==true) checkBoxWRITE_MINMAX.Checked=true;
         else checkBoxWRITE_MINMAX.Checked=false;
       }

       textBoxSTATE.Text=val1.ToString();
     */
    }
    void CheckBoxWRITE_MINMAXCheckedChanged(object sender, EventArgs e)
    {
      for (int i = 0; i < checkedListBoxSTATE.Items.Count; i++)
      {
        string curItem = checkedListBoxSTATE.GetItemText(checkedListBoxSTATE.Items[i]);
        int ip1 = curItem.IndexOf('(');
        string text1 = curItem.Substring(ip1+1) ;
        int ip2 = text1.IndexOf(')');
        text1 = text1.Substring(0,ip2) ;
        if (text1=="2") {
           checkedListBoxSTATE.SetItemChecked(i, checkBoxWRITE_MINMAX.Checked);

           int val1 = Convert.ToInt32(textBoxSTATE.Text);
           int val2 = Convert.ToInt32(text1);

           if (checkBoxWRITE_MINMAX.Checked==true) val1=val1+val2;
            else val1=val1-val2;
           if (val1<0) val1=0;

           textBoxSTATE.Text=val1.ToString();
           break ;
        }
       }
    }
    void Button3Click(object sender, EventArgs e)
    {
        // cancel
        this.Close();
    }
    void Button2Click(object sender, EventArgs e)
    {
        // ok
        //check

        string stSchema="";
        if (_OptionSchemaName>0) {
          stSchema=OptionSchemaMain + "." ;
        }

        String strS = "" ;
        // UPDATE ARC_GINFO
        // SET street = 'Лизюкова', city = 'Воронеж'
        // WHERE ID=" + id_arcginfo;

        Application.DoEvents();

        ErrInput = 0 ;

        //textBoxID.Text=VL[0] ;

        int flag_gtopt = -1 ;
        int ID_GTOPT=-1;
        for (int ii = 0; ii < dataGridView1.RowCount ; ii++) {
          if (Convert.ToBoolean(dataGridView1.Rows[ii].Cells[0].Value)) {
            ID_GTOPT =Convert.ToInt32( dataGridView1.Rows[ii].Cells[1].Value ) ;
            break ;
          }
        }
        if (ID_GTOPT>0) {
          if (ID_GTOPT.ToString() !=VL[1]) {
            if (strS!="") { strS += " , " ; }
            strS = strS + "ID_GTOPT =" + ID_GTOPT.ToString();
            flag_gtopt = ID_GTOPT ; // флаг смены типа архива в архивной таблице
         }
        }


        String s0=Convert.ToString(comboBoxID_TYPE.SelectedIndex+1) ;
        if (s0!=VL[2] ) {
            if (strS!="") { strS += " , " ; }
            strS = strS + "ID_TYPE =" + s0 ;
        }

        textBoxDEPTH.Text = StrToIntVal(textBoxDEPTH.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxDEPTH.Text!=VL[3]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "DEPTH =" + textBoxDEPTH.Text;
        }

        textBoxDEPTH_LOCAL.Text = StrToIntVal(textBoxDEPTH_LOCAL.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxDEPTH_LOCAL.Text!=VL[4]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "DEPTH_LOCAL =" + textBoxDEPTH_LOCAL.Text;
        }

        textBoxCACHE_SIZE.Text = StrToIntVal(textBoxCACHE_SIZE.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxCACHE_SIZE.Text!=VL[5]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "CACHE_SIZE =" + textBoxCACHE_SIZE.Text;
        }

        textBoxCACHE_TIMEOUT.Text = StrToIntVal(textBoxCACHE_TIMEOUT.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxCACHE_TIMEOUT.Text!=VL[6]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "CACHE_TIMEOUT =" + textBoxCACHE_TIMEOUT.Text;
        }

        textBoxFLUSH_INTERVAL.Text = StrToIntVal(textBoxFLUSH_INTERVAL.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxFLUSH_INTERVAL.Text!=VL[7]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "FLUSH_INTERVAL =" + textBoxFLUSH_INTERVAL.Text;
        }

        textBoxRESTORE_INTERVAL.Text = StrToIntVal(textBoxRESTORE_INTERVAL.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxRESTORE_INTERVAL.Text!=VL[8]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "RESTORE_INTERVAL =" + textBoxRESTORE_INTERVAL.Text;
        }

        textBoxSTACK_INTERVAL.Text = StrToIntVal(textBoxSTACK_INTERVAL.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxSTACK_INTERVAL.Text!=VL[9]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "STACK_INTERVAL =" + textBoxSTACK_INTERVAL.Text;
        }

        int WRITE_MINMAX = 0 ;
        if (checkBoxWRITE_MINMAX.Checked) { WRITE_MINMAX = 1 ; }
        if (WRITE_MINMAX.ToString()!=VL[10] ) {
            if (strS!="") { strS += " , " ; }
            strS = strS + "WRITE_MINMAX =" + WRITE_MINMAX.ToString() ;
        }

        if (textBoxSTATE.Text!=VL[13]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "STATE =" + textBoxSTATE.Text;
        }

        textBoxRESTORE_TIME.Text = StrToIntVal(textBoxRESTORE_TIME.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxRESTORE_TIME.Text!=VL[11]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "RESTORE_TIME =" + textBoxRESTORE_TIME.Text;
        }

        textBoxName.Text=textBoxName.Text.Trim();
        if (textBoxName.Text!=VL[12]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "Name ='" + textBoxName.Text +"'";
        }

        textBoxDEPTH_PARTITION.Text = StrToIntVal(textBoxDEPTH_PARTITION.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxDEPTH_PARTITION.Text!=VL[14]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "DEPTH_PARTITION =" + textBoxDEPTH_PARTITION.Text;
        }

        textBoxRESTORE_TIME_LOCAL.Text = StrToIntVal(textBoxRESTORE_TIME_LOCAL.Text) ;
        if (ErrInput<0) {
          MessageBox.Show("Ошибка ввода данных!\n Сохранение прервано.","Ошибка ввода...",MessageBoxButtons.OK, MessageBoxIcon.Error);
          return ;
        }
        if (textBoxRESTORE_TIME_LOCAL.Text!=VL[15]) {
          if (strS!="") { strS += " , " ; }
          strS = strS + "RESTORE_TIME_LOCAL =" + textBoxRESTORE_TIME_LOCAL.Text;
        }

        Application.DoEvents();

        //save
        if (strS!="") {

          String Add_gtopt = "" ;
          // флаг смены типа архива в архивной таблице
          if (flag_gtopt>0) {
              // flag_gtopt=ID_GTOPT.ToString() - new
              // VL[1] - old
              // Объект для выполнения запросов к базе данных
              OdbcCommand cmd0 = new OdbcCommand();

              cmd0.Connection=this._conn;

              cmd0.CommandText=" SELECT count(*) FROM "+stSchema+ ARC_NAME +
                " WHERE ID_GTOPT=" + VL[1].ToString() + " AND ID_GINFO=" + id_arcginfo ; // VL[0].ToString() ;

              object crec1 = null ;
              try
              {
                crec1=cmd0.ExecuteScalar() ;
              }
              catch (Exception ex1)
              {
                //ID_GTOPT - нет
                flag_gtopt=-1; //MessageBox.Show(ex1.ToString() );
              }
              cmd0.Dispose();
              if (flag_gtopt>0) {
                String s1 = Convert.ToString(crec1);
                Add_gtopt = "\n----------------\nТип архива будет изменен у " + s1 + " параметра(-ов) \n в архивной таблице " + ARC_NAME ;
              }
            }

            DialogResult result = MessageBox.Show ("Изменить профиль архива id=" +id_arcginfo + "   ? \n" + Add_gtopt  ,"Change ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
               // Объект для выполнения запросов к базе данных
               OdbcCommand cmd0 = new OdbcCommand();

               cmd0.Connection=this._conn;

               cmd0.CommandText=" UPDATE "+stSchema+"ARC_GINFO " +
                " SET " + strS +
                " WHERE ID=" + id_arcginfo;

               int crec2 = 0 ;
               try
               {
                 crec2 = cmd0.ExecuteNonQuery();
               }
               catch (Exception ex1)
               {
                 MessageBox.Show(ex1.ToString() );
               }


             if (flag_gtopt>0) {
                  cmd0.CommandText=" UPDATE "+stSchema+ ARC_NAME +
                  " SET ID_GTOPT=" + flag_gtopt.ToString() +
                    " WHERE ID_GTOPT=" + VL[1].ToString() + " AND ID_GINFO=" + id_arcginfo ; // VL[0].ToString() ;

                  int crec3 = 0 ;
                  try
                  {
                     crec3=cmd0.ExecuteNonQuery();
                  }
                  catch (Exception ex1)
                  {
                     MessageBox.Show(ex1.ToString() );
                  }

                }

            }

        }
        this.Close();
        return ;
    }
    void ComboBoxID_TYPESelectedIndexChanged(object sender, EventArgs e)
    {

/*   for (int i = 0; i < checkedListBoxSTATE.Items.Count; i++)
      {
        string curItem = checkedListBoxSTATE.GetItemText(checkedListBoxSTATE.Items[i]);
        int ip1 = curItem.IndexOf('(');
        string text1 = curItem.Substring(ip1+1) ;
        int ip2 = text1.IndexOf(')');
        text1 = text1.Substring(0,ip2) ;
        if (text1=="1") {

           int val1 = Convert.ToInt32(textBoxSTATE.Text);
           int val2 = Convert.ToInt32(text1);

           if (comboBoxID_TYPE.SelectedIndex==1) {
            val1=val1+val2;
            checkedListBoxSTATE.SetItemChecked(i, true);
           }
           else { val1=val1-val2;
            checkedListBoxSTATE.SetItemChecked(i, false);
           }

           textBoxSTATE.Text=val1.ToString();
           break ;
        }
      }   */

    }
    void TextBoxNMouseHover(object sender, EventArgs e)
    {
      toolTip1.SetToolTip(textBoxN, "Если число партиций превышает, необходимо изменять процедуру работу с Партициями.");
    }
    void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        int chkBoxColumnIndex = 0;
        var dataGridView = (DataGridView)sender;
        if (e.ColumnIndex == chkBoxColumnIndex)
        {
            dataGridView1.EndEdit();
            var isChecked = Convert.ToBoolean(dataGridView1[e.ColumnIndex, e.RowIndex].Value);
            if (isChecked)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index != e.RowIndex)
                        row.Cells[chkBoxColumnIndex].Value = !isChecked;
                }
            }
        }

    }

    void CheckedListBoxSTATEItemCheck(object sender, ItemCheckEventArgs e)
    {

       /*if (e.NewValue == CheckState.Checked)
            for (int ix = 0; ix < checkedListBoxSTATE.Items.Count; ++ix)
             if (e.Index != ix) checkedListBoxSTATE.SetItemChecked(ix, false);
       */

       BeginInvoke((MethodInvoker)(() => CheckedItemsChanged(sender, e)));
       /*if (e.NewValue == CheckState.Checked)
         {
            checkedListBoxSTATE.SetItemChecked(e.Index, true);
         }
         else
         {
            checkedListBoxSTATE.SetItemChecked(e.Index, false);
         }
        */
     }

    void CheckedItemsChanged(object sender, ItemCheckEventArgs e)
    {
       int val1 = 0 ;
       for (int jj = 0; jj<checkedListBoxSTATE.Items.Count ; jj++ )
       {
          bool chk = checkedListBoxSTATE.GetItemChecked(jj);

          string curItem = checkedListBoxSTATE.Items[jj].ToString();
          int ip1 = curItem.IndexOf('(');
          string text1 = curItem.Substring(ip1+1) ;
          int ip2 = text1.IndexOf(')');
          text1 = text1.Substring(0,ip2) ;
          int val2 = Convert.ToInt32(text1);

          if (chk==true) {
            val1=val1+val2;
          }

          if (text1=="2") {
            if (chk==true) checkBoxWRITE_MINMAX.Checked=true;
            else checkBoxWRITE_MINMAX.Checked=false;
          }

       }

       textBoxSTATE.Text=val1.ToString();
    }
    void ButtonCalcClick(object sender, EventArgs e)
    {
           System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
           p.WaitForInputIdle();
           //NativeMethods.SetParent(p.MainWindowHandle, this.Handle);
    }
    void Button5Click(object sender, EventArgs e)
    {
         // if > 63
         FormInfo1 fi = new FormInfo1() ;
         fi.ShowDialog();

    }
    void Button1Click(object sender, EventArgs e)
    {
      //Виды характеристик параметров поддерживаемых системой
      FormGTOPT fgt = new FormGTOPT(_conn,_OptionSchemaName) ;
      fgt.ShowDialog();
    }
    void ButtonCassClick(object sender, EventArgs e)
    {
           // if > 63
           FormInfo1 fi = new FormInfo1() ;
           fi.Text = "Отличие в настройке профилей архивов для Cassandra" ;
           fi.richTextBox1.Text="\n" +
"[Рекомендации для БД Cassandra]\n" +
"-------------------------------------------------------------------------------------------------\n" +
"Отличие в настройке профилей архивов от хранения их в Oracle\n\n\n" +
"ARC_GINFO.DEPTH – глубина записи архивов, задается в часах (как и для Oracle). \n" +
"\tМожет быть задана для всех видов архивов (в отличие от Oracle). \n" +
"\tНапример, если глубина записи 5-минутных архивов 5 лет, то можно задать\n\tarc_ginfo.depth = 43848 часов (24x365x5+2x24 на 2 високосных года) и архивы старше 5 лет будут чиститься автоматически. \n" +
"\tЕсли задать arc_ginfo.depth = 0 , то архив будет храниться всегда (до ручной чистки, как в Oracle).\n\n" +
"ARC_GINFO.DEPTH_PARTITION – это размер окна compaction_window_size в Cassandra,\n\tзадается в минутах (в Oracle - глубина партиции).\n" +
"\tДля эффективной работы уплотнения архивов необходимо подбирать размер окна около 1/30 (1/25-1/35, не меньше 1/50) от глубины записи данного типа архивов. \n" +
"\tНапример, для типа архива arc_ginfo.id=28 (Мгновенные ТИ на границе 1 сек), с глубиной 720 часов (30 дней) необходимо задать размер окна 1440 минут (1/30 от глубины записи = 1 день). \n" +
"\tДля неограниченных долговременных архивов с arc_ginfo.depth = 0 рекомендуется задавать arc_ginfo.depth_partition = 132480 минут (92 дня). \n" ;

           fi.ShowDialog();
    }

  }
}
