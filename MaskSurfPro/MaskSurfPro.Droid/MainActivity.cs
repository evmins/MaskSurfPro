using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Android.Content;
using Android.Util;
using Android.Preferences;
using Xamarin.Forms;
using System.IO;
using System.Globalization;

using LicenseVerificationLibrary;
using LicenseVerificationLibrary.Obfuscator;
using LicenseVerificationLibrary.Policy;

[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.DisplaySizeOnAndroid))]

namespace MaskSurfPro.Droid
{
    [Activity(Label = "Mask Surf Pro", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, ILicenseCheckerCallback
    {
        //Google Play app specific values removed

        public static Resources Res { get; private set;}
        public static AssetManager assets { get; private set; }
        public string DBFilePath
        {
            get; private set;
        }
        public string DBDirPath
        {
            get; private set;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            DBDirPath = Android.App.Application.Context.GetDatabasePath("a").AbsolutePath;
            DBDirPath = DBDirPath.Remove(DBDirPath.Length - 1);
            DBFilePath = Android.App.Application.Context.GetDatabasePath("languages.db").AbsolutePath;
            CheckLangsDB();
            //AndroidUtils.Res = Resources;
            Res = Resources;
            assets = this.Assets;
            if (Device.Idiom == TargetIdiom.Phone)
            {
                RequestedOrientation = ScreenOrientation.SensorPortrait;
            }
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                RequestedOrientation = ScreenOrientation.SensorLandscape;
            }

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new MSProApp());

            //CheckLicense();

            AndroidUtils.activity = this;
            try
            {
                SettingsOnAndroid.PrefManager = Android.App.Application.Context.GetSharedPreferences("Mask Surf Pro", 0);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            AndroidUtils.CopyBinaries();

            AndroidUtils.StartTor();
        }
        protected override void OnStart()
        {
            base.OnStart();

            MessagingCenter.Subscribe<MainActivity>(this, "FalseIPScannerStart", (sender) =>
            {
                StartService(new Intent(this, typeof(FalseIPScannerOnAndroidService)));
                //binder.GetService().Start();
            });
        }
        protected void CheckLangsDB()
        {
            //DBs
            System.String path = DBFilePath;

            if (!System.IO.Directory.Exists(DBDirPath))
            {
                System.IO.Directory.CreateDirectory(DBDirPath);
            }
        //    if (!System.IO.File.Exists(DBFilePath))
         //   {

                FileStream LanguagesDBDest = System.IO.File.Create(DBFilePath);
                using (Stream fs = Assets.Open("languages.db"))
                {
                    fs.CopyTo(LanguagesDBDest);
                }
                LanguagesDBDest.Close();
           // }
        }
        private void CheckLicense()
        {
            // Try to use more data here. ANDROID_ID is a single point of attack.
            //string deviceId = Settings.Secure.GetString(ContentResolver, Settings.Secure.AndroidId);

            // Construct the LicenseChecker with a policy.
            //var obfuscator = new AesObfuscator(Salt, this.PackageName, deviceId);
            StrictPolicy policy = new StrictPolicy();
            this.checker = new LicenseChecker(this, policy, Base64PublicKey);
            this.checker.CheckAccess(this);
        }

        //license check interfaces
        public void Allow(PolicyServerResponse reason)
        {
            /*
            string message;

            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru")
            {
                message = "Лицензия действительна";
            }
            else
            {
                message = "License is valid";
            }

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Mask Surf Pro");
            alert.SetMessage(message);
            alert.SetCancelable(false);
            alert.SetPositiveButton(Translation.GetString("OK"), (senderAlert, args) => {
            });

            Dialog dialog = alert.Create();
            dialog.Show();
            */
        }
        public void DontAllow(PolicyServerResponse reason)
        {
            string message = Translation.GetString("You application is not licensed");

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(Translation.GetString("Warning"));
            alert.SetMessage(message);
            alert.SetCancelable(false);
            alert.SetPositiveButton(Translation.GetString("OK"), (senderAlert, args) => 
            {
                if (AndroidUtils.TorProc != null)
                {
                    AndroidUtils.TorProc.Destroy();
                }
                if (AndroidUtils.PolipoProc != null)
                {
                    AndroidUtils.PolipoProc.Destroy();
                }
                Finish();
                System.Environment.Exit(0);
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }
        public void ApplicationError(CallbackErrorCode errorCode)
        {
            string message = Translation.GetString("Can't obtain license information from Google Play");

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(Translation.GetString("Warning"));
            alert.SetMessage(message);
            alert.SetCancelable(false);
            alert.SetPositiveButton(Translation.GetString("OK"), (senderAlert, args) => 
            {
                Finish();
                if (AndroidUtils.TorProc != null)
                {
                    AndroidUtils.TorProc.Destroy();
                }
                if (AndroidUtils.PolipoProc != null)
                {
                    AndroidUtils.PolipoProc.Destroy();
                }
                 System.Environment.Exit(0);
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

    }
    public class DisplaySizeOnAndroid : IDisplaySize
    {
        public DisplaySizeOnAndroid()
        {
        }

        public int GetWidth()
        {
            DisplayMetrics metrics = MainActivity.Res.DisplayMetrics;
            return metrics.WidthPixels;
        }
        public int GetHeight()
        {
            DisplayMetrics metrics = MainActivity.Res.DisplayMetrics;
            return metrics.HeightPixels;
        }
        public int GetWidthDiP()
        {
            DisplayMetrics metrics = MainActivity.Res.DisplayMetrics;
            int dp = (int)((metrics.WidthPixels) / metrics.Density);
            return dp;
        }
        public int GetHeightDiP()
        {
            DisplayMetrics metrics = MainActivity.Res.DisplayMetrics;
            int dp = (int)((metrics.HeightPixels) / metrics.Density);
            return dp;
        }
        public float GetDensity()
        {
            DisplayMetrics metrics = MainActivity.Res.DisplayMetrics;
            return metrics.Density;
        }
    }
}

