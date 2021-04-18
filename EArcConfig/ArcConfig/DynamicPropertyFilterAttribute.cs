using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace PropertyGridUtils
{
   /// <summary>
   /// ������� ��� ��������� ����������� ������������ �������
   /// </summary>
   [AttributeUsage(AttributeTargets.Property, Inherited = true)]
   class DynamicPropertyFilterAttribute : Attribute
   {
      string _propertyName;

      /// <summary>
      /// �������� ��������, �� �������� ����� �������� ���������  
      /// </summary>
      public string PropertyName
      {
         get { return _propertyName; }
      }

      string _showOn;

      /// <summary>
      /// �������� �������� �� �������� ������� ��������� 
      /// (����� �������, ���� ���������), ��� ������� ��������, �
      /// �������� �������� �������, ����� ������. 
      /// </summary>
      public string ShowOn
      {
         get { return _showOn; }
      }

      /// <summary>
      /// �����������  
      /// </summary>
      /// <param name="propName">�������� ��������, �� �������� ����� �������� ���������</param>
      /// <param name="value">�������� ��������, ����� �������, ���� ���������, ��� ������� ��������, �
      /// �������� �������� �������, ����� ������.</param>
      public DynamicPropertyFilterAttribute(string propName, string value)
      {
         _propertyName = propName;
         _showOn = value;
      }
   }

   /// <summary>
   /// ������� ����� ��� ��������, �������������� ������������ 
   /// ����������� ������� � PropertyGrid
   /// </summary>
   public class FilterablePropertyBase : ICustomTypeDescriptor
   {

      protected PropertyDescriptorCollection
      GetFilteredProperties(Attribute[] attributes)
      {
         PropertyDescriptorCollection pdc
         = TypeDescriptor.GetProperties(this, attributes, true);
         PropertyDescriptorCollection finalProps = 
            new PropertyDescriptorCollection(new PropertyDescriptor[0]);

         foreach (PropertyDescriptor pd in pdc)
         {
            bool include = false;
            bool Dynamic = false;
            foreach (Attribute a in pd.Attributes)
            {
               if (a is DynamicPropertyFilterAttribute)
               {
                  Dynamic = true;

                  DynamicPropertyFilterAttribute
                  dpf = (DynamicPropertyFilterAttribute)a;
                  PropertyDescriptor temp = pdc[dpf.PropertyName];

                  if (dpf.ShowOn.IndexOf(temp.GetValue(this).ToString()) > -1)
                  {
                     include = true;
                  }
               }
            }

            if (!Dynamic || include)
               finalProps.Add(pd);
         }

         return finalProps;
      }

      #region ICustomTypeDescriptor Members

      public TypeConverter GetConverter()
      {
         return TypeDescriptor.GetConverter(this, true);
      }

      public EventDescriptorCollection GetEvents(Attribute[] attributes)
      {
         return TypeDescriptor.GetEvents(this, attributes, true);
      }

      EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
      {
         return TypeDescriptor.GetEvents(this, true);
      }

      public string GetComponentName()
      {
         return TypeDescriptor.GetComponentName(this, true);
      }

      public object GetPropertyOwner(PropertyDescriptor pd)
      {
         return this;
      }

      public AttributeCollection GetAttributes()
      {
         return TypeDescriptor.GetAttributes(this, true);
      }

      public PropertyDescriptorCollection GetProperties(
         Attribute[] attributes)
      {
         return GetFilteredProperties(attributes);
      }

      PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
      {
         return GetFilteredProperties(new Attribute[0]);
      }

      public object GetEditor(Type editorBaseType)
      {
         return TypeDescriptor.GetEditor(this, editorBaseType, true);
      }

      public PropertyDescriptor GetDefaultProperty()
      {
         return TypeDescriptor.GetDefaultProperty(this, true);
      }

      public EventDescriptor GetDefaultEvent()
      {
         return TypeDescriptor.GetDefaultEvent(this, true);
      }

      public string GetClassName()
      {
         return TypeDescriptor.GetClassName(this, true);
      }

      #endregion

   }

}
