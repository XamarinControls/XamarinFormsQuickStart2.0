using System;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Target.Interfaces;
using Target.Models;
using Target.Services;
using Target.ViewModels;
using UnitTests.MockFactories;
using UnitTests.MockServices;
using System.Reactive.Linq;

namespace Tests.ViewModels
{
    [TestClass]
    public class LoginViewModelTests
    {
        //private LoginViewModel loginViewModel;

        //public 

        [TestMethod]
        public void SettingsService_WhenInitialized_ShouldSetGreeting()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                var sut = mock.Create<LoginViewModel>();

                // Act
                var actual = sut.Greeting;

                // Assert               
                Assert.AreEqual("Welcome to MYAPP!", actual);
            }
        }

        [TestMethod]
        public async Task SettingsService_WhenInitialized_ShouldSetFontSize()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
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
                var sut = mock.Create<LoginViewModel>();

                // Act
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                var actual = sut.FontSize;

                // Assert     
                mock.Mock<ISettingsService>().Verify(x => x.GetSettings());
                Assert.AreEqual(16, actual);
            }
        }
    }
}

