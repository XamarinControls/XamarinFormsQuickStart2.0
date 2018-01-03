using Autofac;
using FFImageLoading.Svg.Forms;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using Target.Converters;
using Target.Interfaces;
using Target.Models;
using Xamarin.Forms;

namespace Target.Templates
{
    public class ImageForReactive : ContentViewBase<MasterPageItem>
    {
        SvgCachedImage ffimg;
        SvgImageSourceConverterForReactive imgConverter;
        public ImageForReactive()
        {
            imgConverter = (SvgImageSourceConverterForReactive)App.Container.Resolve<ISvgImageSourceConverterForReactive>();
            ffimg = new SvgCachedImage()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };
            this
                .WhenActivated(
                    disposables =>
                    {
                        this.OneWayBind(ViewModel, vm => vm.IconSource, view => view.ffimg.Source, vmToViewConverterOverride: imgConverter)
                            .DisposeWith(disposables);
                        this.OneWayBind(ViewModel, vm => vm.FontSize, view => view.ffimg.HeightRequest, x => GetSquaredImageSize(x))
                            .DisposeWith(disposables);
                        //this.OneWayBind(ViewModel, vm => vm.FontSize, view => view.ffimg.WidthRequest, x => GetSquaredImageSize(x))
                        //    .DisposeWith(disposables);
                    });
            Content = ffimg;
        }
    }
}