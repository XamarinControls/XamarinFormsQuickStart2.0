using Akavache;
using Autofac;
using Target.Converters;
using Target.Factories;
using Target.Interfaces;
using Target.Models;
using Target.Pages;
using Target.Repositories;
using Target.Services;
using Target.ViewModels;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.GoogleAnalytics;
using System;
using Plugin.Toasts;
using Plugin.Connectivity;
using System.Linq;

namespace Target
{
    public partial class App : Application
    {
        MasterDetailPage masterPage;
        Page loginPage;
        public static string Device { get; set; }
        public static string Version { get; set; }
        public static bool loggedIn { get; set; }
        public static IContainer Container { get; set; }
        ISettingsFactory _settingsFactory;
        ISettingsService _settingsService;
        Settings setting;
        public App()
        {
            InitializeComponent();
           
            SetupIOC();

            MessagingCenter.Subscribe<ILoginViewModel, bool>(this, "LoginStatus", (sender, args) =>
            {
                loggedIn = args;
                if (loggedIn)
                {
                    loadMainPage();
                }
                else
                {
                    ShowToast(new NotificationOptions()
                    {
                        Title = "Oops!",
                        Description = $"Login Failed!",
                        IsClickable = true,
                        ClearFromHistory = true
                    });
                }
            });
            MessagingCenter.Subscribe<ITermsPage>(this, "mTermsAgreed", (sender) =>
            {
                if (Constants.IsLoginPageEnabled)
                {
                    loadLoginPage();
                } 
                else
                {
                    loadMainPage();
                }
                    
            });
            MessagingCenter.Subscribe<ILogoutPage>(this, "LogMeOut", (sender) =>
            {
                loggedIn = false;
                loadLoginPage();
            });  

            Task.Run(async () => { await InitializeSettings(); }).Wait();            
            
            setting = _settingsFactory.GetSettings();

            if (Constants.IsTermsPageEnabled)
            {
                if (string.IsNullOrEmpty(setting.AgreedToTermsDate))
                {
                    loadTermsPage();
                } 
                else
                {
                    ContinueWithoutTerms();
                }
            }
            else
            {
                // if we've disabled the terms page, go ahead and wipe out the AgreedToTermsDate, this way we can easily test
                // reseting the terms without having to uninstall
                if (!string.IsNullOrEmpty(setting.AgreedToTermsDate))
                {
                    setting.AgreedToTermsDate = "";
                    var settings = _settingsService.CreateSetting(setting);
                }

                ContinueWithoutTerms();

            }
            if (Constants.CheckInternet && setting.ShowConnectionErrors)
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    ShowToast(new NotificationOptions()
                    {
                        Title = "No Connection",
                        Description = $"Please check your internet connection!",
                        IsClickable = true,
                        //WindowsOptions = new WindowsOptions() { LogoUri = "icon.png" },
                        ClearFromHistory = true
                        //DelayUntil = DateTime.Now.AddSeconds(0)
                    });
                }
            }
            CrossConnectivity.Current.ConnectivityChanged += ConnectionError;
            var wifi = Plugin.Connectivity.Abstractions.ConnectionType.WiFi;
            if (Constants.ShowWifiErrors)
            {
                var connectionTypes = CrossConnectivity.Current.ConnectionTypes;
                if (!connectionTypes.Contains(wifi))
                {
                    ShowToast(new NotificationOptions()
                    {
                        Title = "Wifi Not Detected",
                        Description = $"Please turn Wifi On!",
                        IsClickable = true,
                        //WindowsOptions = new WindowsOptions() { LogoUri = "icon.png" },
                        ClearFromHistory = true
                        //DelayUntil = DateTime.Now.AddSeconds(0)
                    });

                }
            }
            
        }

        private void ContinueWithoutTerms()
        {
            if (Constants.IsLoginPageEnabled)
            {
                loadLoginPage();                
            }
            else
            {
                loggedIn = true;
                loadMainPage();
            }
        }
        //private async Task RunStuffAsync()
        //{
        //    //var sqlservice = App.Container.Resolve<ISQLService>();
        //    //await sqlservice.CreateDB();
        //}

        private async Task InitializeSettings()
        {
            _settingsService = App.Container.Resolve<ISettingsService>();
            await _settingsService.CheckSettings(); // seeds the settings in SQLite if empty
            var settings = await _settingsService.GetSettings();
            _settingsFactory = App.Container.Resolve<ISettingsFactory>();
            _settingsFactory.SaveSettings((Settings)settings);
            
        }
        private void loadTermsPage()
        {
            var termsPage = (Page)App.Container.Resolve<ITermsPage>();
            MainPage = termsPage;
        }
        private void loadMainPage()
        {
            masterPage = (MasterDetailPage)App.Container.Resolve<IMasterPage>();            
            MainPage = masterPage;
        }
        private void loadLoginPage()
        {
            loginPage = (Page)App.Container.Resolve<ILoginPage>();
            MainPage = loginPage;
        }
        private void SetupIOC()
        {
            var dbpath = DependencyService.Get<IPlatformStuff>().GetLocalFilePath(Constants.AppName + ".db3");
            var builder = new ContainerBuilder();
            builder.RegisterType<MasterPage>().As<IMasterPage>();            
            builder.RegisterType<MasterPageViewModel>().As<IMasterPageViewModel>();
            builder.RegisterType<OrganizationPage>().As<IOrganizationPage>();
            builder.RegisterType<PersonPage>().As<IPersonPage>();
            builder.RegisterType<SettingsPage>().As<ISettingsPage>();
            builder.RegisterType<ResyncPage>().As<IResyncPage>();
            builder.RegisterType<ActivitiesPage>().As<IActivitiesPage>();
            builder.RegisterType<HomePage>().As<IHomePage>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance().AutoActivate();
            builder.RegisterType<SettingsFactory>().As<ISettingsFactory>().SingleInstance();
            if (Constants.IsLoginPageEnabled)
            {
                builder.RegisterType<LoginPage>().As<ILoginPage>();
                builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
                builder.RegisterType<LogoutPageViewModel>().As<ILogoutPageViewModel>();
                builder.RegisterType<LogoutPage>().As<ILogoutPage>();
            }
            builder.RegisterType<SettingsViewModel>().As<ISettingsViewModel>();
            builder.RegisterType<HomePageViewModel>().As<IHomePageViewModel>();
            builder.RegisterType<MasterListViewModel>().As<IMasterListViewModel>();
            builder.RegisterType<ActivitiesPageViewModel>().As<IActivitiesPageViewModel>();
            builder.RegisterType<OrganizationPageViewModel>().As<IOrganizationPageViewModel>();
            builder.RegisterType<PersonPageViewModel>().As<IPersonPageViewModel>();
            builder.RegisterType<ResyncPageViewModel>().As<IResyncPageViewModel>();
            builder.RegisterType<SQLiteRepository>().As<ISQLiteRepository>();           
            builder.RegisterType<DoubleToIntConverter>().As<IDoubleToIntConverter>();
            builder.RegisterType<IntToDoubleConverter>().As<IIntToDoubleConverter>();
            builder.RegisterType<PaddingTopBottomConverter>().As<IPaddingTopBottomConverter>();
            builder.RegisterType<ReverseBoolConverter>().As<IReverseBoolConverter>();
            builder.RegisterType<AboutPage>().As<IAboutPage>();
            if (Constants.IsTermsPageEnabled)
            {
                builder.RegisterType<TermsPage>().As<ITermsPage>();
                builder.RegisterType<TermsPageViewModel>().As<ITermsPageViewModel>();
                builder.RegisterType<PolicyPageViewModel>().As<IPolicyPageViewModel>();
                builder.RegisterType<PolicyPage>().As<IPolicyPage>();                
            }
            
            builder.RegisterType<AboutPageViewModel>().As<IAboutPageViewModel>();
            builder.RegisterType<GoodByePage>().As<IGoodByePage>();
            builder.RegisterType<SvgImageSourceConverterForReactive>().As<ISvgImageSourceConverterForReactive>();
            ;

            Container = builder.Build();
        }
        public void ConnectionError(Object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs args)
        {
            if (setting.ShowConnectionErrors)
            {
                var title = "";
                var description = "";
                if (args.IsConnected)
                {
                    title = "Network Connection";
                    description = "Successfully Connected :)";
                }
                else
                {
                    title = "Network Connection";
                    description = "No Connection Detected";
                }

                ShowToast(new NotificationOptions()
                {
                    Title = title,
                    Description = description,
                    IsClickable = true,
                    //WindowsOptions = new WindowsOptions() { LogoUri = "icon.png" },
                    ClearFromHistory = true,
                    DelayUntil = DateTime.Now.AddSeconds(2)
                });
            }
        }
        private async void ShowToast(INotificationOptions options)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            if (notificator != null)
            {
                var result = await notificator.Notify(options);
            }


        }

        protected override void OnSleep()
        {
            BlobCache.Shutdown().Wait();
            MessagingCenter.Unsubscribe<ILoginViewModel, bool>(this, "LoginStatus");
            MessagingCenter.Unsubscribe<ILogoutPage>(this, "LogMeOut");
            MessagingCenter.Unsubscribe<ITermsPage>(this, "mTermsAgreed");
            CrossConnectivity.Current.ConnectivityChanged -= ConnectionError;
            // Handle when your app sleeps
        }
        

        protected override void OnResume()
        {

            // Handle when your app resumes
        }

    }
}