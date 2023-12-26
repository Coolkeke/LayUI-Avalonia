using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interfaces
{
    internal static class ILayDialogWindowExtensions
    {
        internal static ILayDialogAware GetDialogViewModel(this ILayDialogWindow dialog)
        {
            return (ILayDialogAware)dialog.DataContext;
        }
        internal static ILayDialogAware GetDialogView(this ILayDialogWindow dialog)
        {
            return (ILayDialogAware)dialog.Content;
        }
    }
}
