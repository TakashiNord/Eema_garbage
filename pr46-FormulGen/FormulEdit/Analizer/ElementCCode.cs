using System;
using System.Collections.Generic;
using System.Text;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Элемент (составная часть) Си-кода формулы
    /// </summary>
    class ElementCCode
    {
        private readonly ElementCCodeType _elementType;
        private readonly string _name;
        private readonly string _code;
        private readonly List<string> _states;

        public ElementCCode(ElementCCodeType elementType, string name, string code)
        {
            _elementType = elementType;
            _name = name;
            _code = code;
            _states = new List<string>();
        }
        
        /// <summary>
        /// Тип элемента формулы
        /// </summary>
        public ElementCCodeType ElementType
        {
            get { return _elementType; }
        }

        /// <summary>
        /// Имя элемента
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Код элемента
        /// </summary>
        public string Code
        {
            get { return _code; }
        }

        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Статусы которые нужно учесть, для вычисления статуса самого елемента
        /// </summary>
        public List<string> States
        {
            get { return _states; }
        }
    }

    /// <summary>
    /// Тип элемента Си-кода формулы
    /// </summary>
    enum ElementCCodeType
    {
        /// <summary>
        /// Функция вынесенная в область локальных переменных
        /// </summary>
        Func,

        /// <summary>
        /// Выражение находящееся в знаменателе деления и требующие проверку на ноль
        /// </summary>
        Var
    }
}
