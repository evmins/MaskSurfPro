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
    public partial class CitiesPageSW400 : CitiesPage
    {
        public CitiesPageSW400()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.CitiesVM;
            MSProApp.Locator.CitiesVM.CurrentPage = this;

            SelectedCitiesLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WaitLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            AddCityBtn.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            RemoveCityBtn.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CitiesViewModel cvm = MSProApp.Locator.CitiesVM;
            cvm.LoadSettings();
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;

            TotalCitiesLabel.Text = AppStrings.TotalCities + " " + cvm.TotalCities.ToString();
            TotalUnknownCitiesLabel.Text = AppStrings.TotalUnknownLocations + " " + cvm.NotSpecifiedListNum.ToString();
            TotalExitRelaysLabel.Text = AppStrings.TotalExitRelays + " " + cvm.TotalExitRouters.ToString();

            SelectedCountriesModeLabel.Text = cvm.SelectedCountriesModeLabelText;
            SelectedCitiesModeLabel.Text = cvm.SelectedCitiesModeLabelText;
            GetCitiesListBtn.Text = cvm.GetCitiesListBtnText;
            WaitLabel.Text = cvm.WaitLabelText;
            SelectedCitiesLabel.Text = cvm.SelectedCitiesLabelText;
            ApplySelectedCities.Text = cvm.ApplySelectedCitiesText;
            CancelSelectedCities.Text = cvm.CancelSelectedCitiesText;
            AddCityBtn.Text = cvm.AddCityBtnText;
            RemoveCityBtn.Text = cvm.RemoveCityBtnText;
        }

        async void GetCitiesList(object sender, EventArgs e)
        {

            CitiesViewModel cvm = MSProApp.Locator.CitiesVM;
            WaitLabel.IsVisible = true;
            GetCitiesListBtn.IsEnabled = false;
            await cvm.GetCitiesListThread();
        
            CitiesToSelect.ItemsSource = cvm.ExitCities;
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;

            var EmptyNum = from EmptyCity in cvm.AllCitiesList
                           where EmptyCity.Name == String.Empty
                           select EmptyCity;
            TotalCitiesLabel.Text = AppStrings.TotalCities + " " + cvm.TotalCities.ToString();
            TotalUnknownCitiesLabel.Text = AppStrings.TotalUnknownLocations + " " + cvm.NotSpecifiedListNum.ToString();
            TotalExitRelaysLabel.Text = AppStrings.TotalExitRelays + " " + cvm.TotalExitRouters.ToString();
            WaitLabel.IsVisible = false;
            GetCitiesListBtn.IsEnabled = true;
        }
        void AddSelectedCity(object sender, EventArgs e)
        {
            CitiesViewModel cvm = MSProApp.Locator.CitiesVM;
            string Selected = CitiesToSelect.SelectedItem.ToString().Replace("  ", "|");
            string[] Temp = Selected.Split('|');
            string City = Temp[0];
            cvm.AddSelectedCity(City);

        }
        void RemoveSelectedCity(object sender, EventArgs e)
        {
            if (SelectedCities.SelectedItem == null)
            {
                return;
            }
            if (CitiesToSelect.ItemsSource == null || ((List<string>)CitiesToSelect.ItemsSource).Count == 0)
            {
                DisplayAlert(AppStrings.Warning, AppStrings.GetCitiesFirst, AppStrings.OK);
                return;
            }

            CitiesViewModel cvm = MSProApp.Locator.CitiesVM;
            string Selected = SelectedCities.SelectedItem.ToString().Replace("  ", "|");
            string[] Temp = Selected.Split('|');
            string City = Temp[0];
            cvm.RemoveSelectedCity(City);

        }
        void ApplyCitiesList(object sender, EventArgs e)
        {
            if (CitiesToSelect.ItemsSource == null || ((List<string>)CitiesToSelect.ItemsSource).Count == 0)
            {
                DisplayAlert(AppStrings.Warning, AppStrings.GetCitiesFirst, AppStrings.OK);
                return;
            }

            CitiesViewModel cvm = MSProApp.Locator.CitiesVM;
            if (cvm.ApplySelectedCitiesList())
            {
                DisplayAlert("Mask Surf Pro", AppStrings.CitiesSelected, AppStrings.OK);
            }
            else
            {
                DisplayAlert(AppStrings.Warning, AppStrings.CitiesNotSelected, AppStrings.OK);
            }
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;

        }
        void CancelCitiesList(object sender, EventArgs e)
        {
            CitiesViewModel cvm = MSProApp.Locator.CitiesVM;
            cvm.CancelSelectedCitiesList();

            //clear lists in views
            cvm.SelectedCitiesList.Clear();
            StatusViewModel svm = MSProApp.Locator.StatusVM;
            CountriesViewModel cavm = MSProApp.Locator.CountriesVM;
            if (svm != null)
            {
                svm.SelectedRegionsList = new System.Collections.ObjectModel.ObservableCollection<string>();
            }
            if (cavm != null)
            {
                cavm.SelectedCountriesList.Clear();
            }
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;
            DisplayAlert("Mask Surf Pro", AppStrings.CitiesCancelled, AppStrings.OK);

        }

        void SwitchToCountries(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                return;
            }
            CountriesCitiesSwitch.IsToggled = true;
            Tabs MainTabs = ((MSProApp)Application.Current).MainTabs;
            CountriesPageSW400 cp = new CountriesPageSW400();
            cp.Title = AppStrings.Countries;
            MainTabs.Children.RemoveAt(2);
            MainTabs.Children.Insert(2, cp);
            MainTabs.SelectedItem = cp;
        }
    }
}
