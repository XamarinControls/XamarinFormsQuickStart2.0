using FFImageLoading;
using Plugin.GoogleAnalytics;
using Plugin.Toasts.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;

namespace Target.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Windows.UI.Xaml.Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Windows.UI.Xaml.Controls.Frame rootFrame = Window.Current.Content as Windows.UI.Xaml.Controls.Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                var version = GetAppVersion();
                DependencyService.Register<ToastNotification>(); // Register your dependency
                ToastNotification.Init();
                GoogleAnalytics.Current.Config.TrackingId = "<YOUR ID>";
                GoogleAnalytics.Current.Config.AppId = "<YOUR APPID>";
                GoogleAnalytics.Current.Config.AppName = "<YOUR APPNAME>";
                GoogleAnalytics.Current.Config.AppVersion = "<YOUR APPVERSION>";
                GoogleAnalytics.Current.InitTracker();
                Target.App.Version = version;
                
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Windows.UI.Xaml.Controls.Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                

                FFImageLoading.Forms.WinUWP.CachedImageRenderer.Init();

                var config = new FFImageLoading.Config.Configuration()
                {
                    VerboseLogging = false,
                    VerbosePerformanceLogging = false,
                    VerboseMemoryCacheLogging = false,
                    VerboseLoadingCancelledLogging = false,
                    Logger = new CustomLogger(),
                };
                ImageService.Instance.Initialize(config);

                List<Assembly> assembliesToInclude = new List<Assembly>();
                assembliesToInclude.Add(typeof(FFImageLoading.Forms.WinUWP.CachedImageRenderer).GetTypeInfo().Assembly);
                UnhandledException += OnUnhandledExecption;
                Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
            
            
        }
        
        private string GetAppVersion()
        {

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

        }
        private void OnUnhandledExecption(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            GoogleAnalytics.Current.Tracker.SendException(e.Message, false);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            GoogleAnalytics.Current.Tracker.SendException(e.Exception, false);
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
        {
            public void Debug(string message)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            public void Error(string errorMessage)
            {
                GoogleAnalytics.Current.Tracker.SendException(errorMessage, false);
                System.Diagnostics.Debug.WriteLine(errorMessage);
            }

            public void Error(string errorMessage, Exception ex)
            {
                Error(errorMessage + System.Environment.NewLine + ex.ToString());
            }
        }
    }
}
