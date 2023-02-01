using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LayuiApp.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace LayuiApp
{
    public partial class App : PrismApplication
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ButtonPage>(nameof(ButtonPage));
            containerRegistry.RegisterForNavigation<FormPage>(nameof(FormPage));
        }
    }
}
