using System;
using System.Collections.Generic;
using System.Text;

namespace RSDU.Components.FormulEdit.Analizer
{
    class CodeCResult
    {
        /// <summary>
        /// Список параметров
        /// </summary>
        private readonly List<ParamItem> _params;

        /// <summary>
        /// Элементы Си кода формулы
        /// </summary>
        private readonly List<ElementCCode> _elements = new List<ElementCCode>();
        
        /// <summary>
        /// Конструктор по значению
        /// </summary>
        /// <param name="pars">Список параметров</param>
        public CodeCResult(List<ParamItem> pars)
        {
            _params = pars;
        }

        /// <summary>
        /// Список параметров
        /// </summary>
        public List<ParamItem> Params
        {
            get { return _params; }
        }

        /// <summary>
        /// Элементы Си кода формулы
        /// </summary>
        public List<ElementCCode> Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// Добавление функции для определения в локальной переменной
        /// </summary>
        /// <param name="funcCodeC">Си код функции</param>
        /// <returns>имя локальной переменной</returns>
        public string AddFunctionToLocal(string funcCodeC)
        {
            string localName = "func" + (GetCountElement(ElementCCodeType.Func) + 1);
            ElementCCode element = new ElementCCode(ElementCCodeType.Func, localName, funcCodeC);
            _elements.Add(element);

            return localName;
        }
        
        /// <summary>
        /// Добавление выражения знаменателя деления для определения локальной переменной и проверки на ноль
        /// </summary>
        /// <param name="denomCodeC">Си код выражения знаменателя</param>
        /// <returns>имя локальной переменной</returns>
        public string AddDivideDenomExpression(string denomCodeC)
        {
            string localName = "var" + (GetCountElement(ElementCCodeType.Var) + 1);
            ElementCCode element = new ElementCCode(ElementCCodeType.Var, localName, denomCodeC);
            _elements.Add(element);

            return localName;
        }

        /// <summary>
        /// Функция возвращает количество элементов Си кода заданного типа
        /// </summary>
        /// <param name="type">тип элемента</param>
        /// <returns>количество элементов</returns>
        public int GetCountElement(ElementCCodeType type)
        {
            int result = 0;
            foreach (ElementCCode element in _elements)
            {
                if (element.ElementType == type)
                    result++;
            }
            return result;
        }
    }
}
