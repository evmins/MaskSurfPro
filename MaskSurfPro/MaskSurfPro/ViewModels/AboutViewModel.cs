using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MaskSurfPro.ViewModels
{
    public class AboutViewModel : FreshMvvm.FreshBasePageModel
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
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Translate();
        }
        public void Translate()
        {
            VersionLabelText = "Mask Surf Pro " + Translation.GetString("version") + " " + ((MSProApp)MSProApp.Current).Version;
            Device.OnPlatform(Android: () => SystemDescLabelText = Translation.GetString("AndroidNotes"));
            SNLabelText = Translation.GetString("Mask Surf on social networks");
            DisclaimerLabelText = Translation.GetString("The Tor Project disclaimer");
            DisclaimerText = Translation.GetString("Disclaimer");
            CopyrightLabelText = "© " + DateTime.Now.Year + " Thanksoft";
        }
    }
}
