using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    ///  键盘
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2023-02-07 上午 10:02:45</para>
    /// </summary>
    public class LayKeyboard : TemplatedControl
    {
        private Grid PART_KeysRoot = null;
        static LayKeyboard()
        {

        }
        /// <summary>
        /// Defines the <see cref="IsCapital"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsCapitalProperty =
            AvaloniaProperty.Register<LayKeyboard, bool>(nameof(IsCapital), false);

        /// <summary>
        /// 是否开启键盘大写
        /// </summary>
        public bool IsCapital
        {
            get { return GetValue(IsCapitalProperty); }
            set { SetValue(IsCapitalProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="IsShift"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsShiftProperty =
            AvaloniaProperty.Register<LayKeyboard, bool>(nameof(IsShift), false);

        /// <summary>
        /// 是否开启键盘Shift
        /// </summary>
        public bool IsShift
        {
            get { return GetValue(IsShiftProperty); }
            set { SetValue(IsShiftProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="IsCtrl"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsCtrlProperty =
            AvaloniaProperty.Register<LayKeyboard, bool>(nameof(IsCtrl), false);

        /// <summary>
        /// 是否开启键盘Ctrl
        /// </summary>
        public bool IsCtrl
        {
            get { return GetValue(IsCtrlProperty); }
            set { SetValue(IsCtrlProperty, value); }
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_KeysRoot = e.NameScope.Find<Grid>("PART_KeysRoot") as Grid;
            AddOrRemoveKeyButtonEnevt(true);
        }
        // <summary>
        /// 给模拟键盘按钮新增或删除事件
        /// </summary>
        /// <param name="isAdd"></param>
        private void AddOrRemoveKeyButtonEnevt(bool isAdd)
        {
            var itemsControls = PART_KeysRoot.Children.Cast<ItemsControl>();
            if (itemsControls != null)
            {
                foreach (var itemsControl in itemsControls)
                {
                    var buttonBases = itemsControl.Items.Cast<Button>();
                    if (buttonBases != null)
                    {
                        foreach (var button in buttonBases)
                        {
                            if (isAdd)
                            {
                                button.Click -= Button_Click;
                                button.Click += Button_Click;
                            }
                            else
                            {
                                button.Click -= Button_Click;
                            }
                        }
                    }

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is Key key)
                {
                    var value = button.CommandParameter?.ToString();
                    if (IsCapital)
                    {
                        if (value != null)
                        {
                            if (value.Contains("||"))
                            {
                                LayKeyboardHelper.SetText("|");
                            }
                            else
                            {
                                LayKeyboardHelper.SetText(value?.ToUpper().Split('|').LastOrDefault());
                            }
                        }

                    }
                    else
                    {
                        if (value != null)
                        {
                            if (value.Contains("||"))
                            {
                                LayKeyboardHelper.SetText("|");
                            }
                            else
                            {
                                LayKeyboardHelper.SetText(value?.ToLower().Split('|').LastOrDefault());
                            }
                        }
                    }
                    LayKeyboardHelper.SetKey(key);
                }
            }
        }
    }
}
