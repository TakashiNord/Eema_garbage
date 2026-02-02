
using System;
//using RSDU.DataRegistry;
//using RSDU.DataRegistry.Identity;

namespace RSDU.Domain
{
    /// <summary>
    /// Аргумент функции библиотеки функций
    /// </summary>
    public class FunctionArg : DomainObject
    {
        /// <summary>
        /// Имя
        /// </summary>
        string _name;
        
        /// <summary>
        /// краткое имя
        /// </summary>
        string _alias;

        /// <summary>
        /// Функция
        /// </summary>
        FunctionTemplate _function;

        /// <summary>
        /// Если правда, то аргумент является статическим
        /// </summary>
        private bool _isStatic;

        /// <summary>
        /// Порядковый номер аргумента в формуле
        /// </summary>
        private int _order;

        /// <summary>
        /// Конструктор для имеющегося в БД объекта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ds"></param>
        public FunctionArg(IDatabaseSource ds, AbstractIdentity id)
            : base(ds, id)
        {
        }

        /// <summary>
        /// Конструктор для нового объекта
        /// </summary>
        /// <param name="ds"></param>
        public FunctionArg(IDatabaseSource ds)
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
        /// Функциия
        /// </summary>
        public FunctionTemplate Function
        {
            get { return _function; }
            set { _function = value; }
        }

        /// <summary>
        /// Если правда, то аргумент является статическим
        /// </summary>
        public bool IsStatic
        {
            get { return _isStatic; }
            set { _isStatic = value; }
        }

        /// <summary>
        /// Порядковый номер аргумента в формуле
        /// </summary>
        public int Order
        {
            get { return _order; }
            set { _order = value; }
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
        /// Приводит объект DomainObject к типу FunctionArg
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static FunctionArg ConvertFromDomainObject(DomainObject item)
        {
            return (FunctionArg)item;
        }

        /// <summary>
        /// Преобразователь объектов DomainObject к типу FunctionArg
        /// </summary>
        public static Converter<DomainObject, FunctionArg> ConverterFromDomainObject = ConvertFromDomainObject;

    }
}
