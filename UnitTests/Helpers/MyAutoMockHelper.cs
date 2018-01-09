﻿using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Target.Factories;
using Target.Interfaces;
using Target.Models;
using Target.Services;
using UnitTests.Mock.MockRepositories;

namespace UnitTests.Helpers
{
    public class MyAutoMockHelper
    {
        private SettingsFactory settingsFactory;
        private readonly int  _defaultFontSize;
        private readonly bool _defaultIsManualFont;
        private readonly bool _defaultShowConnectionErrors;
        private readonly int _defaultFontSizeMax;
        private IDefaultsFactory defaultsFactory;
        public MyAutoMockHelper()
        {
            defaultsFactory = new DefaultsFactory();
            _defaultFontSize = defaultsFactory.GetFontSize();
            _defaultIsManualFont = defaultsFactory.GetIsManualFont();
            _defaultShowConnectionErrors = defaultsFactory.GetShowConnectionErrors();
            _defaultFontSizeMax = defaultsFactory.GetFontSizeMax();
            
        }

        public void SetupMockForViewModels(AutoMock mock)
        {
            settingsFactory = new SettingsFactory(defaultsFactory);
            var sqlLiteRepository = new MockSQLiteRepository(defaultsFactory);
            var settingsService = new SettingsService(sqlLiteRepository, settingsFactory);
            mock.Provide<ISettingsService>(settingsService);
            mock.Provide<ISettingsFactory>(settingsFactory);
            mock.Provide<IDefaultsFactory>(defaultsFactory);
            // the following will allow autofaq to automatically inject IPlatformStuffService
            // It will also change the normal operation of GetBaseUrl()
            // since that single method is all we are worried about, no point in creating a 
            // custom mock service.
            mock.Mock<IPlatformStuffService>().Setup(x => x.GetBaseUrl()).Returns("some base url");

        }
        public int GetDefaultFontSize()
        {
            return _defaultFontSize;
        }
        public bool GetDefaultIsManualFont()
        {
            return _defaultIsManualFont;
        }
        public bool GetDefaultShowConnectionErrors()
        {
            return _defaultShowConnectionErrors;
        }
        public int GetDefaultFontSizeMax()
        {
            return _defaultFontSizeMax;
        }
        public SettingsFactory GetSettingsFactory()
        {
            return settingsFactory;
        }
        
    }
}
