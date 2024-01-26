using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class WaterfallFlowPanelPageViewModel : BindableBase
    {
        private List<string> _Images;
        public List<string> Images
        {
            get { return _Images; }
            set { SetProperty(ref _Images, value); }
        }
        public WaterfallFlowPanelPageViewModel()
        {
            _Images = new List<string>()
            {
                "https://img95.699pic.com/photo/40117/4482.jpg_wh860.jpg" ,
                "https://ts1.cn.mm.bing.net/th/id/R-C.66d7b796377883a92aad65b283ef1f84?rik=sQ%2fKoYAcr%2bOwsw&riu=http%3a%2f%2fwww.quazero.com%2fuploads%2fallimg%2f140305%2f1-140305131415.jpg&ehk=Hxl%2fQ9pbEiuuybrGWTEPJOhvrFK9C3vyCcWicooXfNE%3d&risl=&pid=ImgRaw&r=0" ,
                "https://bpic.588ku.com/back_origin_min_pic/20/04/19/f753e29e3dbe2ad75b8f6d6053199faa.jpg" ,
                "https://img.zcool.cn/community/0104c15cd45b49a80121416816f1ec.jpg@1280w_1l_2o_100sh.jpg" ,
                "https://ts1.cn.mm.bing.net/th/id/R-C.de7e1dcf98e725ab765c0b61478d8ff5?rik=7yqie%2fa32j2uxA&riu=http%3a%2f%2fb.zol-img.com.cn%2fdesk%2fbizhi%2fstart%2f3%2f1368604511486.jpg&ehk=JGWbrH30aClk4HeRboH7w99%2fMOhMX5m%2f7KepEepxVPE%3d&risl=&pid=ImgRaw&r=0" ,
                "https://pic1.zhimg.com/v2-0dda71bc9ced142bf7bb2d6adbebe4f0_r.jpg?source=1940ef5c" ,
                "https://sc.68design.net/photofiles/202102/2chxa2V1pR.jpg" ,
                "https://img.zcool.cn/community/014e485d6ba1dea801211f9e3e209a.jpg@2o.jpg" ,
                "https://tse4-mm.cn.bing.net/th/id/OIP-C.0_NEV1BFaG8yqw8RFHrZGgHaLA?rs=1&pid=ImgDetMain" ,
                "https://tse4-mm.cn.bing.net/th/id/OIP-C.pfw3xRtoTQGeRhwu0Yx26AHaEK?w=307&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse3-mm.cn.bing.net/th/id/OIP-C._vG1qetFbtpA5jkZeWNXEgHaEh?w=280&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse1-mm.cn.bing.net/th/id/OIP-C.NEEPSrA1OeV7yICbkFgF-wHaEK?w=289&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse3-mm.cn.bing.net/th/id/OIP-C.wsAw57przHBDWlveuvwYvAHaM8?w=119&h=185&c=7&r=0&o=5&pid=1.7" ,
                "https://tse4-mm.cn.bing.net/th/id/OIP-C.en4nrmZC4o_C92iCSo4mpQHaHa?w=190&h=191&c=7&r=0&o=5&pid=1.7" ,
                "https://tse3-mm.cn.bing.net/th/id/OIP-C.5rKLVnUrEqFL5NVtj7rtQAHaEK?w=282&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse3-mm.cn.bing.net/th/id/OIP-C.zRniBV3Ja8dH1Inl-77thAHaEo?w=287&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse4-mm.cn.bing.net/th/id/OIP-C.9ZXDvFGwn3wiJOcdn5rVTgHaEo?w=287&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse3-mm.cn.bing.net/th/id/OIP-C.fZz0i6JD_-LzMuke7HoliAHaEo?w=250&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse4-mm.cn.bing.net/th/id/OIP-C.Ftn59qVEc72KHgwTrOt3HgHaJ2?w=125&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse2-mm.cn.bing.net/th/id/OIP-C.NenFpn7La4XO63YJBOq3pwHaKy?w=116&h=180&c=7&r=0&o=5&pid=1.7" ,
                "https://tse3-mm.cn.bing.net/th/id/OIP-C.lhBT23N7hNJMCmbqhNr2DQHaFj?w=233&h=180&c=7&r=0&o=5&pid=1.7"
            };
        }
    }
}
