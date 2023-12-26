using LayUI.Avalonia.Enums; 

namespace LayUI.Avalonia.Interfaces
{
    /// <summary>
    /// 简单信息提示接口
    /// </summary>
    public interface ILayMessage
    {
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="message">详情</param> 
        /// <param name="token">唯一标识</param>
        /// <returns></returns>
        void Show(string message, string token);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="message">详情</param> 
        /// <param name="token">唯一标识</param> 
        /// <param name="type">效果</param>
        void Show(string message, string token, MessageType type);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="message">详情</param> 
        /// <param name="token">唯一标识</param>
        /// <param name="time">倒计时</param>
        /// <returns></returns>
        void Show(string message, string token,TimeSpan time);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="message">详情</param> 
        /// <param name="token">唯一标识</param>
        /// <param name="time">倒计时</param>
        /// <param name="type">效果</param>
        void Show(string message, string token, TimeSpan time, MessageType type);
        /// <summary>
        /// 关闭目标提示信息容器
        /// </summary>
        /// <param name="token"></param>
        void Close(string token);
        /// <summary>
        /// 关闭所有提示信息容器
        /// </summary>
        void CloseAll();
    }
}
