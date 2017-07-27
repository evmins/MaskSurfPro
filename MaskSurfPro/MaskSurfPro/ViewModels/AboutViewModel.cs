using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MaskSurfPro.Resources;

using Xamarin.Forms;

namespace MaskSurfPro.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        //translation properties
        public string VersionLabelText { get; set; }
        public string SystemDescLabelText { get; set; }
        public string SNLabelText { get; set; }
        public string DisclaimerLabelText { get; set; }
        public string DisclaimerText { get; set; }
        public string CopyrightLabelText { get; set; }

        public AboutViewModel()
        {
            Translate();
        }

        public void Translate()
        {
            VersionLabelText = "Mask Surf Pro " + AppStrings.version + " " + ((MSProApp)MSProApp.Current).Version;
            SystemDescLabelText = AppStrings.AndroidNotes;
            SNLabelText = AppStrings.MaskSurfOnSocialNetworks;
            DisclaimerLabelText = AppStrings.TheTorProjectDisclaimer;
            DisclaimerText = AppStrings.Disclaimer;
            CopyrightLabelText = "© " + DateTime.Now.Year + " Thanksoft";
        }
    }
}
