using System.ComponentModel.DataAnnotations;

namespace TimeSheetAPI.Controllers.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}