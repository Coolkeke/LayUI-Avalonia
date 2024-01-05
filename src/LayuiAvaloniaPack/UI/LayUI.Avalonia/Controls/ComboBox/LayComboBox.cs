using Avalonia.Controls;  

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 下拉框
    /// </summary>
    public class LayComboBox: ComboBox, ILayControl
    {
        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<LayComboBoxItem>(item, out recycleKey);
        }
        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            return new LayComboBoxItem();
        }
    }
}
