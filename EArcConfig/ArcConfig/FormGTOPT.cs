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
		
    public FormGTOPT(OdbcConnection conn)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      _conn = conn ;
      //id_arcginfo = id ;
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

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    void GtLoad( )
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

      //dataSet1.Tables[0].DefaultView.RowFilter= "DEFINE_ALIAS Like '%EL_REG_DBPART%' or DEFINE_ALIAS Like '%PH_REG_DBPART%'  or DEFINE_ALIAS Like '%AUTOMAT_DBPART%'  or DEFINE_ALIAS Like '%PWSWITCH_DBPART%' or DEFINE_ALIAS Like 'OBJECT' " ;

      // (с этого момента она будет отображать его содержимое)
      dataGridView1.DataSource = dataSet1.Tables[0].DefaultView;;

      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridView1.AutoResizeColumns();

      // Configure the details DataGridView so that its columns automatically
      // adjust their widths when the data changes.
      dataGridView1.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.AllCells;

    }

        void FormGTOPTLoad(object sender, EventArgs e)
		{
			GtLoad();
		}

    
		
		
		
	}
}
