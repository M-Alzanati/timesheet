using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeSheetAPI.Models
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private TimeSheetDbContext _ctx;
        
        private ILogger<UserLoginRepository> _logger;

        public UserLoginRepository(TimeSheetDbContext ctx, ILogger<UserLoginRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<IEnumerable<UserLogin>> GetUserLogins(string uuid)
        {
            return await _ctx.UserLogins.Where(r => r.UUId == uuid).AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<UserLogout>> GetUserLogouts(string uuid)
        {
            return await _ctx.UserLogouts.Where(r => r.UUId == uuid).AsQueryable().ToListAsync();
        }

        public async Task<bool> SaveUserLoginAsync(string uuid, DateTime login)
        {
            try
            {
                var userLogin = new UserLogin
                {
                    UUId = uuid,
                    LoginTime = login
                };

                _ctx.UserLogins.Add(userLogin);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't save user login");
                return false;
            }
        }

        public async Task<bool> SaveUserLogoutAsync(string uuid, DateTime logout)
        {
            try
            {
                var userLogout = new UserLogout
                {
                    UUId = uuid,
                    LogoutTime = logout
                };
                
                _ctx.UserLogouts.Add(userLogout);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't save user login");
                return false;
            }
        }
    }
}
