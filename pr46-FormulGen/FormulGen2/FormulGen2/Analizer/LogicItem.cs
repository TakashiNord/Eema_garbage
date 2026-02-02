
namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    ///  Бызовый класс для всех логических элементов присутствующих в формуле
    /// </summary>
    abstract class LogicItem
    {
        /// <summary>
        /// Возвращает логический элемент по позиции в формуле
        /// генерирует исключение FormulaException если не может иденцифировать элемент
        /// </summary>
        /// <param name="formula">формула</param>
        /// <param name="startPosition">позиция для старта анализа</param>
        /// <returns></returns>
        public static LogicItem GetItem(string formula, int startPosition)
        {
            // Пробуем получить элемент по позиции в формуле
            LogicItem elem;
            // проверяем является ли элемент сепаратором
            elem = DividerItem.GetElement(formula, startPosition);
            // Проверяем является ли элемент числом
            if (elem == null)
                elem = NumberItem.GetElement(formula, startPosition);
            // Проверяем является ли элемент параметром
            if (elem == null)
                elem = ParamItem.GetElement(formula, startPosition);
            // Проверяем является ли элемент аргументом
            if (elem == null)
                elem = ArgItem.GetElement(formula, startPosition);
            // Проверяем является ли элемент именем функции
            if (elem == null)
                elem = FuncItem.GetElement(formula, startPosition);

            // Если элемент идентифицировать не удалось, то генерируем ошибку
            if (elem == null)
                throw new FormulaException(Errors.UnknownExpression, 
                    startPosition, startPosition + 1);

            return elem;
        }
        
        /// <summary>
        /// Позиция элемента в формуле
        /// </summary>
        readonly int _startPosition;

        /// <summary>
        /// Строка элемента
        /// </summary>
        readonly string _value;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">Строка элемента</param>
        /// <param name="startPosition">Позиция элемента в формуле</param>
        public LogicItem(string value, int startPosition)
        {
            _value = value;
            _startPosition = startPosition;
        }
        
        /// <summary>
        /// Позиция элемента в формуле
        /// </summary>
        public int StartPosition
        {
            get { return _startPosition; }
        }

        /// <summary>
        /// Позиция конца элемента в формуле
        /// </summary>
        public int EndPosition
        {
            get { return _startPosition + _value.Length; }
        }

        /// <summary>
        /// Строка элемента
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
