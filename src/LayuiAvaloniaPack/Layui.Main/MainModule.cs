using Layui.Core.Resources;
using Layui.Main.Views;
using LayUI.Avalonia;
using LayUI.Avalonia.Interface;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Layui.Main
{
    public class MainModule : IModule
    {
        private ILayDialogService layDialog;
        public void OnInitialized(IContainerProvider containerProvider)
        {
            layDialog = containerProvider.Resolve<ILayDialogService>();
            layDialog.RegisterDialog<Message>(nameof(Message));
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(SystemResource.Nav_MainContent, typeof(HomePage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ButtonPage>(SystemResource.ButtonPage);
            containerRegistry.RegisterForNavigation<FormPage>(SystemResource.FormPage);
            containerRegistry.RegisterForNavigation<HomePage>(SystemResource.HomePage);
            containerRegistry.RegisterForNavigation<ImagePage>(SystemResource.ImagePage);
            containerRegistry.RegisterForNavigation<ProgressBarPage>(SystemResource.ProgressBarPage);
            containerRegistry.RegisterForNavigation<KeyboardPage>(SystemResource.KeyboardPage);
            containerRegistry.RegisterForNavigation<DialogPage>(SystemResource.DialogPage);
            containerRegistry.RegisterForNavigation<CardPage>(SystemResource.CardPage);
            containerRegistry.RegisterForNavigation<MessagePage>(SystemResource.MessagePage);
        }
    }
}
