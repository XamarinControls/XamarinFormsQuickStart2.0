using Autofac;
using Target.Interfaces;
using Target.Models;
using Target.Templates;
using Target.ViewModels;
using Plugin.Toasts;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using Xamarin.Forms;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Splat;
using Target.Converters;
using Plugin.GoogleAnalytics;
using Target.Renderer;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPageBase<SettingsViewModel>, ISettingsPage
    {        
        Switch showConnectionErrors;
        Label isManualFontLabel;
        Label fontSliderLabel;
        Label showConnectionErrorsLabel;
        Slider fontSlider;
        Grid fontSliderGrid;
        Switch isManualFont;
        Settings settings;
        IBindingTypeConverter bindingPaddingTopBottomConverter;
        Grid showConnectionErrorsGrid;
        Grid isManualFontGrid;
        ViewCell viewCellManualFont;
        ViewCell viewCellSlider;
        ViewCell viewCellShowErrors;
        Label sectionLabelDiagnostics;
        Label sectionLabelInterface;

        public SettingsPage()
        {
            
            InitializeComponent();
            bindingPaddingTopBottomConverter = (IBindingTypeConverter)App.Container.Resolve<IPaddingTopBottomConverter>();
            settings = App.Container.Resolve<ISettingsFactory>().GetSettings();
            ViewModel = (SettingsViewModel)App.Container.Resolve<ISettingsViewModel>();
            
            var fontSize = settings.FontSize;

            this.Padding = new Thickness(10, getDevicePadding(), 10, 5);

            isManualFontLabel = new Label()
            {
                Text = "Enable Manual Font",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };


            isManualFont = new Switch()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            

            isManualFontGrid = new TwoValueHorizontalGrid().Create(0d, 80d);
            isManualFontGrid.Children.Add(isManualFontLabel, 0, 0);
            isManualFontGrid.Children.Add(isManualFont, 1, 0);

            fontSliderLabel = new Label
            {
                Text = $"Custom Font Size is {fontSize}",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            

            fontSlider = new Slider
            {
                Maximum = Constants.FontSizeMax,
                Minimum = 12,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };


            fontSliderGrid = new TwoValueHorizontalGrid().Create();
            fontSliderGrid.Children.Add(fontSliderLabel, 0, 0);
            fontSliderGrid.Children.Add(fontSlider, 1, 0);
            showConnectionErrorsLabel = new Label()
            {
                Text = "Show Connection Errors",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start
            };
            
            showConnectionErrors = new Switch()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End
            };
            
            showConnectionErrorsGrid = new TwoValueHorizontalGrid().Create();
            showConnectionErrorsGrid.Children.Add(showConnectionErrorsLabel, 0, 0);
            showConnectionErrorsGrid.Children.Add(showConnectionErrors, 1, 0);

            viewCellManualFont = new ViewCell()
            {
                View = isManualFontGrid
            };
            viewCellSlider = new ViewCell()
            {
                View = fontSliderGrid
            };
            viewCellShowErrors = new ViewCell()
            {
                View = showConnectionErrorsGrid
            };
            
            var stacklayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10, 10, 10, 10),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            var seperator1 = new StackLayout
            {
                HeightRequest = 1,
                BackgroundColor = Color.Gray,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            sectionLabelInterface = new Label()
            {
                Text = "Interface",
                TextColor = Color.Gray
            };
            var seperator2 = new StackLayout
            {
                HeightRequest = 1,
                BackgroundColor = Color.Gray,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            sectionLabelDiagnostics = new Label()
            {
                Text = "Diagnostics",
                TextColor = Color.Gray
            };
            stacklayout.Children.Add(sectionLabelInterface);
            stacklayout.Children.Add(seperator1);
            stacklayout.Children.Add(isManualFontGrid);
            stacklayout.Children.Add(fontSliderGrid);
            stacklayout.Children.Add(sectionLabelDiagnostics);
            stacklayout.Children.Add(seperator2);
            stacklayout.Children.Add(showConnectionErrorsGrid);

            this.Content = stacklayout;

            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.isManualFontGrid.Margin, vmToViewConverterOverride: bindingPaddingTopBottomConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.fontSliderGrid.Margin, vmToViewConverterOverride: bindingPaddingTopBottomConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.showConnectionErrorsGrid.Margin, vmToViewConverterOverride: bindingPaddingTopBottomConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.isManualFontLabel.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.sectionLabelInterface.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.sectionLabelDiagnostics.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this.Bind(ViewModel, vm => vm.FontSize, x => x.fontSlider.Value, vmToViewConverterOverride: bindingIntToDoubleConverter, viewToVMConverterOverride: bindingDoubleToIntConverter)
                            .DisposeWith(disposables);
                        this.fontSlider.Events().ValueChanged
                            .Throttle(TimeSpan.FromMilliseconds(150), RxApp.MainThreadScheduler)
                            .Do((x) =>
                            {
                                var rounded = Math.Round(x.NewValue);
                                fontSliderLabel.Text = $"Custom Font Size is {rounded}";
                                MessagingCenter.Send<ISettingsPage>(this, "mSettingsFontChanged");
                            })
                            .Select(x => Unit.Default)
                            .InvokeCommand(ViewModel.FontSliderChanged)
                            .DisposeWith(disposables);
                        this.isManualFont.Events().Toggled
                            .Select(x => Unit.Default)
                            .InvokeCommand(ViewModel.IsManualFontOnClicked)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.isManualFontLabel.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .Bind(this.ViewModel, x => x.IsManualFontOn, x => x.isManualFont.IsToggled)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.fontSliderLabel.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.IsManualFontOn, x => x.fontSlider.IsEnabled)
                            .DisposeWith(disposables);                        
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.showConnectionErrorsLabel.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .Bind(this.ViewModel, x => x.ShowConnectionErrors, x => x.showConnectionErrors.IsToggled)
                            .DisposeWith(disposables);
                        this.BindCommand(
                            this.ViewModel,
                            x => x.ShowConnectionErrorsCommand,
                            x => x.showConnectionErrors, nameof(showConnectionErrors.Toggled))
                            .DisposeWith(disposables);
                    });
                                    
        }
        double getDevicePadding()
        {

            double topPadding;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    topPadding = 20;
                    break;
                default:
                    topPadding = 0;
                    break;
            }

            return topPadding;
        }
        
        
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            GoogleAnalytics.Current.Tracker.SendView("SettingsPage");
        }

        protected override void OnDisappearing()
        {
            // Not guaranteed to occur, Cannot be depended apon.            
            base.OnDisappearing();
            
        }

    }
}