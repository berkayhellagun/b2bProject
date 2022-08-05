using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class UserForRegisterDtoModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Compare("Password", ErrorMessage = "Please check your password!")]
        public string ConfirmPassword { get; set; }
    }
}