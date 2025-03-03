﻿/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 26.12.2018
 * Time: 6:24
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks ;

using System.Collections;
using System.ComponentModel;

using System.Data ;
using System.Data.Odbc;
using System.IO;
using System.Text;

using System.Data.Common;
using System.Globalization;
using System.Threading;

using System.Reflection;
using System.Resources;

using System.Text.RegularExpressions;

using PropertyGridTest;

//using FormArcGinfo;


namespace ArcConfig
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MainForm : Form
  {
    private OdbcConnection _conn;


//[DllImport("user32.dll")]
//public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
//private int WM_SETREDRAW = 0x000B;


    // редактируемые данные
    private ARC_SUBSYST_PROFILE _profileData = new ARC_SUBSYST_PROFILE();
    List<MEAS1> dp = new List<MEAS1>();
    public DataGridViewCellMouseEventArgs /*DataGridViewCellEventArgs*/ mouseLocation;
    public int PCellValueChanged = 0;

    public string OptionSchemaMain = "RSDUADMIN";
    public int OptionUpdate = 10 ;
    public int OptionFullDelete = 0;
    public int OptionWriteOnDelete = 0 ;
    public int OptionFullName = 0;
    public int OptionCheckData = 0;
    public int OptionSchemaName = 0;
    public int OptionTableDelete = 0;
    public int OptionTableDisable = 1;
    public int OptionTableConntime = 30;
    public int OptionSaveFormat = 0;
    public string OptionDBlink1 = "CASS";
    public string OptionDBlink1login = "cassandra";
    public string OptionDBlink1pass = "";

    public int DB_VER_FLAG = 1 ;
/*
1) oracle : select * from v$version;
2) mysql :  SELECT VERSION();
3) sqlite : select sqlite_version();
4) Postgres : SELECT version();
*/

    public MainForm()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
    }
    void TabControl1SelectedIndexChanged(object sender, EventArgs e)
    {
       int i =tabControl1.SelectedIndex ;
       //AddLogString("tabControl1="+i);
    }

    const string toolStripButton = "ToolStripButton1";

    void ToolStripButton1Click(object sender, EventArgs e)
    {
       AddLogString("Start");
       this._conn = this._getDBConnection();
       if (this._conn == null) return;
       toolStripButton1.Enabled=false;
       toolStripButton2.Enabled=true;
       toolStripButton3.Enabled=true;
       toolStripButton4.Enabled=true;

       toolStripButton5.Enabled=true;
       toolStripButton6.Enabled=true;

       toolStripButton7.Enabled=true;

       toolStripButton8.Enabled=true;

       toolStripButtonDBE.Enabled=true;

       toolStripButton9.Enabled=true;

       tabControl1.Enabled=true;

       _tree21();
       _version();
       _setDBarc();

       _setDBCalc(); // calc
       //Button3Click(sender,e) ;

    }

   public void AddLogString(string s)
   {
       DateTime d = DateTime.Now;
       string strout = "\n"+ "[" + d.ToLongTimeString() + "] " + s ;
       if(InvokeRequired) {
           richTextBox1.Invoke((Action)delegate { richTextBox1.AppendText(strout); });
           richTextBox1.Invoke((Action)delegate { richTextBox1.Update(); });
       } else {
           richTextBox1.AppendText(strout);
           richTextBox1.Update();
         }
       Application.DoEvents();
   }

   public void AddLogStringNoTime(string s)
   {
       string strout = "\n"+ s ;
       if(InvokeRequired) {
           richTextBox1.Invoke((Action)delegate { richTextBox1.AppendText(strout); });
           richTextBox1.Invoke((Action)delegate { richTextBox1.Update(); });
       } else {
           richTextBox1.AppendText(strout);
           richTextBox1.Update();
         }
       Application.DoEvents();
   }


   public string GetTypeValue(ref OdbcDataReader reader, int i)
   {
     object obj ;
     string ret="";

     // выводим названия столбцов
     string name_column = reader.GetName(i);

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

     string stSchema="";
     if (OptionSchemaName>0) {
       stSchema=OptionSchemaMain + "." ;
     }

     string[] arr = new string[3];
     int fl = 0;
     while (fl==0)
     {
       // ORA 1000 !!!!!!
       cmd0.CommandText="SELECT ID, ID_PARENT, NAME FROM " + stSchema + TableTree + " WHERE ID="+ID ;
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


    private OdbcConnection _getDBConnection()
    {
       ConnectDB connectDb = new ConnectDB();
       int num = (int) connectDb.ShowDialog(); // (IWin32Window) this
       if (connectDb.Conn != null)
       {
         AddLogString ( "Подключен к " + connectDb.Conn.ConnectionString);
       }
       else
       {
         AddLogString( " - Не подключен");
       }
       return connectDb.Conn;
    }



/*
 установка ролей
*/
    void _setDBright(String sObj)
    {
       // Объект для выполнения запросов к базе данных
       OdbcCommand command = new OdbcCommand();

       if (this._conn.State==System.Data.ConnectionState.Closed) {
         AddLogString("Невозможно установить соединение с БД");
         AddLogString(" программа прервана..");
         return ;
       }

       command.Connection = this._conn; // уже созданное и открытое соединение
       command.CommandType = System.Data.CommandType.StoredProcedure;
       command.Parameters.Clear();
       command.CommandText="SET ROLE BASE_EXT_CONNECT_OIK , ARC_STAND_ADMIN, ARC_STAND_ADJ, ARC_STAND_READ, BASE_STAND_ADJ";

       try
       {
          command.ExecuteNonQuery();
          AddLogString("Установка роли = Ok ");
       }
       catch (Exception ex7)
       {
          AddLogString("Ошибка установки Ролей ="+ex7.Message);
          return ;
       }

    }


    public TreeNode GetNode(object tag, TreeNode rootNode)
    {
        foreach (TreeNode node in rootNode.Nodes)
        {
            if (node.Tag.Equals(tag)) return node;

            //recursion
            var next = GetNode(tag, node);
            if (next != null) return next;
        }
        return null;
    }

    public TreeNode FromID(string itemId, TreeNode rootNode){
       foreach(TreeNode node in rootNode.Nodes){
         if(node.Tag.Equals(itemId)) return node;
         TreeNode next = FromID(itemId, node);
         if(next != null) return next;
       }
       return null;
    }


    int _checkCol( string nameCol , string nameTbl )
    {
       int is_exists = 1 ;
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd = new OdbcCommand();
       OdbcDataReader reader1 = null ;

       cmd.Connection = this._conn; // уже созданное и открытое соединение

       string stSchema="";
       if (OptionSchemaName>0) {
         stSchema=OptionSchemaMain + "." ;
       }

       // определяем поле
       //ERROR [42S22] [Oracle][ODBC][Ora]ORA-00904: недопустимый идентификатор

       cmd.CommandText="SELECT " + nameCol + " FROM "+stSchema+nameTbl ;
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


/*
 * проверка на существование ARC_DB_SCHEMA.ID_STORAGE_TYPE
 * получение типа бд хранилища
 */
    int _checkStorage( string numTbl , out string SHEMA_NAME, out string DEFINE_ALIAS )
    {
       SHEMA_NAME="";
       DEFINE_ALIAS="ORACLE";
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd = new OdbcCommand();
       OdbcDataReader reader5 = null ;

       cmd.Connection = this._conn; // уже созданное и открытое соединение

       if (0==_checkCol( "ID_STORAGE_TYPE" , "ARC_DB_SCHEMA" )) return (-1);

       ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

       string sl1 = r.GetString("ARH_SYSTBLLST3");
       sl1 = String.Format(sl1,numTbl);

       cmd.CommandText=sl1 ;
       try
       {
          reader5 = cmd.ExecuteReader();
       }
       catch (Exception ex1)
       {
          cmd.Dispose();
          return (-2);
       }

       //string SHEMA_NAME = "" ;
       //string DEFINE_ALIAS = "" ;
       if (reader5.HasRows) {
         while (reader5.Read())
         {
           DEFINE_ALIAS = GetTypeValue(ref reader5, 2).ToUpper() ;
           SHEMA_NAME = GetTypeValue(ref reader5, 1).ToUpper() ;
           break ;
         } // while
       }

       if (!reader5.IsClosed ) reader5.Close();
       reader5.Dispose();
       cmd.Dispose();
       return (0) ;
    }



/*
 построение дерева Технологических систем и отображение в дереве
*/
    void _setDBarc()
    {
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd = new OdbcCommand();
       OdbcDataReader reader1 = null , reader2 = null, reader3 = null  ;

       cmd.Connection = this._conn; // уже созданное и открытое соединение


       string stSchema="";
       if (OptionSchemaName>0) {
         stSchema=OptionSchemaMain + "." ;
       }


       //устанавливаем редактируемый объект
       _profileData.ID="";
       _profileData.ID_TBLLST="";
       _profileData.ID_GINFO="";
       _profileData.IS_WRITEON=false ;
       _profileData.STACK_NAME="NONE";
       _profileData.LAST_UPDATE="";
       _profileData.IS_VIEWABLE=false ;

       propertyGridA.SelectedObject=_profileData;
       propertyGridA.Update();

       buttonSave.Enabled=false ;
       buttonService.Enabled = false ;

       treeViewA.BeginUpdate(); //добавить
       treeViewA.Nodes.Clear();

       // получаем root- id
       cmd.CommandText= "" +
"SELECT DISTINCT id_parent FROM  "+stSchema+"sys_tree21 WHERE id_lsttbl IN " +
"(SELECT id FROM "+stSchema+"sys_tbllst WHERE id_type IN " +
"(SELECT id FROM "+stSchema+"sys_otyp WHERE define_alias like 'LST') " +
"AND id_node IN  " +
"(SELECT id FROM "+stSchema+"sys_db_part WHERE id_parent IN " +
"(SELECT id FROM "+stSchema+"sys_db_part WHERE define_alias like 'MODEL_SUBSYST' OR define_alias like 'DA_SUBSYST' )))";
       try
       {
          reader1 = cmd.ExecuteReader();
       }
       catch (Exception ex1)
       {
          AddLogString(" _setDBarc break = " + cmd.CommandText + " " + ex1.Message);
          return ;
       }


       List<string> id_parents = new List<string> ();
       //treeViewA.Nodes.Add(new TreeNode("Разделы"));

       if (reader1.HasRows)
       {
        while (reader1.Read())
        {
          string arrs = GetTypeValue(ref reader1, 0);
          id_parents.Add(arrs);
        }
       }
       reader1.Close();

       int iS ;
       string sS = "" , si ;
       for (si = "" , iS = 0; iS < id_parents.Count;si=" , ", iS++)
       {
          sS+=(si + id_parents[iS]);
       }
       // AddLogString(" " + cmd.CommandText + " " + sS );


       cmd.CommandText="SELECT id, COALESCE(id_parent,0,0), name, COALESCE(id_lsttbl,0,0) " +
       " FROM  "+stSchema+"sys_tree21 " +
       " WHERE id IN ( " + sS + "  ) " ;

       try
       {
          reader2 = cmd.ExecuteReader();
       }
       catch (Exception ex1)
       {
          AddLogString(" _setDBarc break = " + cmd.CommandText + " " + ex1.Message);
          return ;
       }

       //AddLogString(cmd.CommandText);

       Font font = new Font(treeViewA.Font, FontStyle.Bold);

       string[] arr = new string[4];

       if (reader2.HasRows)
       {
         while (reader2.Read())
         {
           for ( int i = 0; i<4; i++) {
             arr[i]= GetTypeValue(ref reader2, i);
           }

           if (arr[3]=="") { arr[3]="0" ; }

           if (arr[3]=="0") {
             TreeNode rootNode = new TreeNode();
             rootNode.Name = arr[0]; // arr[0];
             rootNode.Text = arr[2];
             rootNode.Tag = "0";
             //rootNode.NodeFont = font ;
             AddLogString( " > " + rootNode.Name + " " + rootNode.Text);
             treeViewA.Nodes.Add(rootNode);
           } else {
             string s5= arr[1];
             TreeNode[] tNodeCurrent = new TreeNode[15];
             tNodeCurrent =treeViewA.Nodes.Find(s5, true);
             foreach (TreeNode ndl in tNodeCurrent)
             {
                if (ndl.Name == s5) {
                    TreeNode Nd = new TreeNode();
                    Nd.Name = arr[0]; // --arr[0]; arr[3]
                    Nd.Text = arr[2] + " (" + arr[3] + ")";
                    Nd.Tag = "0"; // -- arr[3]
                    Nd.ForeColor=Color.Red ;
                    ndl.Nodes.Add(Nd);
                    AddLogString(" >> " +Nd.Name + " " + Nd.Text);
                }
             } // foreach
           } // if

         } // while

       }
       reader2.Close();

       cmd.CommandText="SELECT id, COALESCE(id_parent,0,0), name, COALESCE(id_lsttbl,0,0) " +
       " FROM "+stSchema+"sys_tree21 " +
       " WHERE id_parent IN (" + sS + " ) " ;

       try
       {
          reader3 = cmd.ExecuteReader();
       }
       catch (Exception ex1)
       {
          AddLogString(" _setDBarc break = " + cmd.CommandText + " " + ex1.Message);
          return ;
       }

       //AddLogString( cmd.CommandText);

       if (reader3.HasRows)
       {
         while (reader3.Read())
         {
           for ( int i = 0; i<4; i++) {
             arr[i]= GetTypeValue(ref reader3, i);
           }

           if (arr[3]=="0") {
             TreeNode rootNode = new TreeNode();
             rootNode.Name = arr[0]; // arr[0];
             rootNode.Text = arr[2];
             rootNode.Tag = arr[3];
             AddLogString(" >- " + rootNode.Name + " " + rootNode.Text);
             treeViewA.Nodes.Add(rootNode);
           } else {
             string s5= arr[1];
             TreeNode[] tNodeCurrent = new TreeNode[15];
             tNodeCurrent =treeViewA.Nodes.Find(s5, true);
             foreach (TreeNode ndl in tNodeCurrent)
             {
                if (ndl.Name == s5) {
                    TreeNode Nd = new TreeNode();
                    Nd.Name = arr[0]; // arr[0];
                    Nd.Text = arr[2] + " [" + arr[3] + "]";
                    Nd.Tag = arr[3];

                    string DEFINE_ALIAS = "" ;
                    string SHEMA_NAME = "" ;
                    int res=_checkStorage( arr[3] , out SHEMA_NAME, out DEFINE_ALIAS ) ;
                    if (res>=0) {
                       Nd.ToolTipText = SHEMA_NAME ;
                       if ( DEFINE_ALIAS.IndexOf("ORACLE")<0 ) Nd.ForeColor=Color.Blue ;
                    }

                    ndl.Nodes.Add(Nd);
                    AddLogString(" >>-- " + Nd.Name + " " + Nd.Text);
                }
             } // foreach
           } // if

         } // while

       }
       reader3.Close();
       treeViewA.EndUpdate(); //добавить

       treeViewA.ExpandAll();

    }


/*
 отображение таблицы\вьюшки в dataGridView1
*/
    void _getDBv1(String sObj)
    {
       // Объект для выполнения запросов к базе данных
       OdbcCommand command = new OdbcCommand();
       // Объект для связи между базой данных и источником данных
       OdbcDataAdapter adapter = new OdbcDataAdapter();

       if (this._conn.State==System.Data.ConnectionState.Closed) {
         AddLogString("Невозможно установить соединение с БД");
         //this._conn.Close();
         return ;
       }

       // Открываем соединение
       //this._conn.Open();

       // Устанавливаем связь между объектами связи и команд
       command.Connection = this._conn;
       // Запрос по умолчанию
       command.CommandText = "SELECT * FROM " + sObj ;

       dataSet1.Clear();
       dataGridView1.DataSource = null;

       //удалим все строки из dataGridView1
       while (0 != dataGridView1.Columns.Count)
               dataGridView1.Columns.RemoveAt(0);

       dataSet1.Clear();
       dataGridView1.DataSource = null;
       dataSet1.Tables.Clear();

       // Указываем запрос для выполнения
       adapter.SelectCommand = command;
       // Заполняем объект источника данных

       //try
       //{
          adapter.Fill(dataSet1,sObj);
       //}
       //catch (Exception ex1)
       //{
       //   return ;
       //}

       // Запрет удаления данных
       dataSet1.Tables[0].DefaultView.AllowDelete = false;
       // Запрет модификации данных
       dataSet1.Tables[0].DefaultView.AllowEdit = false;
       // Запрет добавления данных
       dataSet1.Tables[0].DefaultView.AllowNew = false;

       // (с этого момента она будет отображать его содержимое)
       dataGridView1.DataSource = dataSet1.Tables[0];

       dataGridView1.Update();


       toolStripStatusLabel2.Text = sObj;

       return ;
    }

    void ToolStripButton2Click(object sender, EventArgs e)
    {
       // save to xls
       ButtonExportMClick(sender,e);
    }
    void RadioButton2CheckedChanged(object sender, EventArgs e)
    {
       try
       {
          if (radioButton2.Checked == true) _getDBv1("arc_stat_current_v");
       }
       catch (Exception ex1)
       {
          AddLogString("Error ="+ex1.Message);
       }
    }
    void RadioButton1CheckedChanged(object sender, EventArgs e)
    {
       try
       {
          if (radioButton1.Checked == true) _getDBv1("arc_stat");
       }
       catch (Exception ex1)
       {
          AddLogString("Error ="+ex1.Message);
       }
    }
    void RadioButton3CheckedChanged(object sender, EventArgs e)
    {
       try
       {
          if (radioButton3.Checked == true) _getDBv1("arc_stat_avg_v");
       }
       catch (Exception ex1)
       {
          AddLogString("Error ="+ex1.Message);
       }
    }
    void RadioButton4CheckedChanged(object sender, EventArgs e)
    {
       try
       {
          if (radioButton4.Checked == true) _getDBv1("arc_retroviewable_v");
       }
       catch (Exception ex1)
       {
          AddLogString("Error ="+ex1.Message);
       }
    }
    void MainFormLoad(object sender, EventArgs e)
    {

       this.Text = this.Text + "  :  " + Application.ProductVersion ;

       timer1.Interval = 1000;
       timer1.Enabled = true ;

       tabControl1.Enabled=false;
       tabControl1.SelectedIndex=0;

       dp.Capacity=250000;
       typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty).SetValue(dataGridViewP, true, null);

       DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
       checkBoxColumn.HeaderText = "";
       checkBoxColumn.Width = 30;
       checkBoxColumn.TrueValue = true;
       checkBoxColumn.FalseValue = false;
       checkBoxColumn.Name = "checkTurnonoff";
       dataGridViewA.Columns.Insert(0, checkBoxColumn);

       //устанавливаем редактируемый объект
       propertyGridA.SelectedObject = _profileData;
       buttonSave.Enabled=false ;
       buttonService.Enabled = false ;

       toolStripButton2.Enabled=false;
       toolStripButton3.Enabled=false;
       toolStripButton4.Enabled=false;

       toolStripButton5.Enabled=false;
       toolStripButton6.Enabled=false;

       toolStripButton7.Enabled=false;

       toolStripButton8.Enabled=false;

       toolStripButtonDBE.Enabled=false;

       toolStripButton9.Enabled=false; // 30

       _setDBS();

       Action tmea = new Action( _Meas30 ) ;
       Task tskmea = new Task(tmea);
       tskmea.Start();

    }


    void TreeViewAAfterSelecttabControl1(object sender)
    {
      /*
      построение
       либо списка параметров (с архивами)
       либо список профилей архивов с активными на данном Тех сервере
      */

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcCommand cmd1 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;
      cmd1.Connection=this._conn;

      //Получить Имя выделенного элемента
      string id_parent = treeViewA.SelectedNode.Name;
      string _id_tbl = Convert.ToString ( treeViewA.SelectedNode.Tag ) ;
      //AddLogString(" id_parent=" + id_parent + "  _id_tbl=" + _id_tbl);
      if (_id_tbl=="0" || _id_tbl=="") return ;

      // re-read
      //dataGridViewA.Rows.Clear();
      Application.DoEvents();

      string stSchema="";
      if (OptionSchemaName>0) {
          stSchema=OptionSchemaMain + "." ;
      }

      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        PCellValueChanged = 0;

        try {
          dataGridViewP.DataSource = null ;
          dataGridViewP.Rows.Clear();
          Application.DoEvents();
          dataGridViewP.Columns.Clear();
        }
        catch (Exception ex1)
        {
          AddLogString("TreeView Error ="+ex1.Message);
        }
        // dataGridViewP.DataSource = new object();
        Application.DoEvents();

        var column0 = new DataGridViewColumn();
        column0.HeaderText = "Группа";
        column0.Name = "Group";
        //column0.ReadOnly = true;
        column0.CellTemplate = new DataGridViewTextBoxCell();

        var column1 = new DataGridViewColumn();
        column1.HeaderText = "id"; //текст в шапке
        //column1.Width = 100; //ширина колонки
        column1.ReadOnly = true; //значение в этой колонке нельзя править
        column1.Name = "id"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
        //column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
        column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

        var column2 = new DataGridViewColumn();
        column2.HeaderText = "Наименование";
        column2.Name = "Name";
        column2.CellTemplate = new DataGridViewTextBoxCell();

        var column3 = new DataGridViewColumn();
        column3.HeaderText = "Тип";
        column3.Name = "type";
        column3.CellTemplate = new DataGridViewTextBoxCell();

        dataGridViewP.Columns.Add(column0);
        dataGridViewP.Columns.Add(column1);
        dataGridViewP.Columns.Add(column2);
        dataGridViewP.Columns.Add(column3);

        dataGridViewP.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки

     // создаем колонки архивов

        List<ARCSUM1> sum = new List<ARCSUM1>();

        string sl1= r.GetString("ARC_SUBSYST_PROFILE2");
        sl1 = String.Format(sl1,_id_tbl);

        cmd1.CommandText=sl1;
        try
        {
          reader = cmd1.ExecuteReader();
        }
        catch (Exception ex1)
        {
          AddLogString("TreeView Error = " + cmd1.CommandText + " " + ex1.Message);
          return ;
        }

        if (reader.HasRows==false) {
          reader.Close();
          return;
        }

        string[] arr = new string[5];

        while (reader.Read())
        {
          for ( int i = 0; i<5; i++)
          {
             arr[i]= GetTypeValue(ref reader, i);
          }

          Application.DoEvents();

         //ARC_SUBSYST_PROFILE.ID, ARC_SUBSYST_PROFILE.ID_GINFO, ARC_SUBSYST_PROFILE.IS_WRITEON,
         //    ARC_GINFO.NAME, SYS_GTOPT.NAME

         /*var column4 = new DataGridViewColumn();
           column4.HeaderText = arr[3] + ", " + arr[4];
           column4.Name = arr[1];
           column4.CellTemplate = new DataGridViewCheckBoxCell();   */

          DataGridViewCheckBoxColumn column4 = new DataGridViewCheckBoxColumn();
          column4.HeaderText = "Profile [" +arr[0] + "] , " + arr[3] + ", " + arr[4] + " (" + arr[1] + ")" ;
          column4.TrueValue = true;
          column4.FalseValue = false;
          column4.Name = arr[1];
          DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
          // auto profile select  another color
          if (arr[2]!="0") {
             cellStyle.BackColor = Color.LightGray;
             //cellStyle.SelectionBackColor = Color.Red;
          }
          cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          column4.DefaultCellStyle = cellStyle;

          dataGridViewP.Columns.Add(column4);

          // обнуляем сумму для каждого архива
          ARCSUM1 item2 = new ARCSUM1(arr[3] + ", " + arr[4],arr[1],0);
          sum.Add(item2);

        } // while
        reader.Close();

        Application.DoEvents();

        string ARC_NAME = "" ;
        string TABLE_NAME = "" ;
        string SHEMA_NAME = "" ;

        // get table списка параметров для данного раздела
        sl1 = r.GetString("ARH_SYSTBLLST2");
        sl1 = String.Format(sl1,_id_tbl);
        cmd0.CommandText=sl1;
        try
        {
           reader = cmd0.ExecuteReader();
        }
        catch (Exception ex1)
        {
          AddLogString("TreeView Error ..прервано.. = " + cmd0.CommandText + " " + ex1.Message);
          return ;
        }
        if (reader.HasRows) {
           while (reader.Read())
           {
               TABLE_NAME = GetTypeValue(ref reader, 1).ToUpper() ;
               ARC_NAME = GetTypeValue(ref reader, 2).ToUpper() ;
               SHEMA_NAME = GetTypeValue(ref reader, 3).ToUpper() ;
               break ;
            } // while
        }
        reader.Close();

        AddLogString("TreeView TABLE_NAME="+ TABLE_NAME+ " ARC_NAME=" + ARC_NAME + " SHEMA_NAME=" + SHEMA_NAME);

        Application.DoEvents();

        if  (SHEMA_NAME.Length<=1) {
          DialogResult result = MessageBox.Show ("Не задана связь раздела с системой архива!\n\n Установить ? ",
                                                 ">>Построение списка ...",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
          if (result == DialogResult.Yes)
          {
            // переход на первую вкладку
            List<String> a = new List<String>();
            List<String> b = new List<String>();
            List<String> bi = new List<String>();

            a.Insert(0,treeViewA.SelectedNode.Text);

            /*
             * проверка на существование ARC_DB_SCHEMA.ID_STORAGE_TYPE
             * получение типа бд хранилища
             */

             if (0==_checkCol( "ID_STORAGE_TYPE" , "ARC_DB_SCHEMA" ))
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
              AddLogString("TreeView Error ..прервано.. = " + cmd0.CommandText + " " + ex1.Message);
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
                  b.Insert(iSa,vS1  + " [" + vS2 + "] - " + vS3);
                  iSa++;
              } // while
            }
            reader.Close();

            FormArc_db_schema_lite arcdbfrm = new FormArc_db_schema_lite(a,0,b,0);
            if (arcdbfrm.ShowDialog()== DialogResult.OK) {
              int rescombo = arcdbfrm._indBoxDB ;
              AddLogString("Selected scheme = " + rescombo.ToString() );

              sl1="INSERT INTO  "+stSchema+"ARC_SERVICES_INFO (ID_LSTTBL,ID_DB_SCHEMA) VALUES " +
                  " ( " + _id_tbl + " , "+ bi[rescombo] + " ) ;";
              int crec1 = 0 ;
              cmd0.CommandText=sl1;
              try
              {
                crec1=cmd0.ExecuteNonQuery();
              }
              catch (Exception ex1)
              {
                 AddLogString(" Insert -> Ошибка. " + cmd0.CommandText + " , " + ex1.Message);
              }
            }
            tabControlAP.SelectedIndex = 0 ;
            return ;
          }
        }

        if  (TABLE_NAME.Length<=1) {
          MessageBox.Show ("Не задана Таблица-Список !",
                           ">>Построение списка ...",
                           MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
          //tabControlAP.SelectedIndex = 0 ;
          return ;
        }

        // построение

        dp.Clear();

        sl1="SELECT 0,0,0,0,0,0,0 FROM DUAL" ;
        string TableTree = "OBJ_TREE" ;

        // определяем поле
        //ERROR [42S22] [Oracle][ODBC][Ora]ORA-00904: "IS_EXDATA": недопустимый идентификатор
        int is_exdata = _checkCol( "is_exdata" , "meas_list" ) ;

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

        Application.DoEvents();

        cmd1.CommandText=sl1;
        try
        {
          reader = cmd1.ExecuteReader();
        }
        catch (Exception ex1)
        {
          AddLogString("TreeView Error ..прервано.. = " + cmd1.CommandText + " " + ex1.Message);
          return ;
        }

        if (reader.HasRows==false) {
          reader.Close();
          return;
        }

        toolStripStatusLabel2.Text = "Buiding lists .. stage one";

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
             int _id = Convert.ToInt32(arr[2]);
             int _idn = Convert.ToInt32(arr[0]);
             string _idt = arr[3].ToString();
             int _ginfo = Convert.ToInt32(arr[5]);
             MEAS1 item1 = new MEAS1(_id,_idn,_idt,arr[4],arr[1],_ginfo);
             dp.Add(item1);
           } else {
             //SELECT ID, id_obj, id_meas_type, NAME, alias , ID_GINFO
             int _id = Convert.ToInt32(arr[0]);
             int _idn = Convert.ToInt32(arr[1]);
             string _idt = arr[2].ToString();
             int _ginfo = Convert.ToInt32(arr[5]);
             MEAS1 item1 = new MEAS1(_id,_idn, _idt,arr[3],arr[4],_ginfo);
             dp.Add(item1);
          }

        } // while
        reader.Close();

        AddLogString(" Capacity=" + dp.Capacity.ToString() + "  Cnt =" + dp.Count.ToString() );

        //SendMessage(dataGridViewP.Handle, WM_SETREDRAW, false, 0);

        toolStripStatusLabel2.Text = "Buiding lists .. stage two";

        int iFindNo =-1;
        int iRowIndex = 0;
        int prevFindNo = -1;
        int dpCnt = dp.Count ;
        foreach (MEAS1 p in dp)
        {

         toolStripStatusLabel2.Text = iRowIndex.ToString() + " of " + dpCnt.ToString() ;

         iFindNo = p.ID;
         if (prevFindNo != iFindNo ) {
           iRowIndex=dataGridViewP.Rows.Add();

           // нумерация
           dataGridViewP.Rows[iRowIndex].HeaderCell.Value = (iRowIndex + 1).ToString();

           if (TABLE_NAME.IndexOf("DA_V_LST")>=0) {
              dataGridViewP.Rows[iRowIndex].Cells[0].Value = p.NAME2;
           } else {
              String nd = "" ;
              if (OptionFullName>0) nd=GetFullName(p.ID_NODE.ToString(),TableTree) ;
              dataGridViewP.Rows[iRowIndex].Cells[0].Value = nd ;
           }
           dataGridViewP.Rows[iRowIndex].Cells[1].Value = p.ID.ToString();
           dataGridViewP.Rows[iRowIndex].Cells[2].Value = p.NAME1;
           dataGridViewP.Rows[iRowIndex].Cells[3].Value = p.ID_TYPE.ToString();
           // dataGridViewP.Rows[iRowIndex].HeaderCell.Value = iRowIndex.ToString();
           for (int jj = 4; jj < dataGridViewP.Columns.Count; ++jj)
           {
              dataGridViewP.Rows[iRowIndex].Cells[jj].Value = CheckState.Unchecked ;
              if ( dataGridViewP.Columns[jj].Name==p.ID_GINFO.ToString() )
              {
                dataGridViewP.Rows[iRowIndex].Cells[jj].Value = CheckState.Checked;
                foreach (ARCSUM1 vq in sum)
                {
                   if (vq.ID_GINFO==p.ID_GINFO.ToString() ) vq.SUM+=1;
                }

                // проверка существования данных
                if (OptionCheckData>0 && ARC_NAME.Length>0) {

                   int err_cnt = 0 ;
                   string RETFNAME = "dual"  ;
                   sl1 = "SELECT RETFNAME FROM " + stSchema+ARC_NAME + " WHERE ID_PARAM=" + p.ID.ToString() + " AND ID_GINFO=" + p.ID_GINFO.ToString() + " ;" ;
                   cmd0.CommandText=sl1;
                   try
                   {
                     reader = cmd0.ExecuteReader();
                   }
                   catch (Exception )
                   {
                     err_cnt++;
                   }
                   if (!reader.IsClosed )
                    if (reader.HasRows) {
                     while (reader.Read())
                     {
                       RETFNAME = GetTypeValue(ref reader, 0).ToUpper() ;
                       break ;
                     } // while
                     reader.Close();
                    }

                   string RETFNAME_CNT = "0" ;

                   sl1 = "SELECT count(*) FROM " + RETFNAME + " ;"  ;
                   cmd0.CommandText=sl1;
                   try
                   {
                     reader = cmd0.ExecuteReader();
                   }
                   catch (Exception )
                   {
                     err_cnt++;
                     RETFNAME_CNT="none" ;
                   }
                   if (!reader.IsClosed )
                    if (reader.HasRows) {
                     while (reader.Read())
                     {
                       RETFNAME_CNT = GetTypeValue(ref reader, 0).ToUpper() ;
                       break ;
                     } // while
                    reader.Close();
                   }

                   DataGridViewCellStyle cellStyle2 = new DataGridViewCellStyle();
                   cellStyle2 = dataGridViewP.Rows[iRowIndex].Cells[jj].Style ;
                   if (RETFNAME_CNT=="none")
                     cellStyle2.BackColor  = Color.Red ;
                   if (RETFNAME_CNT=="0")
                     cellStyle2.BackColor  = Color.GreenYellow ;
                   dataGridViewP.Rows[iRowIndex].Cells[jj].Style=cellStyle2;

                }


              }
            }

         } else {
            for (int jj = 4; jj < dataGridViewP.Columns.Count; ++jj)
            {
              if ( dataGridViewP.Columns[jj].Name==p.ID_GINFO.ToString() )
              {
                dataGridViewP.Rows[iRowIndex].Cells[jj].Value = CheckState.Checked;
                foreach (ARCSUM1 vq in sum)
                {
                  if (vq.ID_GINFO==p.ID_GINFO.ToString() ) vq.SUM+=1;
                }

                // проверка существования данных
                if (OptionCheckData>0 && ARC_NAME.Length>0) {

                   int err_cnt = 0 ;
                   string RETFNAME = "dual"  ;
                   sl1 = "SELECT RETFNAME FROM " + ARC_NAME + " WHERE ID_PARAM=" + p.ID.ToString() + " AND ID_GINFO=" + p.ID_GINFO.ToString() + " ;" ;
                   cmd0.CommandText=sl1;
                   try
                   {
                     reader = cmd0.ExecuteReader();
                   }
                   catch (Exception )
                   {
                     err_cnt++;
                   }
                   if (!reader.IsClosed )
                    if (reader.HasRows) {
                     while (reader.Read())
                     {
                       RETFNAME = GetTypeValue(ref reader, 0).ToUpper() ;
                       break ;
                     } // while
                     reader.Close();
                    }

                   string RETFNAME_CNT = "0" ;

                   sl1 = "SELECT count(*) FROM " + RETFNAME + " ;"  ;
                   cmd0.CommandText=sl1;
                   try
                   {
                     reader = cmd0.ExecuteReader();
                   }
                   catch (Exception )
                   {
                     err_cnt++;
                     RETFNAME_CNT="none" ;
                   }
                   if (!reader.IsClosed )
                    if (reader.HasRows) {
                     while (reader.Read())
                     {
                       RETFNAME_CNT = GetTypeValue(ref reader, 0).ToUpper() ;
                       break ;
                     } // while
                    reader.Close();
                   }

                   DataGridViewCellStyle cellStyle2 = new DataGridViewCellStyle();
                   cellStyle2 = dataGridViewP.Rows[iRowIndex].Cells[jj].Style ;
                   if (RETFNAME_CNT=="none")
                     cellStyle2.BackColor  = Color.Red ;
                   if (RETFNAME_CNT=="0")
                     cellStyle2.BackColor  = Color.GreenYellow ;
                   dataGridViewP.Rows[iRowIndex].Cells[jj].Style=cellStyle2;

                }




              }
            }

          }
         prevFindNo = iFindNo ;
        }

        //SendMessage(dataGridViewP.Handle, WM_SETREDRAW, true, 0);
        // dp.Clear();

        foreach (ARCSUM1 vq in sum)
        {
          AddLogString(" Архив = " + vq.NAME1 + " (" + vq.ID_GINFO + ")  = " + vq.SUM  );
        }

        // отключаем сортировку для каждого столбца
        foreach (DataGridViewColumn col in dataGridViewP.Columns)
        {  // DataGridViewColumnSortMode.NotSortable;
          col.SortMode = DataGridViewColumnSortMode.Automatic ;
        }

        // Resize the master DataGridView columns to fit the newly loaded data.
        dataGridViewP.AutoResizeColumns();

        // вызывает зависание
        //dataGridViewP.AutoSizeColumnsMode =
        //    DataGridViewAutoSizeColumnsMode.AllCells;
        //dataGridViewP.AutoGenerateColumns = true;

        //dataGridViewP.Update();

        toolStripStatusLabel2.Text = "Buiding lists .. done";

        PCellValueChanged = 1;

    }


    void TreeViewAAfterSelect(object sender, TreeViewEventArgs e)
    {
      /*
      построение
       либо списка параметров (с архивами)
       либо список профилей архивов с активными на данном Тех сервере
      */

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();

      cmd0.Connection=this._conn;

      //Получить Имя выделенного элемента
      string id_parent = treeViewA.SelectedNode.Name;
      string _id_tbl = Convert.ToString ( treeViewA.SelectedNode.Tag ) ;
      //AddLogString(" id_parent=" + id_parent + "  _id_tbl=" + _id_tbl);
      if (_id_tbl=="0" || _id_tbl=="") return ;

      // re-read
      //dataGridViewA.Rows.Clear();
      Application.DoEvents();

      string stSchema="";
      if (OptionSchemaName>0) {
          stSchema=OptionSchemaMain + "." ;
      }

      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

      if (1==tabControlAP.SelectedIndex) {

        TreeViewAAfterSelecttabControl1(sender) ;

      } else {

        dataSetA.Clear();

        // "select * from ARC_GINFO";
        cmd0.CommandText= r.GetString("ARC_GINFO1");

        // Указываем запрос для выполнения
        adapter.SelectCommand = cmd0;
        // Заполняем объект источника данных
        //adapter.Fill(dataSetA,"ARC_GINFO");
        adapter.Fill(dataSetA);

        Application.DoEvents();

        // Запрет удаления данных
        dataSetA.Tables[0].DefaultView.AllowDelete = false;
        // Запрет модификации данных
        dataSetA.Tables[0].DefaultView.AllowEdit = false;
        // Запрет добавления данных
        dataSetA.Tables[0].DefaultView.AllowNew = false;

        // (с этого момента она будет отображать его содержимое)
        dataGridViewA.DataSource = dataSetA.Tables[0].DefaultView;;

        // Resize the master DataGridView columns to fit the newly loaded data.
        dataGridViewA.AutoResizeColumns();

        // Configure the details DataGridView so that its columns automatically
        // adjust their widths when the data changes.
        dataGridViewA.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;

        DataGridViewASorted(sender,  e);

      } // change tab

    }


/*
 получение Имени структуры БД из табл tree21
 и вывод в заголовок Формы
*/
    void _tree21()
    {

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      Application.DoEvents();

      //this.Text = this.Text + "  :  " + Application.ProductVersion ;

      //ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

      string stSchema="";
      if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
      }

      string DB_NAME = "" ;
      // 1. получаем имя   -- SELECT id, name FROM SYS_TREE21 WHERE COALESCE(id_parent,0,0)=0 AND COALESCE(ID_LSTTBL,0,0)=0
      cmd0.CommandText="SELECT id, name FROM  "+stSchema+"SYS_TREE21 " +
                       " WHERE (( COALESCE(id_parent,0,0)=0 AND COALESCE(ID_LSTTBL,0,0)=0 ) " +
                            " or (id_parent='' and ID_LSTTBL='')) ";

      try
      {
         reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
         AddLogString("rsdu schema empty = " + cmd0.CommandText + " " + ex1.Message);
         //reader.Close();
         return ;
      }
      if (reader.HasRows) {
         string[] arr = new string[2];
         while (reader.Read())
         {
            for ( int i = 0; i<2; i++)
            {
              arr[i]=GetTypeValue(ref reader, i);
            }
            DB_NAME = arr[1];
            break ;
         } // while
         AddLogString("rsdu schema =" + DB_NAME );

         this.Text = this.Text + "  :  " + DB_NAME ;

      }
      reader.Close();

    }


/* DB_VER_FLAG
 получение Имени БД и version
1) oracle : select * from v$version;
2) mysql :  SELECT VERSION();
3) sqlite : select sqlite_version();
4) Postgres : SELECT version();
*/
    void _version()
    {

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      Application.DoEvents();

      this.Text = this.Text + "  :  "  ;

      string DB_VER = "" ;
      DB_VER_FLAG=0 ; // 0 = none

      cmd0.CommandText="select sqlite_version()";
      try
      {
         reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
         //reader.Close();
         DB_VER_FLAG=-1;
      }
      if (DB_VER_FLAG==0) {
       if (reader.HasRows) {
         while (reader.Read())
         {
            DB_VER = GetTypeValue(ref reader, 0);
            break ;
         } // while
         DB_VER_FLAG=3;
         this.Text = this.Text + " Sqlite ( " + DB_VER + " )" ;
       }
       reader.Close();
       //AddLogString("sqlite =" + DB_VER );
      }
      if (DB_VER_FLAG>0) { return ; }

      DB_VER_FLAG = 0 ;
      cmd0.CommandText="SELECT VERSION()";
      try
      {
         reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
         DB_VER_FLAG=-1;
      }
      if (DB_VER_FLAG==0) {
       if (reader.HasRows) {
         while (reader.Read())
         {
            DB_VER = GetTypeValue(ref reader, 0);
            break ;
         } // while
         DB_VER_FLAG=2;
         this.Text = this.Text + " mysql|Postgres ( " + DB_VER + " )" ;
       }
       reader.Close();
      }
      if (DB_VER_FLAG>0) { return ; }

      DB_VER_FLAG = 0 ;
      cmd0.CommandText="select banner from v$version";
      try
      {
         reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
         DB_VER_FLAG=-1;
      }
      if (DB_VER_FLAG==0) {
       if (reader.HasRows) {
         while (reader.Read())
         {
            DB_VER = GetTypeValue(ref reader, 0);
            break ;
         } // while
         DB_VER_FLAG=1;
         //AddLogString("oracle =" + DB_VER );
         this.Text = this.Text + " " + DB_VER ;
       }
       reader.Close();
      }
      if (DB_VER_FLAG>0) { return ; }

    }


/*
заполняем структуру таблиц в виде дерева
*/
    void _setDBS()
    {
       treeViewS.BeginUpdate(); //добавить
       treeViewS.Nodes.Clear();


       string stSchema="";
       if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
       }


       TreeNode rootNode1 = new TreeNode();
       rootNode1.Name = "0";
       rootNode1.Text = "Справочники подсистемы архивов";
       treeViewS.Nodes.Add(rootNode1);

       TreeNode Nd0 = new TreeNode();
       Nd0.Name = stSchema+"ARC_GINFO";
       Nd0.Text = "Таблица групп архивов (ARC_GINFO)";
       rootNode1.Nodes.Add(Nd0);

       TreeNode Nd00 = new TreeNode();
       Nd00.Name = stSchema+"ARC_SUBSYST_PROFILE";
       Nd00.Text = "Список типов архивов, поддерживаемых подсистемами комплекса (ARC_SUBSYST_PROFILE)";
       rootNode1.Nodes.Add(Nd00);

       TreeNode Nd1 = new TreeNode();
       Nd1.Name = stSchema+"ARC_FTR";
       Nd1.Text = "Справочная таблица опций записи архивов (ARC_FTR)";
       rootNode1.Nodes.Add(Nd1);

       TreeNode Nd2 = new TreeNode();
       Nd2.Name = stSchema+"ARC_TYPE";
       Nd2.Text = "Справочная таблица типов архивов (ARC_TYPE)";
       rootNode1.Nodes.Add(Nd2);

       TreeNode Nd3 = new TreeNode();
       Nd3.Name = stSchema+"ARC_SERVICES_TYPE";
       Nd3.Text = "Таблица типов сервисов (ARC_SERVICES_TYPE)";
       rootNode1.Nodes.Add(Nd3);

       TreeNode Nd31 = new TreeNode();
       Nd31.Name = stSchema+"ARC_SERVICES_TUNE";
       Nd31.Text = "Настройка хранения архивов подсистем в определенной БД Архивов (ARC_SERVICES_TUNE)";
       rootNode1.Nodes.Add(Nd31);


       TreeNode Nd4 = new TreeNode();
       Nd4.Name = stSchema+"ARC_DB_SCHEMA";
       Nd4.Text = "Таблица описания схем владельцев архивов (ARC_DB_SCHEMA)";
       rootNode1.Nodes.Add(Nd4);

       TreeNode Nd5 = new TreeNode();
       Nd5.Name = stSchema+"ARC_READ_DEFAULTS";
       Nd5.Text = "Типы архивов, возвр. сервером оперативного доступа по умолч. (ARC_READ_DEFAULTS)";
       rootNode1.Nodes.Add(Nd5);

       TreeNode Nd6 = new TreeNode();
       Nd6.Name = stSchema+"ARC_INTEGRITY_DESC";
       Nd6.Text = "Состояния - для журнала управления целостностью архивов (ARC_INTEGRITY_DESC)";
       rootNode1.Nodes.Add(Nd6);

       TreeNode Nd61 = new TreeNode();
       Nd61.Name = stSchema+"ARC_STORAGE_TYPE";
       Nd61.Text = "Типы хранилища (ARC_STORAGE_TYPE)";
       rootNode1.Nodes.Add(Nd61);

       TreeNode Nd11 = new TreeNode();
       Nd11.Name = stSchema+"ARC_SERVICES_INFO";
       Nd11.Text = "Соответствия сервисов хранения архивов (ARC_SERVICES_INFO)";
       rootNode1.Nodes.Add(Nd11);

       TreeNode Nd12 = new TreeNode();
       Nd12.Name = stSchema+"ARC_SERVICES_ACCESS";
       Nd12.Text = "Настройка глубины оперативного буфера серверов (ARC_SERVICES_ACCESS)";
       rootNode1.Nodes.Add(Nd12);

       TreeNode Nd14 = new TreeNode();
       Nd14.Name = stSchema+"ARC_READ_VIEW";
       Nd14.Text = "Связи для доступа к архивам (ARC_READ_VIEW)";
       rootNode1.Nodes.Add(Nd14);


       TreeNode rootNode2 = new TreeNode();
       rootNode2.Name = "0";
       rootNode2.Text = "Справочники системные";
       treeViewS.Nodes.Add(rootNode2);

       TreeNode Nd7 = new TreeNode();
       Nd7.Name = stSchema+"SYS_GTOPT";
       Nd7.Text = "Виды характеристик параметров поддерживаемых системой (SYS_GTOPT)";
       rootNode2.Nodes.Add(Nd7);

       TreeNode Nd71 = new TreeNode();
       Nd71.Name = stSchema+"SYS_ATYP";
       Nd71.Text = "Типы агрегированной информации (SYS_ATYP)";
       rootNode2.Nodes.Add(Nd71);

       TreeNode Nd8 = new TreeNode();
       Nd8.Name = stSchema+"SYS_GTYP";
       Nd8.Text = "Глобальные типы данных (SYS_GTYP)";
       rootNode2.Nodes.Add(Nd8);

       TreeNode Nd9 = new TreeNode();
       Nd9.Name = stSchema+"SYS_TBLLST";
       Nd9.Text = "Глобальные типы данных (SYS_TBLLST)";
       rootNode2.Nodes.Add(Nd9);

       TreeNode Nd30 = new TreeNode();
       Nd30.Name = stSchema+"SYS_DB_PART";
       Nd30.Text = "Каталог разделов БД и подсистем комплекса РСДУ2 (SYS_DB_PART)";
       rootNode2.Nodes.Add(Nd30);


       TreeNode rootNode3 = new TreeNode();
       rootNode3.Name = "0";
       rootNode3.Text = "Справочники";
       treeViewS.Nodes.Add(rootNode3);

       TreeNode Nd10 = new TreeNode();
       Nd10.Name = stSchema+"AD_SERVICE";
       Nd10.Text = "Таблица характеристик типовых сервисов (AD_SERVICE)";
       rootNode3.Nodes.Add(Nd10);


       TreeNode rootNode4 = new TreeNode();
       rootNode4.Name = "0";
       rootNode4.Text = "Таблицы";
       treeViewS.Nodes.Add(rootNode4);

       TreeNode Nd13 = new TreeNode();
       Nd13.Name = stSchema+"ARC_VIEW_PARTITIONS";
       Nd13.Text = "Описания разделов стека для архивов мгновенных значений (ARC_VIEW_PARTITIONS)";
       rootNode4.Nodes.Add(Nd13);

       TreeNode Nd15 = new TreeNode();
       Nd15.Name = stSchema+"ARC_STAT";
       Nd15.Text = "Статистика записи архивов (ARC_STAT)";
       rootNode4.Nodes.Add(Nd15);


       TreeNode rootNode5 = new TreeNode();
       rootNode5.Name = "0";
       rootNode5.Text = "Журналы";
       treeViewS.Nodes.Add(rootNode5);

       TreeNode Nd16 = new TreeNode();
       Nd16.Name = stSchema+"ARC_INTEGRITY";
       Nd16.Text = "Журнал управления целостностью архивов (ARC_INTEGRITY)";
       rootNode5.Nodes.Add(Nd16);

       TreeNode Nd17 = new TreeNode();
       Nd17.Name = stSchema+"J_ARC_HIST_CLEAR";
       Nd17.Text = "Журнал выполнения чисток архивов параметров по разделам (J_ARC_HIST_CLEAR)";
       rootNode5.Nodes.Add(Nd17);

       TreeNode Nd18 = new TreeNode();
       Nd18.Name = stSchema+"J_ARC_RESTORE";
       Nd18.Text = "Журнал восстановления пропусков архивных таблиц параметров (J_ARC_RESTORE)";
       rootNode5.Nodes.Add(Nd18);

       TreeNode Nd19 = new TreeNode();
       Nd19.Name = stSchema+"J_ARC_VAL_CHANGE";
       Nd19.Text = "Журнал изменения значений архивных таблиц параметров (J_ARC_VAL_CHANGE)";
       rootNode5.Nodes.Add(Nd19);

       TreeNode Nd20 = new TreeNode();
       Nd20.Name = stSchema+"J_MOVE_STACK";
       Nd20.Text = "Журнал разбора подсистемы архивов (J_MOVE_STACK)";
       rootNode5.Nodes.Add(Nd20);

       TreeNode Nd21 = new TreeNode();
       Nd21.Name = stSchema+"J_RSDU_ERROR";
       Nd21.Text = "Журнал ошибок РСДУ (J_RSDU_ERROR)";
       rootNode5.Nodes.Add(Nd21);


       TreeNode rootNode8 = new TreeNode();
       rootNode8.Name = "0";
       rootNode8.Text = "Описание таблиц хранения архивов";
       treeViewS.Nodes.Add(rootNode8);

       TreeNode Nd22 = new TreeNode();
       Nd22.Name = stSchema+"MEAS_ARC";
       Nd22.Text = "Описание таблиц хранения архивов измерений (MEAS_ARC)";
       rootNode8.Nodes.Add(Nd22);

       TreeNode Nd23 = new TreeNode();
       Nd23.Name = stSchema+"DG_ARC";
       Nd23.Text = "Описание таблиц хранения архивов диспетчерских графиков (DG_ARC)";
       rootNode8.Nodes.Add(Nd23);

       TreeNode Nd24 = new TreeNode();
       Nd24.Name = stSchema+"DA_ARC";
       Nd24.Text = "Описание таблиц хранения архивов параметров подсистемы сбора (DA_ARC)";
       rootNode8.Nodes.Add(Nd24);

       TreeNode Nd25 = new TreeNode();
       Nd25.Name = stSchema+"CALC_ARC";
       Nd25.Text = "Описание таблиц хранения архивов универсального дорасчета (CALC_ARC)";
       rootNode8.Nodes.Add(Nd25);


       TreeNode rootNode6 = new TreeNode();
       rootNode6.Name = "0";
       rootNode6.Text = "Таблицы источников";
       treeViewS.Nodes.Add(rootNode6);

       TreeNode rootNode61 = new TreeNode();
       rootNode61.Name = stSchema+"CALC_SOURCE";
       rootNode61.Text = "Получения значений параметров (CALC_SOURCE)";
       rootNode6.Nodes.Add(rootNode61);

       TreeNode rootNode62 = new TreeNode();
       rootNode62.Name = stSchema+"DA_SOURCE";
       rootNode62.Text = "Для рапределенной системы сбора (DA_SOURCE)";
       rootNode6.Nodes.Add(rootNode62);

       TreeNode rootNode63 = new TreeNode();
       rootNode63.Name = stSchema+"DG_SOURCE";
       rootNode63.Text = "Получения значений параметров ДГ (DG_SOURCE)";
       rootNode6.Nodes.Add(rootNode63);

       TreeNode rootNode64 = new TreeNode();
       rootNode64.Name = stSchema+"EA_SOURCE";
       rootNode64.Text = "Для параметров учета электроэнергии (EA_SOURCE)";
       rootNode6.Nodes.Add(rootNode64);

       TreeNode rootNode65 = new TreeNode();
       rootNode65.Name = stSchema+"MEAS_SOURCE";
       rootNode65.Text = "Получения значений параметров (MEAS_SOURCE)";
       rootNode6.Nodes.Add(rootNode65);


       treeViewA.EndUpdate(); //добавить

    }


/*     -----------------------------     */
    void _setDBCalc()
    {
       string stSchema="";
       if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
       }


       for (int i = 0; i < 10; ++i)
       {
         //Добавляем строку, указывая значения каждой ячейки по имени (можно использовать индекс 0, 1, 2 вместо имен)
         dataGridViewCalc.Rows.Add();
       }

       foreach (DataGridViewColumn column in dataGridViewCalc.Columns)
       {

           column.HeaderText = String.Concat("#",column.Index.ToString());
       }

       dataGridViewCalc.Rows[0].Cells[0].Value="";

       dataGridViewCalc[1, 0].Value="Параметры Сбора";
       dataGridViewCalc[2, 0].Value="Электрические параметры";
       dataGridViewCalc[3, 0].Value="Прочие параметры";
       dataGridViewCalc[4, 0].Value="Коммутационные аппараты";
       dataGridViewCalc[5, 0].Value="Устройства  защиты";
       dataGridViewCalc[6, 0].Value="Внешний Сбор";
       dataGridViewCalc[7, 0].Value="Диспетчерские графики";
       dataGridViewCalc[8, 0].Value="Формула";

       dataGridViewCalc.Rows[0].ReadOnly=true;
       dataGridViewCalc.Rows[0].DefaultCellStyle.Font  = new Font(DataGridView.DefaultFont, FontStyle.Bold);

       dataGridViewCalc[0, 1].Value="Число параметров:";
       dataGridViewCalc[1, 1].Value="10000";
       dataGridViewCalc[2, 1].Value="7000";
       dataGridViewCalc[3, 1].Value="2000";
       dataGridViewCalc[4, 1].Value="1000";
       dataGridViewCalc[5, 1].Value="1000";
       dataGridViewCalc[6, 1].Value="100";
       dataGridViewCalc[7, 1].Value="50";
       dataGridViewCalc[8, 1].Value="п.1";

       dataGridViewCalc[0,2].Value="Ширина одной записи ( в байтах ):";
       dataGridViewCalc[1,2].Value="90";
       dataGridViewCalc[2,2].Value="90";
       dataGridViewCalc[3,2].Value="90";
       dataGridViewCalc[4,2].Value="90";
       dataGridViewCalc[5,2].Value="90";
       dataGridViewCalc[6,2].Value="90";
       dataGridViewCalc[7,2].Value="90";
       dataGridViewCalc[8,2].Value="п.2 = TIME1970(11) + PARAM(11) + STATUS(11) + VAL(19) + VMIN(19) + VMAX(19) = 90";

      dataGridViewCalc[0,3].Value="Количество записей в час:";
      dataGridViewCalc[1,3].Value="3600";
      dataGridViewCalc[2,3].Value="1";
      dataGridViewCalc[3,3].Value="1";
      dataGridViewCalc[4,3].Value="1";
      dataGridViewCalc[5,3].Value="1";
      dataGridViewCalc[6,3].Value="1";
      dataGridViewCalc[7,3].Value="1";
      dataGridViewCalc[8,3].Value="п.3";

      dataGridViewCalc[0, 4].Value="Длительность хранения архивов, в часах:";
      dataGridViewCalc[1, 4].Value="720";
      dataGridViewCalc[2, 4].Value="720";
      dataGridViewCalc[3, 4].Value="720";
      dataGridViewCalc[4, 4].Value="720";
      dataGridViewCalc[5, 4].Value="720";
      dataGridViewCalc[6, 4].Value="720";
      dataGridViewCalc[7, 4].Value="720";
      dataGridViewCalc[8, 4].Value="п.4";

      dataGridViewCalc[0, 5].Value="Объем данных, гб:";
      dataGridViewCalc[1, 5].Value="0";
      dataGridViewCalc[2, 5].Value="0";
      dataGridViewCalc[3, 5].Value="0";
      dataGridViewCalc[4, 5].Value="0";
      dataGridViewCalc[5, 5].Value="0";
      dataGridViewCalc[6, 5].Value="0";
      dataGridViewCalc[7, 5].Value="0";
      dataGridViewCalc[8, 5].Value="п.5 = ( n.1 * n.2 * n.3 * n.4 ) / (1024 * 1024 * 1024)";

      dataGridViewCalc[0, 6].Value="С учетом индексов (обслуживание поиска, построения графиков, анализа и т.п.), гб:";
      dataGridViewCalc[1, 6].Value="0";
      dataGridViewCalc[2, 6].Value="0";
      dataGridViewCalc[3, 6].Value="0";
      dataGridViewCalc[4, 6].Value="0";
      dataGridViewCalc[5, 6].Value="0";
      dataGridViewCalc[6, 6].Value="0";
      dataGridViewCalc[7, 6].Value="0";
      dataGridViewCalc[8, 6].Value="п.6 =  п.5 *50% ";

      dataGridViewCalc[0, 7].Value="Дополнительный объем для системных и оперативных объектов БД, гб:";
      dataGridViewCalc[1, 7].Value="0";
      dataGridViewCalc[2, 7].Value="0";
      dataGridViewCalc[3, 7].Value="0";
      dataGridViewCalc[4, 7].Value="0";
      dataGridViewCalc[5, 7].Value="0";
      dataGridViewCalc[6, 7].Value="0";
      dataGridViewCalc[7, 7].Value="0";
      dataGridViewCalc[8, 7].Value="п.7 = (п.5 + п.6)*10%";

      dataGridViewCalc[0, 8].Value="Дополнительный объем для выполнения резервного копирования БД, гб:";
      dataGridViewCalc[1, 8].Value="0";
      dataGridViewCalc[2, 8].Value="0";
      dataGridViewCalc[3, 8].Value="0";
      dataGridViewCalc[4, 8].Value="0";
      dataGridViewCalc[5, 8].Value="0";
      dataGridViewCalc[6, 8].Value="0";
      dataGridViewCalc[7, 8].Value="0";
      dataGridViewCalc[8, 8].Value="п.8 = (п.5 + п.6 + п.7)*50%";

      dataGridViewCalc[0, 9].Value="Сумма = , гб:";
      dataGridViewCalc[1, 9].Value="0";
      dataGridViewCalc[2, 9].Value="0";
      dataGridViewCalc[3, 9].Value="0";
      dataGridViewCalc[4, 9].Value="0";
      dataGridViewCalc[5, 9].Value="0";
      dataGridViewCalc[6, 9].Value="0";
      dataGridViewCalc[7, 9].Value="0";
      dataGridViewCalc[8, 9].Value="п.9 = (п.5 + п.6 + п.7 + п.8)";


      //dataGridViewCalc.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
      dataGridViewCalc.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

      dataGridViewCalc.AutoResizeColumns();


      // Configure the details DataGridView so that its columns automatically
      // adjust their widths when the data changes.
      dataGridViewCalc.AutoSizeColumnsMode =
             DataGridViewAutoSizeColumnsMode.AllCells;
      dataGridViewCalc.AutoGenerateColumns = true;

      dataGridViewCalc.EnableHeadersVisualStyles = false;
      // dataGridViewCalc.AlternatingRowsDefaultCellStyle.BackColor =Color.LightGray;

      dataGridViewCalc.Update();

      comboBoxCalc.SelectedIndex=6;

    }


void _Meas30 ( )
{
    // Объект для связи между базой данных и источником данных
    OdbcDataAdapter adapter = new OdbcDataAdapter();
    // Объект для выполнения запросов к базе данных
    OdbcCommand cmd0 = new OdbcCommand();
    ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

    cmd0.Connection=this._conn;

    string stSchema="";
    if (OptionSchemaName>0) {
      stSchema=OptionSchemaMain + "." ;
    }

    // get
    string sl1 = r.GetString("MEAS_SNAPSHOT30_TUNE_t");
    sl1 = String.Format(sl1, stSchema+"MEAS_SNAPSHOT30_TUNE",stSchema+"OBJ_TREE");

    cmd0.CommandText=sl1;



    //=========================================================
    AddLogStringNoTime("------------------------");



    AddLogStringNoTime("------------------------");
    //=========================================================
    toolStripStatusLabel2.Text = "Read meas30 tables - finished.";
    return ;
}



    void TreeViewSAfterSelect(object sender, TreeViewEventArgs e)
    {
       /*
       событие после выбора таблицы
       */

       // Объект для связи между базой данных и источником данных
       OdbcDataAdapter adapter = new OdbcDataAdapter();
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();

       cmd0.Connection=this._conn;

       if (this._conn.State==System.Data.ConnectionState.Closed) {
         AddLogString("Невозможно установить соединение с БД");
         return ;
       }

       //Получить Имя выделенного элемента
       string table_name=treeViewS.SelectedNode.Name;
       if (table_name=="0") return ;
       if (table_name=="") return ;

       //AddLogString("Tb=" + id_parent);

       //удалим все строки из dataGridView1
       //while (0 != dataGridViewS.Columns.Count)
       //        dataGridViewS.Columns.RemoveAt(0);


       string stSchema="";
       if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
       }


       cmd0.CommandText="SELECT * FROM " + stSchema + table_name;
       Application.DoEvents();

       dataSetS.Clear();
       dataGridViewS.DataSource = null;
       dataSetS.Tables.Clear();

       //bindingSourceS.Clear();
       //bindingNavigatorS.BindingSource.Clear();

       // Указываем запрос для выполнения
       adapter.SelectCommand = cmd0;
       // Заполняем объект источника данных
       adapter.Fill(dataSetS,table_name);

       // (с этого момента она будет отображать его содержимое)
       dataGridViewS.DataSource = dataSetS.Tables[0];
       //dataGridViewS.DataMember=table_name;

       bindingSourceS.DataSource=dataSetS.Tables[0];
       bindingNavigatorS.BindingSource = bindingSourceS;


       // Resize the master DataGridView columns to fit the newly loaded data.
       dataGridViewS.AutoResizeColumns();

       // Configure the details DataGridView so that its columns automatically
       // adjust their widths when the data changes.
       dataGridViewS.AutoSizeColumnsMode =
             DataGridViewAutoSizeColumnsMode.AllCells;
       dataGridViewS.AutoGenerateColumns = true;

       dataGridViewS.EnableHeadersVisualStyles = false;
       dataGridViewS.AlternatingRowsDefaultCellStyle.BackColor =Color.LightGray;

       dataGridViewS.Update();

    }

    void Timer1Tick(object sender, EventArgs e)
    {
       // событие обновления только при выборе вьюшки arc_stat_current_v  1 раз в 10 сек

       DateTime end = DateTime.Now;
       //LocalDateTime end = LocalDateTime.now();
       //toolStripStatusLabel1.Text = DateTime.Today.ToLongDateString();
       toolStripStatusLabel1.Text = end.ToString();

       string s1=end.ToString("ss");
       int num = Convert.ToInt32(s1, 10 );

       // && tabPage1.Focused==true  dataGridView1.Focused==true
       if (radioButton2.Checked==true)
       {
         if (num%OptionUpdate==0) {
            s1 = "..update.."+s1;
            try
            {
               _getDBv1("arc_stat_current_v");
            }
            catch (Exception ex1)
            {
               s1="Error ="+ex1.Message;
            }
            //AddLogString("Timer1->arc_stat_current_v");
            toolStripStatusLabel2.Text = s1;
         }
       } else {
         if (num%30==0) {
           toolStripStatusLabel2.Text = "";
         }
       }

    }
    void ToolStripButtonCommitClick(object sender, EventArgs e)
    {
       // событие для StripButton  = Commit

       string table_name=treeViewS.SelectedNode.Name;
       if (table_name=="0") return ;
       AddLogString("Commit=" + table_name);

       try
       {
         using (OdbcDataAdapter adapter = new OdbcDataAdapter()) {
             OdbcCommand cmd0 = new OdbcCommand();
             cmd0.Connection=this._conn;
             //adapter.UpdateCommand = cmd0;
             cmd0.CommandText="select * from " + table_name;
             // Указываем запрос для выполнения
             adapter.SelectCommand = cmd0;
             OdbcCommandBuilder cb;
             cb = new OdbcCommandBuilder(adapter);
             adapter.UpdateCommand = cb.GetUpdateCommand();
             string s = adapter.UpdateCommand.CommandText;
             AddLogString(s);
             adapter.Update(dataSetS,table_name);
        }
       }
       catch (Exception ex1)
       {
         AddLogString(" Exception= "+ex1.Message );
       }


    }

    void DataGridViewASelectionChanged(object sender, EventArgs e)
    {

      // событие при выборе элемента профиля архива
      //

      if (dataGridViewA.SelectedRows.Count==0) {
        //AddLogString(" DataGridViewASelectionChanged-> = 0"   );
        return  ;
      }

      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      if (selRowNum<0) {
        //AddLogString(" DataGridViewASelectionChanged-> = " + selRowNum.ToString()  );
        return  ;
      }

      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;
      //String ck = dataGridViewA.Rows[selRowNum].Cells[0].Value.ToString() ;

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      //Получить Имя выделенного элемента
      string ID_TBLLST = Convert.ToString(treeViewA.SelectedNode.Tag) ;

      string stSchema="";
      if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
      }

      string sl1="SELECT  ID, ID_TBLLST, ID_GINFO, IS_WRITEON, STACK_NAME, LAST_UPDATE, IS_VIEWABLE  " +
      "FROM  "+stSchema+"ARC_SUBSYST_PROFILE " +
      "WHERE ID_TBLLST=" + ID_TBLLST + " AND ID_GINFO="+ID_GINFO ;

      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" SelectionChanged-> выполнение алгоритма  - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
        return ;
      }

      //устанавливаем редактируемый объект
      _profileData.ID="";
      _profileData.ID_TBLLST="";
      _profileData.ID_GINFO="";
      _profileData.IS_WRITEON=false ;
      _profileData.STACK_NAME="NONE";
      _profileData.LAST_UPDATE="";
      _profileData.IS_VIEWABLE=false ;

      buttonSave.Enabled=false ;
      buttonService.Enabled = false ;

      string[] arr = new string[7];

      if (reader.HasRows) {
        while (reader.Read())
        {
          for ( int i = 0; i<7; i++)
          {
            arr[i]= GetTypeValue(ref reader, i);
          }
        } // while

        //устанавливаем редактируемый объект
        _profileData.ID=arr[0];
        _profileData.ID_TBLLST=arr[1];
        _profileData.ID_GINFO=arr[2];
        bool v1 = true ;
        if (arr[3]=="0") v1 = false ;
        _profileData.IS_WRITEON=v1 ;
        _profileData.STACK_NAME=arr[4];
        if (arr[5]!="" && arr[5]!="0") {
           DateTime t1 = UnixTimestampToDateTime(Convert.ToDouble(arr[5]));

           t1=t1.ToLocalTime();

           _profileData.LAST_UPDATE=t1.ToString();
        } else {
           _profileData.LAST_UPDATE=arr[5];
        }
        bool v2 = true ;
        if (arr[6]=="0") v2 = false ;
        _profileData.IS_VIEWABLE=v2 ;

        buttonSave.Enabled=true ;
        buttonService.Enabled = true ;
      }

      reader.Close();

      propertyGridA.SelectedObject=_profileData;
      propertyGridA.Update();

      dataGridViewA.EndEdit();
    }
    void ToolStripMenuItem1Click(object sender, EventArgs e)
    {
      // modify - создание окна для модификаций профиля архивов
      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      if (selRowNum<0) return ;
      string id_tbl = Convert.ToString(treeViewA.SelectedNode.Tag) ;
      if (id_tbl=="0" || id_tbl=="") return ;
      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;

      Form ifrm = new FormArcGinfo(this._conn, ID_GINFO, id_tbl, OptionSchemaName);
      ifrm.ShowDialog();
      TreeViewAAfterSelect(treeViewA, null);
      // dataGridViewA.FirstDisplayedScrollingRowIndex=selRowNum;

      dataGridViewA.CurrentCell = dataGridViewA.Rows[selRowNum].Cells[0] ;
      // dataGridViewA.Rows[selRowNum].Selected=true ;

      dataGridViewA.Update();
    }
    void ToolStripMenuItem2Click(object sender, EventArgs e)
    {
      // copy - скопировать строку и вставить в Профиль архивов
      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;
      string id_dest = "";
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;


      string stSchema="";
      if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
      }


      string sl1= "SELECT ID+1 FROM "+stSchema+"ARC_GINFO WHERE ID+1 NOT IN (SELECT ID FROM "+stSchema+"ARC_GINFO )" ; //  AND id > 3
      //AddLogString(" copy-> = " + sl1);
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" copy-> break .. = " + cmd0.CommandText + " " + ex1.Message);
        return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
          id_dest = GetTypeValue(ref reader, 0);
          break ;
        } // while
      }
      reader.Close();

      if (id_dest=="")
      {
        AddLogString(" copy-> break .. id_dest = " + id_dest);
        return ;
      }

      sl1="INSERT INTO  "+stSchema+"ARC_GINFO (" +
"    ID ,  " +
"    ID_GTOPT ,  " +
"    ID_TYPE, " +
"    DEPTH , " +
"    DEPTH_LOCAL , " +
"    CACHE_SIZE , " +
"    CACHE_TIMEOUT , " +
"    FLUSH_INTERVAL , " +
"    RESTORE_INTERVAL , " +
"    STACK_INTERVAL , " +
"    WRITE_MINMAX  , " +
"    RESTORE_TIME , " +
"    NAME , " +
"    STATE , " +
"    DEPTH_PARTITION  , " +
"    RESTORE_TIME_LOCAL ) " +
"SELECT  " +
"    " + id_dest + " , " +
"    ag.ID_GTOPT ,  " +
"    ag.ID_TYPE, " +
"    ag.DEPTH , " +
"    ag.DEPTH_LOCAL , " +
"    ag.CACHE_SIZE , " +
"    ag.CACHE_TIMEOUT , " +
"    ag.FLUSH_INTERVAL , " +
"    ag.RESTORE_INTERVAL , " +
"    ag.STACK_INTERVAL , " +
"    ag.WRITE_MINMAX  , " +
"    ag.RESTORE_TIME , " +
"    ag.NAME , " +
"    ag.STATE , " +
"    ag.DEPTH_PARTITION  , " +
"    ag.RESTORE_TIME_LOCAL " +
" FROM "+stSchema+"ARC_GINFO ag" +
" WHERE ag.ID=" + ID_GINFO + " ;";

      int crec1 = 0 ;
      cmd0.CommandText=sl1;
      try
      {
        crec1=cmd0.ExecuteNonQuery();
      }
      catch (Exception ex1)
      {
        AddLogString(" copy-> Ошибка. " + cmd0.CommandText + " , " + ex1.Message);
        return ;
      }
      AddLogString(" copy-> в id = " + id_dest + " (статус = " + crec1.ToString() + " )");
      if (crec1>0) {
        TreeViewAAfterSelect(sender, null);
        MessageBox.Show ("Профиль id=" +ID_GINFO + "\n скопирован в id = " + id_dest ,"Копирование строк", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

    }
    void ToolStripMenuItem3Click(object sender, EventArgs e)
    {
      // delete удаление строки в таблице профиля архивов
      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string stSchema="";
      if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
      }

      string sl1="SELECT count(*) FROM "+stSchema+"ARC_SUBSYST_PROFILE WHERE ID_GINFO="+ID_GINFO ;

      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" удаление строки -> выполнение алгоритма  - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
        return ;
      }

      int cnt=0;

      if (reader.HasRows) {
        while (reader.Read())
        {
          cnt = Convert.ToInt32(GetTypeValue(ref reader, 0));
        } // while
      }
      reader.Close();

      if (cnt==0) {
        DialogResult result = MessageBox.Show ("Действительно удалить ? строку профиля архива id=" +ID_GINFO ,"Удаление строки", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
          cmd0.CommandText="DELETE FROM "+stSchema+"ARC_GINFO WHERE ID="+ID_GINFO ;
          int crec1 = 0 ;
          try
          {
            crec1=cmd0.ExecuteNonQuery();
          }
          catch (Exception ex1)
          {
            AddLogString(" ToolStripMenuItem3Click-> delete - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
            MessageBox.Show ("Ошибка удаления !" ,"Удаление строки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return ;
          }
          if (crec1>0) {
            TreeViewAAfterSelect(sender, null );
            MessageBox.Show ("Профиль архива id=" +ID_GINFO + " удален!" ,"Удаление строки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
      } else {
       MessageBox.Show ("Профиль id=" +ID_GINFO + " используется, удаление невозможно." ,"Удаление строки", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }

    }
    void DataGridViewACellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      // отработка нажатия галочки в таблице Профиля архивов
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());
      List<MEAS1> dp = new List<MEAS1>();

      string stSchema="";
      if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
      }

      int chkBoxColumnIndex = 0;
      var dataGridView = (DataGridView)sender;
      if (e.ColumnIndex == chkBoxColumnIndex)
      {
        dataGridView.EndEdit();
        var isChecked = Convert.ToBoolean(dataGridView[e.ColumnIndex, e.RowIndex].Value);

        int selRowNum = dataGridView.CurrentCell.RowIndex;
        string ID_GINFO = dataGridView.Rows[selRowNum].Cells[1].Value.ToString() ;
        string id_tbl = Convert.ToString(treeViewA.SelectedNode.Tag) ;

        string nm1 = dataGridView.Rows[selRowNum].Cells[2].Value.ToString() ;
        string nm2 = dataGridView.Rows[selRowNum].Cells[15].Value.ToString() ;

        if (isChecked)
        {
          /*
           элемент чекнутый - мы пытаемся снять удаление
          */

          _profileData=(ARC_SUBSYST_PROFILE)propertyGridA.SelectedObject;
          String ID = _profileData.ID ;
          if (ID=="") {
              return ;
          }

          //ID_GINFO = _profileData.ID_GINFO ;
          //id_tbl = _profileData.ID_TBLLST ;

          int dlCnt = 0 ;
          string sl1 = "" ;


          /*
           получаем имена таблиц для удаления из таблиц архива MEAS_ARC\DA_ARC
          */

          dataGridViewA.Enabled = false ;

          // 2. получаем название таблицы раздела

          // get table
          string TABLE_NAME = "" ;

          sl1= "SELECT UPPER(lst.TABLE_NAME) FROM "+stSchema+"sys_tbllst lst WHERE lst.ID=" + id_tbl;
          cmd0.CommandText=sl1;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            AddLogString("A CellContent: Get TABLE_NAME = " + cmd0.CommandText + "," + ex1.Message);
          }

          if (! reader.IsClosed ) {
            if (reader.HasRows) {
              while (reader.Read())
              {
                TABLE_NAME = GetTypeValue(ref reader, 0);
                TABLE_NAME = TABLE_NAME.ToUpper() ;
                break ;
              } // while
            }
            reader.Close();
          }

          dp.Clear();

          // определяем поле
          //ERROR [42S22] [Oracle][ODBC][Ora]ORA-00904: "IS_EXDATA": недопустимый идентификатор
          int is_exdata = _checkCol( "is_exdata" , "meas_list" );

          if  (TABLE_NAME.Length>1) {

              // 3. получаем параметры+включенные на них архивы

              sl1="SELECT 0,0,0,0,0,0,0 FROM DUAL" ;

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
              }
              if (TABLE_NAME.IndexOf("EXDATA_LIST_V")>=0) {
                  sl1 = r.GetString("EXDATA_LIST_V");     ;
              }
              if (TABLE_NAME.IndexOf("DA_V_LST")>=0) {
                  sl1 = r.GetString("DA_V_LST");
                  sl1 = String.Format(sl1,TABLE_NAME);
              }

              cmd0.CommandText=sl1;
              try
              {
                reader = cmd0.ExecuteReader();
              }
              catch (Exception ex1)
              {
                AddLogString("A CellContent: Get List = " + cmd0.CommandText + " " + ex1.Message);
              }

              if (! reader.IsClosed ) {
                if (reader.HasRows) {

                    string[]  arr = new string[6];

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
                         if (ID_GINFO==_ginfo.ToString()) {
                            MEAS1 item1 = new MEAS1(_id1,_idn,_idt,arr[4],arr[1],_ginfo);
                            dp.Add(item1);
                         }
                       } else {
                         //SELECT ID, id_obj, id_meas_type, NAME, alias , ID_GINFO
                         int _id2 = Convert.ToInt32(arr[0]);
                         int _idn = Convert.ToInt32(arr[1]);
                         string _idt = arr[2].ToString();
                         int _ginfo = Convert.ToInt32(arr[5]);
                         if (ID_GINFO==_ginfo.ToString()  ) {
                             MEAS1 item1 = new MEAS1(_id2,_idn, _idt,arr[3],arr[4],_ginfo);
                             dp.Add(item1);
                         }
                      }

                    } // while

                }
                reader.Close();
              }
              dlCnt = dp.Count;
              AddLogString(" Calculating from " + TABLE_NAME + " number of archive = " + dlCnt );

          }
          dataGridViewA.Enabled = true ;

          DialogResult result = MessageBox.Show ("Деактивировать профиль?\n\n\n" +
          " id PROFILE = " + ID + ", '" + nm2 + "'\n" +
          " id GINFO   = " + ID_GINFO + ", '"+ nm1 + "'\n" +
          " id TBLLST  = " + id_tbl + "\n\n" +
          " Архив включен у " + dlCnt.ToString() + " параметров.\n\n" +
          " **Опция отключения архива у параметров : " + ((OptionTableDisable>0)?"ВКЛ":"ОТКЛ") ,
           "Деактивация..", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,
           MessageBoxDefaultButton.Button2);
          if (result == DialogResult.Yes)
          {
            // delete
            // Get 1\Name Schema 2\name arc table 3\ids params
            //-----------------------
            // Del from arc table - ids params + ID_GINFO
            // DELETE * FROM ?MEAS_ARC? WHERE ID_GINFO = ID_GINFO
            //-----------------------
            //ARC_SERVICES_TUNE
            //  ID_SPROFILE = ID
            //
            //ARC_SERVICES_ACCESS
            //  ID_SPROFILE=ID
            //
            // ARC_SUBSYST_PROFILE
            //    ID
            //----------------------

            // 4. отключаем архив у параметра ! если опция активирована
            if ( dlCnt>0 && OptionTableDisable>0 ) {
                sl1 = r.GetString("TABLE_ARC");
                sl1 = String.Format(sl1, id_tbl) ;
                cmd0.CommandText=sl1;
                try
                {
                  reader = cmd0.ExecuteReader();
                }
                catch (Exception ex1)
                {
                  AddLogString("DataGridViewACellContent Del выполнение алгоритма  - = " + cmd0.CommandText + " " + ex1.Message);
                }
                if (reader.HasRows) {
                  while (reader.Read())
                  {
                    TABLE_NAME = GetTypeValue(ref reader, 0);
                    break ;
                  } // while
                }
                reader.Close();

                AddLogString("CellContent Del from TABLE_NAME= " + TABLE_NAME );
                foreach (MEAS1 pv in dp)
                {
                   String ID_param = pv.ID.ToString();


                   string RETFNAME = "dual"  ;

                   sl1="SELECT RETFNAME FROM " + stSchema + TABLE_NAME +
                        " WHERE ID_PARAM=" + ID_param + " AND " + " ID_GINFO=" + ID_GINFO +" ;" ;

                   cmd0.CommandText=sl1;
                   try
                   {
                     reader = cmd0.ExecuteReader();
                   }
                   catch (Exception ex1)
                   {
                     AddLogString(" ---- " + ex1.Message);
                   }
                   if (!reader.IsClosed )
                    if (reader.HasRows) {
                     while (reader.Read())
                     {
                       RETFNAME = GetTypeValue(ref reader, 0).ToUpper() ;
                       break ;
                     } // while
                     reader.Close();
                    }


                   sl1="DELETE FROM " + stSchema + TABLE_NAME +
                       " WHERE ID_PARAM=" + ID_param + " AND " + " ID_GINFO=" + ID_GINFO +" ;" ;

                   AddLogString(" ---- " + sl1);

                   cmd0.CommandText=sl1;
                   int res1 = 0 ;
                   try
                   {
                     res1 = cmd0.ExecuteNonQuery();
                   }
                   catch (Exception ex1)
                   {
                     AddLogString(" ------ Регистрация архива для параметра " + ID + " не удалена , " + ex1.Message);
                     res1=-1;
                   }

                   if (res1>0) {
                     sl1=" DROP TABLE " + RETFNAME + " CASCADE CONSTRAINTS PURGE ;" ;
                     if (OptionTableDelete>0) {
                        cmd0.CommandText=sl1;
                        res1 = 0 ;
                        try
                        {
                           res1 = cmd0.ExecuteNonQuery();
                        }
                        catch (Exception ex1)
                        {
                           AddLogString(" -- " + sl1 + " -- " + ex1.Message);
                           res1=-1;
                        }
                        if (res1>0) {
                          AddLogString(" ---- DROP TABLE " + RETFNAME + " ; -- OK!");
                        }
                     } else {
                      AddLogString(" -- " + sl1 );
                     }
                   } //if (res1>0)

                } // foreach

            } //if dlCnt>0


            cmd0.CommandText="DELETE FROM "+stSchema+"ARC_SERVICES_TUNE WHERE ID_SPROFILE="+ID ;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception ex1)
            {
              AddLogString("DataGridViewACellContent DELETE - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
              return ;
            }
            reader.Close();

            cmd0.CommandText="DELETE FROM "+stSchema+"ARC_SERVICES_ACCESS WHERE ID_SPROFILE="+ID ;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception ex1)
            {
              AddLogString("DataGridViewACellContent DELETE - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
              return ;
            }
            reader.Close();

            cmd0.CommandText="DELETE FROM "+stSchema+"ARC_SUBSYST_PROFILE WHERE ID="+ID ;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception ex1)
            {
              AddLogString("DataGridViewACellContent DELETE - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
              return ;
            }
            reader.Close();

            AddLogString(" DELETE - profile - Ok = " + ID);

            Application.DoEvents();

            TreeViewAAfterSelect(treeViewA, null);

          }

         } else {

           /* создаем новый профиль архива  */

           DialogResult result = MessageBox.Show ("Создать новый профиль для id = " + ID_GINFO + " ?" ,"Активация профиля", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2 );
           if (result == DialogResult.Yes)
           {
              // new create
              ;
              //Получить Имя выделенного элемента
              string id_parent=treeViewA.SelectedNode.Name;
              if (id_parent=="0") return ;
              //string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
              AddLogString("new create=id_tbl=" + id_tbl);

              if (id_tbl=="0" || id_tbl=="") return ;

              DialogResult result1;
              Form ifrmC = new FormProfileCreate(this._conn, ID_GINFO, id_tbl, OptionSchemaName);
              result1=ifrmC.ShowDialog();
              ifrmC.Dispose();

              // (result1 == DialogResult.Yes)
              if (result1.ToString()=="OK")
              {
                AddLogString("new create = Ok");

                TreeViewAAfterSelect(treeViewA, null);
                //dataGridViewA.FirstDisplayedScrollingRowIndex=selRowNum;

                dataGridViewA.CurrentCell = dataGridViewA.Rows[selRowNum].Cells[0] ;
                //dataGridViewA.Rows[selRowNum].Selected=true ;
                DataGridViewASelectionChanged(dataGridViewA, null);

                //dataGridViewA.Update();
              }

           }

        } // if (isChecked)

      }

    }
    void DataGridViewACellClick(object sender, DataGridViewCellEventArgs e)
    {
      // работает в паре с DataGridViewACellContentClick
      // не дает поставить\удалить галочку - сначала получить подтверждение.
      int chkBoxColumnIndex = 0;
      var dataGridView = (DataGridView)sender;
      if (e.ColumnIndex == chkBoxColumnIndex)
      {
        dataGridView.EndEdit();
        //var isChecked = Convert.ToBoolean(dataGridView[e.ColumnIndex, e.RowIndex].Value);
        //if (isChecked)
        //{
        //  MessageBox.Show("1-3");
        //} else {
        //  MessageBox.Show("0-3");
        //}
      }

    }
    void TabControlAPSelectedIndexChanged(object sender, EventArgs e)
    {
       //
       TreeViewAAfterSelect(sender, null) ;
    }
    void GraphicsToolStripMenuItemClick(object sender, EventArgs e)
    {
       // вызов формы для построения графика через контекстное меню

       // string msg = String.Format("Row: {0}, Column: {1}",
       //   dataGridViewP.CurrentCell.RowIndex,
       //   dataGridViewP.CurrentCell.ColumnIndex);
       // MessageBox.Show(msg, "Current Cell");

       int selRowNum , selColNum ; // = dataGridViewP.CurrentCell.RowIndex;
       selRowNum = dataGridViewP.CurrentCell.RowIndex; //mouseLocation.RowIndex ;
       selColNum = dataGridViewP.CurrentCell.ColumnIndex ; //mouseLocation.ColumnIndex ;

       AddLogString(" GraphicsToolStripMenuItemClick -> selRowNum=" + selRowNum.ToString() + " selColNum=" + selColNum.ToString());

       if (selRowNum<0) return ;
       // group id name type {}
       if (selColNum<=3) return ;

       string ID = dataGridViewP.Rows[selRowNum].Cells[1].Value.ToString() ;
       string IDNAME = dataGridViewP.Rows[selRowNum].Cells[2].Value.ToString() ;
       string IDGINFO = dataGridViewP.Columns[selColNum].Name ;
       string NAMEHEADER = dataGridViewP.Columns[selColNum].HeaderText ;

       object obj1=dataGridViewP.Rows[selRowNum].Cells[selColNum].Value ;

       try
       {
         /*
         unchecked   true   - включение
         checked false   - выключение
         false  true  - включение
         true false - выключение
         */

         // если ячейка Unchecked - покидаем алгоритм
         if (  obj1.ToString()=="Unchecked" ) { return ; }
         if (  obj1.ToString()=="False" ) { return ; }
       }
       catch (Exception ex1)
       {
         AddLogString("GraphicsToolStripMenuItemClick - прервано.. = " + ex1.Message);
         return ;
       }


       //Получить Имя выделенного элемента
       string id_parent=treeViewA.SelectedNode.Name;
       if (id_parent=="0") return ;
       string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
       //AddLogString("GraphicsTool=id_tbl=" + id_tbl);

       if (id_tbl=="0" || id_tbl=="") return ;

       dataGridViewP.Rows[selRowNum].Cells[selColNum].Style.ForeColor  = Color.Cyan ;

       //DialogResult result1;
       Form ifrgr = new FormGr(_conn, ID, IDNAME, IDGINFO, NAMEHEADER, id_tbl, OptionSchemaName);
       //ifrgr.StartPosition=Start​Position.CenterParent;
       ifrgr.Show();

    }

    void DataGridViewASorted(object sender, EventArgs e)
    {
      // После сортировки, заново проставляем галочки у нужных архивов.

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd1 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd1.Connection=this._conn;

      //Получить Имя выделенного элемента
      string _id_tbl = Convert.ToString ( treeViewA.SelectedNode.Tag ) ;

      if (dataGridViewA.RowCount<=0) return ;

      for (int ii = 0; ii < dataGridViewA.RowCount ; ii++) {
        dataGridViewA.Rows[ii].HeaderCell.Value = (ii + 1).ToString(); // нумерация строк
        dataGridViewA.Rows[ii].Cells[0].Value = false ;
      }

      Application.DoEvents();

      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

      cmd1.CommandText=r.GetString("ARC_SUBSYST_PROFILE_ALL");
      try
      {
        reader = cmd1.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString("DataGridViewASorted - break ! " + cmd1.CommandText + ", " + ex1.Message);
        //reader.Close();
        return ;
      }

      if (reader.HasRows==false) {
        reader.Close();
        return;
      }

      string[] arr = new string[7];

      while (reader.Read())
      {
        for ( int j = 0; j<3; j++)
        {
          arr[j]=GetTypeValue(ref reader, j) ;
        }

        for (int ii = 0; ii < dataGridViewA.RowCount ; ii++)
          if (dataGridViewA.Rows[ii].Cells[1].Value.ToString()==arr[2]) { // id = id_ginfo
            if (arr[1]==_id_tbl) {
               dataGridViewA.Rows[ii].Cells[0].Value =true ;
               dataGridViewA.Rows[ii].HeaderCell.Style.BackColor = Color.LightGreen ;
               dataGridViewA.Rows[ii].DefaultCellStyle.BackColor = Color.LightGreen ;
            } else {
            if (dataGridViewA.Rows[ii].HeaderCell.Style.BackColor != Color.LightGreen )
                  dataGridViewA.Rows[ii].HeaderCell.Style.BackColor = Color.PeachPuff ;
            if (dataGridViewA.Rows[ii].DefaultCellStyle.BackColor != Color.LightGreen )
                  dataGridViewA.Rows[ii].DefaultCellStyle.BackColor = Color.PeachPuff ;
            }
          }

      } // while
      reader.Close();

    }


    void DataGridViewPCurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      var dataGridView = (DataGridView)sender;
      if (dataGridView.IsCurrentCellDirty && dataGridView.CurrentCell is DataGridViewCheckBoxCell)
      {
          dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }
    void DataGridViewPCellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
       if (PCellValueChanged <= 0) return;
    }
    void DataGridViewPCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        // записывает в структуру местоположения курсора
        mouseLocation = e;
    }
    void DataGridViewPCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
    {
       // End of edition on each click on column of checkbox
       if (e.ColumnIndex >=4 && e.RowIndex != -1)
       {
         dataGridViewP.EndEdit();
       }
    }

    void DataGridViewPCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
       // End of edition on each click on column of checkbox
       if (e.ColumnIndex >=4 && e.RowIndex != -1)
       {
         dataGridViewP.EndEdit();
       }

    }

    void DataGridViewPCellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        //
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;
        cmd0.Connection=this._conn;

        var dataGridView = (DataGridView)sender;

        DataGridViewCell currentCell = dataGridView.CurrentCell;

        //-----------------------
        if (currentCell.RowIndex>=0 && currentCell.ColumnIndex<=3) {
          return ;
        }
        //-----------------------

        DataGridViewCheckBoxCell checkCell =
           (DataGridViewCheckBoxCell)dataGridView.CurrentCell;
        DataGridViewColumn OwnCol = dataGridView.CurrentCell.OwningColumn ;
        object obj1 = checkCell.Value;
        object obj2 = checkCell.EditedFormattedValue ;

        // AddLogString("Выкл|вкл архив = before status =" + obj1.ToString());

        // если пытаемся выключить - обьязательный архив - Checked or True
        if (obj1.ToString()=="Unchecked" || obj1.ToString()=="False") {
            if (OwnCol.DefaultCellStyle.BackColor==Color.LightGray) {
              if (OptionWriteOnDelete == 0) {
                  //checkCell.EditingCellFormattedValue = true ;
                  checkCell.Value = true ;
                  dataGridView.CurrentCell = checkCell;
                  dataGridView.EndEdit();
                  dataGridView.Invalidate();
                  return ;
              }
            }
        }

        /*
        unchecked   true   - включение
        checked false   - выключение
        false  true  - включение
        true false - выключение
        */

        // получаем ячейку
        int selRowNum , selColNum ;
        selRowNum = currentCell.RowIndex ;
        selColNum = currentCell.ColumnIndex ;

        if (selRowNum>=0 && selColNum>3) {
            if (Convert.ToBoolean(obj2)==false) {
              //AddLogString("Выкл|вкл архив = delete = " + obj2.ToString());
              int ret1 = ArcDel( sender, selRowNum , selColNum) ;
              if (ret1!=0) { checkCell.Value = true ; /*checkCell.EditingCellFormattedValue=true; */}
              //AddLogString( "Выкл|вкл удаления завершен." );
            } else {
                 int flag = 0 ; // флаг для алгоритма

                 string IDGINFO = dataGridView.Columns[selColNum].Name ;

                 string stName = "" ;
                 if (OptionSchemaName>0) {
                    stName=OptionSchemaMain + "." ;
                 }

                 cmd0.CommandText="select sg.DEFINE_ALIAS from "+stName+"arc_ginfo ag, "+stName+"sys_gtopt sg" +
                                  " where ag.ID_GTOPT=sg.ID and ag.id=" + IDGINFO ;
                 try
                 {
                   reader = cmd0.ExecuteReader();
                 }
                 catch (Exception ex1)
                 {
                   ;
                 }

                 string DEFINE_ALIAS="";
                 if (!reader.IsClosed )
                  if (reader.HasRows) {
                   while (reader.Read())
                   {
                     DEFINE_ALIAS = GetTypeValue(ref reader, 0).ToUpper() ;
                     break ;
                   } // while
                   reader.Close();
                  }

                 int bool_flag = 0 ;
                 if (DEFINE_ALIAS.IndexOf("BOOL")>=0) {
                   bool_flag = 1 ;
                 }

                 string typestr = "";
                 object typeobj=dataGridView.Rows[selRowNum].Cells[3].Value ; // тип в 3-ей колонке
                 try
                 {
                   typestr=typeobj.ToString();
                 }
                 catch (Exception ex1)
                 {
                   ;
                 }

                 flag = 1 ;
                 if ( typestr=="ТС" || typestr=="ТУ" ) {
                   if (bool_flag == 0) flag = 0 ;
                 }
                 if ( typestr=="ТИ" ) {
                   if (bool_flag == 1) flag = 0 ;
                 }

                 if ( flag == 0 ) {
                    checkCell.Value = false ;
                 } else {
                   //AddLogString("Выкл|вкл архив = turn on = " + obj2.ToString());
                   int ret2 = ArcAdd( sender, selRowNum , selColNum) ;
                   if (ret2!=0) { checkCell.Value = false ; /*checkCell.EditingCellFormattedValue=false;*/ }
                   //AddLogString( "Выкл|вкл добавления завершен." );
                 }

                reader = null ;
                cmd0.Dispose();

              }

            dataGridView.CurrentCell = checkCell;
            dataGridView.Invalidate();


        }

    }

/*
 процедура создания у параметра необходимого архива
*/
int ArcAdd(object sender, int selRowNum , int selColNum)
{
   var dataGridView = (DataGridView)sender;

   // Объект для выполнения запросов к базе данных
   OdbcCommand cmd0 = new OdbcCommand();
   OdbcCommand cmd1 = new OdbcCommand();
   OdbcDataReader reader = null ;
   cmd0.Connection=this._conn;
   cmd1.Connection=this._conn;

   string vRetVal = "";
   string retname = "" ;
   string sname = "" ;

   string ID = dataGridView.Rows[selRowNum].Cells[1].Value.ToString() ;
   string IDGINFO = dataGridView.Columns[selColNum].Name ;

   //Получить Имя выделенного элемента
   string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
   // AddLogString("ArcAdd =id_tbl=" + id_tbl);

   ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

   string stSchema="";
   if (OptionSchemaName>0) {
      stSchema=OptionSchemaMain + "." ;
   }

   string ARC_NAME = "" ; // 2. получаем название архивной таблицы
   string TABLE_NAME = "" ;
   string SCHEMA_NAME = "" ; // 1. получаем имя схемы

   // get table списка параметров для данного раздела
   string sl1 = r.GetString("ARH_SYSTBLLST2");
   sl1 = String.Format(sl1,id_tbl);
   cmd0.CommandText=sl1;
   try
   {
      reader = cmd0.ExecuteReader();
   }
   catch (Exception ex1)
   {
     return (-2);
   }
   if (reader.HasRows) {
      while (reader.Read())
      {
          ARC_NAME = GetTypeValue(ref reader, 2).ToUpper() ;
          TABLE_NAME = GetTypeValue(ref reader, 1).ToUpper() ;
          SCHEMA_NAME = GetTypeValue(ref reader, 3).ToUpper() ;
          break ;
       } // while
   }
   reader.Close();

   AddLogString("ArcAdd TABLE_NAME="+ TABLE_NAME+ " ARC_NAME=" + ARC_NAME + " SHEMA_NAME=" + SCHEMA_NAME);

   // 5. устанавливаем роли
   cmd0.CommandType = System.Data.CommandType.StoredProcedure;
   cmd0.Parameters.Clear();
   cmd0.CommandText="SET ROLE BASE_EXT_CONNECT_OIK , ARC_STAND_ADJ";
   try
   {
      cmd0.ExecuteNonQuery();
      AddLogString("ArcAdd Установка роли = Ok -> " + cmd0.CommandText);
   }
   catch (Exception ex7)
   {
      AddLogString("ArcAdd Ошибка установки Ролей ="+ex7.Message);
   }


   // 3. проверяем что в таблице архивов есть колонка ID_GTOPT
   int exists_idgopt=_checkCol( "ID_GTOPT" , ARC_NAME );
   string idgopt = "";

   // 4. если колонка ID_GTOPT в табл архивов есть - запрашиваем значение из ARC_GINFO
   if (exists_idgopt==1) {
      sl1 = r.GetString("ARC_GINFO2");
      sl1 = String.Format(sl1, IDGINFO) ;
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString("ArcAdd break .. = " + cmd0.CommandText + " " + ex1.Message);
        return(-3) ;
      }
      if (reader.HasRows==false) {
        AddLogString("ArcAdd ID_GTOPT =" + idgopt + " не обнаружена" );
        reader.Close();
        return(-4) ;
      }
      while (reader.Read())
      {
        idgopt = GetTypeValue(ref reader, 0);
        break ;
      } // while
      reader.Close();
   }

   cmd0.CommandType = System.Data.CommandType.StoredProcedure;
   cmd0.Parameters.Clear();

//RSDU2AUARH.au_arh_pkg=AU_ARH_PKG   RSDU2AUARH.au_util_pkg=AU_UTIL_PKG
//RSDU2DAARH.arc_arh_pkg=DA_ARH_PKG
//RSDU2DGARH.arc_arh_pkg=DG_ARH_PKG
//RSDU2EAARH.arc_arh_pkg=EA_ARH_PKG
//RSDU2ELARH.arc_arh_pkg=ELREG_ARH_PKG
//RSDU2PHARH.arc_arh_pkg=PHREG_ARH_PKG
//RSDU2PSARH.arc_arh_pkg=PS_ARH_PKG
//RSDU2EXARH.arc_arh_pkg=EX_ARH_PKG
//RSDU2CLARH.calc_arh_pkg=CL_ARH_PKG

   switch (SCHEMA_NAME)
   {
       case "RSDU2AUARH":
           sl1 = SCHEMA_NAME+".au_arh_pkg.create_arh(?,?,?,?)" ;
           break;
       //case "RSDU2CLARH":
       //    sl1 = SCHEMA_NAME+".calc_arh_pkg.create_arh(?,?,?,?)" ;
       //    break;
       default:
           sl1 = SCHEMA_NAME+".arc_arh_pkg.create_arh(?,?,?,?)" ;
           break;
   }

   //AddLogString("ArcAdd Вызов процедуры " + sl1);

   // i:=RSDU2ELARH.arc_arh_pkg.create_arh (parnum,  gtype,retname, sname);
   cmd0.CommandText = "{ ? = call "+sl1+" }";

   OdbcParameter parOut = new OdbcParameter();
   parOut.Direction = System.Data.ParameterDirection.ReturnValue;
   parOut.OdbcType = OdbcType.Decimal;
   parOut.ParameterName = "i";

   cmd0.Parameters.Add(parOut);

   OdbcParameter param1 = new OdbcParameter();
   param1.Direction = System.Data.ParameterDirection.Input;
   param1.OdbcType = OdbcType.Decimal;
   param1.ParameterName = "parnum";
   param1.Value = Convert.ToDecimal(ID);

   cmd0.Parameters.Add(param1);

   OdbcParameter param2 = new OdbcParameter();
   param2.Direction = System.Data.ParameterDirection.Input;
   param2.OdbcType = OdbcType.Decimal;
   param2.ParameterName = "gtype";
   param2.Value = Convert.ToDecimal(IDGINFO);

   cmd0.Parameters.Add(param2);

   OdbcParameter param3 = new OdbcParameter();
   param3.Direction = System.Data.ParameterDirection.Output;
   param3.OdbcType = OdbcType.NText;
   param3.ParameterName = "retname";
   param3.Size = 64 ;

   cmd0.Parameters.Add(param3);

   OdbcParameter param4 = new OdbcParameter();
   param4.Direction = System.Data.ParameterDirection.InputOutput;
   param4.OdbcType = OdbcType.NText;
   param4.ParameterName = "sname";
   param4.Value = "";
   param4.Size = 2048;

   cmd0.Parameters.Add(param4);

   cmd0.CommandTimeout = OptionTableConntime; // 30..90
   AddLogString("ArcAdd OptionTableConntime = " + OptionTableConntime.ToString() );

   try
   {
      cmd0.ExecuteNonQuery();
      vRetVal = cmd0.Parameters["i"].Value.ToString() ;
      retname = cmd0.Parameters["retname"].Value.ToString() ;
      sname = cmd0.Parameters["sname"].Value.ToString() ;
   }
   catch (Exception ex8)
   {
      AddLogString("ArcAdd Не удалось вызвать процедуру arc_arh_pkg.create_arh");
      AddLogString("ArcAdd Ошибка вызова процедуры ="+ex8.Message);
      //MessageBox.Show("Не удалось вызвать процедуру arc_arh_pkg.create_arh","arc_arh_pkg.create_arh","ArcAdd",MessageBoxButtons.OK, MessageBoxIcon.Error);
      return(-5) ;
   }

   cmd0.Dispose();

/*
--   ПЕРЕЧЕНЬ ВОЗВРАЩАЕМЫХ ОШИБОК:
--            Прим.: еще всегда возвращается строка расшифровки!
--   функция create_arh
--           0 - все хорошо
--           16384  - НЕ задан номер параметра
--            < 0   - проч ошибка ORACLE
*/


   if (vRetVal!="0") {
     AddLogString("ArcAdd Ошибка при работе процедуры (arc_arh_pkg.create_arh) ="+vRetVal+"  sname="+sname );
     return(-6) ;
   }
   AddLogString( "ArcAdd retname=" + retname + "; vRetVal="+vRetVal );

   if (vRetVal=="0") {
     // проверяем , есть ли в таблице ARC_NAME уже запись о регистрации архива
     sl1="SELECT COUNT(*) FROM " + stSchema + ARC_NAME + " WHERE " +
         "ID_PARAM="+ID+" AND RETFNAME='"+retname+"' AND ID_GINFO="+IDGINFO ;
     if (exists_idgopt==1) {
       sl1=sl1+" AND ID_GTOPT="+idgopt ;
     }
     sl1=sl1 + " ;" ;

     object rec1 = null ;

     cmd1.CommandText=sl1;
     try
     {
      rec1 = cmd1.ExecuteScalar();
     }
     catch (Exception ex1)
     {
       AddLogString("ArcAdd - " + cmd1.CommandText + " " + ex1.Message);
     }

     int crec1=Convert.ToInt32(rec1) ;
     AddLogString("ArcAdd record count =" + crec1.ToString() );

     // записей нет, вставляем
     if (crec1==0) {

         if (exists_idgopt==1) {
           sl1="INSERT INTO " + stSchema + ARC_NAME + " (ID_PARAM, ID_GTOPT, RETFNAME, ID_GINFO) VALUES " +
               " (" + ID + "," + idgopt + ",'" + retname + "'," + IDGINFO +");" ;
         } else {
           sl1="INSERT INTO " + stSchema + ARC_NAME + " (ID_PARAM, RETFNAME, ID_GINFO) VALUES " +
               " (" + ID +  ",'" + retname + "'," + IDGINFO +");" ;
         }

         crec1 = 0;
         cmd1.CommandText=sl1;
         try
         {
          crec1=cmd1.ExecuteNonQuery();
         }
         catch (Exception ex1)
         {
           AddLogString("ArcAdd регистрация отменена, " + cmd1.CommandText + ", " + ex1.Message);
           return(-7) ;
         }
         AddLogString("ArcAdd регистрация архива " + retname + " = " + crec1.ToString() );

     }

    rec1 = null ;
    cmd1.Dispose();

   }

   return(0);
}



/*
процедура удаления регистрации из таблицы архивов необходимой записи для данного параметра
*/
int ArcDel(object sender, int selRowNum , int selColNum)
{
    var dataGridView = (DataGridView)sender;

    // Объект для выполнения запросов к базе данных
    OdbcCommand cmd0 = new OdbcCommand();
    OdbcCommand cmd1 = new OdbcCommand();
    OdbcDataReader reader = null ;
    cmd0.Connection=this._conn;
    cmd1.Connection=this._conn;

    string vRetVal = "";
    string retname = "" ;
    string sname = "" ;

    string ID = dataGridView.Rows[selRowNum].Cells[1].Value.ToString() ;
    string IDGINFO = dataGridView.Columns[selColNum].Name ;

    //Получить Имя выделенного элемента
    string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
    //AddLogString("ArcDel =id_tbl=" + id_tbl);

    ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());


    string stSchema="";
    if (OptionSchemaName>0) {
      stSchema=OptionSchemaMain + "." ;
    }

    string ARC_NAME = "" ; // 2. получаем название архивной таблицы
    string TABLE_NAME = "" ;
    string SCHEMA_NAME = "" ; // 1. получаем имя схемы

    // get table списка параметров для данного раздела
    string sl1 = r.GetString("ARH_SYSTBLLST2");
    sl1 = String.Format(sl1,id_tbl);
    cmd0.CommandText=sl1;
    try
    {
       reader = cmd0.ExecuteReader();
    }
    catch (Exception ex1)
    {
      return (-2);
    }
    if (reader.HasRows) {
       while (reader.Read())
       {
           ARC_NAME = GetTypeValue(ref reader, 2).ToUpper() ;
           TABLE_NAME = GetTypeValue(ref reader, 1).ToUpper() ;
           SCHEMA_NAME = GetTypeValue(ref reader, 3).ToUpper() ;
           break ;
        } // while
    }
    reader.Close();

    AddLogString("ArcDel TABLE_NAME="+ TABLE_NAME+ " ARC_NAME=" + ARC_NAME + " SHEMA_NAME=" + SCHEMA_NAME);


    // 5. устанавливаем роли
    cmd0.CommandType = System.Data.CommandType.StoredProcedure;
    cmd0.Parameters.Clear();
    cmd0.CommandText="SET ROLE BASE_EXT_CONNECT_OIK , ARC_STAND_ADJ";
    try
    {
       cmd0.ExecuteNonQuery();
       AddLogString("ArcDel Установка роли = Ok -> " + cmd0.CommandText);
    }
    catch (Exception ex7)
    {
       AddLogString("ArcDel Ошибка установки Ролей ="+ex7.Message);
    }


    if (OptionFullDelete == 1) {

      sl1 = SCHEMA_NAME+".arc_arh_pkg.drop_arh(?,?)" ;
      cmd0.CommandText = "{ ? = call "+sl1+" }";

      cmd0.CommandType = System.Data.CommandType.StoredProcedure;
      cmd0.Parameters.Clear();

      OdbcParameter parOut = new OdbcParameter();
      parOut.Direction = System.Data.ParameterDirection.ReturnValue;
      parOut.OdbcType = OdbcType.Decimal;
      parOut.ParameterName = "i";

      cmd0.Parameters.Add(parOut);

      OdbcParameter param1 = new OdbcParameter();
      param1.Direction = System.Data.ParameterDirection.Input;
      param1.OdbcType = OdbcType.Decimal;
      param1.ParameterName = "parnum";
      param1.Value = Convert.ToDecimal(ID);

      cmd0.Parameters.Add(param1);

      OdbcParameter param4 = new OdbcParameter();
      param4.Direction = System.Data.ParameterDirection.InputOutput;
      param4.OdbcType = OdbcType.NText;
      param4.ParameterName = "sname";
      param4.Value = "";
      param4.Size = 2048;

      cmd0.Parameters.Add(param4);

      AddLogString("ArcDel Вызов процедуры arc_arh_pkg.drop_arh (parnum,sname) ..");

      cmd0.CommandTimeout = OptionTableConntime; // 30..90

      AddLogString("ArcDel CommandTimeout =  " + OptionTableConntime.ToString() );

      try
      {
         cmd0.ExecuteNonQuery();
         vRetVal = cmd0.Parameters["i"].Value.ToString() ;
         retname = cmd0.Parameters["parnum"].Value.ToString() ;
         sname = cmd0.Parameters["sname"].Value.ToString() ;
      }
      catch (Exception ex8)
      {
         AddLogString("ArcDel Не удалось вызвать процедуру arc_arh_pkg.drop_arh");
         AddLogString("ArcDel Ошибка вызова процедуры ="+ex8.Message);
      }
      AddLogString("ArcDel vRetVal =" + vRetVal + " sname=" + sname);

    }

  reader = null ;
    cmd0.Dispose();

/*
--   функция drop_arh
--           0 - все хорошо
--           1 - не найдены арх.таблица параметра - неправ. задано имя табл списка
--            < 0   - проч ошибка ORACLE
*/
    vRetVal="0";

    if (vRetVal=="0") {

      string RETFNAME = "dual"  ;

      sl1="SELECT RETFNAME FROM " + stSchema + ARC_NAME +
           " WHERE ID_PARAM=" + ID + " AND " + " ID_GINFO=" + IDGINFO +" ;" ;

      cmd1.CommandText=sl1;
      try
      {
        reader = cmd1.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" ---- " + ex1.Message);
      }
      if (!reader.IsClosed )
       if (reader.HasRows) {
        while (reader.Read())
        {
          RETFNAME = GetTypeValue(ref reader, 0).ToUpper() ;
          break ;
        } // while
        reader.Close();
       }


      sl1="DELETE FROM " + stSchema + ARC_NAME +
          " WHERE ID_PARAM=" + ID + " AND " + " ID_GINFO=" + IDGINFO +" ;" ;
      cmd1.CommandText=sl1;
      int res1 = 0 ;
      try
      {
        res1 = cmd1.ExecuteNonQuery();
      }
      catch (Exception ex1)
      {
        AddLogString(" ------ Регистрация архива для параметра " + ID + " не удалена , " + ex1.Message);
        res1=-1;
      }

      if (res1<=0) { return(-7); }

      if (res1>0) {
        sl1=" DROP TABLE " + SCHEMA_NAME + "." + RETFNAME + " CASCADE CONSTRAINTS PURGE ;" ;
        AddLogString(" -- " + sl1 );
        if (OptionTableDelete>0) {
           cmd1.CommandText=sl1;
           res1 = 0 ;
           try
           {
              res1 = cmd1.ExecuteNonQuery();
           }
           catch (Exception ex1)
           {
              AddLogString(" -- " + sl1 + " -- " + ex1.Message);
              res1=0;
           }
        }

      } //if (res1>0)

      reader = null ;
      cmd1.Dispose();

    } // if (vRetVal=="0")

    return(0);
}

    void ToolStripButton3Click(object sender, EventArgs e)
    {
      // Output Stat to Logs
      toolStripStatusLabel2.Text = "Output Stat to Logs - Start";
      Action th = new Action( OracleStat ) ;
      Task tsk = new Task(th);
      tsk.Start();
      tabControl1.SelectedIndex = 5; // tabPage5
      MessageBox.Show(" Задача вывода в лог Статистики по БД - запущена...",toolStripStatusLabel2.Text,MessageBoxButtons.OK , MessageBoxIcon.Information);
    }


/*
 процедура получения данных из Бд и вывод результатов в лог
*/
    string ReadData(string st )
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;
      cmd0.Connection=this._conn;
      string strQry = "";

      ResourceManager ro = new ResourceManager("ArcConfig.StatOracle", Assembly.GetExecutingAssembly());

      string tmpName = ro.GetString(st);
      if (tmpName == null || tmpName.Trim().Length == 0)
      {
         AddLogString("ReadData = variable '" + st + "' not set" );
         return (strQry);
      }

      cmd0.CommandText=tmpName;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString("ReadData =" + cmd0.CommandText + "=" + ex1.Message);
        return (strQry);
      }

      while (reader.Read())
      {
         strQry = ";" ;
         for ( int i = 0; i<reader.FieldCount; i++)
         {
           strQry+=GetTypeValue(ref reader, i) +  ";";
         }
         if (reader.FieldCount>1) AddLogStringNoTime(strQry);
      } // while
      reader.Close();

      return (strQry);
    }


    string ReadDataVal(string st )
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;
      cmd0.Connection=this._conn;
      string strQry = "0";

      ResourceManager ro = new ResourceManager("ArcConfig.StatOracle", Assembly.GetExecutingAssembly());

      strQry = ro.GetString(st);
      if (strQry == null || strQry.Trim().Length == 0)
      {
         AddLogString("ReadDataVal = variable '" + st + "' not set" );
         return ("0");
      }

      cmd0.CommandText=strQry;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString("ReadDataVal =" + strQry + "=" + ex1.Message);
        return ("0");
      }

      while (reader.Read())
      {
         strQry = "" ;
         for ( int i = 0; i<reader.FieldCount; i++)
         {
           strQry+=GetTypeValue(ref reader, i) +  "";
         }
         if (reader.FieldCount>1) AddLogStringNoTime(strQry);
      } // while
      reader.Close();

      if (strQry == "") { strQry = "0" ; }

      return (strQry);
    }


/*
 процедура получения данных из Бд и вывод результатов в Grid
*/
    string ReadDataDB(string st )
    {
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      dataGridViewDB.DataSource = null;
      dataGridViewDB.Rows.Clear();
      dataGridViewDB.Columns.Clear();
      dataGridViewDB.Refresh();

      ResourceManager ro = new ResourceManager("ArcConfig.StatOracle", Assembly.GetExecutingAssembly());

      string strQry = ro.GetString(st);
      if (strQry == null || strQry.Trim().Length == 0)
      {
        strQry="";
        return (strQry);
      }

      cmd0.CommandText=strQry;

      Application.DoEvents();

      // Указываем запрос для выполнения
      adapter.SelectCommand = cmd0;
      // Заполняем объект источника данных
      DataTable dataTable = new DataTable();
      try
      {
         adapter.Fill(dataTable);
      }
      catch (Exception ex1)
      {
         return (strQry);
      }

      try
      {
         dataGridViewDB.DataSource = dataTable;
         //dataGridViewDB.DataSource = dataTable.DefaultView;
         // //dataGridViewDB.DataSource = ds.Tables[0];
      }
      catch (Exception ex1)
      {
         return (strQry);
      }

      Application.DoEvents();

      // To refresh data
      // dataGridViewDB.Refresh();

      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridViewDB.AutoResizeColumns();

      // Configure the details DataGridView so that its columns automatically
      // adjust their widths when the data changes.
      dataGridViewDB.AutoSizeColumnsMode =
             DataGridViewAutoSizeColumnsMode.AllCells;
      dataGridViewDB.AutoGenerateColumns = true;

      //dataGridViewDB.EnableHeadersVisualStyles = false;
      //dataGridViewDB.AlternatingRowsDefaultCellStyle.BackColor =Color.LightGray;

      dataGridViewDB.ReadOnly = true;

      dataGridViewDB.Update();

      return (strQry);
    }


void OracleStat ( )
{
    //=========================================================
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">> RSDU <<");
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(";Число параметров:;;");
    AddLogStringNoTime(";  Параметры Сбора = " + ReadData( "DA_COLUMN_DATA_V" ));
    AddLogStringNoTime(";  Электрические параметры = " + ReadData( "elreg_list_v" ));
    AddLogStringNoTime(";  Прочие параметры = " + ReadData( "phreg_list_v" ));
    AddLogStringNoTime(";  Коммутационные аппараты = " + ReadData( "pswt_list_v" ));
    AddLogStringNoTime(";  Дополнительные параметры = " + ReadData( "EXDATA_LIST_V" ));
    AddLogStringNoTime(";  Диспетчерские графики = " + ReadData( "DG_LIST" ));
    AddLogStringNoTime(";  Количество отчетов = " + ReadData( "RPT_LST_V" ));
    AddLogStringNoTime(";  Количество отчетов веб-сервера = " + ReadData( "RPT_LST_WWW_V" ));
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>RSDU_UPDATE");
    ReadData( "RSDU_UPDATE" );
    AddLogStringNoTime("------------------------");
    //AddLogStringNoTime(">>dba_tab_privs-RSDUADMIN");
    //ReadData( "dba_tab_privs" ) ;
    //AddLogStringNoTime(">>Users Serv");
    //ReadData( "UsersServ" ) ;

    //=========================================================
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">> Instance <<");
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>Status");
    ReadData( "Status0" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>dbtimezone");
    ReadData( "dbtimezone" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>v$version");
    ReadData( "version" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>Размер и свободное место");
    ReadData( "datafile1" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>tablespace size");
    ReadData( "tablespace size" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>gv$database");
    ReadData( "database" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>gv$asm_diskgroup");
    ReadData( "asm_diskgroup" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>dba_directories - Oracle-directories: владелец, имя, путь в файловой системе ОС");
    ReadData( "dba_directories" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>dba_db_links - database links: владелец, имя линка, имя схемы....");
    ReadData( "dba_db_links" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>v$resource_limit");
    ReadData( "resource_limit" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>v$parameter");
    ReadData( "parameter" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>nls_session_parameters");
    ReadData( "nls_session_parameters" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>Процент использования FRA:");
    ReadData( "RECOVERY_FILE_DEST" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>UNDO");
    ReadData( "UNDO" ) ;
    AddLogStringNoTime("------------------------");
    //=========================================================

    AddLogStringNoTime(">>v$sga_dynamic_components");
    ReadData( "sga_dynamic_components" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>v$pgastat");
    ReadData( "pgastat" ) ;
    AddLogStringNoTime("------------------------");
    //=========================================================

    AddLogStringNoTime(">>v$locked_object");
    ReadData( "locked_object" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>dba_recyclebin");
    ReadData( "dba_recyclebin" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>v$session");
    ReadData( "session1" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>gv$session");
    ReadData( "session2" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>v$process");
    ReadData( "process" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>Количество открытых курсоров = "+ ReadData( "open_cursor" )) ;
    AddLogStringNoTime(">>v$open_cursor");
    ReadData( "open_cursor1" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>opened cursors current");
    ReadData( "openedcursorscurrent" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>v$sysstat");
    ReadData( "sysstat" ) ;
    AddLogStringNoTime("------------------------");
    AddLogStringNoTime(">>Сессии - CPU used by this session");
    ReadData( "GPU" ) ;
    AddLogStringNoTime("------------------------");
    //=========================================================

    toolStripStatusLabel2.Text = "Output Stat to Logs - End";

    return ;
}

    void DeleteDataToolStripMenuItemClick(object sender, EventArgs e)
    {
       // stat\delete data media

       // получаем ячейку
       int selRowNum ;
       selRowNum = dataGridViewA.CurrentCell.RowIndex; //mouseLocation.RowIndex ;

       if (selRowNum<0) return ;

       object obj1 = dataGridViewA.Rows[selRowNum].Cells[0].Value ;

       //DataGridViewCheckBoxColumn

       DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridViewA.Rows[selRowNum].Cells[0] ;

       //AddLogString("chk.Value="+chk.Value.ToString() );

       // если ячейка Unchecked - покидаем алгоритм
       if (Convert.ToBoolean(chk.Value) == false || chk.Value == null)
       {
          return ;
       }

       //Получить Имя выделенного элемента
       string id_parent=treeViewA.SelectedNode.Name;
       if (id_parent=="0") return ;

       string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
       if (id_tbl=="0" || id_tbl=="") return ;

       string ID = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;
       string IDNAME = dataGridViewA.Rows[selRowNum].Cells[2].Value.ToString() ;
       string IDGTOPT = dataGridViewA.Rows[selRowNum].Cells[3].Value.ToString() ;
       string NAMEHEADER = dataGridViewA.Rows[selRowNum].Cells[16].Value.ToString() ;

       //DialogResult result1;
       //
       Form ifdel = new FormDel(_conn, ID, IDNAME, IDGTOPT, NAMEHEADER, id_tbl,OptionFullName, OptionSchemaName, OptionSaveFormat);
       ifdel.StartPosition=FormStartPosition.CenterParent ;
       ifdel.Show();
    }
    void ToolStripButton4Click(object sender, EventArgs e)
    {
       //  процедура чтения и установки настроек

       FormOption ofrm = new FormOption();

       ofrm._OptionUpdate = OptionUpdate.ToString() ;
       if (OptionFullDelete==0) ofrm._OptionFullDelete = false ;
       else  ofrm._OptionFullDelete = true ;
       if (OptionWriteOnDelete==0) ofrm._OptionWriteOnDelete = false ;
       else  ofrm._OptionWriteOnDelete = true ;
       if (OptionFullName==0) ofrm._OptionFullName = false ;
       else  ofrm._OptionFullName = true ;
       if (OptionCheckData==0) ofrm._OptionCheckData = false ;
       else  ofrm._OptionCheckData = true ;
       if (OptionSchemaName==0) ofrm._OptionSchemaName = false ;
       else  ofrm._OptionSchemaName = true ;
       if (OptionTableDelete==0) ofrm._OptionTableDelete = false ;
       else  ofrm._OptionTableDelete = true ;
       if (OptionTableDisable==0) ofrm._OptionTableDisable = false ;
       else  ofrm._OptionTableDisable = true ;
       ofrm._OptionTableConntime = OptionTableConntime.ToString() ;
       if (OptionSaveFormat==0) ofrm._OptionSaveFormat = false ;
       else  ofrm._OptionSaveFormat = true ;

       ofrm.ShowDialog();

       int val1 = 10 ;
       if ( int.TryParse(ofrm._OptionUpdate, out val1 ) )
           OptionUpdate = val1 ;
       else OptionUpdate = 10 ;

       OptionFullDelete=0 ;
       if (ofrm._OptionFullDelete) OptionFullDelete=1 ;
       OptionWriteOnDelete=0 ;
       if (ofrm._OptionWriteOnDelete) OptionWriteOnDelete=1 ;
       OptionFullName=0 ;
       if (ofrm._OptionFullName) OptionFullName=1 ;
       OptionCheckData=0 ;
       if (ofrm._OptionCheckData) OptionCheckData=1 ;
       OptionSchemaName=0 ;
       if (ofrm._OptionSchemaName) OptionSchemaName=1 ;
       OptionTableDelete=0 ;
       if (ofrm._OptionTableDelete) OptionTableDelete=1 ;
       OptionTableDisable=0 ;
       if (ofrm._OptionTableDisable) OptionTableDisable=1 ;
       int val0 = 30 ;
       if ( int.TryParse(ofrm._OptionTableConntime, out val0 ) )
           OptionTableConntime = val0 ;
       else OptionTableConntime = 30 ;
       OptionSaveFormat=0 ;
       if (ofrm._OptionSaveFormat) OptionSaveFormat=1 ;

       ofrm.Dispose();
    }
    void ButtonServiceClick(object sender, EventArgs e)
    {
       // редактирование сервисов в БД
       ;
       //Получить Имя выделенного элемента
       string id_parent=treeViewA.SelectedNode.Name;
       if (id_parent=="0") return ;
       string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
       if (id_tbl=="0" || id_tbl=="") return ;

       _profileData=(ARC_SUBSYST_PROFILE)propertyGridA.SelectedObject;
       String ID= _profileData.ID ;

       //DialogResult result1;
       FormService frmServ = new FormService(this._conn, ID, id_tbl, OptionSchemaName);
       frmServ.ShowDialog();
       frmServ.Dispose();

    }
    void ToolStripButton5Click(object sender, EventArgs e)
    {
        // show form Status
        FormStatus frmSt = new FormStatus(this._conn, OptionSchemaName); //(this._conn);
        frmSt.Show();
        //frmSt.Dispose();
    }
    void ToolStripButton6Click(object sender, EventArgs e)
    {
        // установка источников
        FormSource frmSo = new FormSource(this._conn, OptionSchemaName); //(this._conn);
        frmSo.ShowDialog();
        frmSo.Dispose();
    }
    void ExportDataToolStripMenuItemClick(object sender, EventArgs e)
    {
       // ExportData
       // вызов формы для построения графика через контекстное меню

       // string msg = String.Format("Row: {0}, Column: {1}",
       //   dataGridViewP.CurrentCell.RowIndex,
       //   dataGridViewP.CurrentCell.ColumnIndex);
       // MessageBox.Show(msg, "Current Cell");

       int selRowNum , selColNum ; // = dataGridViewP.CurrentCell.RowIndex;
       selRowNum = dataGridViewP.CurrentCell.RowIndex; //mouseLocation.RowIndex ;
       selColNum = dataGridViewP.CurrentCell.ColumnIndex ; //mouseLocation.ColumnIndex ;

       //AddLogString(" ExportData -> selRowNum=" + selRowNum.ToString() + " selColNum=" + selColNum.ToString());

       if (selRowNum<0) return ;
       // group id name type {}
       if (selColNum<=3) return ;

       string ID = dataGridViewP.Rows[selRowNum].Cells[1].Value.ToString() ;
       string IDNAME = dataGridViewP.Rows[selRowNum].Cells[2].Value.ToString() ;
       string IDGINFO = dataGridViewP.Columns[selColNum].Name ;
       //string NAMEHEADER = dataGridViewP.Columns[selColNum].HeaderText ;


       // если ячейка Unchecked - покидаем алгоритм
       //if ((System.Windows.Forms.CheckState)dataGridViewP.Rows[selRowNum].Cells[selColNum].Value
       //                 ==CheckState.Unchecked) { return ; }


       object obj1=dataGridViewP.Rows[selRowNum].Cells[selColNum].Value ;

       try
       {
         /*
         unchecked   true   - включение
         checked false   - выключение
         false  true  - включение
         true false - выключение
         */

         // если ячейка Unchecked - покидаем алгоритм
         if (  obj1.ToString()=="Unchecked" ) { return ; }
         if (  obj1.ToString()=="False" ) { return ; }
       }
       catch (Exception ex1)
       {
         AddLogString("ExportDataTool - прервано.. = " + ex1.Message);
         return ;
       }

       //Получить Имя выделенного элемента
       string id_parent=treeViewA.SelectedNode.Name;
       if (id_parent=="0") return ;
       string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
       if (id_tbl=="0" || id_tbl=="") return ;

       dataGridViewP.Rows[selRowNum].Cells[selColNum].Style.ForeColor = Color.Cyan ;

       //DialogResult result1;
       FormExport ifex = new FormExport(_conn, ID, IDNAME, IDGINFO, id_tbl, OptionSchemaName);
       ifex.Show();

    }

    void ToolStripButton7Click(object sender, EventArgs e)
    {
        // button Test
        FormInfo1 fi = new FormInfo1() ;
        fi.Text="Check...";
        fi.richTextBox1.Text="";

        // Output Stat to Logs
        toolStripStatusLabel2.Text = "Create Logs.";

        //=========================================================
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;
        cmd0.Connection=this._conn;
        string strQry = "";

        List<string> listOfNames = new List<string>()
        {
            "datafile1",  "tablespace size"  , "ad_serv_ini_v"
        };

        ResourceManager ro = new ResourceManager("ArcConfig.StatOracle", Assembly.GetExecutingAssembly());

        for(int ii = 0; ii < listOfNames.Count; ii++)
        {
          strQry=listOfNames[ii];

          fi.richTextBox1.AppendText("\n--" + strQry);

          string tmpName = ro.GetString(strQry);
          if (tmpName == null || tmpName.Trim().Length == 0)
          {
             AddLogString("Check = variable '" + strQry + "' not set" );
             continue ;
          }

          cmd0.CommandText=tmpName;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            AddLogString("Check =" + cmd0.CommandText + "=" + ex1.Message);
            continue ;
          }

          while (reader.Read())
          {
             strQry = ";" ;
             for ( int i = 0; i<reader.FieldCount; i++)
             {
               strQry+=GetTypeValue(ref reader, i) +  ";";
             }
             if (reader.FieldCount>1) fi.richTextBox1.AppendText("\n"+strQry);
          } // while
          reader.Close();

        }

        List<string> listTable = new List<string>()
        {
            "ARC_GINFO", "ARC_SERVICES_ACCESS", "ARC_SERVICES_TUNE", "ARC_SUBSYST_PROFILE", "SYS_GTOPT"
        };
        listTable.Sort();
        foreach (string name in listTable) {

          string stName=name;
          if (OptionSchemaName>0) {
             stName=OptionSchemaMain + "." + stName ;
          }

          fi.richTextBox1.AppendText("\n -- "+stName);

          cmd0.CommandText="SELECT * FROM " + stName +" ;" ;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            AddLogString("Check =" + cmd0.CommandText + "=" + ex1.Message);
            continue ;
          }

          while (reader.Read())
          {
             strQry = ";" ;
             for ( int i = 0; i<reader.FieldCount; i++)
             {
               strQry+=GetTypeValue(ref reader, i) +  ";";
             }
             if (reader.FieldCount>1) fi.richTextBox1.AppendText("\n"+strQry);
          } // while
          reader.Close();

        }


        fi.ShowDialog();
    }

    void ButtonClearClick(object sender, EventArgs e)
    {
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();

        cmd0.Connection=this._conn;

        int cnt = 0;

        string stName="ARC_STAT";
        if (OptionSchemaName>0) {
           stName=OptionSchemaMain + "." + stName ;
        }

        DialogResult result = MessageBox.Show ("Очистить " + stName + " ? ",
                                               "Удаление содержимого " + stName,
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button2);
        if (result == DialogResult.Yes)
        {
            //cmd0.CommandText="TRUNCATE TABLE " + stName ;
            cmd0.CommandText="DELETE FROM " + stName ;
            try
            {
               cnt=cmd0.ExecuteNonQuery();
            }
            catch (Exception ex7)
            {
               AddLogString("Ошибка удаления записей ="+ex7.Message);
            }
            AddLogString(cmd0.CommandText + " = " + cnt.ToString() + " rows ");
        }
        return ;
    }
    void AddForALLToolStripMenuItemClick(object sender, EventArgs e)
    {
      //
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;
      cmd0.Connection=this._conn;

      int selRowNum , selColNum ; // = dataGridViewP.CurrentCell.RowIndex;
      selRowNum = dataGridViewP.CurrentCell.RowIndex; //mouseLocation.RowIndex ;
      selColNum = dataGridViewP.CurrentCell.ColumnIndex ; //mouseLocation.ColumnIndex ;

      if (selRowNum<0) return ;
      // group id name type {}
      if (selColNum<=3) return ;

      //Получить Имя выделенного элемента
      string id_parent=treeViewA.SelectedNode.Name;
      if (id_parent=="0") return ;
      string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
      //AddLogString("AddForALL=id_tbl=" + id_tbl);
      if (id_tbl=="0" || id_tbl=="") return ;

      DialogResult result = MessageBox.Show ("Включить архивы у всех параметров ?",
                                         "Включение архивов....",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2);
      if (result == DialogResult.No)
      {
        return ;
      }

      string stName = "" ;
      if (OptionSchemaName>0) {
         stName=OptionSchemaMain + "." ;
      }

      string IDGINFO = dataGridViewP.Columns[selColNum].Name ;

      cmd0.CommandText="select sg.DEFINE_ALIAS from "+stName+"arc_ginfo ag, "+stName+"sys_gtopt sg" +
" where ag.ID_GTOPT=sg.ID and ag.id=" + IDGINFO ;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString("AddForALL =" + cmd0.CommandText + "=" + ex1.Message);
      }

      string DEFINE_ALIAS="";
      if (!reader.IsClosed )
       if (reader.HasRows) {
        while (reader.Read())
        {
          DEFINE_ALIAS = GetTypeValue(ref reader, 0).ToUpper() ;
          break ;
        } // while
        reader.Close();
       }

      int bool_flag = 0 ;
      if (DEFINE_ALIAS.IndexOf("BOOL")>=0) {
        bool_flag = 1 ;
      }

      tabControlAP.Enabled = false ;
      int flag = 0 ; // флаг для алгоритма
      int sflag = 0 ; // число вкл архивов
      //А теперь просто пройдемся циклом по всем ячейкам
      for (int i = 0 ; i < dataGridViewP.Rows.Count; ++i)
      {
        string typestr = "";
        object typeobj=dataGridViewP.Rows[i].Cells[3].Value ; // тип в 3-ей колонке
        try
        {
          typestr=typeobj.ToString();
        }
        catch (Exception ex1)
        {
          ;
        }

        if ( typestr=="ТС" || typestr=="ТУ" ) {
          if (bool_flag == 0) continue ;
        }
        if ( typestr=="ТИ" ) {
          if (bool_flag == 1) continue ;
        }

        flag = 0 ;
        object obj1=dataGridViewP.Rows[i].Cells[selColNum].Value ;
        try
        {
            /*
              unchecked   true   - включение
              checked false   - выключение
              false  true  - включение
              true false - выключение
            */

           // если ячейка Unchecked - активация
           if (  obj1.ToString()=="Unchecked" ) { flag = 1 ; }
           if (  obj1.ToString()=="False" ) { flag = 1 ; }
        }
        catch (Exception ex1)
        {
          continue ;
        }
        if ( flag == 1 ) {
           int ret2 = ArcAdd( dataGridViewP, i , selColNum) ;
           if (ret2>=0) sflag++;
           else AddLogString("AddForALL=(" + i.ToString() + ")= " + ret2.ToString() );
        }

      }
      tabControlAP.Enabled = true ;

      if ( sflag >0 ) {
        MessageBox.Show (" Включено архивов = " + sflag.ToString() + "\n" +
                         " Необходимо обновить вкладку 'Параметры'",
                           "Включение архивов....",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Asterisk);
        tabControlAP.SelectTab(0);
        //tabControlAP.SelectedIndex=0;
      }

    }
    void DelByALLToolStripMenuItemClick(object sender, EventArgs e)
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;
      cmd0.Connection=this._conn;

      //
      int selRowNum , selColNum ; // = dataGridViewP.CurrentCell.RowIndex;
      selRowNum = dataGridViewP.CurrentCell.RowIndex; //mouseLocation.RowIndex ;
      selColNum = dataGridViewP.CurrentCell.ColumnIndex ; //mouseLocation.ColumnIndex ;

      if (selRowNum<0) return ;
      // group id name type {}
      if (selColNum<=3) return ;

      //Получить Имя выделенного элемента
      string id_parent=treeViewA.SelectedNode.Name;
      if (id_parent=="0") return ;
      string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
      //AddLogString("DelByALL=id_tbl=" + id_tbl);
      if (id_tbl=="0" || id_tbl=="") return ;

      DialogResult result = MessageBox.Show ("Отключить архивы у всех параметров ?",
                                         "Отключение архивов....",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2);
      if (result == DialogResult.No)
      {
        return ;
      }

      string stName = "" ;
      if (OptionSchemaName>0) {
         stName=OptionSchemaMain + "." ;
      }

      string IDGINFO = dataGridViewP.Columns[selColNum].Name ;

      cmd0.CommandText="select sg.DEFINE_ALIAS from "+stName+"arc_ginfo ag, "+stName+"sys_gtopt sg" +
" where ag.ID_GTOPT=sg.ID and ag.id=" + IDGINFO ;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString("DelByALL =" + cmd0.CommandText + "=" + ex1.Message);
      }

      string DEFINE_ALIAS="";
      if (!reader.IsClosed )
       if (reader.HasRows) {
        while (reader.Read())
        {
          DEFINE_ALIAS = GetTypeValue(ref reader, 0).ToUpper() ;
          break ;
        } // while
        reader.Close();
       }

      int bool_flag = 0 ;
      if (DEFINE_ALIAS.IndexOf("BOOL")>=0) {
        bool_flag = 1 ;
      }

      tabControlAP.Enabled = false ;
      int flag = 0 ; // флаг для алгоритма
      int sflag = 0 ; // число вкл архивов
      //А теперь пройдемся циклом по всем ячейкам
      for (int i = 0 ; i < dataGridViewP.Rows.Count; ++i)
      {
        string typestr = "";
        object typeobj=dataGridViewP.Rows[i].Cells[3].Value ; // тип в 3-ей колонке
        try
        {
          typestr=typeobj.ToString();
        }
        catch (Exception ex1)
        {
          ;
        }

        if ( typestr=="ТС" || typestr=="ТУ" ) {
          if (bool_flag == 0) continue ;
        }
        if ( typestr=="ТИ" ) {
          if (bool_flag == 1) continue ;
        }

        flag = 0 ;
        object obj1=dataGridViewP.Rows[i].Cells[selColNum].Value ;
        try
        {
            /*
              unchecked   true   - включение
              checked false   - выключение
              false  true  - включение
              true false - выключение
            */

           // если ячейка Unchecked - активация
           if (  obj1.ToString()=="Checked" ) { flag = 1 ; }
           if (  obj1.ToString()=="True" ) { flag = 1 ; }
        }
        catch (Exception ex1)
        {
          continue ;
        }
        if ( flag == 1 ) {
           int ret2 = ArcDel( dataGridViewP, i , selColNum) ;
           if (ret2>=0) sflag++;
           else AddLogString("DelByALL=(" + i.ToString() + ")= " + ret2.ToString() );
        }

      }
      tabControlAP.Enabled = true ;

      if ( sflag >0 ) {
        MessageBox.Show (" Отключено архивов = " + sflag.ToString() + "\n" +
                         " Необходимо обновить вкладку 'Параметры'",
                           "Отключение архивов....",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Asterisk);
        tabControlAP.SelectTab(0);
        //tabControlAP.SelectedIndex=0;
      }
    }
    void RadioButton5CheckedChanged(object sender, EventArgs e)
    {
      //
      try
      {
          if (radioButton5.Checked == true) _getDBv1("meas_snapshot30_stat");
      }
      catch (Exception ex1)
      {
          AddLogString("Error ="+ex1.Message);
      }
    }
    void ButtonClear30Click(object sender, EventArgs e)
    {
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();

      cmd0.Connection=this._conn;

      DateTime date1 = DateTime.Today;
      DateTime d01 = date1.AddDays(-30);
      int dt1970 = Convert.ToInt32( DateTimeToUnixTimestamp(d01) );


      int cnt = 0;

      string stName="meas_snapshot30_stat";
      if (OptionSchemaName>0) {
         stName=OptionSchemaMain + "." + stName ;
      }

      DialogResult result = MessageBox.Show ("Очистить таблицу : " + stName + " ? " + "\n до : " + d01.ToShortDateString() ,
                                             "Удаление содержимого " + stName,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question,
                                             MessageBoxDefaultButton.Button2);
      if (result == DialogResult.Yes)
      {
          //cmd0.CommandText="TRUNCATE TABLE " + stName ;
          cmd0.CommandText="DELETE FROM " + stName + " WHERE dt1970<=" + dt1970.ToString() ;
          try
          {
             cnt=cmd0.ExecuteNonQuery();
          }
          catch (Exception ex7)
          {
             AddLogString("Ошибка удаления записей ="+ex7.Message);
          }
          AddLogString(cmd0.CommandText + " = " + cnt.ToString() + " rows ");
      }
      return ;
    }

void PartitionMedia(object sender, EventArgs e)
{
  // Partition

  // получаем ячейку
  int selRowNum ;
  selRowNum = dataGridViewA.CurrentCell.RowIndex; //mouseLocation.RowIndex ;

  if (selRowNum<0) return ;

  DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridViewA.Rows[selRowNum].Cells[0] ;

  // если ячейка Unchecked - покидаем алгоритм
  if (Convert.ToBoolean(chk.Value) == false || chk.Value == null)
  {
     return ;
  }

  //Получить Имя выделенного элемента
  string id_parent=treeViewA.SelectedNode.Name;
  if (id_parent=="0") return ;

  string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
  if (id_tbl=="0" || id_tbl=="") return ;

  string ID = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;
  string IDNAME = dataGridViewA.Rows[selRowNum].Cells[2].Value.ToString() ;
  string IDGTOPT = dataGridViewA.Rows[selRowNum].Cells[3].Value.ToString() ;
  string NAMEHEADER = dataGridViewA.Rows[selRowNum].Cells[16].Value.ToString() ;

  Form ifpart = new FormPartitions(_conn, ID, IDNAME, IDGTOPT, NAMEHEADER, id_tbl, OptionSchemaName);
  ifpart.StartPosition=FormStartPosition.CenterParent ;
  ifpart.Show();

}
    void ToolStripButton8Click(object sender, EventArgs e)
    {
      Form arcdbfrm = new FormArc_db_schema(this._conn, "", OptionSchemaName);
      arcdbfrm.StartPosition=FormStartPosition.CenterParent ;
      arcdbfrm.ShowDialog();
    }
    void ButtonProfileClick(object sender, EventArgs e)
    {
      string stSchema="";
      if (OptionSchemaName>0) {
          stSchema=OptionSchemaMain + "." ;
      }

      Form lstdbfrm = new FormList(this._conn, stSchema+"ARC_SUBSYST_PROFILE", "LAST_UPDATE");
      lstdbfrm.StartPosition=FormStartPosition.CenterParent ;
      lstdbfrm.ShowDialog();

    }
    void ButtonVarSchClick(object sender, EventArgs e)
    {
      Form arcdbfrm = new FormARCSCH();
      arcdbfrm.StartPosition=FormStartPosition.CenterParent ;
      arcdbfrm.ShowDialog();
    }
    void ToolStripMenuItem4Click(object sender, EventArgs e)
    {
      // Params move

      // получаем ячейку
      int selRowNum ;
      selRowNum = dataGridViewA.CurrentCell.RowIndex; //mouseLocation.RowIndex ;

      if (selRowNum<0) return ;

      object obj1 = dataGridViewA.Rows[selRowNum].Cells[0].Value ;

      //DataGridViewCheckBoxColumn

      DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) dataGridViewA.Rows[selRowNum].Cells[0] ;

      //AddLogString("chk.Value="+chk.Value.ToString() );

      // если ячейка Unchecked - покидаем алгоритм
      if (Convert.ToBoolean(chk.Value) == false || chk.Value == null)
      {
         return ;
      }

      //Получить Имя выделенного элемента
      string id_parent=treeViewA.SelectedNode.Name;
      if (id_parent=="0") return ;

      string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
      if (id_tbl=="0" || id_tbl=="") return ;

      string ID = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;
      string IDNAME = dataGridViewA.Rows[selRowNum].Cells[2].Value.ToString() ;
      string IDGTOPT = dataGridViewA.Rows[selRowNum].Cells[3].Value.ToString() ;
      string NAMEHEADER = dataGridViewA.Rows[selRowNum].Cells[16].Value.ToString() ;

      //DialogResult result1;
      //
      Form ifpch = new FormParamChange(_conn, ID, IDNAME, IDGTOPT, NAMEHEADER, id_tbl,OptionFullName, OptionSchemaName);
      ifpch.StartPosition=FormStartPosition.CenterParent ;
      ifpch.Show();

    }
    void ButVariableClick(object sender, EventArgs e)
    {
      // variable
      string stSchema="";
      if (OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }

      FormVariable lstv = new FormVariable(this._conn, stSchema );
      lstv.StartPosition=FormStartPosition.CenterParent ;
      lstv.ShowDialog();
    }
    void ToolStripButtonDBEClick(object sender, EventArgs e)
    {
      //DBE
      FormDBE fdbe = new FormDBE(this._conn, OptionSchemaName );
      fdbe.StartPosition=FormStartPosition.CenterParent ;
      fdbe.ShowDialog();
    }
    void ToolStripButton9Click(object sender, EventArgs e)
    {
      //30
      FormMeas30 fdbe30 = new FormMeas30(this._conn, OptionSchemaName );
      fdbe30.StartPosition=FormStartPosition.CenterParent ;
      fdbe30.ShowDialog();
    }
    void MainFormFormClosed(object sender, FormClosedEventArgs e)
    {
          try
          {
            this._conn.Dispose();
            this._conn = (OdbcConnection) null;
          }
          catch (Exception ex)
          {
            ;
          }
    }

    void ButtonCalcsClick(object sender, EventArgs e)
    {
      //
      int lVal = 0;
      double fVal = 0;

      for( int i=1; i<=5; i++ ) {
        if (Int32.TryParse(dataGridViewCalc[1, i].Value.ToString(), out lVal))
           dataGridViewCalc[1, i].Value=lVal.ToString() ;
        else dataGridViewCalc[1, i].Value="0" ;
        if (Int32.TryParse(dataGridViewCalc[2, i].Value.ToString(), out lVal))
           dataGridViewCalc[2, i].Value=lVal.ToString() ;
        else dataGridViewCalc[2, i].Value="0" ;
        if (Int32.TryParse(dataGridViewCalc[3, i].Value.ToString(), out lVal))
           dataGridViewCalc[3, i].Value=lVal.ToString() ;
        else dataGridViewCalc[3, i].Value="0" ;
        if (Int32.TryParse(dataGridViewCalc[4, i].Value.ToString(), out lVal))
           dataGridViewCalc[4, i].Value=lVal.ToString() ;
        else dataGridViewCalc[4, i].Value="0" ;
        if (Int32.TryParse(dataGridViewCalc[5, i].Value.ToString(), out lVal))
           dataGridViewCalc[5, i].Value=lVal.ToString() ;
        else dataGridViewCalc[5, i].Value="0" ;
        if (Int32.TryParse(dataGridViewCalc[6, i].Value.ToString(), out lVal))
           dataGridViewCalc[6, i].Value=lVal.ToString() ;
        else dataGridViewCalc[6, i].Value="0" ;
        if (Int32.TryParse(dataGridViewCalc[7, i].Value.ToString(), out lVal))
           dataGridViewCalc[7, i].Value=lVal.ToString() ;
        else dataGridViewCalc[7, i].Value="0" ;
        dataGridViewCalc.Update();
      }

      for( int i=1; i<=7; i++ ) {
        double v1 = Convert.ToDouble(dataGridViewCalc[i, 1].Value.ToString());
        double v2 = Convert.ToDouble(dataGridViewCalc[i, 2].Value.ToString());
        double v3 = Convert.ToDouble(dataGridViewCalc[i, 3].Value.ToString());
        double v4 = Convert.ToDouble(dataGridViewCalc[i, 4].Value.ToString());
        fVal= (v1*v2*v3*v4)/(1024*1024*1024);
        if (fVal<0.0) fVal=0.0;
        int lInt=Convert.ToInt32(dataGridViewCalc[i, 2].Value.ToString());
        if (lInt<=0) fVal=0.0;
        double i5 = Math.Round(fVal, 4) ;
        dataGridViewCalc[i, 5].Value=i5.ToString();
        double i6 = Math.Round(0.5 * i5 , 3) ;
        dataGridViewCalc[i, 6].Value=i6.ToString();
        double i7 = Math.Round(0.1 * (i5+i6) , 3) ;
        dataGridViewCalc[i, 7].Value=i7.ToString();
        double i8 = Math.Round(0.5 * (i5+i6+i7) , 3) ;
        dataGridViewCalc[i, 8].Value=i8.ToString();

        double i9 = Math.Round(1 * (i5+i6+i7+i8) , 3) ;
        dataGridViewCalc[i, 9].Value=i9.ToString();

      }

    }
    void ComboBoxCalcSelectedIndexChanged(object sender, EventArgs e)
    {
      int coef=0;

      switch ( comboBoxCalc.SelectedIndex )
      {
       case 0 : coef=1;break;
       case 1 : coef=12;break;
       case 2 : coef=1*24;break;
       case 3 : coef=2*24;break;
       case 4 : coef=3*24;break;
       case 5 : coef=7*24;break;
       case 6 : coef=30*24;break;
       case 7 : coef=40*24;break;
       case 8 : coef=2*30*24;break;
       case 9 : coef=3*30*24;break;
       case 10 : coef=6*30*24;break;
       case 11 : coef=1*365*24;break;
       case 12 : coef=2*365*24;break;
       case 13 : coef=3*365*24;break;
       case 14 : coef=4*365*24;break;
       case 15 : coef=5*365*24;break;
       case 16 : coef=10*365*24;break;
       default:
              coef=24;
              break;
      }

      dataGridViewCalc[1, 4].Value=coef.ToString();
      dataGridViewCalc[2, 4].Value=coef.ToString();
      dataGridViewCalc[3, 4].Value=coef.ToString();
      dataGridViewCalc[4, 4].Value=coef.ToString();
      dataGridViewCalc[5, 4].Value=coef.ToString();
      dataGridViewCalc[6, 4].Value=coef.ToString();
      dataGridViewCalc[7, 4].Value=coef.ToString();

    }
    void ButtonDBCLSClick(object sender, EventArgs e)
    {
      //
      richTextBoxDB.Clear();
    }
    void ButtonDBRSDU_UPDATEClick(object sender, EventArgs e)
    {
      //RSDU_UPDATE
      richTextBoxDB.AppendText( ReadDataDB( "RSDU_UPDATE" )) ;
    }
    void ButtonDBdba_tab_privsClick(object sender, EventArgs e)
    {
      //dba_tab_privs
      richTextBoxDB.AppendText( ReadDataDB( "dba_tab_privs" )) ;
    }
    void ButtonDBUsersServClick(object sender, EventArgs e)
    {
      //UsersServ
      richTextBoxDB.AppendText( ReadDataDB( "UsersServ" )) ;
    }
    void ButtonDBStatus0Click(object sender, EventArgs e)
    {
      //Status0
      richTextBoxDB.AppendText( ReadDataDB( "Status0" )) ;
    }
    void ButtonDBdbtimezoneClick(object sender, EventArgs e)
    {
      //dbtimezone
      richTextBoxDB.AppendText( ReadDataDB( "dbtimezone" )) ;
    }
    void ButtonDBvversionClick(object sender, EventArgs e)
    {
      //v$version
      richTextBoxDB.AppendText( ReadDataDB( "version" )) ;
    }
    void ButtonDBdatafile1Click(object sender, EventArgs e)
    {
      //datafile1
      richTextBoxDB.AppendText( ReadDataDB( "datafile1" )) ;
    }
    void ButtonDBtablespacesizeClick(object sender, EventArgs e)
    {
      //tablespace size
      richTextBoxDB.AppendText( ReadDataDB( "tablespace size" )) ;
    }
    void ButtonDBgvdatabaseClick(object sender, EventArgs e)
    {
      //gv$database
      richTextBoxDB.AppendText( ReadDataDB( "database" )) ;
    }
    void ButtonDBgvasm_diskgroupClick(object sender, EventArgs e)
    {
      //gv$asm_diskgroup
      richTextBoxDB.AppendText( ReadDataDB( "asm_diskgroup" )) ;
    }
    void ButtonDBdba_directoriesClick(object sender, EventArgs e)
    {
      //dba_directories
      richTextBoxDB.AppendText( ReadDataDB( "dba_directories" )) ;
    }
    void ButtonDBdba_db_linksClick(object sender, EventArgs e)
    {
      //dba_db_links
      richTextBoxDB.AppendText( ReadDataDB( "dba_db_links" )) ;
    }
    void ButtonDBvresource_limitClick(object sender, EventArgs e)
    {
      //v$resource_limit
      richTextBoxDB.AppendText( ReadDataDB( "resource_limit" )) ;
    }
    void ButtonDBvparameterClick(object sender, EventArgs e)
    {
      //v$parameter
      richTextBoxDB.AppendText( ReadDataDB( "parameter" )) ;
    }
    void ButtonDBnls_session_parametersClick(object sender, EventArgs e)
    {
      //nls_session_parameters
      richTextBoxDB.AppendText( ReadDataDB( "nls_session_parameters" )) ;
    }
    void ButtonDBfraClick(object sender, EventArgs e)
    {
      //RECOVERY_FILE_DEST
      richTextBoxDB.AppendText( ReadDataDB( "RECOVERY_FILE_DEST" )) ;
    }
    void ButtonDBundoClick(object sender, EventArgs e)
    {
      //UNDO
      richTextBoxDB.AppendText( ReadDataDB( "UNDO" )) ;
    }
    void ButtonDBvsga_dynamic_componentsClick(object sender, EventArgs e)
    {
      //v$sga_dynamic_components
      richTextBoxDB.AppendText( ReadDataDB( "sga_dynamic_components" )) ;
    }
    void ButtonDBvpgastatClick(object sender, EventArgs e)
    {
      //v$pgastat
      richTextBoxDB.AppendText( ReadDataDB( "pgastat" )) ;
    }
    void ButtonDBvlocked_objectClick(object sender, EventArgs e)
    {
      //v$locked_object
      richTextBoxDB.AppendText( ReadDataDB( "locked_object" )) ;
    }
    void ButtonDBdba_recyclebinClick(object sender, EventArgs e)
    {
      //dba_recyclebin
      richTextBoxDB.AppendText( ReadDataDB( "dba_recyclebin" )) ;
    }
    void ButtonDBvsessionClick(object sender, EventArgs e)
    {
      //v$session
      richTextBoxDB.AppendText( ReadDataDB( "session1" )) ;
    }
    void ButtonDBgvsessionClick(object sender, EventArgs e)
    {
      //gv$session
      richTextBoxDB.AppendText( ReadDataDB( "session2" )) ;
    }
    void ButtonDBvprocessClick(object sender, EventArgs e)
    {
      //v$process
      richTextBoxDB.AppendText( ReadDataDB( "process" )) ;
    }
    void ButtonDBvopen_cursorClick(object sender, EventArgs e)
    {
      //v$open_cursor
      richTextBoxDB.AppendText( ReadDataDB( "open_cursor1" )) ;
    }
    void ButtonDBopen_cursorClick(object sender, EventArgs e)
    {
      //open_cursor
      richTextBoxDB.AppendText( ReadDataDB( "open_cursor" )) ;
    }
    void ButtonDBopenedcursorscurrentClick(object sender, EventArgs e)
    {
      //openedcursorscurrent
      richTextBoxDB.AppendText( ReadDataDB( "openedcursorscurrent" )) ;
    }
    void ButtonDBvsysstatClick(object sender, EventArgs e)
    {
      //v$sysstat
      richTextBoxDB.AppendText( ReadDataDB( "sysstat" )) ;
    }
    void ButtonDBGPUClick(object sender, EventArgs e)
    {
      //GPU
      richTextBoxDB.AppendText( ReadDataDB( "GPU" )) ;
    }
    void ButtonDBunlockpassClick(object sender, EventArgs e)
    {
      //unlockuser
      //richTextBoxDB.AppendText( ReadDataDB( "unlockuser_unpass" )) ;
      if (DB_VER_FLAG==3) {
         richTextBoxDB.AppendText( ReadDataDB( "unlockuser_unpass_sqlite" )) ;
      } else {
        richTextBoxDB.AppendText( ReadDataDB( "unlockuser_unpass2" )) ;
      }

    }
    void ButtonDBtoUnixClick(object sender, EventArgs e)
    {
        // DateTime -> Unix
        DateTime t1 = dateTimePickerDB1.Value ; // date
        DateTime t2 = dateTimePickerDB2.Value ; // sec
        string s1 = t1.ToShortDateString() + " " + t2.ToLongTimeString() ;

        // "yyyy-MM-dd HH:mm:ss"  "dd.MM.yyyy HH:mm:ss"
        DateTime myDate = DateTime.ParseExact(s1, "dd.MM.yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
        // DateTime -> Unix
        int i1 = Convert.ToInt32( DateTimeToUnixTimestamp(myDate) );
        textBoxDBUTC.Text=i1.ToString();

        Application.DoEvents();
    }
    void ButtonDBUNIXClick(object sender, EventArgs e)
    {
      // Unix -> DateTime
      double d1 = 0;
      try
      {
        d1=Convert.ToDouble(textBoxDBUTC.Text) ;
      }
      catch (Exception )
      {
        d1 = 0;
        textBoxDBUTC.Text="0";
      }
      d1 = Math.Abs( d1 );
      DateTime t1 = UnixTimestampToDateTime(d1);
      dateTimePickerDB1.Value = t1 ; // date
      dateTimePickerDB2.Value = t1 ; // sec
      Application.DoEvents();
    }
    void ButtonDBad_serv_iniClick(object sender, EventArgs e)
    {
       //ad_serv_ini_v
       richTextBoxDB.AppendText( ReadDataDB( "ad_serv_ini_v" )) ;
    }
    void ButtonParamCalcClick(object sender, EventArgs e)
    {
      //strQry:='select count (*) from DA_COLUMN_DATA_V;';
      dataGridViewCalc[1, 1].Value=ReadDataVal( "DA_COLUMN_DATA_V" );

      //strQry:='select count (*) from elreg_list_v;';
      dataGridViewCalc[2, 1].Value=ReadDataVal( "elreg_list_v" );

      //strQry:='select count (*) from phreg_list_v';
      dataGridViewCalc[3, 1].Value=ReadDataVal( "phreg_list_v" );

      //strQry:='select count (*) from pswt_list_v';
      dataGridViewCalc[4, 1].Value=ReadDataVal( "pswt_list_v" );

      //strQry:='select count (*) from auto_list_v';
      dataGridViewCalc[5, 1].Value=ReadDataVal( "auto_list_v" );

      //strQry:='select count (*) from EXDATA_LIST_V';
      dataGridViewCalc[6, 1].Value=ReadDataVal( "EXDATA_LIST_V" );

      //strQry:='select count (*) from DG_LIST';
      dataGridViewCalc[7, 1].Value=ReadDataVal( "DG_LIST" );

      dataGridViewCalc.Rows[1].DefaultCellStyle.Font  = new Font(DataGridView.DefaultFont, FontStyle.Bold);

    }
    void ButtonSaveClick(object sender, EventArgs e)
    {
       //устанавливаем редактируемый объект
       //_profileData.ID="";
       //_profileData.ID_TBLLST="";
       //_profileData.ID_GINFO="";
       //_profileData.IS_WRITEON=false ;
       //_profileData.STACK_NAME="NONE";
       //_profileData.LAST_UPDATE="";
       //_profileData.IS_VIEWABLE=false ;
       _profileData=(ARC_SUBSYST_PROFILE)propertyGridA.SelectedObject;
       String ID= _profileData.ID ;
       String IS_WRITEON = "1" ;
       if (_profileData.IS_WRITEON==false) { IS_WRITEON="0"; }
       String IS_VIEWABLE = "1" ;
       if (_profileData.IS_VIEWABLE==false) { IS_VIEWABLE="0"; }
       String STACK_NAME = _profileData.STACK_NAME.Trim() ;
       if (STACK_NAME.Equals("")) { return ; }

       string stSchema="";
       if (OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
       }

       string strS = "UPDATE "+stSchema+"ARC_SUBSYST_PROFILE SET " +
          " IS_WRITEON=" + IS_WRITEON +
          ", IS_VIEWABLE=" + IS_VIEWABLE +
          ", STACK_NAME='" + STACK_NAME + "'" +
          " WHERE ID=" + ID;

       AddLogString(" SAVE -> " + strS);

       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();

       cmd0.Connection=this._conn;

       int crec1 = 0 ;
       cmd0.CommandText=strS;
       try
       {
         crec1=cmd0.ExecuteNonQuery();
       }
       catch (Exception ex1)
       {
         AddLogString(" SAVE -> error = " + ex1.Message);
       }
       AddLogString(" SAVE -> done = " + crec1.ToString());

    }
    void ButtonExportMClick(object sender, EventArgs e)
    {
      // экспорт в csv

      var  dataGridView = (DataGridView)null ;
      //dataGridView1
      if (tabControl1.SelectedIndex==0) {
        dataGridView = (DataGridView)dataGridView1;
      }
      if (tabControl1.SelectedIndex==1) {
        if (tabControlAP.SelectedIndex==0) dataGridView = (DataGridView)dataGridViewA;
        else dataGridView = (DataGridView)dataGridViewP;
      }
      if (tabControl1.SelectedIndex==2) {
        dataGridView = (DataGridView)dataGridViewS;
      }
      if (tabControl1.SelectedIndex==3) {
        dataGridView = (DataGridView)dataGridViewCalc;
      }
      if (tabControl1.SelectedIndex==4) {
        dataGridView = (DataGridView)dataGridViewDB;
      }


      if (dataGridView==null) return ;

      //if (dataGridView.DataSource==null || dataGridView.DataSource=="" ) return ;

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
            Clipboard.SetDataObject(dataGridView.GetClipboardContent());
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
              fileCSV += "\t" + Environment.NewLine;
              string st , tst ;
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
                        tst = dataGridView[j, i].Value.GetType().ToString() ;
                      }
                      catch (Exception ex1)
                      {
                        tst = "" ;
                      }

                      if ("System.Windows.Forms.CheckState"==tst) {
                        //Checked Unchecked
                        if (OptionSaveFormat>0) {
                          if (st=="Checked") st="1";
                          if (st=="Unchecked") st="0";
                        } else {
                          if (st=="Checked") st="x";
                          if (st=="Unchecked") st="";
                        }
                      }
                      fileCSV += st + ";";
                      //fileCSV += ( dataGridView[j, i].Value).ToString() + ";";
                    }
                    //fileCSV += "\t" + Environment.NewLine;
                    wr.WriteLine(fileCSV);
                    fileCSV="";
                }
              } //using

                //StreamWriter wr = new StreamWriter(filename, false, Encoding.UTF8); // Encoding.GetEncoding("windows-1251")
                //wr.Write(fileCSV);
                //wr.Close();

          break ;
      }
      String s1="filename="+filename + " -> Save";
      AddLogString(s1);
      toolStripStatusLabel2.Text = s1;
      //MessageBox.Show("Export to \n " + filename + " \n done");
    }
		void DataGridViewDBDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		    //
		    // //e.Cancel=false;
		    e.ThrowException=false;
 
		}



  }

}
