using Avalonia.Controls;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Layui.Main.Views
{
    public partial class TabControlPage : UserControl
    {
        private ObservableCollection<object> Tabs { get; } = new ObservableCollection<object> {
            new { Title="ѡ�һ",Message="" },
            new { Title="ѡ���",Message="" },
            new { Title="ѡ���",Message="" },
            new { Title="ѡ���",Message="" },
        };
        public TabControlPage()
        {
            InitializeComponent();
            if (!Design.IsDesignMode)
                ViewModelLocator.SetAutoWireViewModel(this, true);
            else DataContext = this;
        }
    }
}
