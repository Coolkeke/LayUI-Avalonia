using Layui.Core.Mvvm;
using LayUI.Avalonia.Interface;
using LayUI.Avalonia.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class NotificationPageViewModel : ViewModelBase
    {
        private ILayNotification notification;
        public NotificationPageViewModel(IContainerExtension container) : base(container)
        {
            notification= container.Resolve<ILayNotification>();
        }
        private DelegateCommand _NotificationCommand;
        public DelegateCommand NotificationCommand =>
            _NotificationCommand ?? (_NotificationCommand = new DelegateCommand(ExecuteNotificationCommand));

        void ExecuteNotificationCommand()
        {
            notification.Show(new Information() {Title="这是标题",Message="恭喜您完成信息通知功能！" }, "RootNotification");
        }
    }
}
