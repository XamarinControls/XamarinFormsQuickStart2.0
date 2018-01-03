using Target.Interfaces;
using Target.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Factories
{
    public class SettingsFactory : SQLiteItem, ISettingsFactory
    {
        private Settings _settings;
        public override string KeyName => "settings";
        public Settings GetSettings()
        {
            if(_settings == null)
            {
                _settings = new Settings() { };
                SetDefaults();
            }
            return _settings;
        }
        public void SetDefaults()
        {
            _settings.IsManualFont = Constants.IsManualFont;
            _settings.FontSize = Constants.FontSize;
            _settings.ShowConnectionErrors = Constants.ShowConnectionErrors;
            _settings.AgreedToTermsDate = "";
        }
        public void SaveSettings(Settings settings)
        {
            _settings = settings;
        }
    }
}
