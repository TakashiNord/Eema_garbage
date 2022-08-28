/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 31.03.2021
 * Time: 18:52
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArcConfig
{
	/// <summary>
	/// Description of FormOption.
	/// </summary>
	public partial class FormOption : Form
	{

    public bool _OptionFullDelete
    {
        get { return checkBox1.Checked; }
        set { checkBox1.Checked = value; }
    }
    public bool _OptionWriteOnDelete
    {
        get { return checkBox2.Checked; }
        set { checkBox2.Checked = value; }
    }
    public bool _OptionFullName
    {
        get { return checkBox3.Checked; }
        set { checkBox3.Checked = value; }
    }
    public bool _OptionCheckData
    {
        get { return checkBox4.Checked; }
        set { checkBox4.Checked = value; }
    }
    public bool _OptionSchemaName
    {
        get { return checkBox5.Checked; }
        set { checkBox5.Checked = value; }
    }    
    public bool _OptionTableDelete
    {
        get { return checkBox6.Checked; }
        set { checkBox6.Checked = value; }
    }
    
		public FormOption()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}


	}
}
