using Avalonia.Controls;
using Prism.Mvvm;

namespace Layui.Main.Views
{
    public partial class LoadingPage : UserControl
    {
        public LoadingPage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
