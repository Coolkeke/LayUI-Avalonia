using Avalonia;
using Avalonia.Logging;
using Avalonia.Threading;
using LayUI.Avalonia.Controls;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interfaces; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Global
{
    /// <summary>
    /// 简单信息提示
    /// </summary>
    public class LayMessage : ILayMessage
    {
        private static Dictionary<string, LayMessageHost> _MessageHosts;
        /// <summary>
        /// 容器
        /// </summary>
        internal static Dictionary<string, LayMessageHost> MessageHosts
        {
            get => _MessageHosts = _MessageHosts ?? new Dictionary<string, LayMessageHost>();
        }
        static LayMessage()
        {
            TokenProperty.Changed.AddClassHandler<AvaloniaObject>((o,e)=> OnTokenChanged(e));
        }

        private static void OnTokenChanged(AvaloniaPropertyChangedEventArgs obj)
        {
            if (obj.Sender is LayMessageHost host)
            {
                var token = GetToken(host);
                if (token == null) token = Guid.NewGuid().ToString();
                if (MessageHosts.ContainsKey(token)) MessageHosts?.Remove(token);
                host.GUID = Guid.NewGuid().ToString();
                MessageHosts?.Add(token, host);
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
        public static readonly AttachedProperty<string> TokenProperty = AvaloniaProperty.RegisterAttached<AvaloniaObject, AvaloniaObject, string>(
            "Token", null);
        public void Show(string message, string token)
        {
            Show(message, token, TimeSpan.FromMilliseconds(3000));
        }
        public void Show(string message, string token, TimeSpan time)
        {
            Show(message, token, time, MessageType.Zoom);
        }

        public void Show(string message, string token, MessageType type)
        {
            Show(message, token, TimeSpan.FromMilliseconds(3000), type);
        }

        public void Show(string message, string token, TimeSpan time, MessageType type)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                try
                {
                    if (!MessageHosts.ContainsKey(token)) return;
                    var messageHost = MessageHosts[token];
                    var content = new LayMessageControl(messageHost, time)
                    {
                        Content = message,
                        Type = type
                    };
                    messageHost?.Items?.Children?.Clear();
                    messageHost?.Items?.Children?.Add(content);

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

                    if (!MessageHosts.ContainsKey(token)) return;
                    var messageHost = MessageHosts[token];
                    messageHost?.Items?.Children?.Clear();

                }
                catch (Exception ex)
                {
                    Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                             ?.Log("Close", "信息提示关闭异常", ex);
                }
            });
        }

        public void CloseAll()
        {
            try
            {
                if (MessageHosts == null) return;
                foreach (var messageHost in MessageHosts)
                {
                    Close(messageHost.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                                         ?.Log("CloseAll", "信息提示关闭异常", ex);
            }
        }
    }
}
