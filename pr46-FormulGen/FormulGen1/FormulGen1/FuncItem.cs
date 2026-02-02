
namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Логический элемент - имя функции
    /// </summary>
    class FuncItem : LogicItem
    {
        /// <summary>
        /// Символы, с которых может начинаться имя функции
        /// </summary>
        const string startSymbol = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

        /// <summary>
        /// Функция возвращает имя функции или null, если элемент по указанной позиции именем функции не является  
        /// </summary>
        /// <param name="formule">формула</param>
        /// <param name="startPosition">позиция для старта анлиза</param>
        /// <returns>Логический элемент - имя функции</returns>
        public static FuncItem GetElement(string formule, int startPosition)
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

            // возвращаем логический элемент - имя функции
            return new FuncItem(value, startPosition);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">строка</param>
        /// <param name="startPosition">позиция строки в формуле</param>
        protected FuncItem(string value, int startPosition)
            : base(value, startPosition)
        {
        }

        /// <summary>
        /// Имя функции
        /// </summary>
        public string Name
        {
            get { return Value; }
        }
    }
}
