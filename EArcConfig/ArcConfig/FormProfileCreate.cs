/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 03.02.2021
 * Time: 10:37
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data.Odbc;
using System.Collections.Generic ;

namespace ArcConfig
{
  /// <summary>
  /// Description of FormProfileCreate.
  /// </summary>
  public partial class FormProfileCreate : Form
  {
    public FormProfileCreate()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //


    }

  public FormProfileCreate(OdbcConnection conn, String id_gt, String id_tbl)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      _conn = conn ;
      _id_gpt = id_gt ;
      _id_tbl = id_tbl ;
      //
    }

    public OdbcConnection _conn;
    public String _id_gpt;
    public String _id_tbl;
    //public string[] Dpload = new string[100];
    public List<string> Dpload = new List<string>();
    public List<string> Tech = new List<string>();

    public OdbcConnection Conn
    {
      get
      {
        return this._conn;
      }
    }
    void FormProfileCreateLoad(object sender, EventArgs e)
    {

      buttonOk.Enabled=false ;

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string id_dest = "" ;
      string sl1= "SELECT ID+1 FROM ARC_SUBSYST_PROFILE WHERE ID+1 NOT IN (SELECT ID FROM ARC_SUBSYST_PROFILE )" ; //  AND id > 3
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.ToString() );
      	return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
          int i = 0 ;
          string sn="";
          if (reader.IsDBNull(i)) {
              ;
          } else {
              sn= reader.GetDataTypeName(i);
              string stmp = sn.ToUpper();
              if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
              if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
              if (stmp=="VARCHAR2") sn = reader.GetString(i);
              if (stmp=="NVARCHAR") sn = reader.GetString(i);
              if (stmp=="TEXT") sn = reader.GetString(i);
              if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
          }
          id_dest = sn ;
          break ;
        } // while
      }
      reader.Close();

      string st_name="" ;
      sl1= "SELECT upper(table_name) , lnk.ID_LSTTBL " +
       " FROM sys_tbllst lst, sys_tbllnk lnk, sys_otyp t " +
       " WHERE lnk.id_lsttbl = " + _id_tbl +
       "  AND lnk.id_dsttbl = lst.ID " +
       "  AND lst.id_type = t.ID " +
       "  AND t.define_alias LIKE 'ARH'";
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.ToString() );
      	return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
          int i = 0 ;
          string sn="";
          if (reader.IsDBNull(i)) {
              ;
          } else {
              sn= reader.GetDataTypeName(i);
              string stmp = sn.ToUpper();
              if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
              if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
              if (stmp=="VARCHAR2") sn = reader.GetString(i);
              if (stmp=="NVARCHAR") sn = reader.GetString(i);
              if (stmp=="WVARCHAR") sn = reader.GetString(i);
              if (stmp=="TEXT") sn = reader.GetString(i);
              if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
          }
          st_name = sn ;
          break ;
        } // while
      }
      reader.Close();

      if (st_name=="") {
        st_name = "MEAS_ARC" ;
      }

      st_name = st_name + "_" + _id_tbl + "_" + _id_gpt ;

      textBoxID.Text=id_dest;
      textBoxID_GINFO.Text=_id_gpt;
      textBoxID_TBLLST.Text=_id_tbl;
      comboBoxIS_WRITEON.SelectedIndex=0;
      comboBoxIS_VIEWABLE.SelectedIndex=1;
      textBoxSTACK_NAME.Text = st_name ;


      // get table
      string TABLE_NAME = "" ;

      sl1= "SELECT upper(lst.TABLE_NAME) FROM sys_tbllst lst WHERE lst.ID=" + _id_tbl;
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.ToString() );
      	return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
          int i = 0;
          string sn="";
          if (reader.IsDBNull(i)) {
              ;
          } else {
              sn= reader.GetDataTypeName(i);
              string stmp = sn.ToUpper();
              if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
              if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
              if (stmp=="VARCHAR2") sn = reader.GetString(i);
              if (stmp=="NVARCHAR") sn = reader.GetString(i);
              if (stmp=="WVARCHAR") sn = reader.GetString(i);
              if (stmp=="TEXT") sn = reader.GetString(i);
              if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
          }
          TABLE_NAME = sn.ToUpper() ;
          break ;

        } // while
      }
      reader.Close();


      string Serv_name ="" ;
      int FL_dpload = 1 ; // флаг загрузки
      int FL_rdp = 0 ;

      if (TABLE_NAME.IndexOf("PHREG_LIST_V")>=0) {
        Serv_name="ADV_SRVC_PHROIC_ACCESPORT";
      }
      if (TABLE_NAME.IndexOf("ELREG_LIST_V")>=0) {
        Serv_name="ADV_SRVC_ELROIC_ACCESPORT";
      }
      if (TABLE_NAME.IndexOf("PSWT_LIST_V")>=0) {
        Serv_name="ADV_SRVC_PWSOIC_ACCESPORT";
      }
      if (TABLE_NAME.IndexOf("AUTO_LIST_V")>=0) {
        Serv_name="ADV_SRVC_SSWOIC_ACCESPORT" ;
        FL_dpload = 0 ;
      }
      if (TABLE_NAME.IndexOf("EA_CHANNELS")>=0) {
        FL_rdp = 1 ;
        FL_dpload = 0 ;
      }
      if (TABLE_NAME.IndexOf("EA_V_CONSUMER_POINTS")>=0) {
        FL_rdp = 1 ;
        FL_dpload = 0 ;
      }
      if (TABLE_NAME.IndexOf("CALC_LIST")>=0) {
        FL_rdp = 1 ;
        FL_dpload = 0 ;
      }
      if (TABLE_NAME.IndexOf("DG_LIST")>=0) {
        FL_rdp = 1 ;
        FL_dpload = 0 ;
      }
      if (TABLE_NAME.IndexOf("EXDATA_LIST_V")>=0) {
        FL_rdp = 1 ;
        FL_dpload = 0 ;
      }

      if (TABLE_NAME.IndexOf("DA_V_LST")>=0) {
        Serv_name="ADV_SRVC_DCSOIC";
        //Serv_name="ADV_SRVC_DCSOICTCP_ACCESSPORT";
        //Serv_name="ADV_SRVC_DCSOIC_ACCESPORT";
      }
      //MessageBox.Show(TABLE_NAME + " === " + Serv_name );

      Dpload.Clear();
      checkedListBoxDpload.Items.Clear();
      sl1= "SELECT id, name FROM ad_service WHERE define_alias LIKE 'ADV_SRVC_DPLOADADCP_ACCESPORT%' ORDER BY id ASC" ;
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.ToString() );
      	return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {

          string id_sn="";
          for ( int i = 0; i<2; i++)
          {
            string sn="";
            if (reader.IsDBNull(i)) {
                ;
            } else {
                sn= reader.GetDataTypeName(i);
                string stmp = sn.ToUpper();
                if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
                if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
                if (stmp=="VARCHAR2") sn = reader.GetString(i);
                if (stmp=="NVARCHAR") sn = reader.GetString(i);
                if (stmp=="WVARCHAR") sn = reader.GetString(i);
                if (stmp=="TEXT") sn = reader.GetString(i);
                if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
            }
            if (i==0) { Dpload.Add(sn); }
            id_sn = id_sn + " " + sn ;
          }

          if (FL_dpload ==0) {
            checkedListBoxDpload.Items.Add(id_sn,CheckState.Unchecked );
          } else {
            checkedListBoxDpload.Items.Add(id_sn,CheckState.Checked );
          }

        } // while
      }
      reader.Close();


      textBoxPeriod.Text="3660" ;
      checkedListBoxTech.Items.Clear();
      Tech.Clear();

      if (Serv_name.Equals("")==false ) {

       sl1= " SELECT "+
"  ad_pinfo.portnumber,ad_pinfo.id_proto,ad_sinfo.id_lsttbl,ad_list.id_type,"+
"  sys_otyp.alias,ad_service.NAME,ad_service.define_alias "+
" FROM ad_dir,ad_dir dir1,ad_list,ad_pinfo,ad_ncard,ad_sinfo,sys_otyp,ad_service,ad_hosts "+
" WHERE ad_sinfo.id_server_node = ad_dir.ID "+
" AND ad_list.id_node = ad_dir.ID "+
" AND ad_pinfo.id_param = ad_list.ID "+
" AND ad_pinfo.id_intrface_node = ad_ncard.id_node "+
" AND ad_list.id_type = sys_otyp.ID "+
" AND ad_pinfo.portnumber = ad_service.ID "+
" AND ad_pinfo.id_intrface_node = dir1.ID "+
" AND dir1.id_parent = ad_hosts.id_host_node "+
" AND ad_pinfo.id_proto > 2 AND ad_pinfo.id_proto <> 9 "+
" AND ad_dir.ID IN (SELECT ad_dir.ID FROM ad_dir WHERE ad_dir.id_type > 1000) "+
" AND ad_sinfo.ID_LSTTBL=" + _id_tbl +
"  UNION "+
" SELECT "+
"   ad_pinfo.portnumber,ad_pinfo.id_proto,ad_sinfo.id_lsttbl,ad_list.id_type,"+
"   sys_otyp.alias,ad_service.NAME,ad_service.define_alias "+
"  FROM ad_dir,ad_dir dir1,ad_list,ad_pinfo,ad_ipinfo,ad_sinfo,sys_otyp,ad_service,ad_hosts "+
"  WHERE ad_sinfo.id_server_node = ad_dir.ID "+
" AND ad_list.id_node = ad_dir.ID "+
" AND ad_pinfo.id_param = ad_list.ID "+
" AND ad_pinfo.id_intrface_node = ad_ipinfo.id_node "+
" AND ad_list.id_type = sys_otyp.ID "+
" AND ad_pinfo.portnumber = ad_service.ID "+
" AND ad_pinfo.id_intrface_node = dir1.ID "+
" AND dir1.id_parent = ad_hosts.id_host_node "+
" AND ((ad_pinfo.id_proto <= 2) OR (ad_pinfo.id_proto = 9)) "+
" AND ad_dir.ID IN (SELECT ad_dir.ID FROM ad_dir WHERE ad_dir.id_type > 1000) "+
" AND ad_sinfo.ID_LSTTBL="+ _id_tbl ;

      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
      	MessageBox.Show(ex1.ToString() );
      	return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {
          string[] arr = new string[7];
          for ( int i = 0; i<7; i++)
          {
            string sn="";
            if (reader.IsDBNull(i)) {
              ;
            } else {
              sn= reader.GetDataTypeName(i);
              string stmp = sn.ToUpper();
              if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
              if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
              if (stmp=="VARCHAR2") sn = reader.GetString(i);
              if (stmp=="NVARCHAR") sn = reader.GetString(i);
              if (stmp=="WVARCHAR") sn = reader.GetString(i);
              if (stmp=="TEXT") sn = reader.GetString(i);
              if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
           }
           arr[i]=sn;
          }

          //arr[6] -- define_alias
          //
          //MessageBox.Show(arr[6] + " " + Serv_name );

          if (arr[6].IndexOf(Serv_name)>=0) {
            //arr[1] -- id_proto=
            int ci=Convert.ToInt32(arr[1]);
            if (ci==1) {
              // arr[0] -- portnumber
              Tech.Add(arr[0]);
              string id_sn = arr[0] + " " + arr[5] ;
              checkedListBoxTech.Items.Add(id_sn,CheckState.Checked );
            }
          }

        } // while
      }
      reader.Close();
    }


    if (FL_rdp == 1) {

      sl1= "SELECT id, name FROM ad_service WHERE define_alias LIKE 'ADV_SRVC_RDAADCP_ACCESPORT%' ORDER BY id ASC" ;
      cmd0.CommandText=sl1;
      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.ToString() );
      	return ;
      }

      if (reader.HasRows) {
        while (reader.Read())
        {

          string id_sn="";
          for ( int i = 0; i<2; i++)
          {
            string sn="";
            if (reader.IsDBNull(i)) {
                ;
            } else {
                sn= reader.GetDataTypeName(i);
                string stmp = sn.ToUpper();
                if (stmp=="DECIMAL") sn = reader.GetValue(i).ToString();
                if (stmp=="NUMBER") sn = reader.GetValue(i).ToString(); //GetDecimal(i).ToString();
                if (stmp=="VARCHAR2") sn = reader.GetString(i);
                if (stmp=="NVARCHAR") sn = reader.GetString(i);
                if (stmp=="WVARCHAR") sn = reader.GetString(i);
                if (stmp=="TEXT") sn = reader.GetString(i);
                if (stmp=="INTEGER") sn = reader.GetValue(i).ToString();
            }
            if (i==0) { Tech.Add(sn); }
            id_sn = id_sn + " " + sn ;
          }

          if (id_sn!="") { checkedListBoxTech.Items.Add(id_sn,CheckState.Checked ); }

        } // while
      }
      reader.Close();
    }

     buttonOk.Enabled=true ;

    }
    void ButtonCancelClick(object sender, EventArgs e)
    {
      // cancel
      this.Close();
    }
    void ButtonOkClick(object sender, EventArgs e)
    {
      // create
      //----------------------
      // ARC_SUBSYST_PROFILE
      //    ID
      //ARC_SERVICES_TUNE
      //  ID_SPROFILE = ID
      //
      //ARC_SERVICES_ACCESS
      //  ID_SPROFILE=ID
      //
      //-----------------------

      string id_dest=textBoxID.Text;
      string id_gpt=textBoxID_GINFO.Text;
      string id_tbl=textBoxID_TBLLST.Text;
      string IS_WRITEON = "0";
      if (comboBoxIS_WRITEON.SelectedIndex==1) { IS_WRITEON = "1"; }
      string IS_VIEWABLE = "0" ;
      if (comboBoxIS_VIEWABLE.SelectedIndex==1) { IS_VIEWABLE = "1"; }
      string st_name=textBoxSTACK_NAME.Text.Trim() ;

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string sl1= ""+
"Insert into ARC_SUBSYST_PROFILE (ID, ID_TBLLST, ID_GINFO, IS_WRITEON, STACK_NAME, LAST_UPDATE, IS_VIEWABLE)" +
" Values ("+id_dest+","+id_tbl+","+id_gpt+","+IS_WRITEON+",'"+ st_name + "',0," + IS_VIEWABLE + ")";
      cmd0.CommandText=sl1;

      try
      {
        reader = cmd0.ExecuteReader();
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.ToString() );
      	return ;
      }
      reader.Close();


      //Вставка в ARC_SERVICES_TUNE настроек для записи нового профиля архивов
      int nID_PRIORITY = 1 ;
      for (int i = 0; i < checkedListBoxDpload.Items.Count; i++)
      {
      	if (checkedListBoxDpload.GetItemChecked(i)) {
      		string rec_id=Dpload[i] ;
      		sl1= ""+
           "Insert into ARC_SERVICES_TUNE(ID_SPROFILE, ID_SERVICE, PRIORITY) " +
           " Values ("+id_dest+","+rec_id+","+nID_PRIORITY+")";
          cmd0.CommandText=sl1;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            MessageBox.Show(ex1.ToString() );
          	return ;
          }
          reader.Close();
          		
          nID_PRIORITY++;
      	}
      } 
      
      //Вставка в ARC_SERVICES_ACCESS настроек для чтения нового профиля архивов:');
      string Period = textBoxPeriod.Text.Trim() ;
      for (int i = 0; i < checkedListBoxTech.Items.Count; i++)
      {
      	if (checkedListBoxTech.GetItemChecked(i)) {
      		string rec_id=Tech[i] ;
      		sl1= ""+
           "Insert into ARC_SERVICES_ACCESS(ID_SPROFILE, ID_SERVICE, RETRO_DEPTH) " +
           " Values ("+id_dest+","+rec_id+","+Period+")";
          cmd0.CommandText=sl1;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            MessageBox.Show(ex1.ToString() );
          	return ;
          }
          reader.Close();	
      	}
      } 

      Close();
         
    }





  }
}
