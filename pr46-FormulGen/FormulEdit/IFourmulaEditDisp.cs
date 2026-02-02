using System.Runtime.InteropServices;

namespace RSDU.Components
{
    [ComVisible(true)]
    public interface IFourmulaEditDisp
    {
        [ComVisible(true)]
        void InitData(int command, string applName, int idTable, bool readOnly);

        [ComVisible(true)]
        void PropCalcChannel(string key, int id, string inChannelName, out string outChannelName);

        [ComVisible(true)]
        void DeleteChannel(string key);

        [ComVisible(true)]
        void Save();

        [ComVisible(true)]
        void ClearContext();
    }
}