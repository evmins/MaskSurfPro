using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

using MaskSurfPro.Pages;

namespace MaskSurfPro.ViewModels
{
    public class CountriesViewModel : FreshMvvm.FreshBasePageModel
    {
        //all countries lists
        List<string> ExitListNames = new List<string>();
        List<string> ExitListIPs = new List<string>();
        public CountriesList WorkCountriesList = new CountriesList();
        CountriesList ExitCountriesList = new CountriesList();
        public List<string> ExitCountries = new List<string>();
        string AllCountriesCodes;

        //selected countries list
        private ObservableCollection<string> selectedCountriesList;
        public ObservableCollection<string> SelectedCountriesList
        {
            get { return selectedCountriesList; }
        }
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
            selectedCountriesList = new ObservableCollection<string>();
            MessagingCenter.Subscribe<CountriesViewModel>(this, "CountriesCanceled", (sender) =>
            {
                selectedCountriesList.Clear();
                MessagingCenter.Send<CountriesPage>((CountriesPage)CurrentPage, "CountriesChanged");
                /*
                for (int i = selectedCountriesList.Count - 1; i > 0; i--)
                {
                    selectedCountriesList.Clear();
                }
                */
            });

        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Translate();
        }
        public void LoadSettings()
        {
            if (Settings.GetStringCollection("Selected countries list") != null)
            {
                selectedCountriesList = new ObservableCollection<string>(Settings.GetStringCollection("Selected countries list"));
            }
        }
        public async Task GetCountiesListThread()
        {
            await Task.Run(() =>
          {
             //code removed
          });


            var SortedCountries = from item in WorkCountriesList
                                  orderby item.Number descending
                                  select item;

            foreach (Country cur in SortedCountries)
            {
                ExitCountries.Add(cur.Name + "  " + cur.Number);
            }
        }
        public bool ApplySelectedCountriesList()
        {

            if (SelectedCountriesList.Count <= 0)
            {
                //MessageBox.Show(GetTranslation("Countries are not selected", "Messages"));
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
            //if (rbtnListModeInclude.IsChecked == true)
            //{
            for (int i = 0; i < SelectedCountriesList.Count; i++)
            {
                strNewExitCountriesEntry.Append("{");
                strNewExitCountriesEntry.Append(Tor.CountryToCode(SelectedCountriesList.ElementAt(i)));
                strNewExitCountriesEntry.Append("},");
            }
            strNewExitCountriesEntry.Remove(strNewExitCountriesEntry.Length - 1, 1);
            //}
            /*
            if (rbtnListModeExclude.IsChecked == true)
            {
                strNewExitCountriesEntry.Append(AllCountriesCodes.ToUpper());
                strNewExitCountriesEntry.Append(",");
                StringBuilder CodeToRemove = new StringBuilder();
                for (int i = 0; i < lbSelectedCountries.Items.Count; i++)
                {
                    CodeToRemove.Clear();
                    CodeToRemove.Append("{");
                    CodeToRemove.Append(Tor.CountryToCode(lbSelectedCountries.Items.GetItemAt(i).ToString()));
                    CodeToRemove.Append("},");
                    strNewExitCountriesEntry.Replace(CodeToRemove.ToString(), "");
                }
                strNewExitCountriesEntry.Remove(strNewExitCountriesEntry.Length - 1, 1);
            }
            */

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

            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            if (svm != null)
            {
                MessagingCenter.Send<StatusViewModel, ObservableCollection<string>>(svm, "RegionsChanged", SelectedCountriesList);
            }
            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            if (cvm != null)
            {
                MessagingCenter.Send<CitiesViewModel>(cvm, "CitiesCanceled");
            }
            MessagingCenter.Send<CountriesPage>((CountriesPage)CurrentPage, "CountriesChanged");

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

            SelectedCountriesList.Clear();
            MessagingCenter.Send<CountriesPage>((CountriesPage)CurrentPage, "CountriesChanged");
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            if (svm != null)
            {
                MessagingCenter.Send<StatusViewModel>(svm, "RegionsCanceled");
            }
            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            if (cvm != null)
            {
                //cvm.SelectedCitiesList.Clear();
                MessagingCenter.Send<CitiesViewModel>(cvm, "CitiesCanceled");
            }
        }
        public void AddSelectedCountry(string NewCountry)
        {
            if (selectedCountriesList.IndexOf(NewCountry) == -1)
            {
                selectedCountriesList.Add(NewCountry);
            }
        }
        public void RemoveSelectedCountry(string NewCountry)
        {
            selectedCountriesList.Remove(NewCountry);
        }
        public string GetCountryFromDB(string IP)
        {
            return DependencyService.Get<ISQLite>().Query(IP);
        }
        void Translate()
        {
            SelectedCountriesModeLabelText = Translation.GetString("Countries");
            SelectedCitiesModeLabelText = Translation.GetString("Cities");
            GetCountriesListBtnText = Translation.GetString("Get countries list");
            TotalCountriesLabelText = Translation.GetString("Total countries");
            TotalExitRelaysLabelText = Translation.GetString("Total exit relays");
            SelectedCountriesLabelText = Translation.GetString("Selected countries");
            ApplySelectedCountriesText = Translation.GetString("Apply list");
            CancelSelectedCountriesText = Translation.GetString("Cancel list");
            AddCountryBtnText = Translation.GetString("Add");
            RemoveCountryBtnText = Translation.GetString("Remove");
            WaitLabelText = Translation.GetString("Download wait message");
        }
    }
    public interface ISQLite
    {
        string Query(string IP);
    }
}
