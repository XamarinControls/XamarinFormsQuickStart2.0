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
        bool isManualFontOn;
        public bool IsManualFontOn
        {
            get { return isManualFontOn; }
            set { this.RaiseAndSetIfChanged(ref isManualFontOn, value); }
        }

        bool isSwitchOn;
        public bool IsSwitchOn
        {
            get { return isSwitchOn; }
            set { this.RaiseAndSetIfChanged(ref isSwitchOn, value); }
        }
        

        private bool showConnectionErrors;
        public bool ShowConnectionErrors
        {
            get { return showConnectionErrors; }
            set { this.RaiseAndSetIfChanged(ref showConnectionErrors, value); }
        }

        private readonly ReactiveCommand showConnectionErrorsCommand;
        public ReactiveCommand ShowConnectionErrorsCommand => this.showConnectionErrorsCommand;

        private readonly ReactiveCommand isManualFontOnClicked;
        public ReactiveCommand IsManualFontOnClicked => this.isManualFontOnClicked;
        private readonly ReactiveCommand fontSliderChanged;
        public ReactiveCommand FontSliderChanged => this.fontSliderChanged;
        public SettingsViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "Settings Page";
            
            var fireandforget = Task.Run(async () => await InitializeSettings());
            this.fontSliderChanged = ReactiveCommand.CreateFromTask(async _ =>  await SetFontSize());
            this.isManualFontOnClicked = ReactiveCommand.CreateFromTask(async _ =>  await SetManualFont());
            this.showConnectionErrorsCommand = ReactiveCommand.CreateFromTask(async _ =>  await SetShowConnectionErrors());

        }
        private async Task SetManualFont()
        {
            var setting = _settingsFactory.GetSettings();
            setting.IsManualFont = IsManualFontOn;
            if (!IsManualFontOn) FontSize = 16;
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
            setting.ShowConnectionErrors = ShowConnectionErrors;
            var settings = await _settingsService.CreateSetting(setting);
        }
        private async Task InitializeSettings()
        {
            var settings = await _settingsService.GetSettings();
            IsManualFontOn = settings.IsManualFont;
            ShowConnectionErrors = settings.ShowConnectionErrors;
        }
    }
}
