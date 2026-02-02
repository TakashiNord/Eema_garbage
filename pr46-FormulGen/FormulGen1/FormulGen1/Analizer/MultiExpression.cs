using System.Collections.Generic;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Выражение произведения
    /// </summary>
    class MultiExpression : Expression
    {
        /// <summary>
        /// Функция возвращает выражение произведения или null, 
        /// если выражение произведения по указанному диапазону сформировать невозможно  
        /// </summary>
        /// <param name="elements">элементы</param>
        /// <param name="startIndex">индекс начала диапазона</param>
        /// <param name="endIndex">индекс элемента до которого производится анализ</param>
        /// <returns>выражение произведения</returns>
        public static MultiExpression GetExpression(List<LogicItem> elements, int startIndex, int endIndex)
        {
            MultiExpression multiExpression = null;
            int startSection = startIndex;
            // количество открытых скобок
            int brecketOpenCount = 0;
            // оператор стоящий перед выражением
            TypeDivider prevDivider = TypeDivider.Increase;

            // ищем стоящие вне скобок знаки * или /
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

                    // Если нет ни одной открытой скобки и попадается знак * или /
                    if (brecketOpenCount == 0 &&
                        (item.TypeDivider == TypeDivider.Increase ||
                        item.TypeDivider == TypeDivider.Divide))
                    {
                        // Создаем выражение произведения
                        if (multiExpression == null)
                            multiExpression = new MultiExpression();

                        // Получаем дочернее выражение
                        Expression expression = GetMainExpression(multiExpression, elements, startSection, i);
                        expression.Return = (prevDivider == TypeDivider.Divide);
                        multiExpression.Childs.Add(expression);
                        prevDivider = item.TypeDivider;
                        startSection = i + 1;
                    }
                }
            }
            // получаем последнее дочернее выражение 
            if (multiExpression != null)
            {
                Expression expression = GetMainExpression(multiExpression, elements, startSection, endIndex);
                expression.Return = (prevDivider == TypeDivider.Divide);
                multiExpression.Childs.Add(expression);
            }

            return multiExpression;
        }

        public override string ToString()
        {
            string result = "(";
            foreach (Expression child in Childs)
            {
                if (child.Return)
                    result = result + "*(1/" + child + ")";
                result = result + "*" + child;
            }
            result = result + ")";
            
            return result;
        }

        /// <summary>
        /// Получение Си кода для выражения
        /// </summary>
        /// <param name="result">Результат получения Си кода</param>
        /// <param name="stateItems">строки статусов для формирования общего статуса</param>
        /// <returns></returns>
        public override string GetExpressionCodeC(CodeCResult result, out List<string> stateItems)
        {
            stateItems = new List<string>();

            string res = string.Empty;
            int count = Childs.Count;
            for (int i = 0; i < Childs.Count; i++)
            {
                Expression exp = Childs[i];

                string sign = exp.Return ? " / " : " * ";

                if (i > 0)
                    res += sign;

                List<string> tmpStates;
                string expCodeC = exp.GetExpressionCodeC(result, out tmpStates);
                stateItems.AddRange(tmpStates);

                if (i > 0 && exp.Return)
                {
                    expCodeC = result.AddDivideDenomExpression(expCodeC);
                }
                else if ((exp as SumExpression) != null || (exp as MultiExpression) != null)
                {
                    // Если выражение является суммой, разностью, умножением или делением,
                    // то необходимо ее заключить в скобки
                    expCodeC = string.Format("({0})", expCodeC);
                }

                res += expCodeC;
            }

            

            return res;
        }
    }
}
