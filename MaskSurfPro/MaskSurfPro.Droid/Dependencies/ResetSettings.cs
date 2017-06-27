using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;

using MaskSurfPro.ViewModels;


[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.ResetSettingsOnAndroid))]

namespace MaskSurfPro.Droid
{
    public class ResetSettingsOnAndroid : IResetSettings
    {
        AssetManager Assets;
        public ResetSettingsOnAndroid()
        {
            Assets = MainActivity.assets;
        }
        public void ResetAll()
        {
            //files and folders

            string folder = Android.App.Application.Context.GetDir("Tor", FileCreationMode.WorldWriteable).AbsolutePath;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            /*
            string path = @"/data/data/com.thanksoft.masksurfpro/Tor/tor";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            FileStream TorDest = System.IO.File.Create(path);
            using (Stream fs = Assets.Open("tor"))
            {
                fs.CopyTo(TorDest);
            }

            TorDest.Close();
            
            Java.IO.File fileTor = new Java.IO.File(path);
            fileTor.SetExecutable(true);
            */

            //torrc
            string path = folder + "/torrc";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            FileStream TorrcDest = System.IO.File.Create(path);
            using (Stream fs = Assets.Open("torrc"))
            {
                fs.CopyTo(TorrcDest);
            }

            TorrcDest.Close();

            //geoip
            path = folder + "/geoip";
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            FileStream GeoIPDest = System.IO.File.Create(path);
            using (Stream fs = Assets.Open("geoip"))
            {
                fs.CopyTo(GeoIPDest);
            }

            GeoIPDest.Close();

            //data folder
            /*
            path = @"/data/data/com.thanksoft.masksurfpro/Tor/data";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }*/

            //Polipo
            /*
            path = @"/data/data/com.thanksoft.masksurfpro/Tor/polipo";
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            FileStream PolipoDest = System.IO.File.Create(path);
            if (!System.IO.File.Exists(path))
            {
                using (Stream fs = Assets.Open("polipo"))
                {
                    fs.CopyTo(PolipoDest);
                }
            }
            PolipoDest.Close();

            Java.IO.File filePolipo = new Java.IO.File(path);
            filePolipo.SetExecutable(true);
            */
            path = folder + "/polipo.conf";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            FileStream PolipoConfDest = System.IO.File.Create(path);
            using (Stream fs = Assets.Open("polipo.conf"))
            {
                fs.CopyTo(PolipoConfDest);
            }
            PolipoConfDest.Close();

            //DBs
            /*
            folder = @"/data/data/com.thanksoft.masksurfpro/databases/";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            */
            path = @"/data/data/com.thanksoft.masksurfpro/databases/languages.db";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            FileStream LanguagesDBDest = System.IO.File.Create(path);
            using (Stream fs = Assets.Open("languages.db"))
            {
                fs.CopyTo(LanguagesDBDest);
            }
            LanguagesDBDest.Close();

            //settings
            Settings.Remove("Tor port");
            Settings.Remove("Tor control port");
            Settings.Remove("Tor DNS port");
            Settings.Remove("Polipo port");
            //Settings.Remove("Start on boot");
            Settings.Remove("Selected cities list");
            Settings.Remove("Selected countries list");
            Settings.Remove("Selected regions list");
        }
    }
}