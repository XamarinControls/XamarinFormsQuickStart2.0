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

    public class RecentPageViewModelTests : BaseViewModel
    {
        
        [Fact]
        public void WhenInitialized_ShouldSetDefaults()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<RecentPageViewModel>();

                // Act

                // Assert    
                _myHelper.RunBaseViewModelTests(sut);

            }
        }        

    }
}

