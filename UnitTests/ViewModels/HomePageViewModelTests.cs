using Autofac.Extras.Moq;
using Target.Interfaces;
using Target.ViewModels;
using Xunit;
using UnitTests.Helpers;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace UnitTests.ViewModels
{

    public class HomePageViewModelTests : BaseViewModel
    {
        
        [Fact]
        public void WhenInitialized_ShouldSetDefaults()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<HomePageViewModel>();                

                // Act

                // Assert    
                Assert.False(string.IsNullOrWhiteSpace(sut.Greeting), "You didn't set a greeting");
                Assert.Equal(defaultsFactory.GetFontSize(), sut.FontSize);
                Assert.False(string.IsNullOrWhiteSpace(sut.SourceImg), "You didn't set a SVG source Image");
            }
        }        

    }
}

