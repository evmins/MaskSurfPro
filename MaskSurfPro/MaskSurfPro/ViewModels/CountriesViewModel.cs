using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using GalaSoft.MvvmLight;

using MaskSurfPro.Pages;
using MaskSurfPro.Resources;
using MaskSurfPro.Models;

namespace MaskSurfPro.ViewModels
{
    public class CountriesViewModel : ViewModelBase
    {
        public CountriesPage CurrentPage { get; set; }

        //all countries lists
        List<string> ExitListNames = new List<string>();
        List<string> ExitListIPs = new List<string>();
        public CountriesList WorkCountriesList = new CountriesList();
        CountriesList ExitCountriesList = new CountriesList();
        public List<string> ExitCountries = new List<string>();
        string AllCountriesCodes;

        //selected countries list
        public ObservableCollection<string> SelectedCountriesList;
       /* public ObservableCollection<string> SelectedCountriesList
        {
            get { return selectedCountriesList; }
        }*/
        public int ExitIPsNum
        {
            get { return ExitListIPs.Count; }
        }
        public int TotalCountries
        {
            get; set;
        }

        //translation properties 
        public string SelectedCountriesModeLabelText { get; set; }
        public string SelectedCitiesModeLabelText { get; set; }
        public string GetCountriesListBtnText { get; set; }
        public string TotalCountriesLabelText { get; set; }
        public string TotalExitRelaysLabelText { get; set; }
        public string SelectedCountriesLabelText { get; set; }
        public string ApplySelectedCountriesText { get; set; }
        public string CancelSelectedCountriesText { get; set; }
        public string AddCountryBtnText { get; set; }
        public string RemoveCountryBtnText { get; set; }
        public string WaitLabelText { get; set; }

        public CountriesViewModel()
        {
            SelectedCountriesList = new ObservableCollection<string>();
            Translate();
        }
        public void LoadSettings()
        {
            if (Settings.GetStringCollection("Selected countries list") != null)
            {
                SelectedCountriesList = new ObservableCollection<string>(Settings.GetStringCollection("Selected countries list"));
            }
        }
        public async Task GetCountiesListThread()
        {
            //code removed
        }
        public bool ApplySelectedCountriesList()
        {

            if (SelectedCountriesList.Count <= 0)
            {
                return false;
            }


            //удаление серверов предыдущей страны
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
                strResult += "\r\n";
            }

            StringBuilder strNewExitCountriesEntry = new StringBuilder();
            for (int i = 0; i < SelectedCountriesList.Count; i++)
            {
                strNewExitCountriesEntry.Append("{");
                strNewExitCountriesEntry.Append(Tor.CountryToCode(SelectedCountriesList.ElementAt(i)));
                strNewExitCountriesEntry.Append("},");
            }
            strNewExitCountriesEntry.Remove(strNewExitCountriesEntry.Length - 1, 1);

            //write country codes
            if (!String.IsNullOrEmpty(strNewExitCountriesEntry.ToString()))
            {
                strConfig.Append("StrictNodes 1\r\n");
                strConfig.Append("ExitNodes ");
                strConfig.Append(strNewExitCountriesEntry);
                strConfig.Append("\r\n");
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

            StatusViewModel svm = MSProApp.Locator.StatusVM;
            CitiesViewModel civm = MSProApp.Locator.CitiesVM;
            Device.BeginInvokeOnMainThread(() =>
            {
                svm.SelectedRegionsList = new ObservableCollection<string>(SelectedCountriesList);
                civm.SelectedCitiesList.Clear();
            });

            Settings.SetStringCollection("Selected countries list", SelectedCountriesList);
            Settings.SetStringCollection("Selected regions list", SelectedCountriesList);

            return true;
        }
        public void CancelSelectedCountriesList()
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
            Settings.Remove("Selected cites list");
            Settings.Remove("Selected regions list");

            StatusViewModel svm = MSProApp.Locator.StatusVM;
            CitiesViewModel civm = MSProApp.Locator.CitiesVM;

            Device.BeginInvokeOnMainThread(() =>
            {
                SelectedCountriesList.Clear();
                svm.SelectedRegionsList.Clear();
                civm.SelectedCitiesList.Clear();
            });
        }
        public void AddSelectedCountry(string NewCountry)
        {
            if (SelectedCountriesList.IndexOf(NewCountry) == -1)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SelectedCountriesList.Add(NewCountry);
                });
            }
        }
        public void RemoveSelectedCountry(string NewCountry)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SelectedCountriesList.Remove(NewCountry);
            });
        }
        void Translate()
        {
            SelectedCountriesModeLabelText = AppStrings.Countries;
            SelectedCitiesModeLabelText = AppStrings.Cities;
            GetCountriesListBtnText = AppStrings.GetCountriesList;
            TotalCountriesLabelText = AppStrings.TotalCountries;
            TotalExitRelaysLabelText = AppStrings.TotalExitRelays;
            SelectedCountriesLabelText = AppStrings.SelectedCountries;
            ApplySelectedCountriesText = AppStrings.ApplyList;
            CancelSelectedCountriesText = AppStrings.CancelList;
            AddCountryBtnText = AppStrings.Add;
            RemoveCountryBtnText = AppStrings.Remove;
            WaitLabelText = AppStrings.DownloadWaitMessage;
        }
    }
}
