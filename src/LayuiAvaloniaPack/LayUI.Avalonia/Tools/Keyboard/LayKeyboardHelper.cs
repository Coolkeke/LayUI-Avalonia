using Avalonia.Input.Raw;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls; 

namespace LayUI.Avalonia.Tools
{
    public class LayKeyboardHelper
    {
        public static void SetKey(Key key)
        {
            //参考 https://github.com/AvaloniaUI/Avalonia/issues/6775
            InputManager.Instance.ProcessInput(new RawTextInputEventArgs(KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, null, ""));
            InputManager.Instance.ProcessInput(new RawKeyEventArgs(KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, null, RawKeyEventType.KeyDown, key, RawInputModifiers.None));
            InputManager.Instance.ProcessInput(new RawKeyEventArgs(KeyboardDevice.Instance, (ulong)DateTime.Now.Ticks, null, RawKeyEventType.KeyUp, key, RawInputModifiers.None));
        }
    }
}
