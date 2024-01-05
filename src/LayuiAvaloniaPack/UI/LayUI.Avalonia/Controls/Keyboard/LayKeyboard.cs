using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity; 
using System.Text.RegularExpressions; 

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 键盘
    /// </summary>
    [TemplatePart(Name = nameof(PART_KeysRoot), Type = typeof(Grid))]
    public class LayKeyboard : TemplatedControl, ILayControl
    {
        private Grid? PART_KeysRoot;

        /// <summary>
        /// Defines the <see cref="IsCapsLock"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsCapsLockProperty =
            AvaloniaProperty.Register<LayKeyboard, bool>(nameof(IsCapsLock));

        /// <summary>
        /// 大小写
        /// </summary>
        public bool IsCapsLock
        {
            get { return GetValue(IsCapsLockProperty); }
            set { SetValue(IsCapsLockProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="IsCtrlExtend"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsCtrlExtendProperty =
            AvaloniaProperty.Register<LayKeyboard, bool>(nameof(IsCtrlExtend));

        /// <summary>
        /// 开启Ctrl扩展
        /// <para>支持Ctrl+其他按钮进行组合</para>
        /// </summary>
        public bool IsCtrlExtend
        {
            get { return GetValue(IsCtrlExtendProperty); }
            set { SetValue(IsCtrlExtendProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsAltExtend"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsAltExtendProperty =
            AvaloniaProperty.Register<LayKeyboard, bool>(nameof(IsAltExtend));

        /// <summary>
        /// 开启Alt扩展
        /// <para>支持Alt+其他按钮进行组合</para>
        /// </summary>
        public bool IsAltExtend
        {
            get { return GetValue(IsAltExtendProperty); }
            set { SetValue(IsAltExtendProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsShiftExtend"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsShiftExtendProperty =
            AvaloniaProperty.Register<LayKeyboard, bool>(nameof(IsShiftExtend));

        /// <summary>
        /// 开启Shift扩展
        /// <para>支持Shift+其他按钮进行组合</para>
        /// </summary>
        public bool IsShiftExtend
        {
            get { return GetValue(IsShiftExtendProperty); }
            set { SetValue(IsShiftExtendProperty, value); }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_KeysRoot = e.NameScope.Find<Grid>(nameof(PART_KeysRoot));
        }
        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            if (PART_KeysRoot != null)
            {
                AddOrRemoveKeyButtonEnevt(true);
            }
        }
        protected override void OnUnloaded(RoutedEventArgs e)
        {
            base.OnUnloaded(e);
            if (PART_KeysRoot != null)
            {
                AddOrRemoveKeyButtonEnevt(false);
            }
        }

        /// <summary>
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
        /// <summary>
        /// 开始进行文本内容填充
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter != null && button.CommandParameter is Key key)
                {
                    if (key == Key.RightShift)
                    {
                        IsShiftExtend = !IsShiftExtend;
                        IsAltExtend = false;
                        IsCtrlExtend = false;
                    }
                    else if (key == Key.RightAlt)
                    {
                        IsShiftExtend = false;
                        IsAltExtend = !IsAltExtend;
                        IsCtrlExtend = false;
                    }
                    else if (key == Key.RightCtrl)
                    {
                        IsShiftExtend = false;
                        IsAltExtend = false;
                        IsCtrlExtend = !IsCtrlExtend;
                    }
                    else if (key == Key.Back || key == Key.Delete || key == Key.Enter || key == Key.Tab || key == Key.LWin || key == Key.Escape)
                    {
                        LayKeyboardHelper.SetKey(key, KeyModifiers.None);
                    }
                    else
                    {
                        if (button.Tag != null)
                        {
                            var value = button.Tag.ToString().Split(',');
                            var isLetter = Regex.IsMatch(button.Tag.ToString(), "[a-zA-Z]");
                           if(!isLetter) LayKeyboardHelper.SetText(!IsShiftExtend ? (value.Count() == 3 ? "," : value.LastOrDefault()) :  value.FirstOrDefault());
                           else LayKeyboardHelper.SetText(!IsCapsLock ? value.LastOrDefault() : value.FirstOrDefault()); 
                        }
                        if (IsShiftExtend)
                        { 
                            LayKeyboardHelper.SetHotKey(key, KeyModifiers.Shift); 
                            IsShiftExtend = false;
                            IsAltExtend = false;
                            IsCtrlExtend = false;
                            return;
                        }
                        else if (IsAltExtend)
                        { 
                            LayKeyboardHelper.SetHotKey(key, KeyModifiers.Alt);
                            IsShiftExtend = false;
                            IsAltExtend = false;
                            IsCtrlExtend = false;
                            return;
                        }
                        else if (IsCtrlExtend)
                        {
                            LayKeyboardHelper.SetHotKey(key, KeyModifiers.Control); 
                            IsShiftExtend = false;
                            IsAltExtend = false;
                            IsCtrlExtend = false;
                            return;
                        }
                        if (key== Key.Space)
                        {
                            LayKeyboardHelper.SetText(" ");
                            return;
                        }
                        LayKeyboardHelper.SetKey(key, KeyModifiers.None);
                    }
                }
            }
        }
    }
}
