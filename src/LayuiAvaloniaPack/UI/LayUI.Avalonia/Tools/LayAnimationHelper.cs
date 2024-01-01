using Avalonia.Data.Core;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Animation.Easings;
using Microsoft.VisualBasic;
using Avalonia.Threading;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Styling;

namespace LayUI.Avalonia
{
    /// <summary>
    /// 动画创建帮助类
    /// </summary>
    public class LayAnimationHelper
    {
        /// <summary>
        /// 创建关键帧
        /// </summary>
        /// <param name="cue"></param>
        /// <param name="property">被执行动画的依赖属性</param>
        /// <param name="value">执行动画所需要的值</param>
        /// <returns></returns>
        public static KeyFrame CreateKeyFrame(Cue cue, AvaloniaProperty property, object? value)
        {
            try
            {
                var keyFrame = new KeyFrame()
                {
                    Setters = { new Setter(property, value), },
                    Cue = cue
                };
                return keyFrame;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 常见几种动画创建解决方案
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration) => CreateAnimation(keyFrames, duration, null, null, null, null, null, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="easing">缓动函数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, Easing? easing) => CreateAnimation(keyFrames, duration, null, null, null, null, null, null, easing);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="iteration">动画重复次数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, IterationCount? iteration) => CreateAnimation(keyFrames, duration, null, null, null, null, iteration, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="iteration">动画重复次数</param>
        /// <param name="easing">缓动函数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, IterationCount? iteration, Easing? easing) => CreateAnimation(keyFrames, duration, null, null, null, null, iteration, null, easing);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay) => CreateAnimation(keyFrames, duration, delay, null, null, null, null, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="easing">缓动函数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, Easing? easing) => CreateAnimation(keyFrames, duration, delay, null, null, null, null, null, easing);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="iteration">动画重复次数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, IterationCount? iteration) => CreateAnimation(keyFrames, duration, delay, null, null, null, iteration, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param>
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="iteration">动画重复次数</param>
        /// <param name="easing">缓动函数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, IterationCount? iteration, Easing? easing) => CreateAnimation(keyFrames, duration, delay, null, null, null, iteration, null, easing);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="fillMode">动画运行后，属性如何持续保留或在运行之间的任何间隙中显示</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, FillMode? fillMode) => CreateAnimation(keyFrames, duration, null, null, null, fillMode, null, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="fillMode">动画运行后，属性如何持续保留或在运行之间的任何间隙中显示</param>
        /// <param name="iteration">动画重复次数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, FillMode? fillMode, IterationCount? iteration) => CreateAnimation(keyFrames, duration, null, null, null, fillMode, iteration, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="fillMode">动画运行后，属性如何持续保留或在运行之间的任何间隙中显示</param>
        /// <param name="iteration">动画重复次数</param>
        /// <param name="easing">缓动函数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, FillMode? fillMode, IterationCount? iteration, Easing? easing) => CreateAnimation(keyFrames, duration, null, null, null, fillMode, iteration, null, easing);
        #endregion
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="delayBetweenIteration">动画重复执行间隔时间（秒计算）</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, double? delayBetweenIteration) => CreateAnimation(keyFrames, duration, delay, delayBetweenIteration, null, null, null, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="delayBetweenIteration">动画重复执行间隔时间（秒计算）</param>
        /// <param name="speedRatio">速比（尚未发现很好的翻译，机翻）</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, double? delayBetweenIteration, double? speedRatio) => CreateAnimation(keyFrames, duration, delay, delayBetweenIteration, speedRatio, null, null, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="delayBetweenIteration">动画重复执行间隔时间（秒计算）</param>
        /// <param name="speedRatio">速比（尚未发现很好的翻译，机翻）</param>
        /// <param name="fillMode">动画运行后，属性如何持续保留或在运行之间的任何间隙中显示</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, double? delayBetweenIteration, double? speedRatio, FillMode? fillMode) => CreateAnimation(keyFrames, duration, delay, delayBetweenIteration, speedRatio, fillMode, null, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="delayBetweenIteration">动画重复执行间隔时间（秒计算）</param>
        /// <param name="speedRatio">速比（尚未发现很好的翻译，机翻）</param>
        /// <param name="fillMode">动画运行后，属性如何持续保留或在运行之间的任何间隙中显示</param>
        /// <param name="iteration">动画重复次数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, double? delayBetweenIteration, double? speedRatio, FillMode? fillMode, IterationCount? iteration) => CreateAnimation(keyFrames, duration, delay, delayBetweenIteration, speedRatio, fillMode, iteration, null, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="delayBetweenIteration">动画重复执行间隔时间（秒计算）</param>
        /// <param name="speedRatio">速比（尚未发现很好的翻译，机翻）</param>
        /// <param name="fillMode">动画运行后，属性如何持续保留或在运行之间的任何间隙中显示</param>
        /// <param name="iteration">动画重复次数</param>
        /// <param name="playback">动画播放方向</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, double? delayBetweenIteration, double? speedRatio, FillMode? fillMode, IterationCount? iteration, PlaybackDirection? playback) => CreateAnimation(keyFrames, duration, delay, delayBetweenIteration, speedRatio, fillMode, iteration, playback, null);
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="keyFrames">关键帧</param> 
        /// <param name="duration">执行动画消耗时间（秒计算）</param>
        /// <param name="delay">动画延迟执行时间（秒计算）</param>
        /// <param name="delayBetweenIteration">动画重复执行间隔时间（秒计算）</param>
        /// <param name="speedRatio">速比（尚未发现很好的翻译，机翻）</param>
        /// <param name="fillMode">动画运行后，属性如何持续保留或在运行之间的任何间隙中显示</param>
        /// <param name="iteration">动画重复次数</param>
        /// <param name="playback">动画播放方向</param>
        /// <param name="easing">缓动函数</param>
        /// <returns></returns>
        public static Animation CreateAnimation(KeyFrames keyFrames, double duration, double? delay, double? delayBetweenIteration, double? speedRatio, FillMode? fillMode, IterationCount? iteration, PlaybackDirection? playback, Easing? easing)
        {
            try
            {
                var animation = new Animation()
                {
                    Duration = TimeSpan.FromSeconds(duration),
                };
                if (delayBetweenIteration != null) animation.DelayBetweenIterations = TimeSpan.FromSeconds((double)delayBetweenIteration);
                if (playback != null) animation.PlaybackDirection = (PlaybackDirection)playback;
                if (fillMode != null) animation.FillMode = (FillMode)fillMode;
                if (delay != null) animation.Delay = TimeSpan.FromSeconds((double)delay);
                if (iteration != null) animation.IterationCount = (IterationCount)iteration;
                if (speedRatio != null) animation.SpeedRatio = (double)speedRatio;
                if (easing != null) animation.Easing = easing;
                if (keyFrames == null) return animation;
                foreach (var item in keyFrames)
                {
                    animation.Children.Add(item);
                }
                return animation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行动画
        /// </summary>
        /// <param name="animation">动画</param>
        /// <param name="element">需要被执行动画的控件</param>
        /// <param name="callBack">动画运行完成回调(异步回调)</param>
        public static void ExecuteAnimation(Animation animation, Control element) => ExecuteAnimation(animation, element, null);
        /// <summary>
        /// 执行动画
        /// </summary>
        /// <param name="animation">动画</param>
        /// <param name="element">需要被执行动画的控件</param>
        /// <param name="callBack">动画运行完成回调(异步回调)</param>
        public static void ExecuteAnimation(Animation animation, Control element, Action callBack)
        {
            try
            {
                if (animation == null) return;
                if (element == null) return;
                animation.RunAsync(element);
                var time = new DispatcherTimer(DispatcherPriority.Background);
                time.Interval = animation.Duration;
                EventHandler handler = null;
                handler= (o, e) =>
                {
                    time.Stop();
                    time.Tick -= handler;
                    callBack?.Invoke();
                };
                time.Tick += handler;
                time.Start();
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, nameof(LayAnimationHelper)).GetValueOrDefault().Log(nameof(ExecuteAnimation), "", ex);
            }
        }
    }
}
