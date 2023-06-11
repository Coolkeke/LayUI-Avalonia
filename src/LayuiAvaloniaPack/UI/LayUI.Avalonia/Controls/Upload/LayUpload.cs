using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls.Primitives;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Models;
using System.Collections;
using System.Collections.Generic;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 上传组件
    /// </summary>
    public class LayUpload:TemplatedControl
    {

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<UploadType> TypeProperty =
            AvaloniaProperty.Register<LayUpload, UploadType>(nameof(Type), UploadType.Image);

        /// <summary>
        /// 上传类型
        /// </summary>
        public UploadType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsDrag"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsDragProperty =
            AvaloniaProperty.Register<LayUpload, bool>(nameof(IsDrag), false);

        /// <summary>
        /// 开启拖拽上传
        /// </summary>
        public bool IsDrag
        {
            get { return GetValue(IsDragProperty); }
            set { SetValue(IsDragProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Suffix"/> property.
        /// </summary>
        public static readonly StyledProperty<string> SuffixProperty =
            AvaloniaProperty.Register<LayUpload, string>(nameof(Suffix));

        /// <summary>
        /// 上传后缀类型
        /// <para>多个类型用逗号（,）分隔开</para>
        /// <para>例如 jpeg,jpg</para>
        /// </summary>
        public string Suffix
        {
            get { return GetValue(SuffixProperty); }
            set { SetValue(SuffixProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Tip"/> property.
        /// </summary>
        public static readonly StyledProperty<string> TipProperty =
            AvaloniaProperty.Register<LayUpload, string>(nameof(Tip));

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tip
        {
            get { return GetValue(TipProperty); }
            set { SetValue(TipProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Limit"/> property.
        /// </summary>
        public static readonly StyledProperty<int> LimitProperty =
            AvaloniaProperty.Register<LayUpload, int>(nameof(Limit),5);

        /// <summary>
        /// 上传数量
        /// </summary>
        public int Limit
        {
            get { return GetValue(LimitProperty); }
            set { SetValue(LimitProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="FileList"/> property.
        /// </summary>
        public static readonly DirectProperty<LayUpload, AvaloniaList<FileDetails>> FileListProperty =
            AvaloniaProperty.RegisterDirect<LayUpload, AvaloniaList<FileDetails>>(nameof(FileList), o => o.FileList);

        /// <summary>
        /// 上传集合
        /// </summary>
        public AvaloniaList<FileDetails> FileList
        {
            get { return GetValue(FileListProperty); }
            private set { SetValue(FileListProperty, value); }
        }
    }
}
