using System.Collections.Generic;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Выражение функции
    /// Childs для выражения функции содержит аргументы функции
    /// </summary>
    class FuncExpression : Expression
    {
        /// <summary>
        /// Функция возвращает выражение функции или null, 
        /// если выражение функции по указанному диапазону сформировать невозможно  
        /// </summary>
        /// <param name="elements">элементы</param>
        /// <param name="startIndex">индекс начала диапазона</param>
        /// <param name="endIndex">индекс элемента до которого производится анализ</param>
        /// <returns>выражение функции</returns>
        public static FuncExpression GetExpression(List<LogicItem> elements, int startIndex, int endIndex)
        {
            // Получим логический элемент - функция
            FuncItem fItem = elements[startIndex] as FuncItem;
            if (fItem == null)
                return null;
            
            FuncExpression fExpression = new FuncExpression(fItem);
            DividerItem item = null;
            
            if (endIndex > startIndex + 1)
            {
                item = elements[startIndex + 1] as DividerItem;
            }

            // Если следующий после функции элемент не является открывающей скобкой
            // генерируем исключение
            if (item == null || item.TypeDivider != TypeDivider.BracketOpen)
            {
                throw new FunctionException(Errors.ArgumentsNeed,
                                            fItem);
            }

            int startSection = startIndex + 2;
            // Ищем разделители аргументов или закрывающую скобку
            int brecketOpenCount = 0;
            for (int i = startIndex + 2; i < endIndex; i++)
            {
                DividerItem dItem = elements[i] as DividerItem;
                if (dItem != null)
                {
                    // Если внутри функции есть еще скобки
                    // то не ищем разделители в них
                    if (dItem.TypeDivider == TypeDivider.BracketOpen)
                    {
                        brecketOpenCount++;
                    }

                    if (brecketOpenCount > 0 &&
                        dItem.TypeDivider == TypeDivider.BracketClose)
                    {
                        brecketOpenCount--;
                    }
                    else if (brecketOpenCount == 0 &&
                        (dItem.TypeDivider == TypeDivider.Semicolon ||
                        dItem.TypeDivider == TypeDivider.BracketClose))
                    {
                        // После каждого разделителя получаем выражение из аргумента
                        Expression exp = GetMainExpression(fExpression, elements, startSection, i);
                        if (exp != null)
                        {
                            fExpression.Childs.Add(exp);
                        }
                        // Если после закрывающей скобки что то есть - это неправильно
                        // полсе скобки должен быть оператор
                        if (dItem.TypeDivider == TypeDivider.BracketClose
                            && i != endIndex - 1)
                            throw new FormulaException(Errors.OperatorNeed,
                                                       elements[i].EndPosition, elements[i].EndPosition + 1);

                        startSection = i + 1;
                    }
                }
            }

            return fExpression;
        }
        
        
        /// <summary>
        /// Функциия
        /// </summary>
        private FuncItem _function;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="function">функция</param>
        public FuncExpression(FuncItem function)
        {
            _function = function;
        }

        /// <summary>
        /// Функциия
        /// </summary>
        public FuncItem Function
        {
            get { return _function; }
            set { _function = value; }
        }

        public override string ToString()
        {
            string result = _function.Value + "(";
            foreach (Expression child in Childs)
            {
                result = result + ";" + child;
            }
            result = result + ")";

            return result;
        }

        /// <summary>
        /// Получение Си кода для выражения
        /// </summary>
        /// <param name="result">Результат получения Си кода</param>
        /// <param name="stateItems">строки для расчета статусов</param>
        /// <returns></returns>
        public override string GetExpressionCodeC(CodeCResult result, out List<string> stateItems)
        {
            string res = Function.Name + "(";

            int count = Childs.Count;

            List<string> myStateItems = new List<string>();
            for (int i = 0; i < count; i++)
            {
                List<string> tmpStates; 
                res += Childs[i].GetExpressionCodeC(result, out tmpStates);
                if (i < (count - 1))
                    res += ", ";

                myStateItems.AddRange(tmpStates);
            }
            res += ")";

            // Выносим функцию в заголовок Си кода с получением ее значения в локальную переменную
            // и возвращаем имя этой локальной переменной
            string strRes = result.AddFunctionToLocal(res) + ".vl";

            // в качестве статусов которые нужно учитывать возвращаем статус функции
            stateItems = new List<string>();
            stateItems.Add(result.Elements[result.Elements.Count - 1].Name + ".ft");

            // К елементу функции добавляем статусы аргументов входящих в функцию
            result.Elements[result.Elements.Count - 1].States.AddRange(myStateItems);

            return strRes;
        }
    }
}
