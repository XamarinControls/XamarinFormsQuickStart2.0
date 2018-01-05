using Autofac.Extras.Moq;
using Target.ViewModels;
using Xunit;
using UnitTests.Helpers;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using Target.Interfaces;
using Target.Factories;

namespace UnitTests.ViewModels
{

    public class SettingsViewModelTests
    {
        MyAutoMockHelper _myHelper;
        public SettingsViewModelTests()
        {
            _myHelper = new MyAutoMockHelper();
        }

        [Fact]
        public void WhenInitialized_ShouldSetDefaults()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();

                var defaultIsManualFontOn = _myHelper.GetDefaultIsManualFont();
                var defaultShowConnectionErrors = _myHelper.GetDefaultShowConnectionErrors();

                // Act
                var greeting = sut.Greeting;
                var fontSize = sut.FontSize;
                var isManualFontOn = sut.IsManualFontOn;
                var showConnectionErrors = sut.ShowConnectionErrors;

                // Assert    
                Assert.Equal("Settings Page", greeting);
                Assert.Equal(_myHelper.GetDefaultFontSize(), fontSize);
                if (defaultIsManualFontOn)
                {
                    Assert.True(isManualFontOn, "IsManualFont had wrong initial value");
                }
                else
                {
                    Assert.False(isManualFontOn, "IsManualFont had wrong initial value");
                }
                if (defaultShowConnectionErrors)
                {
                    Assert.True(showConnectionErrors, "ShowConnectionErrors had wrong initial value");
                }
                else
                {
                    Assert.False(showConnectionErrors, "ShowConnectionErrors had wrong initial value");
                }
            }
        }


        [Fact]
        public void WhenManualFont_IsClickedOff_ShouldSetFontSizeToDefault()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();

                // Act
                // if IsManualFont is already turned off
                if (!_myHelper.GetDefaultIsManualFont())
                {
                    // turn it on
                    Observable.Return(Unit.Default).InvokeCommand(sut.IsManualFontOnClicked);
                }
                // set the font size to something other than the default
                var fontsize = _myHelper.GetDefaultFontSize() + 1;
                sut.FontSize = fontsize;
                var x = sut.IsManualFontOn;
                // Assert  
                // At this point the IsManualFont switch should be turned on
                // Also the value of the font size should be 1 greater than the default
                // when the below command is invoked, it should automatically turn off the switch 
                // and set the value of the FontSize to the default causing an INotifyPropertyChanged event                
                Assert.PropertyChanged(sut, "FontSize", () => Observable.Return(Unit.Default).InvokeCommand(sut.IsManualFontOnClicked));
                // The value of FontSize should now be back to the default
                Assert.Equal(sut.FontSize, _myHelper.GetDefaultFontSize());
            }
        }

    }
}

