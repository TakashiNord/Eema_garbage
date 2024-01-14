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
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		
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
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkedListBoxDpload = new System.Windows.Forms.CheckedListBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.BoxPeriod = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.checkedListBoxTech = new System.Windows.Forms.CheckedListBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.numericUpDown1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.checkedListBoxDpload);
			this.groupBox2.Location = new System.Drawing.Point(4, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(427, 152);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Загрузка в БД (dpload)";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.numericUpDown1.Location = new System.Drawing.Point(8, 36);
			this.numericUpDown1.Maximum = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(57, 20);
			this.numericUpDown1.TabIndex = 4;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1ValueChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Приоритеты:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 131);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(410, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Note:";
			// 
			// checkedListBoxDpload
			// 
			this.checkedListBoxDpload.FormattingEnabled = true;
			this.checkedListBoxDpload.Location = new System.Drawing.Point(71, 19);
			this.checkedListBoxDpload.Name = "checkedListBoxDpload";
			this.checkedListBoxDpload.Size = new System.Drawing.Size(349, 109);
			this.checkedListBoxDpload.TabIndex = 0;
			this.checkedListBoxDpload.SelectedIndexChanged += new System.EventHandler(this.CheckedListBoxDploadSelectedIndexChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.BoxPeriod);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.checkedListBoxTech);
			this.groupBox3.Location = new System.Drawing.Point(4, 160);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(427, 257);
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
			this.BoxPeriod.Location = new System.Drawing.Point(340, 201);
			this.BoxPeriod.Name = "BoxPeriod";
			this.BoxPeriod.Size = new System.Drawing.Size(78, 21);
			this.BoxPeriod.TabIndex = 4;
			this.BoxPeriod.SelectedIndexChanged += new System.EventHandler(this.BoxPeriodSelectedIndexChanged);
			this.BoxPeriod.TextChanged += new System.EventHandler(this.BoxPeriodTextChanged);
			// 
			// label8
			// 
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label8.Location = new System.Drawing.Point(5, 201);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(223, 53);
			this.label8.TabIndex = 3;
			this.label8.Text = "! Для серверов Сбора\\Передачи - список Техн серверов - не должен быть пустой. Ина" +
	"че, необходимо исправить описание серверов в Навигаторе БД";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(269, 201);
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
			this.checkedListBoxTech.Size = new System.Drawing.Size(414, 169);
			this.checkedListBoxTech.TabIndex = 0;
			this.checkedListBoxTech.SelectedIndexChanged += new System.EventHandler(this.CheckedListBoxTechSelectedIndexChanged);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(356, 423);
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
			this.buttonCancel.Location = new System.Drawing.Point(273, 423);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 17;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// FormService
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(434, 458);
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
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
