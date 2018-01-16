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

    public class LoginViewModelTests : BaseViewModel
    {
        
        [Fact]
        public void WhenInitialized_ShouldSetDefaults()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<LoginViewModel>();

                // Act

                // Assert    
                _myHelper.RunBaseViewModelTests(sut);
            }
        }
        [Fact]
        public async Task WhenLoginButton_IsClicked_ShouldEventuallyLogin()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                _myHelper.SetupMockForViewModels(mock);
                var sut = mock.Create<LoginViewModel>();
                bool sentMessage = false;
                MessagingCenter.Subscribe<ILoginViewModel, bool>(this, "LoginStatus", (sender, args) => sentMessage = args);

                // Act
                // if sentMessage is true, then user has logged in
                while (!sentMessage)
                {
                    await sut.LoginCommand.Execute().FirstAsync();
                }


                // Assert     
                Assert.True(sentMessage);
                MessagingCenter.Unsubscribe<ILoginViewModel, bool>(this, "LoginStatus");
            }
        }
        

    }
}

