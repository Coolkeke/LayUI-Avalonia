using Avalonia.Collections;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Avalonia;
using LayUI.Avalonia.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Main.Controls
{
    public class LayGradientCarousel : TemplatedControl, ICarousel
    {

        LayGradientCarousel()
        {
            SubscribeToItems(_items);
        }
        static LayGradientCarousel()
        {
            SelectedIndexProperty.Changed.AddClassHandler<LayGradientCarousel>((o, e) => o.OnSelectedIndexChanged(e));
            ItemsProperty.Changed.AddClassHandler<LayGradientCarousel>((o, e) => o.OnItemsChanged(e));
        }
        public int SelectedIndex
        {
            get { return GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        private IEnumerable _items = new AvaloniaList<object>();
        [Content]
        public IEnumerable Items
        {
            get { return _items; }
            set { SetAndRaise(ItemsProperty, ref _items, value); }
        }
        /// <summary>
        /// 定义<see cref="string"/>属性
        /// </summary>
        public static readonly DirectProperty<LayGradientCarousel, IEnumerable> ItemsProperty =
        AvaloniaProperty.RegisterDirect<LayGradientCarousel, IEnumerable>(nameof(Items), o => o.Items);

        public int ItemCount
        {
            get { return GetValue(ItemCountProperty); }
            private set { SetValue(ItemCountProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly DirectProperty<LayGradientCarousel, int> ItemCountProperty =
       AvaloniaProperty.RegisterDirect<LayGradientCarousel, int>(nameof(ItemCount), o => o.ItemCount);


        public IDataTemplate ItemTemplate
        {
            get { return GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IDataTemplate"/>属性
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> ItemTemplateProperty =
       AvaloniaProperty.Register<LayGradientCarousel, IDataTemplate>(nameof(ItemTemplate));

        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly StyledProperty<int> SelectedIndexProperty =
       AvaloniaProperty.Register<LayGradientCarousel, int>(nameof(SelectedIndex));

        public AvaloniaObject GetContainerForItemOverride()
        {
            return new LayCarouselItem();
        }
        public bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayCarouselItem;
        }
        private void OnSelectedIndexChanged(AvaloniaPropertyChangedEventArgs e)
        {
            LayGradientCarousel carousel = (LayGradientCarousel)e.Sender;
        }

        private void OnItemsChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e == null) return;
            IEnumerable enumerable = (IEnumerable)e.OldValue;
            IEnumerable items = (IEnumerable)e.NewValue;
            INotifyCollectionChanged notifyCollectionChanged = enumerable as INotifyCollectionChanged;
            if (notifyCollectionChanged != null) notifyCollectionChanged.CollectionChanged -= (o, e) => ((LayGradientCarousel)o).OnItemsChanged(e);
            UpdateItemCount();
            SubscribeToItems(items);
        }
        /// <summary>
        /// 新增数据集通知效果
        /// </summary>
        /// <param name="items"></param>
        private void SubscribeToItems(IEnumerable items)
        {
            INotifyCollectionChanged notifyCollectionChanged = items as INotifyCollectionChanged;
            if (notifyCollectionChanged != null) notifyCollectionChanged.CollectionChanged += (o, e) => ((LayGradientCarousel)o).OnItemsChanged(e);
        }
        /// <summary>
        /// 刷新数量
        /// </summary>
        private void UpdateItemCount()
        {
            if (Items == null)
            {
                ItemCount = 0;
                return;
            }
            IList list = Items as IList;
            if (list != null) ItemCount = list.Count;
            else ItemCount = Enumerable.Count(Items.Cast<object>());
        }
        /// <summary>
        /// 数据集合项变化通知
        /// </summary>
        /// <param name="e"></param>
        private void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            UpdateItemCount();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    //AddControlItems(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    //RemoveControlItems(e.OldItems);
                    break;
            }
        }
        public void Next()
        {

        }

        public void Previous()
        {

        }
    }
}
