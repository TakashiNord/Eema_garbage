/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 07.06.2021
 * Time: 8:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ScanIP
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.RichTextBox richTextOut;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxMask;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxIP;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxAcsrvd;
		private System.Windows.Forms.CheckBox checkBoxSysmon;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button buttonCalc;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxEndIP;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.maskedTextBoxEndIP = new System.Windows.Forms.MaskedTextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.buttonCalc = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.maskedTextBoxMask = new System.Windows.Forms.MaskedTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.maskedTextBoxIP = new System.Windows.Forms.MaskedTextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.checkBoxAcsrvd = new System.Windows.Forms.CheckBox();
			this.checkBoxSysmon = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.richTextOut = new System.Windows.Forms.RichTextBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1,
			this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 467);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(337, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(147, 17);
			this.toolStripStatusLabel1.Text = "----------------------------";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(12, 17);
			this.toolStripStatusLabel2.Text = "/";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label9);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel1.Controls.Add(this.button5);
			this.splitContainer1.Panel1.Controls.Add(this.checkBoxAcsrvd);
			this.splitContainer1.Panel1.Controls.Add(this.checkBoxSysmon);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.richTextOut);
			this.splitContainer1.Size = new System.Drawing.Size(337, 467);
			this.splitContainer1.SplitterDistance = 304;
			this.splitContainer1.TabIndex = 2;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(237, 162);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 29);
			this.label9.TabIndex = 14;
			this.label9.Text = "Проверять порты :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.maskedTextBoxEndIP);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.buttonCalc);
			this.groupBox2.Controls.Add(this.button4);
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.maskedTextBoxMask);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.maskedTextBoxIP);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(329, 149);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Запрос";
			// 
			// maskedTextBoxEndIP
			// 
			this.maskedTextBoxEndIP.Location = new System.Drawing.Point(108, 122);
			this.maskedTextBoxEndIP.Name = "maskedTextBoxEndIP";
			this.maskedTextBoxEndIP.Size = new System.Drawing.Size(119, 20);
			this.maskedTextBoxEndIP.TabIndex = 21;
			// 
			// label10
			// 
			this.label10.ForeColor = System.Drawing.Color.Red;
			this.label10.Location = new System.Drawing.Point(9, 122);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(96, 14);
			this.label10.TabIndex = 20;
			this.label10.Text = "Конечный адрес";
			// 
			// buttonCalc
			// 
			this.buttonCalc.Location = new System.Drawing.Point(248, 99);
			this.buttonCalc.Name = "buttonCalc";
			this.buttonCalc.Size = new System.Drawing.Size(75, 43);
			this.buttonCalc.TabIndex = 19;
			this.buttonCalc.Text = "Рассчитать";
			this.buttonCalc.UseVisualStyleBackColor = true;
			this.buttonCalc.Click += new System.EventHandler(this.ButtonCalcClick);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(168, 96);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(20, 20);
			this.button4.TabIndex = 17;
			this.button4.Text = "i";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(148, 96);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(20, 20);
			this.button3.TabIndex = 16;
			this.button3.Text = "C";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(128, 96);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(20, 20);
			this.button2.TabIndex = 15;
			this.button2.Text = "B";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(108, 96);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(20, 20);
			this.button1.TabIndex = 14;
			this.button1.Text = "A";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// maskedTextBoxMask
			// 
			this.maskedTextBoxMask.Location = new System.Drawing.Point(108, 79);
			this.maskedTextBoxMask.Name = "maskedTextBoxMask";
			this.maskedTextBoxMask.Size = new System.Drawing.Size(102, 20);
			this.maskedTextBoxMask.TabIndex = 12;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(54, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 14);
			this.label2.TabIndex = 11;
			this.label2.Text = "Маска";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(2, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 14);
			this.label1.TabIndex = 10;
			this.label1.Text = "Начальный адрес";
			// 
			// maskedTextBoxIP
			// 
			this.maskedTextBoxIP.Location = new System.Drawing.Point(108, 54);
			this.maskedTextBoxIP.Name = "maskedTextBoxIP";
			this.maskedTextBoxIP.Size = new System.Drawing.Size(119, 20);
			this.maskedTextBoxIP.TabIndex = 9;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.Location = new System.Drawing.Point(7, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(316, 35);
			this.label8.TabIndex = 0;
			this.label8.Text = "Задайте начальный и конечный адрес, маску подсети, нажмите Рассчитать. После - На" +
	"йти.";
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(254, 263);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 36);
			this.button5.TabIndex = 12;
			this.button5.Text = "Start";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// checkBoxAcsrvd
			// 
			this.checkBoxAcsrvd.Location = new System.Drawing.Point(237, 217);
			this.checkBoxAcsrvd.Name = "checkBoxAcsrvd";
			this.checkBoxAcsrvd.Size = new System.Drawing.Size(92, 22);
			this.checkBoxAcsrvd.TabIndex = 11;
			this.checkBoxAcsrvd.Text = "2005 (acsrvd)";
			this.checkBoxAcsrvd.UseVisualStyleBackColor = true;
			// 
			// checkBoxSysmon
			// 
			this.checkBoxSysmon.Location = new System.Drawing.Point(237, 194);
			this.checkBoxSysmon.Name = "checkBoxSysmon";
			this.checkBoxSysmon.Size = new System.Drawing.Size(94, 20);
			this.checkBoxSysmon.TabIndex = 10;
			this.checkBoxSysmon.Text = "2003 (sysmon)";
			this.checkBoxSysmon.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox5);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textBox4);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBox3);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(5, 158);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(226, 132);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Расчет ";
			// 
			// textBox5
			// 
			this.textBox5.Enabled = false;
			this.textBox5.Location = new System.Drawing.Point(88, 105);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(117, 20);
			this.textBox5.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6, 108);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 14);
			this.label7.TabIndex = 8;
			this.label7.Text = "Число хостов";
			// 
			// textBox4
			// 
			this.textBox4.Enabled = false;
			this.textBox4.Location = new System.Drawing.Point(88, 82);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(117, 20);
			this.textBox4.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(22, 85);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 14);
			this.label6.TabIndex = 6;
			this.label6.Text = "Broadcast";
			// 
			// textBox3
			// 
			this.textBox3.Enabled = false;
			this.textBox3.Location = new System.Drawing.Point(88, 59);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(117, 20);
			this.textBox3.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(32, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(45, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "max IP";
			// 
			// textBox2
			// 
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(88, 36);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(117, 20);
			this.textBox2.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(37, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(36, 14);
			this.label4.TabIndex = 2;
			this.label4.Text = "min IP";
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(88, 13);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(117, 20);
			this.textBox1.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(42, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 14);
			this.label3.TabIndex = 0;
			this.label3.Text = "Сеть";
			// 
			// richTextOut
			// 
			this.richTextOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextOut.Location = new System.Drawing.Point(0, 0);
			this.richTextOut.Name = "richTextOut";
			this.richTextOut.Size = new System.Drawing.Size(337, 159);
			this.richTextOut.TabIndex = 0;
			this.richTextOut.Text = "";
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1RunWorkerCompleted);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(337, 489);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "ScanIP";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
