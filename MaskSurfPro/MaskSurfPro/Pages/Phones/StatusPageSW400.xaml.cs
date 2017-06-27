using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class StatusPageSW400 : StatusPage
    {
        public StatusPageSW400()
        {
            InitializeComponent();
            SetWaitingState();
            TrueIPDesc.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            TrueIP.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            MSPLogo.Source = ImageSource.FromResource("MaskSurfPro.images.masksurfpro.png");
            ProgramName.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            //NetworkLogLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            RegionsLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            ConnectionStatusDescription.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            SizeChanged += OnPageSizeChanged;

            MessagingCenter.Subscribe<StatusPage>(this, "BootstrappFinished", (sender) =>
            {
                RemoveWaitingState();
                StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    TrueIP.Text = svm.GetTrueIP();
                });
            });
            MessagingCenter.Subscribe<StatusPage>(this, "RegionsChanged", (sender) =>
            {
                StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    SelectedRegions.ItemsSource = svm.SelectedRegionsList;
                });
            });
            MessagingCenter.Subscribe<StatusPage>(this, "NewLoadMessage", (sender) =>
            {

                StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    //NetworkLog.ItemsSource = svm.NetworkMessages;
                });
            });
            MessagingCenter.Subscribe<StatusPage>(this, "FalseIPChanged", (sender) =>
            {

                StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    FalseIPList.ItemsSource = svm.FalseIPsList;
                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            svm.GetActiveConnection();
            if (svm.ActiveConnection != null)
            {
                ActiveConName.Detail = svm.ActiveConnection.Name;
                ActiveConProxy.Detail = svm.ActiveConnection.Proxy;
                ActiveConStatus.Detail = svm.ActiveConnection.DisplayStatus;
            }
            svm.LoadSettings();
            ActiveConStatus.DetailColor = svm.ConnStatusColor;
            ConnectionStatusDescription.Text = svm.ConnectionStatusDescriptionText;

            //tune up

        }

        void Refresh(object sender, EventArgs e)
        {
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            svm.GetActiveConnection();
            if (svm.ActiveConnection != null)
            {
                ActiveConName.Detail = svm.ActiveConnection.Name;
                ActiveConProxy.Detail = svm.ActiveConnection.Proxy;
                ActiveConStatus.Detail = svm.ActiveConnection.DisplayStatus;
            }
            ActiveConStatus.DetailColor = svm.ConnStatusColor;
            ConnectionStatusDescription.Text = svm.ConnectionStatusDescriptionText;
        }
        void GoToTorLogNav(object sender, EventArgs e)
        {
            TorLogPageSW400 tPage = (TorLogPageSW400)((MSProApp)Application.Current).TorLogVM.CurrentPage;
            Application.Current.MainPage.Navigation.PushAsync(tPage);
        }
        void GoToCountriesNav(object sender, EventArgs e)
        {
            CountriesPageSW400 cPage = (CountriesPageSW400)((MSProApp)Application.Current).CountriesVM.CurrentPage;
            Application.Current.MainPage.Navigation.PushAsync(cPage);
        }
        void GoToSettingsNav(object sender, EventArgs e)
        {
            SettingsPage sPage = (SettingsPage)((MSProApp)Application.Current).SettingsVM.CurrentPage;
            Application.Current.MainPage.Navigation.PushAsync(sPage);
        }
        void GoToAboutNav(object sender, EventArgs e)
        {
            AboutPageSW400 aPage = (AboutPageSW400)((MSProApp)Application.Current).AboutVM.CurrentPage;
            Application.Current.MainPage.Navigation.PushAsync(aPage);
        }
        void SetWaitingState()
        {
            GoToCountries.IsEnabled = false;
            GoToSettings.IsEnabled = false;
            TestIPBtn.IsEnabled = false;
            ResetAllBtn.IsEnabled = false;

            LoadingMessage.IsVisible = true;
        }
        void RemoveWaitingState()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GoToCountries.IsEnabled = true;
                GoToSettings.IsEnabled = true;
                TestIPBtn.IsEnabled = true;
                ResetAllBtn.IsEnabled = true;

                LoadingMessage.IsVisible = false;
            });
        }
        async void ResetAll(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Mask Surf Pro", Translation.GetString("Reset all confirmation"), Translation.GetString("Yes"), Translation.GetString("No"));
            if (answer == false)
            {
                return;
            }

            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            svm.ResetSettings();
            SelectedRegions.ItemsSource = svm.SelectedRegionsList;

            await DisplayAlert("Mask Surf Pro", Translation.GetString("Settings were reset"), Translation.GetString("OK"));
        }
        void TestIP(object sender, EventArgs e)
        {
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            if (svm.ActiveConnection.IsSafe == false)
            {
                string message = Translation.GetString("Traffic not anonymized") + " " + Translation.GetString("Set proxy first");
                DisplayAlert(Translation.GetString("Warning"), message, Translation.GetString("OK"));
            }

            svm.TestIP();
        }
        void ShowTips(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(((MSProApp)Application.Current).ProxyTips);
        }
        void OnPageSizeChanged(object sender, EventArgs args)
        {
            /*
            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (this.Width > this.Height)
                {
                    if (DisplayPosition == DisplayPos.Portrait)
                    {
                        Application.Current.MainPage.Navigation.PopAsync();
                        ((MSProApp)Application.Current).StatusPagePhoneL = new StatusPageSW400();
                        ((MSProApp)Application.Current).StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                        Application.Current.MainPage.Navigation.PushAsync(((MSProApp)Application.Current).StatusPagePhoneL);
                    }
                }
                else
                {
                    if (DisplayPosition == DisplayPos.Landscape)
                    {
                        Application.Current.MainPage.Navigation.PopAsync();
                        ((MSProApp)Application.Current).StatusPagePhoneP = new StatusPageSW400p();
                        ((MSProApp)Application.Current).StatusPagePhoneP.DisplayPosition = DisplayPos.Landscape;
                        Application.Current.MainPage.Navigation.PushAsync(((MSProApp)Application.Current).StatusPagePhoneP);
                    }
                }
            }
            */
        }
    }
}

