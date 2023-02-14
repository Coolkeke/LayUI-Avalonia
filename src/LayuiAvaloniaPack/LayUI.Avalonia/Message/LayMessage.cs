using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interface;
using LayUI.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Message
{
    /// <summary>
    /// 简单信息提示
    /// </summary>
    public class LayMessage : ILayMessage
    {
        public Task Send(Information info, MessageType type, string token)
        {
            throw new NotImplementedException();
        }
    }
}
