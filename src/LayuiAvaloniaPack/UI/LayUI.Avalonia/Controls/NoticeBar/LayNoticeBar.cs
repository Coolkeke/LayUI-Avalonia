using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Animation;
using Avalonia; 
using Avalonia.Media;
using Avalonia.Interactivity; 

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 通知栏
    /// </summary>
    [TemplatePart(Name = nameof(PART_ContentPresenter), Type = typeof(ContentPresenter))]
    [TemplatePart(Name = nameof(PART_Grid), Type = typeof(Grid))]
    public class LayNoticeBar : ContentControl, ILayControl
    {
        public LayNoticeBar()
        {
            DurationProperty.Changed.AddClassHandler<LayNoticeBar>((o, e) => o.InitAnimation());
        }
        private Grid? PART_Grid;
        private ContentPresenter? PART_ContentPresenter;

        /// <summary>
        /// Defines the <see cref="InnerLeftContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> InnerLeftContentProperty =
            AvaloniaProperty.Register<Control, object>(nameof(InnerLeftContent));

        /// <summary>
        /// 左侧填充内容
        /// </summary>
        public object InnerLeftContent
        {
            get { return GetValue(InnerLeftContentProperty); }
            set { SetValue(InnerLeftContentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="InnerRightContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> InnerRightContentProperty =
            AvaloniaProperty.Register<Control, object>(nameof(InnerRightContent));

        /// <summary>
        /// 右侧填充内容
        /// </summary>
        public object InnerRightContent
        {
            get { return GetValue(InnerRightContentProperty); }
            set { SetValue(InnerRightContentProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="Duration"/> property.
        /// </summary>
        public static readonly StyledProperty<double> DurationProperty =
            AvaloniaProperty.Register<Control, double>(nameof(Duration));

        /// <summary>
        /// 动画持续时间
        /// </summary>
        public double Duration
        {
            get { return GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_ContentPresenter = e.NameScope.Find<ContentPresenter>(nameof(PART_ContentPresenter));
            PART_Grid = e.NameScope.Find<Grid>(nameof(PART_Grid));
            if (PART_Grid != null && PART_Grid != null)
            {
                PART_Grid.Loaded -= PART_Grid_Loaded;
                PART_Grid.Loaded += PART_Grid_Loaded;
            }
        }
        private void PART_Grid_Loaded(object? sender, RoutedEventArgs e)
        {
            if (PART_Grid != null)
            {
                PART_Grid.Loaded -= PART_Grid_Loaded;
                InitAnimation();
            }
        }
        /// <summary>
        /// 初始化动画
        /// </summary>
        void InitAnimation()
        {
            if (!IsLoaded) return;
            if (Content == null) return;
            if (PART_Grid == null) return;
            if (PART_ContentPresenter == null) return;
            LayAnimationHelper.ExecuteAnimation(LayAnimationHelper.CreateAnimation(new KeyFrames
            {
                LayAnimationHelper.CreateKeyFrame(new Cue(0.0d), TranslateTransform.XProperty, PART_Grid?.Bounds.Width),
                LayAnimationHelper.CreateKeyFrame(new Cue(1.0d), TranslateTransform.XProperty, -PART_ContentPresenter?.Bounds.Width)
            }, Duration, IterationCount.Infinite), PART_ContentPresenter);
        }
        protected override void OnSizeChanged(SizeChangedEventArgs e)
        {
            base.OnSizeChanged(e);
            InitAnimation();
        }
    }
}
