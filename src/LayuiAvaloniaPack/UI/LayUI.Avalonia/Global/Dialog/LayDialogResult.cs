using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Global
{
    /// <summary>
    /// 返回值
    /// </summary>
    public class LayDialogResult : ILayDialogResult
    {
        public LayDialogResult()
        {
            Result = ButtonResult.None;
        }
        public LayDialogResult(ButtonResult result) {
            this.Result= result;
        }
        public LayDialogResult(ButtonResult result, ILayDialogParameter parameters)
        {
            Result = result;
            Parameters = parameters;
        }
        public ButtonResult Result { get; set; }

        public ILayDialogParameter Parameters { get; set; }
    }
}
