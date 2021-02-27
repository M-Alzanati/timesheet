using System;
using System.ComponentModel.DataAnnotations;

namespace TimeSheetAPI.Controllers.ViewModels
{
    public class SubmitSheetViewModel
    {
        [Required]
        public string UUId { set; get; }

        [Required]
        public string LoginTime { set; get; }

        [Required]
        public string LogoutTime { set; get; }

        [Required]
        public DateTime Date { set; get; }
    }
}