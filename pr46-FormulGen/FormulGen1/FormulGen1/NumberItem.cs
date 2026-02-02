
using System.Globalization;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Логический элемент - число
    /// </summary>
    class NumberItem : LogicItem
    {
        /// <summary>
        /// Символы, с которых может начинаться число
        /// </summary>
        const string startSymbol = @"0123456789";

        /// <summary>
        /// Функция возвращает число или null, если элемент по указанной позиции числом не является  
        /// </summary>
        /// <param name="formule">формула</param>
        /// <param name="startPosition">позиция для старта анлиза</param>
        /// <returns>Логический элемент - число</returns>
        public static NumberItem GetElement(string formule, int startPosition)
        {
            // Смотрим начинается ли с разрешенных символов
            string value = formule[startPosition].ToString();
            if (value.IndexOfAny(startSymbol.ToCharArray()) < 0)
                return null;

            // Получаем строку до следующего разделителя
            for (int i = startPosition + 1; i < formule.Length; i++)
            {
                string ch = formule[i].ToString();
                if (ch.IndexOfAny(DividerItem.dividers.ToCharArray()) < 0)
                {
                    value = value + ch;
                }
                else
                {
                    break;
                }
            }

            // Преобразуем строку в число
            double dValue;
            NumberFormatInfo info =new NumberFormatInfo();
            info.CurrencyDecimalSeparator = ".";
            if (!double.TryParse(value, NumberStyles.Float, info, out dValue))
            {
                // Если не получилось то число задано неверно
                throw new FormulaException(Errors.NumberFailed,
                                           startPosition, startPosition + value.Length);
            }

            // возвращаем логический элемент - число
            return new NumberItem(value, startPosition, dValue);
        }

        /// <summary>
        /// Значение числа (double)
        /// </summary>
        private readonly double _doubleValue;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">строка</param>
        /// <param name="startPosition">позиция строки в формуле</param>
        /// <param name="doubleValue">Значение числа (double)</param>
        protected NumberItem(string value, int startPosition, double doubleValue)
            : base(value, startPosition)
        {
            _doubleValue = doubleValue;
        }

        /// <summary>
        /// Значение числа (double)
        /// </summary>
        public double DoubleValue
        {
            get { return _doubleValue; }
        }

    }
}
