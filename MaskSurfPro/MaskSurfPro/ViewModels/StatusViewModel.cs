using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.Models;
using MaskSurfPro.Pages;

namespace MaskSurfPro.ViewModels
{
    public class StatusViewModel : FreshMvvm.FreshBasePageModel
    {
        public ConnectionDetails ActiveConnection;
        public  List<IP> FalseIPs;
        private ObservableCollection<string> falseIPsList; //to display
        public ObservableCollection<string> NetworkMessages;
        public FreshMvvm.FreshTabbedNavigationContainer TipsTabs;

        private ObservableCollection<string> selectedRegionsList;

        public ObservableCollection<string> SelectedRegionsList
        {
            get
            {
                return selectedRegionsList;
            }
            set
            {
                if (selectedRegionsList != value)
                {
                    selectedRegionsList = value;
                    MessagingCenter.Send<StatusPage>((StatusPage)CurrentPage, "RegionsChanged");
                }
            } 
        }

        public ObservableCollection<string> FalseIPsList
        {
            get
            {
                return falseIPsList;
            }
            set
            {
                //if (falseIPsList != value)
                //{
                    falseIPsList = value;
                    MessagingCenter.Send<StatusPage>((StatusPage)CurrentPage, "FalseIPChanged");
                //}
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

        public StatusViewModel()
        {
            falseIPsList = new ObservableCollection<string>();
            NetworkMessages = new ObservableCollection<string>();
            selectedRegionsList = new ObservableCollection<string>();

            MessagingCenter.Subscribe<StatusViewModel>(this, "IPsScaned", (sender) =>
            {
                FalseIPs = FalseIPScanner.CurrentIPs;
                ObservableCollection<string> Temp = new ObservableCollection<string>();
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
            MessagingCenter.Subscribe<StatusViewModel>(this, "RegionsCanceled", (sender) =>
            {
                SelectedRegionsList = new ObservableCollection<string>();
            });
            MessagingCenter.Subscribe<StatusViewModel,ObservableCollection<string>>(this, "RegionsChanged", (sender, arg) =>
            {
                SelectedRegionsList = arg;
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
                    MessagingCenter.Send<StatusPage>((StatusPage)CurrentPage, "NewLoadMessage");
                }
            });
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Translate();
            ConnStatusColor = Color.Default;
        }
        void Translate()
        {
            TrueIPDescription = Translation.GetString("Your true IP is");
            FalseIPDesc = Translation.GetString("Your false IP is");
            ActiveConnectionTitle = Translation.GetString("Active connection");
            ActiveConnectionName = Translation.GetString("Name");
            ActiveConnectionProxy = Translation.GetString("Proxy");
            ActiveConnectionStatus = Translation.GetString("Status");
            RefreshButtonText = Translation.GetString("Refresh");
            GoToCountriesText = Translation.GetString("Countries");
            GoToTorLogText = Translation.GetString("Tor log");
            GoToSettingsText = Translation.GetString("Settings");
            GoToAboutText = Translation.GetString("About");
            RegionsLabelText = Translation.GetString("Selected regions");
            ResetAllBtnText = Translation.GetString("Reset all");
            ResetAllLabelText = Translation.GetString("Reset all description");
            StartWaitMessageText = Translation.GetString("Start wait message");
            TestIPText = Translation.GetString("Test IP");
            NetworkStatusTitleText = Translation.GetString("Anonymous network status");
        }
        public void LoadSettings()
        {
            ObservableCollection<string> EmptyCollection = new ObservableCollection<string>();
            if (Settings.GetStringCollection("Selected regions list") != null)
            {
                SelectedRegionsList = new ObservableCollection<string>(Settings.GetStringCollection("Selected regions list"));
            }
        }

        public void GetActiveConnection()
        {
            ActiveConnection = DependencyService.Get<IConnectionStatus>().GetCurrentConnection();

            if (ActiveConnection == null)
            {
                ConnectionStatusDescriptionText = Translation.GetString("No active connection");
            }
            else
            {

                if (ActiveConnection.IsSafe)
                {
                    ConnStatusColor = Color.Green;
                    ConnectionStatusDescriptionText = Translation.GetString("Traffic anonymized");
                }
                else
                {
                    ConnStatusColor = Color.Red;
                    ConnectionStatusDescriptionText = Translation.GetString("Traffic not anonymized").Replace("8000", Settings.GetInt("Polipo port", 8000).ToString());
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
            MessagingCenter.Send<CountriesPage>((CountriesPage)((MSProApp)MSProApp.Current).CountriesVM.CurrentPage, "CountriesCanceled");
            MessagingCenter.Send<CitiesPage>((CitiesPage)((MSProApp)MSProApp.Current).CitiesVM.CurrentPage, "CitiesCanceled");
            selectedRegionsList.Clear();
            MessagingCenter.Send<StatusPage>((StatusPage)CurrentPage, "RegionsChanged");
        }
        public void TestIP()
        {
            Device.OpenUri(new Uri("https://showtheip.net/mobile.php"));
        }
        public void ShowTips()
        {
            TipsTabs = new FreshMvvm.FreshTabbedNavigationContainer("TipsNavPage");
            TipsTabs.AddTab<ProxyTipsWifiViewModel>("Wifi", null);
            TipsTabs.AddTab<ProxyTipsAPNViewModel>("Mobile", null);
        }
    }
    public interface IResetSettings
    {
        void ResetAll();
    }
}
