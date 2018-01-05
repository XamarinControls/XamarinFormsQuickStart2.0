using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Target.Factories;
using Target.Interfaces;
using Target.Models;

namespace UnitTests.Helpers
{
    public class MyAutoMockHelper
    {
        public void SetupMockForViewModels(AutoMock mock)
        {

            var taskCompletion = new TaskCompletionSource<ISettings>();
            taskCompletion.SetResult(new Settings()
            {
                AgreedToTermsDate = "",
                FontSize = 16,
                IsManualFont = false,
                ShowConnectionErrors = false
            });

            mock.Mock<ISettingsService>().Setup(x => x.GetSettings()).Returns(taskCompletion.Task);
            var defaultsFactory = new DefaultsFactory();
            var settingsFactory = new SettingsFactory(defaultsFactory);
            mock.Provide<ISettingsFactory>(settingsFactory);            
            mock.Provide<IDefaultsFactory>(defaultsFactory);

        }
    }
}
