/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 05.02.2024
 * Time: 14:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.Reflection;
using System.Resources;

using System.Drawing.Imaging;
using System.IO;

namespace ArcConfig
{
	/// <summary>
	/// Description of FormARCSCH.
	/// </summary>
	public partial class FormARCSCH : Form
	{
		public FormARCSCH()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void FormARCSCHLoad(object sender, EventArgs e)
		{
			radioButton1.Checked=true ;
			RadioButton1CheckedChanged(sender, e) ;	
		}
		void RadioButton1CheckedChanged(object sender, EventArgs e)
		{
			ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());
 
			if (radioButton1.Checked) {
			  try {
                Bitmap myImage = (Bitmap)r.GetObject("v1");
                pictureBox1.Image= myImage;
              }
              catch (Exception ex1)
              {
                MessageBox.Show("Error ="+ex1.Message);
              } 
              richTextBox1.Clear(); 
              richTextBox1.AppendText(r.GetString("v1text"));
			}
		}
		void RadioButton2CheckedChanged(object sender, EventArgs e)
		{
			ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());
 
			if (radioButton2.Checked) {
			  try {
                Bitmap myImage = (Bitmap)r.GetObject("v2");
                pictureBox1.Image= myImage;
              }
              catch (Exception ex1)
              {
                MessageBox.Show("Error ="+ex1.Message);
              }
              richTextBox1.Clear();
              richTextBox1.AppendText(r.GetString("v2text"));              
			}	
		}
		void RadioButton3CheckedChanged(object sender, EventArgs e)
		{
			ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());
 
			if (radioButton3.Checked) {
			  try {
                Bitmap myImage = (Bitmap)r.GetObject("v3");
                pictureBox1.Image= myImage;
              }
              catch (Exception ex1)
              {
                MessageBox.Show("Error ="+ex1.Message);
              } 
              richTextBox1.Clear();              
			}	
		}
		void RadioButton4CheckedChanged(object sender, EventArgs e)
		{
			ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());
 
			if (radioButton4.Checked) {
			  try {
                Bitmap myImage = (Bitmap)r.GetObject("v4");
                pictureBox1.Image= myImage;
              }
              catch (Exception ex1)
              {
                MessageBox.Show("Error ="+ex1.Message);
              } 

              richTextBox1.Clear();
              
			}	
		}
	}
}
