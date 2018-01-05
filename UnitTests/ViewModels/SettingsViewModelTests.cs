using Autofac.Extras.Moq;
using Target.ViewModels;
using Xunit;
using UnitTests.Helpers;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using Target.Interfaces;

namespace UnitTests.ViewModels
{

    public class SettingsViewModelTests : ReactiveObject
    {
        MyAutoMockHelper _autoHelper;
        public SettingsViewModelTests()
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
                var sut = mock.Create<SettingsViewModel>();

                // Act
                var actual = sut.Greeting;

                // Assert    
                Assert.Equal("Settings Page", actual);
            }
        }

        [Fact]
        public void WhenManualFont_IsClicked_ShouldSetFontSize()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _autoHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();

                // Act
                //var actual = sut.FontSize;

                // Assert     
                Assert.PropertyChanged(sut, "FontSize", () => Observable.Return(Unit.Default).InvokeCommand(sut.IsManualFontOnClicked));
            }
        }

    }
}

