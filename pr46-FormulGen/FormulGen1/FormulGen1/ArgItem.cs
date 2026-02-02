
namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Логический элемент - аргумент
    /// </summary>
    class ArgItem : LogicItem
    {
        /// <summary>
        /// Префикс с которого начинается аргумент
        /// </summary>
        public const string argPrefix = @"arg";

        /// <summary>
        /// Функция возвращает аргумент или null, если элемент по указанной позиции аргументом не является  
        /// </summary>
        /// <param name="formule">формула</param>
        /// <param name="startIndex">позиция для старта анлиза</param>
        /// <returns>Логический элемент - аргумент</returns>
        public static ArgItem GetElement(string formule, int startIndex)
        {
            // Смотрим начинается ли элемент с префикса
            int pos = formule.IndexOf(argPrefix, startIndex);
            if (pos != startIndex)
                return null;

            string value = "";
            // Получаем строку до следующего разделителя
            for (int i = startIndex + argPrefix.Length; i < formule.Length; i++)
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
            // Пробуем конвертить строку в число, если получается, 
            // то элемент отвечает формату argX, и является аргументом
            int temp;
            if (!int.TryParse(value, out temp))
                return null;

            // Возвращаем логический элемент - аргумент
            return new ArgItem(argPrefix + value, startIndex);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">строка</param>
        /// <param name="startIndex">позиция строки в формуле</param>
        protected ArgItem(string value, int startIndex)
            : base(value, startIndex)
        {
        }

        /// <summary>
        /// Имя аргумента
        /// </summary>
        public string Name
        {
            get { return Value; }
        }
    }
}
