/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 27.12.2018
 * Time: 5:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class ConnectDB
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button simpleButton1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox _txDsn;
		private System.Windows.Forms.TextBox _txLogin;
		private System.Windows.Forms.TextBox _txPassword;
		private System.Windows.Forms.Label label4;
		
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
			this.simpleButton1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this._txDsn = new System.Windows.Forms.TextBox();
			this._txLogin = new System.Windows.Forms.TextBox();
			this._txPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(167, 98);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(91, 24);
			this.simpleButton1.TabIndex = 0;
			this.simpleButton1.Text = "Подключиться";
			this.simpleButton1.UseVisualStyleBackColor = true;
			this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Имя источника БД (DSN):";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(117, 22);
			this.label2.TabIndex = 2;
			this.label2.Text = "Логин пользователя:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(131, 20);
			this.label3.TabIndex = 3;
			this.label3.Text = "Пароль пользователя:";
			// 
			// _txDsn
			// 
			this._txDsn.Location = new System.Drawing.Point(158, 21);
			this._txDsn.Name = "_txDsn";
			this._txDsn.Size = new System.Drawing.Size(100, 20);
			this._txDsn.TabIndex = 4;
			// 
			// _txLogin
			// 
			this._txLogin.Location = new System.Drawing.Point(158, 46);
			this._txLogin.Name = "_txLogin";
			this._txLogin.Size = new System.Drawing.Size(100, 20);
			this._txLogin.TabIndex = 5;
			// 
			// _txPassword
			// 
			this._txPassword.Location = new System.Drawing.Point(158, 72);
			this._txPassword.Name = "_txPassword";
			this._txPassword.Size = new System.Drawing.Size(100, 20);
			this._txPassword.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(12, 1);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(246, 20);
			this.label4.TabIndex = 7;
			this.label4.Text = "Заполните поля для подключения  к БД";
			// 
			// ConnectDB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(274, 130);
			this.Controls.Add(this.label4);
			this.Controls.Add(this._txPassword);
			this.Controls.Add(this._txLogin);
			this.Controls.Add(this._txDsn);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.simpleButton1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectDB";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Подключение к РСДУ5";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConnectDBFormClosed);
			this.Shown += new System.EventHandler(this.ConnectDBShown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConnectDBKeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
