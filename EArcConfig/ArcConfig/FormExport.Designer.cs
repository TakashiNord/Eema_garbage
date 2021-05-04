/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 28.04.2021
 * Time: 11:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormExport
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ComboBox comboBoxFormat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.TextBox textBoxFile;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.CheckBox checkBoxExportSel;
		private System.Windows.Forms.CheckBox checkBoxIncludeSch;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxColon;
		private System.Windows.Forms.CheckBox checkBoxDelimiterSE;
		private System.Windows.Forms.GroupBox groupBoxTime;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxUseTime;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxOnly500;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
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
			this.comboBoxFormat = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.textBoxFile = new System.Windows.Forms.TextBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.checkBoxExportSel = new System.Windows.Forms.CheckBox();
			this.checkBoxIncludeSch = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxDelimiterSE = new System.Windows.Forms.CheckBox();
			this.comboBoxColon = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBoxTime = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.checkBoxUseTime = new System.Windows.Forms.CheckBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBoxOnly500 = new System.Windows.Forms.CheckBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBoxTime.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxFormat
			// 
			this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFormat.FormattingEnabled = true;
			this.comboBoxFormat.Items.AddRange(new object[] {
			"Delimited Text",
			"HTML Table",
			"Insert Statements",
			"Merge Statements"});
			this.comboBoxFormat.Location = new System.Drawing.Point(97, 6);
			this.comboBoxFormat.Name = "comboBoxFormat";
			this.comboBoxFormat.Size = new System.Drawing.Size(131, 21);
			this.comboBoxFormat.TabIndex = 0;
			this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFormatSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "Export format";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonOpen);
			this.groupBox1.Controls.Add(this.textBoxFile);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new System.Drawing.Point(6, 33);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(274, 82);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Output";
			// 
			// buttonOpen
			// 
			this.buttonOpen.Location = new System.Drawing.Point(247, 21);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(21, 23);
			this.buttonOpen.TabIndex = 2;
			this.buttonOpen.Text = "...";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.ButtonOpenClick);
			// 
			// textBoxFile
			// 
			this.textBoxFile.Location = new System.Drawing.Point(58, 23);
			this.textBoxFile.Name = "textBoxFile";
			this.textBoxFile.Size = new System.Drawing.Size(190, 20);
			this.textBoxFile.TabIndex = 1;
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(15, 19);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(52, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "File";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// checkBoxExportSel
			// 
			this.checkBoxExportSel.Location = new System.Drawing.Point(12, 121);
			this.checkBoxExportSel.Name = "checkBoxExportSel";
			this.checkBoxExportSel.Size = new System.Drawing.Size(139, 24);
			this.checkBoxExportSel.TabIndex = 3;
			this.checkBoxExportSel.Text = "Export selected rows";
			this.checkBoxExportSel.UseVisualStyleBackColor = true;
			// 
			// checkBoxIncludeSch
			// 
			this.checkBoxIncludeSch.Location = new System.Drawing.Point(12, 151);
			this.checkBoxIncludeSch.Name = "checkBoxIncludeSch";
			this.checkBoxIncludeSch.Size = new System.Drawing.Size(139, 25);
			this.checkBoxIncludeSch.TabIndex = 4;
			this.checkBoxIncludeSch.Text = "Include schema name";
			this.checkBoxIncludeSch.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxDelimiterSE);
			this.groupBox2.Controls.Add(this.comboBoxColon);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(6, 182);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(274, 68);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Delimiter";
			// 
			// checkBoxDelimiterSE
			// 
			this.checkBoxDelimiterSE.Location = new System.Drawing.Point(15, 37);
			this.checkBoxDelimiterSE.Name = "checkBoxDelimiterSE";
			this.checkBoxDelimiterSE.Size = new System.Drawing.Size(181, 25);
			this.checkBoxDelimiterSE.TabIndex = 2;
			this.checkBoxDelimiterSE.Text = "Include delimiter in start and end ";
			this.checkBoxDelimiterSE.UseVisualStyleBackColor = true;
			// 
			// comboBoxColon
			// 
			this.comboBoxColon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxColon.FormattingEnabled = true;
			this.comboBoxColon.Items.AddRange(new object[] {
			";",
			","});
			this.comboBoxColon.Location = new System.Drawing.Point(70, 13);
			this.comboBoxColon.Name = "comboBoxColon";
			this.comboBoxColon.Size = new System.Drawing.Size(34, 21);
			this.comboBoxColon.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Character";
			// 
			// groupBoxTime
			// 
			this.groupBoxTime.Controls.Add(this.label4);
			this.groupBoxTime.Controls.Add(this.label3);
			this.groupBoxTime.Controls.Add(this.dateTimePicker2);
			this.groupBoxTime.Controls.Add(this.dateTimePicker1);
			this.groupBoxTime.Location = new System.Drawing.Point(6, 273);
			this.groupBoxTime.Name = "groupBoxTime";
			this.groupBoxTime.Size = new System.Drawing.Size(274, 75);
			this.groupBoxTime.TabIndex = 7;
			this.groupBoxTime.TabStop = false;
			this.groupBoxTime.Text = "Time";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(15, 45);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 15);
			this.label4.TabIndex = 4;
			this.label4.Text = "До:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(15, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 18);
			this.label3.TabIndex = 3;
			this.label3.Text = "От:";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(49, 45);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(147, 20);
			this.dateTimePicker2.TabIndex = 2;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(49, 19);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(147, 20);
			this.dateTimePicker1.TabIndex = 1;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(205, 354);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(114, 354);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 9;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// checkBoxUseTime
			// 
			this.checkBoxUseTime.Location = new System.Drawing.Point(12, 250);
			this.checkBoxUseTime.Name = "checkBoxUseTime";
			this.checkBoxUseTime.Size = new System.Drawing.Size(78, 25);
			this.checkBoxUseTime.TabIndex = 10;
			this.checkBoxUseTime.Text = "Use Time";
			this.checkBoxUseTime.UseVisualStyleBackColor = true;
			this.checkBoxUseTime.CheckedChanged += new System.EventHandler(this.CheckBoxUseTimeCheckedChanged);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(220, 124);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(60, 20);
			this.numericUpDown1.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(162, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 14);
			this.label5.TabIndex = 12;
			this.label5.Text = "Commit";
			// 
			// checkBoxOnly500
			// 
			this.checkBoxOnly500.Location = new System.Drawing.Point(157, 151);
			this.checkBoxOnly500.Name = "checkBoxOnly500";
			this.checkBoxOnly500.Size = new System.Drawing.Size(123, 24);
			this.checkBoxOnly500.TabIndex = 13;
			this.checkBoxOnly500.Text = "Only 500 last values";
			this.checkBoxOnly500.UseVisualStyleBackColor = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1,
			this.toolStripProgressBar1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 383);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(287, 22);
			this.statusStrip1.TabIndex = 14;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(40, 17);
			this.toolStripStatusLabel1.Text = "...........";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			// 
			// FormExport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(287, 405);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.checkBoxOnly500);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.checkBoxUseTime);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.groupBoxTime);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.checkBoxIncludeSch);
			this.Controls.Add(this.checkBoxExportSel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxFormat);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormExport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Save dataset as ..";
			this.Load += new System.EventHandler(this.FormExportLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBoxTime.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
