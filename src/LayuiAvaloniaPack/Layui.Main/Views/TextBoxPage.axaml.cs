using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.TextInput;
using LayUI.Avalonia.Tools;

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
            LayKeyboardHelper.SetText("1");
        }
    }
}
