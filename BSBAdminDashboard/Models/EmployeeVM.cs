using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Name")]
        [MaxLength(50, ErrorMessage = "Max Len 50")]
        [MinLength(3, ErrorMessage = "Min Len 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Salary")]
        [Range(3000, 10000, ErrorMessage = "Enter Salary From 3K To 10K")]
        public float Salary { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [RegularExpression("[0-9]{2,5}-[a-zA-Z]{3,50}-[a-zA-Z]{3,50}-[a-zA-Z]{3,50}", ErrorMessage ="Enter Like 12-Street Name-City Name-Country Name")]
        public string Address { get; set; }
        public DateTime HierDate { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        // To Reseve File
        public IFormFile PhotoUrl { get; set; }
        public IFormFile CVUrl { get; set; }

        // Upload Files
        public string PhotoName { get; set; }
        public string CVName { get; set; }

        // Forign key 1-M
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

    }
}
