/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 16.04.2021
 * Time: 18:07
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data.Odbc;
using System.Collections.Generic ;

using System.Reflection;
using System.Resources;

namespace ArcConfig
{
  /// <summary>
  /// Description of FormService.
  /// </summary>
  public partial class FormService : Form
  {
    public FormService()
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      //
    }

    public FormService(OdbcConnection conn, String id, String id_tbl, int SchemaName)
    {
      //
      // The InitializeComponent() call is required for Windows Forms designer support.
      //
      InitializeComponent();

      //
      // TODO: Add constructor code after the InitializeComponent() call.
      _conn = conn ;
      _id = id ;
      _id_tbl = id_tbl ;
      _OptionSchemaName = SchemaName ;
      //
    }

    public OdbcConnection _conn;
    public String _id;
    public String _id_tbl;
    public int _OptionSchemaName = 0;
    public string OptionSchemaMain = "RSDUADMIN";
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

   public string GetTypeValue(ref OdbcDataReader reader, int i)
   {
     object obj ;
     string ret="";
     if (reader.IsDBNull(i)) {
          ;
     } else {
        obj = reader.GetValue(i) ;
        string stype= reader.GetDataTypeName(i).ToUpper();
        //AddLogString("reader.GetDataTypeName = " + stype);
        ret = obj.ToString();
     }
     return(ret);
   }


   public void GetLoad()
   {

      ResourceManager r = new ResourceManager("ArcConfig.ArcResource", Assembly.GetExecutingAssembly());

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      string sl1 = "" ;

      buttonSave.Enabled = false ;
      
      
      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }       
      

      // =============================================================================

      // dpload
      sl1= "SELECT id, name FROM  "+stSchema+"ad_service " + 
      	"WHERE define_alias LIKE 'ADV_SRVC_DPLOADADCP_ACCESPORT%' ORDER BY id ASC" ;
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
            string sn=GetTypeValue(ref reader, i);
            id_sn = id_sn + " " + sn ;
          }

          checkedListBoxDpload.Items.Add(id_sn,CheckState.Unchecked );

        } // while
      }
      reader.Close();


      sl1 = r.GetString("AD_SERVICE1");
      sl1 = String.Format(sl1,_id_tbl);

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
             arr[i]=GetTypeValue(ref reader, i);
          }

          //arr[6] -- define_alias
          //arr[1] -- id_proto=
          int ci=Convert.ToInt32(arr[1]);
          if (ci==1) {
            // arr[0] -- portnumber
            string id_sn = arr[0] + " (TCP) " + arr[5] ;
            checkedListBoxTech.Items.Add(id_sn,CheckState.Unchecked );
          }
          if (ci==2) {
            // arr[0] -- portnumber
            string id_sn = arr[0] + " (UDP) " + arr[5] ;
            checkedListBoxTech.Items.Add(id_sn,CheckState.Unchecked );
          }
          if (ci==9) {
            // arr[0] -- portnumber
            string id_sn = arr[0] + " (ClearUDP) " + arr[5] ;
            checkedListBoxTech.Items.Add(id_sn,CheckState.Unchecked );
          }

        } // while
      }
      reader.Close();
      
      //++++++ ??? ADV_SRVC_OICOPC_ACCESSPORT%
      
      // define_alias LIKE 'ADV_SRVC_RDAADCP_ACCESPORT%'
      sl1= "SELECT id, name FROM "+stSchema+"ad_service " +
      	"WHERE define_alias LIKE 'ADV_SRVC_RDA%_ACCESPORT%' ORDER BY id ASC" ;
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
            string sn=GetTypeValue(ref reader, i);
            id_sn = id_sn + " " + sn ;
          }

          checkedListBoxTech.Items.Add(id_sn,CheckState.Unchecked );

        } // while
      }
      reader.Close();

 // ==================================================

      //ARC_SERVICES_TUNE
      //  ID_SPROFILE = ID
      //
      //ARC_SERVICES_ACCESS
      //  ID_SPROFILE=ID
      //
      //-----------------------

/*
SELECT arcs.ID_SERVICE, ads.NAME
FROM ARC_SERVICES_TUNE arcs, AD_SERVICE ads
WHERE ads.ID=arcs.ID_SERVICE and arcs.ID_SPROFILE=5
union
SELECT arcs.ID_SERVICE, "?????????????????" as NAME
FROM ARC_SERVICES_TUNE arcs, AD_SERVICE ads
WHERE arcs.ID_SERVICE not in (SELECT id FROM AD_SERVICE)
and arcs.ID_SPROFILE=5
*/

      Dpload.Clear();

      sl1 = r.GetString("FormSERVICE1");
      sl1 = String.Format(sl1,_id);

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
          int flag_add = 0 ; // элемент добавлен
          string id_sn="";
          for ( int i = 0; i<2; i++)
          {
            string sn = GetTypeValue(ref reader, i);

            if (i==0) { Dpload.Add(sn); }

            id_sn = id_sn + " " + sn ;

            string itemN = "" ;

            if (i==0) {
              for (int j = 0; j < checkedListBoxDpload.Items.Count; j++)
              {
                //itemN = checkedListBoxDpload.Items[j].ToString();
                itemN = checkedListBoxDpload.GetItemText(checkedListBoxDpload.Items[j]);

                /*
                itemN = itemN.Trim() ;
                int tParam1 = 0 ;
                if (!Int32.TryParse(itemN, out tParam1)) {
                continue ;
                }
                tParam1 = Math.Abs(tParam1);
                itemN = tParam1.ToString();
                */

                if (itemN.IndexOf(sn)>=0) {
                  checkedListBoxDpload.SetItemChecked(j,true);
                  flag_add = 1;
                  break ;
                }

               } // for
             }//if==0

           }

           if (flag_add==0) {
               checkedListBoxDpload.Items.Add(id_sn,CheckState.Checked );
           }

        } // while
      }
      reader.Close();

      Tech.Clear();
      String RETRO_DEPTH = "0" ;


/*
SELECT arcs.ID_SERVICE, ads.NAME , arcs.RETRO_DEPTH
FROM ARC_SERVICES_ACCESS arcs, AD_SERVICE ads
WHERE ads.ID=arcs.ID_SERVICE and arcs.ID_SPROFILE=5
union
SELECT arcs.ID_SERVICE, "?????????????????" as NAME , 0 as RETRO_DEPTH
FROM ARC_SERVICES_ACCESS arcs, AD_SERVICE ads
WHERE arcs.ID_SERVICE not in (SELECT id FROM AD_SERVICE)
and arcs.ID_SPROFILE=5
*/

      sl1 = r.GetString("FormSERVICE2");
      sl1 = String.Format(sl1,_id);

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
          int flag_add = 0 ; // элемент добавлен
          string id_sn="";
          for ( int i = 0; i<3; i++)
          {
            string sn = GetTypeValue(ref reader, i);

            if (i==0) { Tech.Add(sn); }

            if (i==2) {
              if (sn!="0") RETRO_DEPTH = sn ;
              continue ;
            }

            id_sn = id_sn + " " + sn ;

            string itemN = "" ;

            if (i==0) {
              for (int j = 0; j < checkedListBoxTech.Items.Count; j++)
              {
                //itemN = checkedListBoxTech.Items[j].ToString();
                itemN = checkedListBoxTech.GetItemText(checkedListBoxTech.Items[j]);

                if (itemN.IndexOf(sn)>=0) {
                  checkedListBoxTech.SetItemChecked(j,true);
                  flag_add = 1;
                  break ;
                }

               } // for
             }//if==0

           }

           if (flag_add==0) {
               checkedListBoxTech.Items.Add(id_sn,CheckState.Checked );
           }

        } // while
      }
      reader.Close();

      //RETRO_DEPTH
      BoxPeriod.Text = RETRO_DEPTH ;

      buttonSave.Enabled = true ;

   }


   public void SaveData()
   {

      //ARC_SERVICES_TUNE
      //  ID_SPROFILE = ID
      //
      //ARC_SERVICES_ACCESS
      //  ID_SPROFILE=ID
      //
      //-----------------------

      // Объект для выполнения запросов к базе данных
      OdbcCommand cmd0 = new OdbcCommand();
      OdbcDataReader reader = null ;

      cmd0.Connection=this._conn;

      String sl1 = "" ;
      int ret1 = 0 ;
      int tParam1 = 0 ;

      
      string stSchema="";
      if (_OptionSchemaName>0) {
        stSchema=OptionSchemaMain + "." ;
      }       
      
      //for(int i=0;i<Tech.Count; i++) MessageBox.Show(Tech[i].ToString() );

      //for(int i=0;i<Dpload.Count; i++) MessageBox.Show(Dpload[i].ToString() );

      //Вставка в ARC_SERVICES_TUNE настроек для записи нового профиля архивов
      sl1= "DELETE FROM "+stSchema+"ARC_SERVICES_TUNE WHERE ID_SPROFILE=" + _id ;
      cmd0.CommandText=sl1;
      try
      {
        ret1 = cmd0.ExecuteNonQuery();
      }
      catch (Exception )
      {
        ;//MessageBox.Show(ex.ToString() );
      }

      int nID_PRIORITY = 1 ;
      for (int i = 0; i < checkedListBoxDpload.Items.Count; i++)
      {
        if (checkedListBoxDpload.GetItemChecked(i)) {

          string rec_id = checkedListBoxDpload.Items[i].ToString();

          rec_id = rec_id.Trim() ;
          tParam1 = 0 ;
          if (!Int32.TryParse(rec_id.Split(' ')[0], out tParam1)) {
              continue ;
          }
          tParam1 = Math.Abs(tParam1);
          rec_id = tParam1.ToString();

          sl1= ""+
           "Insert into "+stSchema+"ARC_SERVICES_TUNE(ID_SPROFILE, ID_SERVICE, PRIORITY) " +
           " Values ("+_id+","+rec_id+","+nID_PRIORITY+")";
          cmd0.CommandText=sl1;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            MessageBox.Show(ex1.ToString() );
            reader.Close();
            return ;
          }
          reader.Close();

          nID_PRIORITY++;
        }
      }


      //Вставка в ARC_SERVICES_ACCESS настроек для чтения нового профиля архивов

      sl1= "DELETE FROM "+stSchema+"ARC_SERVICES_ACCESS WHERE ID_SPROFILE =" + _id ;
      cmd0.CommandText=sl1;
      try
      {
        ret1 = cmd0.ExecuteNonQuery();
      }
      catch (Exception)
      {
        ;//MessageBox.Show(ex.ToString() );
      }

      string Period = BoxPeriod.Text.Trim() ;
      tParam1 = 0 ;
      if (!Int32.TryParse(Period.Split(' ')[0], out tParam1)) {
        tParam1 = 3660 ;
      }
      tParam1 = Math.Abs(tParam1);
      Period = tParam1.ToString();

      for (int i = 0; i < checkedListBoxTech.Items.Count; i++)
      {
        if (checkedListBoxTech.GetItemChecked(i)) {

          string rec_id = checkedListBoxTech.Items[i].ToString();

          rec_id = rec_id.Trim() ;
          tParam1 = int.Parse(rec_id.Split(' ')[0]);
          tParam1 = Math.Abs(tParam1);
          rec_id = tParam1.ToString();

          sl1= ""+
           "Insert into "+stSchema+"ARC_SERVICES_ACCESS(ID_SPROFILE, ID_SERVICE, RETRO_DEPTH) " +
           " Values ("+_id+","+rec_id+","+Period+")";
          cmd0.CommandText=sl1;
          try
          {
            reader = cmd0.ExecuteReader();
          }
          catch (Exception ex1)
          {
            reader.Close();
            MessageBox.Show(ex1.ToString() );
            return ;
          }
          reader.Close();
        }
      }



   }


    void ButtonCancelClick(object sender, EventArgs e)
    {
      this.Close();
    }
    void ButtonSaveClick(object sender, EventArgs e)
    {
       DialogResult result = MessageBox.Show ("Записать изменения и завершить Диалог ? " ," Применение изменений",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
       if (result == DialogResult.Yes)
       {
          SaveData();
          this.Close();
       }
    }
    void FormServiceLoad(object sender, EventArgs e)
    {
      GetLoad();
    }
  }
}
