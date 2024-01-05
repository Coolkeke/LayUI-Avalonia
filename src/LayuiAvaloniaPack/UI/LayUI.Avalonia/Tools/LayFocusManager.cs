using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Input;
using LayUI.Avalonia.Interfaces;

namespace LayUI.Avalonia
{

    /// <summary>
    /// 聚焦管理器
    /// </summary>
    public class LayFocusManager: ILayFocusManager
    {
        /// <summary>
        /// 聚焦管理器
        /// </summary>
        public IFocusManager? FocusManager { get { return LayKeyboardHelper.TopLevel?.FocusManager; } } 
    }
}
