using Layui.Core;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;

namespace Layui.Main.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title => "欢迎使用 LayUI-Avalonia";
        public string Message => "项目地址：https://github.com/Layui-WPF-Team/Layui-WPF";
        public MainWindowViewModel(IContainerExtension provider) {
             
        }
    }
}