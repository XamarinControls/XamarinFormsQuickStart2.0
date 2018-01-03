using System;
using System.IO;
using Target.Interfaces;
using Target.UWP;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformStuff))]
namespace Target.UWP
{
    public class PlatformStuff : IPlatformStuff
    {
        //public string GetPersonalPath => Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        public string GetLocalFilePath(string filename) => Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        public string GetBaseUrl()
        {
            return "ms-appx-web:///" + "web/";
        }
    }
}
