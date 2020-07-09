/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 24.03.2016
 * Time: 13:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcWrite
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.MonthCalendar monthCalendar1;
		private System.Windows.Forms.MonthCalendar monthCalendar2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
			this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.listView1 = new System.Windows.Forms.ListView();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button7 = new System.Windows.Forms.Button();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1,
			this.toolStripStatusLabel3,
			this.toolStripStatusLabel4,
			this.toolStripProgressBar1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 532);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(724, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(85, 17);
			this.toolStripStatusLabel1.Text = "..........................";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(118, 17);
			this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.IsLink = true;
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(74, 17);
			this.toolStripStatusLabel4.Text = "www.ema.ru";
			this.toolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.richTextBox1.Location = new System.Drawing.Point(0, 366);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(724, 166);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Location = new System.Drawing.Point(50, 4);
			this.monthCalendar1.MaxSelectionCount = 1;
			this.monthCalendar1.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 3;
			this.monthCalendar1.TodayDate = new System.DateTime(2016, 5, 6, 0, 0, 0, 0);
			// 
			// monthCalendar2
			// 
			this.monthCalendar2.Location = new System.Drawing.Point(353, 4);
			this.monthCalendar2.MaxSelectionCount = 1;
			this.monthCalendar2.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			this.monthCalendar2.Name = "monthCalendar2";
			this.monthCalendar2.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(37, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "+";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(2, 189);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(40, 23);
			this.button2.TabIndex = 10;
			this.button2.Text = "Start";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(2, 234);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(40, 23);
			this.button3.TabIndex = 11;
			this.button3.Text = "Stop";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(2, 273);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(40, 23);
			this.button4.TabIndex = 12;
			this.button4.Text = "Clear";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Multiselect = true;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 23);
			this.label3.TabIndex = 13;
			this.label3.Text = "From:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(311, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(21, 23);
			this.label4.TabIndex = 14;
			this.label4.Text = "To";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Checked = false;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker1.Location = new System.Drawing.Point(92, 178);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new System.Drawing.Size(72, 20);
			this.dateTimePicker1.TabIndex = 15;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker2.Location = new System.Drawing.Point(397, 178);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.ShowUpDown = true;
			this.dateTimePicker2.Size = new System.Drawing.Size(77, 20);
			this.dateTimePicker2.TabIndex = 16;
			// 
			// listView1
			// 
			this.listView1.CheckBoxes = true;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(724, 366);
			this.listView1.TabIndex = 17;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView1DragDrop);
			this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListView1DragEnter);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(560, 6);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(85, 23);
			this.button5.TabIndex = 18;
			this.button5.Text = "Find";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(3, 41);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(37, 23);
			this.button6.TabIndex = 19;
			this.button6.Text = "-";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.Button6Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(560, 109);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(86, 24);
			this.checkBox1.TabIndex = 20;
			this.checkBox1.Text = "Output file";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.listView1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(724, 366);
			this.panel1.TabIndex = 21;
			// 
			// panel2
			// 
			this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.button1);
			this.panel2.Controls.Add(this.button6);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.button4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(677, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(47, 366);
			this.panel2.TabIndex = 22;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.button7);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.monthCalendar1);
			this.panel3.Controls.Add(this.checkBox1);
			this.panel3.Controls.Add(this.dateTimePicker1);
			this.panel3.Controls.Add(this.button5);
			this.panel3.Controls.Add(this.dateTimePicker2);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.monthCalendar2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 155);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(677, 211);
			this.panel3.TabIndex = 23;
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label2.Location = new System.Drawing.Point(538, 181);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 17);
			this.label2.TabIndex = 23;
			this.label2.Text = "mailto:support@ema.ru";
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label1.Location = new System.Drawing.Point(538, 166);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 15);
			this.label1.TabIndex = 22;
			this.label1.Text = "http://www.ema.ru";
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(560, 44);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(85, 23);
			this.button7.TabIndex = 21;
			this.button7.Text = "set [start;end]";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.Button7Click);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1RunWorkerCompleted);
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 554);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "MainForm";
			this.Text = "ArcWrite";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
