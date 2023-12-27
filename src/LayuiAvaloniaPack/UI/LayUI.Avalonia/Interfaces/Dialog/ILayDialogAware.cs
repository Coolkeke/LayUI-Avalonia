using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Interfaces
{
    public interface ILayDialogAware
    {
        /// <summary>
        /// 回调
        /// </summary>
        event Action<ILayDialogResult> RequestClose;
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="parameters"></param>
        void OnOpened(ILayDialogParameter parameters);
        /// <summary>
        /// 关闭
        /// </summary> 
        void OnClosed();
        /// <summary>
        /// 当前窗体是否只允许存留一个
        /// </summary>
        bool isSingle { get; }
    }
}
