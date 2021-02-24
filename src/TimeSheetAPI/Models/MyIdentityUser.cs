using Microsoft.AspNetCore.Identity;

namespace TimeSheetAPI.Models
{
    public class MyIdentityUser : IdentityUser
    {
        public MyIdentityUser() : base() { }

        public MyIdentityUser(string userName) : base(userName)
        {
            Email = userName;
        }

        public string FullName { set; get; }
    }
}