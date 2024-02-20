using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 时间线子项
    /// </summary>
    
    public class LayTimelineItem : HeaderedContentControl, ILayControl
    {
        private const string PC_First = ":first";
        private const string PC_Last = ":last"; 
        private const string PC_None = ":none";
        internal void SetIndex(bool isFirst, bool isLast, bool isNone)
        {
            PseudoClasses.Set(PC_First, isFirst);
            PseudoClasses.Set(PC_Last, isLast);
            PseudoClasses.Set(PC_None, isNone);
        } 
        /// <summary>
        /// Defines the <see cref="Icon"/> property.
        /// </summary>
        public static readonly StyledProperty<object?> IconProperty =
            AvaloniaProperty.Register<LayTimelineItem, object?>(nameof(Icon));

        /// <summary>
        /// 图标
        /// </summary>
        public object? Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IconTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate?> IconTemplateProperty =
            AvaloniaProperty.Register<LayTimelineItem, IDataTemplate?>(nameof(IconTemplate));

        /// <summary>
        /// 图标模板
        /// </summary>
        public IDataTemplate? IconTemplate
        {
            get { return GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        } 
    }
}
