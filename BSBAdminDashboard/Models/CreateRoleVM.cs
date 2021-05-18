using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.Models
{
    public class CreateRoleVM
    {
        [Required(ErrorMessage = "Role Name Requierd")]
        public string RoleName { get; set; }
    }
}
