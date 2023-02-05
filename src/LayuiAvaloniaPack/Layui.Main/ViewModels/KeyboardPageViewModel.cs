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

namespace Layui.Main.ViewModels
{
    public class KeyboardPageViewModel : ViewModelBase
    {
        private IKeyboardDevice keyboard = KeyboardDevice.Instance;
        private IInputManager inputManager = InputManager.Instance;
        public KeyboardPageViewModel(IContainerExtension container) : base(container) { }
        private DelegateCommand _KeyCommand;
        public DelegateCommand KeyCommand =>
            _KeyCommand ?? (_KeyCommand = new DelegateCommand(ExecuteKeyCommand));

        void ExecuteKeyCommand()
        {
            var app = Application.Current.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;
            var window = app.Windows.Cast<Window>().ToList().Where((o) => o.IsActive)?.FirstOrDefault();
            if (window != null)
            {
                //Avalonia.Input.Key.Y 目前尚未找到映射的办法
                inputManager.ProcessInput(new RawTextInputEventArgs(keyboard, (ulong)DateTime.Now.Ticks, window, "123"));
            }
        }
    }
}
