/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 24.03.2016
 * Time: 13:40
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Common;
using System.IO ;
using System.Globalization;
using System.Data.Odbc;
using System.Threading;

//using System.Data.OracleClient ;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;

namespace ArcWrite
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MainForm : Form
  {
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

    void AddFile(object FilePath)
    {
      string[] arr = new string[6];
      ListViewItem itm ;

      string file=(string)FilePath ;

      AddLogString(" Присоединение файла. Подождите .......");

      //Form1 example = new Form1();
      //example.Show();

      bool CheckFile = true ;
      String tblName = "";
      SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", file));
      connection.Open();
      SQLiteCommand command ;
      SQLiteDataReader reader = null;
      try {
        command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;", connection);
        reader = command.ExecuteReader();
      }
      catch (Exception ex1)
      {
        AddLogString(" File=" + file);
        AddLogString(" Original error: " + ex1.Message );
        CheckFile = false ;
        return ;
      }

      if (reader==null) {
        AddLogString(" File=" + file + " - нет Таблиц ");
        connection.Close();
        return ;
      }

      // Принимаем - 1 файл = 1 таблица
      foreach (DbDataRecord record in reader)
           tblName =  record["name"].ToString() ;

      if (tblName=="" || tblName== null || tblName == System.String.Empty) {
         AddLogString(" File=" + file + " - нет Таблицы ");
         connection.Close();
         return ;
      }

      string minD = "";
      string maxD = "";
      string cntD = "";

      try {
        command = new SQLiteCommand(string.Format("select datetime(min(TIME1970), 'unixepoch'),  datetime(max(TIME1970), 'unixepoch'),count(*) from {0};", tblName), connection);
        reader = command.ExecuteReader();
      }
      catch (Exception ex2)
      {
        AddLogString(" File=" + file);
        AddLogString(" Original error: " + ex2.Message );
        CheckFile = false ;
        //return ; // файл имеет формат sqlite,но не имеет таблиц
      }

      if (reader==null) {
        AddLogString(" File=" + file + " - ошибка преобразования времени и получения общего числа записей");
        connection.Close();
        return ;
      }

      foreach (DbDataRecord record in reader) {
        minD =  record[0].ToString() ;
        maxD =  record[1].ToString() ;
        cntD =  record[2].ToString() ;
      }

      connection.Close();

      arr[0] = System.IO.Path.GetFileName(file);
      arr[1] = minD;
      arr[2] = maxD;
      arr[3] = cntD;
      arr[4] = "";
      arr[5] = System.IO.Path.GetDirectoryName(file);

      AddLogString(" Присоединение файла: " + arr[0] + ". Выполнено успешно.");
      itm = new ListViewItem(arr);
      itm.Checked=CheckFile ;
      if(InvokeRequired) listView1.Invoke((Action)delegate { listView1.Items.Add(itm); });
      else listView1.Items.Add(itm);

      Application.DoEvents();

      return ;
    }

    void AddFiles(string[] files)
    {
       Thread[] array = new Thread[files.Length];
       ParameterizedThreadStart operation =
              new ParameterizedThreadStart(AddFile);
       int i = 0;
       foreach (string file in files)
       {
          System.IO.FileInfo fileinfo = new System.IO.FileInfo(file);
          long size = fileinfo.Length;
          int sz = Convert.ToInt32(size/(50*1024*1024)) + 2; // 2s добавляем на открытие
          AddLogString(" .....время обработки файла =~" + sz.ToString() + " сек.");
          Thread thr = new Thread(operation);
          thr.Priority=ThreadPriority.AboveNormal;
          thr.Start(file);//AddFile(file);
          array[i] = thr;
          i=i+1;
       }
    }


    void Button1Click(object sender, EventArgs e)
    {
      // +
      openFileDialog1.Title = "Выберите файл(-ы)";
      openFileDialog1.Filter = "SQLite файлы|*.db|All files (*.*)|*.*" ;
      openFileDialog1.FilterIndex =  0;
      openFileDialog1.Multiselect = true ;
      openFileDialog1.FileName = "";
      // выход, если была нажата кнопка Отмена и прочие (кроме ОК)
      if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
      // всё. имя файла теперь хранится в openFileDialog1.FileName
      //button3.Enabled=true;
      AddFiles(openFileDialog1.FileNames);
      //button3.Enabled=false;
    }
    void Timer1Tick(object sender, EventArgs e)
    {
       //
       DateTime ThToday = DateTime.Now; // DateTime.Now.TimeOfDay;
       toolStripStatusLabel3.Text = ThToday.ToString();
    }
    void MainFormLoad(object sender, EventArgs e)
    {
       listView1.View = View.Details;
       listView1.GridLines = true;
       listView1.FullRowSelect = true;
       listView1.AllowDrop = true ;

       //Add column header
       listView1.CheckBoxes = true;
       listView1.Columns.Add("Name", 240);
       listView1.Columns.Add("Start", 115);
       listView1.Columns.Add("End", 115);
       listView1.Columns.Add("count", 80);
       listView1.Columns.Add("*", 80);
       listView1.Columns.Add("Path", 250);

       // добавлено для попытки быстродействия чтения из Sqlite
       using (SQLiteConnection destination = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;"))
       {
          destination.Open();
          using (var cmd = new SQLiteCommand("BEGIN", destination))
              cmd.ExecuteNonQuery();
          destination.Close();
       }

    }
    void Button5Click(object sender, EventArgs e)
    {
       // Find
       if (listView1.CheckedItems.Count<=0) return ;

       AddLogString(" Обработка БД SQLite. Подождите ......");

       //получаем даты
       // начало  monthCalendar1  dateTimePicker1
       string date1 ; //=  this.monthCalendar1.SelectionRange.Start.ToShortDateString();
       date1 = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
       DateTime dtp1 = dateTimePicker1.Value;
       string dt1 = dtp1.ToLongTimeString();
       dt1=dtp1.ToString("HH:mm:ss");
       string t1=date1 + " " + dt1 ;

       // конец   monthCalendar2   dateTimePicker2
       string date2 ; //=  this.monthCalendar2.SelectionRange.Start.ToShortDateString();
       date2 = monthCalendar2.SelectionRange.Start.ToString("yyyy-MM-dd");
       DateTime dtp2 = dateTimePicker2.Value;
       string dt2 = dtp2.ToLongTimeString();
       dt2 = dtp2.ToString("HH:mm:ss");
       string t2=date2 + " " + dt2 ;

       // проверяем, чтобы dtp1<=dtp2
       // ??

       int indexSet = 0;
       for (int i = listView1.CheckedItems.Count - 1; i >= 0; i--)
       {
          ListViewItem itm = listView1.CheckedItems[i];
          indexSet = itm.Index ;

          string file = System.IO.Path.Combine(itm.SubItems[5].Text, itm.SubItems[0].Text);

          // 1. проверка на сущестование файла
          if (!File.Exists(file)) {
           //listView1.BeginUpdate();
           listView1.Items[indexSet].Checked=false ;
           //listView1.EndUpdate();
           AddLogString("\t" + file + " not exists. Unchecked.");
           continue ;
          }

          String tblName = "";
          SQLiteConnection connection =
               new SQLiteConnection(string.Format("Data Source={0};", file));
          connection.Open();
          SQLiteCommand command ;
          SQLiteDataReader reader = null;
          try {
          command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;", connection);
          reader = command.ExecuteReader();
          }
          catch (Exception ex1)
          {
             AddLogString(" File=" + file);
             AddLogString(" Original error: " + ex1.Message );
             connection.Close();
             continue ;
          }

          if (reader==null) { connection.Close();  continue ; }

          // Принимаем - 1 файл = 1 таблица
          foreach (DbDataRecord record in reader)
                tblName =  record["name"].ToString() ;

          string minD = "";
          string maxD = "";
          string cntD = "";
          string selD = "";

          try {
           command = new SQLiteCommand(string.Format("select strftime('%s','{0}'), strftime('%s','{1}')", t1,t2), connection);
           reader = command.ExecuteReader();
          }
          catch (Exception ex2)
          {
             AddLogString(" File=" + file);
             AddLogString(" Original error: " + ex2.Message );
             //continue ; // файл sqlite - но не имеет таблиц
          }

          foreach (DbDataRecord record in reader) {
             minD = record[0].ToString() ;
             maxD = record[1].ToString() ;
          }

          if (minD=="" || minD==null || minD == System.String.Empty) minD="0";
          if (maxD=="" || maxD==null || maxD == System.String.Empty) maxD="0";

          int numValue1;
          if (!Int32.TryParse(minD, out numValue1))
              AddLogString(" Int32.TryParse could not parse "+minD+" to an int.");
          int numValue2;
          if (!Int32.TryParse(maxD, out numValue2))
              AddLogString(" Int32.TryParse could not parse "+maxD+" to an int.");

          selD = "0" ;
          // проверяем, чтобы dtp1<=dtp2
          // ??
          if (numValue1<=numValue2) {
              try {
                command = new SQLiteCommand(string.Format("select count(*) from {0} where TIME1970>={1} and TIME1970<={2} ", tblName,numValue1,numValue2), connection);
                reader = command.ExecuteReader();
              }
              catch (Exception ex3)
              {
                 AddLogString(" File=" + file);
                 AddLogString(" Original error: " + ex3.Message );
                 //continue ; // файл sqlite - но не имеет таблиц
              }

              if (reader==null) {
                 AddLogString(" File=" + file + " - пустое число записей");
                 connection.Close();
                 continue ;
              }

              foreach (DbDataRecord record in reader)
                  selD = record[0].ToString() ;

              if (selD=="" || selD==null || selD == System.String.Empty) selD="0";
           }

           connection.Close();

           //listView1.BeginUpdate();
           //listView1.Items[indexSet].SubItems[4].ForeColor=Color.Red; лишен смысла при стандартной прорисовке
           listView1.Items[indexSet].SubItems[4].Text=selD ;
           //listView1.EndUpdate();

           AddLogString("Найдено: \t " + t1 + " <= " + t2 + " = " + selD + " записей");
       } // for
    }
    void Button6Click(object sender, EventArgs e)
    {
      // Delete -
      if (listView1.SelectedItems.Count <= 0) return ;

      int indexSet = 0;
      for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
      {
           ListViewItem itm = listView1.SelectedItems[i];
           indexSet = itm.Index ;
           listView1.Items[itm.Index].Remove();
      }
      // устанавливаем фокус
      if (listView1.Items.Count>0) {
          indexSet=0;
          listView1.Items[indexSet].Focused = true;
          listView1.Items[indexSet].Selected = true;
          listView1.Focus();
      }
    }
    void Button4Click(object sender, EventArgs e)
    {
       // clear
       richTextBox1.Clear();
    }
    void Button2Click(object sender, EventArgs e)
    {
       // start
       if (listView1.CheckedItems.Count<= 0) return ;
       AddLogString("................................Start...");
       Button5Click(sender, e) ; // предварительно запускаем метод Find ??

       //получаем даты
       // начало  monthCalendar1  dateTimePicker1
       string date1 ; //=  this.monthCalendar1.SelectionRange.Start.ToShortDateString();
       date1 = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
       DateTime dtp1 = dateTimePicker1.Value;
       string dt1 = dtp1.ToLongTimeString();
       dt1=dtp1.ToString("HH:mm:ss");
       string t1=date1 + " " + dt1 ;

       // конец   monthCalendar2   dateTimePicker2
       string date2 ; //=  this.monthCalendar2.SelectionRange.Start.ToShortDateString();
       date2 = monthCalendar2.SelectionRange.Start.ToString("yyyy-MM-dd");
       DateTime dtp2 = dateTimePicker2.Value;
       string dt2 = dtp2.ToLongTimeString();
       dt2 = dtp2.ToString("HH:mm:ss");
       string t2=date2 + " " + dt2 ;


       // listView1.BeginUpdate();
       int indexSet = 0;
       for (int i = listView1.CheckedItems.Count - 1; i >= 0; i--)
       {
           ListViewItem itm = listView1.CheckedItems[i];
           indexSet = itm.Index ;

           string file = System.IO.Path.Combine(itm.SubItems[5].Text, itm.SubItems[0].Text);
           string cnt = itm.SubItems[4].Text;

           if (cnt=="" || cnt==null || cnt == System.String.Empty) cnt="0";

           int numCnt = 0;
           if (!Int32.TryParse(cnt, out numCnt))
                 AddLogString(" start>> Int32.TryParse could not parse " +cnt+" to an int.");

           if (numCnt<=0) continue ;

           //подсоединяемся к БД
           //получаем имя таблицы
           //преобразовываем метки время в Unix-формат
           //получаем имена столбцов
           //получаем records из таблицы
           //форматируем записи в Insert скидываем в файл\память.
           // отдаем в Oracle

           string[] arr = new string[6];

           String tblName = "";
           SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", file));
           connection.Open();
           SQLiteCommand command ;
           SQLiteDataReader reader = null;
           try {
              command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;", connection);
              reader = command.ExecuteReader();
           }
           catch (Exception ex1)
           {
              AddLogString(" File=" + file);
              AddLogString(" Original error: " + ex1.Message );
              connection.Close();
              continue ;
           }

           if (reader==null) { connection.Close();  continue ; }

           // Принимаем - 1 файл = 1 таблица (это в корне не верно, но пока принимаем так)
           foreach (DbDataRecord record in reader)
               tblName =  record["name"].ToString() ;

           string minD = "";
           string maxD = "";
           string cntD = "";
           string selD = "";

           try {
               command = new SQLiteCommand(string.Format("select strftime('%s','{0}'), strftime('%s','{1}')", t1,t2), connection);
               reader = command.ExecuteReader();
           }
           catch (Exception ex2)
           {
               AddLogString(" File=" + file);
               AddLogString(" Original error: " + ex2.Message );
               connection.Close();
               continue ; // файл sqlite - но не имеет таблиц
            }
           foreach (DbDataRecord record in reader) {
               minD =  record[0].ToString() ;
               maxD =  record[1].ToString() ;
           }

           if (minD=="" || minD==null || minD == System.String.Empty) minD="0";
           if (maxD=="" || maxD==null || maxD == System.String.Empty) maxD="0";

           int numValue1 = 0;
           if (!Int32.TryParse(minD, out numValue1))
                  AddLogString(" start>> Int32.TryParse could not parse " +minD+" to an int.");
           int numValue2 = 0;
           if (!Int32.TryParse(maxD, out numValue2))
                  AddLogString(" start>> Int32.TryParse could not parse "+maxD+" to an int.");

           if (numValue1>numValue2) {
             AddLogString(" Wrong Data = "+minD+" > " + maxD + " - abort process file. ");
             connection.Close();
             continue ;
           }


           try {
               command = new SQLiteCommand(string.Format("PRAGMA table_info({0})", tblName), connection);
               reader = command.ExecuteReader();
           }
           catch (Exception ex3)
           {
               AddLogString(" Table=" + tblName);
               AddLogString(" Original error: " + ex3.Message );
               connection.Close();
               continue ;
           }

           List<string> strFields = new List<string>();
           foreach (DbDataRecord record in reader) {
             strFields.Add(record["name"].ToString());
           }

           // select ID , TIME1970 , VAL , STATE , MIN_VAL , MAX_VAL from tblName where TIME1970>=t1 and TIME1970<=t2

           // INSERT INTO table_name ([column_name, ... ]) VALUES (expressions, ...)
           // insert into tblName  (ID , TIME1970 , VAL , STATE , MIN_VAL , MAX_VAL)  values (ID , TIME1970 , VAL , STATE , MIN_VAL , MAX_VAL)

           // INSERT INTO table_name VALUES (expressions, ...)
           // insert into tblName  values (ID , TIME1970 , VAL , STATE , MIN_VAL , MAX_VAL)
           // insert into %s(%s) values(%s)

           // формируем шаблоны строки для Select и insert.  Порядок Важен.
           string strInsert = "";
           string strValuesInsert = "";
           string strValues = "";

           int strNumber;
           int strIndex = -1;
           string strDelim = "";
           int nVal = 0;
           string strnVal = "";

           string findThisString = "ID";
           for (strNumber = 0; strNumber < strFields.Count; strNumber++)
           {
               strIndex = strFields[strNumber].IndexOf(findThisString);
               if (strIndex >= 0) {
                 nVal = nVal + 1 ;
                 strnVal=string.Format("{0}", nVal); ;
                 strValuesInsert = strValuesInsert + strDelim + "{" + strnVal  + "}";
                 strValues = strValues + strDelim + findThisString ;
                 strDelim = "," ;
                 break;
               }
           }

           findThisString = "TIME1970";
           for (strNumber = 0; strNumber < strFields.Count; strNumber++)
           {
               strIndex = strFields[strNumber].IndexOf(findThisString);
               if (strIndex >= 0) {
                 nVal = nVal + 1 ;
                 strnVal=string.Format("{0}", nVal); ;
                 strValuesInsert = strValuesInsert + strDelim + "{" + strnVal  + "}";
                 strValues = strValues + strDelim + findThisString ;
                 strDelim = "," ;
                 break;
               }
           }

           findThisString = "VAL";
           for (strNumber = 0; strNumber < strFields.Count; strNumber++)
           {
               strIndex = strFields[strNumber].IndexOf(findThisString);
               if (strIndex >= 0) {
                 nVal = nVal + 1 ;
                 strnVal=string.Format("{0}", nVal); ;
                 strValuesInsert = strValuesInsert + strDelim + "{" + strnVal  + "}";
                 strValues = strValues + strDelim + findThisString ;
                 strDelim = "," ;
                 break;
               }
           }

           findThisString = "STATE";
           for (strNumber = 0; strNumber < strFields.Count; strNumber++)
           {
               strIndex = strFields[strNumber].IndexOf(findThisString);
               if (strIndex >= 0) {
                 nVal = nVal + 1 ;
                 strnVal=string.Format("{0}", nVal); ;
                 strValuesInsert = strValuesInsert + strDelim + "{" + strnVal  + "}";
                 strValues = strValues + strDelim + findThisString ;
                 strDelim = "," ;
                 break;
               }
           }

           findThisString = "MIN_VAL";
           for (strNumber = 0; strNumber < strFields.Count; strNumber++)
           {
               strIndex = strFields[strNumber].IndexOf(findThisString);
               if (strIndex >= 0) {
                 nVal = nVal + 1 ;
                 strnVal=string.Format("{0}", nVal); ;
                 strValuesInsert = strValuesInsert + strDelim + "{" + strnVal  + "}";
                 strValues = strValues + strDelim + findThisString ;
                 strDelim = "," ;
                 break;
               }
           }

           findThisString = "MAX_VAL";
           for (strNumber = 0; strNumber < strFields.Count; strNumber++)
           {
               strIndex = strFields[strNumber].IndexOf(findThisString);
               if (strIndex >= 0) {
                 nVal = nVal + 1 ;
                 strnVal=string.Format("{0}", nVal); ;
                 strValuesInsert = strValuesInsert + strDelim + "{" + strnVal  + "}";
                 strValues = strValues + strDelim + findThisString ;
                 strDelim = "," ;
                 break;
               }
           }

           //AddLogString("\nvalues= " +strValues+"");
           //AddLogString("\nvalues= " +strValuesInsert+"");

           //получаем записи
           try {
             command = new SQLiteCommand(string.Format("select " + strValues + " from {0} where TIME1970>={1} and TIME1970<={2} ", tblName,numValue1,numValue2), connection);
             reader = command.ExecuteReader();
           }
           catch (Exception ex5)
           {
              AddLogString(" File " + file);
              AddLogString(" Original error: " + ex5.Message );
              //continue ; // файл sqlite - но не имеет таблиц
           }

           if (reader==null) {
              connection.Close();
              AddLogString(" Select from SQLite БД - имеет пустое число записей");
              continue ;
           }

           //strInsert = "insert into " + tblName + " (" + strValues + " ) values ( " + strValuesInsert + " ) ;" ;
           strInsert = "insert into {0} (" + strValues + " ) values ( " + strValuesInsert + " ) ;" ;

           // Если пишем в Oracle
           // 1/ Открываем соединение
           // 2/ Устанавливаем роли
           // 3/ Вызываем arc_writer_pkg.prepare_table
           // 5/  вставляем записи insert.
           // 6/ Вызываем перенос (arc_writer_pkg.MOVE_DATA) из таблицы стэка
           // 7/ завершаем
           string connetionString = "DSN=rsdu2;UID=rsduadmin; PWD=passme";
           OdbcConnection cnn = null ;
           OdbcCommand cmd = new OdbcCommand();
           String vRetVal = "";

           if (checkBox1.Checked==false) {

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
                AddLogString(" выполнение алгоритма вставки - прервано..");
                cnn.Close();
                connection.Close();
                return ;
              }

              cmd.Connection = cnn; // уже созданное и открытое соединение
              cmd.CommandType = System.Data.CommandType.StoredProcedure;
              cmd.Parameters.Clear();
              cmd.CommandText="SET ROLE BASE_EXT_CONNECT_OIK , ARC_STAND_ADMIN";

              try
              {
                  cmd.ExecuteNonQuery();
                  AddLogString("Установка роли = Ok ");
              }
              catch (Exception ex7)
              {
                AddLogString("Ошибка установки Ролей ="+ex7.Message);
                AddLogString(" выполнение алгоритма вставки - прервано..");
                cnn.Close();
                connection.Close();
                return ;
              }

              AddLogString("Вызов процедуры очистки таблицы стэка (arc_writer_pkg.prepare_table)...");

              cmd.CommandType = System.Data.CommandType.StoredProcedure;
              cmd.Parameters.Clear();
              cmd.CommandText = "{ ? = call arc_writer_pkg.prepare_table( ? )}";

              OdbcParameter parOut = new OdbcParameter();
              parOut.Direction = System.Data.ParameterDirection.ReturnValue;
              parOut.OdbcType = OdbcType.NChar;
              parOut.ParameterName = "vRetVal";
              parOut.Size = 1024;

              cmd.Parameters.Add(parOut);

              OdbcParameter param = new OdbcParameter();
              param.Direction = System.Data.ParameterDirection.Input;
              param.OdbcType = OdbcType.NChar;
              param.ParameterName = "pTname";
              param.Value = tblName;
              param.Size = 1024 ;

              cmd.Parameters.Add(param);

              try
              {
                 cmd.ExecuteNonQuery();
                 vRetVal = cmd.Parameters["vRetVal"].Value.ToString() ;
              }
              catch (Exception ex8)
              {
                 AddLogString("Не удалось вызвать процедуру очистки таблицы стэка.");
                 AddLogString("Ошибка вызова процедуры ="+ex8.Message);
                 cnn.Close(); // закрываем oRacle
                 connection.Close(); // закрываем sqlite
                 continue ; // продолжаем со следующего файла
              }

              if (vRetVal!="") {
                 AddLogString("Ошибка при работе процедуры(arc_writer_pkg.prepare_table) = " + vRetVal );
                 cnn.Close();// закрываем oRacle
                 connection.Close();// закрываем sqlite
                 continue ; // продолжаем со следующего файла
              }

              AddLogString("Вызов процедуры очистки таблицы стэка прошел успешно.");
              AddLogString("Идет перенос данных в таблицу = "+tblName+". Подождите...");

            }

            string folderName = @"c:\retro";
            //
            folderName=System.IO.Path.GetDirectoryName(file);

            // Create a file name for the file you want to create.
            //string fileNametblName = System.IO.Path.GetRandomFileName();
            string fileNametblName = tblName+".sql";

            // Use Combine again to add the file name to the path.
            string pathString = System.IO.Path.Combine(folderName, fileNametblName);

            System.IO.StreamWriter fileOutput = null;
            OdbcTransaction dbTr=null;
            if (checkBox1.Checked==true)
                 fileOutput = new System.IO.StreamWriter(pathString);
            else
               dbTr=cnn.BeginTransaction();

            string s0="", s1="", s2="", s3="", s4="", s5="";
            string strOutput = "";
            const int nCapacity= 1000 ;
            int nCnt = 0 ;
            List<string> strOutputs = new List<string>();

            AddLogString("Начало цикла обработки "+tblName+".");

            foreach (DbDataRecord record in reader) {
              // select извлек данные из sqlite и произвел их модификацию !????? в соответствии .... непонятно чем.
              // Итого, вместо . (десятичной точки) оказалась ,(запятая)
              //d3=Convert.ToDouble(record[2].ToString(),System.Globalization.CultureInfo.InvariantCulture);
              //d3=Double.Parse(record[2].ToString().Replace(".",","),System.Globalization.NumberStyles.Number);
              s0=record[0].ToString() ;
              s1=record[1].ToString() ;
              s2=record[2].ToString().Replace(",",".") ;
              s3=record[3].ToString() ;
              if (nVal>4) {
                 s4=record[4].ToString().Replace(",",".") ;
                 s5=record[5].ToString().Replace(",",".") ;
                 strOutput=string.Format(strInsert, tblName, s0, s1, s2, s3, s4, s5);
              }  else {
                 strOutput=string.Format(strInsert, tblName, s0, s1, s2, s3);
              }

              strOutputs.Add(strOutput);
              nCnt = nCnt + 1 ;
              // Пишем группой по 1000 строк
              if (nCnt>=nCapacity) {

                if (checkBox1.Checked==true) {
                    foreach (string strTemp1 in strOutputs) fileOutput.WriteLine(strTemp1);
                } else {
                    //strOutputs.Add("commit;");
                    // вариант засовывания файла в БД с помощью пакета arc_writer_pkg
                    cmd.CommandType = System.Data.CommandType.Text ;
                    cmd.Transaction = dbTr;
                    cmd.Parameters.Clear();
                    foreach (string strTemp2 in strOutputs) {
                       cmd.CommandText = strTemp2 ;
                       try
                       {
                          cmd.ExecuteNonQuery();
                       }
                       catch (Exception ex9)
                       {
                          AddLogString("Ошибка при выполнении INSERT ="+ex9.Message);
                          break ;
                       }
                    }
                    ////try
                    ////{
                    ////   // Commit the transaction.
                    ////   dbTr.Commit();
                    ////}
                    ////catch (Exception ex10)
                    ////{
                    ////   AddLogString("Ошибка при выполнении Commit ="+ex10.Message);
                    ////   try
                    ////   {
                    ////      // Attempt to roll back the transaction.
                    ////      dbTr.Rollback();
                    ////   }
                    ////   catch
                    ////   {
                    ////      // Do nothing here; transaction is not active.
                    ////   }
                    ////   break ;
                    ////}
                    strOutputs.Clear();
                    nCnt = 0;
               }

              } // (nCnt>=nCapacity)

            } // foreach


            // Этот кусок необходим для записи оставшихся элементов блока
              if (nCnt>0) {

                if (checkBox1.Checked==true) {
                    foreach (string strTemp1 in strOutputs) fileOutput.WriteLine(strTemp1);
                } else {
                    //strOutputs.Add("commit;");
                    // вариант засовывания файла в БД с помощью пакета arc_writer_pkg
                    cmd.CommandType = System.Data.CommandType.Text ;
                    cmd.Transaction = dbTr;
                    cmd.Parameters.Clear();
                    foreach (string strTemp2 in strOutputs) {
                       cmd.CommandText = strTemp2 ;
                       try
                       {
                          cmd.ExecuteNonQuery();
                       }
                       catch (Exception ex9)
                       {
                          AddLogString("Ошибка при выполнении INSERT ="+ex9.Message);
                          break ;
                       }
                    }
                    ////try
                    ////{
                    ////   // Commit the transaction.
                    ////   dbTr.Commit();
                    ////}
                    ////catch (Exception ex10)
                    ////{
                    ////   AddLogString("Ошибка при выполнении Commit ="+ex10.Message);
                    ////   try
                    ////   {
                    ////      // Attempt to roll back the transaction.
                    ////      dbTr.Rollback();
                    ////   }
                    ////   catch
                    ////   {
                    ////      // Do nothing here; transaction is not active.
                    ////   }
                    ////   //break ;
                    ////}
                    strOutputs.Clear();
                    nCnt = 0;
               }

              } // (nCnt>=0)


            AddLogString("Конец цикла обработки "+tblName+".");

            if (checkBox1.Checked==true) {
              fileOutput.Flush();
              fileOutput.Close();
              AddLogString("\t File = " + pathString + " - создан.");
            } else {

              AddLogString("Вызов процедуры переноса данных (arc_writer_pkg.MOVE_DATA) из таблицы стэка. Подождите......");

              cmd.CommandType = System.Data.CommandType.StoredProcedure;
              cmd.Parameters.Clear();
              cmd.CommandText = "{? = call arc_writer_pkg.move_data( ? )}";

              OdbcParameter parOut = new OdbcParameter();
              parOut.Direction = System.Data.ParameterDirection.ReturnValue;
              parOut.OdbcType = OdbcType.NChar;
              parOut.ParameterName = "vRetVal";
              parOut.Size = 1024;

              cmd.Parameters.Add(parOut);

              OdbcParameter param = new OdbcParameter();
              param.Direction = System.Data.ParameterDirection.Input;
              param.OdbcType = OdbcType.NChar;
              param.ParameterName = "pTname";
              param.Value = tblName;
              param.Size = 1024 ;

              cmd.Parameters.Add(param);

              vRetVal = "" ;
              try
              {
                cmd.ExecuteNonQuery();
                vRetVal = cmd.Parameters["vRetVal"].Value.ToString() ;
              }
              catch (Exception ex11)
              {
                AddLogString("Не удалось вызвать процедуру переноса данных из таблицы стэка.");
                AddLogString("Ошибка вызова процедуры ="+ex11.Message);
              }

              if (vRetVal!="") {
                  AddLogString("Ошибка при работе процедуры (arc_writer_pkg.MOVE_DATA) = " + vRetVal );
              } else {
                  AddLogString("Вызов процедуры переноса данных из таблицы стэка прошел успешно." );
              }

              cnn.Close();

            }

            connection.Close();
            AddLogString("Обработка БД Sqlite = " + file + " - завершена.");

       } // for file
       AddLogString("...............................Finished.");
      // listView1.EndUpdate();
    }
    void ListView1DragDrop(object sender, DragEventArgs e)
    {
       string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
       //button3.Enabled=true;
       AddFiles(files) ;
       //button3.Enabled=false;
    }
    void ListView1DragEnter(object sender, DragEventArgs e)
    {
       if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
       {
          e.Effect = DragDropEffects.All;
       }
    }
    void BackgroundWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
       // This event handler is where the actual work is done.
       // This method runs on the background thread.
       // Get the BackgroundWorker object that raised this event.
       System.ComponentModel.BackgroundWorker worker;
       worker = (System.ComponentModel.BackgroundWorker)sender;
    }
    void BackgroundWorker1ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
    {
       // This method runs on the main thread.
    }
    void BackgroundWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
       // This event handler is called when the background thread finishes.
       // This method runs on the main thread.
       if (e.Error != null)
          MessageBox.Show("Error: " + e.Error.Message);
       else if (e.Cancelled)
               MessageBox.Show("canceled.");
            else
                MessageBox.Show("Finished");
    }
    void Button3Click(object sender, EventArgs e)
    {
       // stop=cancel
       // Cancel the asynchronous operation.
       this.backgroundWorker1.CancelAsync();
    }
    void Button7Click(object sender, EventArgs e)
    {
       // установка промежутка времени из файла в Компоненты Формы
       if (listView1.SelectedItems.Count <= 0) {
         AddLogString("Для установки Даты и Времени - выделите соответствующий файл.");
         return ;
       }

       ListViewItem itm = null;
       for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
       {
          itm = listView1.SelectedItems[i];
          break ; // устанавливаем только для одного элемента в выделенном списке
       }

       string startTime=itm.SubItems[1].Text ;
       string endTime =itm.SubItems[2].Text ;

       CultureInfo provider = null ; // CultureInfo.InvariantCulture;
       string frm = "yyyy-MM-dd HH:mm:ss" ; // Дата = yyyy-MM-dd HH:mm:ss
       DateTime d ;

       //устанавливаем  даты

       // начало  monthCalendar1  dateTimePicker1
       try {
          d = DateTime.ParseExact(startTime, frm, provider);
       }
       catch (FormatException) {
          d = DateTime.Now;
       }
       //monthCalendar1.SelectionStart= DateTime.ParseExact(startTime, frm, provider);
       //monthCalendar1.SelectionEnd= DateTime.ParseExact(startTime, frm, provider);
       monthCalendar1.SetDate(d);
       monthCalendar1.TodayDate=d;
       monthCalendar1.Update();
       dateTimePicker1.Value = DateTime.Parse(startTime);

       try {
          d = DateTime.ParseExact(endTime, frm, provider);
       }
       catch (FormatException) {
          d = DateTime.Now;
       }
       // конец   monthCalendar2   dateTimePicker2
       monthCalendar2.SetDate(d);
       monthCalendar2.TodayDate=d;
       monthCalendar2.Update();
       dateTimePicker2.Value = DateTime.Parse(endTime);
    }

 }
}
