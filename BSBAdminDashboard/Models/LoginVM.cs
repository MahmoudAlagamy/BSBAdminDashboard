using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.Models
{
    public class LoginVM
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
        public bool RememberMe { get; set; }

    }
}
