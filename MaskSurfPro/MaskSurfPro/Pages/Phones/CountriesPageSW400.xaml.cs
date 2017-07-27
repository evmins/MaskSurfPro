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
    public partial class CountriesPageSW400 : CountriesPage
    {
        public CountriesPageSW400()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.CountriesVM;
            MSProApp.Locator.CountriesVM.CurrentPage = this;

            SelectedCountriesLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WaitLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            AddCountryBtn.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            RemoveCountryBtn.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            SelectedCountries.ItemsSource = MSProApp.Locator.CountriesVM.SelectedCountriesList;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CountriesViewModel cvm = MSProApp.Locator.CountriesVM;
            cvm.LoadSettings();
            SelectedCountries.ItemsSource = cvm.SelectedCountriesList;
            TotalCountriesLabel.Text = AppStrings.TotalCountries + " " + cvm.TotalCountries.ToString();
            TotalExitRelaysLabel.Text = AppStrings.TotalExitRelays + " " + cvm.ExitIPsNum.ToString();

            SelectedCountriesModeLabel.Text = cvm.SelectedCountriesModeLabelText;
            SelectedCitiesModeLabel.Text = cvm.SelectedCitiesModeLabelText;
            GetCountriesListBtn.Text = cvm.GetCountriesListBtnText;
            WaitLabel.Text = cvm.WaitLabelText;
            SelectedCountriesLabel.Text = cvm.SelectedCountriesLabelText;
            ApplySelectedCountries.Text = cvm.ApplySelectedCountriesText;
            CancelSelectedCountries.Text = cvm.CancelSelectedCountriesText;
            AddCountryBtn.Text = cvm.AddCountryBtnText;
            RemoveCountryBtn.Text = cvm.RemoveCountryBtnText;
        }
        async void GetCountriesList(object sender, EventArgs e)
        {
            CountriesViewModel cvm = MSProApp.Locator.CountriesVM;
            WaitLabel.IsVisible = true;
            GetCountriesListBtn.IsEnabled = false;
            await cvm.GetCountiesListThread();

            CountriesToSelect.ItemsSource = cvm.ExitCountries;
            SelectedCountries.ItemsSource = cvm.SelectedCountriesList;

            TotalCountriesLabel.Text = AppStrings.TotalCountries + " " + cvm.TotalCountries.ToString();
            TotalExitRelaysLabel.Text = AppStrings.TotalExitRelays + " " + cvm.ExitIPsNum.ToString();
            WaitLabel.IsVisible = false;
            GetCountriesListBtn.IsEnabled = true;
        }
        void AddSelectedCountry(object sender, EventArgs e)
        {
            CountriesViewModel cvm = MSProApp.Locator.CountriesVM;
            string Selected = CountriesToSelect.SelectedItem.ToString().Replace("  ", "|");
            string[] Temp = Selected.Split('|');
            string Country = Temp[0];
            cvm.AddSelectedCountry(Country);
        }
        void RemoveSelectedCountry(object sender, EventArgs e)
        {
            if (SelectedCountries.SelectedItem == null)
            {
                return;
            }

            CountriesViewModel cvm = MSProApp.Locator.CountriesVM;
            string Selected = SelectedCountries.SelectedItem.ToString().Replace("  ", "|");
            string[] Temp = Selected.Split('|');
            string Country = Temp[0];
            cvm.RemoveSelectedCountry(Country);
        }
        void ApplyCountriesList(object sender, EventArgs e)
        {
            CountriesViewModel cvm = MSProApp.Locator.CountriesVM;
            if (cvm.ApplySelectedCountriesList())
            {
                DisplayAlert("Mask Surf Pro", AppStrings.CountriesSelected, AppStrings.OK);
            }
            else
            {
                DisplayAlert(AppStrings.Warning, AppStrings.CountriesNotSelected, AppStrings.OK);
            }
            SelectedCountries.ItemsSource = cvm.SelectedCountriesList;
        }
        void CancelCountriesList(object sender, EventArgs e)
        {
            CountriesViewModel cvm = MSProApp.Locator.CountriesVM;
            cvm.CancelSelectedCountriesList();

            DisplayAlert("Mask Surf Pro", AppStrings.CountriesCancelled, AppStrings.OK);
        }
        void SwitchToCities(object sender, ToggledEventArgs e)
        {
            if (!e.Value)
            {
                return;
            }
            CountriesCitiesSwitch.IsToggled = false;

            Tabs MainTabs = ((MSProApp)Application.Current).MainTabs;
            CitiesPageSW400 cip = new CitiesPageSW400();
            cip.Title = AppStrings.Cities;
            //MainTabs.Children[2] = cip;
            MainTabs.Children.RemoveAt(2);
            MainTabs.Children.Insert(2,cip);
            MainTabs.SelectedItem = cip;


        }
    }
}
