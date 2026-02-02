/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 04.12.2017
 * Time: 13:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

using System.Data.Common;
using System.IO ;
using System.Globalization;
using System.Data.Odbc;
using System.Threading;

namespace SYS_DSRT_DTYP
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
     public OdbcConnection cnn ;
     int FlagLoad = 0;

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
		
      
    public void AddLogString(string s)
    {
      DateTime d = DateTime.Now;
      string strout = "\n"+ "[" + d.ToLongTimeString() + "] " + s ;
      richTextBox1.AppendText(strout);
      Application.DoEvents();
    }

    public void RefreshDB ( )
    {
        OdbcCommand cmd = new OdbcCommand();
        OdbcDataReader reader = null ;

        cmd.Connection=cnn;

        if (cnn.State==System.Data.ConnectionState.Closed) {
            AddLogString("Невозможно установить соединение с БД (Oracle, MySql)");
            AddLogString(" выполнение алгоритма  - прервано..");
            cnn.Close();
            return ;
        }

        treeView1.BeginUpdate(); //добавить
        treeView1.Nodes.Clear();

        cmd.CommandText="select ID,ID_PARENT,NAME,ID_TYPE from SYS_DSRT where id_parent IS NULL order by id asc";
        try
        {
           reader = cmd.ExecuteReader();
        }
        catch (Exception ex7)
        {
           AddLogString(" выполнение алгоритма  - прервано.. = " + cmd.CommandText);
          return ;
        }

        string[] arr = new string[4];
        
        if (reader.HasRows)
        {
            while (reader.Read())
            {
              TreeNode rootNode = new TreeNode();
              
              for ( int i = 0; i<4; i++) {
              	arr[i]="";
              	if (reader.IsDBNull(i)) {
              		arr[i]="";
              	} else {
              		arr[i]= reader.GetDataTypeName(i);
              		string stmp = arr[i].ToUpper();
              		if (stmp=="DECIMAL") arr[i] = reader.GetValue(i).ToString();
              		if (stmp=="NUMBER") arr[i] = reader.GetDecimal(i).ToString();
              		if (stmp=="VARCHAR2") arr[i] = reader.GetString(i);
              		if (stmp=="NVARCHAR") arr[i] = reader.GetString(i);
              		
              		
              		if (stmp=="TEXT") arr[i] = reader.GetString(i);
              	}
              }              
              
              
              
              rootNode.Name = arr[0];
              rootNode.Text = arr[2] ;
              AddLogString(rootNode.Name + " " + rootNode.Text);
              treeView1.Nodes.Add(rootNode);
            }
        }
        else
        {
            treeView1.Nodes.Add(new TreeNode("root"));
        }
        reader.Close();

        cmd.CommandText="select ID,ID_PARENT,NAME,ID_TYPE from SYS_DSRT where not (id_parent is NULL) order by id_parent asc" ;
        try
        {
           reader = cmd.ExecuteReader();
        }
        catch (Exception ex8)
        {
                      AddLogString(" выполнение алгоритма  - прервано.. = " + cmd.CommandText);
          return ;
        }

        if (reader.HasRows)
        {
            while (reader.Read())
            {

              for ( int i = 0; i<4; i++) {
              	arr[i]="";
              	if (reader.IsDBNull(i)) {
              		arr[i]="";
              	} else {
              		arr[i]= reader.GetDataTypeName(i);
              		string stmp = arr[i].ToUpper();
              		if (stmp=="DECIMAL") arr[i] = reader.GetValue(i).ToString();
              		if (stmp=="NUMBER") arr[i] = reader.GetDecimal(i).ToString();
              		if (stmp=="VARCHAR2") arr[i] = reader.GetString(i);
              		if (stmp=="NVARCHAR") arr[i] = reader.GetString(i);
              		
              		
              		if (stmp=="TEXT") arr[i] = reader.GetString(i);
              	}
              }              
              

            	
            	string s5= arr[1];
           	TreeNode[] tNodeCurrent = new TreeNode[10];
           	tNodeCurrent =treeView1.Nodes.Find(s5, true);
           	   foreach (TreeNode ndl in tNodeCurrent)
               {
                      if (ndl.Name == s5) {
                          TreeNode Nd = new TreeNode();
                          Nd.Name = arr[0];
                          Nd.Text = arr[2];
                          ndl.Nodes.Add(Nd);
                          AddLogString(Nd.Name + " " + Nd.Text);
                       }

                } // foreach
              } // while
         } // if
         reader.Close();


         treeView1.EndUpdate(); //добавить
         //treeView1.ExpandAll();

    }
		void MainFormLoad(object sender, EventArgs e)
		{
        string connetionString = "DSN=rsdu1;UID=rsduadmin; PWD=passme";
        OdbcCommand cmd = new OdbcCommand();
        cnn = null ;

        cnn = new OdbcConnection(connetionString);
        try
        {
          cnn.Open();
          AddLogString("Connection Open = Ok ");
        }
        catch (Exception ex6)
        {
          AddLogString("Connection Open = NONE  "+ex6.Message);
        }

        if (cnn.State==System.Data.ConnectionState.Closed) {
            AddLogString("Невозможно установить соединение с БД (Oracle, MySql)");
            AddLogString(" выполнение алгоритма  - прервано..");
            cnn.Close();
            return ;
        }

        cmd.Connection = cnn; // уже созданное и открытое соединение
        cmd.CommandType = System.Data.CommandType.Text ;
        cmd.Parameters.Clear();
        cmd.CommandText="SET ROLE BASE_EXT_CONNECT_OIK , SBOR_STAND_ADJ, SBOR_STAND_READ";

        /*
        try
        {
           cmd.ExecuteNonQuery();
           AddLogString("Установка роли = Ok ");
        }
        catch (Exception ex7)
        {
           AddLogString("Ошибка установки Ролей ="+ex7.Message);
           AddLogString(" выполнение алгоритма вставки - прервано..=" + cmd.CommandText);
           cnn.Close();
           return ;
        }*/

        RefreshDB();

        treeView1.SelectedNode = null;
        //treeView1.Focus();
        treeView1.LabelEdit=true ;

        // SYS_DTYP ( ID,ID_NODE,NAME,ALIAS,CODE,ID_ICON,ID_GTYPE,DEFINE_ALIAS ) 
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "ID"; //текст в шапке
            column1.Width = 100; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "ID"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "NAME"; 
            column2.Name = "NAME";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "ALIAS";
            column3.Name = "ALIAS";
            column3.CellTemplate = new DataGridViewTextBoxCell();

            var column4 = new DataGridViewColumn();
            column4.HeaderText = "CODE";
            column4.Name = "CODE";
            column4.CellTemplate = new DataGridViewTextBoxCell();            
            
            var column5 = new DataGridViewColumn();
            column5.HeaderText = "ID_ICON";
            column5.Name = "ID_ICON";
            column5.CellTemplate = new DataGridViewTextBoxCell(); 
            
            var column6 = new DataGridViewColumn();
            column6.HeaderText = "ID_GTYPE";
            column6.Name = "ID_GTYPE";
            column6.CellTemplate = new DataGridViewTextBoxCell();             

            var column7 = new DataGridViewColumn();
            column7.HeaderText = "DEFINE_ALIAS";
            column7.Name = "DEFINE_ALIAS";
            column7.CellTemplate = new DataGridViewTextBoxCell();             
            
            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);
            dataGridView1.Columns.Add(column6);
            dataGridView1.Columns.Add(column7);
        
        FlagLoad=1;

        return ;
		}
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{

        OdbcCommand cmd0 = new OdbcCommand();
        OdbcCommand cmd1 = new OdbcCommand();
        OdbcDataReader reader = null ;
        OdbcDataReader reader1 = null ;

        if (cnn.State==System.Data.ConnectionState.Closed) {
            AddLogString("Невозможно установить соединение с БД (Oracle, MySql)");
            AddLogString(" выполнение алгоритма  - прервано..");
            cnn.Close();
            return ;
        }
      
        cmd0.Connection=cnn;
        cmd1.Connection=cnn;


        //Получить Имя выделенного элемента
        string id_parent=treeView1.SelectedNode.Name;
        //AddLogString(" id_parent=" + id_parent);
        // полный путь

        cmd0.CommandText="select ID,ID_NODE,NAME,ALIAS,CODE,ID_ICON,ID_GTYPE,DEFINE_ALIAS from SYS_DTYP where id_node="+id_parent+" order by id asc";
        try
        {
           reader = cmd0.ExecuteReader();
        }
        catch (Exception ex7)
        {
           AddLogString(" выполнение алгоритма  - прервано.." + cmd0.CommandText);
           return ;
        }

		   // re-read
		   dataGridView1.Rows.Clear();
           Application.DoEvents();
        
        string[] arr = new string[8];

       
        if (reader.HasRows)
        {
            while (reader.Read())
            {

            	
// SYS_DTYP ( ID,ID_NODE,NAME,ALIAS,CODE,ID_ICON,ID_GTYPE,DEFINE_ALIAS )            	
//  ID            NUMBER(11),
//  ID_NODE       NUMBER(11),
//  NAME          VARCHAR2(255 BYTE),
//  ALIAS         VARCHAR2(255 BYTE),
//  CODE          VARCHAR2(15 BYTE),
//  ID_ICON       NUMBER(11),
//  ID_GTYPE      NUMBER(11),
//  DEFINE_ALIAS  VARCHAR2(63 BYTE)

              

              for ( int i = 0; i<8; i++) {
              	arr[i]="";
              	if (reader.IsDBNull(i)) {
              		arr[i]="";
              	} else {
              		arr[i]= reader.GetDataTypeName(i);
              		string stmp = arr[i].ToUpper();
              		if (stmp=="DECIMAL") arr[i] = reader.GetValue(i).ToString();
              		if (stmp=="NUMBER") arr[i] = reader.GetDecimal(i).ToString();
              		if (stmp=="VARCHAR2") arr[i] = reader.GetString(i);
              		if (stmp=="NVARCHAR") arr[i] = reader.GetString(i);
              		
              		
              		if (stmp=="TEXT") arr[i] = reader.GetString(i);
              	}
              } 
              
              dataGridView1.Rows.Add(arr[0],arr[2],arr[3],arr[4],arr[5],arr[6],arr[7]);
              	
              Application.DoEvents();
            }

        }

        reader.Close();
        
                // Resize the master DataGridView columns to fit the newly loaded data.
    dataGridView1.AutoResizeColumns();

    // Configure the details DataGridView so that its columns automatically
    // adjust their widths when the data changes.
    dataGridView1.AutoSizeColumnsMode = 
        DataGridViewAutoSizeColumnsMode.AllCells;
    
		}


		
		
		
	}
}
