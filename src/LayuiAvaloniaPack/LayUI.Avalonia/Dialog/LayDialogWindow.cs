using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using LayUI.Avalonia.Controls;
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

        internal EventHandler OpenHander = null;
        internal TaskCompletionSource<ILayDialogResult> _dialogTaskCompletionSource;
        public ILayDialogResult Result { get; set; }

        [Bindable(true)]

        /// <summary>
        /// Defines the <see cref="IsOpen"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<LayDialogWindow, bool>(nameof(IsOpen), false);

        /// <summary>
        /// 开启
        /// </summary>
        public bool IsOpen
        {
            get { return GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }

        }
        public static readonly RoutedEvent UnloadedEvent =
        RoutedEvent.Register<LayDialogWindow, RoutedEventArgs>(nameof(Unloaded), RoutingStrategies.Bubble);

        // Provide CLR accessors for the event
        public event EventHandler Unloaded
        {
            add => AddHandler(UnloadedEvent, value);
            remove => RemoveHandler(UnloadedEvent, value);
        }
        public static readonly RoutedEvent LoadedEvent =
        RoutedEvent.Register<LayDialogWindow, RoutedEventArgs>(nameof(Loaded), RoutingStrategies.Bubble);

        // Provide CLR accessors for the event
        public event EventHandler Loaded
        {
            add => AddHandler(LoadedEvent, value);
            remove => RemoveHandler(LoadedEvent, value);
        }
        
        //public event EventHandler Unloaded;
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            this.RaiseEvent(new RoutedEventArgs(UnloadedEvent));
        }
        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);
        }
    }
}
