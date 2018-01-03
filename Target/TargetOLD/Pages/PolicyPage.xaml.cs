using Autofac;
using Plugin.GoogleAnalytics;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PolicyPage : ContentPageBase<PolicyPageViewModel>, IPolicyPage
    {
        public PolicyPage()
        {
            InitializeComponent();
            ViewModel = (PolicyPageViewModel)App.Container.Resolve<IPolicyPageViewModel>();
            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.Greeting, x => x.lbl.Text)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.lbl.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                           .OneWayBind(ViewModel, vm => vm.HTMLSource, x => x.webview.Source)
                           .DisposeWith(disposables);
                    });
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            GoogleAnalytics.Current.Tracker.SendView("PolicyPage");
        }
    }
}