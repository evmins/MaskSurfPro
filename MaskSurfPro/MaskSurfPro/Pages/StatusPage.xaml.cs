using System;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;
using MaskSurfPro.Resources;

namespace MaskSurfPro.Pages
{
    public partial class StatusPage : FreshMvvm.FreshBaseContentPage
    {
        public DisplayPos DisplayPosition;
        public StatusPage()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.StatusVM;
            MSProApp.Locator.StatusVM.CurrentPage = this;

            SetWaitingState();
            TrueIPDesc.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            TrueIP.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            MSPLogo.Source = ImageSource.FromResource("MaskSurfPro.images.masksurfpro.png");
            ProgramName.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));

            MessagingCenter.Subscribe<StatusPage>(this, "BootstrappFinished", (sender) =>
            {
                RemoveWaitingState();
                StatusViewModel svm = MSProApp.Locator.StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    TrueIP.Text = svm.GetTrueIP();
                });
            });
            MessagingCenter.Subscribe<StatusPage>(this, "NewLoadMessage", (sender) =>
            {
                StatusViewModel svm = MSProApp.Locator.StatusVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    NetworkLog.ItemsSource = svm.NetworkMessages;
                    for (int i = 0; i < svm.NetworkMessages.Count; i++)
                    {
                        if (svm.NetworkMessages[i].IndexOf("[err]") != -1)
                        {
                            NetworkLog.SelectedItem = svm.NetworkMessages[i];
                        }
                    }
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
            LoadingMessage.IsVisible = true;
            TestIPBtn.IsEnabled = false;
            ResetAllBtn.IsEnabled = false;
        }
        void RemoveWaitingState()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                LoadingMessage.IsVisible = false;
                TestIPBtn.IsEnabled = true;
                ResetAllBtn.IsEnabled = true;
            });
        }
        async void ResetAll(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Mask Surf Pro", AppStrings.ResetAllConfirmation, AppStrings.Yes, AppStrings.No);
            if (answer == false)
            {
                return;
            }

            StatusViewModel svm = MSProApp.Locator.StatusVM;
            svm.ResetSettings();
            SelectedRegions.ItemsSource = svm.SelectedRegionsList;

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
    }
}
