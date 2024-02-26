/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 19.02.2024
 * Time: 19:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormArc_db_schema_lite
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ComboBox comboBoxSvc;
		private System.Windows.Forms.ComboBox comboBoxDB;
		private System.Windows.Forms.Button butBind;
		private System.Windows.Forms.Button butCancel;
		
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
			this.comboBoxSvc = new System.Windows.Forms.ComboBox();
			this.comboBoxDB = new System.Windows.Forms.ComboBox();
			this.butBind = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBoxSvc
			// 
			this.comboBoxSvc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSvc.FormattingEnabled = true;
			this.comboBoxSvc.Location = new System.Drawing.Point(8, 2);
			this.comboBoxSvc.Name = "comboBoxSvc";
			this.comboBoxSvc.Size = new System.Drawing.Size(559, 21);
			this.comboBoxSvc.TabIndex = 0;
			// 
			// comboBoxDB
			// 
			this.comboBoxDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDB.FormattingEnabled = true;
			this.comboBoxDB.Location = new System.Drawing.Point(9, 43);
			this.comboBoxDB.Name = "comboBoxDB";
			this.comboBoxDB.Size = new System.Drawing.Size(558, 21);
			this.comboBoxDB.TabIndex = 1;
			// 
			// butBind
			// 
			this.butBind.Location = new System.Drawing.Point(247, 79);
			this.butBind.Name = "butBind";
			this.butBind.Size = new System.Drawing.Size(75, 23);
			this.butBind.TabIndex = 2;
			this.butBind.Text = "Связать";
			this.butBind.UseVisualStyleBackColor = true;
			this.butBind.Click += new System.EventHandler(this.Button1Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(492, 79);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 23);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Отмена";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.ButCancelClick);
			// 
			// FormArc_db_schema_lite
			// 
			this.AcceptButton = this.butBind;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(571, 105);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butBind);
			this.Controls.Add(this.comboBoxDB);
			this.Controls.Add(this.comboBoxSvc);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormArc_db_schema_lite";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = ":";
			this.Load += new System.EventHandler(this.FormArc_db_schema_liteLoad);
			this.ResumeLayout(false);

		}
	}
}
