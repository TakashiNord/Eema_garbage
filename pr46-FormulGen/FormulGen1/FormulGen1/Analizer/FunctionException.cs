namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Ошибка при построении выражения из функции
    /// </summary>
    class FunctionException : FormulaException
    {
        /// <summary>
        /// Имя функции
        /// </summary>
        private readonly FuncItem _function;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">причина ошибки</param>
        /// <param name="func">логический элемент функция</param>
        public FunctionException(string message, FuncItem func) :
            base(message, func.StartPosition, func.EndPosition)
        {
            _function = func;
        }

        /// <summary>
        /// Имя функции
        /// </summary>
        public FuncItem Function
        {
            get { return _function; }
        }
    }
}
