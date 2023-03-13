using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Windows.Input;

namespace LayUI.Avalonia.Tools
{
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
            GetLoadedCommand((AvaloniaObject)sender).Execute(GetCommandParameter((AvaloniaObject)sender));
        }

        private static void Ui_Unloaded(object sender, global::Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs e)
        {
            GetUnloadedCommand((AvaloniaObject)sender).Execute(GetCommandParameter((AvaloniaObject)sender));
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
        /// <summary>
        /// 扩展界面初始化加载命令
        /// </summary>
        public static readonly AttachedProperty<ICommand> LoadedCommandProperty = 
            AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, ICommand>(
            "LoadedCommand", null);

        /// <summary>
        /// Accessor for Attached property <see cref="LoadedCommandProperty"/>.
        /// </summary>
        public static void SetLoadedCommand(AvaloniaObject element, ICommand value)
        {
            element.SetValue(LoadedCommandProperty, value);
        }
        /// <summary>
        /// Accessor for Attached property <see cref="LoadedCommandProperty"/>.
        /// </summary>
        public static ICommand GetLoadedCommand(AvaloniaObject element)
        {
            return element.GetValue(LoadedCommandProperty);
        }
        /// <summary>
        /// 扩展界面销毁命令
        /// </summary>
        public static readonly AttachedProperty<ICommand> UnloadedCommandProperty = 
            AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, ICommand>(
            "UnloadedCommand", null);
        /// <summary>
        /// Accessor for Attached property <see cref="UnloadedCommandProperty"/>.
        /// </summary>
        public static void SetUnloadedCommand(AvaloniaObject element, ICommand value)
        {
            element.SetValue(UnloadedCommandProperty, value);
        }

        /// <summary>
        /// Accessor for Attached property <see cref="UnloadedCommandProperty"/>.
        /// </summary>
        public static ICommand GetUnloadedCommand(AvaloniaObject element)
        {
            return element.GetValue(UnloadedCommandProperty);
        }

        /// <summary>
        /// 参数传递
        /// </summary>
        public static readonly AttachedProperty<object> CommandParameterProperty = 
            AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, object>(
            "CommandParameter", null);


        /// <summary>
        /// Accessor for Attached property <see cref="CommandParameterProperty"/>.
        /// </summary>
        public static void SetCommandParameter(AvaloniaObject element, object value)
        {
            element.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Accessor for Attached property <see cref="CommandParameterProperty"/>.
        /// </summary>
        public static object GetCommandParameter(AvaloniaObject element)
        {
            return element.GetValue(CommandParameterProperty);
        }

    }
}
