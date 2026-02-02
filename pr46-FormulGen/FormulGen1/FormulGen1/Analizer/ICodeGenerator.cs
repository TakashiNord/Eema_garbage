using System;
using System.Collections.Generic;
using System.Text;

namespace RSDU.Components.FormulEdit.Analizer
{
    /// <summary>
    /// Генератор кода
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Генерация C кода
        /// </summary>
        /// <param name="idChannel">Идентификатор канала измерения</param>
        string GetCodeC(int idChannel);
    }
}
