using Target.Interfaces;
using ReactiveUI;
using System.Threading.Tasks;

namespace Target.ViewModels
{

    public class BaseViewModel : ReactiveObject, ISupportsActivation, IBaseViewModel
    {
        public ISettingsFactory _settingsFactory;
        public ISettingsService _settingsService;
        public readonly IDefaultsFactory defaultsFactory;

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
            var fireandforget = Task.Run(async () => await InitializeSettings()); 
        }
        private async Task InitializeSettings()
        {
            var settings = await _settingsService.GetSettings();
            FontSize = settings.FontSize;
        }
    }
}
