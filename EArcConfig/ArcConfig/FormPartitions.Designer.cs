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
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem countToolStripMenuItem;
		
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPartitions));
			this.dataGridViewPart = new System.Windows.Forms.DataGridView();
			this.dataSet1 = new System.Data.DataSet();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.countToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewPart
			// 
			this.dataGridViewPart.AllowUserToDeleteRows = false;
			this.dataGridViewPart.AllowUserToOrderColumns = true;
			this.dataGridViewPart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPart.ContextMenuStrip = this.contextMenuStrip1;
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
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.exportToolStripMenuItem,
			this.countToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(108, 48);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.exportToolStripMenuItem.Text = "Export";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItemClick);
			// 
			// countToolStripMenuItem
			// 
			this.countToolStripMenuItem.Name = "countToolStripMenuItem";
			this.countToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.countToolStripMenuItem.Text = "Count";
			this.countToolStripMenuItem.Click += new System.EventHandler(this.CountToolStripMenuItemClick);
			// 
			// FormPartitions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(831, 297);
			this.Controls.Add(this.dataGridViewPart);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormPartitions";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Partitions";
			this.Load += new System.EventHandler(this.FormPartitionsLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
