using Avalonia.Controls;
using Prism.Mvvm;

namespace Layui.Main.Views
{
    public partial class NotificationPage : UserControl
    {
        public NotificationPage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
