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
		private System.Windows.Forms.ComboBox comboBoxFilter;
		private System.Windows.Forms.Label label8;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.Panel panel1;
		
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.dataSet1 = new System.Data.DataSet();
			this.panel1 = new System.Windows.Forms.Panel();
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
			this.label8 = new System.Windows.Forms.Label();
			this.comboBoxFilter = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dataGridView1.Location = new System.Drawing.Point(0, 294);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(1025, 234);
			this.dataGridView1.TabIndex = 16;
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.comboBoxFilter);
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
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(748, 276);
			this.panel1.TabIndex = 19;
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(138, 223);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 33);
			this.buttonClose.TabIndex = 31;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(38, 223);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 33);
			this.buttonAdd.TabIndex = 30;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			// 
			// comboBoxID_ATYPE
			// 
			this.comboBoxID_ATYPE.FormattingEnabled = true;
			this.comboBoxID_ATYPE.Location = new System.Drawing.Point(162, 192);
			this.comboBoxID_ATYPE.Name = "comboBoxID_ATYPE";
			this.comboBoxID_ATYPE.Size = new System.Drawing.Size(121, 21);
			this.comboBoxID_ATYPE.TabIndex = 29;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(38, 181);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 33);
			this.label7.TabIndex = 28;
			this.label7.Text = "ID_ATYPE";
			// 
			// textBoxINTERVAL
			// 
			this.textBoxINTERVAL.Location = new System.Drawing.Point(162, 165);
			this.textBoxINTERVAL.Name = "textBoxINTERVAL";
			this.textBoxINTERVAL.Size = new System.Drawing.Size(100, 20);
			this.textBoxINTERVAL.TabIndex = 27;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(38, 154);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 33);
			this.label6.TabIndex = 26;
			this.label6.Text = "INTERVAL";
			// 
			// comboBoxDEFINE_ALIAS
			// 
			this.comboBoxDEFINE_ALIAS.FormattingEnabled = true;
			this.comboBoxDEFINE_ALIAS.Location = new System.Drawing.Point(162, 137);
			this.comboBoxDEFINE_ALIAS.Name = "comboBoxDEFINE_ALIAS";
			this.comboBoxDEFINE_ALIAS.Size = new System.Drawing.Size(121, 21);
			this.comboBoxDEFINE_ALIAS.TabIndex = 25;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(38, 127);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 33);
			this.label5.TabIndex = 24;
			this.label5.Text = "DEFINE_ALIAS";
			// 
			// comboBoxID_GTYPE
			// 
			this.comboBoxID_GTYPE.FormattingEnabled = true;
			this.comboBoxID_GTYPE.Location = new System.Drawing.Point(162, 102);
			this.comboBoxID_GTYPE.Name = "comboBoxID_GTYPE";
			this.comboBoxID_GTYPE.Size = new System.Drawing.Size(121, 21);
			this.comboBoxID_GTYPE.TabIndex = 23;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(38, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 33);
			this.label4.TabIndex = 22;
			this.label4.Text = "ID_GTYPE";
			// 
			// textBoxALIAS
			// 
			this.textBoxALIAS.Location = new System.Drawing.Point(162, 75);
			this.textBoxALIAS.Name = "textBoxALIAS";
			this.textBoxALIAS.Size = new System.Drawing.Size(100, 20);
			this.textBoxALIAS.TabIndex = 21;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(38, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 33);
			this.label3.TabIndex = 20;
			this.label3.Text = "ALIAS";
			// 
			// textBoxNAME
			// 
			this.textBoxNAME.Location = new System.Drawing.Point(162, 46);
			this.textBoxNAME.Name = "textBoxNAME";
			this.textBoxNAME.Size = new System.Drawing.Size(100, 20);
			this.textBoxNAME.TabIndex = 19;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(38, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 33);
			this.label2.TabIndex = 18;
			this.label2.Text = "NAME";
			// 
			// textBoxID
			// 
			this.textBoxID.Location = new System.Drawing.Point(162, 19);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.Size = new System.Drawing.Size(100, 20);
			this.textBoxID.TabIndex = 17;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(38, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 33);
			this.label1.TabIndex = 16;
			this.label1.Text = "id =";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(373, 216);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 21);
			this.label8.TabIndex = 33;
			this.label8.Text = "Фильтр";
			// 
			// comboBoxFilter
			// 
			this.comboBoxFilter.FormattingEnabled = true;
			this.comboBoxFilter.Location = new System.Drawing.Point(439, 213);
			this.comboBoxFilter.Name = "comboBoxFilter";
			this.comboBoxFilter.Size = new System.Drawing.Size(259, 21);
			this.comboBoxFilter.TabIndex = 32;
			// 
			// FormGTOPT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1025, 528);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dataGridView1);
			this.Name = "FormGTOPT";
			this.Text = "Виды характеристик параметров поддерживаемых системой";
			this.Load += new System.EventHandler(this.FormGTOPTLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
