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
using Android.Content.PM;

namespace Target.Android
{
    [Activity(Label = "MYAPP", Icon = "@mipmap/icon", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    //[IntentFilter(
    //    new[] { Intent.ActionView },
    //    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    //    DataScheme = "myapp.app.scheme"
    //)]
    public class Splash : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}