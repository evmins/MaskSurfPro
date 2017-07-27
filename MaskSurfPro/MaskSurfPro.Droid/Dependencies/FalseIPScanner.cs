using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

using MaskSurfPro.Models;

[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.FalseIPScannerOnAndroidService))]

namespace MaskSurfPro.Droid
{
    [Service]
    public class FalseIPScannerOnAndroidService : Service, IFalseIPScanner
    {
        private bool AlreadyRunning;
        public FalseIPScannerOnAndroidService()
        {
            AlreadyRunning = false;
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (AlreadyRunning)
            {
                return StartCommandResult.NotSticky;
            }

            AlreadyRunning = true;
            Task t = Task.Run(() =>
            {
                Start();
            });
            return StartCommandResult.Sticky;
        }
        public void Start()
        {
            do
            {
                Task t = Task.Run(() =>
                {
                    ScanFalseIPs();
                    CheckOutdated();
                    MessagingCenter.Send(MSProApp.Locator.StatusVM, "IPsScaned");
                });
                t = Task.Delay(60000);
                t.Wait();
            }
            while (true);
        }
        private void ScanFalseIPs()
        {
            UrlReader ur = new UrlReader();
            string result = ur.GetUrl();

            if (!System.String.IsNullOrWhiteSpace(result))
            {
                string[] ResultArray = result.Split(',');
                IP NewIP = new IP(ResultArray[0], System.DateTime.Now, ResultArray[1], ResultArray[2], ResultArray[3]);
                bool bNotExist = true;
                foreach (IP CurIP in FalseIPScanner.CurrentIPs)
                {
                    if (CurIP.IPAddress == NewIP.IPAddress)
                    {
                        bNotExist = false;
                    }
                }

                if (bNotExist)
                {
                    FalseIPScanner.CurrentIPs.Add(NewIP);
                }
                for (int i = 0; i < FalseIPScanner.CurrentIPs.Count; i++)
                {
                    if (result.ToString().IndexOf(FalseIPScanner.CurrentIPs[i].IPAddress) != -1)
                    {
                        FalseIPScanner.CurrentIPs[i].LastChecked = System.DateTime.Now;
                    }
                }
            }
        }
        private void CheckOutdated()
        {
            for (int i = 0; i < FalseIPScanner.CurrentIPs.Count; i++)
            {
                System.TimeSpan result = System.DateTime.Now - FalseIPScanner.CurrentIPs[i].LastChecked;
                if (result.TotalMinutes > 5)
                {
                    FalseIPScanner.CurrentIPs.RemoveAt(i);
                }
            }
        }
        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            return null;
        }

    }
}