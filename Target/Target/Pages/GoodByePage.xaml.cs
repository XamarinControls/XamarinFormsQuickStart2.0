using Autofac;
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
    // I have to disable all xml compilation or I can't hit breakpoints while degugging.  Enable for production
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodByePage : ContentPageBase<GoodByePageViewModel>, IGoodByePage
    {
        public GoodByePage()
        {
            InitializeComponent();
            ViewModel = (GoodByePageViewModel)App.Container.Resolve<IGoodByePageViewModel>();
            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.Title, x => x.Title)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(ViewModel, vm => vm.Greeting, x => x.lblThanks.Text)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.lblThanks.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                    });
        }
    }
}