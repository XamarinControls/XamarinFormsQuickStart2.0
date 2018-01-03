using Autofac;
using Plugin.GoogleAnalytics;
using Plugin.Toasts;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Factories;
using Target.Interfaces;
using Target.Services;
using Target.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsPage : ContentPageBase<TermsPageViewModel>, ITermsPage
    {
        ISettingsFactory _settingsFactory;
        ISettingsService _settingsService;
        
        public TermsPage()
        {
            InitializeComponent();
            ViewModel = (TermsPageViewModel)App.Container.Resolve<ITermsPageViewModel>();
            _settingsService = (SettingsService)App.Container.Resolve<ISettingsService>();
            _settingsFactory = (SettingsFactory)App.Container.Resolve<ISettingsFactory>();
            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.Greeting, x => x.lbl.Text)
                            .DisposeWith(disposables);                        
                        this.btnAgree.Events().Clicked.Throttle(TimeSpan.FromMilliseconds(150), RxApp.MainThreadScheduler)
                            .Subscribe(async x => {
                                await this.SetAgreement();
                                MessagingCenter.Send<ITermsPage>(this, "mTermsAgreed");
                            }).DisposeWith(disposables);
                        // hide btnAgree if terms are already agreed to
                        this
                            .OneWayBind(ViewModel, vm => vm.IsTermsOn, x => x.btnAgree.IsVisible)
                            .DisposeWith(disposables);
                        this.btnDisagree.Events().Clicked.Throttle(TimeSpan.FromMilliseconds(150), RxApp.MainThreadScheduler)
                            .Subscribe(x => {
                                var goodbyepage = (Page)App.Container.Resolve<IGoodByePage>();
                                Navigation.PushModalAsync(goodbyepage);
                            }).DisposeWith(disposables);
                        // hide btnDisagree if terms are already agreed to
                        this
                            .OneWayBind(ViewModel, vm => vm.IsTermsOn, x => x.btnDisagree.IsVisible)
                            .DisposeWith(disposables);
                        this
                           .OneWayBind(ViewModel, vm => vm.IsTermsOn, x => x.lblAgreedOnLabel.IsVisible, vmToViewConverterOverride: reverseBoolConverter)
                           .DisposeWith(disposables);
                        this
                           .OneWayBind(ViewModel, vm => vm.IsTermsOn, x => x.lblAgreedOn.IsVisible, vmToViewConverterOverride: reverseBoolConverter)
                           .DisposeWith(disposables);
                        this
                           .OneWayBind(ViewModel, vm => vm.HTMLSource, x => x.webview.Source)
                           .DisposeWith(disposables);
                        this
                           .OneWayBind(ViewModel, vm => vm.AgreedOn, x => x.lblAgreedOn.Text)
                           .DisposeWith(disposables);
                    });
            
        }
        private async Task SetAgreement()
        {
            var setting = _settingsFactory.GetSettings();
            setting.AgreedToTermsDate = DateTime.Now.ToString();
            var settings = await _settingsService.CreateSetting(setting);
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            GoogleAnalytics.Current.Tracker.SendView("TermsPage");
        }
    }
}