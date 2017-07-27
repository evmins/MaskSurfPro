using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Util;

using MaskSurfPro.Droid;


[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.DisplaySizeOnAndroid))]

namespace MaskSurfPro.Droid
{
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
            //int dp = (int)((metrics.WidthPixels) / metrics.Density);
            int dp = (int)(metrics.WidthPixels / (metrics.Xdpi / 160));
            return dp;
        }
        public int GetHeightDiP()
        {
            DisplayMetrics metrics = MainActivity.Res.DisplayMetrics;
            //int dp = (int)((metrics.HeightPixels) / metrics.Density);
            int dp = (int)(metrics.HeightPixels / (metrics.Ydpi / 160));
            return dp;
        }
        public float GetDensity()
        {
            DisplayMetrics metrics = MainActivity.Res.DisplayMetrics;
            return metrics.Density;
        }
    }
}