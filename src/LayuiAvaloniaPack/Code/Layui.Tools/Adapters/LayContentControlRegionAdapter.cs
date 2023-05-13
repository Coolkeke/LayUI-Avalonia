using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using LayUI.Avalonia.Controls;
using Prism.DryIoc.Properties;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Adapters
{
    /// <summary>
    /// 自定义内容显示适配器
    /// </summary>
    public class LayContentControlRegionAdapter : RegionAdapterBase<LayContentControl>
    {
        public LayContentControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {

        }
        protected override void Adapt(IRegion region, LayContentControl regionTarget)
        {
            if (regionTarget == null)
                throw new ArgumentNullException(nameof(regionTarget));

            bool contentIsSet = regionTarget.Content != null;
            contentIsSet = contentIsSet || regionTarget[LayContentControl.ContentProperty] != null;

            if (contentIsSet)
                throw new InvalidOperationException("LayContentControl有内容异常");

            region.ActiveViews.CollectionChanged += delegate
            {
                regionTarget.Content = region.ActiveViews.FirstOrDefault();
            };

            region.Views.CollectionChanged +=
                (sender, e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add && region.ActiveViews.Count() == 0)
                    {
                        region.Activate(e.NewItems[0]);
                    }
                };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
