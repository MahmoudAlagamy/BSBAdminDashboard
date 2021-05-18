using AutoMapper;
using BSBAdminDashboard.BL.Interface;
using BSBAdminDashboard.DAL.Database;
using BSBAdminDashboard.DAL.Entities;
using BSBAdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Repository
{
    public class DepartmentRep : IDepartmentRep
    {

        //private DbContainer db = new DbContainer();

        private readonly DbContainer db;
        private readonly IMapper mapper;

        public DepartmentRep(DbContainer db, IMapper _Mapper)
        {
            this.db = db;
            mapper = _Mapper;
        }

        public IQueryable<DepartmentVM> Get()
        {
            IQueryable<DepartmentVM> data = GetAllDepts();

            return data;
        }

        public DepartmentVM GetById(int id)
        {
            DepartmentVM data = GetDepartmentById(id);
            // FirstOrDefault couse if not found id back null
            return data;
        }

        // Save Data into database 
        public void Add(DepartmentVM dept)
        {
            // mapping database with DepartmentVM

            //Department d = new Department();
            //d.DepartmentName = dept.DepartmentName;
            //d.DepartmentCode = dept.DepartmentCode;

            // After Auto Mapper

            var data = mapper.Map<Department>(dept);

            db.Department.Add(data);
            db.SaveChanges();
        }

        public void Edit(DepartmentVM dept)
        {
            //var oldData = db.Department.Find(dept.Id);
            //oldData.DepartmentName = dept.DepartmentName;
            //oldData.DepartmentCode = dept.DepartmentCode;

            //After Mapper 

            var data = mapper.Map<Department>(dept);
            db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var deletedObject = db.Department.Find(id);

            db.Department.Remove(deletedObject);
            db.SaveChanges();
        }

        // Refactor

        private DepartmentVM GetDepartmentById(int id)
        {
            return db.Department.Where(a => a.Id == id)
                                    .Select(a => new DepartmentVM
                                    {
                                        Id = a.Id,
                                        DepartmentName = a.DepartmentName,
                                        DepartmentCode = a.DepartmentCode
                                    }).FirstOrDefault();
        }

        private IQueryable<DepartmentVM> GetAllDepts()
        {
            return db.Department.Select(a => new DepartmentVM
            {
                Id = a.Id,
                DepartmentName = a.DepartmentName,
                DepartmentCode = a.DepartmentCode
            });
        }

    }
}
