using System;

namespace TimeSheetAPI.Models
{
    public class UserLogin
    {
        public int Id { set; get; }

        public string UUId { set; get; }

        public DateTime LoginTime { set; get; }
    }
}