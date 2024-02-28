/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 19.02.2024
 * Time: 19:31
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ArcConfig
{
  /// <summary>
  /// Description of FormArc_db_schema_lite.
  /// </summary>
  public partial class FormArc_db_schema_lite : Form
  {
    public FormArc_db_schema_lite()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
    }

    public int _indBoxDB
    {
      get { return comboBoxDB.SelectedIndex ; }
      //set { comboBoxDB.SelectedIndex = value; }
    }
    public int _ind_aa ;
    public int _ind_bb ;
    public List<String> _aa = new List<String>();
    public List<String> _bb = new List<String>();

    public FormArc_db_schema_lite(List<String> a , int ia, List<String> b, int ib)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
      _aa = a ;
      _ind_aa = ia;
      if (_aa.Count<=ia) _ind_aa=_aa.Count-1;
      _bb = b ;
      if (_bb.Count<=ib) _ind_bb=_bb.Count-1;
    }
    void Button1Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    void FormArc_db_schema_liteLoad(object sender, EventArgs e)
    {
      //comboBoxSvc.Items.AddRange((object)_aa);
      for(int i=0;i<_aa.Count;i++)
        comboBoxSvc.Items.Add(_aa[i]);
      comboBoxSvc.SelectedIndex=_ind_aa;
      //comboBoxDB.Items.AddRange(_bb);
      for(int i=0;i<_bb.Count;i++)
        comboBoxDB.Items.Add(_bb[i]);
      comboBoxDB.SelectedIndex=_ind_bb;
    }
    void ButCancelClick(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }


  }
}
