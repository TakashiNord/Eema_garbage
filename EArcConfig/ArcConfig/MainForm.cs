/*
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

using System.Collections;
using System.ComponentModel;
using System.Data.Odbc;
using System.IO;
using System.Text;

using System.Data.Common;
using System.Globalization;
using System.Threading;

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

    // редактируемые данные
      private ARC_SUBSYST_PROFILE _profileData = new ARC_SUBSYST_PROFILE();

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
       AddLogString("tabControl1="+i);
    }

    const string toolStripButton = "ToolStripButton1";

    void ToolStripButton1Click(object sender, EventArgs e)
    {
       AddLogString(toolStripButton);
       this._conn = this._getDBConnection();
       if (this._conn == null) return;
       toolStripButton1.Enabled=false;
       tabControl1.Enabled=true;
       _setDBarc();
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
       command.CommandText="SET ROLE BASE_EXT_CONNECT_OIK , ARC_STAND_ADMIN";

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

    void _setDBarc()
    {
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd = new OdbcCommand();
       OdbcDataReader reader1 = null , reader2 = null, reader3 = null  ;

       cmd.Connection = this._conn; // уже созданное и открытое соединение


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

       treeViewA.BeginUpdate(); //добавить
       treeViewA.Nodes.Clear();

       // получаем root- id
       cmd.CommandText="select distinct id_parent from sys_tree21 where id_lsttbl in " +
"(select id from sys_tbllst where id_type in " +
"(select id from sys_otyp where define_alias like 'LST') " +
"and id_node in  " +
"(select id from sys_db_part where id_parent in " +
"(select id from sys_db_part where define_alias like 'MODEL_SUBSYST' or define_alias like 'DA_SUBSYST' )))";
       try
       {
          reader1 = cmd.ExecuteReader();
       }
       catch (Exception ex1)
       {
          AddLogString(" выполнение алгоритма  - прервано.. = " + cmd.CommandText + " " + ex1.Message);
          return ;
       }


       List<string> id_parents = new List<string> ();
       //treeViewA.Nodes.Add(new TreeNode("Разделы"));

       if (reader1.HasRows)
       {
        while (reader1.Read())
        {
          int i = 0 ;
          string arrtype = reader1.GetDataTypeName(i);
          //AddLogString(" arrtype = " + arrtype);
          string stmp = arrtype.ToUpper();
          if (stmp=="DECIMAL") id_parents.Add( reader1.GetValue(i).ToString() );
          if (stmp=="NUMBER") id_parents.Add( reader1.GetValue(i).ToString()); //GetDecimal(i).ToString();
          if (stmp=="VARCHAR2") id_parents.Add( reader1.GetString(i) );
          if (stmp=="NVARCHAR") id_parents.Add( reader1.GetString(i) );
          if (stmp=="WVARCHAR") id_parents.Add( reader1.GetString(i) );
          if (stmp=="TEXT") id_parents.Add( reader1.GetString(i) );
          if (stmp=="INTEGER") id_parents.Add( reader1.GetValue(i).ToString() );
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


       cmd.CommandText="SELECT id, COALESCE(id_parent,0,0) as id_p, name, COALESCE(id_lsttbl,0,0) as id_l " +
       " FROM sys_tree21 " +
       "WHERE id IN ( " + sS + "  ) " ;

       try
       {
          reader2 = cmd.ExecuteReader();
       }
       catch (Exception ex1)
       {
          AddLogString(" выполнение алгоритма  - прервано.. = " + cmd.CommandText + " " + ex1.Message);
          return ;
       }

       //AddLogString(cmd.CommandText);

       string[] arr = new string[4];

       if (reader2.HasRows)
       {
        while (reader2.Read())
         {
           for ( int i = 0; i<4; i++) {
             arr[i]="";
             if (reader2.IsDBNull(i)) {
               arr[i]="";
             } else {
               arr[i]= reader2.GetDataTypeName(i);
               //AddLogString(" arrtype2 = " + arr[i]);
               string stmp = arr[i].ToUpper();
               if (stmp=="DECIMAL") arr[i] = reader2.GetValue(i).ToString();
               if (stmp=="NUMBER") arr[i] = reader2.GetValue(i).ToString(); //GetDecimal(i).ToString();
               if (stmp=="VARCHAR2") arr[i] = reader2.GetString(i);
               if (stmp=="NVARCHAR") arr[i] = reader2.GetString(i);
               if (stmp=="WVARCHAR") arr[i] = reader2.GetString(i);
               if (stmp=="TEXT") arr[i] = reader2.GetString(i);
               if (stmp=="INTEGER") arr[i] = reader2.GetValue(i).ToString();
             }
           }

          if (arr[3]=="") { arr[3]="0" ; }

           if (arr[3]=="0") {
             TreeNode rootNode = new TreeNode();
             rootNode.Name = arr[0]; // arr[0];
             rootNode.Text = arr[2];
             rootNode.Tag = "0";
             AddLogString(rootNode.Name + " " + rootNode.Text);
             treeViewA.Nodes.Add(rootNode);
           } else {
             string s5= arr[1];
             TreeNode[] tNodeCurrent = new TreeNode[15];
             tNodeCurrent =treeViewA.Nodes.Find(s5, true);
             foreach (TreeNode ndl in tNodeCurrent)
             {
                if (ndl.Name == s5) {
                    TreeNode Nd = new TreeNode();
                    Nd.Name = arr[3]; // arr[0];
                    Nd.Text = arr[2] + " (" + arr[3] + ")";
                    ndl.Nodes.Add(Nd);
                    AddLogString(Nd.Name + " " + Nd.Text);
                }

              } // foreach
           } // if

         } // while

       }
       reader2.Close();

       cmd.CommandText="select id, COALESCE(id_parent,0,0), name, COALESCE(id_lsttbl,0,0) " +
       " from sys_tree21 " +
       "where id_parent in (" + sS + " ) " ;

       try
       {
          reader3 = cmd.ExecuteReader();
       }
       catch (Exception ex1)
       {
          AddLogString(" выполнение алгоритма  - прервано.. = " + cmd.CommandText + " " + ex1.Message);
          return ;
       }

       //AddLogString( cmd.CommandText);

       if (reader3.HasRows)
       {
        while (reader3.Read())
         {
           for ( int i = 0; i<4; i++) {
             arr[i]="";
             if (reader3.IsDBNull(i)) {
               arr[i]="";
             } else {
               arr[i]= reader3.GetDataTypeName(i);
               //AddLogString(" arrtype3 = " + arr[i]);
               string stmp = arr[i].ToUpper();
               if (stmp=="DECIMAL") arr[i] = reader3.GetValue(i).ToString();
               if (stmp=="NUMBER") arr[i] = reader3.GetValue(i).ToString(); //GetDecimal(i).ToString();
               if (stmp=="VARCHAR2") arr[i] = reader3.GetString(i);
               if (stmp=="NVARCHAR") arr[i] = reader3.GetString(i);
                if (stmp=="WVARCHAR") arr[i] = reader3.GetString(i);
               if (stmp=="TEXT") arr[i] = reader3.GetString(i);
               if (stmp=="INTEGER") arr[i] = reader3.GetValue(i).ToString();
             }
           }

           if (arr[3]=="0") {
             TreeNode rootNode = new TreeNode();
             rootNode.Name = arr[3]; // arr[0];
             rootNode.Text = arr[2];
             rootNode.Tag = arr[3];
             AddLogString(rootNode.Name + " " + rootNode.Text);
             treeViewA.Nodes.Add(rootNode);
           } else {
             string s5= arr[1];
             TreeNode[] tNodeCurrent = new TreeNode[15];
             tNodeCurrent =treeViewA.Nodes.Find(s5, true);
             foreach (TreeNode ndl in tNodeCurrent)
             {
                if (ndl.Name == s5) {
                    TreeNode Nd = new TreeNode();
                    Nd.Name = arr[3]; // arr[0];
                    Nd.Text = arr[2] + " (" + arr[3] + ")";
                    Nd.Tag = arr[3];
                    ndl.Nodes.Add(Nd);
                    AddLogString(Nd.Name + " " + Nd.Text);
                }

              } // foreach
           } // if

         } // while

       }
       reader3.Close();
       treeViewA.EndUpdate(); //добавить

       treeViewA.ExpandAll();

    }

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
       command.CommandText = "select * from " + sObj ;

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
       adapter.Fill(dataSet1,sObj);

       // Запрет удаления данных
       dataSet1.Tables[0].DefaultView.AllowDelete = false;
       // Запрет модификации данных
       dataSet1.Tables[0].DefaultView.AllowEdit = false;
       // Запрет добавления данных
       dataSet1.Tables[0].DefaultView.AllowNew = false;

       // (с этого момента она будет отображать его содержимое)
       dataGridView1.DataSource = dataSet1.Tables[0];

       dataGridView1.Update();

       //this._conn.Close();

       return ;
    }
    void ToolStripButton2Click(object sender, EventArgs e)
    {
      //
      Button1Click(sender,e);
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
       timer1.Interval = 1000;
       timer1.Enabled = true ;

       tabControl1.Enabled=false;
       tabControl1.SelectedIndex=0;

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

       _setDBS();
    }
    void TreeViewAAfterSelect(object sender, TreeViewEventArgs e)
    {
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcCommand cmd1 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;
      cmd1.Connection=this._conn;

      //Получить Имя выделенного элемента
      string id_parent=treeViewA.SelectedNode.Name;
      string id_index = Convert.ToString ( treeViewA.SelectedNode.Tag ) ;
      AddLogString(" id_parent=" + id_parent + "  id_index=" + id_index);

      // re-read
      //dataGridViewA.Rows.Clear();
      Application.DoEvents();

      dataSetA.Clear();

      if (id_index=="0" || id_index=="") return;
      // полный путь

      // "select * from ARC_GINFO";
      cmd0.CommandText="" +
"SELECT  " +
"    ARC_GINFO.ID as ID,  " +
"    sys_gtopt.NAME as VIEW_ARCHIVE, " +
"    ARC_GINFO.ID_GTOPT ,  " +
"    ARC_TYPE.NAME as TYPE_ARCHIVE, " +
"    ARC_GINFO.ID_TYPE as ID_TYPE, " +
"    ARC_GINFO.DEPTH , " +
"    ARC_GINFO.DEPTH_LOCAL , " +
"    ARC_GINFO.CACHE_SIZE , " +
"    ARC_GINFO.CACHE_TIMEOUT , " +
"    ARC_GINFO.FLUSH_INTERVAL , " +
"    ARC_GINFO.RESTORE_INTERVAL , " +
"    ARC_GINFO.STACK_INTERVAL , " +
"    ARC_GINFO.WRITE_MINMAX  , " +
"    ARC_GINFO.RESTORE_TIME , " +
"    ARC_GINFO.NAME  , " +
"    ARC_GINFO.STATE  , " +
"    ARC_GINFO.DEPTH_PARTITION  , " +
"    ARC_GINFO.RESTORE_TIME_LOCAL " +
" FROM ARC_GINFO , sys_gtopt, ARC_TYPE " +
" WHERE sys_gtopt.ID=ARC_GINFO.ID_GTOPT " +
"AND ARC_GINFO.ID_TYPE=ARC_TYPE.ID" ;

      // Указываем запрос для выполнения
      adapter.SelectCommand = cmd0;
      // Заполняем объект источника данных
      //adapter.Fill(dataSetA,"ARC_GINFO");
      adapter.Fill(dataSetA);

      // Запрет удаления данных
      dataSetA.Tables[0].DefaultView.AllowDelete = false;
      // Запрет модификации данных
      dataSetA.Tables[0].DefaultView.AllowEdit = false;
      // Запрет добавления данных
      dataSetA.Tables[0].DefaultView.AllowNew = false;

      // (с этого момента она будет отображать его содержимое)
      dataGridViewA.DataSource = dataSetA.Tables[0].DefaultView;;
      //dataGridViewA.Columns["ID"].ReadOnly = true;
      //dataGridViewA.Columns["ID_NODE"].ReadOnly = true;

      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridViewA.AutoResizeColumns();

      // Configure the details DataGridView so that its columns automatically
      // adjust their widths when the data changes.
      dataGridViewA.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.AllCells;

      for (int ii = 0; ii < dataGridViewA.RowCount ; ii++) {
        dataGridViewA.Rows[ii].Cells[0].Value = false ;
        dataGridViewA.Rows[ii].HeaderCell.Style.BackColor = Color.White ;
        dataGridViewA.Rows[ii].DefaultCellStyle.BackColor = Color.White ;
      }

      dataGridViewA.EnableHeadersVisualStyles = false;
      dataGridViewA.AlternatingRowsDefaultCellStyle.BackColor =Color.LightGray;
      dataGridViewA.Columns[0].HeaderCell.Style.BackColor = Color.Blue ;

      //dataGridViewA.Columns[0].HeaderCell.ToolTipText = "Подсказка11";

      dataGridViewA.Update();

string sl1="SELECT  ARC_SUBSYST_PROFILE.ID,ARC_SUBSYST_PROFILE.ID_TBLLST,ARC_SUBSYST_PROFILE.ID_GINFO," +
  "ARC_SUBSYST_PROFILE.IS_WRITEON, ARC_SUBSYST_PROFILE.STACK_NAME,ARC_SUBSYST_PROFILE.LAST_UPDATE,ARC_SUBSYST_PROFILE.IS_VIEWABLE  " +
"FROM  ARC_SUBSYST_PROFILE " +
//"FROM sys_tree21, ARC_SUBSYST_PROFILE " +
"WHERE " + id_parent + "=ARC_SUBSYST_PROFILE.ID_TBLLST" ;
//"WHERE sys_tree21.id=" + id_parent + " AND sys_tree21.id_lsttbl=ARC_SUBSYST_PROFILE.ID_TBLLST" ;

      cmd1.CommandText=sl1;
      try
      {
        reader = cmd1.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" выполнение алгоритма  - прервано.. = " + cmd1.CommandText + " " + ex1.Message);
        return ;
      }

      if (reader.HasRows==false) {
        reader.Close();
        //dataGridViewA.Enabled=false;
        return;
      }
      //dataGridViewA.Enabled=true;

      string[] arr = new string[7];

      while (reader.Read())
      {
        for ( int i = 0; i<7; i++)
        {
          arr[i]="";
          if (reader.IsDBNull(i)) {
            arr[i]="";
          } else {
            arr[i]= reader.GetDataTypeName(i);
            string stmp = arr[i].ToUpper();
            if (stmp=="DECIMAL") arr[i] = reader.GetValue(i).ToString();
            if (stmp=="NUMBER") arr[i] = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
            if (stmp=="VARCHAR2") arr[i] = reader.GetString(i);
            if (stmp=="NVARCHAR") arr[i] = reader.GetString(i);
             if (stmp=="WVARCHAR") arr[i] = reader.GetString(i);
            if (stmp=="TEXT") arr[i] = reader.GetString(i);
            if (stmp=="INTEGER") arr[i] = reader.GetValue(i).ToString();
          }
        }

         for (int ii = 0; ii < dataGridViewA.RowCount ; ii++)
          if (dataGridViewA.Rows[ii].Cells[1].Value.ToString()==arr[2]) {
            dataGridViewA.Rows[ii].Cells[0].Value =true ;
            dataGridViewA.Rows[ii].HeaderCell.Style.BackColor = Color.LightGreen ;
            dataGridViewA.Rows[ii].DefaultCellStyle.BackColor = Color.LightGreen ;
          }

       } // while
       reader.Close();

    }



    void _setDBS()
    {
       treeViewS.BeginUpdate(); //добавить
       treeViewS.Nodes.Clear();

       TreeNode rootNode1 = new TreeNode();
       rootNode1.Name = "0";
       rootNode1.Text = "Справочники подсистемы архивов";
       treeViewS.Nodes.Add(rootNode1);

       TreeNode Nd0 = new TreeNode();
       Nd0.Name = "ARC_GINFO";
       Nd0.Text = "Таблица групп архивов (ARC_GINFO)";
       rootNode1.Nodes.Add(Nd0);

       TreeNode Nd00 = new TreeNode();
       Nd00.Name = "ARC_SUBSYST_PROFILE";
       Nd00.Text = "Список типов архивов, поддерживаемых подсистемами комплекса (ARC_SUBSYST_PROFILE)";
       rootNode1.Nodes.Add(Nd00);

       TreeNode Nd1 = new TreeNode();
       Nd1.Name = "ARC_FTR";
       Nd1.Text = "Справочная таблица опций записи архивов (ARC_FTR)";
       rootNode1.Nodes.Add(Nd1);

       TreeNode Nd2 = new TreeNode();
       Nd2.Name = "ARC_TYPE";
       Nd2.Text = "Справочная таблица типов архивов (ARC_TYPE)";
       rootNode1.Nodes.Add(Nd2);

       TreeNode Nd3 = new TreeNode();
       Nd3.Name = "ARC_SERVICES_TYPE";
       Nd3.Text = "Таблица типов сервисов (ARC_SERVICES_TYPE)";
       rootNode1.Nodes.Add(Nd3);

       TreeNode Nd31 = new TreeNode();
       Nd31.Name = "ARC_SERVICES_TUNE";
       Nd31.Text = "Настройка хранения архивов подсистем в определенной БД Архивов (ARC_SERVICES_TUNE)";
       rootNode1.Nodes.Add(Nd31);


       TreeNode Nd4 = new TreeNode();
       Nd4.Name = "ARC_DB_SCHEMA";
       Nd4.Text = "Таблица описания схем владельцев архивов (ARC_DB_SCHEMA)";
       rootNode1.Nodes.Add(Nd4);

       TreeNode Nd5 = new TreeNode();
       Nd5.Name = "ARC_READ_DEFAULTS";
       Nd5.Text = "Типы архивов, возвр. сервером оперативного доступа по умолч. (ARC_READ_DEFAULTS)";
       rootNode1.Nodes.Add(Nd5);

       TreeNode Nd6 = new TreeNode();
       Nd6.Name = "ARC_INTEGRITY_DESC";
       Nd6.Text = "Состояния - для журнала управления целостностью архивов (ARC_INTEGRITY_DESC)";
       rootNode1.Nodes.Add(Nd6);

       TreeNode rootNode2 = new TreeNode();
       rootNode2.Name = "0";
       rootNode2.Text = "Справочники системные";
       treeViewS.Nodes.Add(rootNode2);

       TreeNode Nd7 = new TreeNode();
       Nd7.Name = "SYS_GTOPT";
       Nd7.Text = "Виды характеристик параметров поддерживаемых системой (SYS_GTOPT)";
       rootNode2.Nodes.Add(Nd7);

       TreeNode Nd71 = new TreeNode();
       Nd71.Name = "SYS_ATYP";
       Nd71.Text = "Типы агрегированной информации (SYS_ATYP)";
       rootNode2.Nodes.Add(Nd71);

       TreeNode Nd8 = new TreeNode();
       Nd8.Name = "SYS_GTYP";
       Nd8.Text = "Глобальные типы данных (SYS_GTYP)";
       rootNode2.Nodes.Add(Nd8);

       TreeNode Nd9 = new TreeNode();
       Nd9.Name = "SYS_TBLLST";
       Nd9.Text = "Глобальные типы данных (SYS_TBLLST)";
       rootNode2.Nodes.Add(Nd9);


       TreeNode rootNode3 = new TreeNode();
       rootNode3.Name = "0";
       rootNode3.Text = "Справочники";
       treeViewS.Nodes.Add(rootNode3);

       TreeNode Nd10 = new TreeNode();
       Nd10.Name = "AD_SERVICE";
       Nd10.Text = "Таблица характеристик типовых сервисов (AD_SERVICE)";
       rootNode3.Nodes.Add(Nd10);


       TreeNode rootNode4 = new TreeNode();
       rootNode4.Name = "0";
       rootNode4.Text = "Таблицы";
       treeViewS.Nodes.Add(rootNode4);

       TreeNode Nd11 = new TreeNode();
       Nd11.Name = "ARC_SERVICES_INFO";
       Nd11.Text = "Соответствия сервисов хранения архивов (ARC_SERVICES_INFO)";
       rootNode4.Nodes.Add(Nd11);

       TreeNode Nd12 = new TreeNode();
       Nd12.Name = "ARC_SERVICES_ACCESS";
       Nd12.Text = "Настройка глубины оперативного буфера серверов (ARC_SERVICES_ACCESS)";
       rootNode4.Nodes.Add(Nd12);

       TreeNode Nd13 = new TreeNode();
       Nd13.Name = "ARC_VIEW_PARTITIONS";
       Nd13.Text = "Описания разделов стека для архивов мгновенных значений (ARC_VIEW_PARTITIONS)";
       rootNode4.Nodes.Add(Nd13);

       TreeNode Nd14 = new TreeNode();
       Nd14.Name = "ARC_READ_VIEW";
       Nd14.Text = "Связи для доступа к архивам (ARC_READ_VIEW)";
       rootNode4.Nodes.Add(Nd14);

       TreeNode rootNode5 = new TreeNode();
       rootNode5.Name = "0";
       rootNode5.Text = "Журналы";
       treeViewS.Nodes.Add(rootNode5);

       TreeNode Nd15 = new TreeNode();
       Nd15.Name = "ARC_STAT";
       Nd15.Text = "Статистика записи архивов (ARC_STAT)";
       rootNode5.Nodes.Add(Nd15);

       TreeNode Nd16 = new TreeNode();
       Nd16.Name = "ARC_INTEGRITY";
       Nd16.Text = "Журнал управления целостностью архивов (ARC_INTEGRITY)";
       rootNode5.Nodes.Add(Nd16);

       TreeNode Nd17 = new TreeNode();
       Nd17.Name = "J_ARC_HIST_CLEAR";
       Nd17.Text = "Журнал выполнения чисток архивов параметров по разделам (J_ARC_HIST_CLEAR)";
       rootNode5.Nodes.Add(Nd17);

       TreeNode Nd18 = new TreeNode();
       Nd18.Name = "J_ARC_RESTORE";
       Nd18.Text = "Журнал восстановления пропусков архивных таблиц параметров (J_ARC_RESTORE)";
       rootNode5.Nodes.Add(Nd18);

       TreeNode Nd19 = new TreeNode();
       Nd19.Name = "J_ARC_VAL_CHANGE";
       Nd19.Text = "Журнал изменения значений архивных таблиц параметров (J_ARC_VAL_CHANGE)";
       rootNode5.Nodes.Add(Nd19);

       TreeNode Nd20 = new TreeNode();
       Nd20.Name = "J_MOVE_STACK";
       Nd20.Text = "Журнал разбора подсистемы архивов (J_MOVE_STACK)";
       rootNode5.Nodes.Add(Nd20);

       TreeNode Nd21 = new TreeNode();
       Nd21.Name = "J_RSDU_ERROR";
       Nd21.Text = "Журнал ошибок РСДУ (J_RSDU_ERROR)";
       rootNode5.Nodes.Add(Nd21);

       TreeNode Nd22 = new TreeNode();
       Nd22.Name = "MEAS_ARC";
       Nd22.Text = "Описание таблиц хранения архивов измерений (MEAS_ARC)";
       rootNode5.Nodes.Add(Nd22);

       TreeNode Nd23 = new TreeNode();
       Nd23.Name = "DG_ARC";
       Nd23.Text = "Описание таблиц хранения архивов диспетчерских графиков (DG_ARC)";
       rootNode5.Nodes.Add(Nd23);

       TreeNode Nd24 = new TreeNode();
       Nd24.Name = "DA_ARC";
       Nd24.Text = "Описание таблиц хранения архивов параметров подсистемы сбора (DA_ARC)";
       rootNode5.Nodes.Add(Nd24);

       TreeNode Nd25 = new TreeNode();
       Nd25.Name = "CALC_ARC";
       Nd25.Text = "Описание таблиц хранения архивов универсального дорасчета (CALC_ARC)";
       rootNode5.Nodes.Add(Nd25);


       treeViewA.EndUpdate(); //добавить

    }

    void TreeViewSAfterSelect(object sender, TreeViewEventArgs e)
    {
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
       string id_parent=treeViewS.SelectedNode.Name;
       if (id_parent=="0") return ;

       AddLogString("Tb=" + id_parent);

       //удалим все строки из dataGridView1
       //while (0 != dataGridViewS.Columns.Count)
       //        dataGridViewS.Columns.RemoveAt(0);

       cmd0.CommandText="select * from " + id_parent;
       Application.DoEvents();

       dataSetS.Clear();
       dataGridViewS.DataSource = null;
       dataSetS.Tables.Clear();

       //bindingSourceS.Clear();
       //bindingNavigatorS.BindingSource.Clear();

       // Указываем запрос для выполнения
       adapter.SelectCommand = cmd0;
       // Заполняем объект источника данных
       adapter.Fill(dataSetS,id_parent);

       // (с этого момента она будет отображать его содержимое)
       dataGridViewS.DataSource = dataSetS.Tables[0];
       //dataGridViewS.DataMember=id_parent;

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
    void Button1Click(object sender, EventArgs e)
    {
      if (dataGridView1.DataSource==null || dataGridView1.DataSource=="" ) return ;

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
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridView1.SelectAll();
            Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            string pattern = @"^;(.*)$";
            string str = (Clipboard.GetText(TextDataFormat.Text)).Replace(" ", ";");
            str=Regex.Replace(str, pattern, "$1",RegexOptions.Multiline);
            File.WriteAllText(filename, str, Encoding.UTF8);
            if (objectSave != null)
            {
                Clipboard.SetDataObject(objectSave);
            }
            dataGridView1.ClearSelection();
         break;
         default :
                string fileCSV = "";
                for (int f = 0; f < dataGridView1.ColumnCount; f++)
                    fileCSV += (dataGridView1.Columns[f].HeaderText + ";");
                fileCSV += "\t\n";
                for (int i = 0; i < dataGridView1.RowCount -1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        fileCSV += ( dataGridView1[j, i].Value).ToString() + ";";
                    fileCSV += "\t\n";
                }
                StreamWriter wr = new StreamWriter(filename, false, Encoding.UTF8); // Encoding.GetEncoding("windows-1251")
                wr.Write(fileCSV);
                wr.Close();
          break ;
      }
      String s1="filename="+filename + " -> Save";
      AddLogString(s1); toolStripStatusLabel2.Text = s1;
      //MessageBox.Show("Export to \n " + filename + " \n done");
    }
    void Timer1Tick(object sender, EventArgs e)
    {
       DateTime end = DateTime.Now;
       //LocalDateTime end = LocalDateTime.now();
       //toolStripStatusLabel1.Text = DateTime.Today.ToLongDateString();
       toolStripStatusLabel1.Text = end.ToString();

       string s1=end.ToString("ss");
       int num = Convert.ToInt32(s1, 10);

       // && tabPage1.Focused==true  dataGridView1.Focused==true
       if (radioButton2.Checked==true)
       {
         if (num%10==0) {
           _getDBv1("arc_stat_current_v");
           AddLogString("Timer1->arc_stat_current_v");
           toolStripStatusLabel2.Text = "..update.."+s1;
         }
       } else {
         if (num%30==0) {
           toolStripStatusLabel2.Text = "";
         }
       }

    }
    void ToolStripButtonCommitClick(object sender, EventArgs e)
    {

       string id_parent=treeViewS.SelectedNode.Name;
       if (id_parent=="0") return ;
       AddLogString("Commit=" + id_parent);

       try
       {
         using (OdbcDataAdapter adapter = new OdbcDataAdapter()) {
             OdbcCommand cmd0 = new OdbcCommand();
             cmd0.Connection=this._conn;
             //adapter.UpdateCommand = cmd0;
             cmd0.CommandText="select * from " + id_parent;
             // Указываем запрос для выполнения
             adapter.SelectCommand = cmd0;
             OdbcCommandBuilder cb;
             cb = new OdbcCommandBuilder(adapter);
             adapter.UpdateCommand = cb.GetUpdateCommand();
             string s = adapter.UpdateCommand.CommandText;
             AddLogString(s);
             adapter.Update(dataSetS,id_parent);
        }
       }
       catch (Exception ex1)
       {
         AddLogString(" Exception= "+ex1.Message );
       }


    }

    void DataGridViewASelectionChanged(object sender, EventArgs e)
    {
      if (dataGridViewA.SelectedRows.Count==0) {
        AddLogString(" DataGridViewASelectionChanged-> = 0"   );
        return  ;
      }

      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      if (selRowNum<0) {
        AddLogString(" DataGridViewASelectionChanged-> = " + selRowNum.ToString()  );
        return  ;
      }

      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;
      //String ck = dataGridViewA.Rows[selRowNum].Cells[0].Value.ToString() ;

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      //Получить Имя выделенного элемента
      string ID_TBLLST=treeViewA.SelectedNode.Name;
      AddLogString("DataGridViewACellContentClick-> ID_TBLLST=" + ID_TBLLST + "  ID_GINFO=" + ID_GINFO);

string sl1="SELECT  ARC_SUBSYST_PROFILE.ID,ARC_SUBSYST_PROFILE.ID_TBLLST,ARC_SUBSYST_PROFILE.ID_GINFO," +
"ARC_SUBSYST_PROFILE.IS_WRITEON, ARC_SUBSYST_PROFILE.STACK_NAME,ARC_SUBSYST_PROFILE.LAST_UPDATE,ARC_SUBSYST_PROFILE.IS_VIEWABLE  " +
"FROM ARC_SUBSYST_PROFILE " +
"WHERE ARC_SUBSYST_PROFILE.ID_TBLLST=" + ID_TBLLST + " AND ARC_SUBSYST_PROFILE.ID_GINFO="+ID_GINFO ;

      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" DataGridViewACellContentClick-> выполнение алгоритма  - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
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

      string[] arr = new string[7];

      if (reader.HasRows) {
        while (reader.Read())
        {
          for ( int i = 0; i<7; i++)
          {
            arr[i]="";
            if (reader.IsDBNull(i)) {
              arr[i]="";
            } else {
              arr[i]= reader.GetDataTypeName(i);
              string stmp = arr[i].ToUpper();
              if (stmp=="DECIMAL") arr[i] = reader.GetValue(i).ToString();
              if (stmp=="NUMBER") arr[i] = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
              if (stmp=="VARCHAR2") arr[i] = reader.GetString(i);
              if (stmp=="NVARCHAR") arr[i] = reader.GetString(i);
              if (stmp=="WVARCHAR") arr[i] = reader.GetString(i);
              if (stmp=="TEXT") arr[i] = reader.GetString(i);
              if (stmp=="INTEGER") arr[i] = reader.GetValue(i).ToString();
            }
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
        _profileData.LAST_UPDATE=arr[5];
        bool v2 = true ;
        if (arr[6]=="0") v2 = false ;
        _profileData.IS_VIEWABLE=v2 ;

        buttonSave.Enabled=true ;
      }

      reader.Close();

      propertyGridA.SelectedObject=_profileData;
      propertyGridA.Update();

      dataGridViewA.EndEdit();
    }
    void ToolStripMenuItem1Click(object sender, EventArgs e)
    {
      // modify
      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;

      Form ifrm = new FormArcGinfo(this._conn, ID_GINFO);
      ifrm.ShowDialog();
      TreeViewAAfterSelect(treeViewA, null);
      //dataGridViewA.FirstDisplayedScrollingRowIndex=selRowNum;

      dataGridViewA.CurrentCell = dataGridViewA.Rows[selRowNum].Cells[0] ;
      //    dataGridViewA.Rows[selRowNum].Selected=true ;

      dataGridViewA.Update();
    }
    void ToolStripMenuItem2Click(object sender, EventArgs e)
    {
      // copy
      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;
      string id_dest = "";
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string sl1= "SELECT ID+1 FROM ARC_GINFO WHERE ID+1 NOT IN (SELECT ID FROM ARC_GINFO )" ; //  AND id > 3
      AddLogString(" sql  = " + sl1);
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" ToolStripMenuItem2Click-> выполнение алгоритма  - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
        return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
          int i = 0 ;
          string sn="";
          if (reader.IsDBNull(i)) {
              ;
          } else {
              sn= reader.GetDataTypeName(i);
              string stmp = sn.ToUpper();
              if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
              if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
              if (stmp=="VARCHAR2") sn = reader.GetString(i);
              if (stmp=="NVARCHAR") sn = reader.GetString(i);
              if (stmp=="WVARCHAR") sn = reader.GetString(i);
              if (stmp=="TEXT") sn = reader.GetString(i);
              if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
          }
          id_dest = sn ;
          break ;
        } // while
      }
      reader.Close();

      if (id_dest=="")
      {
        AddLogString(" ToolStripMenuItem2Click-> выполнение алгоритма  - прервано.. id_dest = " + id_dest);
        return ;
      }

      sl1="INSERT INTO  ARC_GINFO (" +
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
"    ARC_GINFO.ID_GTOPT ,  " +
"    ARC_GINFO.ID_TYPE, " +
"    ARC_GINFO.DEPTH , " +
"    ARC_GINFO.DEPTH_LOCAL , " +
"    ARC_GINFO.CACHE_SIZE , " +
"    ARC_GINFO.CACHE_TIMEOUT , " +
"    ARC_GINFO.FLUSH_INTERVAL , " +
"    ARC_GINFO.RESTORE_INTERVAL , " +
"    ARC_GINFO.STACK_INTERVAL , " +
"    ARC_GINFO.WRITE_MINMAX  , " +
"    ARC_GINFO.RESTORE_TIME , " +
"    ARC_GINFO.NAME , " +
"    ARC_GINFO.STATE , " +
"    ARC_GINFO.DEPTH_PARTITION  , " +
"    ARC_GINFO.RESTORE_TIME_LOCAL " +
" FROM ARC_GINFO " +
" WHERE ARC_GINFO.ID=" + ID_GINFO + " ;";

      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" ToolStripMenuItem2Click-> выполнение алгоритма  - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
        reader.Close();
        return ;
      }
      reader.Close();
      TreeViewAAfterSelect(sender, null);
      AddLogString(" Копия создана в id = " + id_dest );
      MessageBox.Show ("Профиль id=" +ID_GINFO + " скопирован в id = " + id_dest ,"Копирование строк", MessageBoxButtons.OK, MessageBoxIcon.Information);

    }
    void ToolStripMenuItem3Click(object sender, EventArgs e)
    {
      // delete
      int selRowNum = dataGridViewA.CurrentCell.RowIndex;
      string ID_GINFO = dataGridViewA.Rows[selRowNum].Cells[1].Value.ToString() ;

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string sl1="SELECT  count(*) FROM ARC_SUBSYST_PROFILE WHERE ID_GINFO="+ID_GINFO ;

      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" ToolStripMenuItem3Click-> выполнение алгоритма  - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
        return ;
      }

      int cnt=0;

      if (reader.HasRows) {
        while (reader.Read())
        {
          string sn = "";
          int i = 0 ;
          if (reader.IsDBNull(i)) {
              sn="";
            } else {
              sn= reader.GetDataTypeName(i);
              string stmp = sn.ToUpper();
              if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
              if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
              if (stmp=="VARCHAR2") sn = reader.GetString(i);
              if (stmp=="NVARCHAR") sn = reader.GetString(i);
              if (stmp=="WVARCHAR") sn = reader.GetString(i);
              if (stmp=="TEXT") sn = reader.GetString(i);
              if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
            }

          cnt = Convert.ToInt32(sn);
       } // while
      }
      reader.Close();

      if (cnt==0) {
        DialogResult result = MessageBox.Show ("Действительно удалить ? строку профиля архива id=" +ID_GINFO ,"Удаление строки", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
          cmd0.CommandText="DELETE FROM ARC_GINFO WHERE ID="+ID_GINFO ;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            AddLogString(" ToolStripMenuItem3Click-> delete - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
            return ;
          }
          reader.Close();

          TreeViewAAfterSelect(sender, null );
          MessageBox.Show ("Профиль архива id=" +ID_GINFO + " удален!" ,"Удаление строки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      } else {
       MessageBox.Show ("Профиль id=" +ID_GINFO + " используется, удаление невозможно." ,"Удаление строки", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }

    }
    void DataGridViewACellContentClick(object sender, DataGridViewCellEventArgs e)
    {

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      int chkBoxColumnIndex = 0;
      var dataGridView = (DataGridView)sender;
      if (e.ColumnIndex == chkBoxColumnIndex)
      {
        dataGridView.EndEdit();
        var isChecked = Convert.ToBoolean(dataGridView[e.ColumnIndex, e.RowIndex].Value);

        int selRowNum = dataGridView.CurrentCell.RowIndex;
        string ID_GINFO = dataGridView.Rows[selRowNum].Cells[1].Value.ToString() ;

        string nm1 = dataGridView.Rows[selRowNum].Cells[2].Value.ToString() ;
        string nm2 = dataGridView.Rows[selRowNum].Cells[15].Value.ToString() ;

        if (isChecked)
        {

          _profileData=(ARC_SUBSYST_PROFILE)propertyGridA.SelectedObject;
          String ID= _profileData.ID ;
          if (ID=="") {
              return ;
          }

          DialogResult result = MessageBox.Show ("Деактивировать профиль?\n\n\n" +
          " id = " + ID + " '" + nm2 + "'\n" +
          " ("+ID_GINFO +"),'"  + nm1 + "'\n"  ,
           "Деактивация..", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,
           MessageBoxDefaultButton.Button2);
          if (result == DialogResult.Yes)
          {
            // delete
            //----------------------
            //ARC_SERVICES_TUNE
            //  ID_SPROFILE = ID
            //
            //ARC_SERVICES_ACCESS
            //  ID_SPROFILE=ID
            //
            // ARC_SUBSYST_PROFILE
            //    ID
            //-----------------------

            cmd0.CommandText="DELETE FROM ARC_SERVICES_TUNE WHERE ID_SPROFILE="+ID ;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception ex1)
            {
              AddLogString(" DELETE - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
              return ;
            }
            reader.Close();

            cmd0.CommandText="DELETE FROM ARC_SERVICES_ACCESS WHERE ID_SPROFILE="+ID ;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception ex1)
            {
              AddLogString(" DELETE - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
              return ;
            }
            reader.Close();

            cmd0.CommandText="DELETE FROM ARC_SUBSYST_PROFILE WHERE ID="+ID ;
            try
            {
              reader = cmd0.ExecuteReader();
            }
            catch (Exception ex1)
            {
              AddLogString(" DELETE - прервано.. = " + cmd0.CommandText + " " + ex1.Message);
              return ;
            }
            reader.Close();

            AddLogString(" DELETE - Ok = " + ID);
            TreeViewAAfterSelect(treeViewA, null);

          }

         } else {
           DialogResult result = MessageBox.Show ("Создать новый профиль для id = " + ID_GINFO + " ?" ,"Активация профиля", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation );
           if (result == DialogResult.Yes)
           {
              // new create
              ;
              //Получить Имя выделенного элемента
              string id_parent=treeViewA.SelectedNode.Name;
              if (id_parent=="0") return ;
              string id_tbl =Convert.ToString(treeViewA.SelectedNode.Tag) ;
              AddLogString("new create=id_tbl=" + id_tbl);
              string id_name=treeViewA.SelectedNode.Text ;
              AddLogString("new create=" + id_parent + " " + id_name);
              if (id_tbl=="0" || id_tbl=="") return ;

              DialogResult result1;

              Form ifrmC = new FormProfileCreate(this._conn, ID_GINFO, id_tbl);
              result1=ifrmC.ShowDialog();

              //if(result1 == DialogResult.Yes)
              //{

                AddLogString("new create = Ok");

                TreeViewAAfterSelect(treeViewA, null);
                //dataGridViewA.FirstDisplayedScrollingRowIndex=selRowNum;

                dataGridViewA.CurrentCell = dataGridViewA.Rows[selRowNum].Cells[0] ;
                //dataGridViewA.Rows[selRowNum].Selected=true ;
                DataGridViewASelectionChanged(dataGridViewA, null);

                //dataGridViewA.Update();
              //}

              ifrmC.Dispose();

           }
        } // if (isChecked)

      }

    }
    void Button2Click(object sender, EventArgs e)
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

       string strS = "UPDATE ARC_SUBSYST_PROFILE SET " +
          " IS_WRITEON="+ IS_WRITEON +
          ", IS_VIEWABLE=" + IS_VIEWABLE +
          ", STACK_NAME='" + STACK_NAME + "'" +
          " WHERE ID=" + ID;

       AddLogString(" SAVE -> " + strS);

       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();
       OdbcDataReader reader = null ;

       cmd0.Connection=this._conn;

       cmd0.CommandText=strS;
       try
       {
         reader = cmd0.ExecuteReader();
       }
       catch (Exception ex1)
       {
         AddLogString(" SAVE -> выполнение алгоритма  - прервано." + ex1.Message);
         return ;
       }
       reader.Close();

       AddLogString(" SAVE -> done ");

    }
    void DataGridViewACellClick(object sender, DataGridViewCellEventArgs e)
    {
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



  }
}
