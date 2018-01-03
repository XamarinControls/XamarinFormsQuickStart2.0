using Autofac;
using Target.Interfaces;
using Target.Models;
using Target.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Diagnostics;
using Plugin.Toasts;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPageBase<MasterPageViewModel>,  IMasterPage
    {
        
        NavigationPage _homePage = new NavigationPage(new HomePage());
        public MasterPage()
        {
            InitializeComponent();
            ViewModel = (MasterPageViewModel)App.Container.Resolve<IMasterPageViewModel>();
            this.MasterBehavior = MasterBehavior.Popover;
            MessagingCenter.Subscribe<ILogoutPage>(this, "GoHome", (sender) =>
            {
                this.Detail = _homePage;
            });
            _homePage.BarBackgroundColor = Constants.ToolbarColor;
            _homePage.BarTextColor = Constants.ToolbarTextColor;
            this.Detail = _homePage;
            masterPage.BackgroundColor = Constants.SideMenuColor;
            masterPage.ListView.BackgroundColor = Constants.SideMenuColor;

            this
                .WhenActivated(
                    disposables =>
                    {
                        Observable.FromEventPattern<SelectedItemChangedEventArgs>(h => masterPage.ListView.ItemSelected += h,
                                                    h => masterPage.ListView.ItemSelected -= h)
                                .Subscribe(x => OnItemSelected(x.Sender, x.EventArgs))
                                .DisposeWith(disposables);
                        Observable.FromEventPattern<ConnectivityChangedEventHandler, ConnectivityChangedEventArgs>(h => CrossConnectivity.Current.ConnectivityChanged += h,
                                                                                  h => CrossConnectivity.Current.ConnectivityChanged -= h)
                                 .Subscribe(x => {
                                     var connection = (x.EventArgs.IsConnected) ? "Connected" : "disconnected";
                                     if (setting.ShowConnectionErrors)
                                     {
                                         ShowToast(new NotificationOptions()
                                         {
                                             Title = "Connection",
                                             Description = $"Connectivity changed to {connection}",
                                             IsClickable = true,
                                             //WindowsOptions = new WindowsOptions() { LogoUri = "icon.png" },
                                             ClearFromHistory = true
                                         });
                                     }                                     
                                 })
                                 .DisposeWith(disposables);
                    });
        }
        
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            
        }
        protected override void OnDisappearing()
        {
            // Not guaranteed to occur, Cannot be depended apon. 
            MessagingCenter.Unsubscribe<ILogoutPage>(this, "GoHome");
            base.OnDisappearing();
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterPageItem item)
            {
                var nextPage = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                nextPage.BarBackgroundColor = Constants.ToolbarColor;
                nextPage.BarTextColor = Constants.ToolbarTextColor;
                Detail = nextPage;
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}