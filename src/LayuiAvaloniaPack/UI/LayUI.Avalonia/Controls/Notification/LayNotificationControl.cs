using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Threading;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    [TemplatePart(Name = nameof(CloseButton), Type = typeof(Button))] 
    public class LayNotificationControl : TemplatedControl, ILayControl
    { 
        /// <summary>
        /// 关闭通知信息按钮
        /// </summary>
        private Button CloseButton;
        private LayNotificationHost Host;
        private DispatcherTimer timer;
        private TimeSpan Time;
        static LayNotificationControl()
        {
            IsOpenProperty.Changed.AddClassHandler<LayNotificationControl>((x, e) => x.OnIsOpenChanged());
        }
        public LayNotificationControl()
        {

        }
        public LayNotificationControl(LayNotificationHost host, TimeSpan time)
        {
            Host = host;
            Time = time;
        }
        private async void OnIsOpenChanged()
        {
            if (IsOpen) return;
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
                timer = null;
            }
            await Task.Delay(250);
            Host.Items.Children.Remove(this);
        }
        /// <summary>
        /// Defines the <see cref="IsOpen"/> property.
        /// </summary>
        internal static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<LayNotificationControl, bool>(nameof(IsOpen));

        /// <summary>
        /// 是否开启
        /// </summary>
        internal bool IsOpen
        {
            get { return GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<NotificationType> TypeProperty =
            AvaloniaProperty.Register<LayNotificationControl, NotificationType>(nameof(Type), NotificationType.Info);


        /// <summary>
        /// Defines the <see cref="Title"/> property.
        /// </summary>
        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<LayNotificationControl, string>(nameof(Title), string.Empty);

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Content"/> property.
        /// </summary>
        public static readonly StyledProperty<object> ContentProperty =
            AvaloniaProperty.Register<LayNotificationControl, object>(nameof(Content), null);

        /// <summary>
        /// 内容
        /// </summary>
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        /// <summary>
        /// Comment
        /// </summary>
        public NotificationType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            CloseButton = e.NameScope.Find<Button>("PART_CloseButton");
            if (CloseButton != null)
            {
                CloseButton.RemoveHandler(Button.ClickEvent, Closed);
                CloseButton.AddHandler(Button.ClickEvent, Closed);
            }
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
            if (Type != NotificationType.Error && Type != NotificationType.Warning) timer.Start();
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
            if (CloseButton != null)
            {
                CloseButton.RemoveHandler(Button.ClickEvent, Closed);
            }
        }
        /// <summary>
        /// 倒计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer == null) return;
            timer.Stop();
            timer.Tick -= Timer_Tick;
            IsOpen = false;
        }
        /// <summary>
        /// 关闭当前提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Closed(object sender, RoutedEventArgs e)
        {
            if (Design.IsDesignMode) return;
            IsOpen = false;
        }
    }
}
