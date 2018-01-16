using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Target.Factories;
using Target.Interfaces;
using Target.Models;
using Target.Services;
using UnitTests.Mock.MockRepositories;
using Xunit;

namespace UnitTests.Helpers
{
    public class MyAutoMockHelper
    {
        private SettingsFactory settingsFactory;


        private IDefaultsFactory defaultsFactory;
        public MyAutoMockHelper()
        {
            defaultsFactory = new DefaultsFactory();

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

        public void RunBaseViewModelTests(IBaseViewModel sut)
        {
            // Assert   
            Assert.False(string.IsNullOrEmpty(sut.Title), "You didn't set a title");
            Assert.False(string.IsNullOrWhiteSpace(sut.Greeting), "You didn't set a greeting");
            Assert.Equal(defaultsFactory.GetFontSize(), sut.FontSize);
        }
        public IDefaultsFactory GetDefaultsFactory()
        {
            return defaultsFactory;
        }
        public SettingsFactory GetSettingsFactory()
        {
            return settingsFactory;
        }

    }
}
