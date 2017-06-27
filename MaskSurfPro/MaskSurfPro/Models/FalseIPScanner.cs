using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using MaskSurfPro.ViewModels;

namespace MaskSurfPro.Models
{
    public static class FalseIPScanner
    {
        static public List<IP> CurrentIPs= new List<IP>();
    }
    public class IP
    {
        public IP(string IP, System.DateTime Time, string CountryIn, string CityIn, string OrgIn)
        {
            IPAddress = IP;
            LastChecked = Time;
            Country = CountryIn;
            City = CityIn;
            Organization = OrgIn;
        }

        public string IPAddress { get; set; }
        public System.DateTime LastChecked { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Organization { get; set; }
    }
    public interface IFalseIPScanner
    {
        void Start();
    }
    public interface IGetUrl
    {
        string GetUrl();
        string PostToUrl(string IPsToSend);
    }
}