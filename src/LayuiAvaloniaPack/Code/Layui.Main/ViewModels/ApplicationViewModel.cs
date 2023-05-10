using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data;
using Prism.Commands;
using Prism.Mvvm; 

namespace Layui.Main.ViewModels
{
    public class ApplicationViewModel: BindableBase
    {
        private DelegateCommand _ExitCommand;
        public DelegateCommand ExitCommand =>
            _ExitCommand ?? (_ExitCommand = new DelegateCommand(ExecuteExitCommand));

        void ExecuteExitCommand()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                lifetime.Shutdown();
            }
        }
    }
}
