using AutoMapper;
using BSBAdminDashboard.DAL.Entities;
using BSBAdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // Mapper for Department
            CreateMap<Department, DepartmentVM>();
            CreateMap<DepartmentVM, Department>();

            // Mapper for Employee
            CreateMap<Employee, EmployeeVM>();
            CreateMap<EmployeeVM, Employee>();
        }
    }
}
