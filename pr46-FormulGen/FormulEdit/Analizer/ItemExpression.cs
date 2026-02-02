using System;
using System.Collections.Generic;


namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Выражение логического элемента
    /// конечный элемент иерархии
    /// </summary>
    class ItemExpression : Expression
    {
        /// <summary>
        /// Функция возвращает выражение логического элемента или null, 
        /// если выражение логического элемента по указанному диапазону сформировать невозможно  
        /// </summary>
        /// <param name="elements">элементы</param>
        /// <param name="startIndex">индекс начала диапазона</param>
        /// <param name="endIndex">индекс элемента до которого производится анализ</param>
        /// <returns>выражение логического элемента</returns>
        public static ItemExpression GetExpression(List<LogicItem> elements, int startIndex, int endIndex)
        {
            LogicItem item = elements[startIndex] as ParamItem;
            if (item == null)
            {
                item = elements[startIndex] as NumberItem;
            }

            if (item == null)
            {
                item = elements[startIndex] as ArgItem;
            }
            
            if (item == null)
                return null;

            ItemExpression iExpression = new ItemExpression(item);

            // Если после элемент что то есть - это неправильно 
            // После должен быть оператор
            if (startIndex != endIndex - 1)
                throw new FormulaException(Errors.OperatorNeed,
                                           item.EndPosition, item.EndPosition + 1);

            return iExpression;
        }        
        
        /// <summary>
        /// Логический элемент
        /// </summary>
        private LogicItem _item;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="item">Логический элемент</param>
        public ItemExpression(LogicItem item)
        {
            _item = item;
        }

        /// <summary>
        /// Логический элемент
        /// </summary>
        public LogicItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public override string ToString()
        {
            return Item.Value;
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

            ParamItem param = Item as ParamItem;
            if (param != null)
            {
                int index = result.Params.IndexOf(param) + 1;
                if (index <= 0)
                    throw new Exception("Параметр формулы не найден в списке параметров");
                if (param.UseAsArgument)
                {
                    res += string.Format("(*rba)[{0}]", index);
                }
                else
                {
                    res += string.Format("(*(*rba)[{0}]).rv[current_index].vl", index);
                    // Добавляем параметр в статусы, которые следует учесть при формировании общего статуса
                    string strStatusItem = string.Format("(*(*rba)[{0}]).rv[current_index].ft", index);
                    stateItems.Add(strStatusItem);
                }
            }
            else
            {
                string value = Item.Value;
                if (value.IndexOf('.') < 0)
                    value += ".0";
                res += value;
            }

            return res;
        }
    }
}
