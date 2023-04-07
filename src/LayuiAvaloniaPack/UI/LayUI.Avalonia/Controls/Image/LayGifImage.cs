using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using LayUI.Avalonia.Controls.GIF;
using LayUI.Avalonia.Controls.GIF.Decoding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using static Avalonia.Markup.Xaml.AvaloniaXamlLoader;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// GIF动画
    /// <para>用于播放GIF动画</para>
    /// </summary>
    /// 借鉴开源项目 https://github.com/jmacato/AvaloniaGif/tree/skia-rendering
    public class LayGifImage : Control
    {
        public Image TargetControl { get; set; }
        public Stream Stream { get; private set; }
        public IterationCount IterationCount { get; private set; }
        public bool AutoStart { get; private set; } = true;
        public Progress<int> Progress { get; private set; }
        bool _streamCanDispose;
        private GifDecoder _gifDecoder;
        private GifBackgroundWorker _bgWorker;
        private WriteableBitmap _targetBitmap;
        private volatile bool _hasNewFrame;
        private bool _isDisposed;
        private readonly object _bitmapSync = new object();
        private static readonly object _globalUIThreadUpdateLock = new object();

        /// <summary>
        /// Defines the <see cref="Source"/> property.
        /// </summary>
        public static readonly StyledProperty<Uri> SourceProperty =
            AvaloniaProperty.Register<LayGifImage, Uri>(nameof(Source));

        /// <summary>
        /// Defines the <see cref="Stretch"/> property.
        /// </summary>
        public static readonly StyledProperty<Stretch> StretchProperty =
            AvaloniaProperty.Register<LayGifImage, Stretch>(nameof(Stretch), Stretch.Uniform);

        static LayGifImage()
        {
            AffectsRender<LayGifImage>(SourceProperty, StretchProperty);
            AffectsMeasure<LayGifImage>(SourceProperty, StretchProperty);
            SourceProperty.Changed.AddClassHandler<LayGifImage>(SourceChanged);
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public Uri Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// 获取或设置一个值，该值控制图像的拉伸方式。
        /// </summary>
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        /// <summary>
        /// 渲染控件。
        /// </summary>
        /// <param name="context">The drawing context.</param>
        public override void Render(DrawingContext context)
        {
            var source = _targetBitmap;

            if (source != null)
            {
                if (_hasNewFrame)
                {
                    using (var lockedBitmap = _targetBitmap?.Lock())
                        _gifDecoder?.WriteBackBufToFb(lockedBitmap.Address);
                    _hasNewFrame = false;
                }

                Rect viewPort = new Rect(Bounds.Size);
                Size sourceSize = new Size(source.PixelSize.Width, source.PixelSize.Height);
                Vector scale = Stretch.CalculateScaling(Bounds.Size, sourceSize);
                Size scaledSize = sourceSize * scale;
                Rect destRect = viewPort
                    .CenterRect(new Rect(scaledSize))
                    .Intersect(viewPort);
                Rect sourceRect = new Rect(sourceSize)
                    .CenterRect(new Rect(destRect.Size / scale));

                var interpolationMode = RenderOptions.GetBitmapInterpolationMode(this);

                context.DrawImage(source, sourceRect, destRect, interpolationMode);
            }
        }

        /// <summary>
        /// 测量控制。
        /// </summary>
        /// <param name="availableSize">可用大小。</param>
        /// <returns>控件的所需大小。</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var source = _targetBitmap;

            if (source != null)
            {
                Size sourceSize = new Size(source.PixelSize.Width, source.PixelSize.Height);
                if (double.IsInfinity(availableSize.Width) || double.IsInfinity(availableSize.Height))
                {
                    return sourceSize;
                }
                else
                {
                    return Stretch.CalculateSize(availableSize, sourceSize);
                }
            }
            else
            {
                return new Size();
            }
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var source = _targetBitmap;

            if (source != null)
            {
                var sourceSize = new Size(source.PixelSize.Width, source.PixelSize.Height);
                var result = Stretch.CalculateSize(finalSize, sourceSize);
                return result;
            }
            else
            {
                return new Size();
            }
        }

        private static void SourceChanged(LayGifImage arg1, AvaloniaPropertyChangedEventArgs _)
        {
            arg1.SetSource(_.NewValue);
        }

        private async void SetSource(object newValue)
        {
            var sourceUri = newValue as Uri;
            var sourceStr = newValue as Stream;

            _bgWorker?.SendCommand(BgWorkerCommand.Dispose);
            _gifDecoder?.Dispose();

            Stream stream = null;

            if (sourceUri != null)
            {
                _streamCanDispose = true;
                this.Progress = new Progress<int>();

                if (sourceUri.OriginalString.Trim().StartsWith("resm"))
                {
                    var assetLocator = AvaloniaLocator.Current.GetService<IAssetLoader>();
                    stream = assetLocator.Open(sourceUri);
                }
                else if (sourceUri.OriginalString.Trim().StartsWith("avares"))
                {
                    var assetLocator = AvaloniaLocator.Current.GetService<IAssetLoader>();
                    var data = assetLocator.GetAssembly(sourceUri);
                    //stream = ;
                }
                else
                {
                    stream = (await WebRequest.Create(sourceUri).GetResponseAsync()).GetResponseStream();
                }

            }
            else if (sourceStr != null)
            {
                stream = sourceStr;
            }
            else
            {
                throw new InvalidDataException("Missing valid URI or Stream.");
            }

            Stream = stream;
            this._gifDecoder = new GifDecoder(Stream);
            this._bgWorker = new GifBackgroundWorker(_gifDecoder);
            var pixSize = new PixelSize(_gifDecoder.Header.Dimensions.Width, _gifDecoder.Header.Dimensions.Height);
            this._targetBitmap = new WriteableBitmap(pixSize, new Vector(96, 96), PixelFormat.Bgra8888);

            _bgWorker.CurrentFrameChanged += FrameChanged;

            Run();
        }

        private void FrameChanged()
        {
            _hasNewFrame = true;
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }

        private void Run()
        {
            if (!Stream.CanSeek)
                throw new ArgumentException("The stream is not seekable");

            this._bgWorker?.SendCommand(BgWorkerCommand.Play);
            InvalidateVisual();
        }

        public void IterationCountChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var newVal = (IterationCount)e.NewValue;
            this.IterationCount = newVal;
        }

        public void AutoStartChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var newVal = (bool)e.NewValue;
            this.AutoStart = newVal;
        }
        public void Dispose()
        {
            _isDisposed = true;
            this._bgWorker?.SendCommand(BgWorkerCommand.Dispose);
            _targetBitmap?.Dispose();
        }
    }
}
