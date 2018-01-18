using Autofac;
using ReactiveUI;
using Target.Interfaces;
using Target.ViewModels;
using Xamarin.Forms;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Plugin.Connectivity;
using System;
using Plugin.Toasts;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Target.Pages
{
    // I have to disable all xml compilation or I can't hit breakpoints while degugging.  Enable for production
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPageBase<HomePageViewModel>, IHomePage
    {
        public HomePage()
        {
            InitializeComponent();
            ViewModel = (HomePageViewModel)App.Container.Resolve<IHomePageViewModel>();
            
            this
                .WhenActivated(
                    disposables =>
                    {
                        this.OneWayBind(this.ViewModel, x => x.Title, x => x.Title)
                            .DisposeWith(disposables);
                        this.OneWayBind(this.ViewModel, x => x.FontSize, x => x.btnRecent.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this.OneWayBind(this.ViewModel, x => x.FontSize, x => x.btnPerson.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this.OneWayBind(ViewModel, vm => vm.FontSize, x => x.btnOrg.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this.OneWayBind(ViewModel, vm => vm.FontSize, view => view.ffimage.HeightRequest, x => GetSquaredImageSize(x))
                            .DisposeWith(disposables);
                        this.OneWayBind(ViewModel, vm => vm.FontSize, view => view.ffimage.WidthRequest, x => GetSquaredImageSize(x))
                            .DisposeWith(disposables);
                        this.btnPerson.Events().Clicked
                            .Subscribe(async (_) => {
                                var personPage = (Page)App.Container.Resolve<IPersonPage>();
                                await Navigation.PushAsync(personPage); })
                            .DisposeWith(disposables);
                        this.btnOrg.Events().Clicked
                            .Subscribe(async (_) =>
                            {
                                var orgPage = (Page)App.Container.Resolve<IOrganizationPage>();
                                await Navigation.PushAsync(orgPage);
                            })
                            .DisposeWith(disposables);
                        this.btnRecent.Events().Clicked
                            .Subscribe(async (_) =>
                            {
                                var recentPage = (Page)App.Container.Resolve<IRecentPage>();
                                await Navigation.PushAsync(recentPage);
                            })
                            .DisposeWith(disposables);
                    });
        }

    }
}