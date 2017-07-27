/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MaskSurfPro"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MaskSurfPro.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<CitiesViewModel>();
            SimpleIoc.Default.Register<CountriesViewModel>();
            SimpleIoc.Default.Register<ProxyTipsAPNViewModel>();
            SimpleIoc.Default.Register<ProxyTipsWifiViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<StatusViewModel>();
            SimpleIoc.Default.Register<TorLogViewModel>();
        }

        public AboutViewModel AboutVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }
        public CitiesViewModel CitiesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CitiesViewModel>();
            }
        }
        public CountriesViewModel CountriesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CountriesViewModel>();
            }
        }
        public ProxyTipsAPNViewModel ProxyTipsAPNVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProxyTipsAPNViewModel>();
            }
        }
        public ProxyTipsWifiViewModel ProxyTipsWifiVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProxyTipsWifiViewModel>();
            }
        }
        public SettingsViewModel SettingsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }
        public StatusViewModel StatusVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StatusViewModel>();
            }
        }
        public TorLogViewModel TorLogVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TorLogViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}