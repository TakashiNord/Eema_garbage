using System;
using System.Collections.Generic;
using RSDU.Domain;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Базовый класс для всех выражений
    /// </summary>
    abstract class Expression : IEnumerable<Expression>
    {
        /// <summary>
        /// Возвращает выражение по указанному дипазону в списке элементов
        /// генерирует исключение FormulaException если невозможно построить выражение
        /// </summary>
        /// <param name="parent">родительское выражение</param>
        /// <param name="elements">список элементов</param>
        /// <param name="startIndex">индекс начала диапазона</param>
        /// <param name="endIndex">индекс элемента до которого производится анализ</param>
        /// <returns>выражение</returns>
        public static Expression GetMainExpression(Expression parent, List<LogicItem> elements, int startIndex, int endIndex)
        {
            // если равны startIndex и endIndex, то выражение пустое 
            if (startIndex == endIndex)
            {
                if (elements.Count == 0)
                {
                    throw new FormulaException(Errors.ExpressionMissing, 0, 0);
                }

                if (elements.Count == startIndex)
                {
                    throw new FormulaException(Errors.ExpressionNeedAfter,
                                               elements[startIndex - 1].StartPosition,
                                               elements[startIndex - 1].StartPosition + 1);
                }
                else
                {
                    if (parent is FuncExpression)
                    {
                        // Функция с пустым набором аргументов
                        return null;
                    }
                    else
                    {
                        throw new FormulaException(Errors.ExpressionNeedBefore,
                                                   elements[startIndex].StartPosition,
                                                   elements[startIndex].StartPosition + 1);
                    }
                }
            }

            // выражение
            Expression expression;

            // пробуем построить выражение суммы
            expression = SumExpression.GetExpression(elements, startIndex, endIndex);

            // пробуем построить выражение произведения
            if (expression == null)
                expression = MultiExpression.GetExpression(elements, startIndex, endIndex);
            
            // Освобождаемся от лишних скобок
            if (expression == null)
            {
                // Если первый элемент - открывающая скобка 
                DividerItem item = elements[startIndex] as DividerItem;
                if (item != null && item.TypeDivider == TypeDivider.BracketOpen)
                {
                    // Ищем закрывающую скобку
                    int brecketOpenCount = 1;
                    for (int i = startIndex + 1; i < endIndex; i++)
                    {
                        item = elements[i] as DividerItem;
                        if (item != null)
                        {
                            if (item.TypeDivider == TypeDivider.BracketOpen)
                                brecketOpenCount++;
                            if (item.TypeDivider == TypeDivider.BracketClose)
                                brecketOpenCount--;

                            if (brecketOpenCount == 0)
                            {
                                // Получаем выражение из диапазона в скобках
                                expression = GetMainExpression(parent, elements, startIndex + 1, i);
                                // Если скобки покрывают не весь диапазон
                                // то значит что то не так - конкретно после скобок должен был быть оператор
                                if (i != endIndex - 1)
                                    throw new FormulaException(Errors.OperatorNeed,
                                                               elements[i].EndPosition, elements[i].EndPosition + 1);
                            }
                        }
                    }
                    // Если закрывающей скобки так и не нашли :(
                    if (expression == null)
                        throw new FormulaException(Errors.NoFindBrecketCloseForThis,
                                                   elements[startIndex].StartPosition, elements[startIndex].StartPosition + 1);
                }
            }

            // Пробуем получить выражение функции
            if (expression == null)
            {
                expression = FuncExpression.GetExpression(elements, startIndex, endIndex);
            }

            // Пробуем получить выражение логического элемента
            if (expression == null)
            {
                expression = ItemExpression.GetExpression(elements, startIndex, endIndex);
            }

            // Если выражение сформировано так и не было - значит алгоритм содержит ошибку
            if (expression == null)
                throw new Exception("Не удалось сформировать выражение");

            // Установим родителя
            expression.Parent = parent;

            return expression;
        }
        
        /// <summary>
        /// Родительское выражение
        /// </summary>
        private Expression _parent;

        /// <summary>
        /// Дочерние выражения
        /// </summary>
        readonly List<Expression> _childs = new List<Expression>();

        /// <summary>
        /// Флаг отрицания 
        /// (если выражение входит в состав выражения суммы, 
        /// флаг говорит нужно ли данный элемент вычитать из общей суммы и суммировать с ней)
        /// </summary>
        bool _negative;

        /// <summary>
        /// Флаг инверсии 
        /// (если выражение входит в состав выражения произведения, 
        /// флаг говорит нужно ли на данный элемент делить общее произведение или умножать с на него)
        /// </summary>
        bool _return;

        /// <summary>
        /// Родительское выражение
        /// </summary>
        public Expression Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Дочерние выражения
        /// </summary>
        public List<Expression> Childs
        {
            get { return _childs; }
        }

        /// <summary>
        /// Флаг отрицания 
        /// (если выражение входит в состав выражения суммы, 
        /// флаг говорит нужно ли данный элемент вычитать из общей суммы и суммировать с ней)
        /// </summary>
        public bool Negative
        {
            get { return _negative; }
            set { _negative = value; }
        }

        /// <summary>
        /// Флаг инверсии 
        /// Если выражение входит в состав выражения произведения, 
        /// флаг говорит нужно ли на данный элемент делить общее произведение или умножать на него
        /// </summary>
        public bool Return
        {
            get { return _return; }
            set { _return = value; }
        }

        /// <summary>
        /// Получение Си кода для выражения
        /// </summary>
        /// <returns></returns>
        public abstract string GetExpressionCodeC(CodeCResult result, out List<string> stateItems);

        /// <summary>
        /// Генерация C кода
        /// </summary>
        /// <param name="pars">Список параметров</param>
        /// <param name="funcs">Список функций</param>
        /// <param name="idChannel">Идентификатор канала измерения</param>
        public string GetCodeC(List<ParamItem> pars, List<FunctionTemplate> funcs, int idChannel)
        {
            if (pars == null)
                throw new ArgumentNullException("pars", "В функцию GetCodeC передана нулевая ссылка на список параметров");

            // Создаем результат построения Си кода. С помощью него в дальнейшем получаем какие
            // аргументы необходимо вынести и\или проверить на ноль
            CodeCResult result = new CodeCResult(pars);

            // Получаем Си код основного вычисляемого выражения
            List<string> states; 
            string expCodeC = GetExpressionCodeC(result, out states);

            string codeC = "\r\n";

            // Подключаем заголовочные файлы
            codeC += "#include <formule.h>\r\n";
            codeC += "#include <formlib.h>\r\n\r\n";

            // Добавляем заголовок главной функции
            codeC += string.Format(
                "void Formule{0:0000000}(REG_BASE ***rba, unsigned int current_index)\r\n{{\r\n", idChannel);

            // Сбрасываем флаги возвращаемого результата
            codeC += 
                "    (*(*rba)[0]).rv[current_index].ft &= ~(ELRF_VALUENOCORRECT | ELRF_SRCNOPRESENT);\r\n\r\n";
            
            
            // обрабатываем элементы Си кода
            // Вставляем определения локальных перменных
            foreach (ElementCCode element in result.Elements)
            {
                if (element.ElementType == ElementCCodeType.Func)
                {
                    // Выносим вычисление формулы в локальную переменную
                    string func = string.Format(
                        "    REG_VAL {0} = {1};\r\n", element.Name, element.Code);

                    codeC += func;

                    // Определяем статус значения функции
                    if (element.States.Count > 0)
                    {
                        // Массив аргументов которые уже добавлены, чтобы не добавлять два раза
                        List<string> useStr = new List<string>();
                        string funcS = string.Format("    {0}.ft |= ", element.Name);
                        foreach (string str in element.States)
                        {
                            if (!useStr.Contains(str))
                            {
                                useStr.Add(str);
                                funcS += str + " | ";
                            }
                        }
                        funcS = funcS.Remove(funcS.Length - 3, 3);
                        funcS += ";\r\n";

                        codeC += funcS;
                    }
                }
                else if (element.ElementType == ElementCCodeType.Var)
                {
                    // Выносим выражение знаменателя деления в локальную переменную и проверяем его на ноль
                    string var = string.Format("    double {0} = {1};\r\n", element.Name, element.Code);

                    // Проверим знаменатель деления на ноль
                    string denom = string.Format(
                        "    if (ISZERO({0}))\r\n" +
                        "    {{\r\n" +
                        "        (*(*rba)[0]).rv[current_index].ft |= ELRF_VALUENOCORRECT;\r\n" +
                        "        (*(*rba)[0]).rv[current_index].vl = 0.0;\r\n" +
                        "        return;\r\n" +
                        "    }}\r\n", element.Name);

                    codeC += var + denom;
                }
                else
                    throw new Exception("Unknown value of ElementCCodeType");
                
                codeC += "\r\n";
            }

            // Добавляем код основного вычисляемого выражения
            string main = string.Format("    (*(*rba)[0]).rv[current_index].vl = {0};\r\n\r\n", expCodeC);

            codeC += main;

            // Устанавливаем статус конечного значения
            if (states.Count > 0)
            {
                // Массив аргументов которые уже добавлены, чтобы не добавлять два раза
                List<string> useStr = new List<string>();
                string mainsS = string.Format("    (*(*rba)[0]).rv[current_index].ft |= ");
                foreach (string str in states)
                {
                    if (!useStr.Contains(str))
                    {
                        useStr.Add(str);
                        mainsS += str + " | ";
                    }
                }
                mainsS = mainsS.Remove(mainsS.Length - 3, 3);
                mainsS += ";\r\n";

                codeC += mainsS;
            }

            codeC += "}\r\n";

            return codeC;
        }

        #region IEnumerable<Expression> Members

        /// <summary>
        /// Возвращает итератор выражений
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Expression> GetEnumerator()
        {
            return new ExpressionIterator(this);
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Возвращает итератор выражений
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new ExpressionIterator(this);
        }

        #endregion

    }
}
