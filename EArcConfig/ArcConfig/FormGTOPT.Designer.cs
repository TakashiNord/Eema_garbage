/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 18.08.2022
 * Time: 11:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormGTOPT
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxNAME;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxALIAS;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxID_GTYPE;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBoxDEFINE_ALIAS;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxINTERVAL;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboBoxID_ATYPE;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		
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
			this.dataSet1 = new System.Data.DataSet();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.comboBoxID_ATYPE = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxINTERVAL = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboBoxDEFINE_ALIAS = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxID_GTYPE = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxALIAS = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxNAME = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
			this.splitContainer1.Size = new System.Drawing.Size(1261, 528);
			this.splitContainer1.SplitterDistance = 145;
			this.splitContainer1.TabIndex = 20;
			// 
			// panel1
			// 
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Controls.Add(this.buttonAdd);
			this.panel1.Controls.Add(this.comboBoxID_ATYPE);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.textBoxINTERVAL);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.comboBoxDEFINE_ALIAS);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.comboBoxID_GTYPE);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.textBoxALIAS);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBoxNAME);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.textBoxID);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1261, 145);
			this.panel1.TabIndex = 20;
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(179, 4);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(138, 23);
			this.checkBox1.TabIndex = 34;
			this.checkBox1.Text = "Отключение триггера";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(1174, 6);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 33);
			this.buttonClose.TabIndex = 31;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonAdd.Location = new System.Drawing.Point(1174, 109);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 33);
			this.buttonAdd.TabIndex = 30;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
			// 
			// comboBoxID_ATYPE
			// 
			this.comboBoxID_ATYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxID_ATYPE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxID_ATYPE.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.comboBoxID_ATYPE.FormattingEnabled = true;
			this.comboBoxID_ATYPE.Location = new System.Drawing.Point(689, 63);
			this.comboBoxID_ATYPE.Name = "comboBoxID_ATYPE";
			this.comboBoxID_ATYPE.Size = new System.Drawing.Size(221, 21);
			this.comboBoxID_ATYPE.TabIndex = 29;
			this.comboBoxID_ATYPE.SelectedIndexChanged += new System.EventHandler(this.ComboBoxID_ATYPESelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(548, 65);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(135, 18);
			this.label7.TabIndex = 28;
			this.label7.Text = "Типы архивных данных";
			// 
			// textBoxINTERVAL
			// 
			this.textBoxINTERVAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxINTERVAL.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.textBoxINTERVAL.Location = new System.Drawing.Point(127, 117);
			this.textBoxINTERVAL.Name = "textBoxINTERVAL";
			this.textBoxINTERVAL.Size = new System.Drawing.Size(100, 20);
			this.textBoxINTERVAL.TabIndex = 27;
			this.textBoxINTERVAL.Text = "0";
			this.textBoxINTERVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 97);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(235, 18);
			this.label6.TabIndex = 26;
			this.label6.Text = "Интервал получения значений параметра";
			// 
			// comboBoxDEFINE_ALIAS
			// 
			this.comboBoxDEFINE_ALIAS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxDEFINE_ALIAS.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.comboBoxDEFINE_ALIAS.FormattingEnabled = true;
			this.comboBoxDEFINE_ALIAS.Location = new System.Drawing.Point(365, 116);
			this.comboBoxDEFINE_ALIAS.Name = "comboBoxDEFINE_ALIAS";
			this.comboBoxDEFINE_ALIAS.Size = new System.Drawing.Size(347, 21);
			this.comboBoxDEFINE_ALIAS.TabIndex = 25;
			this.comboBoxDEFINE_ALIAS.Text = "GLT_";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(316, 96);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(228, 19);
			this.label5.TabIndex = 24;
			this.label5.Text = "Уникальный символьный идентификатор";
			// 
			// comboBoxID_GTYPE
			// 
			this.comboBoxID_GTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxID_GTYPE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxID_GTYPE.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.comboBoxID_GTYPE.FormattingEnabled = true;
			this.comboBoxID_GTYPE.Location = new System.Drawing.Point(207, 63);
			this.comboBoxID_GTYPE.Name = "comboBoxID_GTYPE";
			this.comboBoxID_GTYPE.Size = new System.Drawing.Size(301, 21);
			this.comboBoxID_GTYPE.TabIndex = 23;
			this.comboBoxID_GTYPE.SelectedIndexChanged += new System.EventHandler(this.ComboBoxID_GTYPESelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(189, 19);
			this.label4.TabIndex = 22;
			this.label4.Text = "Глобальный тип данных параметра";
			// 
			// textBoxALIAS
			// 
			this.textBoxALIAS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxALIAS.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.textBoxALIAS.Location = new System.Drawing.Point(489, 32);
			this.textBoxALIAS.Name = "textBoxALIAS";
			this.textBoxALIAS.Size = new System.Drawing.Size(237, 20);
			this.textBoxALIAS.TabIndex = 21;
			this.textBoxALIAS.Text = "<Имя краткое>";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(409, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 20);
			this.label3.TabIndex = 20;
			this.label3.Text = "Краткое имя";
			// 
			// textBoxNAME
			// 
			this.textBoxNAME.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxNAME.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.textBoxNAME.Location = new System.Drawing.Point(104, 33);
			this.textBoxNAME.Name = "textBoxNAME";
			this.textBoxNAME.Size = new System.Drawing.Size(293, 20);
			this.textBoxNAME.TabIndex = 19;
			this.textBoxNAME.Text = "<Наименование>";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Наименование";
			// 
			// textBoxID
			// 
			this.textBoxID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxID.ForeColor = System.Drawing.Color.DarkRed;
			this.textBoxID.Location = new System.Drawing.Point(46, 6);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.Size = new System.Drawing.Size(121, 20);
			this.textBoxID.TabIndex = 17;
			this.textBoxID.Text = "00";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 17);
			this.label1.TabIndex = 16;
			this.label1.Text = "id =";
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(1261, 379);
			this.dataGridView1.TabIndex = 17;
			// 
			// FormGTOPT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1261, 528);
			this.Controls.Add(this.splitContainer1);
			this.Name = "FormGTOPT";
			this.Text = "Виды характеристик параметров поддерживаемых системой";
			this.Load += new System.EventHandler(this.FormGTOPTLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
