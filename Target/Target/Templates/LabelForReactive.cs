using Autofac;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Target.Interfaces;
using Target.Models;
using Xamarin.Forms;

namespace Target.Templates
{
    public class LabelForReactive : ContentViewBase<MasterPageItem>
    {
        Label lbl;
        public LabelForReactive()
        {
            var defaultsFactory = App.Container.Resolve<IDefaultsFactory>();
            ViewModel = (MasterPageItem)this.BindingContext;
            lbl = new Label()
            {
                TextColor = defaultsFactory.GetSideMenuTextColor(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            //lbl.SetBinding(Label.TextProperty, new Binding("Title"));
            //lbl.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));

            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.Title, x => x.lbl.Text)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, x => x.FontSize, x => x.lbl.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                    });

            Content = lbl;
        }
    }
}