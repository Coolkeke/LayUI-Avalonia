using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LayUI.Avalonia.Dialog
{
    /// <summary>
    ///  LayDialogWindow
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-12 下午 1:50:20</para>
    /// </summary>
    internal class LayDialogUserControlWindow : ContentControl, ILayDialogWindow
    {
        public ILayDialogResult Result { get; set; }
        [Bindable(true)]

        /// <summary>
        /// Defines the <see cref="IsOpen"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<LayDialogUserControlWindow, bool>(nameof(IsOpen), false);

        /// <summary>
        /// 开启
        /// </summary>
        public bool IsOpen
        {
            get { return GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
            
        }
    }
}
