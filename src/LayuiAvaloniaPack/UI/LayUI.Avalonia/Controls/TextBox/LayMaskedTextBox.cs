using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 遮掩框
    /// </summary>
    public class LayMaskedTextBox: MaskedTextBox, ILayControl
    {
        /// <summary>
        /// 重置默认输入框样式指定LayTextBox为当前输入框样式
        /// </summary>
        protected override Type StyleKeyOverride => typeof(LayTextBox);
    }
}
