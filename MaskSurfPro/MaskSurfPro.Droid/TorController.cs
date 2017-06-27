using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using System.Collections.Concurrent;
using System.Net;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Text.RegularExpressions;

using Android.Content;

using MaskSurfPro;
using Java.Net;
using Java.IO;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(TorControllerOnAndroid))]

namespace MaskSurfPro
{
    //dependency services
    public class TorControllerOnAndroid : ITorController
    {
        //class content removed
    }


    public class Router
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public UInt16 Port { get; set; }   //ORPort
        public string Platform { get; set; }
        public string Fingerprint { get; set; }
        public TimeSpan Uptime { get; set; }
        public ulong BandwidthAverage { get; set; }
        public ulong BandwidthBurst { get; set; }
        public ulong BandwidthObserved { get; set; }
        public ulong BandwidthCurrent { get; set; }
        public ulong DisplayBandwidth { get; set; }
        public long DisplayBandwidthFree { get; set; }
        public string Accept { get; set; }
        public string Reject { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }

        //Flags
        public bool IsExit { get; set; }
        public bool IsFast { get; set; }
        public bool IsStable { get; set; }
        public bool IsRunning { get; set; }
        public bool IsValid { get; set; }

        public Router(string RouterIP)
        {
            IP = RouterIP;
        }

    }
}
