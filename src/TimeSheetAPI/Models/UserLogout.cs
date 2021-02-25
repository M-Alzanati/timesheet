using System;

namespace TimeSheetAPI.Models
{
    public class UserLogout
    {
        public int Id { set; get; }

        public string UUId { set; get; }

        public DateTime LogoutTime { set; get; }
    }
}