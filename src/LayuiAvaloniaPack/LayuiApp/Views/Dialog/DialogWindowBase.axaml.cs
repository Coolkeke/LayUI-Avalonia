using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Prism.Services.Dialogs;
using System.Linq;

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
