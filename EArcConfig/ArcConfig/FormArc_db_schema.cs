/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 21.12.2023
 * Time: 17:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Reflection;
using System.Resources;
using System.IO;
using System.Text;

namespace ArcConfig
{
	/// <summary>
	/// Description of FormArc_db_schema.
	/// </summary>
	public partial class FormArc_db_schema : Form
	{
		public FormArc_db_schema()
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
    public String _id_tbl;
    public int _OptionSchemaName = 0;
    public string OptionSchemaMain = "RSDUADMIN";

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormArc_db_schema(OdbcConnection conn, String tbl, int SchemaName)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      _conn = conn ;
      _id_tbl = tbl ;
      _OptionSchemaName = SchemaName ;
    }		
		
    void Load1()
    {
        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;

        cmd0.Connection=this._conn;


        string stSchema="";
        if (_OptionSchemaName>0) {
          stSchema=OptionSchemaMain + "." ;
        }


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
          return ;
        }

        String ARC_NAME = "" ;
       // _SCHEME_NAME = "" ;
        if (reader.HasRows) {
          while (reader.Read())
          {
            ARC_NAME = GetTypeValue(ref reader, 2).ToUpper() ;
            //_SCHEME_NAME = GetTypeValue(ref reader, 3).ToUpper() ;
            break ;
          } // while
        }
        reader.Close();

        if (ARC_NAME=="") return ;
        Application.DoEvents();


//          MessageBox.Show(ex1.Message);
        int rq = 86400;
//        int.TryParse(ext1[2], out rq);


    }		
		
    void Select1(object sender)
    {
       /*
       событие после выбора таблицы
       */

       // Объект для связи между базой данных и источником данных
       OdbcDataAdapter adapter = new OdbcDataAdapter();
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd0 = new OdbcCommand();

       cmd0.Connection=this._conn;

       string table_name="ARC_DB_SCHEMA";

       string stSchema="";
       if (_OptionSchemaName>0) {
           stSchema=OptionSchemaMain + "." ;
       }

       Application.DoEvents();

       cmd0.CommandText="SELECT * FROM " + stSchema + table_name;
       
cmd0.CommandText="select ads.ID, ads.NAME, ads.SCHEMA_NAME , ast.DEFINE_ALIAS as STORAGE " + 
"from " + stSchema + "ARC_DB_SCHEMA ads, " + stSchema + "ARC_STORAGE_TYPE ast " +
"where ads.ID_STORAGE_TYPE=ast.ID " +
"order by ads.ID asc " ;

       dataSet1.Clear();
       dataGridView1.DataSource = null;
       dataSet1.Tables.Clear();

       // Указываем запрос для выполнения
       adapter.SelectCommand = cmd0;
	   
	   OdbcCommandBuilder builder = new OdbcCommandBuilder(adapter);
	   
       // Заполняем объект источника данных
       adapter.Fill(dataSet1,table_name);

       // (с этого момента она будет отображать его содержимое)
       dataGridView1.DataSource = dataSet1.Tables[0];
       //dataGridView1.DataMember=table_name;

       bindingSource1.DataSource=dataSet1.Tables[0];
       bindingNavigator1.BindingSource = bindingSource1;


       // Resize the master DataGridView columns to fit the newly loaded data.
       dataGridView1.AutoResizeColumns();

       // Configure the details DataGridView so that its columns automatically
       // adjust their widths when the data changes.
       dataGridView1.AutoSizeColumnsMode =
             DataGridViewAutoSizeColumnsMode.AllCells;
       dataGridView1.AutoGenerateColumns = true;

       dataGridView1.EnableHeadersVisualStyles = false;
       //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor =Color.LightGray;

       dataGridView1.Update();

    }
		void FormArc_db_schemaLoad(object sender, EventArgs e)
		{
			Select1(sender) ;
		}		
		
		
	}
}
