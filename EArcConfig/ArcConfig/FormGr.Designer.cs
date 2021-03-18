/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 03.03.2021
 * Time: 15:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormGr
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataVisualization.Charting.Chart myChart;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ComboBox comboBox1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.myChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.dataSet1 = new System.Data.DataSet();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.myChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.comboBox1);
			this.splitContainer2.Panel1.Controls.Add(this.button1);
			this.splitContainer2.Panel1.Controls.Add(this.label3);
			this.splitContainer2.Panel1.Controls.Add(this.label2);
			this.splitContainer2.Panel1.Controls.Add(this.trackBar1);
			this.splitContainer2.Panel1.Controls.Add(this.dateTimePicker2);
			this.splitContainer2.Panel1.Controls.Add(this.dateTimePicker1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer2.Size = new System.Drawing.Size(769, 387);
			this.splitContainer2.SplitterDistance = 88;
			this.splitContainer2.TabIndex = 1;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
			"SeriesChartType.Point",
			"SeriesChartType.FastPoint",
			"SeriesChartType.Bubble",
			"SeriesChartType.Line",
			"SeriesChartType.Spline",
			"SeriesChartType.StepLine",
			"SeriesChartType.FastLine",
			"SeriesChartType.Bar",
			"SeriesChartType.StackedBar",
			"SeriesChartType.StackedBar100",
			"SeriesChartType.Column",
			"SeriesChartType.StackedColumn",
			"SeriesChartType.StackedColumn100",
			"SeriesChartType.Area",
			"SeriesChartType.SplineArea",
			"SeriesChartType.StackedArea",
			"SeriesChartType.StackedArea100",
			"SeriesChartType.Pie",
			"SeriesChartType.Doughnut",
			"SeriesChartType.Stock",
			"SeriesChartType.Candlestick",
			"SeriesChartType.Range",
			"SeriesChartType.SplineRange",
			"SeriesChartType.RangeBar",
			"SeriesChartType.RangeColumn",
			"SeriesChartType.Radar",
			"SeriesChartType.Polar",
			"SeriesChartType.ErrorBar",
			"SeriesChartType.BoxPlot",
			"SeriesChartType.Renko",
			"SeriesChartType.ThreeLineBreak",
			"SeriesChartType.Kagi",
			"SeriesChartType.PointAndFigure",
			"SeriesChartType.Funnel",
			"SeriesChartType.Pyramid"});
			this.comboBox1.Location = new System.Drawing.Point(577, 51);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(185, 21);
			this.comboBox1.TabIndex = 8;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(577, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 39);
			this.button1.TabIndex = 7;
			this.button1.Text = "Load data..";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(327, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 18);
			this.label3.TabIndex = 6;
			this.label3.Text = "До:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(25, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 19);
			this.label2.TabIndex = 5;
			this.label2.Text = "От:";
			// 
			// trackBar1
			// 
			this.trackBar1.LargeChange = 2;
			this.trackBar1.Location = new System.Drawing.Point(54, 38);
			this.trackBar1.Maximum = 62;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(505, 45);
			this.trackBar1.TabIndex = 2;
			this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1Scroll);
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(359, 12);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
			this.dateTimePicker2.TabIndex = 1;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(54, 12);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
			this.dateTimePicker1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(769, 295);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(761, 269);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.myChart);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
			this.splitContainer1.Size = new System.Drawing.Size(755, 263);
			this.splitContainer1.SplitterDistance = 532;
			this.splitContainer1.TabIndex = 2;
			// 
			// myChart
			// 
			chartArea1.Name = "ChartArea1";
			this.myChart.ChartAreas.Add(chartArea1);
			this.myChart.Dock = System.Windows.Forms.DockStyle.Fill;
			legend1.Name = "Legend1";
			this.myChart.Legends.Add(legend1);
			this.myChart.Location = new System.Drawing.Point(0, 0);
			this.myChart.Name = "myChart";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.myChart.Series.Add(series1);
			this.myChart.Size = new System.Drawing.Size(532, 263);
			this.myChart.TabIndex = 0;
			this.myChart.Text = "chart1";
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(219, 263);
			this.dataGridView1.TabIndex = 1;
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1,
			this.toolStripStatusLabel2,
			this.toolStripStatusLabel3});
			this.statusStrip1.Location = new System.Drawing.Point(0, 365);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(769, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(27, 17);
			this.toolStripStatusLabel1.Text = "----";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(35, 17);
			this.toolStripStatusLabel2.Text = " -----";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(45, 17);
			this.toolStripStatusLabel3.Text = " -------";
			// 
			// FormGr
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(769, 387);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainer2);
			this.Name = "FormGr";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Graphics";
			this.Load += new System.EventHandler(this.FormGrLoad);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.myChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
