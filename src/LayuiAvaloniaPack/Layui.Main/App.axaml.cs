using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Layui.Core;
using Layui.Main.ViewModels;
using Layui.Main.Views;
using LayUI.Avalonia;
using LayUI.Avalonia.Global;
using LayUI.Avalonia.Interfaces;
using LayUI.Avalonia.Tools;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System.Collections.Specialized;

namespace Layui.Main
{
    public partial class App : PrismApplication
    {
        protected override AvaloniaObject CreateShell()
        {
            AvaloniaObject view;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            {
                view = Container.Resolve<MainWindow>();
            }
            else
            {
                view = Container.Resolve<MainPage>();
            } 
            LayKeyboardHelper.InitializeKeyboard(view as Visual);
            return view;
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Logger.Sink = Container.Resolve<ILogSink>();
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(SystemResource.Nav_MainContent, SystemResource.HomePage);
        } 
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILogSink, LayLogSink>();
            containerRegistry.RegisterSingleton<ILayLogger, LayLogger>();
            containerRegistry.RegisterSingleton<ILayClipboard, LayClipboard>();
            containerRegistry.RegisterSingleton<ILayDialogService, LayDialogService>();
            containerRegistry.RegisterSingleton<ILayMessage, LayMessage>();
            containerRegistry.RegisterSingleton<ILayNotification, LayNotification>();
            containerRegistry.RegisterForNavigation<MainPage, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<HomePage>(SystemResource.HomePage);
            containerRegistry.RegisterForNavigation<IconPage>(SystemResource.IconPage);
            containerRegistry.RegisterForNavigation<ButtonPage>(SystemResource.ButtonPage);
            containerRegistry.RegisterForNavigation<CardPage>(SystemResource.CardPage);
            containerRegistry.RegisterForNavigation<BadgePage>(SystemResource.BadgePage);
            containerRegistry.RegisterForNavigation<LoadingPage>(SystemResource.LoadingPage);
            containerRegistry.RegisterForNavigation<TextBoxPage>(SystemResource.TextBoxPage);
            containerRegistry.RegisterForNavigation<DialogPage>(SystemResource.DialogPage);
            containerRegistry.RegisterForNavigation<MessagePage>(SystemResource.MessagePage);
            containerRegistry.RegisterForNavigation<NotificationPage>(SystemResource.NotificationPage);
            containerRegistry.RegisterForNavigation<GroupBoxPage>(SystemResource.GroupBoxPage);
            containerRegistry.RegisterForNavigation<RadioButtonPage>(SystemResource.RadioButtonPage);
            containerRegistry.RegisterForNavigation<CheckBoxPage>(SystemResource.CheckBoxPage);
            containerRegistry.RegisterForNavigation<NoticeBarPage>(SystemResource.NoticeBarPage);
            containerRegistry.RegisterForNavigation<ProgressBarPage>(SystemResource.ProgressBarPage);
            containerRegistry.RegisterForNavigation<SkeletonPage>(SystemResource.SkeletonPage);
            containerRegistry.RegisterForNavigation<FlowItemsControlPage>(SystemResource.FlowItemsControlPage);
            containerRegistry.RegisterForNavigation<ImagePage>(SystemResource.ImagePage);
            containerRegistry.RegisterForNavigation<RipplePage>(SystemResource.RipplePage);
            containerRegistry.RegisterForNavigation<LegendPage>(SystemResource.LegendPage);
            containerRegistry.RegisterForNavigation<ToolTipPage>(SystemResource.ToolTipPage);
            containerRegistry.RegisterForNavigation<PopupBoxPage>(SystemResource.PopupBoxPage);
            containerRegistry.RegisterForNavigation<KeyboardPage>(SystemResource.KeyboardPage);
            var layDialog = Container.Resolve<ILayDialogService>();
            layDialog.RegisterDialog<Message>(nameof(Message));
        }
    }
}