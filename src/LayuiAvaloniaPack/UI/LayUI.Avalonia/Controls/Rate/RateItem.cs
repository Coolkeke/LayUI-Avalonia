using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 评分项
    /// </summary>
    public class RateItem : Button, ILayControl
    { 
        /// <summary>
        /// Defines the <see cref="IsSelected"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<RateItem, bool>(nameof(IsSelected), false);

        /// <summary>
        /// 选中项
        /// </summary>
        public bool IsSelected
        {
            get { return GetValue(IsSelectedProperty); }
            private set { SetValue(IsSelectedProperty, value); }
        }
    }
}
