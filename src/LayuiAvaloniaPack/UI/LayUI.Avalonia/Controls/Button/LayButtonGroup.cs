using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Metadata;
using System.Collections.Specialized;
using System.Linq;
using System;
using Avalonia.Layout;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 按钮容器
    /// </summary>
    public class LayButtonGroup : TemplatedControl
    {
        private StackPanel PART_Panel = null;
        public LayButtonGroup()
        {
            Children.CollectionChanged += ChildrenChanged;
        }
        /// <summary>
        /// Defines the <see cref="Orientation"/> property.
        /// </summary>
        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<Control, Orientation>(nameof(Orientation));

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }


        private void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<Control> controls;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    controls = e.NewItems.OfType<Control>().ToList();
                    LogicalChildren.InsertRange(e.NewStartingIndex, controls);
                    VisualChildren.InsertRange(e.NewStartingIndex, e.NewItems.OfType<Visual>());
                    break;

                case NotifyCollectionChangedAction.Move:
                    LogicalChildren.MoveRange(e.OldStartingIndex, e.OldItems.Count, e.NewStartingIndex);
                    VisualChildren.MoveRange(e.OldStartingIndex, e.OldItems.Count, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    controls = e.OldItems.OfType<Control>().ToList();
                    LogicalChildren.RemoveAll(controls);
                    VisualChildren.RemoveAll(e.OldItems.OfType<Visual>());
                    break;

                case NotifyCollectionChangedAction.Replace:
                    for (var i = 0; i < e.OldItems.Count; ++i)
                    {
                        var index = i + e.OldStartingIndex;
                        var child = (IControl)e.NewItems[i];
                        LogicalChildren[index] = child;
                        VisualChildren[index] = child;
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    throw new NotSupportedException();
            }
            InvalidateMeasure();
        }

        [Content]
        public AvaloniaList<IControl> Children { get; } = new AvaloniaList<IControl>();

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_Panel = e.NameScope.Find("PART_Panel") as StackPanel;
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            PART_Panel?.Children?.Clear();
            if (PART_Panel != null)
            {
                foreach (var child in LogicalChildren)
                {
                    PART_Panel.Children.Add(child as Control);
                }
            }
        }
    }
}
