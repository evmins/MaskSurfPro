using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


//setting names
//Tor port
//Tor control port
//Tor DNS port
//Polipo port
//Start on boot
//Language


namespace MaskSurfPro
{
    static public class Settings
    {
        public static bool GetBool(string Name, bool DefaultValue)
        {
            return DependencyService.Get<ISettings>().GetBool(Name, DefaultValue);
        }
        public static int GetInt(string Name, int DefaultValue)
        {
            return DependencyService.Get<ISettings>().GetInt(Name, DefaultValue);
        }
        public static string GetString(string Name, string DefaultValue)
        {
            return DependencyService.Get<ISettings>().GetString(Name, DefaultValue);
        }
        public static ICollection<string> GetStringCollection(string Name)
        {
            return DependencyService.Get<ISettings>().GetStringCollection(Name);
        }

        static public void SetBool(string Name, bool Value)
        {
            DependencyService.Get<ISettings>().SetBool(Name,Value);
        }
        static public void SetInt(string Name, int Value)
        {
            DependencyService.Get<ISettings>().SetInt(Name, Value);
        }
        static public void SetString(string Name, string Value)
        {
            DependencyService.Get<ISettings>().SetString(Name, Value);
        }
        static public void SetStringCollection(string Name, ICollection<string> Value)
        {
            DependencyService.Get<ISettings>().SetStringCollection(Name, Value);
        }

        static public void Remove(string Name)
        {
            DependencyService.Get<ISettings>().Remove(Name);
        }
    }
    public interface ISettings
    {
        bool GetBool(string Name, bool DefaultValue);
        int GetInt(string Name, int DefaultValue);
        string GetString(string Name, string DefaultValue);
        ICollection<string> GetStringCollection(string Name);

        void SetBool(string Name, bool Value);
        void SetInt(string Name, int Value);
        void SetString(string Name, string Value);
        void SetStringCollection(string Name, ICollection<string> Value);

        void Remove(string Name);
    }
}
