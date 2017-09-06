using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using GalaSoft.MvvmLight;

using MaskSurfPro.Models;
using MaskSurfPro.Pages;
using MaskSurfPro.Resources;

namespace MaskSurfPro.ViewModels
{
    public class StatusViewModel : ViewModelBase
    {
        public StatusPage CurrentPage { get; set; }
        public bool IsLoaded { get; set; }

        public ConnectionDetails ActiveConnection;
        public  List<IP> FalseIPs;
        private ObservableRangeCollection<string> falseIPsList; //to display
        public ObservableRangeCollection<string> NetworkMessages;
        public TabbedPage TipsTabs;

        private ObservableRangeCollection<string> selectedRegionsList;

        public ObservableRangeCollection<string> SelectedRegionsList
        {
            get
            {
                return selectedRegionsList;
            }
            set
            {
                if (selectedRegionsList != value)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        selectedRegionsList = value;
                    });
                }
            } 
        }

        public ObservableRangeCollection<string> FalseIPsList
        {
            get
            {
                return falseIPsList;
            }
            set
            {
                if (falseIPsList != value)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        falseIPsList = value;
                    });
                }
            }
        }

        public Color ConnStatusColor { get; set; }
        public string ConnectionStatusDescriptionText { get; set; }

        //translation properties
        public string TrueIPDescription { get; set; }
        public string ActiveConnectionTitle { get; set; }
        public string FalseIPDesc { get; set; }
        public string ActiveConnectionName { get; set; }
        public string ActiveConnectionProxy { get; set; }
        public string ActiveConnectionStatus { get; set; }
        public string RefreshButtonText { get; set; }
        public string GoToCountriesText { get; set; }
        public string GoToTorLogText { get; set; }
        public string GoToSettingsText { get; set; }
        public string GoToAboutText { get; set; }
        public string NetworkStatusTitleText { get; set; }
        public string RegionsLabelText { get; set; }
        public string ResetAllBtnText { get; set; }
        public string ResetAllLabelText { get; set; }
        public string StartWaitMessageText { get; set; }
        public string TestIPText { get; set; }
        public string WarningLabel { get; set; }
        public string OKLabel { get; set; }
        public string Anonymous { get; set; }
        public string NotAnonymous { get; set; }
        public string NotLicensed { get; set; }
        public string GooglePlayNotAvailable { get; set; }

        public StatusViewModel()
        {
            falseIPsList = new ObservableRangeCollection<string>();
            NetworkMessages = new ObservableRangeCollection<string>();
            selectedRegionsList = new ObservableRangeCollection<string>();
            IsLoaded = false;
            Translate();
            ConnStatusColor = Color.Default;


            MessagingCenter.Subscribe<StatusViewModel>(this, "IPsScaned", (sender) =>
            {
                FalseIPs = FalseIPScanner.CurrentIPs;
                ObservableRangeCollection<string> Temp = new ObservableRangeCollection<string>();
                foreach (IP curip in FalseIPs)
                {
                    if (Temp.IndexOf(curip.IPAddress)==-1)
                    {
                        Temp.Add(curip.IPAddress);
                        if (!String.IsNullOrWhiteSpace(curip.Country))
                        {
                            Temp.Add(curip.Country);
                        }
                        if (!String.IsNullOrWhiteSpace(curip.City))
                        {
                            Temp.Add(curip.City);
                        }
                    }
                }
                FalseIPsList = Temp;
                MessagingCenter.Send<StatusPage>((StatusPage)CurrentPage, "FalseIPChanged");
            });
            MessagingCenter.Subscribe<StatusViewModel, string>(this, "NewLoadMessage", (sender, arg) =>
            {
                if (arg.CompareTo(String.Empty) != 0 && NetworkMessages.IndexOf(arg)==-1)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        NetworkMessages.Add(arg);
                        if (NetworkMessages.Count > 3)
                        {
                            NetworkMessages.RemoveAt(0);
                        }
                    });
                }
            });
        }
        void Translate()
        {
            TrueIPDescription = AppStrings.TrueIPLabel;
            FalseIPDesc = AppStrings.FalseIPLabel;
            ActiveConnectionTitle = AppStrings.ActiveConnection;
            ActiveConnectionName = AppStrings.Name;
            ActiveConnectionProxy = AppStrings.Proxy;
            ActiveConnectionStatus = AppStrings.Status;
            RefreshButtonText = AppStrings.Refresh;
            GoToCountriesText = AppStrings.Countries;
            GoToTorLogText = AppStrings.TorLog;
            GoToSettingsText = AppStrings.Settings;
            GoToAboutText = AppStrings.About;
            RegionsLabelText = AppStrings.SelectedRegions;
            ResetAllBtnText = AppStrings.ResetAllLabel;
            ResetAllLabelText = AppStrings.ResetAllDescription;
            StartWaitMessageText = AppStrings.StartWaitMessage;
            TestIPText = AppStrings.TestIP;
            NetworkStatusTitleText = AppStrings.AnonymousNetworkStatus;
            WarningLabel = AppStrings.Warning;
            OKLabel = AppStrings.OK;
            Anonymous = AppStrings.Anonymous;
            NotAnonymous = AppStrings.NotAnonymous;
            NotLicensed = AppStrings.NotLicensed;
            GooglePlayNotAvailable = AppStrings.GooglePlayNotAvailable;
        }
        public void LoadSettings()
        {
            if (Settings.GetStringCollection("Selected regions list") != null)
            {
                SelectedRegionsList = new ObservableRangeCollection<string>(Settings.GetStringCollection("Selected regions list"));
            }
        }

        public void GetActiveConnection()
        {
            ActiveConnection = DependencyService.Get<IConnectionStatus>().GetCurrentConnection();

            if (ActiveConnection == null)
            {
                ConnectionStatusDescriptionText = AppStrings.NoActiveConnection;
            }
            else
            {
                if (ActiveConnection.IsSafe)
                {
                    ConnStatusColor = Color.Green;
                    ConnectionStatusDescriptionText = AppStrings.TrafficAnonymized;
                }
                else
                {
                    ConnStatusColor = Color.Red;
                    ConnectionStatusDescriptionText = AppStrings.TrafficNotAnonymized.Replace("8000", Settings.GetInt("Polipo port", 8000).ToString());
                }
            }
        }
        public string GetTrueIP()
        {
            string IP="";
            Task t = Task.Run(() =>
            {
                IP = Tor.GetCurrentIP();
            });
            t.Wait();

            return IP;
        }
        public void ResetSettings()
        {
            DependencyService.Get<IResetSettings>().ResetAll();
            selectedRegionsList.Clear();
            Device.BeginInvokeOnMainThread(() =>
            {
                MSProApp.Locator.CountriesVM.CancelSelectedCountriesList();
                MSProApp.Locator.CitiesVM.CancelSelectedCitiesList();
                SelectedRegionsList.Clear();
            });
        }
        public void TestIP()
        {
            Device.OpenUri(new Uri("https://showtheip.net/mobile.php"));
        }
        public void ShowTips()
        {
            TipsTabs = new TabbedPage();
            TipsTabs.Children.Add(new ProxyTipsWifiPage());
            TipsTabs.Children.Add(new ProxyTipsAPNPage());
        }
    }
    public interface IResetSettings
    {
        void ResetAll();
    }
}
