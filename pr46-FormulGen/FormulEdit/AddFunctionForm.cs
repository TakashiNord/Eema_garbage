using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RSDU.Domain;

namespace RSDU.Components.FormulEdit
{
    /// <summary>
    /// Диалог добавления функции
    /// </summary>
    public partial class AddFunctionForm : Form
    {
        /// <summary>
        /// Хеш функций
        /// </summary>
        private readonly IDictionary<string, FunctionTemplate> _functions;

        /// <summary>
        /// Список функций выбранной категории
        /// </summary>
        private readonly List<FunctionTemplate> _listSelectedFunc = new List<FunctionTemplate>();

        /// <summary>
        /// Конструктор по значениям
        /// </summary>
        /// <param name="functions">Хеш функции</param>
        /// <param name="types">Типы функций - категории</param>
        public AddFunctionForm(IDictionary<string, FunctionTemplate> functions, IEnumerable<ObjectType> types)
        {
            InitializeComponent();

            _cbxCategory.Items.Add(Category.oftenFunctions);
            
            foreach (ObjectType type in types)
                _cbxCategory.Items.Add(type);
            
            _functions = functions;
            _gvFunctions.ReadOnly = true;
            if (_cbxCategory.Items.Count > 0)
                _cbxCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Событие на изменение выбора категории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCbxCategorySelValueChanged(object sender, EventArgs e)
        {
            if (_cbxCategory.SelectedItem == Category.oftenFunctions)
            {
                // получим лист
                _gvFunctions.RowCount = 0;
                _listSelectedFunc.Clear();
                
                foreach (FunctionTemplate function in _functions.Values)
                {
                    _listSelectedFunc.Add(function);
                }
                // отсортируем во убыванию количесва использований
                _listSelectedFunc.Sort(new FuncComparer());
                // сократим до 10
                if (_listSelectedFunc.Count > 10)
                {
                    _listSelectedFunc.RemoveRange(10, _listSelectedFunc.Count - 10);
                }

                _gvFunctions.RowCount = _listSelectedFunc.Count;

            }
            
            ObjectType type = _cbxCategory.SelectedItem as ObjectType;
            
            if (type != null && _functions != null)
            {
                _gvFunctions.RowCount = 0;
                _listSelectedFunc.Clear();
                foreach (FunctionTemplate function in _functions.Values)
                {
                    if (function.Type == type)
                        _listSelectedFunc.Add(function);
                }
                _gvFunctions.RowCount = _listSelectedFunc.Count;
            }
            
        }

        /// <summary>
        /// Событие на запрос значений для строки GridView в вирт. моде
        /// </summary>
        /// <param name="sender">Отправитель сообщения</param>
        /// <param name="e">Аргументы</param>
        private void OnGridFuncCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = _listSelectedFunc[e.RowIndex].GetNameWithArgs();
        }

        /// <summary>
        /// Событие на изменение значений источника данных для строки GridView в вирт. моде
        /// </summary>
        /// <param name="sender">Отправитель сообщения</param>
        /// <param name="e">Аргументы</param>
        private void OnGridFuncCellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
        }

        /// <summary>
        /// Событие на форматирование ячеек для строки GridView в вирт. моде
        /// </summary>
        /// <param name="sender">Отправитель сообщения</param>
        /// <param name="e">Аргументы</param>
        private void OnGridFuncCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        /// <summary>
        /// Получение выбранной функции
        /// </summary>
        /// <returns>Выбранная функция</returns>
        public FunctionTemplate GetSelectedFunction()
        {
            FunctionTemplate func = null;
            if (_gvFunctions.SelectedCells.Count > 0)
            {
                int rowIndex = _gvFunctions.SelectedCells[0].RowIndex;
                func = _listSelectedFunc[rowIndex];
            }

            return func;
        }

        /// <summary>
        /// Событие на изменение выбора функции в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridFuncSelChanged(object sender, EventArgs e)
        {
            string funcDesc = string.Empty;
            FunctionTemplate func = GetSelectedFunction();
            _btnOK.Enabled = (func != null);
            if (func != null)
                funcDesc = func.Name;

            _labFuncDesc.Text = funcDesc;
        }

        /// <summary>
        /// Событие на двойной клик по выбранной функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridFuncCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FunctionTemplate func = GetSelectedFunction();
                if (func != null)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        /// <summary>
        /// Класс для специфических категорий
        /// </summary>
        class Category
        {
            /// <summary>
            /// Часто используемые функции
            /// </summary>
            public static readonly Category oftenFunctions = new Category("Часто используемые");
            
            /// <summary>
            /// Имя категории
            /// </summary>
            private readonly string _name;

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="name">Имя категории</param>
            private Category(string name)
            {
                _name = name;
            }

            public override string ToString()
            {
                return _name;
            }
        }

        /// <summary>
        /// Сравнитель для функций 
        /// </summary>
        class FuncComparer : IComparer<FunctionTemplate>
        {

            #region IComparer<FunctionTemplate> Members

            public int Compare(FunctionTemplate x, FunctionTemplate y)
            {
                return  - x.UseCount + y.UseCount;
            }

            #endregion
        }

        private void AddFunctionForm_Load(object sender, EventArgs e)
        {

        }
    }
}