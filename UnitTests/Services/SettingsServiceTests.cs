using Autofac.Extras.Moq;
using System.Reactive;
using System.Threading.Tasks;
using Target.Factories;
using Target.Interfaces;
using Target.Models;
using Target.Services;
using UnitTests.Mock.MockRepositories;
using Xunit;

namespace UnitTests.Services
{
    public class SettingsServiceTests
    {
        private void CreateMock(AutoMock mock)
        {
            var mockSQLiteRepository = new MockSQLiteRepository();
            mock.Provide<ISQLiteRepository>(mockSQLiteRepository);
            // using the real settings factory because it's just a POCO
            var defaults = new DefaultsFactory();
            var realSettingsFactory = new SettingsFactory(defaults);
            mock.Provide<ISettingsFactory>(realSettingsFactory);
        }

        [Fact]
        public void CheckSettings_ReturnsVoidTask()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                CreateMock(mock);
                var sut = mock.Create<SettingsService>();

                // Act
                var task = sut.CheckSettings();

                // Assert     
                Assert.True(task.Wait(10), "failed to load in time");
                
            }
        }

        [Fact]
        public void GetSettings_ReturnsSettings()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                CreateMock(mock);
                var sut = mock.Create<SettingsService>();

                // Act
                var actual = sut.GetSettings().Result;

                // Assert     
                Assert.IsType<Settings>(actual);
            }
        }

        [Fact]
        public void ResetToDefaults_ReturnsUnitTask()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                CreateMock(mock);
                var sut = mock.Create<SettingsService>();                

                // Act
                var task = sut.ResetToDefaults();

                // Assert                    
                Assert.True(task.Wait(10), "failed to load in time");
                Assert.IsType<Task<Unit>>(task);
            }
        }

        [Fact]
        public void CreateSetting_ReturnsUnitTask()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                CreateMock(mock);
                var sut = mock.Create<SettingsService>();
                var setting = new Settings()
                {
                    AgreedToTermsDate = "",
                    FontSize = 16,
                    IsManualFont = false,
                    ShowConnectionErrors = false
                };

                // Act
                var task = sut.CreateSetting(setting);

                // Assert                    
                Assert.True(task.Wait(10), "failed to load in time");
                Assert.IsType<Task<Unit>>(task);
            }
        }
    }
}
