using System;
using System.ComponentModel;
using System.Globalization;

namespace PropertyGridUtils
{
   /// <summary>
   /// TypeConverter ��� ������������� ���������
   /// </summary>
   class CollectionTypeConverter : TypeConverter
   {
      /// <summary>
      /// ������ � ������
      /// </summary>
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
      {
         return destType == typeof (string);
      }

      /// <summary>
      /// � ������ ���
      /// </summary>
      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
         object value, Type destType)
      {
         return "< ������... >";
      }
   }
}
