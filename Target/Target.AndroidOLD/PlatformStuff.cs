using System;
using Android.OS;
using Target.Interfaces;
using System.IO;
using Target.Android;
using System.Runtime.Serialization.Formatters.Binary;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformStuff))]
namespace Target.Android
{
    public class PlatformStuff : IPlatformStuff
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public string GetBaseUrl()
        {
            //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            return "file:///android_asset/web/";
        }
    }
}