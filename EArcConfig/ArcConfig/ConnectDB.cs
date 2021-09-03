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

using System.Collections.Generic;

using System.Security.Permissions;
using Microsoft.Win32;


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
    	string sDsn = _cmbBoxDsn.Text.Trim();
    	//sDsn = this._txDsn.Text.Trim();
   	    	
    	OdbcConnection odbcConnection = new OdbcConnection("DSN=" + sDsn + ";UID=" + this._txLogin.Text + ";PWD=" + this._txPassword.Text + "; Pooling=False;");
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
			
			List<string> list1 = EnumDsn();
			foreach (string nm in list1) {
				this._cmbBoxDsn.Items.Add(nm);
			}
			this._cmbBoxDsn.Text = "RSDU2";
			
		}

		void ConnectDBKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return) return;
			this._connectSelect_function();
		}


    private List<string> EnumDsn()
    {
        List<string> list = new List<string>();
        list.AddRange(EnumDsn(Registry.CurrentUser));
        list.AddRange(EnumDsn(Registry.LocalMachine));
        return list;
    }

    private IEnumerable<string> EnumDsn(RegistryKey rootKey)
    {
        RegistryKey regKey = rootKey.OpenSubKey(@"Software\ODBC\ODBC.INI\ODBC Data Sources");
        if (regKey != null)
        {
            foreach (string name in regKey.GetValueNames())
            {
                string value = regKey.GetValue(name, "").ToString();
                yield return name;
            }
        }
    }



	}
}
