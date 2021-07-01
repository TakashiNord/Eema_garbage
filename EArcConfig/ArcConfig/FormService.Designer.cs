/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 16.04.2021
 * Time: 18:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormService
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckedListBox checkedListBoxDpload;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox BoxPeriod;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckedListBox checkedListBoxTech;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkedListBoxDpload = new System.Windows.Forms.CheckedListBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.BoxPeriod = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.checkedListBoxTech = new System.Windows.Forms.CheckedListBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.checkedListBoxDpload);
			this.groupBox2.Location = new System.Drawing.Point(4, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(427, 152);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Загрузка в БД (dpload)";
			// 
			// checkedListBoxDpload
			// 
			this.checkedListBoxDpload.FormattingEnabled = true;
			this.checkedListBoxDpload.Location = new System.Drawing.Point(6, 19);
			this.checkedListBoxDpload.Name = "checkedListBoxDpload";
			this.checkedListBoxDpload.Size = new System.Drawing.Size(414, 109);
			this.checkedListBoxDpload.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.BoxPeriod);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.checkedListBoxTech);
			this.groupBox3.Location = new System.Drawing.Point(4, 160);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(427, 193);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Технологический сервер -- ( если несколько - выбирать 1 с протоколом TCP )";
			// 
			// BoxPeriod
			// 
			this.BoxPeriod.FormattingEnabled = true;
			this.BoxPeriod.Items.AddRange(new object[] {
			"3660",
			"43200",
			"86400"});
			this.BoxPeriod.Location = new System.Drawing.Point(342, 134);
			this.BoxPeriod.Name = "BoxPeriod";
			this.BoxPeriod.Size = new System.Drawing.Size(78, 21);
			this.BoxPeriod.TabIndex = 4;
			// 
			// label8
			// 
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label8.Location = new System.Drawing.Point(5, 131);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(223, 53);
			this.label8.TabIndex = 3;
			this.label8.Text = "! Для серверов Сбора\\Передачи - список Техн серверов - не должен быть пустой. Ина" +
	"че, необходимо исправить описание серверов в Навигаторе БД";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(269, 131);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(66, 28);
			this.label7.TabIndex = 2;
			this.label7.Text = "Период для кэша (сек):";
			// 
			// checkedListBoxTech
			// 
			this.checkedListBoxTech.FormattingEnabled = true;
			this.checkedListBoxTech.Location = new System.Drawing.Point(5, 19);
			this.checkedListBoxTech.Name = "checkedListBoxTech";
			this.checkedListBoxTech.ScrollAlwaysVisible = true;
			this.checkedListBoxTech.Size = new System.Drawing.Size(414, 109);
			this.checkedListBoxTech.TabIndex = 0;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(356, 359);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 16;
			this.buttonSave.Text = "Применить..";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(275, 359);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 17;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 131);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(410, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Note:";
			// 
			// FormService
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(434, 394);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormService";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Сервисы архива";
			this.Load += new System.EventHandler(this.FormServiceLoad);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
