using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Threading;

namespace LayUI.Avalonia.Controls
{
    public class LayImage : Image, ILayControl
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
            if (e.NewValue != null)
            {
                Source = null;
                Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    Source = await LayImageHelper.CreateImageAsync((string)e.NewValue);
                }, DispatcherPriority.Background);
            }
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
