using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Android.Net.Wifi;


[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.ConnectionStatus))]

namespace MaskSurfPro.Droid
{
    public class ConnectionStatus : IConnectionStatus
    {
        public ConnectionStatus()
        {
        }
        public ConnectionDetails GetCurrentConnection()
        {
            ConnectionDetails ActiveCon = new ConnectionDetails();
            ConnectivityManager connManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo current = connManager.ActiveNetworkInfo;
            IList<Java.Net.Proxy> list = Java.Net.ProxySelector.Default.Select(new Java.Net.URI("http://google.com"));

            if (current == null)
            {
                return null;
            }

            if (current.IsConnected)
            {
                WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
                WifiInfo wfInfo = wifiManager.ConnectionInfo;
                ActiveCon.Name = wfInfo.SSID.Replace("\"", "");

                foreach (Java.Net.Proxy p in list)
                {
                    Java.Net.SocketAddress adr = p.Address();
                    ActiveCon.Proxy = p.ToString();
                }

                //status
                if (ActiveCon.Proxy.IndexOf("localhost:" + Settings.GetInt("Polipo port", 8000).ToString()) >= 0 || ActiveCon.Proxy.IndexOf("127.0.0.1:" + Settings.GetInt("Polipo port", 8000).ToString()) >= 0)
                {
                    ActiveCon.IsSafe = true;
                    ActiveCon.DisplayStatus = MSProApp.Locator.StatusVM.Anonymous;
                }
                else
                {
                    ActiveCon.IsSafe = false;
                    ActiveCon.DisplayStatus = MSProApp.Locator.StatusVM.NotAnonymous;
                }

                return ActiveCon;
            }

            return null;
        }
    }
}