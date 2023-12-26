using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interfaces
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

        //void ShowDialog(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token);
        /// <summary>
        /// 关闭目标容器对话框
        /// </summary>
        /// <param name="token"></param>
        void Close(string token);
        /// <summary>
        /// 关闭所有容器对话框
        /// </summary>
        void CloseAll();
    }
}
