using Avalonia.Controls;
using Prism.Mvvm;

namespace Layui.Main.Views
{
    public partial class MessagePage : UserControl
    {
        public MessagePage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
