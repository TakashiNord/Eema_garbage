using System;
using System.ComponentModel;
using System.Globalization;

namespace PropertyGridUtils
{
   /// <summary>
   /// TypeConverter ��� bool
   /// </summary>
   class BooleanTypeConverter : BooleanConverter
   {
      public override object ConvertTo(ITypeDescriptorContext context, 
         CultureInfo culture,
         object value, 
         Type destType)
      {
         return (bool)value ? 
            "����" : "���";
      }

      public override object ConvertFrom(ITypeDescriptorContext context, 
         CultureInfo culture,
         object value)
      {
         return (string)value == "����";
      }
   }
}
