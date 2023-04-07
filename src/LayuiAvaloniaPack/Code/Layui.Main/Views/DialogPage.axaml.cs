using Avalonia.Controls;
using Prism.Mvvm;

namespace Layui.Main.Views
{
    public partial class DialogPage : UserControl
    {
        public DialogPage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
