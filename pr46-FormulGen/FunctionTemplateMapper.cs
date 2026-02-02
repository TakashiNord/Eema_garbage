using System;
using System.Collections.Generic;
using System.Text;
using RSDU.DataRegistry;
using RSDU.DataRegistry.Identity;
using RSDU.Domain;

namespace RSDU.Database.Mappers
{
    [DomainObjectType(typeof(FunctionTemplate))]
    public class FunctionTemplateMapper : AbstractMapper
    {
        /// <summary>
        /// Конструктор по значению
        /// </summary>
        /// <param name="ds">Источник данных</param>
        public FunctionTemplateMapper(IDatabaseSource ds)
            : base(ds)
        {
        }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public override string TableName
        {
            get { return "meas_function_template"; }
        }

        /// <summary>
        /// Поля таблицы
        /// </summary>
        protected override string Fields
        {
            get { return "id, name, alias, template_argnum, template_header, template_code, n_use, id_type"; }
        }

        /// <summary>
        /// Создание нового объекта бизнес-логики
        /// </summary>
        /// <returns></returns>
        public override DomainObject CreateNew()
        {
            return new FunctionTemplate(DatabaseSource);
        }

        /// <summary>
        /// Создание фиктивного объекта бизнес-логики
        /// </summary>
        /// <param name="idKey">Идентификатор объекта бизнес-логики</param>
        /// <returns>Объект бизнес-логики</returns>
        protected override DomainObject CreateGhost(AbstractIdentity idKey)
        {
            return new FunctionTemplate(DatabaseSource, idKey);
        }

        /// <summary>
        /// Загрузка объекта бизнес-логики из БД
        /// </summary>
        /// <param name="obj">Объект бизнес-логики</param>
        protected override void DoMiddleLoad(DomainObject obj)
        {
            string strSQL = string.Format(
                "select {0} from {1} where id = {2}",
                Fields, TableName, obj.Identity);

            LoadOne(obj, strSQL);
        }

        /// <summary>
        /// Загрузка объекта бизнес-логики из БД полностью
        /// </summary>
        /// <param name="obj">Объект бизнес-логики</param>
        protected override void DoFullLoad(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавление объекта бизнес-логики в БД
        /// </summary>
        /// <param name="obj"></param>
        protected override void DoInsert(DomainObject obj)
        {
            FunctionTemplate item = (FunctionTemplate)obj;
            ((ObjectIdentity)item.Identity).Id = GetNextId();

            string strSQL = string.Format(
                    "insert into {0}({1}) values({2})",
                    TableName, 
                    Fields,
                    InsertFields);

            ExecQuery(strSQL,
                      item.Identity,
                      item.Name,
                      item.Alias,
                      item.Arguments.Count,
                      item.Header,
                      item.Code,
                      item.UseCount,
                      item.Type);
        }

        /// <summary>
        /// Обновление объекта бизнес-логики в БД
        /// </summary>
        /// <param name="obj"></param>
        protected override void DoUpdate(DomainObject obj)
        {
            FunctionTemplate item = (FunctionTemplate)obj;

            string strSQL = string.Format(
                "update {0} set {1} where id = {2}",
                TableName, UpdateFields, item.Identity);

            ExecQuery(strSQL,
                      item.Identity,
                      item.Name,
                      item.Alias,
                      item.Arguments.Count,
                      item.Header,
                      item.Code,
                      item.UseCount,
                      item.Type);
        }

        /// <summary>
        /// Удаление объекта бизнес-логики из БД
        /// </summary>
        /// <param name="obj"></param>
        protected override void DoDelete(DomainObject obj)
        {
            string strSQL = string.Format("delete from {0} where id = {1}",
                TableName, obj.Identity);
            ExecQuery(strSQL);
        }

        /// <summary>
        /// Получение всех функций
        /// </summary>
        /// <returns>Список функций</returns>
        public List<FunctionTemplate> GetAll()
        {
            string strSQL = string.Format(
                "select {0} from {1} order by name",
                Fields, TableName);

            List<FunctionTemplate> list = GetAny<FunctionTemplate>(strSQL);

            // Загрузим все аргументы функций в одном запросе
            FunctionArgMapper argMapper = (FunctionArgMapper)DatabaseSource.Mapper<FunctionArg>();
            argMapper.GetAll();

            return list;
        }

        /// <summary>
        /// Читает поля ридера в объект
        /// </summary>
        /// <param name="obj">Объект бизнес-логики</param>
        /// <param name="reader">Ридер данных</param>
        protected override void ReadFields(DomainObject obj, RsduDataReader reader)
        {
            FunctionTemplate item = (FunctionTemplate)obj;

            int id = reader.GetInt32(0);
            item.Identity = new ObjectIdentity(id);
            item.Name = reader.GetString(1);
            item.Alias = reader.GetString(2);

            FunctionArgMapper mapper = (FunctionArgMapper)DatabaseSource.Mapper<FunctionArg>();
            item.Arguments = mapper.GetArgumentsByFunction(id);

            item.Header = reader.GetString(4);
            item.Code = reader.GetString(5);
            item.UseCount = reader.GetInt32(6);
            item.Type = DatabaseSource.Find<ObjectType>(new ObjectIdentity(reader.GetInt32(7)));
        }
    }
}
