using System;
using System.Linq;
using System.Collections.Generic;

namespace TimeSheetAPI.Models
{
    public interface IUserLoginRepository
    {
        IQueryable<UserLogin> GetUserLogins(string uuid);

        IQueryable<UserLogin> GetUserLogins(string uuid, DateTime dateTime);
        
        bool SaveUserLogin(string uuid, DateTime login, DateTime logout);

        bool SaveUserLogin(string uuid, IEnumerable<DateTime> login, IEnumerable<DateTime> logout);
    }
}