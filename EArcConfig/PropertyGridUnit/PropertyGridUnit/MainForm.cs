/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 09.05.2021
 * Time: 19:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using PropertyGridTest;

namespace PropertyGridUnit
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
		
		
		private ARC_SUBSYST_PROFILE _profileData = new ARC_SUBSYST_PROFILE();
		
		void MainFormLoad(object sender, EventArgs e)
		{
	       //устанавливаем редактируемый объект
       _profileData.ID="";
       _profileData.ID_TBLLST="";
       _profileData.ID_GINFO="";
       _profileData.IS_WRITEON=false ;
       _profileData.STACK_NAME="NONE";
       _profileData.LAST_UPDATE="";
       _profileData.IS_VIEWABLE=false ;

       propertyGrid1.SelectedObject=_profileData;
       propertyGrid1.Update();
       
      
		}
	}
}
