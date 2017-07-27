using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class ProxyTipsWifiPage : FreshMvvm.FreshBaseContentPage
    {
        public ProxyTipsWifiPage()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.ProxyTipsWifiVM;

            WifiImage1.Source = ImageSource.FromResource("MaskSurfPro.images.wifitip.png");
            WifiHeading.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            WifiTip1.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WifiTip2.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WifiTip3.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WifiTip4.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WifiTip5.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WifiTip6.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WifiTip7.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ProxyTipsWifiViewModel ptwfvm = MSProApp.Locator.ProxyTipsWifiVM;

            WifiHeading.Text = ptwfvm.WifiHeadingText;
            WifiTip1.Text = ptwfvm.WifiTip1Text;
            WifiTip2.Text = ptwfvm.WifiTip2Text;
            WifiTip3.Text = ptwfvm.WifiTip3Text;
            WifiTip4.Text = ptwfvm.WifiTip4Text;
            WifiTip5.Text = ptwfvm.WifiTip5Text;
            WifiTip6.Text = ptwfvm.WifiTip6Text;
            WifiTip7.Text = ptwfvm.WifiTip7Text;
        }
    }
}
