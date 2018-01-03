using Autofac;
using Target.Interfaces;
using Target.ViewModels;
using ReactiveUI;
using Plugin.GoogleAnalytics;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesPage : ContentPageBase<ActivitiesPageViewModel>, IActivitiesPage
    {
        public ActivitiesPage()
        {            
            InitializeComponent();
            ViewModel = (ActivitiesPageViewModel)App.Container.Resolve<IActivitiesPageViewModel>();
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
                    });
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            GoogleAnalytics.Current.Tracker.SendView("ActivitiesPage");
        }
    }
}