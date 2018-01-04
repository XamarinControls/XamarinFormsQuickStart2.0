using System;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Target.Interfaces;
using Target.Models;
using Target.Services;
using Target.ViewModels;
using UnitTests.MockFactories;
using UnitTests.MockServices;
using System.Reactive.Linq;
using Xunit;
using Target.Factories;
using UnitTests.Helpers;

namespace CoreTests.ViewModels
{
    
    public class LoginViewModelTests
    {
        MyAutoMockHelper _autoHelper;
        public LoginViewModelTests()
        {
            _autoHelper = new MyAutoMockHelper();
        }

        [Fact]
        public void WhenInitialized_ShouldSetGreeting()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _autoHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<LoginViewModel>();

                // Act
                var actual = sut.Greeting;

                // Assert    
                Assert.Equal("Welcome to MYAPP!", actual);
            }
        }

        [Fact]
        public void WhenInitialized_ShouldSetFontSize()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _autoHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<LoginViewModel>();

                // Act
                //await Task.Delay(TimeSpan.FromMilliseconds(10));
                var actual = sut.FontSize;

                // Assert     
                mock.Mock<ISettingsService>().Verify(x => x.GetSettings());
                Assert.Equal(16, actual);
            }
        }
        
    }
}

