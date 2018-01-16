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

    public class PolicyPageViewModelTests : BaseViewModel
    {
        
        [Fact]
        public void WhenInitialized_ShouldSetDefaults()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<PolicyPageViewModel>();

                // Act

                // Assert    
                _myHelper.RunBaseViewModelTests(sut);
                Assert.False(string.IsNullOrWhiteSpace(sut.HTMLSource.Html), "You have no HTML content");
                Assert.False(string.IsNullOrWhiteSpace(sut.HTMLSource.BaseUrl), "You haven't set the baseUrl");
                
            }
        }        

    }
}

