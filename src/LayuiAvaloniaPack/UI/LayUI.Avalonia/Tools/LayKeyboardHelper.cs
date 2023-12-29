using Avalonia.Controls;
using Avalonia.Input;
using Avalonia; 

namespace LayUI.Avalonia.Tools
{
    public class LayKeyboardHelper
    {
        private static TopLevel? TopLevel;
        public static void InitializeInputElement(Visual? visual)
        {
            if (visual != null) TopLevel = TopLevel.GetTopLevel(visual);
        }
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="value"></param>
        public static void SetText(string value)
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
        public static void SetKeyDown(Key key)
        {
            if (TopLevel == null) return;
            var input = TopLevel?.FocusManager?.GetFocusedElement();
            if (input == null) return;
            input?.RaiseEvent(new KeyEventArgs
            {
                RoutedEvent = InputElement.KeyDownEvent,
                Key = key,
                KeyModifiers = KeyModifiers.Shift,
                Source = input,
            });
        }
        public static void SetKeyUp(Key key)
        {
            if (TopLevel == null) return;
            var input = TopLevel?.FocusManager?.GetFocusedElement();
            if (input == null) return;
            input?.RaiseEvent(new KeyEventArgs
            {
                RoutedEvent = InputElement.KeyUpEvent,
                Key = key,
                KeyModifiers = KeyModifiers.Shift,
                Source = input,
            });
        }
    }
}
