using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(100, MinimumLength =2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength =2)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength =6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Please check your password!")]
        public string ConfirmPassword { get; set; }
    }
}
