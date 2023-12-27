using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayNotificationHost : ContentControl, ILayControl
    {
        /// <summary>
        /// 存储对话视图容器
        /// </summary>
        internal StackPanel Items { get; private set; }
        /// <summary>
        /// 唯一标识ID
        /// </summary>
        internal string GUID
        {
            get { return GetValue(GUIDProperty); }
            set { SetValue(GUIDProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="string"/>属性
        /// </summary>
        internal static readonly StyledProperty<string> GUIDProperty =
       AvaloniaProperty.Register<LayNotificationHost, string>(nameof(GUID));
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            Items = e.NameScope.Find<StackPanel>("PART_Items");
        }
    }
}
