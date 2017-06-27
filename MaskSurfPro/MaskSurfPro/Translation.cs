using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;

namespace MaskSurfPro
{
    public static class Translation
    {
        public static string GetString(string StringID)
        {
            switch (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
            {
                case "ru":
                    {
                        return DependencyService.Get<ITranslate>().GetRussian(StringID);
                    }
                case "en":
                default:
                    {
                        return DependencyService.Get<ITranslate>().GetEnglish(StringID);
                    }
            }
        }
    }
    public interface ITranslate
    {
        string GetEnglish(string StringID);
        string GetRussian(string StringID);
    }
}
