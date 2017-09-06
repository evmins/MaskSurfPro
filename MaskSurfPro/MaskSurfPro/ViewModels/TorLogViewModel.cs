using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using GalaSoft.MvvmLight;
using MaskSurfPro.Pages;
using MaskSurfPro.Resources;

namespace MaskSurfPro.ViewModels
{
    public class TorLogViewModel : ViewModelBase
    {
        public TorLogPage CurrentPage { get; set; }

        //translation properties
        public string TorLogLabelText { get; set; }
        public string ClearLogBtnText { get; set; }

        private ObservableRangeCollection<string> torLog;
        
        public ObservableRangeCollection<string> TorLog
        {
            get { return torLog; }
        }
        

        public TorLogViewModel()
        {
            Translate();
            torLog = new ObservableRangeCollection<string>();
        }
        public void ClearLog()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                torLog.Clear();
            });
        }
        void Translate()
        {
            TorLogLabelText = AppStrings.TorLogLabel;
            ClearLogBtnText = AppStrings.Clear;
        }
    }
}
