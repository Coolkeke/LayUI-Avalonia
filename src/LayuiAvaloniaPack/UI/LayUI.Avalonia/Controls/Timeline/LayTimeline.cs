using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using System.Collections.Specialized;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 时间线子项
    /// <para>摘抄Ursa控件库中的Timeline逻辑，这里就不再重写了</para>
    /// </summary>
    public class LayTimeline : ItemsControl, ILayControl
    {
        public LayTimeline()
        {
            ItemsView.CollectionChanged += ItemsViewOnCollectionChanged;
        }

        /// <summary>
        /// Defines the <see cref="IconCornerRadius"/> property.
        /// </summary>
        public static readonly StyledProperty<CornerRadius> IconCornerRadiusProperty =
            AvaloniaProperty.Register<LayTimeline, CornerRadius>(nameof(IconCornerRadius));

        /// <summary>
        /// 图标圆角效果
        /// </summary>
        public CornerRadius IconCornerRadius
        {
            get { return GetValue(IconCornerRadiusProperty); }
            set { SetValue(IconCornerRadiusProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="IconWidth"/> property.
        /// </summary>
        public static readonly StyledProperty<double> IconWidthProperty =
            AvaloniaProperty.Register<LayTimeline, double>(nameof(IconWidth));

        /// <summary>
        /// 图标可视化宽度
        /// </summary>
        public double IconWidth
        {
            get { return GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IconHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<double> IconHeightProperty =
            AvaloniaProperty.Register<LayTimeline, double>(nameof(IconHeight));

        /// <summary>
        /// 图标可视化高度
        /// </summary>
        public double IconHeight
        {
            get { return GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="LineColor"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> LineColorProperty =
            AvaloniaProperty.Register<LayTimeline, IBrush>(nameof(LineColor));

        /// <summary>
        /// 线的颜色
        /// </summary>
        public IBrush LineColor
        {
            get { return GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        private void ItemsViewOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshTimelineItems();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            RefreshTimelineItems();
        }

        /// <summary>
        /// Defines the <see cref="HeaderTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate?> HeaderTemplateProperty =
            AvaloniaProperty.Register<LayTimeline, IDataTemplate?>(nameof(HeaderTemplate));

        /// <summary>
        /// 头部模板
        /// </summary>
        public IDataTemplate? HeaderTemplate
        {
            get { return GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IconTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate?> IconTemplateProperty =
            AvaloniaProperty.Register<LayTimeline, IDataTemplate?>(nameof(IconTemplate));

        /// <summary>
        /// 图标模板
        /// </summary>
        public IDataTemplate? IconTemplate
        {
            get { return GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }
        /// <summary>
        /// 判断当前Item项中状态
        /// </summary>
        private void RefreshTimelineItems()
        {
            for (int i = 0; i < this.LogicalChildren.Count; i++)
            {
                if (this.LogicalChildren[i] is LayTimelineItem t)
                {
                    t.SetIndex(i == 0, i == this.LogicalChildren.Count - 1, i == 0 ? false : i == this.LogicalChildren.Count - 1 ? false : true);
                }
                else if (this.LogicalChildren[i] is ContentPresenter { Child: LayTimelineItem t2 })
                {
                    t2.SetIndex(i == 0, i == this.LogicalChildren.Count - 1, i == 0 ? false : i == this.LogicalChildren.Count - 1 ? false : true);
                }
            }
        }
        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<LayTimelineItem>(item, out recycleKey);
        }
        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            return new LayTimelineItem();
        }
        protected override void ClearContainerForItemOverride(Control container)
        {
            if (container is not LayTimelineItem hcc) return;
            hcc.ClearValue(ContentControl.ContentProperty);
            hcc.ClearValue(HeaderedContentControl.HeaderProperty);
            hcc.ClearValue(HeaderedContentControl.HeaderTemplateProperty);
            hcc.ClearValue(LayTimelineItem.IconTemplateProperty);
        }
        protected override void PrepareContainerForItemOverride(Control container, object? item, int index)
        {
            if (container == item) return;
            if (container is not LayTimelineItem) return; 
            if (ItemTemplate is not null)
            {
                LayUIHelper.SetIfUnset(container, LayTimelineItem.ContentProperty, item);
                LayUIHelper.SetIfUnset(container, LayTimelineItem.ContentTemplateProperty, ItemTemplate);
            }
            if (HeaderTemplate is not null)
            {
                LayUIHelper.SetIfUnset(container, LayTimelineItem.HeaderProperty, item);
                LayUIHelper.SetIfUnset(container, LayTimelineItem.HeaderTemplateProperty, HeaderTemplate);
            }
            if (IconTemplate is not null)
            {
                LayUIHelper.SetIfUnset(container, LayTimelineItem.IconProperty, item);
                LayUIHelper.SetIfUnset(container, LayTimelineItem.IconTemplateProperty, IconTemplate);
            }
        }
    }
}
