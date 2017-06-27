using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Pages
{
    public partial class TorLogPageSW400 : FreshMvvm.FreshBaseContentPage
    {
        public TorLogPageSW400()
        {
            InitializeComponent();

            TorLogLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TorLogViewModel tvm = ((MSProApp)Application.Current).TorLogVM;
            if (tvm != null && TorLogListView.ItemsSource != tvm.TorLog)
            {
                TorLogListView.ItemsSource = tvm.TorLog;
            }
            TorLogLabel.Text = tvm.TorLogLabelText;
            ClearLogBtn.Text = tvm.ClearLogBtnText;
        }
        void ClearLog(object sender, EventArgs e)
        {
            TorLogViewModel tvm = ((MSProApp)Application.Current).TorLogVM;
            tvm.TorLog.Clear();
        }
    }
}
