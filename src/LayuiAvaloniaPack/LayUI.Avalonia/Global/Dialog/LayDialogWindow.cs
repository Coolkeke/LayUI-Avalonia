using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using LayUI.Avalonia.Controls;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Global
{
    /// <summary>
    ///  LayDialogWindow
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-12 下午 1:50:20</para>
    /// </summary>
    internal class LayDialogWindow : ContentControl, ILayDialogWindow
    {
        private Panel PART_Body;
        private LayDialogHost Host;
        private Action<ILayDialogResult> callback;
        public LayDialogWindow(Action<ILayDialogResult> action, LayDialogHost host)
        {
            Host = host;
            callback = action;
            RequestCloseHandler = GetRequestCloseHandler();
        }
        public ILayDialogResult Result { get; set; }


        /// <summary>
        /// Defines the <see cref="IsOpen"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<LayDialogWindow, bool>(nameof(IsOpen), false);
        [Bindable(true)]
        /// <summary>
        /// 对话框是否开启
        /// </summary>
        public bool IsOpen
        {
            get { return GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); IsOpenChanged(); }
        } 
        private Action<ILayDialogResult> GetRequestCloseHandler()
        {
            Action<ILayDialogResult> requestCloseHandler = null;
            //窗体关闭的回调方法
            requestCloseHandler = (o) =>
            {
                Result = o;
                IsOpen = false;
            };
            return requestCloseHandler;
        }
        private async void IsOpenChanged()
        {
            if (!IsOpen)
            {
                await Task.Delay(90);
                Host.Items.Children.Remove(this);
            }
            else Host.Items.Children.Add(this);
        }
        public static readonly DirectProperty<LayDialogWindow, Action<ILayDialogResult>> RequestCloseHandlerProperty =
        AvaloniaProperty.RegisterDirect<LayDialogWindow, Action<ILayDialogResult>>(nameof(RequestCloseHandler),
            o => o.RequestCloseHandler,
                (o, v) => o.RequestCloseHandler = v);

        private Action<ILayDialogResult> _RequestCloseHandler;
        // Provide CLR accessors for the event
        public Action<ILayDialogResult> RequestCloseHandler
        {
            get => _RequestCloseHandler;
            set => SetAndRaise(RequestCloseHandlerProperty, ref _RequestCloseHandler, value);
        }
        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);
            this.GetDialogViewModel().RequestClose += RequestCloseHandler;
        }
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            if (PART_Body != null) PART_Body.PointerPressed -= PART_Body_PointerPressed;
            this.GetDialogViewModel().RequestClose -= RequestCloseHandler;
            //抓取回调后的数据并回传
            if (Result == null) Result = new LayDialogResult() { Result = ButtonResult.None };
            callback?.Invoke(Result);
            Host.TaskCompletion?.TrySetResult(null);
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_Body = e.NameScope.Find<Panel>("PART_Body");
            if (PART_Body != null) PART_Body.PointerPressed -= PART_Body_PointerPressed;
            if (PART_Body != null) PART_Body.PointerPressed += PART_Body_PointerPressed;
        }

        private void PART_Body_PointerPressed(object sender, global::Avalonia.Input.PointerPressedEventArgs e)
        {
            if (VisualRoot is Window window)
            {
                if (window.WindowState == WindowState.FullScreen)
                    return;
                window.BeginMoveDrag(e);
            }
        }
    }
}
