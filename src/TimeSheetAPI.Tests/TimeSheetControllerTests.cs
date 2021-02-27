// using System.Net.Http;
// using System.Threading.Tasks;
// using TimeSheetAPI.Controllers.ViewModels;
// using Xunit;

// namespace TimeSheetAPI.Tests
// {
//     public class TimeSheetControllerTests : IClassFixture<TestFixture<Startup>>
//     {
//         private HttpClient _client;

//         public TimeSheetControllerTests(TestFixture<Startup> fixture)
//         {
//             _client = fixture.Client;
//         }

//         [Fact]
//         public async Task AddLogin()
//         {
//             // Arrange
//             var request = "/api/timeSheet/logins/add";
//             var model = new LogTimeViewModel()
//             {
//                 UUId ="1",
//                 Time = "12:45 AM"
//             };

//             // Act
//             var response = await _client.PostAsync(request, ContentHelper.GetStringContent(model));

//             // Assert
//             response.EnsureSuccessStatusCode();
//         }

//         [Fact]
//         public async Task AddLogout()
//         {
//             // Arrange
//             var request = "/api/timeSheet/logouts/add";
//             var model = new LogTimeViewModel()
//             {
//                 UUId ="1",
//                 Time = "06:45 PM"
//             };

//             // Act
//             var response = await _client.PostAsync(request, ContentHelper.GetStringContent(model));

//             // Assert
//             response.EnsureSuccessStatusCode();
//         }

//         [Fact]
//         public async Task GetFirstLogin()
//         {
//             // Arrange
//             var request = "/api/timeSheet/logins/first/1";

//             // Act
//             var response = await _client.GetAsync(request);

//             // Assert
//             response.EnsureSuccessStatusCode();
//         }

//         [Fact]
//         public async Task GetLastLogout()
//         {
//             // Arrange
//             var request = "/api/timeSheet/logins/last/1";

//             // Act
//             var response = await _client.GetAsync(request);

//             // Assert
//             response.EnsureSuccessStatusCode();
//         }
//     }
// }
