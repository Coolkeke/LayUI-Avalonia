using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LayUI.Avalonia.Controls
{
    [TemplatePart(Name = nameof(PART_Popup), Type = typeof(Popup))]
    public class LayPopupBox:HeaderedContentControl, ILayControl
    {

        private Popup PART_Popup;
        /// <summary>
        /// Defines the <see cref="IsAutoClose"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsAutoCloseProperty =
            AvaloniaProperty.Register<LayPopupBox, bool>(nameof(IsAutoClose),true);

        /// <summary>
        /// 自动关闭
        /// </summary>
        public bool IsAutoClose
        {
            get { return GetValue(IsAutoCloseProperty); }
            set { SetValue(IsAutoCloseProperty, value); }
        } 
        /// <summary>
        /// Defines the <see cref="IsDropDownOpen"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsDropDownOpenProperty =
            AvaloniaProperty.Register<LayPopupBox, bool>(nameof(IsDropDownOpen)); 
        /// <summary>
        /// 开启下拉
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_Popup = e.NameScope.Find<Popup>(nameof(PART_Popup)); 
            if (PART_Popup != null)
            {
                PART_Popup.Closed -= PopupClosed;
                PART_Popup.Closed += PopupClosed;
            }
        }

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<PositionType> TypeProperty =
            AvaloniaProperty.Register<Control, PositionType>(nameof(Type), PositionType.Bottom);

        /// <summary>
        /// 类型
        /// </summary>
        public PositionType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        private void PopupClosed(object? sender, EventArgs e) => IsDropDownOpen = false;

        protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
        {
            base.OnPointerCaptureLost(e);
            if (!e.Handled && e.Source is Visual source)
            {
                if (PART_Popup?.IsInsidePopup(source) == true)
                {
                    e.Handled = true;
                    return;
                }
                IsDropDownOpen = !IsDropDownOpen; 
            }
        }
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            if (!e.Handled && e.Source is Visual source)
            {
                if (PART_Popup?.IsInsidePopup(source) == true)
                { 
                    if(IsAutoClose) PART_Popup?.Close();
                    e.Handled = true;
                }
                else
                {
                    SetCurrentValue(IsDropDownOpenProperty, !IsDropDownOpen);
                    e.Handled = true;
                }
            } 
            base.OnPointerReleased(e);
        }
    }
}
