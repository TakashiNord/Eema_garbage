/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 27.02.2024
 * Time: 13:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormVariable
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.DataGridView dataGridViewList;
		
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
			this.dataGridViewList = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
			this.SuspendLayout();
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			// 
			// dataGridViewList
			// 
			this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewList.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewList.Name = "dataGridViewList";
			this.dataGridViewList.Size = new System.Drawing.Size(824, 419);
			this.dataGridViewList.TabIndex = 0;
			// 
			// FormVariable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(824, 419);
			this.Controls.Add(this.dataGridViewList);
			this.Name = "FormVariable";
			this.Text = "FormVariable";
			this.Load += new System.EventHandler(this.FormVariableLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
