using System;
using System.ComponentModel;
using System.Globalization;

namespace PropertyGridUtils
{
   /// <summary>
   /// TypeConverter для bool
   /// </summary>
   class BooleanTypeConverter : BooleanConverter
   {
      public override object ConvertTo(ITypeDescriptorContext context, 
         CultureInfo culture,
         object value, 
         Type destType)
      {
         return (bool)value ? 
            "Есть" : "Нет";
      }

      public override object ConvertFrom(ITypeDescriptorContext context, 
         CultureInfo culture,
         object value)
      {
         return (string)value == "Есть";
      }
   }
}
