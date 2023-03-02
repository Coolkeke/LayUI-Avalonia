using Avalonia;
using LayUI.Avalonia.Controls;
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
        private static Dictionary<string, LayNotificationHost> _NotificationHosts;
        /// <summary>
        /// 容器
        /// </summary>
        internal static Dictionary<string, LayNotificationHost> NotificationHosts
        {
            get => _NotificationHosts = _NotificationHosts ?? new Dictionary<string, LayNotificationHost>();
        }
        static LayNotification()
        {
            TokenProperty.Changed.Subscribe(OnTokenChanged);
        }

        private static void OnTokenChanged(AvaloniaPropertyChangedEventArgs<string> obj)
        {
            if (obj.Sender is LayNotificationHost host)
            {
                var token = GetToken(host);
                if (token == null) token = Guid.NewGuid().ToString();
                if (NotificationHosts.ContainsKey(token)) NotificationHosts?.Remove(token);
                host.GUID = Guid.NewGuid().ToString();
                NotificationHosts?.Add(token, host);
            }
        }
        /// <summary>
        /// 设置唯一值
        /// </summary>
        public static void SetToken(AvaloniaObject element, string value)
        {
            element.SetValue(TokenProperty, value);
        }

        /// <summary>
        /// 获取唯一值
        /// </summary>
        public static string GetToken(AvaloniaObject element)
        {
            return element.GetValue(TokenProperty);
        }

        /// <summary>
        /// 获取唯一值
        /// </summary>
        public static readonly AttachedProperty<string> TokenProperty = AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, string>(
            "Token", null);
        public Task Show(Information info, NotificationType type, string token)
        {
            throw new NotImplementedException();
        }

        public Task Show(Information info, NotificationType type, Action<ButtonResult> callback, string token)
        {
            throw new NotImplementedException();
        }
    }
}
