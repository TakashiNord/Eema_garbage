/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 10.08.2021
 * Time: 18:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArcConfig
{
	/// <summary>
	/// Description of FormDelInfo1.
	/// </summary>
	public partial class FormDelInfo1 : Form
	{
		public FormDelInfo1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
        public int Add(String s)
		{
		
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			richTextBox1.AppendText(s);
			richTextBox1.AppendText("\n");
			return(0);
		}		
		
	}
}
