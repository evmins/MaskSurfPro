using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskSurfPro.ViewModels
{
    public class ProxyTipsWifiViewModel : FreshMvvm.FreshBasePageModel
    {

        //translation properties
        public string WifiHeadingText { get; set; }
        public string WifiTip1Text { get; set; }
        public string WifiTip2Text { get; set; }
        public string WifiTip3Text { get; set; }
        public string WifiTip4Text { get; set; }
        public string WifiTip5Text { get; set; }
        public string WifiTip6Text { get; set; }
        public string WifiTip7Text { get; set; }

        public ProxyTipsWifiViewModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Translate();
        }
        public void Translate()
        {
            //VersionLabelText = "Mask Surf Pro " + Translation.GetString("version") + " " + ((MSProApp)MSProApp.Current).Version;
            WifiHeadingText = Translation.GetString("Setup proxy Wifi");
            WifiTip1Text = Translation.GetString("Wifi tip 1");
            WifiTip2Text = Translation.GetString("Wifi tip 2");
            WifiTip3Text = Translation.GetString("Wifi tip 3");
            WifiTip4Text = Translation.GetString("Wifi tip 4");
            WifiTip5Text = Translation.GetString("Wifi tip 5");
            WifiTip6Text = Translation.GetString("Wifi tip 6");
            WifiTip7Text = Translation.GetString("Switch back warning");
        }
    }
}
