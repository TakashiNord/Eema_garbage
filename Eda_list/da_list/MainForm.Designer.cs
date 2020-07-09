/*
 * Created by SharpDevelop.
 * User: Tanuki
 * Date: 02.04.2017
 * Time: 22:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace da_list
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button button4;
		
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(687, 259);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(79, 39);
			this.button1.TabIndex = 0;
			this.button1.Text = "Add";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(687, 215);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(78, 38);
			this.button2.TabIndex = 1;
			this.button2.Text = "Del";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// listView1
			// 
			this.listView1.LabelEdit = true;
			this.listView1.Location = new System.Drawing.Point(8, 6);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(676, 292);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(8, 304);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(761, 96);
			this.richTextBox1.TabIndex = 3;
			this.richTextBox1.Text = "";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(687, 168);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(78, 41);
			this.button3.TabIndex = 4;
			this.button3.Text = "Re-read";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(691, 13);
			this.textBox1.MaxLength = 20;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(71, 20);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "rsdu2";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(691, 40);
			this.textBox2.MaxLength = 20;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(71, 20);
			this.textBox2.TabIndex = 6;
			this.textBox2.Text = "rsduadmin";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(691, 67);
			this.textBox3.MaxLength = 30;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(71, 20);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = "passme";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(691, 94);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(71, 23);
			this.button4.TabIndex = 8;
			this.button4.Text = "Connect";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(771, 409);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "da_list";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
