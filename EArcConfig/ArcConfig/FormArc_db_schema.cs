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
using System.Collections.Generic;

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



    int _checkCol( string nameCol , string nameTbl )
    {
       int is_exists = 1 ;
       // Объект для выполнения запросов к базе данных
       OdbcCommand cmd = new OdbcCommand();
       OdbcDataReader reader1 = null ;

       cmd.Connection = this._conn; // уже созданное и открытое соединение

       string stSchema="";
       if (_OptionSchemaName>0) {
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

    class ARC_DB_SCHEMA
    {
        public int ID { get; set; }
		public string NAME { get; set; } //обязательно нужно использовать get конструкцию
        public string SCHEMA_NAME { get; set; }
		public int ID_STORAGE_TYPE { get; set; }
        public string DEFINE_ALIAS { get; set; }

        public string Hidden = ""; //Данное свойство не будет отображаться как колонка

        public ARC_DB_SCHEMA(int id, string name, string sch, int id_type, string defalias )
        {
            this.ID = id;
			this.NAME = name;
            this.SCHEMA_NAME = sch;
			this.ID_STORAGE_TYPE = id_type ;
            this.DEFINE_ALIAS = defalias ;
        }
    }   

    
   //public DataSet GetCountries()
   //{
   //   DataSet ds = new DataSet();
   //   OdbcDataAdapter da = new OdbcDataAdapter("select distinct country from customers", strConn);
   //   da.Fill(ds, "countries");
   //   return ds;
   //}    
    
    void Select1(object sender)
    {
       List<ARC_DB_SCHEMA> data = new List<ARC_DB_SCHEMA>(); 
       //Специальный список List с вызовом события обновления внутреннего состояния, необходимого для автообновления datagridview


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
       
/*
 * проверка на существование ARC_DB_SCHEMA.ID_STORAGE_TYPE
 * получение типа бд хранилища
 */
 
 if (0==_checkCol( "ID_STORAGE_TYPE" , "ARC_DB_SCHEMA" ))        
cmd0.CommandText="select ads.ID, ads.NAME, ads.SCHEMA_NAME " + 
"from " + stSchema + "ARC_DB_SCHEMA ads " +
"order by ads.ID asc " ;
 else 
cmd0.CommandText="select ads.ID, ads.NAME, ads.SCHEMA_NAME , ast.DEFINE_ALIAS as STORAGE " + 
"from " + stSchema + "ARC_DB_SCHEMA ads, " + stSchema + "ARC_STORAGE_TYPE ast " +
"where ads.ID_STORAGE_TYPE=ast.ID " +
"order by ads.ID asc " ;
 	
 	
       dataSet1.Clear();
       dataGridView1.DataSource = null;
       dataSet1.Tables.Clear();

       // Указываем запрос для выполнения
       adapter.SelectCommand = cmd0;
	   
       // Заполняем объект источника данных
       adapter.Fill(dataSet1,table_name);

       // (с этого момента она будет отображать его содержимое)
       dataGridView1.DataSource = dataSet1.Tables[0];
	   
	   //data.Add(new SampleRow("Товар 1", 100, 1));

       bindingSource1.DataSource=dataSet1.Tables[0];

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
		/*
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Название"; //текст в шапке
            column1.Width = 100; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "name"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Цена"; 
            column2.Name = "price";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Остаток";
            column3.Name = "count";
            column3.CellTemplate = new DataGridViewTextBoxCell();
            
            var column4 = new DataGridViewColumn();
            DataGridViewCheckBoxColumn column4 = new DataGridViewCheckBoxColumn();

DataGridViewComboBoxColumn DGVCMB = new DataGridViewComboBoxColumn();
            DGVCMB.DataPropertyName = "userVALUE";
            DGVCMB.Name = "userVALUE";
            DGVCMB.HeaderText = "Выбор значения";
            DGVCMB.Width = 150;
            DGVCMB.DataSource = ds2.Tables[0];
            DGVCMB.DisplayMember = "userattendancetypesNAME";
            DGVCMB.ValueMember = "userattendancetypesID";
            dataGridView1.Columns.Add(DGVCMB);            
            
            
            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
*/

			Select1(sender) ;
		}		
		
		
	}
}
