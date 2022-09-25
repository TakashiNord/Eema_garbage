/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 28.04.2021
 * Time: 11:48
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
using System.IO;
using System.Text;
using System.Threading.Tasks ;

namespace ArcConfig
{
  /// <summary>
  /// Description of FormExport.
  /// </summary>
  public partial class FormExport : Form
  {
    public FormExport()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
    }


    public OdbcConnection _conn;
    public String _id;
    public String _name;
    public String _id_gpt;
    public String _gpt_name;
    public String _id_tbl;
    public String _TABLE_NAME ;
    public String _SCHEME_NAME ;
    public String _filename ;
    public int _OptionSchemaName = 0;
    public string OptionSchemaMain = "RSDUADMIN";

    public int valueBefore = 0;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormExport(OdbcConnection conn, String id, String name, String ginfo,String tbl, int SchemaName)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      _conn = conn ;
      _id = id ;
      _name = name ;
      _id_gpt = ginfo ;
      _id_tbl = tbl ;
      _TABLE_NAME = "" ;
      _SCHEME_NAME = "" ;
      _OptionSchemaName = SchemaName ;
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
        _SCHEME_NAME = "" ;
        if (reader.HasRows) {
          while (reader.Read())
          {
            ARC_NAME = GetTypeValue(ref reader, 2).ToUpper() ;
            _SCHEME_NAME = GetTypeValue(ref reader, 3).ToUpper() ;
            break ;
          } // while
        }
        reader.Close();

        if (ARC_NAME=="") return ;
        Application.DoEvents();


        sl1 = "SELECT RETFNAME FROM " + stSchema + ARC_NAME + " WHERE ID_PARAM=" +_id + " AND ID_GINFO=" +_id_gpt ;
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

        if (reader.HasRows) {
          while (reader.Read())
          {
            _TABLE_NAME = GetTypeValue(ref reader, 0).ToUpper() ;
            break ;
          } // while
        }
        reader.Close();

        if (_TABLE_NAME=="") return ;
        Application.DoEvents();

        // min max cnt
        string[] ext1 = new string[3];

        sl1 = r.GetString("TABLEOPTION");
        sl1 = String.Format(sl1,_TABLE_NAME );
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

        if (reader.HasRows) {
          while (reader.Read())
          {
            string[] arr = new string[2];
            for (int i = 0;i<2;i++) {
              arr[i] = GetTypeValue(ref reader, i).ToUpper() ;
            }
            if (arr[0]=="MIN") ext1[0]=arr[1];
            if (arr[0]=="MAX") ext1[1]=arr[1];
            if (arr[0]=="CNT") ext1[2]=arr[1];
          } // while
        }
        reader.Close();

        //Unix -> DateTime
        DateTime t1 = UnixTimestampToDateTime(Convert.ToDouble(ext1[0]));
        DateTime t2 = UnixTimestampToDateTime(Convert.ToDouble(ext1[1]));

        Application.DoEvents();

        dateTimePicker1.Value = t1;
        dateTimePicker2.Value = t2;
        
        toolStripStatusLabel1.Text = "Total="+ext1[2];
        int rq = 86400;
        int.TryParse(ext1[2], out rq);
        toolStripProgressBar1.Maximum = rq ;

      }


    void FormExportLoad(object sender, EventArgs e)
    {
       comboBoxFormat.SelectedIndex=0;
       textBoxFile.Text = "";
       comboBoxColon.SelectedIndex = 0 ;
       checkBoxExportSel.Checked = false ; checkBoxExportSel.Enabled = false ;
       checkBoxIncludeSch.Checked = false ;
       checkBoxOnly500.Checked = false ;
       checkBoxDelimiterSE.Checked = false ;
       checkBoxUseTime.Checked = false ;
       groupBoxTime.Enabled=false;
       numericUpDown1.Value = 0 ;
       toolStripProgressBar1.Value =0 ;
       toolStripProgressBar1.Minimum =0;
       toolStripProgressBar1.Step = 1;
       DateTime date1 = DateTime.Today;
       DateTime d01 = date1.AddDays(-1);
       dateTimePicker1.Value = d01; //  AddHours(-24) ;
       dateTimePicker2.Value = date1.AddSeconds(86400-1);
       Load1();
       this.Text = this.Text + " " +_SCHEME_NAME + "." + _TABLE_NAME ;
       string startupPath = System.IO.Directory.GetCurrentDirectory();
       saveFileDialog1.InitialDirectory = @"Desktop" ; //startupPath;
       saveFileDialog1.FilterIndex = 0;
       _filename = _TABLE_NAME + ".csv";

       textBoxFile.Text = _filename ;
    }
    void CheckBoxUseTimeCheckedChanged(object sender, EventArgs e)
    {
       groupBoxTime.Enabled=checkBoxUseTime.Checked;
    }

    int Export1()
    {
        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;

        cmd0.Connection=this._conn;

        int Cnt = 0 ; // количество выводимых строк

        //вставляем commit
        int Commit = Convert.ToInt32(numericUpDown1.Value);
        if (Commit<0) Commit=0;


        String Colon = ";" ;
        if (comboBoxColon.SelectedIndex==1) Colon = "," ;

        String DelimiterSE = "" ;
        if (checkBoxDelimiterSE.Checked) DelimiterSE = Colon ;

        // выводить схему
        String TableName =  _TABLE_NAME ;
        if (checkBoxIncludeSch.Checked)  TableName = _SCHEME_NAME + "." + _TABLE_NAME ;

        //TIME1970, TIME_MKS, VAL, STATE
        string sl1 = "SELECT TIME1970, VAL, STATE FROM " + TableName + " ORDER BY TIME1970 DESC " ;

        if (checkBoxUseTime.Checked==true) {
           int i1 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker1.Value) );
           int i2 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker2.Value) );
           sl1 = "SELECT TIME1970, VAL, STATE FROM " + TableName +
            " WHERE TIME1970 BETWEEN " +i1.ToString() + " AND " + i2.ToString() +
            " ORDER BY TIME1970 DESC " ;
        }

        cmd0.CommandText=sl1;
        try
        {
          reader = cmd0.ExecuteReader();
        }
        catch (Exception ex1)
        {
          MessageBox.Show(ex1.Message);
          return (-1);
        }

        String fileContent="" ;
        DateTime t0 ;
        double vl1 = 0 ;

       //формируем заголовок
       switch(comboBoxFormat.SelectedIndex)
       {
         case 0 : //csv
                fileContent += DelimiterSE + "TIME1970" + Colon + "VAL"+ Colon + "STATE" + DelimiterSE;
                fileContent +=  Environment.NewLine;
         break;
         case 1 : //html
                fileContent += "<!doctype html>" + Environment.NewLine;
                fileContent += "<html lang=\"ru\">" + Environment.NewLine;
                fileContent += "<head>"+ Environment.NewLine;
                fileContent += "   <meta charset=\"utf-8\" />"+ Environment.NewLine;
                fileContent += "   <title>" + TableName + "</title>"+ Environment.NewLine;
                fileContent += "</head>"+ Environment.NewLine;
                fileContent += "<body>"+ Environment.NewLine;
                fileContent += "<table border=\"1\">"+ Environment.NewLine;
                fileContent += "<caption>"  + TableName + "</caption>"+ Environment.NewLine;
                fileContent += "<tr>";
                fileContent += "<th>TIME</th><th>VAL</th><th>STATE</th>";
                fileContent += "</tr>"+ Environment.NewLine;
                // </table>
                // </body>
                // </html>
         break;
         case 2 : //insert
                fileContent += " -- " + TableName;
                fileContent += Environment.NewLine;
         break;
         case 3 : //merge
                fileContent += " -- " + TableName;
                fileContent += Environment.NewLine;
         break;
         default : break;
      }
       
       Application.DoEvents();

      if (reader.HasRows) {
        string v0,v1,v2 ;
        while (reader.Read())
        {
          v0 = GetTypeValue(ref reader, 0).ToUpper() ;
          v1 = GetTypeValue(ref reader, 1).ToUpper() ;
          v2 = GetTypeValue(ref reader, 2).ToUpper() ;

          switch(comboBoxFormat.SelectedIndex)
          {
            case 0 : //csv
              vl1 = Convert.ToDouble(v0);
              t0 = UnixTimestampToDateTime(vl1) ;
              //t0=t0.ToUniversalTime() ;
              t0=t0.ToLocalTime();

              fileContent += DelimiterSE;
              fileContent += t0.ToString("u") + Colon + v1 + Colon + v2 + DelimiterSE;
              fileContent += Environment.NewLine;
              Cnt ++ ;
            break;
            case 1 : //html
              vl1 = Convert.ToDouble(v0);
              t0 = UnixTimestampToDateTime(vl1) ;
              //t0=t0.ToUniversalTime() ;
              t0=t0.ToLocalTime();

              fileContent += "<tr><td>" +t0.ToString("u") + "</td><td>" + v1 + "</td><td>" + v2 + "</td></tr>";
              fileContent += Environment.NewLine;
              Cnt ++ ;
            break;
            case 2 : //insert
              fileContent += "INSERT INTO " + TableName + " (TIME1970, VAL, STATE) VALUES (";
              fileContent += v0 + "," + v1 + "," + v2 + ");";
              fileContent += Environment.NewLine;
              Cnt ++ ;
              if (Commit>0) if (Cnt%Commit==0) fileContent += "COMMIT;"+Environment.NewLine;

            break;
            case 3 : //merge
              fileContent += "MERGE INTO "+ TableName +" A USING " +
"(SELECT "+ v0 + " as \"TIME1970\","+ v1 + " as \"VAL\", "+v2+" as \"STATE\" FROM DUAL) B " +
" ON (A.TIME1970 = B.TIME1970) " +
" WHEN NOT MATCHED THEN " +
" INSERT ( TIME1970, VAL, STATE) VALUES ( B.TIME1970, B.VAL, B.STATE) " +
" WHEN MATCHED THEN " +
" UPDATE SET A.VAL = B.VAL, A.STATE = B.STATE ;" ;
              fileContent += Environment.NewLine;
              Cnt ++ ;
              if (Commit>0) if (Cnt%Commit==0) fileContent += "COMMIT;"+Environment.NewLine;
            break;
            default : break;
           }
          
          Application.DoEvents();
          
          toolStripProgressBar1.PerformStep();

           // ограничение на общее колич выводимых строк
           if (checkBoxOnly500.Checked)
                if (Cnt>=500) break ;

       } // while

      }
      reader.Close();

      //формируем footer
      switch(comboBoxFormat.SelectedIndex)
      {
        case 0 : //csv
        break;
        case 1 : //html
               fileContent += "</table>"+ Environment.NewLine;
               fileContent += "</body>"+ Environment.NewLine;
               fileContent += "</html>"+ Environment.NewLine;
        break;
        case 2 : //insert
               fileContent += "--"+ Environment.NewLine;
               fileContent += "COMMIT;"+ Environment.NewLine;
               fileContent += "--"+ Environment.NewLine;
        break;
        case 3 : //merge
               fileContent += "--"+ Environment.NewLine;
               fileContent += "COMMIT;"+ Environment.NewLine;
               fileContent += "--"+ Environment.NewLine;
        break;
         default : break;
      }
      
      Application.DoEvents();

      // выбрасываем в файл
      StreamWriter wr = new StreamWriter(_filename, false, Encoding.UTF8); // Encoding.GetEncoding("windows-1251")
      wr.Write(fileContent);
      wr.Close();

      return(Cnt);
  }
    
    int Export2()
    {
        ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        OdbcDataReader reader = null ;

        cmd0.Connection=this._conn;

        int Cnt = 0 ; // количество выводимых строк

        //вставляем commit
        int Commit = Convert.ToInt32(numericUpDown1.Value);
        if (Commit<0) Commit=0;


        String Colon = ";" ;
        if (comboBoxColon.SelectedIndex==1) Colon = "," ;

        String DelimiterSE = "" ;
        if (checkBoxDelimiterSE.Checked) DelimiterSE = Colon ;

        // выводить схему
        String TableName =  _TABLE_NAME ;
        if (checkBoxIncludeSch.Checked)  TableName = _SCHEME_NAME + "." + _TABLE_NAME ;

        //TIME1970, TIME_MKS, VAL, STATE
        string sl1 = "SELECT TIME1970, VAL, STATE FROM " + TableName + " ORDER BY TIME1970 DESC " ;

        if (checkBoxUseTime.Checked==true) {
           int i1 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker1.Value) );
           int i2 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker2.Value) );
           sl1 = "SELECT TIME1970, VAL, STATE FROM " + TableName +
            " WHERE TIME1970 BETWEEN " +i1.ToString() + " AND " + i2.ToString() +
            " ORDER BY TIME1970 DESC " ;
        }

        cmd0.CommandText=sl1;
        try
        {
          reader = cmd0.ExecuteReader();
        }
        catch (Exception ex1)
        {
          MessageBox.Show(ex1.Message);
          return (-1);
        }

        String fileContent="" ;
        DateTime t0 ;
        double vl1 = 0 ;

       // выбрасываем в файл
       StreamWriter wr = new StreamWriter(_filename, false, Encoding.UTF8); // Encoding.GetEncoding("windows-1251")
       wr.WriteLine(fileContent);
        
       //формируем заголовок
       switch(comboBoxFormat.SelectedIndex)
       {
         case 0 : //csv
       		wr.WriteLine( DelimiterSE + "TIME1970" + Colon + "VAL"+ Colon + "STATE" + DelimiterSE);
         break;
         case 1 : //html
                wr.WriteLine( "<!doctype html>");
                wr.WriteLine( "<html lang=\"ru\">");
                wr.WriteLine( "<head>");
                wr.WriteLine( "   <meta charset=\"utf-8\" />");
                wr.WriteLine( "   <title>" + TableName + "</title>");
                wr.WriteLine( "</head>");
                wr.WriteLine( "<body>");
                wr.WriteLine( "<table border=\"1\">");
                wr.WriteLine( "<caption>"  + TableName + "</caption>");
                wr.Write("<tr>");
                wr.Write("<th>TIME</th><th>VAL</th><th>STATE</th>");
                wr.WriteLine( "</tr>");
                // </table>
                // </body>
                // </html>
         break;
         case 2 : //insert
                wr.WriteLine( " -- " + TableName);
         break;
         case 3 : //merge
                wr.WriteLine( " -- " + TableName);
         break;
         default : break;
      }
       
       Application.DoEvents();

      if (reader.HasRows) {
        string v0,v1,v2 ;
        while (reader.Read())
        {
          v0 = GetTypeValue(ref reader, 0).ToUpper() ;
          v1 = GetTypeValue(ref reader, 1).ToUpper() ;
          v2 = GetTypeValue(ref reader, 2).ToUpper() ;

          switch(comboBoxFormat.SelectedIndex)
          {
            case 0 : //csv
              vl1 = Convert.ToDouble(v0);
              t0 = UnixTimestampToDateTime(vl1) ;
              //t0=t0.ToUniversalTime() ;
              t0=t0.ToLocalTime();

              wr.WriteLine( DelimiterSE + t0.ToString("u") + Colon + v1 + Colon + v2 + DelimiterSE);
              Cnt ++ ;
            break;
            case 1 : //html
              vl1 = Convert.ToDouble(v0);
              t0 = UnixTimestampToDateTime(vl1) ;
              //t0=t0.ToUniversalTime() ;
              t0=t0.ToLocalTime();

              wr.WriteLine("<tr><td>" +t0.ToString("u") + "</td><td>" + v1 + "</td><td>" + v2 + "</td></tr>");
              Cnt ++ ;
            break;
            case 2 : //insert
              wr.WriteLine( "INSERT INTO " + TableName + " (TIME1970, VAL, STATE) VALUES (" +
                         v0 + "," + v1 + "," + v2 + ");");

              Cnt ++ ;
              if (Commit>0) if (Cnt%Commit==0) wr.WriteLine( "COMMIT;");

            break;
            case 3 : //merge
              wr.WriteLine( "MERGE INTO "+ TableName +" A USING " +
"(SELECT "+ v0 + " as \"TIME1970\","+ v1 + " as \"VAL\", "+v2+" as \"STATE\" FROM DUAL) B " +
" ON (A.TIME1970 = B.TIME1970) " +
" WHEN NOT MATCHED THEN " +
" INSERT ( TIME1970, VAL, STATE) VALUES ( B.TIME1970, B.VAL, B.STATE) " +
" WHEN MATCHED THEN " +
" UPDATE SET A.VAL = B.VAL, A.STATE = B.STATE ;") ;
              Cnt ++ ;
              if (Commit>0) if (Cnt%Commit==0) wr.WriteLine( "COMMIT;");
            break;
            default : break;
           }
          
          Application.DoEvents();
          
          toolStripProgressBar1.PerformStep();

           // ограничение на общее колич выводимых строк
           if (checkBoxOnly500.Checked)
                if (Cnt>=500) break ;

       } // while

      }
      reader.Close();

      //формируем footer
      switch(comboBoxFormat.SelectedIndex)
      {
        case 0 : //csv
        break;
        case 1 : //html
        wr.WriteLine( "</table>");
        wr.WriteLine( "</body>");
        wr.WriteLine( "</html>");
        break;
        case 2 : //insert
        wr.WriteLine( "--");
        wr.WriteLine( "COMMIT;");
        wr.WriteLine( "--");
        break;
        case 3 : //merge
        wr.WriteLine( "--");
        wr.WriteLine( "COMMIT;");
        wr.WriteLine( "--");
        break;
         default : break;
      }
      
      Application.DoEvents();

      wr.Close();

      return(Cnt);
  }    
    
    
    void ButtonOpenClick(object sender, EventArgs e)
    {
       saveFileDialog1.Filter = "CSV files(*.csv)|*.csv|Sql files(*.sql)|*.sql|html files(*.html)|*.html|All files(*.*)|*.*";
       saveFileDialog1.FileName = textBoxFile.Text ;
       if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            return;
       // получаем выбранный файл
       string f1 = saveFileDialog1.FileName;
       if (f1=="") return ;
       textBoxFile.Text = f1;
    }
    void ButtonOkClick(object sender, EventArgs e)
    {

       // Output
       //Action th = new Action( Export1 ) ;
       //Task tsk = new Task(th);
       //tsk.Start();

       _filename = textBoxFile.Text.Trim();
       int ret = Export2();
       if (ret>0) MessageBox.Show("Export to \n " + _filename + " \n done \n Total = " +ret.ToString());
    }
		void ButtonCancelClick(object sender, EventArgs e)
		{
			this.Close();
		}
		void ComboBoxFormatSelectedIndexChanged(object sender, EventArgs e)
		{
			String s1 = textBoxFile.Text.Trim() ;
			String result1 = s1;
			
      switch(comboBoxFormat.SelectedIndex)
      {
        case 0 : //csv
      		result1 = Path.ChangeExtension(s1, ".csv");
        break;
        case 1 : //html
            result1 = Path.ChangeExtension(s1, ".html");
        break;
        case 2 : //insert
            result1 = Path.ChangeExtension(s1, ".sql");
        break;
        case 3 : //merge
            result1 = Path.ChangeExtension(s1, ".sql");
        break;
         default : break;
      }
      textBoxFile.Text = result1 ;
      
      
		}




  }
}
