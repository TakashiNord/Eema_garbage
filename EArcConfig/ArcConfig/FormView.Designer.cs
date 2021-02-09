/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 18.01.2019
 * Time: 14:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormView
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView dataGridViewV;
		
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
			this.dataGridViewV = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewV)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewV
			// 
			this.dataGridViewV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewV.Location = new System.Drawing.Point(30, 12);
			this.dataGridViewV.Name = "dataGridViewV";
			this.dataGridViewV.Size = new System.Drawing.Size(929, 608);
			this.dataGridViewV.TabIndex = 0;
			// 
			// FormView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(971, 668);
			this.Controls.Add(this.dataGridViewV);
			this.Name = "FormView";
			this.Text = "FormView";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewV)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
