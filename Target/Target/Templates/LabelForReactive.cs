using Autofac;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Target.Interfaces;
using Target.Models;
using Xamarin.Forms;

namespace Target.Templates
{
    public class LabelForReactive : ContentViewBase<BaseListItem>
    {
        Label lbl;
        public LabelForReactive()
        {
            var defaultsFactory = App.Container.Resolve<IDefaultsFactory>();
            ViewModel = (BaseListItem)this.BindingContext;
            lbl = new Label()
            {
                TextColor = defaultsFactory.GetSideMenuTextColor(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.FontColor, x => x.lbl.TextColor)
                            .DisposeWith(disposables);
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