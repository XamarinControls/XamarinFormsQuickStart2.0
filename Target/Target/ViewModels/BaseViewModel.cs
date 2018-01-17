using Target.Interfaces;
using ReactiveUI;
using System.Threading.Tasks;
using Target.Models;
using System.Reactive.Linq;
using System.Reactive;
using System;

namespace Target.ViewModels
{

    public class BaseViewModel : ReactiveObject, ISupportsActivation, IBaseViewModel
    {
        public ISettingsFactory _settingsFactory;
        public ISettingsService _settingsService;
        public readonly IDefaultsFactory defaultsFactory;
        public Settings Settings { get; set; }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set { this.RaiseAndSetIfChanged(ref title, value); }
        }

        private string toastMessage;
        public string ToastMessage
        {
            get
            {
                return toastMessage;
            }
            set { this.RaiseAndSetIfChanged(ref toastMessage, value); }
        }
        private string greeting;
        public string Greeting
        {
            get
            {
                return greeting;
            }
            set { this.RaiseAndSetIfChanged(ref greeting, value); }
        }
        private int fontSize;
        public int FontSize
        {
            get { return fontSize; }
            set { this.RaiseAndSetIfChanged(ref fontSize, value); }
        }

        public ViewModelActivator Activator
        {
            get {
                return viewModelActivator;
            }
        }

        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();
        public BaseViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory, IDefaultsFactory defaultsFactory)
        {
            _settingsFactory = settingsFactory;
            _settingsService = settingsService;
            this.defaultsFactory = defaultsFactory;
            Settings = _settingsFactory.GetSettings();
            FontSize = Settings.FontSize;
            this.WhenActivated(
            registerDisposable =>
            {
                registerDisposable(
                        this.WhenAnyValue(x => x.FontSize)
                        .SelectMany(async x => await _settingsService.CreateSetting(Settings))
                        .Subscribe());
            });
        }
        public async Task<Unit> SetSettings(Settings sets)
        {
           return await _settingsService.CreateSetting(sets);
        }
        
    }
}
