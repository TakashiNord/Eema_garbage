using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RSDU.Components.FormulEdit
{
    /// <summary>
    /// Форма подсказки
    /// </summary>
    public partial class ToolTipForm : Form
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ToolTipForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Устанавливает текст подсказки
        /// </summary>
        /// <param name="message"></param>
        /// <param name="irow"></param>
        public void SetText(string message, int irow)
        {
            _textBox.Text = "";
            Size sz = GetSize(irow, message);
            _textBox.Size = new Size(sz.Width, sz.Height);
            this.Size = new Size(sz.Width + 2, sz.Height + 2);
            _textBox.Text = message;
            SelectRow(irow);
        }

        /// <summary>
        /// Имеется ли фокус у формы
        /// </summary>
        public bool HasFocus
        {
            get { return _textBox.Focused; }
        }

        /// <summary>
        /// Возвращает размер текста подсказки
        /// </summary>
        /// <param name="irow"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Size GetSize(int irow, string message)
        {
            string[] lines = message.Split('\n');

            Size szf = TextRenderer.MeasureText(message, _textBox.Font);
            //RSDU.Messaging.Log.Write(szf.Width.ToString("F0") + " " + szf.Height.ToString("F0"));

            string line = lines[irow];

            Size szfSelected = TextRenderer.MeasureText(line, new Font(_textBox.Font, FontStyle.Bold));
            //RSDU.Messaging.Log.Write(szfSelected.Width.ToString("F0") + " " + szfSelected.Height.ToString("F0"));

            if (szf.Width < szfSelected.Width)
                szf.Width = szfSelected.Width;

            Size result = new Size(szf.Width + 3, szf.Height + 6);
            //RSDU.Messaging.Log.Write("richTextBox1: " + result.Width.ToString("F0") + " " + result.Height.ToString("F0"));

            return result;
        }

        /// <summary>
        /// Выделяет строку жирным
        /// </summary>
        /// <param name="irow"></param>
        void SelectRow(int irow)
        {
            int i = 0;
            int cnt = 0;
            foreach (string line in _textBox.Lines)
            {
                if (i == irow)
                {
                    Format(cnt, line.Length);
                    break;
                }
                cnt += (line.Length + 1);
                i++;
            }
        }

        /// <summary>
        /// Выделяет жирным область текста
        /// </summary>
        /// <param name="start"></param>
        /// <param name="lenght"></param>
        void Format(int start, int lenght)
        {
            _textBox.Select(start, lenght);
            _textBox.SelectionFont = new Font(_textBox.Font, FontStyle.Bold);
            _textBox.Select(0,0);
        }

        /// <summary>
        /// Нужно скрыть подсказку
        /// </summary>
        public event EventHandler<EventArgs> NeedHide;

        /// <summary>
        /// Нажата клавиша мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _textBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (NeedHide != null)
                NeedHide(this, new EventArgs());
        }



    }
}