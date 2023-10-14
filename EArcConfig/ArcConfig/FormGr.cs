/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 03.03.2021
 * Time: 15:59
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Data.Odbc;
using System.Reflection;
using System.Resources;

namespace ArcConfig
{
  /// <summary>
  /// Description of FormGr.
  /// </summary>
  public partial class FormGr : Form
  {
    public FormGr()
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
    public String TABLE_NAME ;
    public string OptionSchemaMain = "RSDUADMIN";

    public int valueBefore = 0;

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }

    public FormGr(OdbcConnection conn, String id, String name, String ginfo,String gpt_name, String tbl, int SchemaName)
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
      TABLE_NAME = "" ;
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


    public void GrLoad(int from, int to )
    {
        // Объект для связи между базой данных и источником данных
        OdbcDataAdapter adapter = new OdbcDataAdapter();
        // Объект для выполнения запросов к базе данных
        OdbcCommand cmd0 = new OdbcCommand();
        //OdbcDataReader reader = null ;

        cmd0.Connection=this._conn;

        string sl1 = "SELECT count(*) FROM " + TABLE_NAME +
            " WHERE TIME1970 BETWEEN " +from.ToString() + " AND " + to.ToString()  ;

        //TIME1970, TIME_MKS, VAL, STATE
        sl1 = "SELECT CAST(TIME1970 AS CHAR(12)) AS TIME1970, VAL, STATE FROM " + TABLE_NAME +
            " WHERE TIME1970 BETWEEN " +from.ToString() + " AND " + to.ToString() +
            " ORDER BY TIME1970 ASC " ;
        cmd0.CommandText=sl1;

        try {
          dataGridView1.DataSource = null ;
          dataGridView1.Rows.Clear();
          Application.DoEvents();
          dataGridView1.Columns.Clear();
        }
        catch (Exception)
        {
          ;
        }
        // dataGridView1.DataSource = new object();
        Application.DoEvents();

        dataSet1.Clear();

        // Указываем запрос для выполнения
        adapter.SelectCommand = cmd0;

        // Заполняем объект источника данных
        int a = adapter.Fill(dataSet1);
        toolStripStatusLabel3.Text = "Load = " + a.ToString() + " records" ;

        // DataTable table = new DataTable();
        // table.Locale = System.Globalization.CultureInfo.InvariantCulture;
        // adapter.Fill(table);

        // Запрет удаления данных
        dataSet1.Tables[0].DefaultView.AllowDelete = false;
        // Запрет модификации данных
        dataSet1.Tables[0].DefaultView.AllowEdit = false;
        // Запрет добавления данных
        dataSet1.Tables[0].DefaultView.AllowNew = false;

        // (с этого момента она будет отображать его содержимое)
        dataGridView1.DataSource = dataSet1.Tables[0].DefaultView;;

        // Set up the data source.
        //bindingSource1.DataSource = dataSet1;
        //dataGridView1.DataSource = bindingSource1;
        dataGridView1.Update();

        DateTime t0 ;
        double minV = 0 ;
        double vl1 = 0 , vl2 = 0;
        for (int ii = 0; ii < dataGridView1.RowCount ; ii++) {
           //Unix -> DateTime
           vl1 = Convert.ToDouble(dataGridView1.Rows[ii].Cells[0].Value);
           if (Math.Abs(vl2-vl1)> minV) minV = Math.Abs(vl2-vl1) ;
           t0 = UnixTimestampToDateTime(vl1) ;
           //t0=t0.ToUniversalTime() ;
           t0=t0.ToLocalTime();
           dataGridView1.Rows[ii].Cells[0].Value=t0.ToString("u"); // u s o
           vl2 = vl1 ;
        }

        myChart.Series.Clear();

        if (null == myChart.ChartAreas.FindByName(TABLE_NAME) )
          //добавляем в Chart область для рисования графиков, их может быть
          //много, поэтому даем ей имя.
          myChart.ChartAreas.Add(new ChartArea(TABLE_NAME));

        //Создаем и настраиваем набор точек для рисования графика, в том
        //не забыв указать имя области на которой хоти м отобразить этот
        //набор точек.
        Series mySeriesOfPoint = new Series("DATA");

/*
0        SeriesChartType.Point,
1        SeriesChartType.FastPoint
2        SeriesChartType.Bubble
3        SeriesChartType.Line
4        SeriesChartType.Spline
5        SeriesChartType.StepLine
6        SeriesChartType.FastLine
7        SeriesChartType.Bar
8        SeriesChartType.StackedBar
9        SeriesChartType.StackedBar100
10        SeriesChartType.Column
11        SeriesChartType.StackedColumn
12        SeriesChartType.StackedColumn100
13        SeriesChartType.Area
14        SeriesChartType.SplineArea
15        SeriesChartType.StackedArea
16        SeriesChartType.StackedArea100
17        SeriesChartType.Pie
18        SeriesChartType.Doughnut
19        SeriesChartType.Stock
20        SeriesChartType.Candlestick
21        SeriesChartType.Range
22        SeriesChartType.SplineRange
23        SeriesChartType.RangeBar
24        SeriesChartType.RangeColumn
25        SeriesChartType.Radar
26        SeriesChartType.Polar
27        SeriesChartType.ErrorBar
28        SeriesChartType.BoxPlot
29        SeriesChartType.Renko
30        SeriesChartType.ThreeLineBreak
31        SeriesChartType.Kagi
32        SeriesChartType.PointAndFigure
33        SeriesChartType.Funnel
34        SeriesChartType.Pyramid
*/


        mySeriesOfPoint.ChartType = (SeriesChartType)comboBox1.SelectedIndex; //SeriesChartType.StepLine;
        //mySeriesOfPoint.MarkerStyle = MarkerStyle.None;
        mySeriesOfPoint.ChartArea = TABLE_NAME;
        mySeriesOfPoint.XValueType = ChartValueType.DateTime;
        mySeriesOfPoint.IsXValueIndexed = true ;
        //myChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
        //myChart.ChartAreas[0].AxisX.Interval = minV ;
        myChart.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
        mySeriesOfPoint.XValueMember = dataGridView1.Columns[0].DataPropertyName;
        mySeriesOfPoint.YValueMembers = dataGridView1.Columns[1].DataPropertyName;
        //Добавляем созданный набор точек в Chart
        myChart.Series.Add(mySeriesOfPoint);
        myChart.DataSource = dataGridView1.DataSource;
        myChart.DataBind();
        //
    }



    void FormGrLoad(object sender, EventArgs e)
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

        string sl1 = r.GetString("ARH_SYSTBLLST1");
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
        if (reader.HasRows) {
          while (reader.Read())
          {
            ARC_NAME = GetTypeValue(ref reader, 0).ToUpper() ;
            break ;
          } // while
        }
        reader.Close();

        if (ARC_NAME=="") return ;
        Application.DoEvents();

        sl1 = "SELECT RETFNAME FROM "+stSchema + ARC_NAME + " WHERE ID_PARAM=" +_id + " AND ID_GINFO=" +_id_gpt ;
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
            TABLE_NAME = GetTypeValue(ref reader, 0).ToUpper() ;
            break ;
          } // while
        }
        reader.Close();

        if (TABLE_NAME=="") return ;
        Application.DoEvents();

        // min max cnt
        string[] ext1 = new string[3];

        sl1 = r.GetString("TABLEOPTION");
        sl1 = String.Format(sl1,TABLE_NAME );
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

        toolStripStatusLabel1.Text = TABLE_NAME ;
        toolStripStatusLabel2.Text = "Число записей = "+ ext1[2] +
          " ; Период={" + t1.ToString() + " ; " + t2 + "}" ;
        Application.DoEvents();

        // TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0,DateTimeKind.Utc));
        // double unixTime = span.TotalSeconds;

        DateTime date1 = DateTime.Today; // DateTime.Now; // DateTime.UtcNow  DateTime.Today
        //dateTimePicker1.Value.ToShortDateString();

        //Set a custom format containing the string "of the year"
        dateTimePicker1.Format = DateTimePickerFormat.Custom;
        dateTimePicker1.CustomFormat = "dd'.'MM'.'yyyyy hh':'mm tt";
        // from
        DateTime d01 = date1.AddHours(-6); //  AddHours(-24) ; AddDays(-1);
        dateTimePicker1.Value = d01;
        // to
        dateTimePicker2.Format = DateTimePickerFormat.Custom;
        dateTimePicker2.CustomFormat = "dd'.'MM'.'yyyyy hh':'mm tt";
        dateTimePicker2.Value = date1.AddSeconds(6*60*60-1);

        trackBar1.Minimum=-31;
        trackBar1.Maximum=+31;
        trackBar1.Value=0;
        valueBefore = trackBar1.Value;

        tabPage1.Text = _name ;

        this.Text= "Archive = " + _gpt_name + " ; Parameter = " + _name ;

        int i1 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker1.Value) );
        int i2 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker2.Value) );

        comboBox1.SelectedIndex=5;

        try {
          GrLoad(i1,i2) ;
        }
        catch (Exception)
        {
           ;
        }

      }
    void Button1Click(object sender, EventArgs e)
    {
       int i1 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker1.Value) );
       int i2 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker2.Value) );
       if (i1>=i2) {
         MessageBox.Show(" Период установлен не верно!");
         return ;
       }
       try {
         GrLoad(i1,i2) ;
       }
       catch (Exception)
       {
          ;
       }
    }
    void TrackBar1Scroll(object sender, EventArgs e)
    {
       DateTime dt = DateTime.Now;
       int hr = 0 ;

       // уменьшаем
       if (trackBar1.Value < valueBefore)
       {
           hr = -24 ;
       } else {
         // увеличиваем
         hr = 24 ;
       }
       valueBefore = trackBar1.Value;

       dt = dateTimePicker1.Value ;
       dateTimePicker1.Value = dt.AddHours(hr) ;
       dt= dateTimePicker2.Value ;
       dateTimePicker2.Value = dt.AddHours(hr) ;

       Button1Click(null,  e) ;
    }

    void ToolStripButton1Click(object sender, EventArgs e)
    {
      // delete media

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      int res1 = 0 ;

      cmd0.Connection=this._conn;

      if (TABLE_NAME=="") return ;
      Application.DoEvents();

      int i1 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker1.Value) );
      int i2 = Convert.ToInt32( DateTimeToUnixTimestamp(dateTimePicker2.Value) );
      if (i1>=i2) {
          MessageBox.Show(" Период установлен не верно!\n Ничего не удалено");
          return ;
      }

      DialogResult result = MessageBox.Show ("Удалить значения из таблицы?\n\n\n" +
                " TABLE_NAME = " + TABLE_NAME + "\n" +
                "  from = "+dateTimePicker1.Value.ToString() +"\n" +
                "    to = "+dateTimePicker2.Value.ToString() +"\n" ,
               "Удалить значения..", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,
               MessageBoxDefaultButton.Button2);
      if (result == DialogResult.Yes)
      {
           cmd0.CommandText="DELETE FROM " + TABLE_NAME +
             " WHERE time1970 BETWEEN " + i1.ToString() + " AND "+ i2.ToString() ;
           try
           {
               res1 = cmd0.ExecuteNonQuery();
           }
           catch (Exception ex1)
           {
               MessageBox.Show(ex1.Message);
               return ;
           }
           MessageBox.Show("Операция удаления прошла успешно\n Удалено (записей) = " + res1.ToString() );
      }
    }
    void ToolStripButton2Click(object sender, EventArgs e)
    {
       // export data
       if (TABLE_NAME=="") return ;
       Application.DoEvents();

       FormExport fe = new FormExport(_conn, _id, _name, _id_gpt, _id_tbl, _OptionSchemaName) ;
       fe.ShowDialog();
    }

  }

}
