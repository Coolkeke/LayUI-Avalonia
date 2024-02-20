using Avalonia.Controls;

namespace Layui.Main.Views
{
    public partial class DataGridPage : UserControl
    {
        public DataGridPage()
        {
            InitializeComponent();
        }

        private void LayPagination_Completed(object? sender, LayUI.Avalonia.Controls.PaginationRoutedEventArgs e)
        {
        }
    }
}
