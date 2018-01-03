using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading;
using FFImageLoading.Forms.Droid;
using Newtonsoft.Json;
using Plugin.GoogleAnalytics;
using Plugin.Toasts;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Target.Android
{
    [Activity(Label = "MYAPP", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            GoogleAnalytics.Current.Config.TrackingId = "<Your Trackit ID>";
            GoogleAnalytics.Current.Config.AppId = "<Your App ID";
            GoogleAnalytics.Current.Config.AppName = "<Your App Name>";
            GoogleAnalytics.Current.Config.AppVersion = "<Your App Version>";            
            GoogleAnalytics.Current.InitTracker();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentOnUnhandledException;
            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Context context = this.ApplicationContext;
            App.Version = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            CachedImageRenderer.Init(false);

            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Internet) == (int)Permission.Granted)
            //{
            //    Toast.MakeText(this, "Internet Enabled", ToastLength.Long).Show();
            //   // Console.WriteLine("yes, internet perms enabled");
            //}
            //else
            //{
            //    Toast.MakeText(this, "Internet Disabled", ToastLength.Long).Show();
            //    //Console.WriteLine("no, internet blocked");
            //}
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            DependencyService.Register<ToastNotification>();
            ToastNotification.Init(this);
            LoadApplication(new App());
        }
        
        private void AndroidEnvironmentOnUnhandledException(object sender, RaiseThrowableEventArgs e)
        {
            GoogleAnalytics.Current.Tracker.SendException(e.Exception, false);
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            GoogleAnalytics.Current.Tracker.SendException(e.Exception, false);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GoogleAnalytics.Current.Tracker.SendException(JsonConvert.SerializeObject(e.ExceptionObject), false);
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

