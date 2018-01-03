using Autofac;
using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.GoogleAnalytics;
using Target.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPageBase<LogoutPageViewModel>, ILogoutPage
    {
        public LogoutPage()
        {
            InitializeComponent();

            ViewModel = (LogoutPageViewModel)App.Container.Resolve<ILogoutPageViewModel>();
            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(this.ViewModel, vm => vm.Greeting, x => x.WelcomeMessage.Text)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, vm => vm.FontSize, x => x.WelcomeMessage.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, vm => vm.FontSize, x => x.logoutButton.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, vm => vm.FontSize, x => x.cancelButton.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        Observable.FromEventPattern(
                                  ev => logoutButton.Clicked += ev,
                                  ev => logoutButton.Clicked -= ev
                             )
                             .Subscribe(async x =>
                             {
                                await OnLogoutClicked(x.Sender, x.EventArgs as EventArgs);
                             })
                             .DisposeWith(disposables);
                        Observable.FromEventPattern(
                                  ev => cancelButton.Clicked += ev,
                                  ev => cancelButton.Clicked -= ev
                             )
                             .Subscribe(async x =>
                             {
                                 await OnCancelClicked(x.Sender, x.EventArgs as EventArgs);
                             })
                             .DisposeWith(disposables);

                    });
        }

        private async Task OnCancelClicked(object sender, EventArgs eventArgs)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(0));
            MessagingCenter.Send<ILogoutPage>(this, "GoHome");
        }

        private async Task OnLogoutClicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopToRootAsync();
            MessagingCenter.Send<ILogoutPage>(this, "LogMeOut");
        }
        
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            GoogleAnalytics.Current.Tracker.SendView("LogoutPage");
        }
        protected override void OnDisappearing()
        {
            // Not guaranteed to occur, Cannot be depended apon. 
            base.OnDisappearing();
        }
    }
}