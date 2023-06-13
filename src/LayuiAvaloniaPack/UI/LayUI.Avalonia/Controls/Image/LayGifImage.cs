using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using LayUI.Avalonia.Controls.GIF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// GIF动画
    /// <para>用于播放GIF动画</para>
    /// </summary>
    /// 借鉴开源项目 https://github.com/jmacato/AvaloniaGif/tree/skia-rendering
    public class LayGifImage : Control
    {
        public static readonly StyledProperty<string> SourceUriRawProperty =
            AvaloniaProperty.Register<LayGifImage, string>("SourceUriRaw");

        public static readonly StyledProperty<Uri> SourceUriProperty =
            AvaloniaProperty.Register<LayGifImage, Uri>("SourceUri");

        public static readonly StyledProperty<Stream> SourceStreamProperty =
            AvaloniaProperty.Register<LayGifImage, Stream>("SourceStream");

        public static readonly StyledProperty<IterationCount> IterationCountProperty =
            AvaloniaProperty.Register<LayGifImage, IterationCount>("IterationCount", IterationCount.Infinite);

        private GifInstance gifInstance;

        public static readonly StyledProperty<bool> AutoStartProperty =
            AvaloniaProperty.Register<LayGifImage, bool>("AutoStart");

        public static readonly StyledProperty<StretchDirection> StretchDirectionProperty =
            AvaloniaProperty.Register<LayGifImage, StretchDirection>("StretchDirection");

        public static readonly StyledProperty<Stretch> StretchProperty =
            AvaloniaProperty.Register<LayGifImage, Stretch>("Stretch");

        private RenderTargetBitmap backingRTB;
        private bool _hasNewSource;
        private object? _newSource;
        private Stopwatch _stopwatch;

        static LayGifImage()
        {
            SourceUriRawProperty.Changed.Subscribe(SourceChanged);
            SourceUriProperty.Changed.Subscribe(SourceChanged);
            SourceStreamProperty.Changed.Subscribe(SourceChanged);
            IterationCountProperty.Changed.Subscribe(IterationCountChanged);
            AutoStartProperty.Changed.Subscribe(AutoStartChanged);
            AffectsRender<LayGifImage>(SourceStreamProperty, SourceUriProperty, SourceUriRawProperty, StretchProperty);
            AffectsArrange<LayGifImage>(SourceStreamProperty, SourceUriProperty, SourceUriRawProperty, StretchProperty);
            AffectsMeasure<LayGifImage>(SourceStreamProperty, SourceUriProperty, SourceUriRawProperty, StretchProperty);
        }
        /// <summary>
        /// 图片原始地址
        /// </summary>
        public string SourceUriRaw
        {
            get => GetValue(SourceUriRawProperty);
            set => SetValue(SourceUriRawProperty, value);
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public Uri SourceUri
        {
            get => GetValue(SourceUriProperty);
            set => SetValue(SourceUriProperty, value);
        }
        /// <summary>
        /// 图片文件刘
        /// </summary>
        public Stream SourceStream
        {
            get => GetValue(SourceStreamProperty);
            set => SetValue(SourceStreamProperty, value);
        }
        /// <summary>
        /// 循环次数
        /// </summary>
        public IterationCount IterationCount
        {
            get => GetValue(IterationCountProperty);
            set => SetValue(IterationCountProperty, value);
        }
        /// <summary>
        /// 自动启动
        /// </summary>
        public bool AutoStart
        {
            get => GetValue(AutoStartProperty);
            set => SetValue(AutoStartProperty, value);
        }
        /// <summary>
        /// 拉伸方向
        /// </summary>
        public StretchDirection StretchDirection
        {
            get => GetValue(StretchDirectionProperty);
            set => SetValue(StretchDirectionProperty, value);
        }
        /// <summary>
        /// 图片填充
        /// </summary>
        public Stretch Stretch
        {
            get => GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        private static void AutoStartChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var image = e.Sender as LayGifImage;
            if (image == null)
                return;
        }

        private static void IterationCountChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var image = e.Sender as LayGifImage;
            if (image is null || e.NewValue is not IterationCount iterationCount)
                return;

            image.IterationCount = iterationCount;
        }

        public override void Render(DrawingContext context)
        {
            Dispatcher.UIThread.Post(InvalidateMeasure, DispatcherPriority.Background);
            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);

            if (_hasNewSource)
            {
                StopAndDispose();
                gifInstance = new GifInstance(_newSource);
                gifInstance.IterationCount = IterationCount;
                backingRTB = new RenderTargetBitmap(gifInstance.GifPixelSize, new Vector(96, 96));
                _hasNewSource = false;

                _stopwatch ??= new Stopwatch();
                _stopwatch.Reset();


                return;
            }

            if (gifInstance is null || (gifInstance.CurrentCts?.IsCancellationRequested ?? true))
            {
                return;
            }

            if (!_stopwatch.IsRunning)
            {
                _stopwatch.Start();
            }

            var currentFrame = gifInstance.ProcessFrameTime(_stopwatch.Elapsed);

            if (currentFrame is { } source && backingRTB is { })
            {
                using var ctx = backingRTB.CreateDrawingContext(null);
                var ts = new Rect(source.Size);
                ctx.DrawBitmap(source.PlatformImpl, 1, ts, ts);
            }

            if (backingRTB is not null && Bounds.Width > 0 && Bounds.Height > 0)
            {
                var viewPort = new Rect(Bounds.Size);
                var sourceSize = backingRTB.Size;

                var scale = Stretch.CalculateScaling(Bounds.Size, sourceSize, StretchDirection);
                var scaledSize = sourceSize * scale;
                var destRect = viewPort
                    .CenterRect(new Rect(scaledSize))
                    .Intersect(viewPort);

                var sourceRect = new Rect(sourceSize)
                    .CenterRect(new Rect(destRect.Size / scale));

                var interpolationMode = RenderOptions.GetBitmapInterpolationMode(this);

                context.DrawImage(backingRTB, sourceRect, destRect, interpolationMode);
            }
        }

        /// <summary>
        /// Measures the control.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var source = backingRTB;
            var result = new Size();

            if (source != null)
            {
                result = Stretch.CalculateSize(availableSize, source.Size, StretchDirection);
            }

            return result;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var source = backingRTB;

            if (source != null)
            {
                var sourceSize = source.Size;
                var result = Stretch.CalculateSize(finalSize, sourceSize);
                return result;
            }

            return new Size();
        }

        public void StopAndDispose()
        {
            gifInstance?.Dispose();
            backingRTB?.Dispose();
        }

        private static void SourceChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var image = e.Sender as LayGifImage;

            if (image == null)
                return; 
            if (e.NewValue is null)
            {
                return;
            }
            if (Design.IsDesignMode && e.NewValue is Uri uri&& uri.OriginalString.StartsWith("assembly://"))
            {
                return;
            }
            image._hasNewSource = true;
            image._newSource = e.NewValue;
            Dispatcher.UIThread.Post(image.InvalidateVisual, DispatcherPriority.Background);
        }
    }
}
