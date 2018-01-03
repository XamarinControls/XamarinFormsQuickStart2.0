using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using FFImageLoading.Forms.Touch;
using FFImageLoading;
using Plugin.GoogleAnalytics;
using ObjCRuntime;

namespace Target.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var version = NSBundle.MainBundle.InfoDictionary[new NSString("CFBundleVersion")].ToString(); 

            GoogleAnalytics.Current.Config.TrackingId = "<YOUR ID>";
            GoogleAnalytics.Current.Config.AppId = "<YOUR APPID>";
            GoogleAnalytics.Current.Config.AppName = "<YOUR APPNAME>";
            GoogleAnalytics.Current.Config.AppVersion = "<YOUR APPVERSION>";
            GoogleAnalytics.Current.InitTracker();
            App.Version = version;
            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();

            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            try
            {
                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    Exception e = args.ExceptionObject as Exception;
                    String s = (e != null) ? ("Error Handled: " + e.Message + "\n" + e.StackTrace) :
                        ("Error Handled (not Exception): " + args.ExceptionObject.ToString());
                    GoogleAnalytics.Current.Tracker.SendException(s, false);
                    Console.WriteLine(s);

                };
            }
            catch (Exception)
            {
            }
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
        {
            public void Debug(string message)
            {
                Console.WriteLine(message);
            }

            public void Error(string errorMessage)
            {
                GoogleAnalytics.Current.Tracker.SendException(errorMessage, false);
                Console.WriteLine(errorMessage);
            }

            public void Error(string errorMessage, Exception ex)
            {
                Error(errorMessage + System.Environment.NewLine + ex.ToString());
            }
        }
    }
}
