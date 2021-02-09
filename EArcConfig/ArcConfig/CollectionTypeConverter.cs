using System;
using System.ComponentModel;
using System.Globalization;

namespace PropertyGridUtils
{
   /// <summary>
   /// TypeConverter для редактируемых коллекций
   /// </summary>
   class CollectionTypeConverter : TypeConverter
   {
      /// <summary>
      /// Только в строку
      /// </summary>
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
      {
         return destType == typeof (string);
      }

      /// <summary>
      /// И только так
      /// </summary>
      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
         object value, Type destType)
      {
         return "< Список... >";
      }
   }
}
