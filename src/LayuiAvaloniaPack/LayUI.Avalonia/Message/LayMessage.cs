using Avalonia;
using LayUI.Avalonia.Controls;
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
            TokenProperty.Changed.Subscribe(OnTokenChanged);
        }

        private static void OnTokenChanged(AvaloniaPropertyChangedEventArgs<string> obj)
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
        public static readonly AttachedProperty<string> TokenProperty = AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, string>(
            "Token", null);
        public Task Show(Information info, MessageType type, string token)
        {
            throw new NotImplementedException();
        }
    }
}
