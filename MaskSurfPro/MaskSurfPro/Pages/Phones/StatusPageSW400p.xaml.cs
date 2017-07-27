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
    public partial class StatusPageSW400p : StatusPage
    {
        public StatusPageSW400p()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.StatusVM;
            MSProApp.Locator.StatusVM.CurrentPage = this;

            if (MSProApp.Locator.StatusVM.IsLoaded == false)
            {
                SetWaitingState();
            }
            TrueIPDesc.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            TrueIP.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            //NetworkLogLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            RegionsLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            ConnectionStatusDescription.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            TestIPBtn.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button));
            RefreshActiveConnection.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button));
            ShowTipsBtn.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button));
            ResetAllBtn.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button));
            ResetAllLabel.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));


            MessagingCenter.Subscribe<StatusPage>(this, "BootstrappFinished", (sender) =>
            {
                RemoveWaitingState();
                StatusViewModel svm = MSProApp.Locator.StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    TrueIP.Text = svm.GetTrueIP();
                });
            });
            MessagingCenter.Subscribe<StatusPage>(this, "FalseIPChanged", (sender) =>
            {

                StatusViewModel svm = MSProApp.Locator.StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    FalseIPList.ItemsSource = svm.FalseIPsList;
                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusViewModel svm = MSProApp.Locator.StatusVM;
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
            StatusViewModel svm = MSProApp.Locator.StatusVM;
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
        void SetWaitingState()
        {
            TestIPBtn.IsEnabled = false;
            ResetAllBtn.IsEnabled = false;

            LoadingMessage.IsVisible = true;
        }
        void RemoveWaitingState()
        {
            MSProApp.Locator.StatusVM.IsLoaded = true;

            Device.BeginInvokeOnMainThread(() =>
            {
                TestIPBtn.IsEnabled = true;
                ResetAllBtn.IsEnabled = true;

                LoadingMessage.IsVisible = false;
            });
        }
        async void ResetAll(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Mask Surf Pro",AppStrings.ResetAllConfirmation, AppStrings.Yes, AppStrings.No);
            if (answer == false)
            {
                return;
            }

            StatusViewModel svm = MSProApp.Locator.StatusVM;
            svm.ResetSettings();
            //SelectedRegions.ItemsSource = svm.SelectedRegionsList;

            await DisplayAlert("Mask Surf Pro", AppStrings.SettingsWereReset, AppStrings.OK);
        }
        void TestIP(object sender, EventArgs e)
        {
            StatusViewModel svm = MSProApp.Locator.StatusVM;
            if (svm.ActiveConnection.IsSafe == false)
            {
                string message = AppStrings.TrafficNotAnonymized + " " + AppStrings.SetProxyFirst;
                DisplayAlert(AppStrings.Warning, message, AppStrings.OK);
            }

            svm.TestIP();
        }
        void ShowTips(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(((MSProApp)Application.Current).ProxyTips);
        }
        /*
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            bool res = DeviceInfo.IsOrientationPortrait();

            if (width > height && MSProApp.Locator.StatusVM.IsLoaded == true)
            {
                Tabs MainTabs = ((MSProApp)Application.Current).MainTabs;

                var StatusPagePhoneL = new StatusPageSW400();
                StatusPagePhoneL.Title = AppStrings.Status;
                StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                MainTabs.Children.RemoveAt(0);
                MainTabs.Children.Insert(0, StatusPagePhoneL);
                MainTabs.SelectedItem = StatusPagePhoneL;
            }
        }
        */
        
    }
}
