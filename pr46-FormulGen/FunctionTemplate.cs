using System;
using System.Collections.Generic;
using RSDU.DataRegistry;
using RSDU.DataRegistry.Identity;

namespace RSDU.Domain
{
    /// <summary>
    /// Функция из библиотеки
    /// </summary>
    public class FunctionTemplate : DomainObject
    {
        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// Краткое имя
        /// </summary>
        private string _alias;

        /// <summary>
        /// Тип
        /// </summary>
        private ObjectType _type;

        /// <summary>
        /// Аргументы функции
        /// </summary>
        private List<FunctionArg> _arguments = new List<FunctionArg>();

        /// <summary>
        /// Заголовок формулы
        /// </summary>
        private string _header;

        /// <summary>
        /// Код формулы
        /// </summary>
        private string _code;

        /// <summary>
        /// Количество использований функции
        /// </summary>
        private int _useCount;

        /// <summary>
        /// Конструктор для имеющегося в БД объекта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ds"></param>
        public FunctionTemplate(IDatabaseSource ds, AbstractIdentity id)
            : base(ds, id)
        {
        }

        /// <summary>
        /// Конструктор для нового объекта
        /// </summary>
        /// <param name="ds"></param>
        public FunctionTemplate(IDatabaseSource ds)
            : base(ds)
        {
            _identity = new ObjectIdentity();
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Краткое имя
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        /// <summary>
        /// Тип функции
        /// </summary>
        public ObjectType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Аргументы функции
        /// </summary>
        public List<FunctionArg> Arguments
        {
            get { return _arguments; }
            set { _arguments = value; }
        }

        /// <summary>
        /// Заголовок формулы
        /// </summary>
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        /// <summary>
        /// Код формулы
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// Количество использований функции
        /// </summary>
        public int UseCount
        {
            get { return _useCount; }
            set { _useCount = value; }
        }

        /// <summary>
        /// Представление объекта в качествет строки
        /// </summary>
        /// <returns>Представление объекта в качествет строки</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Приводит объект DomainObject к типу FunctionTemplate
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static FunctionTemplate ConvertFromDomainObject(DomainObject item)
        {
            return (FunctionTemplate)item;
        }

        /// <summary>
        /// Преобразователь объектов DomainObject к типу FunctionTemplate
        /// </summary>
        public static Converter<DomainObject, FunctionTemplate> ConverterFromDomainObject = ConvertFromDomainObject;

        /// <summary>
        /// Получение имени функции с аргументами
        /// </summary>
        /// <returns>Имя функции с аргументами</returns>
        public string GetNameWithArgs()
        {
            string fullName = Alias;

            string strArgs = string.Empty;
            if (_arguments != null)
            {
                int count = _arguments.Count;
                for (int i = 0; i < count; i++)
                {
                    strArgs += string.Format("arg{0}", i);

                    if (i < (count - 1))
                        strArgs += "; ";
                }
            }

            return string.Format("{0} ({1})", fullName, strArgs);
        }
    }
}
