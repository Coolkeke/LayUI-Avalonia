using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Layui.Main;
using Layui.Main.ViewModels;
using Layui.Main.Views;
using Layui.Tools.Fonts;
using Layui.Tools.Languages;
using Layui.Tools.Logs;
using LayUI.Avalonia;
using LayUI.Avalonia.Global;
using LayUI.Avalonia.Interface;
using LayuiApp.Views;
using LayuiApp.Views.Dialog;
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
            containerRegistry.RegisterDialogWindow<DialogWindowBase>();
            containerRegistry.RegisterSingleton<ILayDialogService, LayDialogService>();
            containerRegistry.RegisterSingleton<ILayLogger, LayLogger>();
            containerRegistry.RegisterSingleton<ILayMessage, LayMessage>();
            containerRegistry.RegisterSingleton<ILayNotification, LayNotification>();
            containerRegistry.RegisterDialog<PrismMessageBox, PrismMessageBoxViewModel>("MessageBox"); 
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Container.Resolve<ILayLogger>().Info("ÕýÔÚ×¢²áÄ£¿é...");
            moduleCatalog.AddModule<MainModule>();
        }
    }
}
