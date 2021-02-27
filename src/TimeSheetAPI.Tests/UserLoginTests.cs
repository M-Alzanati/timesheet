using Microsoft.EntityFrameworkCore;
using TimeSheetAPI.Models;
using Xunit;
using System;

namespace TimeSheetAPI.Tests
{
    public class UserLoginTests
    {
        TimeSheetDbContext _ctx;
        UserLoginRepository _repo;

        private DateTime login1 = DateTime.Now.AddSeconds(5);
        private DateTime logout1 = DateTime.Now.AddHours(1);

        private DateTime login2 = DateTime.Now.AddSeconds(10);
        private DateTime logout2 = DateTime.Now.AddHours(2);

        private DateTime login3 = DateTime.Now.AddSeconds(20);
        private DateTime logout3 = DateTime.Now.AddHours(3);

        public UserLoginTests()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TimeSheetDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            _ctx = new TimeSheetDbContext(options);
            _ctx.UserLogins.Add(new UserLogin { LoginTime = login1, UUId = "1" });
            _ctx.UserLogouts.Add(new UserLogout { LogoutTime = logout1, UUId = "1" });

            _ctx.UserLogins.Add(new UserLogin { LoginTime = login2, UUId = "1" });
            _ctx.UserLogouts.Add(new UserLogout { LogoutTime = logout2, UUId = "1" });

            _ctx.UserLogins.Add(new UserLogin { LoginTime = login3, UUId = "1" });
            _ctx.UserLogouts.Add(new UserLogout { LogoutTime = logout3, UUId = "1" });

            _ctx.SaveChanges();

            _repo = new UserLoginRepository(_ctx);
        }

        [Fact]
        public async void TestFirstLogin()
        {
            // Act
            var actualLogin = await _repo.GetFirstLogin("1");

            // Assert
            Assert.Equal(login1.ToShortTimeString(), DateTime.Parse(actualLogin).ToShortTimeString());
            Assert.Equal(login1.ToShortDateString(), DateTime.Parse(actualLogin).ToShortDateString());
        }

        [Fact]
        public async void TestLastLogout()
        {
            // Act
            var actualLogout = await _repo.GetLastLogout("1");

            // Assert
            Assert.Equal(logout3.ToShortTimeString(), DateTime.Parse(actualLogout).ToShortTimeString());
            Assert.Equal(logout3.ToShortDateString(), DateTime.Parse(actualLogout).ToShortDateString());
        }

        [Fact]
        public async void TestCanSaveLogin()
        {
            // Act
            var result = await _repo.SaveUserLoginAsync("1", DateTime.Now);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void TestCanSaveLogout()
        {
            // Act
            var result = await _repo.SaveUserLogoutAsync("1", DateTime.Now);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void TestSaveTimeSheet() 
        {
            // Act
            var result = await _repo.SaveOrUpdateTimeSheetAsync("1", DateTime.Now, "12:01 AM", "05:54 PM");

            // Assert
            Assert.True(result);
        }
    }
}
