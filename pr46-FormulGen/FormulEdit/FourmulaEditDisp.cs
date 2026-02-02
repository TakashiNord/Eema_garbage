using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RSDU.Components.FormulEdit;
using RSDU.Database.Mappers;
using RSDU.DataRegistry;
using RSDU.DataRegistry.Identity;
using RSDU.Domain;

namespace RSDU.Components
{
    [ComVisible(true)]
    [ProgId("RSDU.Components.FourmulaEditDisp")]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("D0A783AF-542A-39A8-9033-498A53B891D8")]
    public class FourmulaEditDisp : IFourmulaEditDisp
    {
        /// <summary>
        /// Реестр
        /// </summary>
        private DataRegistry.DataRegistry _comRegistry;

        private string _applName;
        private int _idTable;
        private Command _command;
        private bool _readonly;

        private readonly Dictionary<string, CalcMeasChannel> _hash = new Dictionary<string, CalcMeasChannel>();

        [ComVisible(true)]
        public void InitData(int command, string applName, int idTable, bool readOnly)
        {
            _comRegistry = Registry.GetNewRegistry();
            _comRegistry.SetAppName(applName);
            _command = (Command)command;
            _applName = applName;
            _idTable = idTable;
            _readonly = readOnly;
        }

        [ComVisible(true)]
        public void PropCalcChannel(string key, int id, string inChannelName, out string outChannelName)
        {
            outChannelName = inChannelName;
            CalcMeasChannel data;
            if (_hash.ContainsKey(key))
            {
                data = _hash[key];
                FormulaEditDialog dlg = new FormulaEditDialog();
                dlg.InitLoad(_comRegistry, (int) _command, _applName, _idTable, data, _readonly);
                dlg.ChannelName = inChannelName;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    outChannelName = dlg.ChannelName;
                }
            }
            else
            {
                if (id == 0)
                    throw new Exception("Данная часть алгоритма не реализована");
                
                CalcMeasChannelMapper chMapper =
                    (CalcMeasChannelMapper)_comRegistry.Map.Mapper<CalcMeasChannel>();
                if (chMapper.Exist(id))
                {
                    FormulaEditDialog dlg;
                    using (_comRegistry.ConnHolder)
                    {
                        data = _comRegistry.Map.Find<CalcMeasChannel>(new ObjectIdentity(id));
                        _comRegistry.Map.MiddleLoad(data);
                        //MessageBox.Show("id = " + id + " for " + data.Formule);

                        dlg = new FormulaEditDialog();
                        dlg.InitLoad(_comRegistry, (int)_command, _applName, _idTable, data, _readonly);
                        dlg.ChannelName = inChannelName;
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        data.MarkDirty();
                        Save();
                        //_hash.Add(key, data);
                        outChannelName = dlg.ChannelName;
                    }
                }
                else
                {
                    data = _comRegistry.Map.CreateNew<CalcMeasChannel>();
                    ((PartObjectIdentity)data.Identity).Id = id;
                    FormulaEditDialog dlg = new FormulaEditDialog();
                    dlg.InitLoad(_comRegistry, (int)_command, _applName, _idTable, data, _readonly);
                    dlg.ChannelName = inChannelName;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        
                        data.MarkNew();
                        Save();
                        //_hash.Add(key, data);
                        outChannelName = dlg.ChannelName;
                        //MessageBox.Show("id = " + data.Id + " for " + data.Formule);
                    }
                }
            }
        }

        [ComVisible(true)]
        public void DeleteChannel(string key)
        {
            if (!_hash.ContainsKey(key)) 
                return;
            CalcMeasChannel chan = _hash[key];
            _hash.Remove(key);
            _comRegistry.Work.RegisterRemoved(chan);
        }

        [ComVisible(true)]
        public void Save()
        {
            try
            {
                using (_comRegistry.TransHolder)
                {
                    _comRegistry.Work.Commit();
                }
            }
            catch (SetRoleException setRoleExc)
            {
                RSDU.Messaging.RsduMessageForm.ShowDialog(setRoleExc.Message,
                        _applName, MessageBoxButtons.OK, MessageBoxIcon.Warning, setRoleExc);
            }
            catch (Exception exc)
            {
                RSDU.Messaging.RsduMessageForm.ShowDialog("Невозможно сохранить данные компонента\r\nОбратитесь к администратору",
                        _applName, MessageBoxButtons.OK, MessageBoxIcon.Warning, exc);
            }
        }

        [ComVisible(true)]
        public void ClearContext()
        {
            _comRegistry = null;
        }

        // Функции регистрации компонента в системе
        #region COM Registration

        [ComRegisterFunction]
        public static void RegisterType(string key)
        {
            CompRegisterHelper.RegisterType(key, Assembly.GetExecutingAssembly());
        }

        [ComUnregisterFunction]
        public static void UnregisterType(string key)
        {
            CompRegisterHelper.UnregisterType(key);
        }
        #endregion
    }
}
