using Autofac.Extras.Moq;
using Target.ViewModels;
using Xunit;
using UnitTests.Helpers;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using Target.Interfaces;
using Target.Factories;
using System;
using System.Threading.Tasks;

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
                    sut.IsManualFontOnClicked.Execute().FirstAsync();
                }
                // set the font size to something other than the default
                sut.FontSize = _myHelper.GetDefaultFontSize() + 1;

                // Assert  
                // At this point the IsManualFont switch should be turned on
                // Also the value of the font size should be 1 greater than the default
                // when the below command is invoked, it should automatically turn off the switch 
                // and set the value of the FontSize to the default causing an INotifyPropertyChanged event                
                Assert.PropertyChanged(sut, "FontSize", () => sut.IsManualFontOnClicked.Execute().FirstAsync());
                // The value of FontSize should now be back to the default
                Assert.Equal(sut.FontSize, _myHelper.GetDefaultFontSize());
            }
        }

        [Fact]
        public async Task WhenFontSliderChanged_Occurs_ShouldSetFontSize()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();

                // Act
                sut.FontSize = 30;
                // couple other ways to execute commands shown below
                // sut.FontSliderChanged.Execute().Subscribe<Unit>();
                // Observable.Return(Unit.Default).InvokeCommand(sut.IsManualFontOnClicked);
                await sut.FontSliderChanged.Execute().FirstAsync();
                var settings = _myHelper.GetSettingsFactory();

                // Assert  
                Assert.Equal(30, settings.GetSettings().FontSize);
            }
        }
        [Fact]
        public async Task WhenShowConnectionErrors_IsToggledOn_ShouldSetConnectionErrorsToTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();

                // Act
                if (!_myHelper.GetDefaultShowConnectionErrors())
                {
                    // turn it on
                    await sut.ShowConnectionErrorsCommand.Execute().FirstAsync();
                } 
                else
                {
                    await sut.ShowConnectionErrorsCommand.Execute().FirstAsync();
                    await sut.ShowConnectionErrorsCommand.Execute().FirstAsync();
                }

                var settings = _myHelper.GetSettingsFactory();

                // Assert  
                Assert.True(settings.GetSettings().ShowConnectionErrors);

            }
        }

    }
}

