using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interface
{
    /// <summary>
    /// 信息通知接口
    /// </summary>
    public interface ILayNotification
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="type">类型</param>
        /// <param name="token">唯一标识</param>
        /// <returns></returns>
        Task Send(Information info, NotificationType type, string token);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="type">类型</param>
        /// <param name="callback">数据处理回调</param>
        /// <param name="token">唯一标识</param>
        /// <returns></returns>
        Task Send(Information info, NotificationType type, Action<ButtonResult> callback, string token);
    }
}
