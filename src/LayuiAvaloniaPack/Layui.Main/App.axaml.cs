using Avalonia; 
using Avalonia.Controls.ApplicationLifetimes; 
using Avalonia.Logging; 
using Layui.Core;
using Layui.Main.ViewModels;
using Layui.Main.Views;
using LayUI.Avalonia;
using LayUI.Avalonia.Extensions;
using LayUI.Avalonia.Global;
using LayUI.Avalonia.Interfaces; 
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions; 

namespace Layui.Main
{
    public partial class App : PrismApplication
    {
        public override void Initialize()
        { 
            LanguageExtension.LoadResourceKey("zh_CN");
            base.Initialize();
        }
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
            containerRegistry.RegisterForNavigation<DrawerPage>(SystemResource.DrawerPage); 
            containerRegistry.RegisterForNavigation<NavExpanderPage>(SystemResource.NavExpanderPage);
            containerRegistry.RegisterForNavigation<ListBoxPage>(SystemResource.ListBoxPage);
            containerRegistry.RegisterForNavigation<TimelinePage>(SystemResource.TimelinePage);
            containerRegistry.RegisterForNavigation<CarouselPage>(SystemResource.CarouselPage);
            containerRegistry.RegisterForNavigation<ComboBoxPage>(SystemResource.ComboBoxPage);
            containerRegistry.RegisterForNavigation<ColorPage>(SystemResource.ColorPage);
            containerRegistry.RegisterForNavigation<NumericUpDownPage>(SystemResource.NumericUpDownPage);
            containerRegistry.RegisterForNavigation<SwitchButtonPage>(SystemResource.SwitchButtonPage);
            containerRegistry.RegisterForNavigation<TabControlPage>(SystemResource.TabControlPage); 
            containerRegistry.RegisterForNavigation<SliderPage>(SystemResource.SliderPage);
            containerRegistry.RegisterForNavigation<DataGridPage>(SystemResource.DataGridPage);
            containerRegistry.RegisterForNavigation<TreeViewPage>(SystemResource.TreeViewPage);
            containerRegistry.RegisterForNavigation<WaterfallFlowPanelPage>(SystemResource.WaterfallFlowPanelPage);
            containerRegistry.RegisterForNavigation<ScrollBarPage>(SystemResource.ScrollBarPage);
            var layDialog = Container.Resolve<ILayDialogService>();
            layDialog.RegisterDialog<Message>(nameof(Message));
        }
    }
}