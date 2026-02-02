using System.Collections.Generic;


namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Выражение суммы
    /// </summary>
    class SumExpression : Expression
    {
        /// <summary>
        /// Функция возвращает выражение суммы или null, 
        /// если выражение суммы по указанному диапазону сформировать невозможно  
        /// </summary>
        /// <param name="elements">элементы</param>
        /// <param name="startIndex">индекс начала диапазона</param>
        /// <param name="endIndex">индекс элемента до которого производится анализ</param>
        /// <returns>выражение суммы</returns>
        public static SumExpression GetExpression(List<LogicItem> elements, int startIndex, int endIndex)
        {
            SumExpression sumExpression = null;
            int startSection = startIndex;
            // количество открытых скобок
            int brecketOpenCount = 0;
            // оператор стоящий перед выражением
            TypeDivider prevDivider = TypeDivider.Combine;
            
            // Ищем стоящие вне скобок знаки + или - 
            for (int i = startIndex; i < endIndex; i++)
            {
                DividerItem item = elements[i] as DividerItem;
                if (item != null)
                {
                    // Считаем открытые скобки
                    if (item.TypeDivider == TypeDivider.BracketOpen)
                        brecketOpenCount++;
                    if (item.TypeDivider == TypeDivider.BracketClose)
                    {
                        brecketOpenCount--;
                        // Лишняя закрывающая скобка
                        if (brecketOpenCount < 0)
                            throw new FormulaException(Errors.NoFindBrecketOpenForThis,  
                                                       item.StartPosition, item.EndPosition);
                    }

                    // Если нет ни одной открытой скобки и попадается знак + или -
                    if (brecketOpenCount == 0 &&
                        (item.TypeDivider == TypeDivider.Combine ||
                        item.TypeDivider == TypeDivider.Substract))
                    {
                        // Создаем выражение суммы
                        if (sumExpression == null)
                            sumExpression = new SumExpression();

                        
                        // Если первым идет знак
                        if (i != startIndex)
                        {
                            // Получаем дочернее выражение
                            Expression expression = GetMainExpression(sumExpression, elements, startSection, i);
                            expression.Negative = (prevDivider == TypeDivider.Substract);
                            sumExpression.Childs.Add(expression);
                        }
                        prevDivider = item.TypeDivider;
                        startSection = i + 1;
                    }
                }
            }
            // получаем последнее дочернее выражение 
            if (sumExpression != null)
            {
                Expression expression = GetMainExpression(sumExpression, elements, startSection, endIndex);
                expression.Negative = (prevDivider == TypeDivider.Substract);
                sumExpression.Childs.Add(expression);
            }

            return sumExpression;
        }

        public override string ToString()
        {
            string result = "(";
            foreach (Expression child in Childs)
            {
                result = result + (child.Negative ? "-" : "+");
                result = result + child;
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
            stateItems = new List<string>();
            string res = string.Empty;
            int count = Childs.Count;
            for (int i = 0; i < Childs.Count; i++)
            {
                Expression exp = Childs[i];

                string sign = exp.Negative ? " - " : " + ";

                if (i > 0 || exp.Negative)
                    res += sign;

                List<string> tmpStates;
                string expCodeC = exp.GetExpressionCodeC(result, out tmpStates);
                stateItems.AddRange(tmpStates);
                
                // Если выражение является суммой, разностью, умножением или делением,
                // то его необходимо заключить в скобки
                if ((exp as SumExpression) != null || (exp as MultiExpression) != null)
                    expCodeC = string.Format("({0})", expCodeC);

                res += expCodeC;
            }

            return res;
        }
    }
}
