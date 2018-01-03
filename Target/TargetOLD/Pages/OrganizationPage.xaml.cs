using Autofac;
using Target.Interfaces;
using Target.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.GoogleAnalytics;
using System.Reactive.Disposables;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrganizationPage : ContentPageBase<OrganizationPageViewModel>, IOrganizationPage
    {
        public OrganizationPage()
        {
            
            InitializeComponent();
            ViewModel = (OrganizationPageViewModel)App.Container.Resolve<IOrganizationPageViewModel>();
            var personPage = (Page)App.Container.Resolve<IPersonPage>();
            btnPerson.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(personPage);
            };
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
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.btnPerson.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                    });
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            GoogleAnalytics.Current.Tracker.SendView("OrganizationPage");
        }        
    }
}