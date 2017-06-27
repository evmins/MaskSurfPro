using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using MaskSurfPro.Models;
using MaskSurfPro.Pages;

namespace MaskSurfPro.ViewModels
{
    public class CitiesViewModel : FreshMvvm.FreshBasePageModel
    {
        //all countries lists
        List<string> ExitNamesList = new List<string>();
        public  List<string> ExitIPsList = new List<string>();
        public CountriesList WorkCitiesList = new CountriesList();  //exit routers with c details
        public ObservableCollection<City> AllCitiesList = new ObservableCollection<City>(); //exit routers with city details
        public List<string> ExitCities = new List<string>();
        public int NotSpecifiedListNum
        { get; set; }
        public int TotalExitRouters
        { get; set; }

        //selected countries list
        private ObservableCollection<string> selectedCitiesList;
        public ObservableCollection<string> SelectedCitiesList
        {
            get { return selectedCitiesList; }
        }

        //translation properties
        public string SelectedCountriesModeLabelText { get; set; }
        public string SelectedCitiesModeLabelText { get; set; }
        public string GetCitiesListBtnText { get; set; }
        public string TotalCitiesLabelText { get; set; }
        public string TotalUnknownLocationsLabelText { get; set; }
        public string TotalExitRelaysLabelText { get; set; }
        public string SelectedCitiesLabelText { get; set; }
        public string ApplySelectedCitiesText { get; set; }
        public string CancelSelectedCitiesText { get; set; }
        public string AddCityBtnText { get; set; }
        public string RemoveCityBtnText { get; set; }
        public string WaitLabelText { get; set; }
        public int TotalCities { get; set; }

        public CitiesViewModel()
        {
            selectedCitiesList = new ObservableCollection<string>();
            MessagingCenter.Subscribe<StatusViewModel>(this, "CitiesCanceled", (sender) =>
            {
                selectedCitiesList.Clear();
                MessagingCenter.Send<CitiesPage>((CitiesPage)CurrentPage, "CitiesChanged");
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Translate();
        }
        public void LoadSettings()
        {
            if (Settings.GetStringCollection("Selected cities list") != null)
            {
                selectedCitiesList = new ObservableCollection<string>(Settings.GetStringCollection("Selected cities list"));
            }
        }
        public async Task GetCitiesListThread()
        {
            await Task.Run(() =>
            {
                //code removed
            });

        }
        public bool ApplySelectedCitiesList()
        {
            if (SelectedCitiesList.Count <= 0)
            {
                //GetTranslation("Cities are not selected", "Messages"));
                return false;
            }

            //remove previous country's nodes
            StringBuilder strConfig = new StringBuilder();
            StringBuilder ExcludedCodesResult = new StringBuilder();
            int nStart;

            strConfig.Append(Tor.ReadTorrc());
            if (!String.IsNullOrWhiteSpace(strConfig.ToString()))
            {
                nStart = strConfig.ToString().IndexOf("StrictNodes");
                if (nStart != -1)
                {
                    strConfig.Remove(nStart, 13);
                }

                nStart = strConfig.ToString().IndexOf("ExitNodes");
                if (nStart != -1)
                {
                    int nEnd = strConfig.ToString().IndexOf(Environment.NewLine, nStart + 1);
                    if (nEnd < 0)
                    {
                        strConfig.Remove(nStart, strConfig.Length - nStart);
                    }
                    else
                    {
                        strConfig.Remove(nStart, nEnd - nStart);
                    }
                }
                //string strResult = strConfig.ToString();
                //strResult += "\r\n";
            }
            StringBuilder SelectedRoutersFingerprints = new StringBuilder();
            foreach (string SelectedCity in SelectedCitiesList)
            {
                var SelectedCitiesGroup = from SelectedGroup in AllCitiesList
                                          where SelectedGroup.Name == SelectedCity
                                          select SelectedGroup;

                foreach (City curcity in SelectedCitiesGroup)
                {
                    string reply = "";
                    Task ts = Task.Run(() =>
                    {
                        //desc/name/
                        reply = Tor.SendSignal("GETINFO desc/name/" + curcity.RouterName + Environment.NewLine);
                    });
                    ts.Wait();
                    if (String.IsNullOrEmpty(reply))
                    {
                        continue;
                    }
                    int end = reply.IndexOf("uptime");
                    int start = reply.IndexOf("fingerprint ");
                    string fingerprint = reply.Substring(start + 12, end - start - 12);
                    fingerprint = fingerprint.Replace(" ", String.Empty);
                    SelectedRoutersFingerprints.Append(fingerprint);
                    SelectedRoutersFingerprints.Append(",");


                }
            }
            if (SelectedRoutersFingerprints.Length > 0)
            {
                SelectedRoutersFingerprints.Remove(SelectedRoutersFingerprints.Length - 1, 1);
                SelectedRoutersFingerprints.Append(Environment.NewLine);
            }

            //write country codes

            if (!String.IsNullOrEmpty(SelectedRoutersFingerprints.ToString()))
            {
                nStart = strConfig.ToString().IndexOf("StrictNodes");
                if (nStart != -1)
                {
                    strConfig.Remove(nStart, 13);
                }
                nStart = strConfig.ToString().IndexOf("ExitNodes");
                if (nStart != -1)
                {
                    int nEnd = strConfig.ToString().IndexOf(Environment.NewLine, nStart + 1);
                    if (nEnd < 0)
                    {
                        strConfig.Remove(nStart, strConfig.Length - nStart);
                    }
                    else
                    {
                        strConfig.Remove(nStart, nEnd - nStart);
                    }
                }
                strConfig.Append("StrictNodes 1\r\n");
                strConfig.Append("ExitNodes ");
                strConfig.Append(SelectedRoutersFingerprints.ToString());
                Tor.WriteTorrc(strConfig.ToString());
            }
            else
            {
                return false;
            }

            //смена цепи и перезагрузка настроек tor
            bool bResult;

            Task t = Task.Run(() =>
            {
                bResult = Tor.SendSimpleSignal("SIGNAL RELOAD\r\n");
                bResult = Tor.SendSimpleSignal("SIGNAL NEWNYM\r\n");
            });

            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            /*
            if (svm != null)
            {
                svm.SelectedRegionsList = SelectedCitiesList;
            }
            */
            if (svm != null)
            {
                MessagingCenter.Send<StatusViewModel, ObservableCollection<string>>(svm, "RegionsChanged", SelectedCitiesList);
            }
            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            if (cvm != null)
            {
                MessagingCenter.Send<CountriesViewModel>(cvm, "CountriesCanceled");
            }
            MessagingCenter.Send<CitiesPage>((CitiesPage)CurrentPage, "CitiesChanged");

            Settings.SetStringCollection("Selected cities list", SelectedCitiesList);
            Settings.SetStringCollection("Selected regions list", SelectedCitiesList);

            return true;
        }
        public void CancelSelectedCitiesList()
        {
            StringBuilder strConfig = new StringBuilder();
            int nStart;

            strConfig.Append(Tor.ReadTorrc());
            if (!String.IsNullOrWhiteSpace(strConfig.ToString()))
            {
                nStart = strConfig.ToString().IndexOf("StrictNodes");
                if (nStart != -1)
                {
                    strConfig.Remove(nStart, 13);
                }

                nStart = strConfig.ToString().IndexOf("ExitNodes");
                if (nStart != -1)
                {
                    int nEnd = strConfig.ToString().IndexOf("\r\n", nStart + 1);
                    if (nEnd < 0)
                    {
                        strConfig.Remove(nStart, strConfig.Length - nStart);
                    }
                    else
                    {
                        strConfig.Remove(nStart, nEnd - nStart);
                    }
                }
                string strResult = strConfig.ToString();

                Tor.WriteTorrc(strResult);

                Task t = Task.Run(() =>
                {
                    bool bResult;
                    bResult = Tor.SendSimpleSignal("SIGNAL RELOAD\r\n");
                    bResult = Tor.SendSimpleSignal("SIGNAL NEWNYM\r\n");
                });
            }
            Settings.Remove("Selected countries list");
            Settings.Remove("Selected cities list");
            Settings.Remove("Selected regions list");

            SelectedCitiesList.Clear();
            MessagingCenter.Send<CitiesPage>((CitiesPage)CurrentPage, "CitiesChanged");
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            if (svm != null)
            {
                MessagingCenter.Send<StatusViewModel>(svm, "RegionsCanceled");
            }
            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            if (cvm != null)
            {
                //cvm.SelectedCitiesList.Clear();
                MessagingCenter.Send<CountriesViewModel>(cvm, "CountriesCanceled");
            }
            /*
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            if (svm != null)
            {
                svm.SelectedRegionsList.Clear();
            }
            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            if (cvm != null)
            {
                cvm.SelectedCountriesList.Clear();
            }
            */
        }
        public void AddSelectedCity(string NewCountry)
        {
            if (selectedCitiesList.IndexOf(NewCountry) == -1)
            {
                selectedCitiesList.Add(NewCountry);
            }
        }
        public void RemoveSelectedCity(string NewCountry)
        {
            selectedCitiesList.Remove(NewCountry);
        }
        public string GetCountryFromDB(string IP)
        {
            return DependencyService.Get<ISQLite>().Query(IP);
        }
        void Translate()
        {
            SelectedCountriesModeLabelText = Translation.GetString("Countries");
            SelectedCitiesModeLabelText = Translation.GetString("Cities");
            GetCitiesListBtnText = Translation.GetString("Get cities list");
            TotalCitiesLabelText = Translation.GetString("Total cities");
            TotalUnknownLocationsLabelText = Translation.GetString("Total unknown locations");
            TotalExitRelaysLabelText = Translation.GetString("Total exit relays");
            SelectedCitiesLabelText = Translation.GetString("Selected cities");
            ApplySelectedCitiesText = Translation.GetString("Apply list");
            CancelSelectedCitiesText = Translation.GetString("Cancel list");
            AddCityBtnText = Translation.GetString("Add");
            RemoveCityBtnText = Translation.GetString("Remove");
            WaitLabelText = Translation.GetString("Download wait message");
        }
    }
    public struct City
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public string Country { get; set; }
        public string RouterName { get; set; }
    }
}

