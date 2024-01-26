using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 评分列表
    /// </summary>
    public class RateItemsControl:ItemsControl, ILayControl
    {
        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<RateItem>(item, out recycleKey);
        }
        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            return new RateItem();
        }
    }
}
