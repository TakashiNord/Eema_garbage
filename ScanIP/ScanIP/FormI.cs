/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 09.06.2021
 * Time: 20:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScanIP
{
	/// <summary>
	/// Description of FormI.
	/// </summary>
	public partial class FormI : Form
	{
		public FormI()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
    public string _RetMask
    {
    	get ; 
    	set ;
    }		
		
		void FormILoad(object sender, EventArgs e)
		{
			_RetMask = "" ;
			
			listView1.Clear();
			listView1.View = View.Details ;
			this.listView1.FullRowSelect = true;
	
    listView1.Columns.Add("Класс", 60, HorizontalAlignment.Center);
    listView1.Columns.Add("Маска", 100, HorizontalAlignment.Left);
    listView1.Columns.Add("Инв. Маска", 100, HorizontalAlignment.Left);
    listView1.Columns.Add("Кол-во хостов", 90, HorizontalAlignment.Left);
    listView1.Columns.Add("Префикс", 60, HorizontalAlignment.Center);    

    // Create items and sets of subitems for each item.
    ListViewItem item1 = new ListViewItem("Класс А",0);
  
    item1.SubItems.Add("255.0.0.0");
    item1.SubItems.Add("0.255.255.255");
    item1.SubItems.Add("16777214");
    item1.SubItems.Add("/8");  

    listView1.Items.AddRange(new ListViewItem[]{item1});
    
    ListViewItem item12 = new ListViewItem(new []{"","255.128.0.0","0.127.255.255","8388606","/9"});
    listView1.Items.Add(item12);     
    ListViewItem item13 = new ListViewItem(new []{"","255.192.0.0","0.63.255.255","4194302","/10"});
    listView1.Items.Add(item13);
    ListViewItem item14 = new ListViewItem(new []{"","255.224.0.0","0.31.255.255","2097150","/11"});
    listView1.Items.Add(item14);    
    ListViewItem item15 = new ListViewItem(new []{"","255.240.0.0","0.15.255.255","1048574","/12"});
    listView1.Items.Add(item15);
    ListViewItem item16 = new ListViewItem(new []{"","255.248.0.0","0.7.255.255","524286","/13"});
    listView1.Items.Add(item16);
    ListViewItem item17 = new ListViewItem(new []{"","255.252.0.0","0.3.255.255","262142","/14"});
    listView1.Items.Add(item17);
    ListViewItem item18 = new ListViewItem(new []{"","255.254.0.0","0.1.255.255","131070","/15"});
    listView1.Items.Add(item18);

    
    ListViewItem item2 = new ListViewItem("Класс В",0);
    item2.SubItems.Add("255.225.0.0");
    item2.SubItems.Add("0.0.255.255");
    item2.SubItems.Add("65534");
    item2.SubItems.Add("/16");
    
    listView1.Items.AddRange(new ListViewItem[]{item2}); 

    ListViewItem item22 = new ListViewItem(new []{"","255.255.128.0","0.0.127.255","32766","/17"});
    listView1.Items.Add(item22);     
    ListViewItem item23 = new ListViewItem(new []{"","255.255.192.0","0.0.63.255","16382","/18"});
    listView1.Items.Add(item23);
    ListViewItem item24 = new ListViewItem(new []{"","255.255.224.0","0.0.31.255","8190","/19"});
    listView1.Items.Add(item24);    
    ListViewItem item25 = new ListViewItem(new []{"","255.255.240.0","0.0.15.255","4094","/20"});
    listView1.Items.Add(item25);
    ListViewItem item26 = new ListViewItem(new []{"","255.255.248.0","0.0.7.255","2046","/21"});
    listView1.Items.Add(item26);
    ListViewItem item27 = new ListViewItem(new []{"","255.255.252.0","0.0.3.255","1022","/22"});
    listView1.Items.Add(item27);
    ListViewItem item28 = new ListViewItem(new []{"","255.255.254.0","0.0.1.255","510","/23"});
    listView1.Items.Add(item28); 
    
    
    ListViewItem item3 = new ListViewItem("Класс С",0);
    item3.SubItems.Add("255.225.255.0");
    item3.SubItems.Add("0.0.0.255");
    item3.SubItems.Add("254");
    item3.SubItems.Add("/24");
		
    //Add the items to the ListView.
    listView1.Items.AddRange(new ListViewItem[]{item3});      
			
    ListViewItem item32 = new ListViewItem(new []{"","255.255.255.128","0.0.0.127","126","/25"});
    listView1.Items.Add(item32);     
    ListViewItem item33 = new ListViewItem(new []{"","255.255.255.192","0.0.0.63","62","/26"});
    listView1.Items.Add(item33);
    ListViewItem item34 = new ListViewItem(new []{"","255.255.255.224","0.0.0.31","30","/27"});
    listView1.Items.Add(item34);    
    ListViewItem item35 = new ListViewItem(new []{"","255.255.255.240","0.0.0.15","14","/28"});
    listView1.Items.Add(item35);
    ListViewItem item36 = new ListViewItem(new []{"","255.255.255.248","0.0.0.7","6","/29"});
    listView1.Items.Add(item36);
    ListViewItem item37 = new ListViewItem(new []{"","255.255.255.252","0.0.0.3","2","/30"});
    listView1.Items.Add(item37);
    
    listView1.Items[0].Selected = true;
    
		
		}
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
		}
		void Button1Click(object sender, EventArgs e)
		{
	       if (listView1.SelectedItems.Count <= 0) return ;
	       String ret = "";
           int indexSet = 0;
           for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
           {
              ListViewItem itm = listView1.SelectedItems[i];
              indexSet = itm.Index ;
              ret = itm.SubItems[1].Text.ToString();
              //break ;
           }
           _RetMask = ret.ToString() ;
           this.Close();
		}
	}
}
