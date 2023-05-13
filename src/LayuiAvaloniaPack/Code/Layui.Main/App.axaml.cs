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
using Avalonia.Controls.ApplicationLifetimes;
using Prism.Commands;
using Avalonia.Controls.Primitives;
using LayUI.Avalonia.Controls;
using Layui.Tools.Adapters;

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
        public override void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();
        }
        protected override IAvaloniaObject CreateShell()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                return Container.Resolve<MainWindow>();
            }
            else 
            {
                return Container.Resolve<MainPage>();
            } 
        }
        protected override IContainerExtension CreateContainerExtension()
        {
            var local = base.CreateContainerExtension();
            AvaloniaLocator.CurrentMutable.Bind<IContainerExtension>().ToConstant(local);
            //将Prism容器打入Avalonia ICO 中
            IContainerExtension? container = AvaloniaLocator.Current?.GetService<IContainerExtension>();
            //非空判断
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
            containerRegistry.RegisterForNavigation<TabControlPage>(SystemResource.TabControlPage);
            containerRegistry.RegisterForNavigation<StepBarPage>(SystemResource.StepBarPage);
            containerRegistry.RegisterForNavigation<CarouselPage>(SystemResource.CarouselPage); 
            containerRegistry.RegisterForNavigation<ExpanderPage>(SystemResource.ExpanderPage);
            containerRegistry.RegisterForNavigation<GroupBoxPage>(SystemResource.GroupBoxPage);
            containerRegistry.RegisterForNavigation<TreeViewPage>(SystemResource.TreeViewPage);
            containerRegistry.RegisterForNavigation<LegendPage>(SystemResource.LegendPage);
            containerRegistry.RegisterForNavigation<BadgePage>(SystemResource.BadgePage); 
            containerRegistry.RegisterForNavigation<DrawerPage>(SystemResource.DrawerPage); 
            containerRegistry.RegisterForNavigation<GridPage>(SystemResource.GridPage); 
            var layDialog = Container.Resolve<ILayDialogService>();
            layDialog.RegisterDialog<Message>(nameof(Message));
        } 
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Container.Resolve<ILayLogger>().Info("正在注册模块...");
        }
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(LayContentControl), Container.Resolve<LayContentControlRegionAdapter>());
        }
    }
}
