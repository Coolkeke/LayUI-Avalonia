using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;

namespace LayuiApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Resources.MergedDictionaries.Add(GetData("assembly://Languages/zh-cn.axaml"));
        }
        private ResourceDictionary GetData(string rawUri)
        {

            var appDirectory = System.IO.Directory.GetCurrentDirectory();
            Uri uri = new Uri($"{appDirectory}/{rawUri.Replace("assembly://", "")}");
            //var bit = new Bitmap(uri.LocalPath);
            return (ResourceDictionary)AvaloniaXamlLoader.Load(uri);
        }
    }
}
