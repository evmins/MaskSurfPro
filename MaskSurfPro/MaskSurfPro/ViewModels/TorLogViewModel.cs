using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MaskSurfPro.ViewModels
{
    public class TorLogViewModel : FreshMvvm.FreshBasePageModel
    {
        //translation properties
        public string TorLogLabelText { get; set; }
        public string ClearLogBtnText { get; set; }

        public ObservableCollection<string> TorLog;
        /*
        ObservableCollection<string> Log
        {
            get { return ((MSProApp)MSProApp.Current).TorLog; }
        }
        */

        public TorLogViewModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Translate();
            TorLog = new ObservableCollection<string>();
        }
        void Translate()
        {
            TorLogLabelText = Translation.GetString("Tor log label");
            ClearLogBtnText = Translation.GetString("Clear");
        }
    }
}
