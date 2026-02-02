using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RSDU.Components.FormulEdit;
using RSDU.Components.FormulEdit.Analizer;
using RSDU.Database.Mappers;
using RSDU.DataRegistry;
using RSDU.DataRegistry.Identity;
using RSDU.Domain;
using RSDU.Domain.Interfaces;
using RSDU.Interop;
using RSDU.Messaging;

namespace RSDU.Components
{
    /// <summary>
    /// Компонент для заведения формул параметров
    /// </summary>
    public partial class FormuleEditComponent : RsduComComponent
    {
        /*/// <summary>
        /// Хеш используемых параметров 
        /// </summary>
        readonly SortedDictionary<int, IMeasure> _params = new SortedDictionary<int, IMeasure>();*/

        /// <summary>
        /// Хеш функций
        /// </summary>
        readonly Dictionary<string, FunctionTemplate> _functions = new Dictionary<string, FunctionTemplate>();

        /// <summary>
        /// Типы функций
        /// </summary>
        private List<ObjectType> _functionTypes = new List<ObjectType>();

        /// <summary>
        /// Канал измерения
        /// </summary>
        private CalcMeasChannel _channel;

        /// <summary>
        /// Лист параметров имеющихся в формуле
        /// </summary>
        private List<IMeasure> _paramList = new List<IMeasure>();

        /// <summary>
        /// Список узлов из которых можно получать параметры для формулы
        /// </summary>
        private List<SysTreeNode> _listSysTreeNodes = new List<SysTreeNode>();

        /// <summary>
        /// Настройщик БД
        /// </summary>
        private readonly AdjustAppl _adjust = new AdjustAppl();

        /// <summary>
        /// Анализатор формулы
        /// </summary>
        private AnalizeHelper _formuleAnalyzer;

        /// <summary>
        /// Задает режим, когда Си код не соответствует коду формулы
        /// и пользователь хочет оставить Скод
        /// </summary>
        private bool _manualMode;

        /// <summary>
        /// Предыдущий настроенный параметр
        /// </summary>
        private IMeasure _prevAdjustMeasure;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public FormuleEditComponent()
        {
            InitializeComponent();
            _formula.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            _formula.LostFocus += OnFormulaLostfocus;
        }

        /// <summary>
        /// Использовать для теста (в приложении TestFormul)
        /// </summary>
        private const bool fTest = false;

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        private void Init()
        {
            if (ParentForm != null)
                ParentForm.Move += OnFormMove;

            using (ComRegistry.ConnHolder)
            {
                _listSysTreeNodes.Clear();
                SysTreeNodeMapper sysTreeMapper = (SysTreeNodeMapper) ComRegistry.Map.Mapper<SysTreeNode>();
                _listSysTreeNodes = sysTreeMapper.GetByTable(_idTable);

                _btnAddParam.Enabled = (_listSysTreeNodes.Count > 0);
                if (_btnAddParam.Enabled)
                    _adjust.AdjustParam += OnAdjustParam;

                // Получение типов функций
                ObjectTypeMapper otypMapper = (ObjectTypeMapper) ComRegistry.Map.Mapper<ObjectType>();
                _functionTypes = otypMapper.GetFromView("MEAS_FUNCTION_TYPE_V");

                // Получение шаблонов функций
                FunctionTemplateMapper templMapper =
                    (FunctionTemplateMapper) ComRegistry.Map.Mapper<FunctionTemplate>();
                List<FunctionTemplate> listFunc = templMapper.GetAll();

                foreach (FunctionTemplate func in listFunc)
                    _functions[func.Alias] = func;

                // Получение параметров
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                if (!fTest)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                {

                }
                else
#pragma warning disable 162
                {
                   /* ElRegMeasureMapper map = (ElRegMeasureMapper)ComRegistry.Map.Mapper<ElRegMeasure>();
                    
                    IMeasure measure = (ElRegMeasure) map.Find(new ObjectIdentity(1));
                    _params.Add(measure.IdParam, measure);

                    measure = (ElRegMeasure)map.Find(new ObjectIdentity(2));
                    _params.Add(measure.IdParam, measure);

                    measure = (ElRegMeasure)map.Find(new ObjectIdentity(3));
                    _params.Add(measure.IdParam, measure);

                    measure = (ElRegMeasure)map.Find(new ObjectIdentity(4));
                    _params.Add(measure.IdParam, measure);

                    measure = (ElRegMeasure)map.Find(new ObjectIdentity(5));
                    _params.Add(measure.IdParam, measure);

                    measure = (ElRegMeasure)map.Find(new ObjectIdentity(6));
                    _params.Add(measure.IdParam, measure);*/
                }
#pragma warning restore 162



                _formuleAnalyzer = new AnalizeHelper(_functions);

                // Заполнение полей
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                if (!fTest)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                {
                    _formula.Text = _channel.Formule;
                }
                _checkFormulResult.Text = string.Empty;
                // Проверка изменения Скода в БД
                bool fHasError = false;  // Ошибка в формуле
                bool fnotEmpty = false;  // Пусто поле формулы но код формулы не пуст
                bool fNotEquals = false; // Не равны формула и код

                string strCCodeFromDB = "";

                if (_formula.Text != "")
                {
                    AnalizeHelper.IResultValue result = CheckFormule();
                    if (!result.HasError)
                    {
                        strCCodeFromDB = result.GetCodeC(((PartObjectIdentity)_channel.Identity).Id);
                        if (strCCodeFromDB != _channel.CodeC)
                            fNotEquals = true;
                    }
                    else
                        fHasError = true;
                }
                else
                {
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                    if (!fTest)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                    {
                        if (!string.IsNullOrEmpty(_channel.CodeC))
                            fnotEmpty = true;
                    }
                }

                // Если код был изменен предлагаем принять изменения
                if (fHasError || fnotEmpty || fNotEquals)
                {
                    
                    string code = "";
                    if (_channel.CodeC != null)
                    {
                        code = _channel.CodeC;
                    }
                    InformationException exc = new InformationException("Код формулы", code);

                    if (fNotEquals)
                    {
                        exc = new InformationException("Код формулы:", "Старый:\r\n" + strCCodeFromDB + "\r\nНовый:\r\n" + code);
                    } 

                    if (_readonly)
                    {
                        RsduMessageForm.ShowDialog(this,
                            "Код формулы был изменен не через картридж.\r\nСодержание строки формулы не актуально.",
                            _applName,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning, exc);

                        _manualMode = true;
                    }
                    else
                    {
                        if (RsduMessageForm.ShowDialog(this, 
                            "Код формулы был изменен не через картридж.\r\n" +
                            "При ответе \"Да\" будет использован этот код, изменение формулы будет запрещено.\r\n" +
                            "При ответе \"Нет\" будет сформирован новый код на основе формулы.",
                            //"Код функции был изменен в БД\r\nПринять изменения?",
                            _applName,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, exc) ==
                            DialogResult.Yes)
                        {
                            _manualMode = true;
                        }
                    }
                }
            }
            SetControlsEnable();
        }

        /// <summary>
        /// Устанавливает доступность кнопок
        /// </summary>
        void SetControlsEnable()
        {
            if (_manualMode)
            {
                _formula.Enabled = false;
                _gridParams.Enabled = false;
                _btnAddFunc.Enabled = false;
                _btnAddParam.Enabled = false;
                _checkFormul.Enabled = false;
                _checkFormulResult.Enabled = false;
            }
            else
            {
                _formula.Enabled = true;
                _gridParams.Enabled = true;
                _checkFormulResult.Enabled = true;
                _formula.ReadOnly = _readonly;
                
                if (_readonly)
                {
                    _btnAddFunc.Enabled = false;
                    _btnAddParam.Enabled = false;
                    _checkFormul.Enabled = false;
                }
                else
                {
                    _btnAddFunc.Enabled = true;
                    _btnAddParam.Enabled = true;
                    _checkFormul.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Добавление нового параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddParamClick(object sender, EventArgs e)
        {
            if (_listSysTreeNodes.Count > 0)
            {
                int idNode = ((ObjectIdentity)_listSysTreeNodes[0].Identity).Id;

                _adjust.ShowAdjust(idNode, Handle);

                IMeasure meas = null;
                if (_prevAdjustMeasure != null)
                {
                    meas = _prevAdjustMeasure;
                }
                else if (_paramList.Count > 0)
                {
                    meas = _paramList[_paramList.Count - 1];
                }
                if (meas != null)
                    _adjust.SelectSpecNode(meas.IdTable, meas.Node.IdParam);
            }
        }

        /// <summary>
        /// Добавление новой функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddFunctionClick(object sender, EventArgs e)
        {
            AddFunctionForm form = new AddFunctionForm(_functions, _functionTypes);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                FunctionTemplate func = form.GetSelectedFunction();
                if (func != null)
                    AddStringToFormuleCurrentPos(func.GetNameWithArgs());
            }
        }

        /// <summary>
        /// Обработка сообщений
        /// </summary>
        /// <param name="m">Сообщение</param>
        protected override void WndProc(ref Message m)
        {
            if (!_adjust.ProcessMessage(ref m))
                base.WndProc(ref m);
        }

        /// <summary>
        /// Обработка сообщений от приложения настройки БД "Adjust"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAdjustParam(object sender, AdjustAppl.AdjustParamEventArgs e)
        {
            if (e.Command == AdjustAppl.RsduCommand.ASEND_LSTTBL ||
                e.Command == AdjustAppl.RsduCommand.ADROP_LSTTBL)
            {
                SysTable table = ComRegistry.Map.Find<SysTable>(new ObjectIdentity(e.IdLstTbl));
                ComRegistry.Map.MiddleLoad(table);
                string tableName = table.TableName.ToUpper();

                DomainObject meas = null;
                switch(tableName)
                {
                    case "ELREG_LIST_V":
                        meas = ComRegistry.Map.Find<ElRegMeasure>(new ObjectIdentity(e.IdParam));
                        break;
                    case "PHREG_LIST_V":
                        meas = ComRegistry.Map.Find<PhRegMeasure>(new ObjectIdentity(e.IdParam));
                        break;
                    case "ELREGDG_LIST_V":
                        meas = ComRegistry.Map.Find<ElRegDgMeasure>(new ObjectIdentity(e.IdParam));
                        break;
                    default:
                        RsduMessageForm.ShowDialog(this, "Настраиваемый параметр неверного типа", _applName, 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }

                IMeasure measure = meas as IMeasure;
                if (measure != null)
                {
                    ComRegistry.Map.MiddleLoad(meas);

                    _prevAdjustMeasure = measure;

                    string paramName = ParamItem.GetParamName(measure.IdParam);
                    AddStringToFormuleCurrentPos(paramName);

                    //if (!_params.ContainsKey(measure.IdParam))
                    //    _params.Add(measure.IdParam, measure);
                }
            }
        }

        /// <summary>
        /// Добавление строки в текущую позицию формулы
        /// </summary>
        /// <param name="text">Добавляемая строка</param>
        private void AddStringToFormuleCurrentPos(string text)
        {
            string selText = _formula.Text.Remove(_formula.SelectionStart, _formula.SelectionLength);

            int selIndex = _formula.SelectionStart + text.Length;
            _formula.Text = selText.Insert(_formula.SelectionStart, text);
            _formula.SelectionStart = selIndex;
        }
        
        /// <summary>
        /// Событие на нажатие кнопки проверки формулы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormuleCheckClick(object sender, EventArgs e)
        {
            CheckFormule();
        }

        /// <summary>
        /// Проверка формулы
        /// </summary>
        /// <returns>Результат проверки формулы</returns>
        private AnalizeHelper.IResultValue CheckFormule()
        {
            AnalizeHelper.IResultValue value = _formuleAnalyzer.Analize(ComRegistry, _formula.Text, _idTable);
            if (!value.HasError)
            {
                _checkFormulResult.Text = "В этой формуле все верно";
                RSDU.Messaging.Log.Write(value.GetCodeC(1));
            }
            else
            {
                _checkFormulResult.Text = value.Message;
                _formula.Select(value.StartPos, value.EndPos - value.StartPos);
            }

            SetParamList(value.MeasureList);

            return value;
        }

        #region Лист параметров

        /// <summary>
        /// Устанавливает лист параметров
        /// </summary>
        private void SetParamList(List<IMeasure> parameters)
        {
            _gridParams.RowCount = 0;
            _paramList = parameters;
            _gridParams.RowCount = _paramList.Count;
            _gridParams.Refresh();
        }

        /// <summary>
        /// Событие заполнения ячеек грида
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case 0:
                    e.Value = AnalizeHelper.GetParameterFormat(_paramList[e.RowIndex].IdParam);
                    break;
                case 1:
                    if (((DomainObject)_paramList[e.RowIndex].NodeFullNameObject).IsGhost)
                        ComRegistry.Map.MiddleLoad((DomainObject)_paramList[e.RowIndex].NodeFullNameObject);
                    e.Value = string.Join("\\", _paramList[e.RowIndex].NodeFullNameObject.FullName, _paramList[e.RowIndex].Name);
                    break;
            }
        }

        /// <summary>
        /// Выделяет параметр в списке
        /// </summary>
        /// <param name="paramId">идентификатор</param>
        void SelectParam(int paramId)
        {
            foreach (IMeasure measure in _paramList)
            {
                if (measure.IdParam == paramId)
                {
                    _gridParams.Rows[_paramList.IndexOf(measure)].Selected = true;
                    break;
                }
            }
        }

        #endregion

        #region Подсказки

        /// <summary>
        /// Клик мышкой по тексту формулы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            //RSDU.Messaging.Log.Write("OnMouseClick");            
            
            // Пытаемся показать подсказку
            TryToShowToolTip();

            // получим формулу
            if (_formula.SelectionLength == 0)
            {
                int funcPos;
                AnalizeHelper.IResultValue val = _formuleAnalyzer.GetToolTip(_formula.Text, _formula.SelectionStart, out funcPos);
                // Если есть то покажем
                if (!val.HasError)
                {
                    val = _formuleAnalyzer.GetBlock(_formula.Text, _formula.SelectionStart);
                    if (!val.HasError)
                    {
                        _formula.Select(val.StartPos, val.EndPos - val.StartPos);
                        int paramId = _formuleAnalyzer.GetParameterId(_formula.Text, val.StartPos);
                        if (paramId != 0)
                            SelectParam(paramId);
                    }
                }
                else
                {
                    // получим блок
                    val = _formuleAnalyzer.GetBlock(_formula.Text, _formula.SelectionStart);
                    if (!val.HasError)
                    {
                        // Выделим его только если это параметр
                        int paramId = _formuleAnalyzer.GetParameterId(_formula.Text, val.StartPos);
                        if (paramId != 0)
                        {
                            _formula.Select(val.StartPos, val.EndPos - val.StartPos);
                            SelectParam(paramId);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Показана ли сейчас подсказка
        /// </summary>
        private bool _toolTipShowed;

        /// <summary>
        /// Нужно ли игнорировать показ подсказки
        /// </summary>
        private bool _toolTipSkipShowing;

        /// <summary>
        /// текст подсказки
        /// </summary>
        private string _toolTipText;

        /// <summary>
        /// позиция функции
        /// </summary>
        private int _funcPos;

        /// <summary>
        /// Форма подсказки
        /// </summary>
        private ToolTipForm _ttForm;

        /// <summary>
        /// Скрыть подсказку
        /// </summary>
        private void HideToolTip()
        {
            //RSDU.Messaging.Log.Write("HideToolTip");
            //_toolTip.Hide(_formula);
            _ttForm.Hide();
            _toolTipShowed = false;
            _toolTipSkipShowing = false;
        }

        /// <summary>
        /// показать подсказку
        /// </summary>
        /// <param name="message"></param>
        private void ShowToolTip(string message)
        {
            //RSDU.Messaging.Log.Write("ShowToolTip");
            if (_ttForm == null)
            {
                _ttForm = new ToolTipForm();
                _ttForm.NeedHide += OnToolTipNeedHide;
            }

            // Получим точку где стоит курсор
            Point pt = _formula.GetPositionFromCharIndex(_formula.SelectionStart);
            // Узнаем его высоту
            using (Graphics gr = _formula.CreateGraphics())
            {
                SizeF sizeF = gr.MeasureString("Q", _formula.Font);
                // Нарисуем подсказку под курсором
                Point ptLocation = new Point(pt.X, pt.Y + (int) sizeF.Height + 3);
                _ttForm.Location = _formula.PointToScreen(ptLocation);
                //_toolTip.Show(message, _formula, new Point(pt.X, pt.Y + (int)sizeF.Height + 3));
                _ttForm.SetText(message, 0);

                if (!_toolTipSkipShowing)
                {
                    _formula.LostFocus -= OnFormulaLostfocus;
                    _ttForm.Show(this);
                    _formula.Focus();
                    _formula.LostFocus += OnFormulaLostfocus;
                }
                // Первый раз почему то глючит, поэтому нужно второй раз
                _ttForm.SetText(message, 0);

                _toolTipText = message;
                _toolTipShowed = true;
            }
        }

        /// <summary>
        /// Попробовать показать подсказку
        /// </summary>
        private void TryToShowToolTip()
        {
            //RSDU.Messaging.Log.Write("TryToShowToolTip");
            // пробуем получить подсказку
            int funcPos;
            AnalizeHelper.IResultValue val = _formuleAnalyzer.GetToolTip(_formula.Text, _formula.SelectionStart, out funcPos);
            if (!val.HasError)
            {
                if (_toolTipShowed)
                {
                    if (_toolTipText != val.Message || _funcPos != funcPos)
                    {
                        // скрываем одну подсказку
                        HideToolTip();

                        // показываем другую
                        _funcPos = funcPos;
                        ShowToolTip(val.Message);
                    }
                }
                else
                {
                    _funcPos = funcPos; 
                    ShowToolTip(val.Message);
                }
            }
            else
            {
                // вруг подсказка сейчас есть, тогда ее надо скрыть
                if (_toolTipShowed)
                    HideToolTip();
            }
        }

        /// <summary>
        /// Пользователь печатает формулу или перемещается по ней курсором
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            TryToShowToolTip();
        }

        /// <summary>
        /// Изменение положения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormMove(object sender, EventArgs e)
        {
            if (_toolTipShowed)
                HideToolTip();
        }

        /// <summary>
        /// Потеря фокуса элементом с текстом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormulaLostfocus(object sender, EventArgs e)
        {
            //RSDU.Messaging.Log.Write("OnFormulaLostfocus");
            if (_toolTipShowed)
            {
                if (_ttForm.HasFocus)
                {
                    //RSDU.Messaging.Log.Write("tool tip has focus");
                    return;
                }

                HideToolTip();
            }
        }
        /// <summary>
        /// Реакция на события, когда форма подсказки должна быть скрыта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnToolTipNeedHide(object sender, EventArgs e)
        {
            //RSDU.Messaging.Log.Write("OnToolTipNeedHide");
            if (_toolTipShowed)
            {
                _ttForm.Hide();
                _toolTipSkipShowing = true;
            }

        }

        #endregion

        //Методы унаследованные от RsduComponent
        #region Members RsduComponent 


        /// <summary>
        /// Запрос на получение изменений в контроле
        /// </summary>
        /// <returns></returns>
        public override bool GetChanges()
        {
            bool result = base.GetChanges();

            // Если код был изменен в БД - ничего не изменяем
            if (result && _manualMode)
                return true;

            if (result)
            {
                AnalizeHelper.IResultValue value = CheckFormule();
                if (value.HasError)
                {
                    RsduMessageForm.ShowDialog(this, "Найдены ошибки в формуле",
                                               ApplName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                }
                else
                {
                    // Если формула менялась запишем статистику используемых функций
                    if (_channel.Formule != _formula.Text)
                    {
                        _formuleAnalyzer.SetFunctionUse(value); 
                    }
                    // Запишем фргументы формулы
                    _channel.Arguments = value.MeasureList;
                    _channel.ArgNumber = _channel.Arguments.Count;

                    _channel.Formule = _formula.Text;
                    int idChannel = ((PartObjectIdentity)_channel.Identity).Id;
                    _channel.CodeC = value.GetCodeC(idChannel);

                    // Формируем имя файла Си кода

                    SysTable table = ComRegistry.Map.Find<SysTable>(new ObjectIdentity(_idTable));
                    ComRegistry.Map.MiddleLoad(table);

                    string codeCFileName = String.Empty;
                    switch (table.TableName)
                    {
                        case "ELREG_LIST_V":
                            codeCFileName = string.Format("e{0:0000000}.c", idChannel);
                            break;
                        case "ELREGDG_LIST_V":
                            codeCFileName = string.Format("e{0:0000000}.c", idChannel);
                            break;
                        case "PHREG_LIST_V":
                            codeCFileName = string.Format("p{0:0000000}.c", idChannel);
                            break;
                    }

                    if (codeCFileName.Length <= 0)
                    {
                        RsduMessageForm.ShowDialog(this, "Не удалось сформировать имя файла формулы",
                                                   ApplName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        result = false;
                    }
                    else
                    {
                        _channel.CFileName = codeCFileName;
                    }
                }
            }
            return result;
        }

        #endregion

        // Обработка событий контролов компонента
        #region Control View

        #endregion

        //Методы для поддержки COM
        #region For COM

        /*/// <summary>
        /// Идентификатор канала измерения
        /// </summary>
        private int _idMeasChannel;*/

        /// <summary>
        /// Идентификатор таблицы параметра
        /// </summary>
        private int _idTable;

        /// <summary>
        /// Инициализация данных
        /// </summary>
        /// <param name="registry"> </param>
        /// <param name="command">Команда</param>
        /// <param name="applName">Имя приложения</param>
        /// <param name="idTable">Идентификатор таблицы параметра</param>
        /// <param name="chan">Идентификатор канала измерения</param>
        [ComVisible(true)]
        public void InitData(DataRegistry.DataRegistry registry, int command, string applName, int idTable, CalcMeasChannel chan)
        {
            _channel = chan;
            _idTable = idTable;
            ComRegistry = registry;
            InitData(command, applName);
        }

        /// <summary>
        /// Очищение контекста компонента
        /// </summary>
        public override void ClearContext()
        {
            ComRegistry = null;
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        protected override void DoLoadData()
        {
            Init();

            base.DoLoadData();
        }


        #region COM Registration

        /// <summary>
        /// Регистрация COM класса
        /// </summary>
        /// <param name="key"></param>
        [ComRegisterFunction]
        public new static void RegisterClass(string key)
        {
            RsduComComponent.RegisterClass(key);
        }

        /// <summary>
        /// Разрегистрация COM класса
        /// </summary>
        /// <param name="key"></param>
        [ComUnregisterFunction]
        public new static void UnregisterClass(string key)
        {
            RsduComComponent.UnregisterClass(key);
        }

        #endregion

        #endregion

        class InformationException : Exception
        {
            private readonly string _text = "";

            public InformationException(string  header, string text)
                : base(header)
            {
                _text = text;
            }

            public override string ToString()
            {
                return _text;
            }
        }

        private void OnListDoubleClick(object sender, EventArgs e)
        {
            if (_gridParams.SelectedRows.Count > 0)
            {
                int index = _gridParams.SelectedRows[0].Index;
                string paramName = AnalizeHelper.GetParameterFormat(_paramList[index].IdParam);
                AddStringToFormuleCurrentPos(paramName);
            }
        }
    }
}
