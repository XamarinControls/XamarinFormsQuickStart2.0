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
        private IDefaultsFactory defaultsFactory;
        public SettingsFactory(IDefaultsFactory defaultsFactory)
        {
            this.defaultsFactory = defaultsFactory;
        }
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
            _settings = _settings ?? new Settings() { };
            _settings.IsManualFont = defaultsFactory.GetIsManualFont();
            _settings.FontSize = defaultsFactory.GetFontSize();
            _settings.ShowConnectionErrors = defaultsFactory.GetShowConnectionErrors();
            _settings.AgreedToTermsDate = "";
        }
        public void SaveSettings(Settings settings)
        {
            _settings = _settings ?? new Settings() { };
            _settings = settings;
        }
    }
}
