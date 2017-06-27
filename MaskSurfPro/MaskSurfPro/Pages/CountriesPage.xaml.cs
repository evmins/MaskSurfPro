using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class CountriesPage : FreshMvvm.FreshBaseContentPage
    {
        public CountriesPage()
        {
            InitializeComponent();
            SelectedCountriesLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WaitLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));

            MessagingCenter.Subscribe<CountriesPage>(this, "CountriesChanged", (sender) =>
            {
                CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    SelectedCountries.ItemsSource = cvm.SelectedCountriesList;
                });
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            cvm.LoadSettings();
            SelectedCountries.ItemsSource = cvm.SelectedCountriesList;
            TotalCountriesLabel.Text = Translation.GetString("Total countries") + " " + cvm.TotalCountries.ToString();
            TotalExitRelaysLabel.Text = Translation.GetString("Total exit relays") + " " + cvm.ExitIPsNum.ToString();

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
            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            WaitLabel.IsVisible = true;
            GetCountriesListBtn.IsEnabled = false;
            await cvm.GetCountiesListThread();

            CountriesToSelect.ItemsSource = cvm.ExitCountries;
            SelectedCountries.ItemsSource = cvm.SelectedCountriesList;

            TotalCountriesLabel.Text = Translation.GetString("Total countries") + " " + cvm.TotalCountries.ToString();
            TotalExitRelaysLabel.Text = Translation.GetString("Total exit relays") + " " + cvm.ExitIPsNum.ToString();
            WaitLabel.IsVisible = false;
            GetCountriesListBtn.IsEnabled = true;
        }
        void AddSelectedCountry(object sender, EventArgs e)
        {
            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
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

            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            string Selected = SelectedCountries.SelectedItem.ToString().Replace("  ","|");
            string[] Temp = Selected.Split('|');
            string Country = Temp[0];
            cvm.RemoveSelectedCountry(Country);
        }
        void ApplyCountriesList(object sender, EventArgs e)
        {
            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            if (cvm.ApplySelectedCountriesList())
            {
                DisplayAlert("Mask Surf Pro", Translation.GetString("Countries selected"), Translation.GetString("OK"));
            }
            else
            {
                DisplayAlert(Translation.GetString("Warning"), Translation.GetString("Countries not selected"), Translation.GetString("OK"));
            }
            SelectedCountries.ItemsSource = cvm.SelectedCountriesList;
        }
        void CancelCountriesList(object sender, EventArgs e)
        {
            CountriesViewModel cvm = ((MSProApp)Application.Current).CountriesVM;
            cvm.CancelSelectedCountriesList();

            //clear lists in views
            cvm.SelectedCountriesList.Clear();
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            CitiesViewModel civm = ((MSProApp)Application.Current).CitiesVM;
            if (svm != null)
            {
                svm.SelectedRegionsList = new System.Collections.ObjectModel.ObservableCollection<string>();
            }
            if (civm != null)
            {
                civm.SelectedCitiesList.Clear();
            }
            SelectedCountries.ItemsSource = cvm.SelectedCountriesList;

            DisplayAlert("Mask Surf Pro", Translation.GetString("Countries cancelled"), Translation.GetString("OK"));
        }
        void SwitchToCities(object sender, ToggledEventArgs e)
        {
            if (!e.Value)
            {
                return;
            }
            CountriesCitiesSwitch.IsToggled = false;
            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            CitiesPage citiesPage = (CitiesPage)cvm.CurrentPage;
            Application.Current.MainPage.Navigation.PushAsync(citiesPage);
        }
    }
}
