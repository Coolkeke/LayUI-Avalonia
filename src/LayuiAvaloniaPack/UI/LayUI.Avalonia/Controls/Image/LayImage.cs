using Avalonia;
using Avalonia.Automation.Peers;
using Avalonia.Automation;
using Avalonia.Controls;
using Avalonia.Controls.Automation.Peers;
using Avalonia.Media;
using Avalonia.Metadata;
using LayUI.Avalonia.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace LayUI.Avalonia.Controls
{
    public class LayImage : Image
    {

        public LayImage()
        {
            UriSourceProperty.Changed.AddClassHandler<LayImage>((o, e) => o.OnUriSourceChanged(e));
        }
        /// <summary>
        ///图片地址修改
        /// </summary>
        private void OnUriSourceChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue != null) LayImageHelper.CreateImageAsync((string)e.NewValue).ContinueWith((o) => {
                Dispatcher.UIThread.InvokeAsync(() => Source = o.Result); 
            });
            else Source = null;
        }
        /// <summary>
        /// Defines the <see cref="UriSource"/> property.
        /// </summary>
        public static readonly StyledProperty<string> UriSourceProperty =
            AvaloniaProperty.Register<Control, string>(nameof(UriSource));
        /// <summary>
        /// 图片(支持网络图片以及本地图片读取)
        /// </summary>
        public string UriSource
        {
            get { return GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }
    }
}
