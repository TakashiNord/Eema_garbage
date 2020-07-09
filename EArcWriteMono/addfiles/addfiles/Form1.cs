/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 13.05.2016
 * Time: 17:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace addfiles
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		public Form1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			timer1.Enabled=true;
		}
		void Form1Load(object sender, EventArgs e)
		{
//			MessageBox.Show(progressBar1.Maximum.ToString());
//		    Thread.Sleep(progressBar1.Maximum*1000);
			//timer1.Enabled=true;
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			progressBar1.Value+=1;
		}
	}
}
