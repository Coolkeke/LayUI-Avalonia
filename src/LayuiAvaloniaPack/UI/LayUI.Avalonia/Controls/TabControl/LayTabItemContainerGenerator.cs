using Avalonia.Controls.Generators;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using Avalonia.Reactive;
using Avalonia;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 重写Items项
    /// <para>当前类不提供二次开发使用,若想进行二开自己进行源码改造</para>
    /// <para>内部源码参考Avalonia原生TabItemContainerGenerator</para>
    /// </summary>
    internal class LayTabItemContainerGenerator : ItemContainerGenerator<LayTabItem>
    { 
        public LayTabItemContainerGenerator(LayTabControl owner)
            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
        {
            Owner = owner;
        }

        public new LayTabControl Owner { get; }

        protected override IControl CreateContainer(object item)
        {
            var tabItem = (LayTabItem)base.CreateContainer(item);

            tabItem.Bind(LayTabItem.TabStripPlacementProperty, new OwnerBinding<Dock>(
                tabItem,
                LayTabControl.TabStripPlacementProperty));

            if (tabItem.HeaderTemplate == null)
            {
                tabItem.Bind(LayTabItem.HeaderTemplateProperty, new OwnerBinding<IDataTemplate>(
                    tabItem,
                    LayTabControl.ItemTemplateProperty));
            }

            if (tabItem.Header == null)
            {
                if (item is IHeadered headered)
                {
                    tabItem.Header = headered.Header;
                }
                else
                {
                    if (!(tabItem.DataContext is IControl))
                    {
                        tabItem.Header = tabItem.DataContext;
                    }
                }
            }

            if (!(tabItem.Content is IControl))
            {
                tabItem.Bind(LayTabItem.ContentTemplateProperty, new OwnerBinding<IDataTemplate>(
                    tabItem,
                    LayTabControl.ContentTemplateProperty));
            }

            return tabItem;
        }

        private class OwnerBinding<T> : SingleSubscriberObservableBase<T>
        {
            private readonly LayTabItem _item;
            private readonly StyledProperty<T> _ownerProperty;
            private IDisposable _ownerSubscription;
            private IDisposable _propertySubscription;

            public OwnerBinding(LayTabItem item, StyledProperty<T> ownerProperty)
            {
                _item = item;
                _ownerProperty = ownerProperty;
            }

            protected override void Subscribed()
            {
                _ownerSubscription = ControlLocator.Track(_item, 0, typeof(LayTabControl)).Subscribe(OwnerChanged);
            }

            protected override void Unsubscribed()
            {
                _ownerSubscription?.Dispose();
                _ownerSubscription = null;
            }

            private void OwnerChanged(ILogical c)
            {
                _propertySubscription?.Dispose();
                _propertySubscription = null;

                if (c is LayTabControl LayTabControl)
                {
                    _propertySubscription = LayTabControl.GetObservable(_ownerProperty)
                        .Subscribe(x => PublishNext(x));
                }
            }
        }
    }
}
