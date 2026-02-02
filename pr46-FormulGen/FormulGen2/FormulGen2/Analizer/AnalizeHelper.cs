using System;
using System.Collections.Generic;
//using RSDU.Database.Mappers;
//using RSDU.DataRegistry;
//using RSDU.DataRegistry.Identity;
//using RSDU.Domain;
//using RSDU.Domain.Interfaces;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Класс анализатор для формул
    /// </summary>
    public class AnalizeHelper
    {
        /// <summary>
        /// функции имеющиеся в БД
        /// </summary>
        readonly Dictionary<string, FunctionTemplate> _permitFunc;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="permitParams">параметры имеющиеся в БД</param>
        /// <param name="permitFunc">функции имеющиеся в БД</param>
        public AnalizeHelper(Dictionary<string, FunctionTemplate> permitFunc)
        {
            _permitFunc = permitFunc;
        }

        public static string GetParameterFormat(int id)
        {
            return ParamItem.GetParamName(id);
        }


        #region Проверка формулы
        
        /// <summary>
        /// Функция анализа формулы
        /// </summary>
        /// <param name="formul">формула</param>
        /// <returns>результат анализа</returns>
        public IResultValue Analize(DataRegistry.DataRegistry comRegistry, string formul, int idTable)
        {
            Expression expression;
            List<ParamItem> paramList = null;
            List<IMeasure> measList = null;
            try
            {
                try
                {
                    // Получаем лист параметров
                    paramList = GetParamList(formul);

                    // проверяем на предмет присутствия в БД
                    List<int> ids = new List<int>();
                    foreach (ParamItem paramItem in paramList)
                    {
                        if (!ids.Contains(paramItem.Id))
                            ids.Add(paramItem.Id);
                    }
                    SysTable table = comRegistry.Map.Find<SysTable>(new ObjectIdentity(idTable));
                    comRegistry.Map.MiddleLoad(table);

                    ElRegMeasureMapper elMapper = (ElRegMeasureMapper)comRegistry.Map.Mapper<ElRegMeasure>();
                    PhRegMeasureMapper phMapper = (PhRegMeasureMapper)comRegistry.Map.Mapper<PhRegMeasure>();
                    ElRegDgMeasureMapper eldgMapper = (ElRegDgMeasureMapper)comRegistry.Map.Mapper<ElRegDgMeasure>();

                    measList = new List<IMeasure>();
                    if (String.Compare(table.TableName, elMapper.TableName, true) == 0)
                    {
                        List<ElRegMeasure> tempList = elMapper.GetMeasures(ids);
                        foreach (ElRegMeasure measure in tempList)
                        {
                            measList.Add(measure);
                        }
                    }
                    else if (String.Compare(table.TableName, phMapper.TableName, true) == 0)
                    {
                        List<PhRegMeasure> tempList = phMapper.GetMeasures(ids);
                        foreach (PhRegMeasure measure in tempList)
                        {
                            measList.Add(measure);
                        }
                    }
                    else if (String.Compare(table.TableName, eldgMapper.TableName, true) == 0)
                    {
                        List<ElRegDgMeasure> tempList = eldgMapper.GetMeasures(ids);
                        foreach (ElRegDgMeasure measure in tempList)
                        {
                            measList.Add(measure);
                        }
                    }
                    else
                        throw new Exception("Формулы дорасчета не поддерживают параметры из таблицы: " + table.TableName);
                    
                    // Теперь выделяем если не нашли в БД
                    foreach (ParamItem paramItem in paramList)
                    {
                        bool ffind = false;
                        foreach (IMeasure measure in measList)
                        {
                            if (measure.IdParam == paramItem.Id)
                            {
                                ffind = true;
                                break;
                            }
                        }

                        if (!ffind)
                        {
                            throw new FormulaException(string.Format(Errors.NoFindParameter, paramItem.Id),
                                                       paramItem.StartPosition, paramItem.EndPosition);
                        }
                    }

                    List<IMeasure> sortedMeasList = new List<IMeasure>();
                    foreach (ParamItem item in paramList)
                    {
                        IMeasure foundMeas = null;
                        foreach (IMeasure meas in measList)
                        {
                            if (item.Id != meas.IdParam)
                                continue;
                            foundMeas = meas;
                            break;
                        }
                        if (foundMeas == null)
                            continue;
                        sortedMeasList.Add(foundMeas);
                        measList.Remove(foundMeas);
                    }
                    measList = sortedMeasList;
                }
                catch (FormulaException)
                {
                    throw;
                }
                catch (Exception exc)
                {
                    throw new Exception("Не удалось сформировать лист параметров", exc);
                }

                // Шаг первый: распознание логических элементов
                List<LogicItem> elements = AnalizeLogicItem(formul);

                // Второй шаг: построение выражения по списку логических элементов
                expression = Expression.GetMainExpression(null, elements, 0, elements.Count);

                // Проверка определения аргументов
                CheckArgs(elements);

                try
                {
                    // Проверка функций, количества аргументов и проверка их типов
                    CheckFuncs(expression);
                }
                catch(FuncArgException exc)
                {
                    // ловим исключение несоответствия типа аргумента (заплатка)
                    // Получаем индекс функции
                    int iFind = 0;
                    int startSection = elements.IndexOf(exc.Function);
                    startSection++;
                    startSection++;
                    // Ищем закрывающие скобки или точку с запятой
                    int brecketOpenCount = 0;
                    for (int i = startSection; i < elements.Count; i++)
                    {
                        // Если внутри функции есть еще скобки
                        // то не ищем разделители в них
                        DividerItem dItem = elements[i] as DividerItem;
                        if (dItem != null)
                        {
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
                                // если это тот аргумент что нам нужен
                                if (iFind == exc.ArgIndex)
                                {
                                    // все это нужно только для того чтобы ВЫДЕЛИТЬ аргумент функции
                                    throw new FormulaException(exc.Message,
                                        elements[startSection].StartPosition, elements[i].StartPosition);
                                }

                                iFind++;
                                startSection = i + 1;
                            }
                        }
                    }
                    throw new Exception("Не удалось выделить ошибочный аргумент функции");
                }

                // проверка деления на ноль
                CheckNumber(expression);
            }
            catch (FunctionException funcexc)
            {
                if (!FuncPermit(funcexc.Function.Name))
                {
                    return new ReturnValue("Неизвестное выражение",
                        funcexc.StartPosition, funcexc.EndPosition, paramList, measList);
                }

                return new ReturnValue(funcexc.Message, funcexc.StartPosition, funcexc.EndPosition, paramList, measList);
            }
            catch (FormulaException fexc)
            {
                return new ReturnValue(fexc.Message, fexc.StartPosition, fexc.EndPosition, paramList, measList);
            }

            List<FunctionTemplate> funcList = GetFunctionList(expression);

            return new ReturnValue(expression, paramList, measList, funcList);
        }

        /// <summary>
        /// Получает список функций
        /// </summary>
        /// <returns></returns>
        private List<FunctionTemplate> GetFunctionList(IEnumerable<Expression> expression)
        {
            if (expression == null) 
                throw new ArgumentNullException("expression");
            
            List<FunctionTemplate> funcs = new List<FunctionTemplate>();

            foreach (Expression expr in expression)
            {
                FuncExpression func = expr as FuncExpression;
                if (func != null)
                {
                    FunctionTemplate funcTempl = _permitFunc[func.Function.Name];
                    if (!funcs.Contains(funcTempl))
                        funcs.Add(funcTempl);
                }
            }

            return funcs;
        }

        /// <summary>
        /// Первый шаг алгоритма. По формуле возвращает лист логических элементов формулы
        /// </summary>
        /// <param name="formula">формула</param>
        /// <returns>лист логических элементов</returns>
        private static List<LogicItem> AnalizeLogicItem(string formula)
        {
            List<LogicItem> elements = new List<LogicItem>();
            int start = 0;
            while (start < formula.Length)
            {
                LogicItem elem = LogicItem.GetItem(formula, start);
                start = elem.EndPosition;

                // Игнорируем пробелы
                DividerItem dItem = elem as DividerItem;
                if (dItem != null && dItem.TypeDivider == TypeDivider.Space)
                    continue;
                
                // Добавляем элемент в лист
                elements.Add(elem);
            }

            return elements;
        }

        /// <summary>
        /// Проверяет присутствие в функции неопределенных аргументов argX
        /// </summary>
        /// <param name="elements">логические элементы</param>
        static void CheckArgs(IEnumerable<LogicItem> elements)
        {
            // Проверка определения аргументов
            foreach (LogicItem item in elements)
            {
                ArgItem aItem = item as ArgItem;
                if (aItem != null)
                {
                    throw new FormulaException(Errors.ArgXMustBeChanged,
                                               item.StartPosition, item.EndPosition);
                }
            }
        }

        /// <summary>
        /// Проверка деления на ноль
        /// </summary>
        /// <param name="expression"></param>
        static void CheckNumber (IEnumerable<Expression> expression)
        {
            foreach (Expression exp in expression)
            {
                ItemExpression itemExp = exp as ItemExpression;
                if (itemExp != null)
                {
                    NumberItem item = itemExp.Item as NumberItem;
                    if (item != null)
                    {
                        if (itemExp.Return && item.DoubleValue == 0)
                        {
                            throw new FormulaException(Errors.NullDivider, item.StartPosition, item.EndPosition);

                        }
                    }
                }
            }

        }

        /// <summary>
        /// Проверка функция на предемет присутствия в БД
        /// соответствия количества аргументов
        /// и соответствие типов аргументов
        /// </summary>
        /// <param name="expression">выражение</param>
        void CheckFuncs(IEnumerable<Expression> expression)
        {
            // Проверка функций
            foreach (Expression exp in expression)
            {
                FuncExpression fExp = exp as FuncExpression;
                if (fExp != null)
                {
                    // Проверяем имя функции
                    if (!FuncPermit(fExp.Function.Name))
                        throw new FormulaException(string.Format(Errors.NoFindFunction, fExp.Function.Name),
                                                   fExp.Function.StartPosition, fExp.Function.EndPosition);

                    FunctionTemplate func = GetFunc(fExp.Function.Name);
                    
                    // проверим количесво аргументов
                    if (func.Arguments.Count != fExp.Childs.Count)
                        throw new FormulaException(Errors.FungErrorArgsCount,
                                                   fExp.Function.StartPosition, fExp.Function.EndPosition);

                    // проверяем типы аргументов
                    for (int i = 0; i < fExp.Childs.Count; i++ )
                    {
                        if (func.Arguments[i].IsStatic)
                        {
                            /*
                            // Если аргумент статический в него можно подставить как параметр так и число
                            // при расширении - любое выражение (т.е. эту проверку просто убрать)
                            ItemExpression iExp = fExp.Childs[i] as ItemExpression;
                            ParamItem pItem = null;
                            if (iExp != null)
                                pItem = iExp.Item as ParamItem;
                            
                            NumberItem nItem = null;
                            if (iExp != null)
                                nItem = iExp.Item as NumberItem;

                            if (pItem == null && nItem == null)
                                throw new FuncArgException(Errors.FuncArgErrorType,
                                                            fExp.Function, i);*/
                            // Второй вариант проверки
                            // Если аргумент статический в него можно засунуть все что угодно 
                            //;
                        }
                        else
                        {
                            // Если аргумент динамический то в него можно подставить только параметр
                            ItemExpression iExp = fExp.Childs[i] as ItemExpression;
                            ParamItem pItem = null;
                            if (iExp != null)
                                pItem = iExp.Item as ParamItem;

                            if (pItem == null)
                                throw new FuncArgException(Errors.FuncArgErrorType,
                                                            fExp.Function, i); 
                            // Выставим флаг
                            pItem.UseAsArgument = true;
                        }
                    }
                }
            }
        }

        #endregion


        #region Функции для подсказок

        /// <summary>
        /// Функция возвращает элемет находящийся на указанной позиции 
        /// между разделителями
        /// </summary>
        /// <param name="formula">формула</param>
        /// <param name="position">позиция</param>
        /// <returns></returns>
        public IResultValue GetBlock(string formula, int position)
        {
            if (position >= formula.Length)
                return new ReturnValue(true);

            DividerItem item = DividerItem.GetElement(formula, position);
            if (item != null)
                return new ReturnValue(true);
            
            //ищем левую позицию
            int left = 0;
            for (int i = position; i >= 0; i--)
            {
                item = DividerItem.GetElement(formula, i);
                if (item != null)
                {
                    left = i + 1;
                    break;
                }
            }

            //ищем правую позицию
            int right = formula.Length;
            for (int i = position; i < formula.Length; i++)
            {
                item = DividerItem.GetElement(formula, i);
                if (item != null)
                {
                    right = i;
                    break;
                }
            }

            // Возвращаем интервал
            return new ReturnValue(left, right);
        }

        /// <summary>
        /// Возвращает идентификатор параметра по позиции в формуле
        /// если на позиции стоит не параметр то возвращает 0
        /// </summary>
        /// <param name="formula">формула</param>
        /// <param name="position">позиция</param>
        /// <returns>идентификатор</returns>
        public int GetParameterId(string formula, int position)
        {
            int id = 0;
            try
            {
                ParamItem item = ParamItem.GetElement(formula, position);
                if (item != null)
                    id = item.Id;
            }
            catch(FormulaException)
            {
                
            }
            return id;
        }

        /// <summary>
        /// Возвращает тескт подсказки для функции
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="position"></param>
        /// <param name="funcPos"></param>
        /// <returns></returns>
        public IResultValue GetToolTip(string formula, int position, out int funcPos)
        {
            // получаем функцию
            FuncItem funcItem = GetFunc(formula, position);
            if (funcItem != null)
            {
                // Смотрим функцию в списке функций
                if (FuncPermit(funcItem.Name))
                {
                    FunctionTemplate func = GetFunc(funcItem.Name);
                    // формируем строку
                    string message = string.Format("{0} - {1}", func.Alias, func.Name);
                    for (int i = 0; i < func.Arguments.Count; i++ )
                    {
                        message = string.Format("{0}\r\n{1}{2} - {3}",
                                                message, ArgItem.argPrefix, i, func.Arguments[i].Name);
                    }

                    funcPos = funcItem.StartPosition;
                    return new ReturnValue(message);
                }
            }

            // Возвращаем интервал
            funcPos = -1;
            return new ReturnValue(true);
        }

        private static FuncItem GetFunc(string formula, int position)
        {
            int bracketOpen = 0;
            int maxBracketOpen = 0;
            bool firstFindIsFunction = true;
            // Идем в право - ищем чем это все кончилось
            int right = formula.Length;
            for (int i = position; i < formula.Length; i++)
            {
                DividerItem item = DividerItem.GetElement(formula, i);
                if (item != null)
                {
                    right = i - 1;
                    break;
                }
            }
            // теперь возвращаемся назад
            if (right == formula.Length)
                right--;
            for (int i = right; i >= 0; i--)
            {
                DividerItem item = DividerItem.GetElement(formula, i);
                // Нашли разделитель                
                if (item != null)
                {
                    // Если длина блока что мы прошли не нулевая
                    if (right - i  - 1 > 0)
                    {
                        // Смотрим является ли этот блок аргументом (argX)
                        ArgItem arg = ArgItem.GetElement(formula, i + 1);
                        if (arg == null)
                        {
                            // Смотрим является ли этот блок функцией
                            FuncItem func = FuncItem.GetElement(formula, i + 1);
                            if (func != null)
                            {
                                // Ура нашли функцию
                                // Если указатель находится где то в ее определении то вернем ее
                                if (firstFindIsFunction || bracketOpen > maxBracketOpen)
                                    return func;
                            }
                        }

                        firstFindIsFunction = false;
                    }

                    // Если это скобки - посчитаем
                    if (item.TypeDivider == TypeDivider.BracketOpen)
                    {
                        if (maxBracketOpen < bracketOpen)
                            maxBracketOpen = bracketOpen;
                        bracketOpen++;
                    }

                    if (item.TypeDivider == TypeDivider.BracketClose)
                    {
                        if (maxBracketOpen < bracketOpen)
                            maxBracketOpen = bracketOpen;
                        bracketOpen--;
                    }

                    // Если разделитель не пробел то уже 
                    if (item.TypeDivider != TypeDivider.Space)
                        firstFindIsFunction = false; 
                    
                    right = i;
                }
            }
            if (right - 1 > 0)
            {
                // Смотрим является ли этот блок аргументом (argX)
                ArgItem arg = ArgItem.GetElement(formula, 0);
                if (arg == null)
                {
                    // Смотрим является ли этот блок функцией
                    FuncItem func = FuncItem.GetElement(formula, 0);
                    if (func != null)
                    {
                        // Ура нашли функцию
                        // Если указатель находится где то в ее определении то вернем ее
                        if (firstFindIsFunction || bracketOpen > maxBracketOpen)
                            return func;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращает лист параметров (имеющихся в БД)
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        private List<ParamItem> GetParamList(string formula)
        {
            List<ParamItem> list = new List<ParamItem>();
            int startSection = 0;

            // получаем подряд все символы
            for (int i = 0; i < formula.Length; i++)
            {
                // Ищем разделители
                DividerItem item = DividerItem.GetElement(formula, i);
                if (item != null)
                {
                    // то что между разделителями пытаемся преобразовать в параметр
                    if (i - startSection > 1)
                    {
                        ParamItem param;
                        try
                        {
                            param = ParamItem.GetElement(formula, startSection);
                        }
                        catch (FormulaException)
                        {
                            param = null;
                        }
                        
                        if (param != null)
                        {
                            // Если параметр присутствует в БД и еще не добавлен в лист - добавим его
                            if (!list.Contains(param))
                                list.Add(param);
                        }
                    }

                    startSection = i + 1;
                }
            }
            // Отдельно пытаемся преобразовать последний элемент
            if (startSection < formula.Length)
            {
                ParamItem param;
                try
                {
                    param = ParamItem.GetElement(formula, startSection);
                }
                catch (FormulaException)
                {
                    param = null;
                }

                if (param != null)
                {
                    // Если параметр присутствует в БД и еще не добавлен в лист - добавим его
                    if (!list.Contains(param))
                        list.Add(param);
                }
            }

            return list;
        }

        #endregion

        /// <summary>
        /// Изменяет количество использований функций
        /// </summary>
        /// <param name="result">результат проверки</param>
        public void SetFunctionUse(IResultValue result)
        {
            ReturnValue val = (ReturnValue) result;

            foreach (Expression expression in val.Expression)
            {
                FuncExpression fexp = expression as FuncExpression;
                if (fexp != null)
                {
                    FunctionTemplate func = _permitFunc[fexp.Function.Name];
                    func.UseCount++;
                    func.MarkDirty();
                }
            }
        }

        /// <summary>
        /// Говорит имеется ли функция в БД
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        bool FuncPermit(string funcName)
        {
            return _permitFunc.ContainsKey(funcName);
        }

        /// <summary>
        /// Возвращает функцию по имени
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        FunctionTemplate GetFunc(string funcName)
        {
            return _permitFunc[funcName];
        }

        /// <summary>
        /// Интерфейс результата анализа
        /// </summary>
        public interface IResultValue : ICodeGenerator
        {
            /// <summary>
            /// Имеется ли ошибка
            /// </summary>
            bool HasError { get; }

            /// <summary>
            /// Сообщение
            /// </summary>
            string Message{ get; }

            /// <summary>
            /// позиция начала блока
            /// </summary>
            int StartPos{ get; }

            /// <summary>
            /// позиция окончания блока
            /// </summary>
            int EndPos { get; }

            /// <summary>
            /// Лист измерений
            /// </summary>
            List<IMeasure> MeasureList { get;}
            
        }

        /// <summary>
        /// Класс результата анализа
        /// </summary>
        private class ReturnValue : IResultValue
        {
            /// <summary>
            /// Имеется ли ошибка
            /// </summary>
            private readonly bool _hasError;

            /// <summary>
            /// Сообщение
            /// </summary>
            private readonly string _message;

            /// <summary>
            /// позиция начала блока
            /// </summary>
            private readonly int _startPos;

            /// <summary>
            /// позиция окончания блока
            /// </summary>
            private readonly int _endPos;
            
            /// <summary>
            /// Выражение
            /// </summary>
            private readonly Expression _expression;

            /// <summary>
            /// Список параметров
            /// </summary>
            private readonly List<ParamItem> _params;

            /// <summary>
            /// Список измерений
            /// </summary>
            private readonly List<IMeasure> _measList;

            /// <summary>
            /// Списко функций
            /// </summary>
            private readonly List<FunctionTemplate> _funcs;

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="message">Сообщение</param>
            /// <param name="startPos">позиция начала блока</param>
            /// <param name="endPos">позиция окончания блока</param>
            /// <param name="pars">Список параметров</param>
            /// <param name="measList">список измерений</param>
            public ReturnValue(string message, int startPos, int endPos, List<ParamItem> pars, List<IMeasure> measList)
            {
                _hasError = true;
                _message = message;
                _startPos = startPos;
                _endPos = endPos;
                _params = pars;
                _measList = measList;
            }

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="startPos">позиция начала блока</param>
            /// <param name="endPos">позиция окончания блока</param>
            public ReturnValue(int startPos, int endPos)
            {
                _hasError = false;
                _startPos = startPos;
                _endPos = endPos;
            }

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="hasError">Признак ошибки</param>
            public ReturnValue(bool hasError)
            {
                _hasError = hasError;
                _message = "";
                _startPos = 0;
                _endPos = 0;
            }

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="message">Сообщение</param>
            public ReturnValue(string message)
            {
                _hasError = false;
                _message = message;
                _startPos = 0;
                _endPos = 0;
            }

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="expression">Выражение</param>
            /// <param name="pars">Список параметров</param>
            /// <param name="measList">списиок измерений</param>
            /// <param name="funcList">Список функций</param>
            public ReturnValue(Expression expression, List<ParamItem> pars, List<IMeasure> measList, List<FunctionTemplate> funcList)
            {
                _hasError = false;
                _message = "";
                _startPos = 0;
                _endPos = 0;
                _expression = expression;
                _params = pars;
                _funcs = funcList;
                _measList = measList;
            }

            /// <summary>
            /// Выражение
            /// </summary>
            public Expression Expression
            {
                get { return _expression; }
            }

            #region IResultValue Members

            /// <summary>
            /// Имеется ли ошибка
            /// </summary>
            public bool HasError
            {
                get { return _hasError; }
            }

            /// <summary>
            /// Сообщение
            /// </summary>
            public string Message
            {
                get { return _message; }
            }

            /// <summary>
            /// позиция начала блока
            /// </summary>
            public int StartPos
            {
                get { return _startPos; }
            }

            /// <summary>
            /// позиция окончания блока
            /// </summary>
            public int EndPos
            {
                get { return _endPos; }
            }

            /// <summary>
            /// Генерация C кода
            /// </summary>
            /// <param name="idChannel">Идентификатор канала измерения</param>
            public string GetCodeC(int idChannel)
            {
                return _expression.GetCodeC(_params, _funcs, idChannel);
            }

            /// <summary>
            /// Список параметров
            /// </summary>
            public List<IMeasure> MeasureList
            {
                get { return _measList; }
            }

            #endregion
        }
    }
}
