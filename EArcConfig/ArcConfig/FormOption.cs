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

    public string _OptionUpdate
    {
      get {
        int v0 = 10;
        if (int.TryParse(textBox3.Text, out v0))
        {
          if (v0<0) v0 = 0 ;
          if (v0==0) v0 = 10 ;
          if (v0>600) v0 = 600 ;
        } else {
          v0 = 10 ;
        }
        textBox3.Text = v0.ToString();
        return textBox3.Text;
      }
      set { textBox3.Text = value ; }
    }  	
  	
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
    public bool _OptionTableDisable
    {
        get { return checkBox7.Checked; }
        set { checkBox7.Checked = value; }
    }
    public string _OptionTableConntime
    {
      get {
        int v0 = 30;
        if (int.TryParse(textBox6.Text, out v0))
          {
          if (v0<0) v0 = 0 ;
          if (v0==0) v0 = 30 ;
          if (v0>600) v0 = 600 ;
        } else {
          v0 = 30 ;
        }
        textBox6.Text = v0.ToString();
        return textBox6.Text;
      }
      set { textBox6.Text = value ; }
    }
    public bool _OptionSaveFormat
    {
        get { return checkBox8.Checked; }
        set { checkBox8.Checked = value; }
    }  

    public string _OptionDBlink1
    {
        get { return comboBox1.Text; }
        set { comboBox1.Text = value; }
    }
    public string _OptionDBlink1login
    {
        get { return textBox1.Text; }
        set { textBox1.Text = value; }
    }
    public string _OptionDBlink1pass
    {
        get { return textBox2.Text; }
        set { textBox2.Text = value; }
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
