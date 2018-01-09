using Target.Interfaces;
using Plugin.Toasts;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Splat;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive;

namespace Target.ViewModels
{
    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private bool isManualFontOnState;
        private bool isShowErrorsOnState;

        bool isManualFontOn;
        public bool IsManualFontOn
        {
            get { return isManualFontOn; }
            set { this.RaiseAndSetIfChanged(ref isManualFontOn, value); }
        }

        private bool showConnectionErrors;
        public bool ShowConnectionErrors
        {
            get { return showConnectionErrors; }
            set { this.RaiseAndSetIfChanged(ref showConnectionErrors, value); }
        }

       
        private readonly ReactiveCommand<Unit, Unit> showConnectionErrorsCommand;
        public ReactiveCommand<Unit, Unit> ShowConnectionErrorsCommand => this.showConnectionErrorsCommand;
        
        public ReactiveCommand<Unit, Unit> IsManualFontOnClicked
        {
            get;
        }
        public ReactiveCommand<Unit, Unit> FontSliderChanged { get; }

        //private readonly ReactiveCommand fontSliderChanged;
        //public ReactiveCommand FontSliderChanged => this.fontSliderChanged;
        public SettingsViewModel(
            ISettingsService settingsService,
            ISettingsFactory settingsFactory,
            IDefaultsFactory defaultsFactory,
            IPlatformStuffService platformStuffService
            )
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            Greeting = "Settings Page";
            
            var fireandforget = Task.Run(async () => await InitializeSettings());
            this.FontSliderChanged = ReactiveCommand.CreateFromTask(SetFontSize);
            this.IsManualFontOnClicked = ReactiveCommand.CreateFromTask(SetManualFont);
            this.showConnectionErrorsCommand = ReactiveCommand.CreateFromTask(SetShowConnectionErrors);

        }
        private async Task SetManualFont()
        {
            var setting = _settingsFactory.GetSettings();
            // I need this variable to maintain state of the switch for testing,
            // otherwise only the view maintains the state of the switch.
            isManualFontOnState = !isManualFontOnState;
            setting.IsManualFont = IsManualFontOn;
            if (!isManualFontOnState) FontSize = defaultsFactory.GetFontSize();
            setting.FontSize = FontSize;
            var settings = await _settingsService.CreateSetting(setting);            
        }
        private async Task SetFontSize()
        {
            var setting = _settingsFactory.GetSettings();
            var mydouble = (double)FontSize;
            var rounded = (int)Math.Round(mydouble);
            setting.FontSize = rounded;
            var settings = await _settingsService.CreateSetting(setting);
        }
        private async Task SetShowConnectionErrors()
        {                        
            var setting = _settingsFactory.GetSettings();
            isShowErrorsOnState = !isShowErrorsOnState;
            setting.ShowConnectionErrors = isShowErrorsOnState;
            var settings = await _settingsService.CreateSetting(setting);
            
        }
        private async Task InitializeSettings()
        {
            var settings = await _settingsService.GetSettings();
            isManualFontOn = settings.IsManualFont;
            ShowConnectionErrors = settings.ShowConnectionErrors;
            isManualFontOnState = IsManualFontOn;
            isShowErrorsOnState = ShowConnectionErrors;
        }
    }
}
