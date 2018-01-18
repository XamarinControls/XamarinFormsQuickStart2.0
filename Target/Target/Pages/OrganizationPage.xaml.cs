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
using System.Reactive.Linq;

namespace Target.Pages
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrganizationPage : ContentPageBase<OrganizationPageViewModel>, IOrganizationPage
    {
        ListView listView;
        StackLayout baseLayout;
        MenuItem editAction;
        MenuItem viewAction;

        public OrganizationPage()
        {

            InitializeComponent();
            ViewModel = (OrganizationPageViewModel)App.Container.Resolve<IOrganizationPageViewModel>();
            viewAction = new MenuItem { Text = "View" };
            viewAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            editAction = new MenuItem { Text = "Edit" }; 
            editAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
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
                var viewCell = new ViewCell();
                
                //deleteAction.Clicked += OnDelete;

                viewCell.ContextActions.Add(viewAction);
                viewCell.ContextActions.Add(editAction);
                viewCell.View = stacklayout;
                return viewCell;
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
                        Observable.FromEventPattern(
                             ev => viewAction.Clicked += ev,
                             ev => viewAction.Clicked -= ev
                        )
                        .Subscribe(x =>
                        {
                            OnView(x.Sender, x.EventArgs as EventArgs);
                        })
                        .DisposeWith(disposables);

                        Observable.FromEventPattern(
                             ev => editAction.Clicked += ev,
                             ev => editAction.Clicked -= ev
                        )
                        .Subscribe(x =>
                        {
                            OnEdit(x.Sender, x.EventArgs as EventArgs);
                        })
                        .DisposeWith(disposables);

                        Observable.FromEventPattern<SelectedItemChangedEventArgs>(
                             ev => listView.ItemSelected += ev,
                             ev => listView.ItemSelected -= ev
                        )
                        .Subscribe(x =>
                        {
                            OnSelection(x.Sender, x.EventArgs);
                        })
                        .DisposeWith(disposables);

                        Observable.FromEventPattern<ItemTappedEventArgs>(
                             ev => listView.ItemTapped += ev,
                             ev => listView.ItemTapped -= ev
                        )
                        .Subscribe(x =>
                        {
                            OnTap(x.Sender, x.EventArgs);
                        })
                        .DisposeWith(disposables);

                        this
                            .OneWayBind(ViewModel, vm => vm.Title, x => x.Title)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.listView.RowHeight, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.Items, x => x.listView.ItemsSource)
                            .DisposeWith(disposables);
                    });
            baseLayout.Children.Add(listView);
            Content = baseLayout;
        }
        void OnTap(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Item Tapped", e.Item.ToString(), "Ok");
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
            //comment out if you want to keep selections
            ListView lst = (ListView)sender;
            lst.SelectedItem = null;
        }
        void OnView(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            //Do something here... e.g. Navigation.pushAsync(new specialPage(item.commandParameter));
            //page.DisplayAlert("More Context Action", item.CommandParameter + " more context action", 	"OK");
        }
        void OnEdit(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            //do something here like send user to edit page
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            //GoogleAnalytics.Current.Tracker.SendView("OrganizationPage");
        }
    }
}