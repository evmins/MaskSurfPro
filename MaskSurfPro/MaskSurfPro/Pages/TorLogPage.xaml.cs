using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class TorLogPage : FreshMvvm.FreshBaseContentPage
    {
        /*
        public ObservableCollection<string> Log
        {
            get { return ((MSProApp)Application.Current).TorLog; }
        }
        */
        public TorLogPage()
        {
            InitializeComponent();

            BindingContext = MSProApp.Locator.TorLogVM;
            MSProApp.Locator.TorLogVM.CurrentPage = this;

            TorLogLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TorLogViewModel tvm = MSProApp.Locator.TorLogVM;
            if (tvm != null && TorLogListView.ItemsSource != tvm.TorLog)
            {
                TorLogListView.ItemsSource = tvm.TorLog;
            }

            TorLogLabel.Text = tvm.TorLogLabelText;
            ClearLogBtn.Text = tvm.ClearLogBtnText;
        }
        void ClearLog(object sender, EventArgs e)
        {
            MSProApp.Locator.TorLogVM.ClearLog();
        }
    }
}
