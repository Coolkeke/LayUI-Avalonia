using Avalonia.Controls;
using Avalonia.Input;
using Avalonia; 

namespace LayUI.Avalonia
{
    public class LayKeyboardHelper
    {
        internal static TopLevel? TopLevel;
        public static void InitializeKeyboard(Visual? visual)
        {
            if (visual != null) TopLevel = TopLevel.GetTopLevel(visual);
        }
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="value"></param>
        public static void SetText(string? value)
        {
            if (value == null) return; 
            if (TopLevel == null) return;
            var input = TopLevel?.FocusManager?.GetFocusedElement();
            if (input == null) return;
            input.RaiseEvent(new TextInputEventArgs
            {
                RoutedEvent = InputElement.TextInputEvent,
                Text = value,
                Source = input,
            });
        }
        /// <summary>
        /// 设置按键点击Key
        /// </summary>
        /// <param name="key"></param>
        public static void SetKey(Key key) {
            SetKeyDown(key, KeyModifiers.Control);
            SetKeyUp(key, KeyModifiers.Control);
        }
        /// <summary>
        /// 键盘指令
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyModifier"></param>
        public static void SetKey(Key key, KeyModifiers keyModifier)
        {
            SetKeyDown(key, keyModifier);
            SetKeyUp(key, keyModifier);
        }
        /// <summary>
        /// 按键按下（只能进行功能键使用）
        /// </summary>
        /// <param name="key"></param>
        public static void SetKeyDown(Key key, KeyModifiers keyModifiers)
        {
            if (TopLevel == null) return;
            var input = TopLevel?.FocusManager?.GetFocusedElement();
            if (input == null) return;
            input?.RaiseEvent(new KeyEventArgs
            {
                RoutedEvent = InputElement.KeyDownEvent,
                Key = key,
                KeyModifiers = keyModifiers,
                Source = input,
            });
        }
        /// <summary>
        /// 按键抬起（只能进行功能键使用）
        /// </summary>
        /// <param name="key"></param>
        public static void SetKeyUp(Key key, KeyModifiers keyModifiers)
        {
            if (TopLevel == null) return;
            var input = TopLevel?.FocusManager?.GetFocusedElement();
            if (input == null) return;
            input?.RaiseEvent(new KeyEventArgs
            {
                RoutedEvent = InputElement.KeyUpEvent,
                Key = key,
                KeyModifiers = keyModifiers,
                Source = input,
            });
        }
        public static void SetHotKey(Key key, KeyModifiers keyModifiers)
        {
            if (TopLevel == null) return;
            var input = TopLevel?.FocusManager?.GetFocusedElement();
            if (input == null) return;
            HotKeyManager.SetHotKey(input as AvaloniaObject, new KeyGesture(key, keyModifiers));
        }
    }
}
