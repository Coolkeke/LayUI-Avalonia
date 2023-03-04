using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Layui.Main;
using Layui.Tools.Fonts;
using Layui.Tools.Languages;
using Layui.Tools.Logs;
using LayUI.Avalonia;
using LayUI.Avalonia.Global;
using LayUI.Avalonia.Interface;
using LayuiApp.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;

namespace LayuiApp
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ILayDialogService>(new LayDialogService());
            containerRegistry.RegisterInstance<ILayLogger>(new LayLogger());
            containerRegistry.RegisterInstance<ILayMessage>(new LayMessage());
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Container.Resolve<ILayLogger>().Info("ÕýÔÚ×¢²áÄ£¿é...");
            moduleCatalog.AddModule<MainModule>();
        }
    }
}
