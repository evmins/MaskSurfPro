using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class AboutPageSW300 : FreshMvvm.FreshBaseContentPage
    {
        public AboutPageSW300()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.AboutVM;

            VersionLabel.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            SNLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            DisclaimerLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            DisclaimerText.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            CopyrightLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            SystemDescLabel.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));

            FBLogo.Source = ImageSource.FromResource("MaskSurfPro.images.fblogo.png");
            GPLogo.Source = ImageSource.FromResource("MaskSurfPro.images.gplogo.png");

            TapGestureRecognizer tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += (s, e) =>
            {
                Device.OpenUri(new Uri("https://facebook.com/MaskSurf"));
            };
            FBLogo.GestureRecognizers.Add(tapGestureRecognizer1);

            TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += (s, e) =>
            {
                Device.OpenUri(new Uri("https://plus.google.com/112133005781438523926"));
            };
            GPLogo.GestureRecognizers.Add(tapGestureRecognizer2);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AboutViewModel avm = MSProApp.Locator.AboutVM;

            VersionLabel.Text = avm.VersionLabelText;
            SystemDescLabel.Text = avm.SystemDescLabelText;
            SNLabel.Text = avm.SNLabelText;
            DisclaimerLabel.Text = avm.DisclaimerLabelText;
            DisclaimerText.Text = avm.DisclaimerText;
            CopyrightLabel.Text = avm.CopyrightLabelText;
        }
    }
}
