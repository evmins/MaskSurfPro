using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;
using MaskSurfPro.Resources;

namespace MaskSurfPro.Pages
{
    public partial class SettingsPage : FreshMvvm.FreshBaseContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.SettingsVM;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            TorPort.Text = Settings.GetInt("Tor port", 9050).ToString();
            TorComPort.Text= Settings.GetInt("Tor control port", 9051).ToString();
            PolipoPort.Text= Settings.GetInt("Polipo port", 8000).ToString();
            //IsStartOnBoot.On = Settings.GetBool("Start on boot", false);

            SettingsViewModel svm = MSProApp.Locator.SettingsVM;
            PortsSection.Title = svm.PortsTitleText;
            TorPort.Label = svm.TorPortLabelText;
            TorComPort.Label = svm.TorComPortText;
            PolipoPort.Label = svm.PolipoPortText;
            ApplySettingsBtn.Text = svm.ApplySettingsBtnText;
            CancelSettingsBtn.Text = svm.CancelSettingsBtnText;
        }
        void ApplySettings(object sender, EventArgs e)
        {
            SettingsViewModel svm = MSProApp.Locator.SettingsVM;

            if (String.IsNullOrWhiteSpace(TorPort.Text))
            {
                Settings.Remove("Tor port");
            }
            else
            {
                if (int.TryParse(TorPort.Text, out svm.TorPort))
                {
                    Settings.SetInt("Tor port", Convert.ToInt32(TorPort.Text));
                }
                else
                {
                    svm.TorPort = 0;
                }
            }
            if (String.IsNullOrWhiteSpace(TorComPort.Text))
            {
                Settings.Remove("Tor control port");
            }
            else
            {
                if (int.TryParse(TorComPort.Text, out svm.TorControlPort))
                {
                    Settings.SetInt("Tor control port", Convert.ToInt32(TorComPort.Text));
                }
                else
                {
                    svm.TorControlPort = 0;
                }
            }
            if (String.IsNullOrWhiteSpace(PolipoPort.Text))
            {
                Settings.Remove("Polipo port");
            }
            else
            {
                if (int.TryParse(PolipoPort.Text, out svm.PolipoPort))
                {
                    Settings.SetInt("Polipo port", Convert.ToInt32(PolipoPort.Text));
                }
                else
                {
                    svm.PolipoPort = 0;
                }
            }

            //Settings.SetBool("Start on boot", IsStartOnBoot.On);
            //svm.IsStartOnBoot = IsStartOnBoot.On;

            if (svm.ApplySettings())
            {
                DisplayAlert("Mask Surf Pro", AppStrings.SettingsApplied, AppStrings.OK);
            }
            else
            {
                DisplayAlert(AppStrings.Warning, AppStrings.SettingsNotApplied, AppStrings.OK);
            }
            Application.Current.MainPage.Navigation.PopAsync();
        }
        void CancelSettings(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
