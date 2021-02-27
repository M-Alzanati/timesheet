using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TimeSheetAPI.Controllers.ViewModels;
using Xunit;

namespace TimeSheetAPI.Tests
{
    public class AccountControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;

        public AccountControllerTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CanRegister()
        {
            // Arrange
            var request = "/api/account/register";
            var model = new RegisterViewModel()
            {
                Email = "test@gmail.com",
                Password = "Admin@123",
                FullName = "Test User",
                Phone = "01158426841"
            };

            // Act
            var response = await _client.PostAsync(request, ContentHelper.GetStringContent(model));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CanLogin()
        {
            // Arrange
            var request = "/api/account/login";
            var model = new LoginViewModel()
            {
                Email = "admin@gmail.com",
                Password = "Admin@123",
            };

            // Act
            var response = await _client.PostAsync(request, ContentHelper.GetStringContent(model));

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
