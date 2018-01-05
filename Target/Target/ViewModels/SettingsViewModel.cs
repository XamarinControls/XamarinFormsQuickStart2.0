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
        private bool _isManualFontOnForBothProductionAndTesting;

        bool isManualFontOn;
        public bool IsManualFontOn
        {
            get { return isManualFontOn; }
            set { this.RaiseAndSetIfChanged(ref isManualFontOn, value); }
        }

        //bool isSwitchOn;
        //public bool IsSwitchOn
        //{
        //    get { return isSwitchOn; }
        //    set { this.RaiseAndSetIfChanged(ref isSwitchOn, value); }
        //}
        

        private bool showConnectionErrors;
        public bool ShowConnectionErrors
        {
            get { return showConnectionErrors; }
            set { this.RaiseAndSetIfChanged(ref showConnectionErrors, value); }
        }

       
        private readonly ReactiveCommand showConnectionErrorsCommand;
        public ReactiveCommand ShowConnectionErrorsCommand => this.showConnectionErrorsCommand;

        //private readonly ReactiveCommand isManualFontOnClicked;
        //public ReactiveCommand IsManualFontOnClicked => this.isManualFontOnClicked;

        // using the following command setup this way allows 2-way binding but doesn't allow you to invoke it from the view
        public ReactiveCommand IsManualFontOnClicked
        {
            get;
        }


        private readonly ReactiveCommand fontSliderChanged;
        public ReactiveCommand FontSliderChanged => this.fontSliderChanged;
        public SettingsViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory, IDefaultsFactory defaultsFactory)
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            Greeting = "Settings Page";
            
            var fireandforget = Task.Run(async () => await InitializeSettings());
            this.fontSliderChanged = ReactiveCommand.CreateFromTask(async _ =>  await SetFontSize());
            this.IsManualFontOnClicked = ReactiveCommand.CreateFromTask(SetManualFont);
            this.showConnectionErrorsCommand = ReactiveCommand.CreateFromTask(SetShowConnectionErrors);

        }
        private async Task SetManualFont()
        {
            var setting = _settingsFactory.GetSettings();
            // I had to use the following _isManualFontOnForBothProductionAndTesting so that
            // the reactive command IsManualFontOnClicked that kicks off this function
            // would actually change the value of "FontSize".  It would be easier to use IsManualFontOn
            // that the property isManualFont.IsToggled in the view changes but this isn't available in a unit test
            _isManualFontOnForBothProductionAndTesting = !_isManualFontOnForBothProductionAndTesting;
            setting.IsManualFont = IsManualFontOn;
            if (!_isManualFontOnForBothProductionAndTesting) FontSize = defaultsFactory.GetFontSize();
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
            isManualFontOn = settings.IsManualFont;
            ShowConnectionErrors = settings.ShowConnectionErrors;
            // need to reverse this so it makes more sence later when I test it
            _isManualFontOnForBothProductionAndTesting = IsManualFontOn;
        }
    }
}
