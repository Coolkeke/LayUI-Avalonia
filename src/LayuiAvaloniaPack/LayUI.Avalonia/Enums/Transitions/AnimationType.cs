﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Enums
{
    /// <summary>
    /// 过度动画枚举
    /// </summary>
    public enum AnimationType
    {
        /// <summary>
        /// 缩放
        /// </summary>
        Zoom,
        /// <summary>
        /// 渐变
        /// </summary>
        Gradient,
        /// <summary>
        /// 默认无效果
        /// </summary>
        Default,
        /// <summary>
        /// 底部滑入
        /// </summary>
        SlideInToBottom,
        /// <summary>
        /// 底部滑出
        /// </summary>
        SlideOutToBottom,
        /// <summary>
        /// 右侧滑入
        /// </summary>
        SlideInToRight,
        /// <summary>
        /// 右侧滑出
        /// </summary>
        SlideOutToRight,
        /// <summary>
        /// 顶部滑入
        /// </summary>
        SlideInToTop,
        /// <summary>
        /// 顶部滑出
        /// </summary>
        SlideOutToTop,
        /// <summary>
        /// 左侧滑入
        /// </summary>
        SlideInToLeft,
        /// <summary>
        /// 左侧滑出
        /// </summary>
        SlideOutToLeft,
        /// <summary>
        /// 旋转进入
        /// </summary>
        RotateIn,
        /// <summary>
        /// 旋转退入
        /// </summary>
        RotateOut
    }
}
