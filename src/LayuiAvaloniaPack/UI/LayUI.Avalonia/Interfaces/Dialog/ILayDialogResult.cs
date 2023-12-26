using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Interfaces
{
    /// <summary>
    /// 返回值
    /// </summary>
    public interface ILayDialogResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        ButtonResult Result { get; }
        /// <summary>
        /// 返回参数
        /// </summary>
        ILayDialogParameter Parameters { get; }

    }
}
