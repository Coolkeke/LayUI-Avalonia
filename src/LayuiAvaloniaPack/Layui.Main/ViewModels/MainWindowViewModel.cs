using Layui.Core;
using Prism.Ioc;
using Prism.Mvvm; 
using System.ComponentModel;

namespace Layui.Main.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title => "AppTitle";
        public string Message => "项目地址：https://github.com/Coolkeke/LayUI-Avalonia/tree/LayUI-Avalonia-11.0.0";
        public MainWindowViewModel(IContainerExtension provider) {
             
        }
    }
}