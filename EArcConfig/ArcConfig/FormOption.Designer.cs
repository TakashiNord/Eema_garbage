/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 31.03.2021
 * Time: 18:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormOption
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox6;
		
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
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBox5
			// 
			this.checkBox5.Location = new System.Drawing.Point(12, 253);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(223, 24);
			this.checkBox5.TabIndex = 9;
			this.checkBox5.Text = "Имя обьекта с названием схемы **";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox6);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.checkBox4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.checkBox3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(263, 235);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "вкладки Главная - Параметры";
			// 
			// checkBox6
			// 
			this.checkBox6.ForeColor = System.Drawing.Color.Red;
			this.checkBox6.Location = new System.Drawing.Point(47, 50);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(196, 15);
			this.checkBox6.TabIndex = 14;
			this.checkBox6.Text = "Удаление таблицы архива";
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(37, 186);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(220, 32);
			this.label4.TabIndex = 13;
			this.label4.Text = "проверка вызовет дополнительный запрос";
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(17, 168);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(239, 24);
			this.checkBox4.TabIndex = 12;
			this.checkBox4.Text = "Проверять наличие данных";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(37, 135);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(206, 20);
			this.label2.TabIndex = 11;
			this.label2.Text = "расчет займет больше времени";
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(17, 118);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(239, 24);
			this.checkBox3.TabIndex = 10;
			this.checkBox3.Text = "Формировать полный путь до параметра";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(61, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 17);
			this.label1.TabIndex = 9;
			this.label1.Text = "через процедуру";
			// 
			// checkBox1
			// 
			this.checkBox1.ForeColor = System.Drawing.Color.Red;
			this.checkBox1.Location = new System.Drawing.Point(47, 71);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(215, 24);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "Удаление Всех архивов параметра.";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(17, 19);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(239, 28);
			this.checkBox2.TabIndex = 6;
			this.checkBox2.Text = "Отключение даже обязательных архивов";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// FormOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(288, 339);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBox5);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOption";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Опции вывода";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
