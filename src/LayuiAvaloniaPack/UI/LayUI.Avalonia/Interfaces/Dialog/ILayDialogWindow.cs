using Avalonia.LogicalTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Avalonia.Interfaces
{
    public interface ILayDialogWindow
    {
        /// <summary>
        /// 内容
        /// </summary>
        object Content { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        ILayDialogResult Result { get; set; }
        /// <summary>
        /// 数据上下文
        /// </summary>
        object DataContext { get; set; }
    }
}
