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
//using Plugin.GoogleAnalytics;
using System.Reactive.Disposables;

namespace Target.Pages
{
    // I have to disable all xml compilation or I can't hit breakpoints while degugging.  Enable for production
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonPage : ContentPageBase<PersonPageViewModel>, IPersonPage
    {
        public PersonPage()
        {
            InitializeComponent();
            ViewModel = (PersonPageViewModel)App.Container.Resolve<IPersonPageViewModel>();
            
            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.Title, x => x.Title)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(ViewModel, vm => vm.Greeting, x => x.lbl.Text)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.lbl.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.btnActivities.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this.btnActivities.Events().Clicked
                            .Subscribe(async (_) =>
                            {
                                var activitiesPage = (Page)App.Container.Resolve<IActivitiesPage>();
                                await Navigation.PushAsync(activitiesPage);
                            })
                            .DisposeWith(disposables);
                    });
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            //GoogleAnalytics.Current.Tracker.SendView("PersonPage");
        }        
    }
}