using System.Collections.Generic;


namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Итератор выражений
    /// </summary>
    class ExpressionIterator : IEnumerator<Expression>
    {
        /// <summary>
        /// Выражение с которого начинается обход
        /// </summary>
        private readonly Expression _start;
        
        /// <summary>
        /// Текущее выражение
        /// </summary>
        private Expression _current;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="exp">выражение которойе нужно обойти</param>
        public ExpressionIterator(Expression exp)
        {
            _start = exp;
            _current = null;
        }
        
        /// <summary>
        /// Текущее выражение
        /// </summary>
        Expression IEnumerator<Expression>.Current
        {
            get { return _current; }
        }

        /// <summary>
        /// функция вызываемая при удалении класса
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// помещает в Current следующий элемент
        /// </summary>
        /// <returns>ложь если обход закончен</returns>
        public bool MoveNext()
        {
            // Если самое начало то первым элементом мы возвращаем родителя
            if (_current == null)
            {
                _current = _start;
                return true;
            }
            
            // Сначала ищем следующий элемент по иерархии вних
            if (_current.Childs.Count > 0)
            {
                _current = _current.Childs[0];
                return true;
            }

            return MoveNextUp();
        }

        /// <summary>
        /// Получет следующий элемент по иерархии вверх
        /// </summary>
        /// <returns></returns>
        private bool MoveNextUp()
        {
            // Ищем следующий по иерархии вверх
            if (_current == _start)
                return false;
            else
            {
                int index = _current.Parent.Childs.IndexOf(_current);
                int cnt = _current.Parent.Childs.Count;
                // Если у отца есть дите старше текущего получаем дите
                if (cnt > index + 1)
                {
                    _current = _current.Parent.Childs[index + 1];
                    return true;
                }
                else
                {
                    // если нет то получим дите от дедушки
                    _current = _current.Parent;
                    return MoveNextUp();
                }
            }
        }

        /// <summary>
        /// Возвращает итератор в стартовое состояние
        /// </summary>
        public void Reset()
        {
            _current = null;
        }

        /// <summary>
        /// текущиее выражение
        /// </summary>
        public object Current
        {
            get { return _current; }
        }
    }
}
