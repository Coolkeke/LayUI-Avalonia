using LayUI.Avalonia.Enums; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interfaces
{
    /// <summary>
    /// 信息通知接口
    /// </summary>
    public interface ILayNotification
    {
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="token">唯一标识</param>
        /// <returns></returns>
        void Show(string title, object content, string token);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="token">唯一标识</param>
        /// <param name="time">倒计时</param>
        /// <returns></returns>
        void Show(string title, object content, string token, TimeSpan time);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="type">类型</param>
        /// <param name="token">唯一标识</param>
        /// <returns></returns>
        void Show(string title, object content, NotificationType type, string token);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="type">类型</param>
        /// <param name="token">唯一标识</param>
        /// <param name="time">倒计时</param>
        void Show(string title, object content, NotificationType type, string token, TimeSpan time);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="type">类型</param>
        /// <param name="callback">数据处理回调</param>
        /// <param name="token">唯一标识</param>
        /// <returns></returns>
        void Show(string title, object content, NotificationType type, Action<ButtonResult> callback, string token);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="type">类型</param>
        /// <param name="callback">数据处理回调</param>
        /// <param name="token">唯一标识</param>
        /// <param name="time">倒计时</param>
        void Show(string title, object content, NotificationType type, Action<ButtonResult> callback, string token, TimeSpan time);
        /// <summary>
        /// 关闭信息通知目标容器
        /// </summary>
        /// <param name="token"></param>
        void Close(string token);
        /// <summary>
        /// 关闭所有信息通知容器
        /// </summary>
        void CloseAll();
    }
}
