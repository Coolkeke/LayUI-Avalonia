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
        /// 这是依赖属性名称
        /// </summary>
        public KeyboardStateType KeyboardState
        {
            get { return GetValue(KeyboardStateProperty); }
            set { SetValue(KeyboardStateProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="KeyboardStateType"/>属性
        /// </summary>
        public static readonly StyledProperty<KeyboardStateType> KeyboardStateProperty =
       AvaloniaProperty.Register<LayKeyboard, KeyboardStateType>(nameof(KeyboardState), KeyboardStateType.Lowercase);


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
                if (button.CommandParameter is Key key)
                {
                    LayKeyboardHelper.SetKey(key);
                }
                else
                {
                    switch (KeyboardState)
                    {
                        case KeyboardStateType.Capital:
                            LayKeyboardHelper.SetText(button.CommandParameter.ToString()?.Trim().ToUpper().Split('|').LastOrDefault());
                            break;
                        case KeyboardStateType.Lowercase:
                            LayKeyboardHelper.SetText(button.CommandParameter.ToString()?.Trim().ToLower().Split('|').LastOrDefault());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
