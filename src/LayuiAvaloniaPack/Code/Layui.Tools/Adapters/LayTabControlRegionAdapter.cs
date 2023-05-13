using Avalonia.Controls;
using LayUI.Avalonia.Controls;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Adapters
{
    public class LayTabControlRegionAdapter : RegionAdapterBase<LayTabControl>
    {
        public LayTabControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {

        }
        protected override void Adapt(IRegion region, LayTabControl regionTarget)
        {
            
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
