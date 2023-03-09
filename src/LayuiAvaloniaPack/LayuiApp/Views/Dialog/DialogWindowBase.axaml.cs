using Avalonia.Controls;
using Prism.Services.Dialogs;

namespace LayuiApp.Views.Dialog
{
    public partial class DialogWindowBase : Window,IDialogWindow
    {
        public DialogWindowBase()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
