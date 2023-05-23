using Avalonia.Controls;

namespace BusinessApp.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Header.PointerPressed += Header_PointerPressed;
        }

        private void Header_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if (e.ClickCount > 1) return;
            BeginMoveDrag(e);
        }
    }
}