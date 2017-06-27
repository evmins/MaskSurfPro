using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using FreshMvvm;
using MaskSurfPro.Models;
using MaskSurfPro.ViewModels;
using MaskSurfPro.Pages;

namespace MaskSurfPro
{
    public class MSProApp : Application
    {
        public string Version { get; set; }

        public StatusPage StatusPage;
        public CountriesPage CountriesPage;
        public CitiesPage CitiesPage;
        public TorLogPage TorLogPage;
        public AboutPage AboutPage;
        public SettingsPage SettingsPage;

        public StatusViewModel StatusVM;
        public CountriesViewModel CountriesVM;
        public CitiesViewModel CitiesVM;
        public TorLogViewModel TorLogVM;
        public SettingsViewModel SettingsVM;
        public AboutViewModel AboutVM;
        public ProxyTipsWifiViewModel PTWFVM;
        public ProxyTipsAPNViewModel PTAPNVM;
        public TabbedPage ProxyTips;

        public StatusPage StatusPagePhoneL;
        public StatusPage StatusPagePhoneP;

        public MSProApp()
        {
            Version = "1.1";
            
            StatusPage = (StatusPage)FreshPageModelResolver.ResolvePageModel<StatusViewModel>();
            StatusPage.Title = Translation.GetString("Status");
            StatusVM = (StatusViewModel)StatusPage.BindingContext;

            CountriesPage = (CountriesPage)FreshPageModelResolver.ResolvePageModel<CountriesViewModel>();
            CountriesPage.Title = Translation.GetString("Countries");
            CountriesVM = (CountriesViewModel)CountriesPage.BindingContext;

            CitiesPage = (CitiesPage)FreshPageModelResolver.ResolvePageModel<CitiesViewModel>();
            CitiesPage.Title = Translation.GetString("Cities");
            CitiesVM = (CitiesViewModel)CitiesPage.BindingContext;

            TorLogPage = (TorLogPage)FreshPageModelResolver.ResolvePageModel<TorLogViewModel>();
            TorLogPage.Title = Translation.GetString("Tor log");
            TorLogVM = (TorLogViewModel)TorLogPage.BindingContext;
            
            SettingsPage = (SettingsPage)FreshPageModelResolver.ResolvePageModel<SettingsViewModel>();
            SettingsPage.Title = Translation.GetString("Settings");
            SettingsVM = (SettingsViewModel)SettingsPage.BindingContext;

            AboutPage = (AboutPage)FreshPageModelResolver.ResolvePageModel<AboutViewModel>();
            AboutPage.Title = Translation.GetString("About");
            AboutVM = (AboutViewModel)AboutPage.BindingContext;

            ProxyTips = new TabbedPage();
            ProxyTipsWifiPage wfPage = (ProxyTipsWifiPage)FreshPageModelResolver.ResolvePageModel<ProxyTipsWifiViewModel>();
            wfPage.Title = Translation.GetString("Wi-Fi");
            PTWFVM = (ProxyTipsWifiViewModel)wfPage.BindingContext;

            ProxyTipsAPNPage apnPage = (ProxyTipsAPNPage)FreshPageModelResolver.ResolvePageModel<ProxyTipsAPNViewModel>();
            apnPage.Title = Translation.GetString("APN");
            PTAPNVM = (ProxyTipsAPNViewModel)apnPage.BindingContext;
            ProxyTips.Children.Add(wfPage);
            ProxyTips.Children.Add(apnPage);

            MainPage = new NavigationPage(StatusPage);

            MessagingCenter.Subscribe<MSProApp, string>(this, "TorOutput", (sender, arg) =>
            {
                if (arg.CompareTo(System.String.Empty) != 0)
                {
                    TorLogVM.TorLog.Add(arg);
                    MessagingCenter.Send<StatusViewModel, string>(StatusVM, "NewLoadMessage", arg);
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
                    MessagingCenter.Send<StatusPage>((StatusPage)StatusVM.CurrentPage, "BootstrappFinished");
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

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                if (smaller >= 500 && smaller < 700) //SW500
                {
                    StatusPageSW500 sp = new StatusPageSW500();
                    sp.Title = Translation.GetString("Status");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, sp, StatusVM);
                    MainPage = new NavigationPage(sp);

                    TorLogPageSW500 tlp = new TorLogPageSW500();
                    tlp.Title = Translation.GetString("Tor log");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, tlp, TorLogVM);

                    CountriesPageSW500 cap = new CountriesPageSW500();
                    cap.Title = Translation.GetString("Countries");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cap, CountriesVM);

                    CitiesPageSW500 cip = new CitiesPageSW500();
                    cip.Title = Translation.GetString("Cities");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cip, CitiesVM);
                }
            }

            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (smaller >= 500) //SW500
                {
                    StatusPageSW500 sp = new StatusPageSW500();
                    sp.Title = Translation.GetString("Status");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, sp, StatusVM);
                    MainPage = new NavigationPage(sp);

                    TorLogPageSW500 tlp = new TorLogPageSW500();
                    tlp.Title = Translation.GetString("Tor log");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, tlp, TorLogVM);

                    CountriesPageSW500 cap = new CountriesPageSW500();
                    cap.Title = Translation.GetString("Countries");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cap, CountriesVM);

                    CitiesPageSW500 cip = new CitiesPageSW500();
                    cip.Title = Translation.GetString("Cities");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cip, CitiesVM);

                    AboutPage ap = new AboutPage();
                    ap.Title = Translation.GetString("About");
                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, ap, AboutVM);
                }
                else
                {
                    if (smaller >= 400) //SW400
                    {
                        if (wd > hd)
                        {
                            StatusPagePhoneL = new StatusPageSW400();
                            StatusPagePhoneL.Title = Translation.GetString("Status");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneL, StatusVM);
                            Application.Current.MainPage.Navigation.PopAsync();
                            MainPage = new NavigationPage(StatusPagePhoneL);
                            StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                        }
                        else
                        {
                            StatusPagePhoneP = new StatusPageSW400p();
                            StatusPagePhoneP.Title = Translation.GetString("Status");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneP, StatusVM);
                            Application.Current.MainPage.Navigation.PopAsync();
                            MainPage = new NavigationPage(StatusPagePhoneP);
                            StatusPagePhoneP.DisplayPosition = DisplayPos.Portrait;
                        }

                        TorLogPageSW400 tlp = new TorLogPageSW400();
                        tlp.Title = Translation.GetString("Tor log");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, tlp, TorLogVM);

                        CountriesPageSW400 cap = new CountriesPageSW400();
                        cap.Title = Translation.GetString("Countries");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cap, CountriesVM);

                        CitiesPageSW400 cip = new CitiesPageSW400();
                        cip.Title = Translation.GetString("Cities");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cip, CitiesVM);

                        AboutPageSW400 ap = new AboutPageSW400();
                        ap.Title = Translation.GetString("About");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, ap, AboutVM);
                    }
                    else
                    {
                        if (smaller >= 300) //SW300
                        {
                            if (wd > hd)
                            {
                                StatusPagePhoneL = new StatusPageSW300();
                                StatusPagePhoneL.Title = Translation.GetString("Status");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneL, StatusVM);
                                Application.Current.MainPage.Navigation.PopAsync();
                                MainPage = new NavigationPage(StatusPagePhoneL);
                                StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                            }
                            else
                            {
                                StatusPagePhoneP = new StatusPageSW300p();
                                StatusPagePhoneP.Title = Translation.GetString("Status");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneP, StatusVM);
                                Application.Current.MainPage.Navigation.PopAsync();
                                MainPage = new NavigationPage(StatusPagePhoneP);
                                StatusPagePhoneP.DisplayPosition = DisplayPos.Portrait;
                            }
                            /*
                            StatusPageSW300 sp = new StatusPageSW300();
                            sp.Title = Translation.GetString("Status");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, sp, StatusVM);
                            MainPage = new NavigationPage(sp);*/

                            TorLogPageSW300 tlp = new TorLogPageSW300();
                            tlp.Title = Translation.GetString("Tor log");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, tlp, TorLogVM);

                            CountriesPageSW300 cap = new CountriesPageSW300();
                            cap.Title = Translation.GetString("Countries");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cap, CountriesVM);

                            CitiesPageSW300 cip = new CitiesPageSW300();
                            cip.Title = Translation.GetString("Cities");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cip, CitiesVM);

                            AboutPageSW300 ap = new AboutPageSW300();
                            ap.Title = Translation.GetString("About");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, ap, AboutVM);

                        }
                        else //small (< 300) SW200
                        {
                            //
                        }
                    }
                }
            }
            if (Device.Idiom != TargetIdiom.Tablet && Device.Idiom != TargetIdiom.Phone) //something strange
            {
                if (smaller < 700)
                {
                    if (smaller >= 500) //SW500
                    {
                        StatusPageSW500 sp = new StatusPageSW500();
                        sp.Title = Translation.GetString("Status");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, sp, StatusVM);
                        MainPage = new NavigationPage(sp);

                        TorLogPageSW500 tlp = new TorLogPageSW500();
                        tlp.Title = Translation.GetString("Tor log");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, tlp, TorLogVM);

                        CountriesPageSW500 cap = new CountriesPageSW500();
                        cap.Title = Translation.GetString("Countries");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cap, CountriesVM);

                        CitiesPageSW500 cip = new CitiesPageSW500();
                        cip.Title = Translation.GetString("Cities");
                        FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cip, CitiesVM);
                    }
                    else
                    {
                        if (smaller >= 400) //SW400
                        {
                            if (wd > hd)
                            {
                                StatusPagePhoneL = new StatusPageSW400();
                                StatusPagePhoneL.Title = Translation.GetString("Status");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneL, StatusVM);
                                Application.Current.MainPage.Navigation.PopAsync();
                                MainPage = new NavigationPage(StatusPagePhoneL);
                                StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                            }
                            else
                            {
                                StatusPagePhoneP = new StatusPageSW400p();
                                StatusPagePhoneP.Title = Translation.GetString("Status");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneP, StatusVM);
                                Application.Current.MainPage.Navigation.PopAsync();
                                MainPage = new NavigationPage(StatusPagePhoneP);
                                StatusPagePhoneP.DisplayPosition = DisplayPos.Portrait;
                            }

                            TorLogPageSW400 tlp = new TorLogPageSW400();
                            tlp.Title = Translation.GetString("Tor log");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, tlp, TorLogVM);

                            CountriesPageSW400 cap = new CountriesPageSW400();
                            cap.Title = Translation.GetString("Countries");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cap, CountriesVM);

                            CitiesPageSW400 cip = new CitiesPageSW400();
                            cip.Title = Translation.GetString("Cities");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cip, CitiesVM);

                            AboutPageSW400 ap = new AboutPageSW400();
                            ap.Title = Translation.GetString("About");
                            FreshMvvm.FreshPageModelResolver.BindingPageModel(null, ap, AboutVM);
                        }
                        else
                        {
                            if (smaller >= 300) //SW300
                            {
                                if (wd > hd)
                                {
                                    StatusPagePhoneL = new StatusPageSW300();
                                    StatusPagePhoneL.Title = Translation.GetString("Status");
                                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneL, StatusVM);
                                    Application.Current.MainPage.Navigation.PopAsync();
                                    MainPage = new NavigationPage(StatusPagePhoneL);
                                    StatusPagePhoneL.DisplayPosition = DisplayPos.Landscape;
                                }
                                else
                                {
                                    StatusPagePhoneP = new StatusPageSW300p();
                                    StatusPagePhoneP.Title = Translation.GetString("Status");
                                    FreshMvvm.FreshPageModelResolver.BindingPageModel(null, StatusPagePhoneP, StatusVM);
                                    Application.Current.MainPage.Navigation.PopAsync();
                                    MainPage = new NavigationPage(StatusPagePhoneP);
                                    StatusPagePhoneP.DisplayPosition = DisplayPos.Portrait;
                                }
                                /*
                                StatusPageSW300 sp = new StatusPageSW300();
                                sp.Title = Translation.GetString("Status");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, sp, StatusVM);
                                MainPage = new NavigationPage(sp);*/

                                TorLogPageSW300 tlp = new TorLogPageSW300();
                                tlp.Title = Translation.GetString("Tor log");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, tlp, TorLogVM);

                                CountriesPageSW300 cap = new CountriesPageSW300();
                                cap.Title = Translation.GetString("Countries");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cap, CountriesVM);

                                CitiesPageSW300 cip = new CitiesPageSW300();
                                cip.Title = Translation.GetString("Cities");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, cip, CitiesVM);

                                AboutPageSW300 ap = new AboutPageSW300();
                                ap.Title = Translation.GetString("About");
                                FreshMvvm.FreshPageModelResolver.BindingPageModel(null, ap, AboutVM);

                            }
                            else //small (< 300) SW200
                            {
                                //
                            }
                        }
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
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
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
            svm.FalseIPsList = Temp;
            MessagingCenter.Send<StatusPage>((StatusPage)((MSProApp)Application.Current).StatusVM.CurrentPage, "FalseIPChanged");
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
}
