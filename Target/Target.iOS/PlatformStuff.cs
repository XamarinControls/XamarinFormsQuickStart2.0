using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Target.iOS;
using Target.Interfaces;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformStuff))]
namespace Target.iOS
{
    class PlatformStuff : IPlatformStuff
    {
        //public string GetPersonalPath => System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        public string GetBaseUrl()
        {
            return NSBundle.MainBundle.BundlePath + "/web";
        }
    }
}