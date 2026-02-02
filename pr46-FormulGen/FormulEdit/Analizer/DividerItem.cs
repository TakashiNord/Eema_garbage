using System;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Логический элемент - сепаратор
    /// </summary>
    class DividerItem : LogicItem
    {
        /// <summary>
        /// Возможные сепараторы
        /// </summary>
        public static string dividers = @" ;+-/*()" + ((char)9) + ((char)10) + ((char)13);
        
        /// <summary>
        /// Функция возвращает сепаратор или null, если элемент по указанной позиции сепаратором не является  
        /// </summary>
        /// <param name="formule">формула</param>
        /// <param name="startPosition">позиция для старта анлиза</param>
        /// <returns>Логический элемент - сепаратор</returns>
        public static DividerItem GetElement(string formule, int startPosition)
        {
            // Знаки переноса табуляции сразу интерпретируем как пробелы
            char ch = formule[startPosition];
            if ( ch == 9 || ch == 10 || ch == 13)
                return new DividerItem(" ", startPosition);

            // Смотрим является ли элемент сеператором
            string value = formule[startPosition].ToString();
            if (value.IndexOfAny(dividers.ToCharArray()) < 0)
                return null;
            // Возвращаем логический элемент - сепаратор
            return new DividerItem(value, startPosition);
        }

        /// <summary>
        /// Тип сепаратора
        /// </summary>
        readonly TypeDivider _typeDivider;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">строка</param>
        /// <param name="startPosition">позиция строкив формуле</param>
        protected DividerItem(string value, int startPosition)
            : base(value, startPosition)
        {
            // Определим тип сепаратора
            if (value == "+")
                _typeDivider = TypeDivider.Combine;
            else if (value == "-")
                _typeDivider = TypeDivider.Substract;
            else if (value == "*")
                _typeDivider = TypeDivider.Increase;
            else if (value == "/")
                _typeDivider = TypeDivider.Divide;
            else if (value == "(")
                _typeDivider = TypeDivider.BracketOpen;
            else if (value == ")")
                _typeDivider = TypeDivider.BracketClose;
            else if (value == ";")
                _typeDivider = TypeDivider.Semicolon;
            else if (value == " ")
                _typeDivider = TypeDivider.Space;
            else
                throw new Exception("Не удалось определить тип сепаратора");
        }

        /// <summary>
        /// Тип сепаратора
        /// </summary>
        public TypeDivider TypeDivider
        {
            get { return _typeDivider; }
        }
    }

    /// <summary>
    /// Типы сепаратора
    /// </summary>
    enum TypeDivider 
    { 
        /// <summary>
        /// знак умножения
        /// </summary>
        Increase, 
        
        /// <summary>
        /// Знак деления
        /// </summary>
        Divide, 
        
        /// <summary>
        /// Знак сложения
        /// </summary>
        Combine, 
        
        /// <summary>
        /// Знак вычитания
        /// </summary>
        Substract,
        
        /// <summary>
        /// Открывающая скобка
        /// </summary>
        BracketOpen,
        
        /// <summary>
        /// Закрывающая скобка
        /// </summary>
        BracketClose,
        
        /// <summary>
        /// Точка с запятой
        /// </summary>
        Semicolon,
        
        /// <summary>
        /// Пробел
        /// </summary>
        Space
    }

}
