

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Логический элемент - параметр
    /// </summary>
    class ParamItem : LogicItem
    {
        /// <summary>
        /// Символы, с которых может начинаться параметр
        /// </summary>
        private const string startSymbol = @"$";

        /// <summary>
        /// Строка формата параметра
        /// </summary>
        private const string paramFormat = @"{0:0000000}";

        /// <summary>
        /// Получение имени параметра
        /// </summary>
        /// <param name="id">Идентификатор параметра</param>
        /// <returns>Имя параметра</returns>
        public static string GetParamName(int id)
        {
            return string.Format(startSymbol + paramFormat, id);
        }

        /// <summary>
        /// Функция возвращает параметр или null, если элемент по указанной позиции параметром не является  
        /// </summary>
        /// <param name="formule">формула</param>
        /// <param name="startPosition">позиция для старта анлиза</param>
        /// <returns>Логический элемент - параметр</returns>
        public static ParamItem GetElement(string formule, int startPosition)
        {
            // Смотрим начинается ли с $
            string value = formule[startPosition].ToString();
            if (value.IndexOfAny(startSymbol.ToCharArray()) < 0)
                return null;

            // Получаем строку до следующего разделителя
            for (int i = startPosition + 1; i < formule.Length; i++)
            {
                string ch = formule[i].ToString();
                if (ch.IndexOfAny(DividerItem.dividers.ToCharArray()) < 0)
                {
                    value = value + ch;
                }
                else
                {
                    break;
                }
            }
            // Пытаемся преобразовать в число
            int id;
            if (!int.TryParse(value.Remove(0, 1), out id))
            {
                // Если не получилось то параметр задан неверно
                throw new FormulaException(Errors.ParamFailed,
                                           startPosition, startPosition + value.Length);
            }

            // Проверим непосредственно сам формат
            if (string.Format(paramFormat, id) != value.Remove(0, 1))
            {
                throw new FormulaException(Errors.ParamFailed,
                                           startPosition, startPosition + value.Length);
            }

            // возвращаем логический элемент - параметр
            return new ParamItem(value, startPosition, id);
        }

        /// <summary>
        /// Идентификатор парамтра
        /// </summary>
        readonly int _id;

        /// <summary>
        /// Признак того, что параметр нужно использовать как аргумент функции, не 
        /// получая его текущее значение. Необходимо для передачи параметров в качестве не статических
        /// аргументов функций
        /// </summary>
        private bool _useAsArgument;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">строка</param>
        /// <param name="startPosition">позиция строки в формуле</param>
        /// <param name="id">Идентификатор парамтра</param>
        protected ParamItem(string value, int startPosition, int id)
            : base(value, startPosition)
        {
            _id = id;
        }

        /// <summary>
        /// Идентификатор парамтра
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Признак того, что параметр нужно использовать как число, предварительно 
        /// получив его текущее значение. Необходимо для передачи параметров в качестве статических 
        /// аргументов функций
        /// </summary>
        public bool UseAsArgument
        {
            get { return _useAsArgument; }
            set { _useAsArgument = value; }
        }

        /// <summary>
        /// Сравнение
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Объект не должен быть null, и он должен быть того же типа
            ParamItem param = obj as ParamItem;
            if (param == null)
                return false;

            // Если равны ссылки то равны и объекты
            if (ReferenceEquals(this, obj))
                return true;

            return (param.Id == Id);
        }

        /// <summary>
        /// Возвращает хэш Код
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
