using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class CitiesPage : FreshMvvm.FreshBaseContentPage
    {
        public CitiesPage()
        {
            InitializeComponent();
            SelectedCitiesLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            WaitLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));

            MessagingCenter.Subscribe<CitiesPage>(this, "CitiesChanged", (sender) =>
            {
                CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
                Device.BeginInvokeOnMainThread(() =>
                {
                    SelectedCities.ItemsSource = cvm.SelectedCitiesList;
                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            cvm.LoadSettings();
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;

            TotalCitiesLabel.Text = Translation.GetString("Total cities") + " " + cvm.TotalCities.ToString();
            TotalUnknownLocationsLabel.Text = Translation.GetString("Total unknown locations") + " " + cvm.NotSpecifiedListNum.ToString();
            TotalExitRelaysLabel.Text = Translation.GetString("Total exit relays") + " " + cvm.TotalExitRouters.ToString();

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

            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            WaitLabel.IsVisible = true;
            GetCitiesListBtn.IsEnabled = false;
            await cvm.GetCitiesListThread();

            CitiesToSelect.ItemsSource = cvm.ExitCities;
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;

            TotalCitiesLabel.Text = Translation.GetString("Total cities") + " " + cvm.TotalCities.ToString();
            TotalUnknownLocationsLabel.Text = Translation.GetString("Total unknown locations") + " " + cvm.NotSpecifiedListNum.ToString();
            TotalExitRelaysLabel.Text = Translation.GetString("Total exit relays") + " " + cvm.TotalExitRouters.ToString();
            WaitLabel.IsVisible = false;
            GetCitiesListBtn.IsEnabled = true;
        }
        void AddSelectedCity(object sender, EventArgs e)
        {
            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
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
                DisplayAlert(Translation.GetString("Warning"), Translation.GetString("Get cities first"), Translation.GetString("OK"));
                return;
            }

            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            string Selected = SelectedCities.SelectedItem.ToString().Replace("  ", "|");
            string[] Temp = Selected.Split('|');
            string City = Temp[0];
            cvm.RemoveSelectedCity(City);

        }
        void ApplyCitiesList(object sender, EventArgs e)
        {
            if (CitiesToSelect.ItemsSource == null || ((List<string>)CitiesToSelect.ItemsSource).Count == 0)
            {
                DisplayAlert(Translation.GetString("Warning"), Translation.GetString("Get cities first"), Translation.GetString("OK"));
                return;
            }

            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            if (cvm.ApplySelectedCitiesList())
            {
                DisplayAlert("Mask Surf Pro", Translation.GetString("Cities selected"), Translation.GetString("OK"));
            }
            else
            {
                DisplayAlert(Translation.GetString("Warning"), Translation.GetString("Cities not selected"), Translation.GetString("OK"));
            }
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;

        }
        void CancelCitiesList(object sender, EventArgs e)
        {
            CitiesViewModel cvm = ((MSProApp)Application.Current).CitiesVM;
            cvm.CancelSelectedCitiesList();

            //clear lists in views
            cvm.SelectedCitiesList.Clear();
            StatusViewModel svm = ((MSProApp)Application.Current).StatusVM;
            CountriesViewModel cavm = ((MSProApp)Application.Current).CountriesVM;
            if (svm != null)
            {
                svm.SelectedRegionsList = new System.Collections.ObjectModel.ObservableCollection<string>();
            }
            if (cavm != null)
            {
                cavm.SelectedCountriesList.Clear();
            }
            SelectedCities.ItemsSource = cvm.SelectedCitiesList;
            DisplayAlert("Mask Surf Pro", Translation.GetString("Cities cancelled"), Translation.GetString("OK"));
            
        }
        
        void SwitchToCountries(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                return;
            }
            CountriesCitiesSwitch.IsToggled = true;
            Application.Current.MainPage.Navigation.PopAsync();
        }  
    }
}

