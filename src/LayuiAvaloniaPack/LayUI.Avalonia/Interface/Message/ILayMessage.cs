using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interface
{
    /// <summary>
    /// 简单信息提示接口
    /// </summary>
    public interface ILayMessage
    {
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="info">详情</param>
        /// <param name="type">类型</param>
        /// <param name="token">唯一标识</param>
        /// <returns></returns>
        Task Show(Information info, MessageType type, string token);
    }
}
