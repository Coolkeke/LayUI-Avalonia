using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Threading;
using LayUI.Avalonia.Controls;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interface;
using LayUI.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Global
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
        public void Show(Information info, string token)
        {
            Show(info, token, TimeSpan.FromMilliseconds(2000));
        }
        public void Show(Information info, string token, TimeSpan time)
        {
            Show(info, NotificationType.Info, token, time);
        }
        public void Show(Information info, NotificationType type, string token)
        {
            Show(info, type, token, TimeSpan.FromMilliseconds(2000));
        }

        public void Show(Information info, NotificationType type, string token, TimeSpan time)
        {
            Show(info, type, null, token, time);
        }

        public void Show(Information info, NotificationType type, Action<ButtonResult> callback, string token)
        {
            Show( info,  type, callback,  token, TimeSpan.FromMilliseconds(2000));
        }
        public void Show(Information info, NotificationType type, Action<ButtonResult> callback, string token, TimeSpan time)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                try
                {
                    if (!NotificationHosts.ContainsKey(token)) return;
                    var messageHost = NotificationHosts[token];
                    var content = new LayNotificationControl(messageHost, time)
                    {
                        DataContext = info,
                        Type = type
                    };
                    messageHost?.Items?.Children?.Insert(0, content);

                }
                catch (Exception ex)
                {

                    Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                              ?.Log("Show", "信息提示显示异常", ex);
                }
            });
        }

        public void Close(string token)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                try
                {

                    if (!NotificationHosts.ContainsKey(token)) return;
                    var notificationHost = NotificationHosts[token];
                    notificationHost?.Items?.Children?.Clear();
                }
                catch (Exception ex)
                {
                    Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                             ?.Log("Close", "信息通知关闭异常", ex);
                }
            });
        }

        public void CloseAll()
        {
            try
            {
                if (NotificationHosts == null) return;
                foreach (var notificationHost in NotificationHosts)
                {
                    Close(notificationHost.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                                         ?.Log("CloseAll", "信息通知关闭异常", ex);
            }
        }


    }
}
