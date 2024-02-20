using Avalonia;
using Avalonia.Automation.Peers;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayDataGrid: DataGrid, ILayControl
    {  
        
        /// <summary>
        /// Defines the <see cref="ColumnHeaderBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ColumnHeaderBackgroundProperty =
            AvaloniaProperty.Register<LayDataGrid, IBrush>(nameof(ColumnHeaderBackground));

        /// <summary>
        /// 
        /// </summary>
        public IBrush ColumnHeaderBackground
        {
            get { return GetValue(ColumnHeaderBackgroundProperty); }
            set { SetValue(ColumnHeaderBackgroundProperty, value); }
        } 
    }
}
