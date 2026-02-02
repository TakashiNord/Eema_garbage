using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using RSDU.Domain;
using RSDU.Messaging;

namespace RSDU.Components.FormulEdit
{
    public partial class FormulaEditDialog : Form
    {
        public FormulaEditDialog()
        {
            InitializeComponent();
        }

        public string ChannelName
        {
            get { return _txName.Text; }
            set { _txName.Text = value; }
        }

        public void InitLoad(DataRegistry.DataRegistry registry, int command, string applName, int idTable, CalcMeasChannel chan, bool readOnly)
        {
            formuleEditComponent1.ReadOnly = readOnly;
            _txName.ReadOnly = readOnly;
            formuleEditComponent1.InitData(registry, command, applName, idTable, chan);
            formuleEditComponent1.LoadData();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (_txName.Text.Trim().Length == 0)
                {
                    RsduMessageForm.ShowDialog(this, "¬ведите им€ канала",
                        formuleEditComponent1.ApplName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    DialogResult = DialogResult.Cancel;
                    e.Cancel = true;
                    return;
                }

                if (!formuleEditComponent1.GetChanges())
                {
                    DialogResult = DialogResult.Cancel;
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}