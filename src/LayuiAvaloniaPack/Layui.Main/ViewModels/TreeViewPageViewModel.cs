using Layui.Core;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Main.ViewModels
{
    public class TreeViewPageViewModel : ViewModelBase
    {
        private List<TreeViewData> _Items = new List<TreeViewData>() {
            new TreeViewData()
            {
                Title="Test",
                Items=new List<TreeViewData>()
                {
                    new TreeViewData()
                    {
                        Title="Test",
                        Items=new List<TreeViewData>()
                        {
                            new TreeViewData()
                            {
                                Title="Test"
                            }, new TreeViewData()
                            {
                                Title="Test"
                            }, new TreeViewData()
                            {
                                Title="Test",
                                Items=new List<TreeViewData>()
                                {
                                    new TreeViewData()
                                    {
                                        Title="Test"
                                    }, new TreeViewData()
                                    {
                                        Title="Test"
                                    }, new TreeViewData()
                                    {
                                        IsChecked=true,
                                        Title="Test"
                                    }
                                }
                            }
                        }
                    }, new TreeViewData()
                    {
                        Title="Test",
                        Items=new List<TreeViewData>()
                        {
                            new TreeViewData()
                            {
                                Title="Test"
                            }, new TreeViewData()
                            {
                                Title="Test"
                            }, new TreeViewData()
                            {
                                Title="Test"
                            }
                        }
                    }, new TreeViewData()
                    {
                        Title="Test"
                    }
                }
            },new TreeViewData(){
                Title="Test"
            },new TreeViewData(){
                Title="Test"
            },
        };
        public List<TreeViewData> Items
        {
            get
            {
                if (_Items == null) _Items = new List<TreeViewData>();
                return _Items;
            }
            set { SetProperty(ref _Items, value); }
        }
        public TreeViewPageViewModel(IContainerExtension container) : base(container)
        {
        }

        protected override void Loaded()
        {
        }

        protected override void Unloaded()
        {
        }
    }
    public class TreeViewData : BindableBase
    {
        private bool? _IsChecked = false;
        public bool? IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        private List<TreeViewData> _Items;
        public List<TreeViewData> Items
        {
            get
            {
                if (_Items == null) _Items = new List<TreeViewData>();
                return _Items;
            }
            set { SetProperty(ref _Items, value); }
        }
    }
}
