using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree; 
using Avalonia.Threading;
using LayUI.Avalonia.Enums; 

namespace LayUI.Avalonia.Controls
{
    public class LayMessageControl : ContentControl, ILayControl
    {
        private LayMessageHost Host;
        private DispatcherTimer timer;
        private TimeSpan Time;
        public LayMessageControl()
        {
        }
        public LayMessageControl(LayMessageHost host, TimeSpan time)
        {
            Host = host;
            Time = time;
        }

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<MessageType> TypeProperty =
            AvaloniaProperty.Register<LayMessageControl, MessageType>(nameof(Type));

        /// <summary>
        /// 提示效果类型
        /// </summary>
        public MessageType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);
            if (Design.IsDesignMode) return;
            timer = new DispatcherTimer()
            {
                Interval = Time,
            };
            timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            if (Design.IsDesignMode) return;
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
                timer = null;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer == null) return;
            timer.Stop();
            timer.Tick -= Timer_Tick;
            Host?.Items?.Children?.Remove(this);
        }
    }
}
