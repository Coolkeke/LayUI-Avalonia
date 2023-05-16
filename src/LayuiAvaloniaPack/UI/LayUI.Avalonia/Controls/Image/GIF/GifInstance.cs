using LayUI.Avalonia.Controls.GIF.Decoding;
using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Animation;
using System.Threading;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Logging;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LayUI.Avalonia.Controls.GIF
{
    public class GifInstance : IDisposable
    {
        public IterationCount IterationCount { get; set; }
        public bool AutoStart { get; private set; } = true;
        private readonly GifDecoder _gifDecoder;
        private readonly WriteableBitmap _targetBitmap;
        private TimeSpan _totalTime;
        private readonly List<TimeSpan> _frameTimes;
        private uint _iterationCount;
        private int _currentFrameIndex;

        public CancellationTokenSource CurrentCts { get; }

        internal GifInstance(object newValue) : this(newValue switch
        { 
            Stream s => s,
            Uri u => GetStreamFromUri(u),
            string str => GetStreamFromString(str),
            _ => throw new InvalidDataException("Unsupported source object")
        })
        { }

        public GifInstance(string uri) : this(GetStreamFromString(uri))
        { }

        public GifInstance(Uri uri) : this(GetStreamFromUri(uri))
        { }

        public GifInstance(Stream currentStream)
        {
            if (!currentStream.CanSeek)
                throw new InvalidDataException("The provided stream is not seekable.");

            if (!currentStream.CanRead)
                throw new InvalidOperationException("Can't read the stream provided.");

            currentStream.Seek(0, SeekOrigin.Begin);

            CurrentCts = new CancellationTokenSource();

            _gifDecoder = new GifDecoder(currentStream, CurrentCts.Token);
            var pixSize = new PixelSize(_gifDecoder.Header.Dimensions.Width, _gifDecoder.Header.Dimensions.Height);

            _targetBitmap = new WriteableBitmap(pixSize, new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Opaque);
            GifPixelSize = pixSize;

            _totalTime = TimeSpan.Zero;

            _frameTimes = _gifDecoder.Frames.Select(frame =>
            {
                _totalTime = _totalTime.Add(frame.FrameDelay);
                return _totalTime;
            }).ToList();

            _gifDecoder.RenderFrame(0, _targetBitmap);

            // Save the color table cache ID's to refresh them on cache while
            // // the image is either stopped/paused.
            // _colorTableIdList = _gifDecoder.Frames
            //     .Where(p => p.IsLocalColorTableUsed)
            //     .Select(p => p.LocalColorTableCacheID)
            //     .ToList();

            // if (_gifDecoder.Header.HasGlobalColorTable)
            //     _colorTableIdList.Add(_gifDecoder.Header.GlobalColorTableCacheID);
        }

        private static Stream GetStreamFromString(string str)
        {
            if (!Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out var res))
            {
                throw new InvalidCastException("The string provided can't be converted to URI.");
            }

            return GetStreamFromUri(res);
        }
        public static Stream FileToStream(string fileName)
        {
            try
            {
                // 打开文件
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                // 读取文件的 byte[] 
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                // 把 byte[] 转换成 Stream 
                Stream stream = new MemoryStream(bytes);
                return stream;
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }
        private static Stream GetStreamFromUri(Uri uri)
        {
            var uriString = uri.OriginalString.Trim();
            if (uriString.StartsWith("assembly://"))
            {
                var appDirectory = System.IO.Directory.GetCurrentDirectory();
                var localUri = $"{appDirectory}/{uriString.Replace("assembly://", "")}";
                return FileToStream(localUri);
            }
            else if (uriString.StartsWith("resm") || uriString.StartsWith("avares"))
            {
                var assetLocator = AvaloniaLocator.Current.GetService<IAssetLoader>();

                if (assetLocator is null)
                    throw new InvalidDataException(
                        "The resource URI was not found in the current assembly.");

                return assetLocator.Open(uri);
            }
            return FileToStream(uriString);
        }

        public int GifFrameCount => _frameTimes.Count;

        public PixelSize GifPixelSize { get; }

        public void Dispose()
        {
            CurrentCts.Cancel();
            _targetBitmap?.Dispose();
        }

        [CanBeNull]
        public WriteableBitmap ProcessFrameTime(TimeSpan stopwatchElapsed)
        {
            if (!IterationCount.IsInfinite && _iterationCount > IterationCount.Value)
            {
                return null;
            }

            if (CurrentCts.IsCancellationRequested)
            {
                return null;
            }

            var elapsedTicks = stopwatchElapsed.Ticks;
            var timeModulus = TimeSpan.FromTicks(elapsedTicks % _totalTime.Ticks);
            var targetFrame = _frameTimes.FirstOrDefault(x => timeModulus < x);
            var currentFrame = _frameTimes.IndexOf(targetFrame);
            if (currentFrame == -1) currentFrame = 0;

            if (_currentFrameIndex == currentFrame)
                return _targetBitmap;

            _iterationCount = (uint)(elapsedTicks / _totalTime.Ticks);

            return ProcessFrameIndex(currentFrame);
        }

        internal WriteableBitmap ProcessFrameIndex(int frameIndex)
        {
            _gifDecoder.RenderFrame(frameIndex, _targetBitmap);
            _currentFrameIndex = frameIndex;

            return _targetBitmap;
        }
    }
}