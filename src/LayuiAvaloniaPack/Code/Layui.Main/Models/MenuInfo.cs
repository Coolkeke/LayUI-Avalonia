using System;
using System.Collections.Generic;
using Prism.Mvvm;

namespace Layui.Main.Models
{
	/// <summary>
	/// 菜单子项Model
	/// </summary>
	public class MenuInfo : BindableBase
	{
		private string _FontIcon;
		/// <summary>
		/// 字体图标代码
		/// </summary>
		public string FontIcon
        {
			get { return _FontIcon; }
			set { SetProperty(ref _FontIcon, value); }
		}
		private string _PageKey;
		/// <summary>
		/// 视图Key
		/// </summary>
		public string PageKey
        {
			get { return _PageKey; }
			set { SetProperty(ref _PageKey, value); }
		}
		private string _Title;
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
        {
			get { return _Title; }
			set { SetProperty(ref _Title, value); }
		}
	}
}