using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MaskSurfPro.Resources;

namespace MaskSurfPro.ViewModels
{
    public class ProxyTipsWifiViewModel : ViewModelBase
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
            Translate();
        }
        public void Translate()
        {
            WifiHeadingText = AppStrings.SetupProxyWifi;
            WifiTip1Text = AppStrings.WifiTip1;
            WifiTip2Text = AppStrings.WifiTip2;
            WifiTip3Text = AppStrings.WifiTip3;
            WifiTip4Text = AppStrings.WifiTip4;
            WifiTip5Text = AppStrings.WifiTip5;
            WifiTip6Text = AppStrings.WifiTip6;
            WifiTip7Text = AppStrings.SwitchBackWarning;
        }
    }
}
