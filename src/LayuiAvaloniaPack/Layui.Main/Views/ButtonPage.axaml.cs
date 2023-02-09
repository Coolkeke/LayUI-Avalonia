using Avalonia.Controls;

namespace Layui.Main.Views
{
    public partial class ButtonPage : UserControl
    {
        public ButtonPage()
        {
            InitializeComponent();
            DetachedFromLogicalTree += ButtonPage_DetachedFromLogicalTree;
        }

        private void ButtonPage_DetachedFromLogicalTree(object sender, Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs e)
        {
           
        }
    }
}
