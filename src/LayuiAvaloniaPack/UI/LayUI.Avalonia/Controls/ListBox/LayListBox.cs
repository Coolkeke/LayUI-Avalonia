using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayListBox : ListBox, ILayControl
    {
        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<LayListBoxItem>(item, out recycleKey);
        }
        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            return new LayListBoxItem();
        }
        protected override void ContainerForItemPreparedOverride(Control container, object? item, int index)
        {
            if (container is LayListBoxItem layListBoxItem)
            {
                if (layListBoxItem.Number != index + 1) layListBoxItem.Number = index + 1;
            }
            base.ContainerForItemPreparedOverride(container, item, index);
        }
        protected override void PrepareContainerForItemOverride(Control container, object? item, int index)
        {
            if (container is LayListBoxItem layListBoxItem)
            {
                if (layListBoxItem.Number != index + 1) layListBoxItem.Number = index + 1;
            }
            base.PrepareContainerForItemOverride(container, item, index);
        }
        protected override void ContainerIndexChangedOverride(Control container, int oldIndex, int newIndex)
        {
            if (container is LayListBoxItem layListBoxItem)
            {
                if (layListBoxItem.Number != newIndex + 1) layListBoxItem.Number = newIndex + 1;
            }
            base.ContainerIndexChangedOverride(container, oldIndex, newIndex);
        }
    }
}
