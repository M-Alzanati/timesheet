using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace TimeSheetAPI.Models
{
    public interface IUserLoginRepository
    {
        Task<IEnumerable<UserLogin>> GetUserLogins(string uuid);

        Task<IEnumerable<UserLogout>> GetUserLogouts(string uuid);

        Task<bool> SaveUserLoginAsync(string uuid, DateTime login);

        Task<bool> SaveUserLogoutAsync(string uuid, DateTime logout);

        Task<string> GetFirstLogin(string uuid);

        Task<string> GetLastLogout(string uuid);

        Task<bool> SaveOrUpdateTimeSheetAsync(string uuid, DateTime date, string login, string logout);

        Task<IEnumerable<SubmissionSheet>> GetSubmissionSheets(string uuid);
    }

    public class UserLoginRepository : IUserLoginRepository
    {
        private TimeSheetDbContext _ctx;

        private ILogger<UserLoginRepository> _logger;

        public UserLoginRepository(TimeSheetDbContext ctx)
        {
            _ctx = ctx;
        }

        public UserLoginRepository(TimeSheetDbContext ctx, ILogger<UserLoginRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<string> GetFirstLogin(string uuid)
        {
            _logger?.LogDebug("GetFirstLogin", uuid);

            var res = await (from day in _ctx.UserLogins
                             where day.UUId == uuid
                             orderby day.LoginTime ascending
                             select day).FirstOrDefaultAsync();
            if (res != null)
            {
                return res.LoginTime.ToString();
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetLastLogout(string uuid)
        {
            _logger?.LogDebug("GetLastLogout", uuid);

            var res = await (from day in _ctx.UserLogouts
                             where day.UUId == uuid
                             orderby day.LogoutTime descending
                             select day).FirstOrDefaultAsync();
            if (res != null)
            {
                return res.LogoutTime.ToString();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserLogin>> GetUserLogins(string uuid)
        {
            _logger?.LogDebug("GetUserLogins", uuid);
            return await _ctx.UserLogins.Where(r => r.UUId == uuid).AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<UserLogout>> GetUserLogouts(string uuid)
        {
            _logger?.LogDebug("GetUserLogouts", uuid);
            return await _ctx.UserLogouts.Where(r => r.UUId == uuid).AsQueryable().ToListAsync();
        }

        public async Task<bool> SaveUserLoginAsync(string uuid, DateTime login)
        {
            _logger?.LogDebug("SaveUserLoginAsync", uuid, login.ToString());

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
                _logger?.LogError(ex, "Can't save user login");
                return false;
            }
        }

        public async Task<bool> SaveUserLogoutAsync(string uuid, DateTime logout)
        {
            _logger?.LogDebug("SaveUserLogoutAsync", uuid, logout.ToString());

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
                _logger?.LogError(ex, "Can't save user login");
                return false;
            }
        }

        public async Task<bool> SaveOrUpdateTimeSheetAsync(string uuid, DateTime date, string login, string logout)
        {
            _logger?.LogDebug("SaveOrUpdateTimeSheetAsync", uuid, date.ToString(), login, logout);

            try
            {
                var loginTime = DateTime.ParseExact(login, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                var logoutTime = DateTime.ParseExact(logout, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;

                var dbRecord = await _ctx.SubmissionSheets.FirstOrDefaultAsync(r => r.UUId == uuid && r.Date.Date == date.Date);
                if (dbRecord != null)
                {
                    dbRecord.Login = loginTime;
                    dbRecord.Logout = logoutTime;
                    _ctx.SubmissionSheets.Update(dbRecord);
                }
                else
                {
                    dbRecord = new SubmissionSheet
                    {
                        UUId = uuid,
                        Login = loginTime,
                        Logout = logoutTime,
                        Date = date
                    };
                    _ctx.SubmissionSheets.Add(dbRecord);
                }

                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Can't save TimeSheet");
                return false;
            }
        }

        public async Task<IEnumerable<SubmissionSheet>> GetSubmissionSheets(string uuid)
        {
            _logger?.LogDebug("GetSubmissionSheets", uuid);

            var lst = await _ctx.SubmissionSheets.Where(r => r.UUId == uuid).ToListAsync();
            return lst;
        }
    }
}
