/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 18.08.2022
 * Time: 11:27
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


/*
CREATE TABLE SYS_GTOPT
(
  ID            NUMBER(11),
  NAME          VARCHAR2(255 BYTE),
  ALIAS         VARCHAR2(255 BYTE),
  ID_GTYPE      NUMBER(11),
  DEFINE_ALIAS  VARCHAR2(63 BYTE),
  INTERVAL      NUMBER(11),
  ID_ATYPE      NUMBER(11)
)
*/

namespace ArcConfig
{
  /// <summary>
  /// Description of FormGTOPT.
  /// </summary>
  public partial class FormGTOPT : Form
  {
    public FormGTOPT()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
    }

    public FormGTOPT(OdbcConnection conn, int SchemaName)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      _conn = conn ;
      //id_arcginfo = id ;
      trigger  = 0 ;
      _OptionSchemaName = SchemaName ;
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
    public String id_arcginfo;
    public int trigger  = 0 ;
    public int _OptionSchemaName ;
    public String OptionSchemaMain = "RSDUADMIN" ;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    string GtID( string iID )
    {
      string id_dest = "" ;

      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      // re-read
      Application.DoEvents();


      string stSchema="";
      if (_OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
      }


      string sl1= "SELECT ID+1001 FROM "+stSchema+"SYS_GTOPT WHERE ID+1001 NOT IN (SELECT ID FROM "+stSchema+"SYS_GTOPT )" ;
      if (trigger == 1 )
        sl1= "SELECT ID+1 FROM "+stSchema+"SYS_GTOPT WHERE ID+1 NOT IN (SELECT ID FROM "+stSchema+"SYS_GTOPT )" ;

      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        return "";
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
          id_dest = GetTypeValue(ref reader, 0);
          break ;
        } // while
      }
      reader.Close();

      return(id_dest);
    }


    void GtLoad( int ID_GTYPE, int ID_ATYPE )
    {
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcCommand cmd1 = new OdbcCommand();
      //OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;
      cmd1.Connection=this._conn;

      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

      string sl1 = r.GetString("GTOPT2");

      // re-read
      Application.DoEvents();

      dataSet1.Clear();

      cmd0.CommandText= sl1 ;

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

      dataSet1.Tables[0].DefaultView.RowFilter= "" ;

      string wh = "" ;
      switch (ID_ATYPE)
      {
          case 4: //4 Нарастающий итог  AGREGAT_TYPE_PROGRESSIVE_TOTAL
              wh="ADEF Like '%AGREGAT_TYPE_PROGRESSIVE_TOTAL%' ";
              break;
          case 1: //1 Интегральные  AGREGAT_TYPE_INTEGRAL
              wh="ADEF Like '%AGREGAT_TYPE_INTEGRAL%' ";
              break;
          case 2: //2 Усредненные на границе интервала  AGREGAT_TYPE_AVERAGE
              wh="ADEF Like '%AGREGAT_TYPE_AVERAGE%' ";
              break;
          case 3: //3 Мгновенные  AGREGAT_TYPE_INSTANT
              wh="ADEF Like '%AGREGAT_TYPE_INSTANT%' ";
              break;
          case 5: //5 Среднее AGREGAT_TYPE_MEAN
              wh="ADEF Like '%AGREGAT_TYPE_MEAN%' ";
              break;
          case 6: //6 прочее
              wh="((ADEF is NULL) or ADEF='')";
              break;
          default:
              ;
              break;
      }

      string wh2 = "" ;
      switch (ID_GTYPE)
      {
          case 1: //1 Непрерывные (аналоговые) данные GLOBAL_TYPE_ANALOG
              wh2="GTYPD Like '%GLOBAL_TYPE_ANALOG%' ";
              break;
          case 2: //2 Цифровые данные GLOBAL_TYPE_DIGIT
              wh2="GTYPD Like '%GLOBAL_TYPE_DIGIT%' ";
              break;
          case 3: //3 Данные состояния (булевые)  GLOBAL_TYPE_BOOL
              wh2="GTYPD Like '%GLOBAL_TYPE_BOOL%' ";
              break;
          case 4: //4 Двойные аналоговые  GLOBAL_TYPE_DANALOG
              wh2="GTYPD Like '%GLOBAL_TYPE_DANALOG%' ";
              break;
          case 5: //5 Двойные цифровые  GLOBAL_TYPE_DDIGIT
              wh2="GTYPD Like '%GLOBAL_TYPE_DDIGIT%' ";
              break;
          case 6: //6 Двойные булевые GLOBAL_TYPE_DBOOL
              wh2="GTYPD Like '%GLOBAL_TYPE_DBOOL%' ";
              break;
          case 7: //7 Большие двоичные  GLOBAL_TYPE_BLOB
              wh2="GTYPD Like '%GLOBAL_TYPE_BLOB%' ";
              break;
          case 8: //8 Байтовая строка GLOBAL_TYPE_STRING
              wh2="GTYPD Like '%GLOBAL_TYPE_STRING%' ";
              break;
          case 9: //9 Панель  GLOBAL_TYPE_PANEL
              wh2="GTYPD Like '%GLOBAL_TYPE_PANEL%' ";
              break;
          case 10: //10 Схема GLOBAL_TYPE_SCHEME
              wh2="GTYPD Like '%GLOBAL_TYPE_SCHEME%' ";
              break;
          case 11: //11 Сложный объект  GLOBAL_TYPE_OBJECT
              wh2="GTYPD Like '%GLOBAL_TYPE_OBJECT%' ";
              break;
          case 12: //12 Объект для перехода GLOBAL_TYPE_OBJECT_JUMP
              wh2="GTYPD Like '%GLOBAL_TYPE_OBJECT_JUMP%' ";
              break;
          case 13: //13 SQL-запрос  GLOBAL_TYPE_QUERY
              wh2="GTYPD Like '%GLOBAL_TYPE_QUERY%' ";
              break;
          case 14: //14 Команда GLOBAL_TYPE_COMMAND
              wh2="GTYPD Like '%GLOBAL_TYPE_COMMAND%' ";
              break;
          case 15: //80 Перечисление  GLOBAL_TYPE_ENUMERATION
              wh2="GTYPD Like '%GLOBAL_TYPE_ENUMERATION%' ";
              break;

          default:
              ;
              break;
      }

      string rowwh = wh ;
      if (wh!="" && wh2!="") rowwh = wh + " AND " + wh2 ;
      else
        if (wh2!="") rowwh = wh2 ;

      dataSet1.Tables[0].DefaultView.RowFilter= rowwh ;

        /*
You can use DataTable.Select:

var strExpr = "CostumerID = 1 AND OrderCount > 2";
var strSort = "OrderCount DESC";
foundRows = ds.Tables[0].Select(strExpr, strSort);

Or you can use DataView:

ds.Tables[0].DefaultView.RowFilter = strExpr;

UPDATE I'm not sure why you want to have a DataSet returned. But I'd go with the following solution:

var dv = ds.Tables[0].DefaultView;
dv.RowFilter = strExpr;
var newDS = new DataSet();
var newDT = dv.ToTable();
newDS.Tables.Add(newDT);

*/


      // (с этого момента она будет отображать его содержимое)
      dataGridView1.DataSource = dataSet1.Tables[0].DefaultView;
      //dataGridView1.DataSource = dataSet1.Tables[0].Select(rowwh);

      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridView1.AutoResizeColumns();

      // Configure the details DataGridView so that its columns automatically
      // adjust their widths when the data changes.
      dataGridView1.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.AllCells;


      //comboBoxDEFINE_ALIAS.

      for (int i = 0; i < dataGridView1.ColumnCount; i++)
      {
         String hd = dataGridView1.Columns[i].HeaderText ;
         if ("DEFINE_ALIAS"==hd) {
          comboBoxDEFINE_ALIAS.Items.Clear();
          for (int j = 0; j < dataGridView1.RowCount ; j++)
            {
            var v1 = dataGridView1.Rows[j].Cells[i].Value ;
            comboBoxDEFINE_ALIAS.Items.Insert(j,v1.ToString());
          }
          break ;
         }
      }

      // нумерация строк
      for (int j = 0; j < dataGridView1.RowCount ; j++)
      {
        dataGridView1.Rows[j].HeaderCell.Value = (j + 1).ToString();
      }

    }

    void FormGTOPTLoad(object sender, EventArgs e)
    {
//4 Нарастающий итог  AGREGAT_TYPE_PROGRESSIVE_TOTAL
//1 Интегральные  AGREGAT_TYPE_INTEGRAL
//2 Усредненные на границе интервала  AGREGAT_TYPE_AVERAGE
//3 Мгновенные  AGREGAT_TYPE_INSTANT
//5 Среднее AGREGAT_TYPE_MEAN

        comboBoxID_ATYPE.Items.Insert(0,"Нет");
        comboBoxID_ATYPE.Items.Insert(1,"Интегральные");
        comboBoxID_ATYPE.Items.Insert(2,"Усредненные на границе интервала");
        comboBoxID_ATYPE.Items.Insert(3,"Мгновенные");
        comboBoxID_ATYPE.Items.Insert(4,"Нарастающий итог");
        comboBoxID_ATYPE.Items.Insert(5,"Среднее");
        comboBoxID_ATYPE.Items.Insert(6,"Прочее");

        comboBoxID_ATYPE.SelectedIndex = 0 ;

//1 Непрерывные (аналоговые) данные GLOBAL_TYPE_ANALOG  Float A type defined by IEEE 754-1985 as double (OBSERVE, not as the IEEE 754-1985 float). The value range is m*2**e where the range of m is -2**52..2**52 -1 and the range of e is -1075..970. Непрерывные (аналоговые) данные
//2 Цифровые данные GLOBAL_TYPE_DIGIT     Цифровые данные
//3 Данные состояния (булевые)  GLOBAL_TYPE_BOOL  Boolean A type with the value space 0..1 where 0 means false and 1 means true.  Данные состояния (булевые)
//4 Двойные аналоговые  GLOBAL_TYPE_DANALOG     Двойные аналоговые
//5 Двойные цифровые  GLOBAL_TYPE_DDIGIT      Двойные цифровые
//6 Двойные булевые GLOBAL_TYPE_DBOOL     Двойные булевые
//7 Большие двоичные  GLOBAL_TYPE_BLOB      Большие двоичные
//8 Байтовая строка GLOBAL_TYPE_STRING  String  A type consisting of a sequence of 8 bit characters. The character encoding is UTF-8. Байтовая строка
//9 Панель  GLOBAL_TYPE_PANEL     Панель
//10  Схема GLOBAL_TYPE_SCHEME      Схема
//11  Сложный объект  GLOBAL_TYPE_OBJECT      Сложный объект
//12  Объект для перехода GLOBAL_TYPE_OBJECT_JUMP     Объект для перехода
//13  SQL-запрос  GLOBAL_TYPE_QUERY     SQL-запрос
//14  Команда GLOBAL_TYPE_COMMAND     Команда
//80  Перечисление  GLOBAL_TYPE_ENUMERATION     Перечисление

       comboBoxID_GTYPE.Items.Insert(0,"Нет");
       comboBoxID_GTYPE.Items.Insert(1,"Непрерывные (аналоговые) данные");
       comboBoxID_GTYPE.Items.Insert(2,"Цифровые данные");
       comboBoxID_GTYPE.Items.Insert(3,"Данные состояния (булевые)");
       comboBoxID_GTYPE.Items.Insert(4,"Двойные аналоговые");
       comboBoxID_GTYPE.Items.Insert(5,"Двойные цифровые");
       comboBoxID_GTYPE.Items.Insert(6,"Двойные булевые");
       comboBoxID_GTYPE.Items.Insert(7,"Большие двоичные");
       comboBoxID_GTYPE.Items.Insert(8,"Байтовая строка");
       comboBoxID_GTYPE.Items.Insert(9,"Панель");
       comboBoxID_GTYPE.Items.Insert(10,"Схема");
       comboBoxID_GTYPE.Items.Insert(11,"Сложный объект");
       comboBoxID_GTYPE.Items.Insert(12,"Объект для перехода");
       comboBoxID_GTYPE.Items.Insert(13,"SQL-запрос");
       comboBoxID_GTYPE.Items.Insert(14,"Команда");
       comboBoxID_GTYPE.Items.Insert(15,"Перечисление");

       comboBoxID_GTYPE.SelectedIndex = 0 ;

       GtLoad(0,0);
       textBoxID.Text=GtID( "" ) ;

    }
    void CheckBox1CheckedChanged(object sender, EventArgs e)
    {
       //
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();

       cmd0.Connection=this._conn;

       string stSchema="";
       //if (_OptionSchemaName>0) {
         stSchema=OptionSchemaMain + "." ;
       //}

       // SYS_GTOPT_ID_CHK_TR
       // ALTER TRIGGER     ENABLE
       // ALTER TRIGGER     DISABLE
       if (checkBox1.Checked) {
           cmd0.CommandText="ALTER TRIGGER "+stSchema+"SYS_GTOPT_ID_CHK_TR DISABLE ; " ;
           trigger = 1;
       } else {
           cmd0.CommandText="ALTER TRIGGER "+stSchema+"SYS_GTOPT_ID_CHK_TR ENABLE ; " ;
           trigger = 0;
       }

       try
       {
         cmd0.ExecuteNonQuery();
       }
       catch (Exception ex1)
       {
         MessageBox.Show(ex1.ToString() );
         trigger = 0;
       }

       textBoxID.Text=GtID( "" ) ;

    }
    void ComboBoxID_ATYPESelectedIndexChanged(object sender, EventArgs e)
    {
       GtLoad(comboBoxID_GTYPE.SelectedIndex,comboBoxID_ATYPE.SelectedIndex);
    }
    void ComboBoxID_GTYPESelectedIndexChanged(object sender, EventArgs e)
    {
       GtLoad(comboBoxID_GTYPE.SelectedIndex,comboBoxID_ATYPE.SelectedIndex);
    }
    void ButtonCloseClick(object sender, EventArgs e)
    {
       this.Close();
    }
    void ButtonAddClick(object sender, EventArgs e)
    {

       //
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();
       OdbcDataReader reader = null ;

       cmd0.Connection=this._conn;

       String sl1 = "" ;

       string stSchema="";
       if (_OptionSchemaName>0) {
         stSchema=OptionSchemaMain + "." ;
       }


       String ID_GTYPE = comboBoxID_GTYPE.SelectedIndex.ToString() ;
       switch (comboBoxID_GTYPE.SelectedIndex)
       {
           case 15: //80 Перечисление  GLOBAL_TYPE_ENUMERATION
               ID_GTYPE="80";
               break;
           default:
               ;
               break;
       }

       if (ID_GTYPE=="0") {
         MessageBox.Show ("Не задан 'Глобальный тип данных' \n","Вставка нового Типа",MessageBoxButtons.OK, MessageBoxIcon.Error);
         return ;
       }


       String ID_GTYPE_DEFINE_ALIAS = "" ;
       cmd0.CommandText="SELECT DEFINE_ALIAS FROM "+stSchema+"SYS_GTYP WHERE ID="+ID_GTYPE ;
       try
       {
         reader = cmd0.ExecuteReader();
       }
       catch (Exception ex1)
       {
         MessageBox.Show (ex1.Message);
         return ;
       }

       if (reader.HasRows) {
         while (reader.Read())
         {
           ID_GTYPE_DEFINE_ALIAS = GetTypeValue(ref reader, 0);
           break ;
         } // while
       }
       reader.Close();



       string ID_ATYPE = comboBoxID_ATYPE.SelectedIndex.ToString() ;
       switch (comboBoxID_ATYPE.SelectedIndex)
       {
           case 6: //6 прочее
               ID_ATYPE="0";
               break;
           default:
               ;
               break;
       }

       String ID_ATYPE_DEFINE_ALIAS = "" ;
       cmd0.CommandText="SELECT DEFINE_ALIAS FROM "+stSchema+"SYS_ATYP WHERE ID="+ID_ATYPE ;
       try
       {
         reader = cmd0.ExecuteReader();
       }
       catch (Exception ex1)
       {
         MessageBox.Show (ex1.Message);
         return ;
       }

       if (reader.HasRows) {
         while (reader.Read())
         {
           ID_ATYPE_DEFINE_ALIAS = GetTypeValue(ref reader, 0);
           break ;
         } // while
       }
       reader.Close();

       if (ID_ATYPE_DEFINE_ALIAS=="") ID_ATYPE="";


       String gid=textBoxID.Text.Trim() ;
       String gname=textBoxNAME.Text.Trim() ;
       String galias=textBoxALIAS.Text.Trim() ;
       String ginterval=textBoxINTERVAL.Text.Trim() ;
       if (ginterval!="") {
          int myInt=0;
          bool isValid = int.TryParse(ginterval, out myInt);
          myInt=Math.Abs(myInt) ;
          ginterval=myInt.ToString() ;
       }
       String gdefinealias=comboBoxDEFINE_ALIAS.Text.Trim() ;

       
       
       int ret = 0 ;
       cmd0.CommandText="select count(*) from "+stSchema+"SYS_GTOPT WHERE DEFINE_ALIAS='"+gdefinealias+"' ;" ;
       try
       {
         ret=cmd0.ExecuteNonQuery();
       }
       catch (Exception ex1)
       {
       	;
       }
       
       String gdefine = "" ;
       if (ret>0) {
       	  gdefine = " - имеются повторения уникальных индентификаторов !!" ;
       }

       
       
       DialogResult result = MessageBox.Show ("Вставить новый тип ? \n\n" +
"id='"+gid+"'\n" +
"name='"+gname+"'\n" +
"alias='"+galias+"'\n" +
"interval='"+ginterval+"'\n" +
"definealias='"+gdefinealias+"' " + gdefine + "\n" +
"gtype='"+ID_GTYPE_DEFINE_ALIAS+"'\n" + "atype='"+ID_ATYPE_DEFINE_ALIAS+"'\n" ,
                                               "Вставка нового Типа",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button2);
       if (result == DialogResult.Yes)
       {
         if (ID_ATYPE=="")
            sl1="INSERT INTO " + stSchema + "SYS_GTOPT (ID, NAME, ALIAS, ID_GTYPE, DEFINE_ALIAS, INTERVAL) VALUES " +
              " ("+gid+",'"+gname+"','"+galias+"',"+ID_GTYPE+",'"+gdefinealias+"',"+ginterval+");" ;
         else
            sl1="INSERT INTO " + stSchema + "SYS_GTOPT (ID, NAME, ALIAS, ID_GTYPE, DEFINE_ALIAS, INTERVAL, ID_ATYPE) VALUES " +
              " ("+gid+",'"+gname+"','"+galias+"',"+ID_GTYPE+",'"+gdefinealias+"',"+ginterval+","+ID_ATYPE+");" ;

         cmd0.CommandText=sl1;
         ret=0;
         try
         {
           ret=cmd0.ExecuteNonQuery();
         }
         catch (Exception ex7)
         {
               ;
         }

         MessageBox.Show ("Готово, вставлено (" + ret.ToString()+ ") строк\n" ,
                          "Вставка нового Типа", MessageBoxButtons.OK, MessageBoxIcon.Information);

         if (ret>0) this.Close();
       }

    }






  }
}
