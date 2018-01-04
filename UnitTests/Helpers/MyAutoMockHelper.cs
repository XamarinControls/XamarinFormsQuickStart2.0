using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Target.Factories;
using Target.Interfaces;
using Target.Models;
using UnitTests.MockFactories;

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

            var mockSettingsFactory = new MockSettingsFactory();
            mock.Provide<ISettingsFactory>(mockSettingsFactory);
            var defaultsFactory = new DefaultsFactory();
            mock.Provide<IDefaultsFactory>(defaultsFactory);

        }
    }
}
