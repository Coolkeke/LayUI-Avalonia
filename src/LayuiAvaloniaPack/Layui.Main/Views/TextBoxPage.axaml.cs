using Avalonia.Controls;
using Avalonia.Input;
using LayUI.Avalonia;

namespace Layui.Main.Views
{
    public partial class TextBoxPage : UserControl
    {
        public TextBoxPage()
        {
            InitializeComponent();
        }

        private void LayButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            LayKeyboardHelper.SetText("123123");
            LayKeyboardHelper.SetKey(Key.A);
            LayKeyboardHelper.SetKey(Key.X);
        }
    }
}
