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

            isManualFontOn = Settings.IsManualFont;
            ShowConnectionErrors = Settings.ShowConnectionErrors;

            this.WhenActivated(
            registerDisposable =>
            {
                registerDisposable(
                        this.WhenAnyValue(x => x.ShowConnectionErrors)
                        .Do(x => SetShowConnectionErrors())
                        .SelectMany(async x => await SetSettings(Settings))
                        .Subscribe());
                registerDisposable(
                        this.WhenAnyValue(x => x.FontSize)
                        .Do(x => {
                            SetFontSize();
                        })
                        .SelectMany(async x => await SetSettings(Settings))
                        .Subscribe());
                registerDisposable(
                        this.WhenAnyValue(x => x.IsManualFontOn)
                        .Do(x => SetManualFont())
                        .SelectMany(async x => await SetSettings(Settings))
                        .Subscribe());
            });

        }
        private void SetManualFont()
        {
            var rounded = Math.Round((double)FontSize);
            FontSliderLabel = $"Custom Font Size is {rounded}";  
            Settings.IsManualFont = IsManualFontOn;
            if (!IsManualFontOn) FontSize = defaultsFactory.GetFontSize();
            Settings.FontSize = FontSize;            
            // tell the side menu to update
            MessagingCenter.Send<ISettingsViewModel>(this, "mSettingsFontChanged");
        }
        private void SetFontSize()
        {
            var rounded = (int)Math.Round((double)FontSize);
            Settings.FontSize = rounded;
            MessagingCenter.Send<ISettingsViewModel>(this, "mSettingsFontChanged");
        }
        private void SetShowConnectionErrors()
        {     
            Settings.ShowConnectionErrors = ShowConnectionErrors;
        }
       
    }
}
