/*
 * Created by SharpDevelop.
 * User: Tanuki
 * Date: 02.04.2017
 * Time: 22:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Common;
using System.IO ;
using System.Globalization;
using System.Data.Odbc;
using System.Threading;

namespace da_list
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public OdbcConnection cnn ;

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
		
   void AddLogString(string s)
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
   
   
    public void RefreshDB ( )
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

        cmd0.CommandText="select sys_tbllst.*,FROM_DT1970(LAST_UPDATE) from sys_tbllst where upper(table_name) like 'DA_V_LST_%'";
        try
        {
           reader = cmd0.ExecuteReader();
        }
        catch (Exception ex7)
        {
           AddLogString(" выполнение алгоритма  - прервано.." + cmd0.CommandText);
           return ;
        }
        
        string[] arr = new string[7];
        ListViewItem itm ;
        decimal id_cnt ;
        
        if (reader.HasRows)
        {
            while (reader.Read())
            {

              //ID            NUMBER(11),
              //ID_NODE       NUMBER(11),
              //NAME          VARCHAR2(255 BYTE) CONSTRAINT SYS_TBLLST_NAME_NN NOT NULL,
              //TABLE_NAME    VARCHAR2(63 BYTE) CONSTRAINT SYS_TBLLST_TABLE_NAME_NN NOT NULL,
              //ID_TYPE       NUMBER(11) CONSTRAINT SYS_TBLLST_ID_TYPE_NN NOT NULL,
              //DEFINE_ALIAS  VARCHAR2(63 BYTE) CONSTRAINT SYS_TBLLST_DEFINE_ALIAS_NN NOT NULL,
              //LAST_UPDATE   NUMBER(11)

              arr[0] = reader.GetDecimal(0).ToString();
              arr[1] = reader.GetDecimal(1).ToString();
              arr[2] = reader.GetString(2);
              arr[3] = reader.GetString(3);
              arr[4] = reader.GetDecimal(4).ToString();
              arr[5] = reader.GetString(5);
              //arr[6] = reader.GetDecimal(6).ToString();
              arr[6] = reader.GetString(7);
              itm = new ListViewItem(arr);
              
              id_cnt=0;
              cmd1.CommandText="select count(id) from sys_tree21 where id_lsttbl in (select id from sys_tbllst where upper(table_name) like '"+arr[3]+"');";
              try
              {
                  reader1 = cmd1.ExecuteReader();
              }
              catch (Exception ex8)
              {
                  AddLogString(" ошибка : " + cmd1.CommandText);
              }  
             
              if (reader1.HasRows)
              {
                while (reader1.Read())
                {              
                  id_cnt=reader1.GetDecimal(0);
                }
              }
              reader1.Close();
              if (id_cnt>0) itm.Checked=false; else itm.Checked=true; 
              
              if(InvokeRequired) listView1.Invoke((Action)delegate { listView1.Items.Add(itm); });
              else listView1.Items.Add(itm);
              Application.DoEvents();
            }
        }

        reader.Close();
        return ;
    }   
   
    public void DeleteFromDB ( int index )
    {
    	OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader0 = null ;

        if (cnn.State==System.Data.ConnectionState.Closed) {
            AddLogString("Невозможно установить соединение с БД (Oracle, MySql)");
            AddLogString(" выполнение алгоритма  - прервано..");
            cnn.Close();
            return ;
        }
      
        cmd0.Connection=cnn;
        
        AddLogString(" Start>>" );
 
        cmd0.CommandText="select count(id) from sys_tree21 where id_lsttbl in (select id from sys_tbllst where upper(table_name) like 'DA_V_LST_"+ index.ToString() +"');";
        try
        {
           reader0 = cmd0.ExecuteReader();
        }
        catch (Exception ex8)
        {
           AddLogString(" выполнение алгоритма  - прервано.." + cmd0.CommandText);
           return ;
        }
       
        decimal id_cnt = 0;        
        if (reader0.HasRows)
          while (reader0.Read()) id_cnt=reader0.GetDecimal(0);
        reader0.Close();
        
        if (id_cnt>0) {
           AddLogString(" Сегмент описан в структуре БД ( sys_tree21 ). Удаление прервано. ");
           return ;        	
        }

        // Part 1
        cmd0.CommandText="drop public synonym DA_V_CAT_"+ index.ToString() +" ;";
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  
        
        cmd0.CommandText="drop public synonym DA_V_LST_"+ index.ToString() +" ;";
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }        
        
        cmd0.CommandText="drop public synonym ARC_DA_V_LST_"+ index.ToString() +" ;";
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }             
        
        cmd0.CommandText="drop view DA_V_CAT_"+ index.ToString() +" ;";
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }        
 
        cmd0.CommandText="drop view DA_V_LST_"+ index.ToString() +" ;";
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }           
        
        cmd0.CommandText="drop view ARC_DA_V_LST_"+ index.ToString() +" ;";
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }         
        

        // Part 2
        cmd0.CommandText="delete from sys_tbllnk where id_lsttbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" + 
        	" and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));" ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 
        cmd0.CommandText="delete from sys_tblref where id_tbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" +
            " and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));" ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 
        cmd0.CommandText="delete from sys_tblref where id_tbl in (select id from sys_tbllst t where table_name like 'DA_V_CAT_" + index.ToString() +"'" +
        	" and not exists (select 1 from sys_tree21 where id_lsttbl IN (SELECT id_lsttbl FROM sys_tabl_v WHERE ID = t.id)));" ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }         

         
         // Part 3
        cmd0.CommandText="delete from ARC_SERVICES_TUNE where id_sprofile in (select id from arc_subsyst_profile " +
            " where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" +
            " AND not exists (select 1 from sys_tree21 where id_lsttbl = t.id))); ";
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }
         
        cmd0.CommandText="delete from ARC_SERVICES_ACCESS where id_sprofile in (select id from arc_subsyst_profile " +
   " where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" +
                      "  AND not exists (select 1 from sys_tree21 where id_lsttbl = t.id)));  " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }            
         
        cmd0.CommandText="delete from ARC_SERVICES_INFO where id_lsttbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" +
                       " AND not exists (select 1 from sys_tree21 where id_lsttbl = t.id)); " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  

       cmd0.CommandText="delete from arc_subsyst_profile where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" +
 " and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));  " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  

       cmd0.CommandText="delete from sys_app_serv_lst where id_lsttbl in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" +
 " and not exists (select 1 from sys_tree21 where id_lsttbl = t.id)); " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  

       cmd0.CommandText="delete from ARC_READ_DEFAULTS  where id_tbllst in (select id from sys_tbllst t where table_name like '%DA_V_LST_"+ index.ToString() +"'" +
 " and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));  " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  

       cmd0.CommandText="delete from ARC_READ_DEFAULTS  where id_tbllst in (select id from sys_tbllst t where table_name like 'ARC_DA_V_LST_"+ index.ToString() +"'" +
 " and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));  " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  

       cmd0.CommandText="delete  from ARC_READ_VIEW  where id_tbllst in (select id from sys_tbllst t where table_name like 'DA_V_LST_"+ index.ToString() +"'" +
 " and not exists (select 1 from sys_tree21 where id_lsttbl = t.id));  " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  

       cmd0.CommandText="delete from ad_sinfo where id_lsttbl  in (select id from sys_tbllst t where table_name like 'DA_V_CAT_"+ index.ToString() +"'" +
   	" and not exists (select 1 from sys_tree21 where id_lsttbl = t.id)); " ;
        try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }  


       // Part 5
       decimal nID = 0; 
       string tname = "'DA_V_LST_"+index.ToString()+"'";
       cmd0.CommandText="select count(id) from sys_tree21 where id_lsttbl in (select id from sys_tbllst where table_name like " + tname + ");" ;
       try { reader0 = cmd0.ExecuteReader(); }
       catch (Exception ex7)
       {
           AddLogString(" выполнение алгоритма  - прервано.." + cmd0.CommandText);
           return ;
       }
       if (reader0.HasRows)
          while (reader0.Read()) nID =reader0.GetDecimal(0);
       reader0.Close();
        
        if (nID==0) {
          cmd0.CommandText="select id_node from sys_tbllst where table_name like " + tname + " ; ";
          try { reader0 = cmd0.ExecuteReader(); }
          catch (Exception ex7)
          {
             AddLogString(" выполнение алгоритма  - прервано.." + cmd0.CommandText);
             return ;
          } 
          if (reader0.HasRows)
              while (reader0.Read()) nID =reader0.GetDecimal(0);
          reader0.Close();          

          cmd0.CommandText="delete from sys_tblref where id_tbl in (select id from sys_tbllst where id_node = " + nID.ToString() + ")" ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }         

          cmd0.CommandText="delete from sys_tbllnk where id_lsttbl in (select id from sys_tbllst where id_node = " + nID.ToString() + ")" ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 

          cmd0.CommandText="delete from sys_tbllnk where id_dsttbl in (select id from sys_tbllst where id_node = " + nID.ToString() + ")" ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 

          cmd0.CommandText="delete from sys_tbllst where id_node = " + nID.ToString() ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }           

          cmd0.CommandText="delete from sys_tbllst where table_name like " + tname ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 

          cmd0.CommandText="delete from sys_db_part where id = " + nID.ToString() ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 
  
          AddLogString(" Удалено для " + tname );
       } else {
       	 AddLogString(" НЕ удалено для " + tname + ": используется в Навигаторе!" );
       }
 
       // Part 6
       nID = 0; 
       tname = "'ARC_DA_V_LST_"+index.ToString()+"'";
       cmd0.CommandText="select count(id) from sys_tree21 where id_lsttbl in (select id from sys_tbllst where table_name like " + tname + ");" ;
       try { reader0 = cmd0.ExecuteReader(); }
       catch (Exception ex7)
       {
           AddLogString(" выполнение алгоритма  - прервано.." + cmd0.CommandText);
           return ;
       }
       if (reader0.HasRows)
          while (reader0.Read()) nID =reader0.GetDecimal(0);
       reader0.Close();
        
        if (nID==0) {
          cmd0.CommandText="select id_node from sys_tbllst where table_name like " + tname + " ; ";
          try { reader0 = cmd0.ExecuteReader(); }
          catch (Exception ex7)
          {
             AddLogString(" выполнение алгоритма  - прервано.." + cmd0.CommandText);
             return ;
          } 
          if (reader0.HasRows)
              while (reader0.Read()) nID =reader0.GetDecimal(0);
          reader0.Close();          

          cmd0.CommandText="delete from sys_tblref where id_tbl in (select id from sys_tbllst where id_node = " + nID.ToString() + ")" ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }         

          cmd0.CommandText="delete from sys_tbllnk where id_lsttbl in (select id from sys_tbllst where id_node = " + nID.ToString() + ")" ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 

          cmd0.CommandText="delete from sys_tbllnk where id_dsttbl in (select id from sys_tbllst where id_node = " + nID.ToString() + ")" ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 

          cmd0.CommandText="delete from sys_tbllst where id_node = " + nID.ToString() ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  }           

          cmd0.CommandText="delete from sys_tbllst where table_name like " + tname ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 

          cmd0.CommandText="delete from sys_db_part where id = " + nID.ToString() ;
          try { cmd0.ExecuteNonQuery(); } catch (Exception ex8) { AddLogString(" Ошибка выполнения = " + cmd0.CommandText);  } 
  
          AddLogString(" Удалено для " + tname );
       } else {
       	 AddLogString(" НЕ удалено для " + tname + ": используется в Навигаторе!" );
       }
        
        AddLogString(" Ready>>" );
        return ;
    }       
    
		void Button2Click(object sender, EventArgs e)
		{          // delete
          
          // Удаляем только выделенную строку
          
          if (listView1.SelectedItems.Count <= 0) return ;
          if (listView1.SelectedItems.Count > 1) {
          	AddLogString(" СОВЕТ: За 1 раз удаляйте только 1 Сегмент Сбора! ");
           	return ;
          }
        
          for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
          {
              ListViewItem itm = listView1.SelectedItems[i];
              string da_v = itm.SubItems[3].Text;
              string da_v_num = "";
            
              foreach (char c in da_v) 
                if (c >= '0' && c <= '9') da_v_num=da_v_num+c ;

              int numCnt = 0;
              if (!Int32.TryParse(da_v_num, out numCnt)) {
                 AddLogString(" start>> Int32.TryParse could not parse " +da_v+" to an int.");
                 continue ;
               }
              if (numCnt<=0) { 
             	AddLogString(" Ошибка создания номера  = " + numCnt.ToString());
            	continue ;
              }
              
              DialogResult result = MessageBox.Show("Удалить Сегмент Сбора " + da_v + " ?","?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
              if (result == System.Windows.Forms.DialogResult.Cancel) break ;
            
              DeleteFromDB ( numCnt );
              Application.DoEvents();
           }
          Button3Click(sender, e);
          return ;
		}
		void MainFormLoad(object sender, EventArgs e)
		{
           textBox1.Enabled=true ;
           textBox2.Enabled=true ;
           textBox3.Enabled=true ; 
           button1.Enabled=false;
           button2.Enabled=false;
           button3.Enabled=false;
           return ;
		}
		void Button3Click(object sender, EventArgs e)
		{
		   // re-read
		   listView1.Clear();
			
	       listView1.View = View.Details;
           listView1.GridLines = true;
           listView1.FullRowSelect = true;
           listView1.AllowDrop = true ;

           //Add column header
           listView1.CheckBoxes = true;
           listView1.Columns.Add("id", 80);
           listView1.Columns.Add("ID_NODE", 80);
           listView1.Columns.Add("NAME", 120);
           listView1.Columns.Add("TABLE_NAME", 100);
           listView1.Columns.Add("ID_TYPE", 40);
           listView1.Columns.Add("DEFINE_ALIAS", 120);
           listView1.Columns.Add("LAST_UPDATE", 130);			
			
           Application.DoEvents();
		   RefreshDB ( );
		   return ;
		}
		void Button1Click(object sender, EventArgs e)
		{
	      // add
          OdbcCommand cmd0 = new OdbcCommand();
          OdbcDataReader reader0 = null ;

          if (cnn.State==System.Data.ConnectionState.Closed) {
            AddLogString("Невозможно установить соединение с БД (Oracle, MySql)");
            AddLogString(" выполнение алгоритма  - прервано..");
            cnn.Close();
            return ;
          }

          cmd0.Connection=cnn;
          
          decimal id_cnt=0;          
	      int i = 0; // можем создать 100 систем сбора, берем ближайшее! свободное! целое число
          for (i = 1; i <= 100; i++)
          {
          	cmd0.CommandText="select count(*) from sys_tbllst where upper(table_name) like 'DA_V_LST_"+i.ToString()+"'";
            try { reader0 = cmd0.ExecuteReader(); }
            catch (Exception ex7) {	continue ; }
                  	
            if (reader0.HasRows)
               while (reader0.Read())          
            		id_cnt=reader0.GetDecimal(0);
            reader0.Close();
            
            if (id_cnt==0) {
            	
              DialogResult result = MessageBox.Show("Создать Сегмент Сбора с номером = " + i.ToString() ,"?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
              if (result == System.Windows.Forms.DialogResult.Cancel) break ;
            	
              cmd0.CommandType = System.Data.CommandType.StoredProcedure;
              cmd0.Parameters.Clear();
              cmd0.CommandText = "{call RSDUADMIN.DA_CREATE_NEW_SEGMENT_P( ? )}";
              //PROCEDURE RSDUADMIN.DA_create_new_segment_p (pTOP_ID NUMBER)

              OdbcParameter param = new OdbcParameter();
              param.Direction = System.Data.ParameterDirection.Input;
              param.OdbcType = OdbcType.Numeric ;
              param.ParameterName = "pTOP_ID";
              param.Value = i;
              //param.Size = 1024 ;

              cmd0.Parameters.Add(param);
              
              try { cmd0.ExecuteNonQuery(); }
              catch (Exception ex11) { AddLogString("Ошибка вызова процедуры ="+ex11.Message); }

              AddLogString("Проверьте список Систем Сбора !");
              break ;
            }
          	
          	
          }
	      // перечитываем из базы
	      Button3Click(sender, e);
	      
		}
		void Button4Click(object sender, EventArgs e)
		{
	
           string connetionString = "DSN=rsdu2;UID=rsduadmin; PWD=passme";
           //string connetionString = "DSN=rsdu2;UID=sys; PWD=passme as sysdba";
           
           connetionString = "DSN=" + textBox1.Text + 
           	  ";UID=" + textBox2.Text + ";PWD=" + textBox3.Text ;
           
           OdbcCommand cmd = new OdbcCommand();
           cnn = null ;
           
           AddLogString(" РЕКОМЕНДАЦИЯ: за 1 раз создавать и использовать 1 Сегмент сбора!");

           cnn = new OdbcConnection(connetionString);
           try
           {
             cnn.Open();
             AddLogString("Connection Open = Ok ");
             textBox1.Enabled=false ;
             textBox2.Enabled=false ;
             textBox3.Enabled=false ;
             button1.Enabled=true;
             button2.Enabled=true;
             button3.Enabled=true;
             button4.Enabled=false;
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

           try
           {
             cmd.ExecuteNonQuery();
             AddLogString("Установка роли = Ok ");
           }
           catch (Exception ex7)
           {
             AddLogString("Ошибка установки Ролей ="+ex7.Message);
             AddLogString(" выполнение программы - прервано..");
             cnn.Close();
             return ;
           }

           Button3Click(sender, e);
           return ;
		}		
		
	}
}
