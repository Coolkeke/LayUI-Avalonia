using Avalonia.Controls;
using Avalonia.Input.Raw;
using Avalonia.Input;
using Layui.Core.Mvvm;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Remote.Protocol.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Runtime.InteropServices;
using Avalonia.Win32;
using Avalonia.Input.TextInput;
using Avalonia.Platform;

namespace Layui.Main.ViewModels
{
    public class KeyboardPageViewModel : ViewModelBase
    {
        public KeyboardPageViewModel(IContainerExtension container) : base(container)
        {

        }
        private DelegateCommand _KeyCommand;
        public DelegateCommand KeyCommand =>
            _KeyCommand ?? (_KeyCommand = new DelegateCommand(ExecuteKeyCommand));
        private void SetKey(Avalonia.Input.Key key)
        {
            var keyboard = KeyboardDevice.Instance;
            var inputManager = InputManager.Instance;
            inputManager.ProcessInput(new RawKeyEventArgs(keyboard, (ulong)DateTime.Now.Ticks, null, RawKeyEventType.KeyDown, key, RawInputModifiers.None));
            inputManager.ProcessInput(new RawKeyEventArgs(keyboard, (ulong)DateTime.Now.Ticks, null, RawKeyEventType.KeyUp, key, RawInputModifiers.None));
        }
        void ExecuteKeyCommand()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //SetKey(Avalonia.Input.Key);
                //return "Microsoft YaHei";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                //return "Menlo"; //换成OSX下的中文字体
            }
            else
            {
                //return "Noto Sans CJK SC";
            }
        }
    }
}
