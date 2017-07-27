using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using GalaSoft.MvvmLight;

using MaskSurfPro.Resources;

namespace MaskSurfPro.ViewModels
{
    public class ProxyTipsAPNViewModel : ViewModelBase
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
            Translate();
        }
        public void Translate()
        {
            APNHeadingText = AppStrings.SetupProxyAPN;
            APNTip1Text = AppStrings.APNTip1;
            APNTip2Text = AppStrings.APNTip2;
            APNTip3Text = AppStrings.APNTip3;
            APNTip4Text = AppStrings.APNTip4;
            APNTip5Text = AppStrings.APNTip5;
            APNTip6Text = AppStrings.APNTip6;
            APNTip7Text = AppStrings.SwitchBackWarning;
        }
    }
}
