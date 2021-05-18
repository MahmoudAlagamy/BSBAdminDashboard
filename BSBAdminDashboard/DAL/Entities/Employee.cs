using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.DAL.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public float Salary { get; set; }
        public string Address { get; set; }
        public DateTime HierDate { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        // Forign Department key 1-M
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        // ForeignKey District
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }

        // Upload Files
        public string PhotoName { get; set; }
        public string CVName { get; set; }

    }
}
