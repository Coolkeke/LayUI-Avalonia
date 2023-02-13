using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interface
{
    public interface ILayDialog
    {
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        Task Show(string dialogName, ILayDialogParameter parameters, string token);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        Task Show(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token);
        void ShowDialog(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token);
    }
}
