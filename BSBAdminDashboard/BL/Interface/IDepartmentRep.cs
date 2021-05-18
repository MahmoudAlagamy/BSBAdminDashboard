using BSBAdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Interface
{
    public interface IDepartmentRep
    {
        /*
         IQueryable => select and filtaring data in database
         IEnumerable => select data from database and filtering in clint memmory
         */
        IQueryable<DepartmentVM> Get();
        DepartmentVM GetById(int id);
        void Add(DepartmentVM dept);
        void Edit(DepartmentVM dept);
        void Delete(int id);
    }
}
