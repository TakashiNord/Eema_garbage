/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 01.03.2024
 * Time: 12:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormArc_db_schema_ch
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOk;
		
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOk = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.butCancel);
			this.splitContainer1.Panel2.Controls.Add(this.butOk);
			this.splitContainer1.Size = new System.Drawing.Size(763, 584);
			this.splitContainer1.SplitterDistance = 521;
			this.splitContainer1.TabIndex = 2;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(763, 521);
			this.dataGridView1.TabIndex = 1;
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(654, 13);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(97, 34);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Отмена";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.ButCancelClick);
			// 
			// butOk
			// 
			this.butOk.Location = new System.Drawing.Point(295, 13);
			this.butOk.Name = "butOk";
			this.butOk.Size = new System.Drawing.Size(162, 34);
			this.butOk.TabIndex = 0;
			this.butOk.Text = "Сохранить изменения";
			this.butOk.UseVisualStyleBackColor = true;
			this.butOk.Click += new System.EventHandler(this.ButOkClick);
			// 
			// FormArc_db_schema_ch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(763, 584);
			this.Controls.Add(this.splitContainer1);
			this.Name = "FormArc_db_schema_ch";
			this.Text = "FormArc_db_schema_ch";
			this.Load += new System.EventHandler(this.FormArc_db_schema_chLoad);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
