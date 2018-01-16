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
        private bool isShowErrorsOnState;

        string fontSliderLabel;
        public string FontSliderLabel
        {
            get { return fontSliderLabel; }
            set { this.RaiseAndSetIfChanged(ref fontSliderLabel, value); }
        }

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
            Title = "Settings";
            Greeting = "Settings Page";
            
            var fireandforget = Task.Run(async () => await InitializeSettings());
            this.FontSliderChanged = ReactiveCommand.CreateFromTask(SetFontSize);
            this.IsManualFontOnClicked = ReactiveCommand.CreateFromTask(SetManualFont);
            this.showConnectionErrorsCommand = ReactiveCommand.CreateFromTask(SetShowConnectionErrors);

            this.WhenActivated(
            registerDisposable =>
            {
                registerDisposable(
                    this.WhenAnyValue(x => x.FontSize)
                    .Select(x => Unit.Default)
                    .InvokeCommand(IsManualFontOnClicked)
                    );
            });

        }
        private async Task SetManualFont()
        {
            var rounded = Math.Round((double)FontSize);
            FontSliderLabel = $"Custom Font Size is {rounded}";
            // tell the side menu to update
            MessagingCenter.Send<ISettingsViewModel>(this, "mSettingsFontChanged");
            var setting = _settingsFactory.GetSettings();
            setting.IsManualFont = IsManualFontOn;
            if (!IsManualFontOn) FontSize = defaultsFactory.GetFontSize();
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
            isShowErrorsOnState = ShowConnectionErrors;
        }
    }
}
