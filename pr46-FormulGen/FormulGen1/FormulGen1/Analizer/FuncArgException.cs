
namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Исключения возникающее при несоответствие типа аргумента функции
    /// в диаграмме классов слабо с выделением ошибочных областей 
    /// - по сути только для этого и нужна эта заплатка
    /// </summary>
    class FuncArgException : FunctionException
    {
        /// <summary>
        /// Номер ошибочного аргумента
        /// </summary>
        private readonly int _argIndex;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">причина ошибки</param>
        /// <param name="func">функция</param>
        /// <param name="argIndex">Номер ошибочного аргумента</param>
        public FuncArgException(string message, FuncItem func, int argIndex) 
            : base(message, func)
        {
            _argIndex = argIndex;
        }

        /// <summary>
        /// Номер ошибочного аргумента
        /// </summary>
        public int ArgIndex
        {
            get { return _argIndex; }
        }
    }
}
