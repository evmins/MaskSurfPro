using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Globalization;

using Android.Content;
using Android.Content.Res;
using Android.Net;
using Android.Net.Wifi;

using Android.App;
using Android.OS;
using Java.Lang;
using Java.IO;
using Java.Net;
using Xamarin.Forms;
using MaskSurfPro.Models;
using MaskSurfPro.ViewModels;
using Mono.Data.Sqlite;


[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.DisplaySizeOnAndroid))]

namespace MaskSurfPro.Droid
{
    public static class AndroidUtils
    {
        static public Java.Lang.Process TorProc
        { get; set; }
        static public Java.Lang.Process PolipoProc
        { get; set; }

        static public MainActivity activity
        { get; set; }

        static public void StartTor()
        {
            string folder = Android.App.Application.Context.GetDir("Tor", FileCreationMode.WorldWriteable).AbsolutePath;

            //Tor
            string[] argsTor = new string[3];
            argsTor[0] = folder + "/tor";
            argsTor[1] = "-f";
            argsTor[2] = folder + "/torrc";

            if (System.IO.File.Exists(argsTor[0]))
            {
                int z = 0;
            }

            if (System.IO.File.Exists(argsTor[2]))
            {
                int z = 0;
            }

            ProcessBuilder pb = new ProcessBuilder(argsTor);
            try
            {
                TorProc = pb.Start();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            System.Action check = CheckLog;
            Runnable runnable = new Runnable(check);
            new Thread(runnable).Start();

            //Polipo
            string[] argsPolipo = new string[3];
            argsPolipo[0] = folder + "/polipo";
            argsPolipo[1] = "-c";
            argsPolipo[2] = folder + "/polipo.conf";

            ProcessBuilder pb2 = new ProcessBuilder(argsPolipo);
            Java.Lang.Process PolipoProc = pb2.Start();
        }
        static public void CheckLog()
        {
            do
            {
                if (TorProc.InputStream.CanRead)
                {
                    System.Text.StringBuilder builder = new System.Text.StringBuilder();

                    using (StreamReader reader = new StreamReader(TorProc.InputStream))
                    {
                        int result;
                        do
                        {
                            char[] buffer = new char[16 * 1024];
                            result = reader.Read(buffer, 0, buffer.Length);
                            if (result > 0)
                            {
                                AddToLog(new string(buffer));
                            }
                        }
                        while (result > 0);
                    }

                    string reply = builder.ToString();

                }
                Thread.Sleep(500);
            }
            while (true);
        }
        public static void AddToLog(string Text)
        {
            if (System.String.IsNullOrWhiteSpace(Text))
            {
                return;
            }
            string[] pieces = Text.Split('\n');
            foreach (string piece in pieces)
            {
                if (piece.CompareTo(System.String.Empty) != 0)
                {
                    MessagingCenter.Send<MSProApp, string>((MSProApp)MSProApp.Current, "TorOutput", piece);
                }
                if (piece.IndexOf("100%") != -1)
                {
                    MessagingCenter.Send<MainActivity>(AndroidUtils.activity, "FalseIPScannerStart");
                }
            }
        }
        static public void CopyBinaries()
        {
            string folder = Android.App.Application.Context.GetDir("Tor", FileCreationMode.WorldWriteable).AbsolutePath;

            string path = folder + "/tor";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            FileStream TorDest = System.IO.File.Create(path);
            using (Stream fs = MainActivity.assets.Open("tor"))
            {
                fs.CopyTo(TorDest);
            }

            TorDest.Close();

            Java.IO.File fileTor = new Java.IO.File(folder + "/tor");
            bool result = fileTor.SetExecutable(true);
            result = fileTor.SetReadable(true);
            result = fileTor.SetWritable(true);
            //Android.OS.file
            //Runtime.GetRuntime().Exec("chmod 755 "+path); //it works!

            //torrc
            path = folder + "/torrc";
            if (!System.IO.File.Exists(path))
            {
                FileStream TorrcDest = System.IO.File.Create(path);
                using (Stream fs = MainActivity.assets.Open("torrc"))
                {
                    fs.CopyTo(TorrcDest);
                }

                TorrcDest.Close();
            }

            Java.IO.File torrcfile = new Java.IO.File(folder + "/torrc");
            result = torrcfile.SetReadable(true);
            result = torrcfile.SetWritable(true);

            StringBuilder text = new StringBuilder();

            BufferedReader br = new BufferedReader(new FileReader(path));
            System.String line;

            while ((line = br.ReadLine()) != null)
            {
                text.Append(line);
                text.Append('\n');
            }
            br.Close();
            if (text.IndexOf("/data/data/com.thanksoft.masksurfpro/Tor/data") !=-1)
            {
                string Temp = text.ToString().Replace("/data/data/com.thanksoft.masksurfpro/Tor/data", folder + "/data");
                text.Delete(0, text.Length());
                text.Append(Temp);
            }
            if (text.IndexOf("/data/data/com.thanksoft.masksurfpro/Tor/geoip") != -1)
            {
                string Temp = text.ToString().Replace("/data/data/com.thanksoft.masksurfpro/Tor/geoip", folder + "/data");
                text.Delete(0, text.Length());
                text.Append(Temp);
            }
            if (text.IndexOf("/data/data/com.thanksoft.masksurfpro/Tor/geoip6") != -1)
            {
                string Temp = text.ToString().Replace("/data/data/com.thanksoft.masksurfpro/Tor/geoip6", folder + "/geoip6");
                text.Delete(0, text.Length());
                text.Append(Temp);
            }

            BufferedWriter bw = new BufferedWriter(new FileWriter(path));
            bw.Write(text.ToString());
            bw.Close();


            //geoip
            path = folder + "/geoip";
            if (!System.IO.File.Exists(path))
            {
                FileStream GeoIPDest = System.IO.File.Create(path);
                using (Stream fs = MainActivity.assets.Open("geoip"))
                {
                    fs.CopyTo(GeoIPDest);
                }

                GeoIPDest.Close();
            }

            //geoip
            path = folder + "/geoip6";
            if (!System.IO.File.Exists(path))
            {
                FileStream GeoIP6Dest = System.IO.File.Create(path);
                using (Stream fs = MainActivity.assets.Open("geoip6"))
                {
                    fs.CopyTo(GeoIP6Dest);
                }

                GeoIP6Dest.Close();
            }

            //data folder
            path = folder + "data";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            //Polipo
            path = folder + "/polipo";
            if (!System.IO.File.Exists(path))
            {
                FileStream PolipoDest = System.IO.File.Create(path);
                using (Stream fs = MainActivity.assets.Open("polipo"))
                {
                    fs.CopyTo(PolipoDest);
                }
                PolipoDest.Close();

                Java.IO.File filePolipo = new Java.IO.File(path);
                filePolipo.SetExecutable(true);
            }

            path = folder + "/polipo.conf";
            if (!System.IO.File.Exists(path))
            {
                FileStream PolipoConfDest = System.IO.File.Create(path);
                using (Stream fs = MainActivity.assets.Open("polipo.conf"))
                {
                    fs.CopyTo(PolipoConfDest);
                }
                PolipoConfDest.Close();
            }
        }
    }

}