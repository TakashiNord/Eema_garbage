/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 13.05.2016
 * Time: 16:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace addfiles
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

    void AddLogString(string s)
    {
    	if(InvokeRequired) {
          richTextBox1.Invoke((Action)delegate { richTextBox1.AppendText("\n invoke="+s); });
          richTextBox1.Invoke((Action)delegate { richTextBox1.Update(); });
    	} else {
    	  richTextBox1.AppendText("\n"+s);
          richTextBox1.Update();
   	    }
    }

		void AddFile( object FilePath)
		{
		   //	
		   string file=(string)FilePath ;
     	System.IO.FileInfo fileinfo = new System.IO.FileInfo(file);
      	long size = fileinfo.Length;
      	int sz = Convert.ToInt32(size/(50*1024*1024));
      	AddLogString(" Размер (байт)=" + size.ToString() + "   время t=" + sz.ToString());   			
      	Form1 example = new Form1();
      	example.textBox1.Text=file ;
        example.progressBar1.Maximum=sz;
        example.progressBar1.Minimum=0;
        example.progressBar1.Step=1;
      	example.Show();
      	Thread.Sleep(sz*1000);
		}
		
		
		void MainFormDragDrop(object sender, DragEventArgs e)
		{
          string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
          int runningWorkers=0;
          ParameterizedThreadStart operation = 
              new ParameterizedThreadStart(AddFile);
          foreach (string file in files)
          {
       	    richTextBox1.AppendText(file); 
       	    Thread thr = new Thread(operation);
       	    thr.Start(file);
       	     runningWorkers=runningWorkers+1;
           }	
		}
		void MainFormDragEnter(object sender, DragEventArgs e)
		{
          if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
          {
             e.Effect = DragDropEffects.All;
          }	
		}
	}
}
