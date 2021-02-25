using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TimeSheetAPI.Models
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private TimeSheetDbContext _ctx;

        public UserLoginRepository(TimeSheetDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<UserLogin> GetUserLogins(string uuid)
        {
            return _ctx.UserLogins.Where(r => r.UUId == uuid);
        }

        public IQueryable<UserLogin> GetUserLogins(string uuid, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public bool SaveUserLogin(string uuid, DateTime login, DateTime logout)
        {
            throw new NotImplementedException();
        }

        public bool SaveUserLogin(string uuid, IEnumerable<DateTime> login, IEnumerable<DateTime> logout)
        {
            throw new NotImplementedException();
        }
    }
}