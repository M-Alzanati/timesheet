using TimeSheetAPI.Models;

namespace TimeSheetAPI.Services
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        
        public string FullName { get; set; }
        
        public string Phone { get; set; }
        
        public string Token { get; set; }

        public AuthenticateResponse(MyIdentityUser user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Phone = user.PhoneNumber;
            Token = token;
        }
    }
}