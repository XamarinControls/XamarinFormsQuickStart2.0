using Autofac;
using Plugin.Toasts;
using ReactiveUI;
using Target.Interfaces;
using Target.ViewModels;
using Xamarin.Forms;
using Target.Templates;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterListPage : ContentPageBase<MasterListViewModel>, IMasterListPage
    {
        private ListView listView;

        public ListView ListView { get { return listView; } }
       

        public MasterListPage()
        {
            InitializeComponent();
            ViewModel = (MasterListViewModel)App.Container.Resolve<IMasterListViewModel>();
            //this.BindingContext = vm;
            Title = Constants.AppName;

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
                BackgroundColor = Color.FromHex("#313e4b"),
                ItemTemplate = dtemplate
            };
            //listView.SetBinding(ListView.ItemsSourceProperty, new Binding("Items"));
            

            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.listView.RowHeight, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.Items, x => x.listView.ItemsSource)
                            .DisposeWith(disposables);
                            
                    });
            Content = listView;
        }
        
    }
}