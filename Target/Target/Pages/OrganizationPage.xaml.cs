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
using Target.Templates;

namespace Target.Pages
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrganizationPage : ContentPageBase<OrganizationPageViewModel>, IOrganizationPage
    {
        ListView listView;
        StackLayout baseLayout;

        public OrganizationPage()
        {

            InitializeComponent();
            ViewModel = (OrganizationPageViewModel)App.Container.Resolve<IOrganizationPageViewModel>();
            var personPage = (Page)App.Container.Resolve<IPersonPage>();
            baseLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            var dtemplate = new DataTemplate(() =>
            {
                var stacklayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(10, 10, 10, 10),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                var lbl = new LabelForReactive();


                var ffimg = new ImageForReactive();
                stacklayout.Children.Add(ffimg);

                stacklayout.Children.Add(lbl);
                return new ViewCell { View = stacklayout };
            });
            listView = new ListView()
            {
                HasUnevenRows = false,
                SeparatorVisibility = SeparatorVisibility.None,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                ItemTemplate = dtemplate
            };
            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.Title, x => x.Title)
                            .DisposeWith(disposables);
                        //this
                        //    .OneWayBind(ViewModel, vm => vm.Greeting, x => x.lbl.Text)
                        //    .DisposeWith(disposables);
                        //this
                        //    .OneWayBind(this.ViewModel, x => x.FontSize, x => x.lbl.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                        //    .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.Items, x => x.listView.ItemsSource)
                            .DisposeWith(disposables);
                    });
            baseLayout.Children.Add(listView);
            Content = baseLayout;
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            //GoogleAnalytics.Current.Tracker.SendView("OrganizationPage");
        }
    }
}