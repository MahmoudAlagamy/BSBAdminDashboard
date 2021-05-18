using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BSBAdminDashboard.Models
{
    public class ResetPasswordVM
    {
        // Email
        [Required(ErrorMessage = "Mail Requierd")]
        [EmailAddress(ErrorMessage = "You Must Enter Valid Mail")]
        public string Email { get; set; }

        // Password
        [Required(ErrorMessage = "Password Requierd")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Min Len 3")]
        public string Password { get; set; }

        // ConfirmPassword
        [Required(ErrorMessage = "Password Requierd")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Min Len 3")]
        [Compare("Password", ErrorMessage = "Not Match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
