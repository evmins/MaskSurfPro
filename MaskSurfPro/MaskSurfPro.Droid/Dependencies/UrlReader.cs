using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Java.Net;
using Java.IO;

using MaskSurfPro.Models;

[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.UrlReader))]

namespace MaskSurfPro.Droid
{
    public class UrlReader : IGetUrl
    {
        //code removed
    }
}