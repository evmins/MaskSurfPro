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

[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.SettingsOnAndroid))]

namespace MaskSurfPro.Droid
{
    public class SettingsOnAndroid : ISettings
    {
        private static ISharedPreferences prefManager;
        static public ISharedPreferences PrefManager
        {
            set { prefManager = value; }
        }

        public bool GetBool(string Name, bool DefaultValue)
        {
            return prefManager.GetBoolean(Name, DefaultValue);
        }
        public int GetInt(string Name, int DefaultValue)
        {
            return prefManager.GetInt(Name, DefaultValue);
        }
        public string GetString(string Name, string DefaultValue)
        {
            return prefManager.GetString(Name, DefaultValue);
        }
        public ICollection<string> GetStringCollection(string Name)
        {
            return prefManager.GetStringSet(Name, null);
        }

        public void SetBool(string Name, bool Value)
        {
            ISharedPreferencesEditor editor = prefManager.Edit();
            editor.PutBoolean(Name, Value);
            editor.Commit();
        }
        public void SetInt(string Name, int Value)
        {
            ISharedPreferencesEditor editor = prefManager.Edit();
            editor.PutInt(Name, Value);
            editor.Commit();
        }
        public void SetString(string Name, string Value)
        {
            ISharedPreferencesEditor editor = prefManager.Edit();
            editor.PutString(Name, Value);
            editor.Commit();
        }
        public void SetStringCollection(string Name, ICollection<string> Value)
        {
            ISharedPreferencesEditor editor = prefManager.Edit();
            editor.PutStringSet(Name, Value);
            editor.Commit();
        }
        public void Remove(string Name)
        {
            ISharedPreferencesEditor editor = prefManager.Edit();
            editor.Remove(Name);
            editor.Commit();
        }
    }
}