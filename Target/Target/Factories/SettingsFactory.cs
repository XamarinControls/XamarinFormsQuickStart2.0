﻿using Target.Interfaces;
using Target.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Factories
{
    // use this class whenever you want to read current settings.
    // use the SettingsService if you need to change she settings 
    public class SettingsFactory : SQLiteItem, ISettingsFactory
    {
        private Settings _settings;
        private IDefaultsFactory defaultsFactory;
        public SettingsFactory(IDefaultsFactory defaultsFactory)
        {
            this.defaultsFactory = defaultsFactory;
            if (_settings == null)
            {
                _settings = new Settings() { };
                SetDefaults();
            }
        }
        public override string KeyName => "settings";
        public Settings GetSettings()
        {
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
