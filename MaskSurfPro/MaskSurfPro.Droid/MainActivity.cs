using System;
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


namespace MaskSurfPro.Droid
{
    [Activity(Label = "Mask Surf Pro", Theme = "@style/MainTheme", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, ILicenseCheckerCallback
    {
        private LicenseChecker checker;

        public static Android.Content.Res.Resources Res { get; private set;}
        public static AssetManager assets { get; private set; }
        public string DBDirPath
        {
            get; private set;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Res = Resources;
            assets = this.Assets;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new MSProApp());

                CheckLicense();

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
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            string str = e.Message;
            System.Diagnostics.Debug.WriteLine(str);
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
            alert.SetPositiveButton(AppStrings.OK, (senderAlert, args) => {
            });

            Dialog dialog = alert.Create();
            dialog.Show();
            */
        }
        public void DontAllow(PolicyServerResponse reason)
        {
            string message = MSProApp.Locator.StatusVM.NotLicensed;

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            
            alert.SetTitle(MSProApp.Locator.StatusVM.WarningLabel);
            alert.SetMessage(message);
            alert.SetCancelable(false);
            alert.SetPositiveButton(MSProApp.Locator.StatusVM.OKLabel, (senderAlert, args) => 
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
            string message = MSProApp.Locator.StatusVM.GooglePlayNotAvailable;

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(MSProApp.Locator.StatusVM.WarningLabel);
            alert.SetMessage(message);
            alert.SetCancelable(false);
            alert.SetPositiveButton(MSProApp.Locator.StatusVM.OKLabel, (senderAlert, args) => 
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

}

