/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 12.02.2024
 * Time: 15:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormParamChange
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button buttonSEL;
		private System.Windows.Forms.Button buttonUS;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button buttonSTART;
		private System.Windows.Forms.Button buttonPARAM;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParamChange));
			this.buttonSEL = new System.Windows.Forms.Button();
			this.buttonUS = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.buttonSTART = new System.Windows.Forms.Button();
			this.buttonPARAM = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSEL
			// 
			this.buttonSEL.Location = new System.Drawing.Point(262, 4);
			this.buttonSEL.Name = "buttonSEL";
			this.buttonSEL.Size = new System.Drawing.Size(117, 23);
			this.buttonSEL.TabIndex = 0;
			this.buttonSEL.Text = "Выбрать ВСЕ";
			this.buttonSEL.UseVisualStyleBackColor = true;
			this.buttonSEL.Click += new System.EventHandler(this.ButtonSELClick);
			// 
			// buttonUS
			// 
			this.buttonUS.Location = new System.Drawing.Point(378, 4);
			this.buttonUS.Name = "buttonUS";
			this.buttonUS.Size = new System.Drawing.Size(117, 23);
			this.buttonUS.TabIndex = 1;
			this.buttonUS.Text = "Отменить выбор ";
			this.buttonUS.UseVisualStyleBackColor = true;
			this.buttonUS.Click += new System.EventHandler(this.ButtonUSClick);
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(3, 33);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(755, 457);
			this.dataGridView1.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Включенные архивы:";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(13, 497);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(163, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Архив Куда переносим:";
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(182, 494);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(471, 21);
			this.comboBox1.TabIndex = 5;
			// 
			// buttonSTART
			// 
			this.buttonSTART.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSTART.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonSTART.ForeColor = System.Drawing.Color.Maroon;
			this.buttonSTART.Location = new System.Drawing.Point(674, 494);
			this.buttonSTART.Name = "buttonSTART";
			this.buttonSTART.Size = new System.Drawing.Size(84, 23);
			this.buttonSTART.TabIndex = 6;
			this.buttonSTART.Text = "Перенос->";
			this.buttonSTART.UseVisualStyleBackColor = true;
			this.buttonSTART.Click += new System.EventHandler(this.ButtonSTARTClick);
			// 
			// buttonPARAM
			// 
			this.buttonPARAM.Location = new System.Drawing.Point(113, 4);
			this.buttonPARAM.Name = "buttonPARAM";
			this.buttonPARAM.Size = new System.Drawing.Size(111, 23);
			this.buttonPARAM.TabIndex = 7;
			this.buttonPARAM.Text = "Построить список";
			this.buttonPARAM.UseVisualStyleBackColor = true;
			this.buttonPARAM.Click += new System.EventHandler(this.ButtonPARAMClick);
			// 
			// FormParamChange
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(761, 523);
			this.Controls.Add(this.buttonPARAM);
			this.Controls.Add(this.buttonSTART);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.buttonUS);
			this.Controls.Add(this.buttonSEL);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormParamChange";
			this.Text = "FormParamChange";
			this.Load += new System.EventHandler(this.FormParamChangeLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
