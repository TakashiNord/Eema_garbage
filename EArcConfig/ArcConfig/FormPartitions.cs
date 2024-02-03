/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 25.11.2023
 * Time: 16:36
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Data.Odbc;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Threading.Tasks ;


namespace ArcConfig
{
  /// <summary>
  /// Description of FormPartitions.
  /// </summary>
  public partial class FormPartitions : Form
  {
    public FormPartitions()
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
    public String _id;
    public String _name;
    public String _id_gpt;
    public String _gpt_name;
    public String _id_tbl;
    public int _OptionSchemaName = 0;
    public string OptionSchemaMain = "RSDUADMIN";
    public int OptionSaveFormat = 0 ;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormPartitions(OdbcConnection conn, String id, String name, String ginfo,String gpt_name, String tbl, int SchemaName )
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
      _gpt_name = gpt_name ;
      _id_tbl = tbl ;
      _OptionSchemaName = SchemaName ;
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

/*
CREATE TABLE ARC_VIEW_PARTITIONS (
    ID         DECIMAL       NOT NULL,
    ID_GINFO   DECIMAL       NOT NULL,
    ID_TBLLST  DECIMAL       NOT NULL,
    DEPTH      DECIMAL       NOT NULL,
    IS_ACTIVE  DECIMAL,
    TABLE_NAME NVARCHAR (63) NOT NULL,
    DT_START   DECIMAL,
    DT_END     DECIMAL

    SELECT ID,ID_GINFO,ID_TBLLST,DEPTH,IS_ACTIVE,TABLE_NAME,DT_START,DT_END
    FROM ARC_VIEW_PARTITIONS
    WHERE ID_GINFO=<id> AND ID_TBLLST=<tbl>
);*/

    public void ARC_VIEW_PARTITIONS(object sender)
    {
      //
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcCommand cmd1 = new OdbcCommand();
      OdbcDataReader reader = null ;
      // Объект для связи между базой данных и источником данных
      OdbcDataAdapter adapter = new OdbcDataAdapter();

      cmd0.Connection=this._conn;
      cmd1.Connection=this._conn;

      try {
        dataGridViewPart.DataSource = null ;
        dataGridViewPart.Rows.Clear();
        Application.DoEvents();
        dataGridViewPart.Columns.Clear();
      }
      catch (Exception ex1)
      {
        ; //MessageBox.Show("Error ="+ex1.Message);
      }

      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }

      string sl1 = "" ;
      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

      sl1 = r.GetString("ARC_VIEW_PARTITIONS1");
      sl1 = String.Format(sl1, stSchema + "ARC_VIEW_PARTITIONS" , _id , _id_tbl );

      cmd0.CommandText=sl1;

      dataSet1.Clear();

      // Указываем запрос для выполнения
      adapter.SelectCommand = cmd0;

      int selarc = 0 ;
      int a = 0;
      // Заполняем объект источника данных
      try {
        a = adapter.Fill(dataSet1);
      }
      catch (Exception ex1)
      {
        selarc = 1 ;
        sl1 = r.GetString("ARC_VIEW_PARTITIONS2");
        sl1 = String.Format(sl1, stSchema + "ARC_VIEW_PARTITIONS" , _id , _id_tbl );
        cmd0.CommandText=sl1;
        adapter.SelectCommand = cmd0;
        a = adapter.Fill(dataSet1);
      }

      // Запрет удаления данных
      dataSet1.Tables[0].DefaultView.AllowDelete = false;
      // Запрет модификации данных
      dataSet1.Tables[0].DefaultView.AllowEdit = false;
      // Запрет добавления данных
      dataSet1.Tables[0].DefaultView.AllowNew = false;

      // (с этого момента она будет отображать его содержимое)
      dataGridViewPart.DataSource = dataSet1.Tables[0].DefaultView;;

      // Set up the data source.
      dataGridViewPart.Update();

      DateTime t0 ;
      double vl1 = 0 ;
      for (int ii = 0; ii < dataGridViewPart.RowCount ; ii++) {

        // нумерация
        dataGridViewPart.Rows[ii].HeaderCell.Value = (ii + 1).ToString();

        // цвет  IS_ACTIVE = поле = 4
        int IS_ACTIVE = 4 ;
        DataGridViewCellStyle cellStyle2 = new DataGridViewCellStyle();
        cellStyle2 = dataGridViewPart.Rows[ii].Cells[IS_ACTIVE].Style ; //
        String SIS_ACTIVE = Convert.ToString(dataGridViewPart.Rows[ii].Cells[IS_ACTIVE].Value) ;
        if (SIS_ACTIVE=="1")
                  cellStyle2.BackColor  = Color.Red ;
        dataGridViewPart.Rows[ii].Cells[IS_ACTIVE].Style=cellStyle2;

        if (selarc == 1) {
          //Unix -> DateTime DT_START = 6 - > 9
          vl1 = Convert.ToDouble(dataGridViewPart.Rows[ii].Cells[6].Value);
          t0 = UnixTimestampToDateTime(vl1) ;
          //t0=t0.ToUniversalTime() ;
          t0=t0.ToLocalTime();
          dataGridViewPart.Rows[ii].Cells[9].Value=t0.ToString("u"); // u s o
          //Unix -> DateTime  DT_END = 7 - > 10
          vl1 = Convert.ToDouble(dataGridViewPart.Rows[ii].Cells[7].Value);
          t0 = UnixTimestampToDateTime(vl1) ;
          //t0=t0.ToUniversalTime() ;
          t0=t0.ToLocalTime();
          dataGridViewPart.Rows[ii].Cells[10].Value=t0.ToString("u"); // u s o
        }

        // table name  TABLE_NAME = 5
        //object crec1 = null ;
        //cmd1.CommandText="SELECT count(*) FROM " + Convert.ToString(dataGridViewPart.Rows[ii].Cells[5].Value) ;
        //try
        //{
        //  crec1 = cmd1.ExecuteScalar();
        //}
        //catch (Exception ex1)
        //{
        //  continue;
        //}
        //dataGridViewPart.Rows[ii].Cells[8].Value=Convert.ToString(crec1) ; // CNT

      } //for


      // Resize the master DataGridView columns to fit the newly loaded data.
      dataGridViewPart.AutoResizeColumns();

      cmd0.Dispose();
      Application.DoEvents();

    }
    void FormPartitionsLoad(object sender, EventArgs e)
    {
      this.Text = this.Text + " ( " + _name + " , " + _gpt_name +" )" ;
      ARC_VIEW_PARTITIONS(sender);
    }
    void ExportToolStripMenuItemClick(object sender, EventArgs e)
    {
      // export
      // экспорт в csv

      var  dataGridView = (DataGridView)null ;
      dataGridView = (DataGridView)dataGridViewPart;

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
      MessageBox.Show("Export to \n " + filename + " \n done");      
      //MessageBox.Show("Nothing ...");
    }
    void CountToolStripMenuItemClick(object sender, EventArgs e)
    {
      // count partition
      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd1 = new OdbcCommand();

      cmd1.Connection=this._conn;

      for (int ii = 0; ii < dataGridViewPart.RowCount ; ii++) {
        // table name  TABLE_NAME = 5
        object crec1 = null ;
        cmd1.CommandText="SELECT count(*) FROM " + Convert.ToString(dataGridViewPart.Rows[ii].Cells[5].Value) ;
        try
        {
          crec1 = cmd1.ExecuteScalar();
        }
        catch (Exception ex1)
        {
          continue;
        }
        dataGridViewPart.Rows[ii].Cells[8].Value=Convert.ToString(crec1) ; // CNT
        
        Application.DoEvents();
      } //for

    }
		void DropToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("Nothing ...");
		}
		void CreateToolStripMenuItemClick(object sender, EventArgs e)
		{
	        MessageBox.Show("Nothing ...");
		}


  }
}
