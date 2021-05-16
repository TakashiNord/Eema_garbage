using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design ;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design ;
using System.Data;

namespace PropertyGridUtils
{
   /// <summary>
   /// CheckListBoxEditor
   /// </summary>
 class CheckListBoxEditor
 {
  private string val = "(Collection)";

  [CategoryAttribute("Editor"), DescriptionAttribute("This property contains the checked ListBox   collection"),
    Editor(typeof(CheckedListBoxUITypeEditor), typeof(UITypeEditor))]
  public string CheckedListBoxCollectionProperty
  {
    get
    {
      return val;
    }
    set
    {
      val =  "(Collection)";
    }
  }
 }
 
  public class CheckedListBoxUITypeEditor : System.Drawing.Design.UITypeEditor
  {
    public CheckedListBox checklisbox1= new CheckedListBox();
    private IWindowsFormsEditorService es;
    public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle (System.ComponentModel.ITypeDescriptorContext context)
    {
     return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
    }
    public override bool IsDropDownResizable
    {
      get
      {
        return true;
      }
    }
    public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,   IServiceProvider provider, object value)
    {
      es = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
      if (es != null)
      {
        LoadValues();
        es.DropDownControl(checklisbox1);
      }
      return "hi";
    }

    public void LoadValues()
    {

       Hashtable table = new Hashtable();
       for(int i =0;i<5;i++)
       {
         table.Add("a",true);
        }
       checklisbox1.Items.Clear();
       foreach (DictionaryEntry dic in table)
       {
          checklisbox1.Items.Add(dic.Key, (bool)dic.Value);
       }
    }
  }
 
}
