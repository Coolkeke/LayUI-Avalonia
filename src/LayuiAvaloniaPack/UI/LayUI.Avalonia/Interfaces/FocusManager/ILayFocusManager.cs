using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interfaces
{
    /// <summary>
    /// 聚焦管理器
    /// </summary>
    public interface ILayFocusManager
    {
        /// <summary>
        /// 聚焦控制
        /// </summary>
        public IFocusManager? FocusManager { get; }
    }
}
