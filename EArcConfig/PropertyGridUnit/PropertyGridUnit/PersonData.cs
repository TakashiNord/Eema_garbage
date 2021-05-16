using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using PropertyGridUtils;
using System.Diagnostics;
using System.Collections.Specialized;

namespace PropertyGridTest
{
   /// <summary>
   /// ������ ��� �������������� � PropertyGrid
   /// </summary>
   class ARC_SUBSYST_PROFILE : FilterablePropertyBase
   {
      /// <summary>
      /// �����������
      /// </summary>
      public ARC_SUBSYST_PROFILE()
      {

      }

      private string _id = "";

      /// <summary>
      /// �������������
      /// </summary>
      [DisplayName("ID")]
      [Description("id - ������� ������")]
      [Category("1. ������")]
      [ReadOnly(true)]
      public string ID
      {
         get { return _id; }
         set { _id = value; }
      }

      private string _id_tbllst = "";
      [DisplayName("ID - ������� �������")]
      [Description("ID - ������� �������")]
      [Category("1. ������")]
      [ReadOnly(true)]
      public string ID_TBLLST
      {
         get { return _id_tbllst ; }
         set { _id_tbllst = value; }
      }

      
      private string _id_ginfo = "";

      [DisplayName("Id - ������")]
      [Description("Id - ������")]
      [Category("1. ������")]
      [ReadOnly(true)]
      public string ID_GINFO
      {
         get { return _id_ginfo; }
         set { _id_ginfo = value; }
      }

      
      private bool _is_writeon = false;
      [DisplayName("������ ��� ����")]
      [Description("���� ������ ��� ���� ���������� �������")]
      [Category("2. �����")]
      [TypeConverter(typeof(BooleanTypeConverter))]
      public bool IS_WRITEON
      {
         get { return _is_writeon; }
         set { _is_writeon = value; }
      }
      
      private string _stack_name = "";
      [DisplayName("�������-����")]
      [Description("��� �������-����� ��� ������ ������ �� ������� ������� �/��� ��� �����")]
      [Category("2. �����")]
      public string STACK_NAME
      {
         get { return _stack_name; }
         set { _stack_name = value; }
      }      


      private bool _is_viewable = false;
      [DisplayName("Retroview")]
      [Description("�������, ��� ������ ������� ���� ����� ��������������� � ����� (1-��, 0-���)")]
      [Category("2. �����")]
      [TypeConverter(typeof(BooleanTypeConverter))]
      public bool IS_VIEWABLE
      {
         get { return _is_viewable; }
         set { _is_viewable = value; }
      }

      private string _last_update = "0";
      [DisplayName("�����")]
      [Description("����� ���������� ����������")]
      [Category("3. �������������")]
      [ReadOnly(true)]
      public string LAST_UPDATE
      {
         get { return _last_update; }
         set { _last_update = value; }
      }

      private bool _is_check = false;
      [DisplayName("Check")]
      [Description("Check Check Check")]
      [Category("4. Check")]
      [TypeConverter(typeof(CheckListBoxEditor))]
      [ReadOnly(false)]
      public bool IS_CHECK
      {
         get { return _is_check; }
         set { _is_check  = value; }
      }
   

 

   }
}
