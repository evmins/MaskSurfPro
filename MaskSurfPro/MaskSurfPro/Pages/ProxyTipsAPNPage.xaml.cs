using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class ProxyTipsAPNPage : FreshMvvm.FreshBaseContentPage
    {
        public ProxyTipsAPNPage()
        {
            InitializeComponent();

            APNImage1.Source = ImageSource.FromResource("MaskSurfPro.images.apntip1.png");
            APNImage2.Source = ImageSource.FromResource("MaskSurfPro.images.apntip2.png");
            APNImage3.Source = ImageSource.FromResource("MaskSurfPro.images.apntip3.png");
            APNImage4.Source = ImageSource.FromResource("MaskSurfPro.images.apntip4.png");
            APNHeading.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            APNTip1.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            APNTip2.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            APNTip3.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            APNTip4.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            APNTip5.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            APNTip6.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            APNTip7.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ProxyTipsAPNViewModel ptapnvm = ((MSProApp)Application.Current).PTAPNVM;

            APNHeading.Text = ptapnvm.APNHeadingText;
            APNTip1.Text = ptapnvm.APNTip1Text;
            APNTip2.Text = ptapnvm.APNTip2Text;
            APNTip3.Text = ptapnvm.APNTip3Text;
            APNTip4.Text = ptapnvm.APNTip4Text;
            APNTip5.Text = ptapnvm.APNTip5Text;
            APNTip6.Text = ptapnvm.APNTip6Text;
            APNTip7.Text = ptapnvm.APNTip7Text;
        }
    }
}
