using System;

namespace TimeSheetAPI.Models
{
    public class SubmissionSheet
    {
        public int Id { set; get; }

        public string UUId { set; get; }

        public TimeSpan Login { set; get; }

        public TimeSpan Logout { set; get; }

        public DateTime Date { set; get; }
    }
}
