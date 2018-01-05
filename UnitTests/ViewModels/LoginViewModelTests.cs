using Autofac.Extras.Moq;
using Target.Interfaces;
using Target.ViewModels;
using Xunit;
using UnitTests.Helpers;

namespace UnitTests.ViewModels
{

    public class LoginViewModelTests
    {
        MyAutoMockHelper _myHelper;
        public LoginViewModelTests()
        {
            _myHelper = new MyAutoMockHelper();
        }
        

        [Fact]
        public void WhenInitialized_ShouldSetGreeting()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
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
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<LoginViewModel>();

                // Act
                //await Task.Delay(TimeSpan.FromMilliseconds(10));
                var actual = sut.FontSize;

                // Assert     
                Assert.Equal(_myHelper.GetDefaultFontSize(), actual);
            }
        }
        
    }
}

