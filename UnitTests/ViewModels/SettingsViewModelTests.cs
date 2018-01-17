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

    public class SettingsViewModelTests : BaseViewModel
    {

        [Fact]
        public void WhenInitialized_ShouldSetDefaults()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();

                var defaultIsManualFontOn = defaultsFactory.GetIsManualFont();
                var defaultShowConnectionErrors = defaultsFactory.GetShowConnectionErrors();

                // Act
                var isManualFontOn = sut.IsManualFontOn;
                var showConnectionErrors = sut.ShowConnectionErrors;

                // Assert    
                _myHelper.RunBaseViewModelTests(sut);
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
        public async Task WhenManualFont_IsClickedOff_ShouldSetFontSizeToDefault()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();
                
                // Act
                // if IsManualFont is on, turn it off
                if (defaultsFactory.GetIsManualFont())
                {
                    // turn it on                    
                    sut.IsManualFontOn = false;
                }
                // set the font size to something other than the default
                sut.FontSize = defaultsFactory.GetFontSize() + 1;

                // Assert  
                // At this point the IsManualFont switch should be turned on
                // Also the value of the font size should be 1 greater than the default
                // when the below command is invoked, it should automatically turn off the switch 
                // and set the value of the FontSize to the default causing an INotifyPropertyChanged event                
                //await Assert.PropertyChangedAsync(sut, "FontSize", async () => await sut.IsManualFontOnClicked.Execute().FirstAsync());
                await Assert.PropertyChangedAsync(sut, "FontSize", async () => await ChangeManualFont(sut));
                // The value of FontSize should now be back to the default
                Assert.Equal(sut.FontSize, defaultsFactory.GetFontSize());
            }
        }
        private async Task ChangeManualFont(SettingsViewModel sut)
        {
            sut.IsManualFontOn = true;
            await Task.Delay(TimeSpan.FromMilliseconds(0));
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
                var settings = _myHelper.GetSettingsFactory();
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
                // Assert  
                Assert.Equal(30, settings.GetSettings().FontSize);
            }
        }
        [Fact]
        public void WhenShowConnectionErrors_IsToggledOn_ShouldSetConnectionErrorsToTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<SettingsViewModel>();

                // Act
                if (!defaultsFactory.GetShowConnectionErrors())
                {
                    // turn it on
                    sut.ShowConnectionErrors = true;
                    //await sut.ShowConnectionErrorsCommand.Execute().FirstAsync();
                } 

                var settings = _myHelper.GetSettingsFactory();

                // Assert  
                Assert.True(settings.GetSettings().ShowConnectionErrors);

            }
        }

    }
}

