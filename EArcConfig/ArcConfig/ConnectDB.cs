/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 27.12.2018
 * Time: 5:55
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data.Odbc;

namespace ArcConfig
{
	/// <summary>
	/// Description of ConnectDB.
	/// </summary>
	public partial class ConnectDB : Form
	{
		public ConnectDB()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		private OdbcConnection _conn;

		public OdbcConnection Conn
		{
			get
			{
				return this._conn;
			}
		}

    private OdbcConnection _DB_Connect()
    {
      OdbcConnection odbcConnection = new OdbcConnection("DSN=" + this._txDsn.Text + ";UID=" + this._txLogin.Text + ";PWD=" + this._txPassword.Text + "; Pooling=False;");
      if (odbcConnection != null)
      {
        try
        {
          odbcConnection.Open();
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Ошибка подключения к БД\n" + ex.Message);
          odbcConnection.Dispose();
          odbcConnection = (OdbcConnection) null;
        }
      }
      return odbcConnection;
    }

    private void _connectSelect_function()
    {
      this._conn = this._DB_Connect();
      if (this._conn == null)
        return;
      this.Visible = false;
      this.FormClosed -= new FormClosedEventHandler(this.ConnectDBFormClosed);
      this.Close();
    }

		void SimpleButton1Click(object sender, EventArgs e)
		{
			this._connectSelect_function();
		}

		void ConnectDBFormClosed(object sender, FormClosedEventArgs e)
		{
			if (e.CloseReason != CloseReason.UserClosing) return;
			//Application.Exit();
		}

		void ConnectDBShown(object sender, EventArgs e)
		{
			this._txDsn.Text = "RSDU2";
			this._txLogin.Text = "rsduadmin";
			this._txPassword.Text = "passme";
			this._txPassword.Focus();
		}

		void ConnectDBKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return) return;
			this._connectSelect_function();
		}





	}
}
