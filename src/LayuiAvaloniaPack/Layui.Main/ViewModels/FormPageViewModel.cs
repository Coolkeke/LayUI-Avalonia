using Layui.Core.Mvvm;
using Prism.Ioc;

namespace Layui.Main.ViewModels
{
    public class FormPageViewModel : ViewModelBase
    {
        public FormPageViewModel(IContainerExtension container) : base(container) { } 
        private int _Value;


        public int Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
    }
}
