using System;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// »сключение, возникающего при попытке анализа неверно составленной формулы
    /// </summary>
    public class FormulaException : Exception
    {
        /// <summary>
        /// ѕозици€ начала ошибочного блока в формуле
        /// </summary>
        private readonly int _startPosition;

        /// <summary>
        /// ѕозици€ конца ошибочного блока в формуле
        /// </summary>
        private readonly int _endPosition;

        /// <summary>
        ///  онструктор
        /// </summary>
        /// <param name="start">ѕозици€ начала ошибочного блока в формуле</param>
        /// <param name="end">ѕозици€ конца ошибочного блока в формуле</param>
        /// <param name="message">причина ошибки</param>
        public FormulaException(string message, int start, int end)
            : base(message)
        {
            _startPosition = start;
            _endPosition = end;
        }

        /// <summary>
        /// ѕозици€ начала ошибочного блока в формуле
        /// </summary>
        public int StartPosition
        {
            get { return _startPosition; }
        }

        /// <summary>
        /// ѕозици€ конца ошибочного блока в формуле
        /// </summary>
        public int EndPosition
        {
            get { return _endPosition; }
        }
    }
}
