using Layui.Core;
using LayUI.Avalonia.Interfaces;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class DrawerPageViewModel : ViewModelBase
    {
        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetProperty(ref _IsOpen, value); }
        }
        private ILayMessage message;
        public DrawerPageViewModel(IContainerExtension container) : base(container)
        {
            message = container.Resolve<ILayMessage>();
        }
        private DelegateCommand _OpenedCommand;
        public DelegateCommand OpenedCommand =>
            _OpenedCommand ?? (_OpenedCommand = new DelegateCommand(ExecuteOpenedCommand));

        void ExecuteOpenedCommand()
        {
            message.Show($"状态:{IsOpen}", "RootMessage", TimeSpan.FromMilliseconds(3000));
        }

        protected override void Loaded()
        {
            
        }

        protected override void Unloaded()
        {
            
        }
    }
}
