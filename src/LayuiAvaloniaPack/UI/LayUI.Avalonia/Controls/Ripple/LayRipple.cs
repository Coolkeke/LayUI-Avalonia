using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using LayUI.Avalonia.Enums; 

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 波浪
    /// </summary>
    [TemplatePart(Name = nameof(PART_Border), Type = typeof(Border))]
    public class LayRipple : ContentControl, ILayControl
    {
        public LayRipple()
        {
            TypeProperty.Changed.AddClassHandler<LayRipple>((o, e) => o.OnTypeChanged());
        }

        private void OnTypeChanged()
        {
            if (IsLoaded) ExecuteAnimation(Type);
        }

        private Border? PART_Border;

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<RippleType> TypeProperty =
            AvaloniaProperty.Register<Control, RippleType>(nameof(Type));

        /// <summary>
        /// 类型
        /// </summary>
        public RippleType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_Border = e.NameScope.Find<Border>(nameof(PART_Border));
            ExecuteAnimation(Type);
        }

        private void ExecuteAnimation(RippleType type)
        {
            ExecuteRemoveAnimation();
            switch (type)
            {
                case RippleType.Auto:
                    ExecuteAutoAnimation();
                    break;
                case RippleType.Default:
                default:
                    ExecuteDefaultAnimation();
                    break;
            }
        }
        /// <summary>
        /// 无背景色扩散动画
        /// </summary>
        private void ExecuteDefaultAnimation()
        {
            if (Type != RippleType.Default) return;
            ExecuteRemoveAnimation();
        }
        /// <summary>
        /// 移除动画
        /// <para>并不算是真正的移除</para>
        /// </summary>
        private void ExecuteAutoAnimation()
        {
            if (PART_Border == null) return;
            if (Type != RippleType.Auto) return; 
            LayAnimationHelper.ExecuteAnimation(LayAnimationHelper.CreateAnimation(new KeyFrames
            { 
                LayAnimationHelper.CreateKeyFrame(new Cue(1.0d), ScaleTransform.ScaleXProperty, 1.4), 
                LayAnimationHelper.CreateKeyFrame(new Cue(1.0d), ScaleTransform.ScaleYProperty, 1.4),
                LayAnimationHelper.CreateKeyFrame(new Cue(1.0d), Border.BackgroundProperty, Brushes.Transparent),
            }, 1, IterationCount.Infinite), PART_Border);
        }
        /// <summary>
        /// 执行点击背景色扩散动画
        /// </summary>
        private void ExecuteRemoveAnimation()
        {
            if (PART_Border == null) return;
        }
        private bool isLoadClickAnimation = true;
        /// <summary>
        /// 执行点击背景色扩散动画
        /// </summary>
        private void ExecuteClickAnimation()
        {
            if (!isLoadClickAnimation) return;
            isLoadClickAnimation = false;
            if (PART_Border == null) return;
            if (Type != RippleType.Click) return; 
            LayAnimationHelper.ExecuteAnimation(LayAnimationHelper.CreateAnimation(new KeyFrames
            { 
                LayAnimationHelper.CreateKeyFrame(new Cue(1.0d), ScaleTransform.ScaleXProperty, 1.4), 
                LayAnimationHelper.CreateKeyFrame(new Cue(1.0d), ScaleTransform.ScaleYProperty, 1.4),
                LayAnimationHelper.CreateKeyFrame(new Cue(1.0d), Border.BackgroundProperty, Brushes.Transparent), 
            }, 1), PART_Border, () =>
            {
                isLoadClickAnimation = true;
            });
        }
        protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
        {
            base.OnPointerCaptureLost(e);
            ExecuteClickAnimation();
        }
    }
}
