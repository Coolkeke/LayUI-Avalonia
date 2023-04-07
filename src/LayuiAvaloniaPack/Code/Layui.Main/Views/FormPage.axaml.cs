using Avalonia.Controls;
using Prism.Mvvm;

namespace Layui.Main.Views
{
    public partial class FormPage : UserControl
    {
        public FormPage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
