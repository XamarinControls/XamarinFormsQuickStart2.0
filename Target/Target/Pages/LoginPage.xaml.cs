﻿using Autofac;
using Target.Interfaces;
using Target.ViewModels;
using System;
using ReactiveUI;
//using Plugin.GoogleAnalytics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    // I have to disable all xml compilation or I can't hit breakpoints while degugging.  Enable for production
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPageBase<LoginViewModel>, ILoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
            ViewModel = (LoginViewModel)App.Container.Resolve<ILoginViewModel>();
            this
                .WhenActivated(
                    disposables =>
                    {
                        this
                            .OneWayBind(ViewModel, vm => vm.Title, x => x.Title)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(ViewModel, vm => vm.Greeting, x => x.loginLabel.Text)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(ViewModel, vm => vm.LoginCommand, x => x.loginButton.Command)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, vm => vm.FontSize, x => x.loginButton.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                        this
                            .OneWayBind(this.ViewModel, vm => vm.FontSize, x => x.loginLabel.FontSize, vmToViewConverterOverride: bindingIntToDoubleConverter)
                            .DisposeWith(disposables);
                    });
        }
       
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            //GoogleAnalytics.Current.Tracker.SendView("LoginPage");
        }
        
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}