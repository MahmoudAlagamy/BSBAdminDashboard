using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.DAL.Entities
{
    [Table("District")]
    public class District
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string DistrictName { get; set; }

        // ForeignKey
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
