using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Logging;
using LayUI.Avalonia.Interface.Page;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Windows.Input;

namespace LayUI.Avalonia.Tools
{
    /// <summary>
    /// 通用控件元素扩展帮助类
    /// </summary>
    public class LayControlHelper
    {

        static LayControlHelper()
        {
            IsAttachProperty.Changed.Subscribe(OnIsAttachChanged);
        }
        private static void OnIsAttachChanged(AvaloniaPropertyChangedEventArgs<bool> obj)
        {
            if (obj.Sender is Control ui)
            {
                if (GetIsAttach(ui))
                { 
                    ui.AttachedToLogicalTree += Ui_Loaded;
                    ui.DetachedFromLogicalTree += Ui_Unloaded;
                }
                else
                {
                    ui.AttachedToLogicalTree -= Ui_Loaded;
                    ui.DetachedFromLogicalTree -= Ui_Unloaded;

                }
            }
        }

        private static void Ui_Loaded(object sender, global::Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs e)
        {
            try
            {
                if (sender is StyledElement ui)
                {
                    if (ui.DataContext is ILayPageInitialized initialized) initialized.Loaded();
                }
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                    ?.Log("LayControlHelper", "VM中Loaded方法出现异常", ex);
            }
        }

        private static void Ui_Unloaded(object sender, global::Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs e)
        {
            try
            {
                if (sender is StyledElement ui)
                {
                    if (ui.DataContext is ILayPageInitialized initialized) initialized.Unloaded();
                }
            }
            catch (Exception ex)
            { 
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                    ?.Log("LayControlHelper", "VM中Unloaded方法出现异常", ex);
            }
        }

        /// <summary>
        /// 是否开始进行元素属性附加
        /// </summary>
        public static readonly AttachedProperty<bool> IsAttachProperty =
            AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, bool>(
            "IsAttach", false);
        /// <summary>
        /// Accessor for Attached property <see cref="IsAttachProperty"/>.
        /// </summary>
        public static void SetIsAttach(AvaloniaObject element, bool value)
        {
            element.SetValue(IsAttachProperty, value);
        }
        /// <summary>
        /// Accessor for Attached property <see cref="IsAttachProperty"/>.
        /// </summary>
        public static bool GetIsAttach(AvaloniaObject element)
        {
            return element.GetValue(IsAttachProperty);
        }
    }
}
