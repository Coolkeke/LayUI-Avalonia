using Avalonia.Controls;
using Avalonia.Input;
using Prism.Mvvm;

namespace Layui.Main.Views
{
    public partial class KeyboardPage : UserControl
    {
        public KeyboardPage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
