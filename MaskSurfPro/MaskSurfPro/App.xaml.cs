using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

using MaskSurfPro.Models;
using MaskSurfPro.ViewModels;
using MaskSurfPro.Pages;
using MaskSurfPro.Resources;

namespace MaskSurfPro
{
    public class MSProApp : Application
    {
        public string Version { get; set; }

        public TabbedPage ProxyTips;
        public Tabs MainTabs;

        public StatusPage StatusPagePhoneL;
        public StatusPage StatusPagePhoneP;

        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }

        public MSProApp()
        {
            Version = "1.1";

            MainTabs = new Tabs();

            ProxyTips = new TabbedPage();
            ProxyTipsWifiPage wfPage = new ProxyTipsWifiPage();
            wfPage.Title = AppStrings.Wifi;

            ProxyTipsAPNPage apnPage = new ProxyTipsAPNPage();
            apnPage.Title = AppStrings.APN;
            ProxyTips.Children.Add(wfPage);
            ProxyTips.Children.Add(apnPage);

            MainPage = new NavigationPage(MainTabs);

            MessagingCenter.Subscribe<MSProApp, string>(this, "TorOutput", (sender, arg) =>
            {
                if (arg.CompareTo(System.String.Empty) != 0)
                {
                    MSProApp.Locator.TorLogVM.TorLog.Add(arg);
                    MessagingCenter.Send<StatusViewModel, string>(MSProApp.Locator.StatusVM, "NewLoadMessage", arg);
                }
                if (arg.IndexOf("[err]") != -1)
                {
                    //error!
                }
                if (arg.IndexOf("100%") != -1)
                {
                    Task tc = Task.Run(() =>
                    {
                        Tor.Connect();
                    });
                    tc.Wait();
                    MessagingCenter.Send<StatusPage>(MSProApp.Locator.StatusVM.CurrentPage, "BootstrappFinished");
                }
            });
            MessagingCenter.Subscribe<MSProApp>(this, "Start", (sender) =>
            {
                OnStart();
            });
        }

        protected override void OnStart()
        {
            //xlarge screens are at least 960dp x 720dp
            //large screens are at least 640dp x 480dp
            //normal screens are at least 470dp x 320dp
            //small screens are at least 426dp x 320dp

            MobileCenter.Start("android=7d19d3ff-dec3-42f0-80d7-3d5b74bc3593;", typeof(Analytics), typeof(Crashes));

            float dens = DependencyService.Get<IDisplaySize>().GetDensity();
            int wd = DependencyService.Get<IDisplaySize>().GetWidthDiP();
            int hd = DependencyService.Get<IDisplaySize>().GetHeightDiP();

            int bigger, smaller;
            if (wd > hd) //landscape
            {
                bigger = wd;
                smaller = hd;
            }
            else //portrait
            {
                bigger = hd;
                smaller = wd;
            }

            if (smaller >= 700) //SW700 and above
            {
                StatusPage sp = new StatusPage();
                sp.Title = AppStrings.Status;
                MainTabs.Children.Add(sp);

                CountriesPage cap = new CountriesPage();
                cap.Title = AppStrings.Countries;
                MainTabs.Children.Add(cap);

                CitiesPage cip = new CitiesPage();
                cip.Title = AppStrings.Cities;
                MainTabs.Children.Add(cip);

                TorLogPage tlp = new TorLogPage();
                tlp.Title = AppStrings.TorLog;
                MainTabs.Children.Add(tlp);

                AboutPage ap = new AboutPage();
                ap.Title = AppStrings.About;
                MainTabs.Children.Add(ap);

                MainPage = new NavigationPage(MainTabs);
            }
            else
            {
                if (smaller >= 500 && smaller < 700) //SW500
                {
                    StatusPageSW500 sp = new StatusPageSW500();
                    sp.Title = AppStrings.Status;
                    MainTabs.Children.Add(sp);

                    CountriesPageSW500 cap = new CountriesPageSW500();
                    cap.Title = AppStrings.Countries;
                    MainTabs.Children.Add(cap);

                    TorLogPageSW500 tlp = new TorLogPageSW500();
                    tlp.Title = AppStrings.TorLog;
                    MainTabs.Children.Add(tlp);

                    AboutPageSW400 ap = new AboutPageSW400();
                    ap.Title = AppStrings.About;
                    MainTabs.Children.Add(ap);

                    MainPage = new NavigationPage(MainTabs);
                }
                else
                {
                    if (smaller >= 400) //SW400
                    {
                        if (wd > hd)
                        {
                            StatusPagePhoneL = new StatusPageSW400();
                            StatusPagePhoneL.Title = AppStrings.Status;
                            //Application.Current.MainPage.Navigation.PopAsync();
                            MainTabs.Children.Add(StatusPagePhoneL);
                            MainPage = new NavigationPage(MainTabs);
                            StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                        }
                        else
                        {
                            StatusPagePhoneP = new StatusPageSW400p();
                            StatusPagePhoneP.Title = AppStrings.Status;
                            MainTabs.Children.Add(StatusPagePhoneP);
                            MainPage = new NavigationPage(MainTabs);
                            StatusPagePhoneP.DisplayPosition = DisplayPos.Portrait;
                        }

                        TorLogPageSW400 tlp = new TorLogPageSW400();
                        tlp.Title = AppStrings.TorLog;
                        MainTabs.Children.Add(tlp);

                        CountriesPageSW400 cap = new CountriesPageSW400();
                        cap.Title = AppStrings.Countries;
                        MainTabs.Children.Add(cap);

                        AboutPageSW400 ap = new AboutPageSW400();
                        ap.Title = AppStrings.About;
                        MainTabs.Children.Add(ap);
                    }
                    else//SW300
                    {
                        if (wd > hd)
                        {
                            StatusPagePhoneL = new StatusPageSW300();
                            StatusPagePhoneL.Title = AppStrings.Status;
                            MainTabs.Children.Add(StatusPagePhoneL);
                            MainPage = new NavigationPage(MainTabs);
                            StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                        }
                        else
                        {
                            
                            StatusPagePhoneP = new StatusPageSW300p();
                            StatusPagePhoneP.Title = AppStrings.Status;
                            MainTabs.Children.Add(StatusPagePhoneP);

                            MainPage = new NavigationPage(MainTabs);
                            StatusPagePhoneP.DisplayPosition = DisplayPos.Portrait;
                        }

                        TorLogPageSW300 tlp = new TorLogPageSW300();
                        tlp.Title = AppStrings.TorLog;
                        MainTabs.Children.Add(tlp);

                        CountriesPageSW300 cap = new CountriesPageSW300();
                        cap.Title = AppStrings.Countries;
                        MainTabs.Children.Add(cap);

                        AboutPageSW300 ap = new AboutPageSW300();
                        ap.Title = AppStrings.About;
                        MainTabs.Children.Add(ap);
                    }
                }
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            StatusViewModel svm = MSProApp.Locator.StatusVM;
            svm.FalseIPs = FalseIPScanner.CurrentIPs;
            ObservableCollection<string> Temp = new ObservableCollection<string>();
            foreach (IP curip in svm.FalseIPs)
            {
                if (Temp.IndexOf(curip.IPAddress) == -1)
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
            svm.FalseIPsList.ReplaceRange(Temp);
            MessagingCenter.Send<StatusPage>(MSProApp.Locator.StatusVM.CurrentPage, "FalseIPChanged");
        }
    }
    public class ConnectionDetails
    {
        string name;
        string proxyHost;
        bool isSafe;
        string displayStatus;

        public ConnectionDetails()
        {
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Proxy
        {
            get { return proxyHost; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    proxyHost = value;
                }
            }
        }
        public bool IsSafe
        {
            get { return isSafe; }
            set { isSafe = value; }
        }
        public string DisplayStatus
        {
            get { return displayStatus; }
            set { displayStatus = value; }
        }
    }
    public interface IConnectionStatus
    {
        ConnectionDetails GetCurrentConnection(); //note that interface members are public by default
    }
    public interface IDisplaySize
    {
        int GetWidth();
        int GetHeight();
        int GetWidthDiP();
        int GetHeightDiP();
        float GetDensity();
    }
    public enum DisplayPos { Portrait, Landscape };
    public class DeviceInfo
    {
        protected static DeviceInfo _instance;
        double width;
        double height;

        static DeviceInfo()
        {
            _instance = new DeviceInfo();
        }
        protected DeviceInfo()
        {
        }

        public static bool IsOrientationPortrait()
        {
            return _instance.height > _instance.width;
        }

        public static void SetSize(double width, double height)
        {
            _instance.width = width;
            _instance.height = height;
        }
    }
}
