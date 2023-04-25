using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 抽屉
    /// </summary>
    public class LayDrawer: ContentControl
    {
        static LayDrawer()
        {
            BoundsProperty.Changed.AddClassHandler<LayDrawer>((s,e)=> s.UpdateDesktopExpand()); 
        } 

        private void UpdateDesktopExpand()
        {
           
        }

        /// <summary>
        /// 开启抽屉
        /// </summary>
        public bool IsOpen
		{
			get { return GetValue(IsOpenProperty); }
			set { SetValue(IsOpenProperty, value); }
		}
		/// <summary>
		/// 定义<see cref="bool"/>属性
		/// </summary>
		public static readonly StyledProperty<bool> IsOpenProperty =
	   AvaloniaProperty.Register<LayDrawer, bool>(nameof(IsOpen), false);


	}
}
