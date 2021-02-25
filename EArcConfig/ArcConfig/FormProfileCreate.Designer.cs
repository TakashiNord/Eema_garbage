/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 03.02.2021
 * Time: 10:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormProfileCreate
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxID;
		private System.Windows.Forms.TextBox textBoxID_GINFO;
		private System.Windows.Forms.TextBox textBoxID_TBLLST;
		private System.Windows.Forms.ComboBox comboBoxIS_WRITEON;
		private System.Windows.Forms.ComboBox comboBoxIS_VIEWABLE;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxSTACK_NAME;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckedListBox checkedListBoxDpload;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxPeriod;
		private System.Windows.Forms.CheckedListBox checkedListBoxTech;
		
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxSTACK_NAME = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxIS_VIEWABLE = new System.Windows.Forms.ComboBox();
			this.comboBoxIS_WRITEON = new System.Windows.Forms.ComboBox();
			this.textBoxID_TBLLST = new System.Windows.Forms.TextBox();
			this.textBoxID_GINFO = new System.Windows.Forms.TextBox();
			this.textBoxID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkedListBoxDpload = new System.Windows.Forms.CheckedListBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxPeriod = new System.Windows.Forms.TextBox();
			this.checkedListBoxTech = new System.Windows.Forms.CheckedListBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxSTACK_NAME);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.comboBoxIS_VIEWABLE);
			this.groupBox1.Controls.Add(this.comboBoxIS_WRITEON);
			this.groupBox1.Controls.Add(this.textBoxID_TBLLST);
			this.groupBox1.Controls.Add(this.textBoxID_GINFO);
			this.groupBox1.Controls.Add(this.textBoxID);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(427, 94);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Главное";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(284, 43);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 20);
			this.label6.TabIndex = 23;
			this.label6.Text = "= Посмотр в Retroview";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(284, 17);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 16);
			this.label5.TabIndex = 22;
			this.label5.Text = "= Для всех параметров";
			// 
			// textBoxSTACK_NAME
			// 
			this.textBoxSTACK_NAME.Location = new System.Drawing.Point(160, 65);
			this.textBoxSTACK_NAME.Name = "textBoxSTACK_NAME";
			this.textBoxSTACK_NAME.Size = new System.Drawing.Size(175, 20);
			this.textBoxSTACK_NAME.TabIndex = 21;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(347, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 16);
			this.label4.TabIndex = 20;
			this.label4.Text = "= Таблица";
			// 
			// comboBoxIS_VIEWABLE
			// 
			this.comboBoxIS_VIEWABLE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxIS_VIEWABLE.FormattingEnabled = true;
			this.comboBoxIS_VIEWABLE.Items.AddRange(new object[] {
			"Нет",
			"Да"});
			this.comboBoxIS_VIEWABLE.Location = new System.Drawing.Point(160, 40);
			this.comboBoxIS_VIEWABLE.Name = "comboBoxIS_VIEWABLE";
			this.comboBoxIS_VIEWABLE.Size = new System.Drawing.Size(118, 21);
			this.comboBoxIS_VIEWABLE.TabIndex = 19;
			// 
			// comboBoxIS_WRITEON
			// 
			this.comboBoxIS_WRITEON.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxIS_WRITEON.FormattingEnabled = true;
			this.comboBoxIS_WRITEON.Items.AddRange(new object[] {
			"Нет",
			"Да"});
			this.comboBoxIS_WRITEON.Location = new System.Drawing.Point(160, 13);
			this.comboBoxIS_WRITEON.Name = "comboBoxIS_WRITEON";
			this.comboBoxIS_WRITEON.Size = new System.Drawing.Size(118, 21);
			this.comboBoxIS_WRITEON.TabIndex = 18;
			// 
			// textBoxID_TBLLST
			// 
			this.textBoxID_TBLLST.Location = new System.Drawing.Point(82, 66);
			this.textBoxID_TBLLST.Name = "textBoxID_TBLLST";
			this.textBoxID_TBLLST.ReadOnly = true;
			this.textBoxID_TBLLST.Size = new System.Drawing.Size(72, 20);
			this.textBoxID_TBLLST.TabIndex = 17;
			// 
			// textBoxID_GINFO
			// 
			this.textBoxID_GINFO.Location = new System.Drawing.Point(82, 40);
			this.textBoxID_GINFO.Name = "textBoxID_GINFO";
			this.textBoxID_GINFO.ReadOnly = true;
			this.textBoxID_GINFO.Size = new System.Drawing.Size(72, 20);
			this.textBoxID_GINFO.TabIndex = 16;
			// 
			// textBoxID
			// 
			this.textBoxID.Location = new System.Drawing.Point(82, 14);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.ReadOnly = true;
			this.textBoxID.Size = new System.Drawing.Size(72, 20);
			this.textBoxID.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 16);
			this.label3.TabIndex = 14;
			this.label3.Text = "Раздел =";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 20);
			this.label2.TabIndex = 13;
			this.label2.Text = "Тип архива =";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(55, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 20);
			this.label1.TabIndex = 12;
			this.label1.Text = "id =";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkedListBoxDpload);
			this.groupBox2.Location = new System.Drawing.Point(3, 103);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(427, 90);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Загрузка в БД (dpload)";
			// 
			// checkedListBoxDpload
			// 
			this.checkedListBoxDpload.FormattingEnabled = true;
			this.checkedListBoxDpload.Location = new System.Drawing.Point(5, 19);
			this.checkedListBoxDpload.Name = "checkedListBoxDpload";
			this.checkedListBoxDpload.Size = new System.Drawing.Size(414, 64);
			this.checkedListBoxDpload.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.textBoxPeriod);
			this.groupBox3.Controls.Add(this.checkedListBoxTech);
			this.groupBox3.Location = new System.Drawing.Point(3, 199);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(427, 98);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Технологический сервер";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(233, 71);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(126, 20);
			this.label7.TabIndex = 2;
			this.label7.Text = "Период для кэша (сек):";
			// 
			// textBoxPeriod
			// 
			this.textBoxPeriod.Location = new System.Drawing.Point(365, 71);
			this.textBoxPeriod.Name = "textBoxPeriod";
			this.textBoxPeriod.Size = new System.Drawing.Size(54, 20);
			this.textBoxPeriod.TabIndex = 1;
			this.textBoxPeriod.Text = "0";
			// 
			// checkedListBoxTech
			// 
			this.checkedListBoxTech.FormattingEnabled = true;
			this.checkedListBoxTech.Location = new System.Drawing.Point(5, 19);
			this.checkedListBoxTech.Name = "checkedListBoxTech";
			this.checkedListBoxTech.ScrollAlwaysVisible = true;
			this.checkedListBoxTech.Size = new System.Drawing.Size(414, 49);
			this.checkedListBoxTech.TabIndex = 0;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(263, 303);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 15;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(359, 303);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(71, 23);
			this.buttonOk.TabIndex = 16;
			this.buttonOk.Text = "Создать";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// FormProfileCreate
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(436, 335);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProfileCreate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Создание профиля архива.";
			this.Load += new System.EventHandler(this.FormProfileCreateLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
