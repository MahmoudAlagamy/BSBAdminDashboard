using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // important for primary key
using System.ComponentModel.DataAnnotations.Schema; // important for edit table

namespace BSBAdminDashboard.DAL.Entities
{
    [Table("Department")] // table name
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string DepartmentName { get; set; }

        [StringLength(50)]
        public string DepartmentCode { get; set; }

        // Forign key From Employee 1-M
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
