using Avalonia.Controls;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Layui.Main.Views
{
    public partial class TabControlPage : UserControl
    {
        private ObservableCollection<object> Tabs { get; } = new ObservableCollection<object> {
            new { Title="选项卡一",Message="" },
            new { Title="选项卡二",Message="" },
            new { Title="选项卡三",Message="" },
            new { Title="选项卡四",Message="" },
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
