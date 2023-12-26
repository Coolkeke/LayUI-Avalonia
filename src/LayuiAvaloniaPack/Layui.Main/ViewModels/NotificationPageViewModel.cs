using Layui.Core;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interfaces;
using Microsoft.VisualBasic;
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
            notification = container.Resolve<ILayNotification>();
        }
        private DelegateCommand _NotificationCommand;
        public DelegateCommand NotificationCommand =>
            _NotificationCommand ?? (_NotificationCommand = new DelegateCommand(ExecuteNotificationCommand));

        void ExecuteNotificationCommand()
        {
            notification.Show("这是标题", "恭喜您完成信息通知功能！", NotificationType.Info, "RootNotification");
            notification.Show("这是标题", "恭喜您完成信息通知功能！", NotificationType.Success, "RootNotification");
            notification.Show("这是标题", "恭喜您完成信息通知功能！", NotificationType.Warning, "RootNotification");
            notification.Show("这是标题", "恭喜您完成信息通知功能！", NotificationType.Error, "RootNotification");
        }

        protected override void Loaded()
        {

        }

        protected override void Unloaded()
        {

        }
    }
}
