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
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Dialog
{
    /// <summary>
    ///  LayDialogWindow
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-12 下午 1:50:20</para>
    /// </summary>
    internal class LayDialogWindow : ContentControl, ILayDialogWindow
    {
        private LayDialogHost Host;
        private Action<ILayDialogResult> callback;
        public LayDialogWindow(Action<ILayDialogResult> action, LayDialogHost host)
        {
            Host = host;
            callback = action;
            //RequestCloseHandler = GetRequestCloseHandler();
        }
        public ILayDialogResult Result { get; set; }

        [Bindable(true)]

        /// <summary>
        /// Defines the <see cref="IsOpen"/> property.
        /// </summary>
        public static readonly DirectProperty<LayDialogWindow, bool> IsOpenProperty =
            AvaloniaProperty.RegisterDirect<LayDialogWindow, bool>(nameof(IsOpen),
                o => o.IsOpen,
                (o, v) => o.IsOpen = v);
        private bool _IsOpen;
        /// <summary>
        /// 对话框是否开启
        /// </summary>
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetAndRaise(IsOpenProperty, ref _IsOpen, value); }//IsOpenChanged(); 
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
            if (!IsOpen) {
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
            //this.GetDialogViewModel().RequestClose += RequestCloseHandler;
        }
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            this.GetDialogViewModel().RequestClose -= RequestCloseHandler;
            //抓取回调后的数据并回传
            if (Result == null) Result = new LayDialogResult() { Result = ButtonResult.None };
            callback?.Invoke(Result);
            Host.TaskCompletion?.TrySetResult(null);
        }
    }
}
