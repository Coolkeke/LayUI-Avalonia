using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia
{
    public class LayUIHelper
    {
        public static void SetIfUnset<T>(AvaloniaObject target, StyledProperty<T> property, T value)
        {
            if (!target.IsSet(property))
                target.SetCurrentValue(property, value);
        }
    }
}
