using System;
using System.Collections.Generic;
using Prism.Mvvm;

namespace Layui.Main.Models
{
	/// <summary>
	/// �˵�����Model
	/// </summary>
	public class MenuInfo : BindableBase
	{
		private string _FontIcon;
		/// <summary>
		/// ����ͼ�����
		/// </summary>
		public string FontIcon
        {
			get { return _FontIcon; }
			set { SetProperty(ref _FontIcon, value); }
		}
		private string _PageKey;
		/// <summary>
		/// ��ͼKey
		/// </summary>
		public string PageKey
        {
			get { return _PageKey; }
			set { SetProperty(ref _PageKey, value); }
		}
		private string _Title;
		/// <summary>
		/// ����
		/// </summary>
		public string Title
        {
			get { return _Title; }
			set { SetProperty(ref _Title, value); }
		}
	}
}