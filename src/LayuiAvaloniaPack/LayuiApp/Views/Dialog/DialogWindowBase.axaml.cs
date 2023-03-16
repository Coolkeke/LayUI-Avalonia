using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using LayUI.Avalonia.Controls;
using Prism.Services.Dialogs;
using System.Linq;

namespace LayuiApp.Views.Dialog
{
    public partial class DialogWindowBase : LayWindow, IDialogWindow
    {
        public DialogWindowBase()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
