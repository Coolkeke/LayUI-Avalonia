using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Interface.Page
{
    /// <summary>
    /// 用于补偿VM无法调用界面销毁事件
    /// </summary>
    public interface ILayPageInitialized
    {
        /// <summary>
        /// 界面初始化
        /// </summary>
        void Loaded();
        /// <summary>
        /// 界面销毁
        /// </summary>
        void Unloaded();
    }
}
