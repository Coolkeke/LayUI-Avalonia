using Avalonia.Controls;
using Prism.Mvvm;
using System.Collections;

namespace Layui.Main.Views
{
    public partial class ListPage : UserControl
    {
        public ListPage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
