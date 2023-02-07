using Avalonia.Input.Raw;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls; 

namespace LayUI.Avalonia.Tools
{
    /// <summary>
    /// 模拟键盘输入帮助类
    /// </summary>
    public class LayKeyboardHelper
    {
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="value"></param>
        public static void SetText(string value)
        {
            if (value == null) return;
            InputManager.Instance.ProcessInput(new RawTextInputEventArgs(
                KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, null, value));
        }
        /// <summary>
        /// 设置按键点击Key
        /// </summary>
        /// <param name="key"></param>
        public static void SetKey(Key key)
        {
            InputManager.Instance.ProcessInput(new RawKeyEventArgs(
                KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, null,
                RawKeyEventType.KeyDown, key, RawInputModifiers.None));
            InputManager.Instance.ProcessInput(new RawKeyEventArgs(
                KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, null,
                RawKeyEventType.KeyUp, key, RawInputModifiers.None));
        }
    }
}
