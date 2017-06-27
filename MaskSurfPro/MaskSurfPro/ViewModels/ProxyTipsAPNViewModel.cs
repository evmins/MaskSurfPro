using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace MaskSurfPro.ViewModels
{
    public class ProxyTipsAPNViewModel : FreshMvvm.FreshBasePageModel
    {

        //translation properties
        public string APNHeadingText { get; set; }
        public string APNTip1Text { get; set; }
        public string APNTip2Text { get; set; }
        public string APNTip3Text { get; set; }
        public string APNTip4Text { get; set; }
        public string APNTip5Text { get; set; }
        public string APNTip6Text { get; set; }
        public string APNTip7Text { get; set; }

        public ProxyTipsAPNViewModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Translate();
        }
        public void Translate()
        {
            APNHeadingText = Translation.GetString("Setup proxy APN");
            APNTip1Text = Translation.GetString("APN tip 1");
            APNTip2Text = Translation.GetString("APN tip 2");
            APNTip3Text = Translation.GetString("APN tip 3");
            APNTip4Text = Translation.GetString("APN tip 4");
            APNTip5Text = Translation.GetString("APN tip 5");
            APNTip6Text = Translation.GetString("APN tip 6");
            APNTip7Text = Translation.GetString("Switch back warning");
        }
    }
}
