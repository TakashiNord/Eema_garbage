/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 25.11.2023
 * Time: 16:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormPartitions
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView dataGridViewPart;
		private System.Data.DataSet dataSet1;
		
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
			this.dataGridViewPart = new System.Windows.Forms.DataGridView();
			this.dataSet1 = new System.Data.DataSet();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewPart
			// 
			this.dataGridViewPart.AllowUserToDeleteRows = false;
			this.dataGridViewPart.AllowUserToOrderColumns = true;
			this.dataGridViewPart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewPart.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewPart.Name = "dataGridViewPart";
			this.dataGridViewPart.ReadOnly = true;
			this.dataGridViewPart.Size = new System.Drawing.Size(831, 297);
			this.dataGridViewPart.TabIndex = 0;
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			// 
			// FormPartitions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(831, 297);
			this.Controls.Add(this.dataGridViewPart);
			this.Name = "FormPartitions";
			this.Text = "Partitions";
			this.Load += new System.EventHandler(this.FormPartitionsLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
