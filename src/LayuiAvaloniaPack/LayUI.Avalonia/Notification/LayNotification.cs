using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interface;
using LayUI.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Notification
{
    /// <summary>
    /// 信息通知
    /// </summary>
    public class LayNotification : ILayNotification
    {
        public Task Send(Information info, NotificationType type, string token)
        {
            throw new NotImplementedException();
        }

        public Task Send(Information info, NotificationType type, Action<ButtonResult> callback, string token)
        {
            throw new NotImplementedException();
        }
    }
}
