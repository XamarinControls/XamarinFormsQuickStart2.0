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

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPageBase<HomePageViewModel>, IHomePage
    {
        public HomePage()
        {
            InitializeComponent();
            ViewModel = (HomePageViewModel)App.Container.Resolve<IHomePageViewModel>();
            this.Title = Constants.AppName;
            this
                .WhenActivated(
                    disposables =>
                    {

                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.btnResync.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this.OneWayBind(ViewModel, vm => vm.FontSize, x => x.btnOrg.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this.OneWayBind(ViewModel, vm => vm.FontSize, view => view.ffimage.HeightRequest, x => GetSquaredImageSize(x))
                            .DisposeWith(disposables);
                        this.OneWayBind(ViewModel, vm => vm.FontSize, view => view.ffimage.WidthRequest, x => GetSquaredImageSize(x))
                            .DisposeWith(disposables);
                    });

            var orgPage = (Page)App.Container.Resolve<IOrganizationPage>();
            var resyncPage = (Page)App.Container.Resolve<IResyncPage>();
            btnOrg.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(orgPage);
            };
            btnResync.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(resyncPage);
            };
        }

    }
}