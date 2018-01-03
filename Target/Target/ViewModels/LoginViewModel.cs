using ReactiveUI;
using Target.Interfaces;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Target.ViewModels
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {

        private readonly ReactiveCommand<Unit, Unit> loginCommand;
        public LoginViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            :base(settingsService, settingsFactory )
        {
            Greeting = "Welcome to " + Constants.AppName + "!";
            var canLogin = Observable.Return<bool>(true); // you could do some logic here instead
            this.loginCommand = ReactiveCommand.CreateFromObservable(
                this.LoginAsync,
                canLogin);
        }
        public ReactiveCommand<Unit, Unit> LoginCommand => this.loginCommand;
        private IObservable<Unit> LoginAsync() =>
            Observable
                 // this allows the login button to fail/succeed randomly
                .Return(new Random().Next(0, 2) == 1)
                .Do(
                    success =>
                    {
                        MessagingCenter.Send<ILoginViewModel, bool>(this, "LoginStatus", success);
                    }
                )
                .Select(_ => Unit.Default);
    }
}
