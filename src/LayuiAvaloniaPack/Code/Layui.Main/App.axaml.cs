using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using LayUI.Avalonia.Global;
using LayUI.Avalonia.Interface;
using Layui.Main.ViewModels;
using Layui.Main.Views;
using Layui.Tools.Fonts;
using Layui.Tools.Logs;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Layui.Core.Resources;
using Prism.Regions;

namespace Layui.Main
{
    public partial class App : PrismApplication
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            if (!Design.IsDesignMode) base.Initialize();
        }
        public override void RegisterServices()
        {
            AvaloniaLocator.CurrentMutable.Bind<IContainerProvider>().ToConstant(Container);
            AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(new CustomFontManagerImpl());
            AvaloniaLocator.CurrentMutable.Bind<ILogSink>().ToConstant(new LayLogSink());
            base.RegisterServices();
        }
        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override IContainerExtension CreateContainerExtension()
        {
            var local = base.CreateContainerExtension();
            AvaloniaLocator.CurrentMutable.Bind<IContainerExtension>().ToConstant(local);
            //��Prism��������Avalonia ICO ��
            IContainerExtension? container = AvaloniaLocator.Current?.GetService<IContainerExtension>();
            //�ǿ��ж�
            return container ?? local;
        }
        protected override void OnInitialized()
        {
            base.OnInitialized(); 
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(SystemResource.Nav_MainContent, typeof(HomePage));
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialogWindow<DialogWindowBase>();
            containerRegistry.RegisterSingleton<ILayDialogService, LayDialogService>();
            containerRegistry.RegisterSingleton<ILayLogger, LayLogger>();
            containerRegistry.RegisterSingleton<ILayMessage, LayMessage>();
            containerRegistry.RegisterSingleton<ILayNotification, LayNotification>();
            containerRegistry.RegisterDialog<PrismMessageBox, PrismMessageBoxViewModel>("MessageBox");
            containerRegistry.RegisterForNavigation<ButtonPage>(SystemResource.ButtonPage);
            containerRegistry.RegisterForNavigation<FormPage>(SystemResource.FormPage);
            containerRegistry.RegisterForNavigation<HomePage>(SystemResource.HomePage);
            containerRegistry.RegisterForNavigation<ImagePage>(SystemResource.ImagePage);
            containerRegistry.RegisterForNavigation<ProgressBarPage>(SystemResource.ProgressBarPage);
            containerRegistry.RegisterForNavigation<KeyboardPage>(SystemResource.KeyboardPage);
            containerRegistry.RegisterForNavigation<DialogPage>(SystemResource.DialogPage);
            containerRegistry.RegisterForNavigation<CardPage>(SystemResource.CardPage);
            containerRegistry.RegisterForNavigation<MessagePage>(SystemResource.MessagePage);
            containerRegistry.RegisterForNavigation<NotificationPage>(SystemResource.NotificationPage);
            containerRegistry.RegisterForNavigation<LoadingPage>(SystemResource.LoadingPage);
            var layDialog = Container.Resolve<ILayDialogService>();
            layDialog.RegisterDialog<Message>(nameof(Message));
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Container.Resolve<ILayLogger>().Info("����ע��ģ��..."); 
        }
    }
}