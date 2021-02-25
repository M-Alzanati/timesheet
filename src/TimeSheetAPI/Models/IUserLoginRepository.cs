using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TimeSheetAPI.Models
{
    public interface IUserLoginRepository
    {
        Task<IEnumerable<UserLogin>> GetUserLogins(string uuid);

        Task<IEnumerable<UserLogout>> GetUserLogouts(string uuid);
        
        Task<bool> SaveUserLoginAsync(string uuid, DateTime login);

        Task<bool> SaveUserLogoutAsync(string uuid, DateTime logout);
    }
}
