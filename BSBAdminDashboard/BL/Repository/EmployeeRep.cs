using AutoMapper;
using BSBAdminDashboard.BL.Helper;
using BSBAdminDashboard.BL.Interface;
using BSBAdminDashboard.DAL.Database;
using BSBAdminDashboard.DAL.Entities;
using BSBAdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Repository
{
    public class EmployeeRep : IEmployeeRep
    {
        //private DbContainer db = new DbContainer();

        private readonly DbContainer db;
        private readonly IMapper mapper;

        public EmployeeRep(DbContainer db, IMapper _Mapper)
        {
            this.db = db;
            mapper = _Mapper;
        }

        public IQueryable<EmployeeVM> Get()
        {
            IQueryable<EmployeeVM> data = GetAllemps();

            return data;
        }

        public EmployeeVM GetById(int id)
        {
            EmployeeVM data = GetEmployeeById(id);
            // FirstOrDefault couse if not found id back null
            return data;
        }

        // Save Data into database 
        public void Add(EmployeeVM emp)
        {
            var data = mapper.Map<Employee>(emp);

            //var path = "/wwwroot/Files/Photos/";
            data.PhotoName = UploadeFileHelper.SaveFile(emp.PhotoUrl, "Photos/");
            data.CVName = UploadeFileHelper.SaveFile(emp.CVUrl, "Docs/");

            db.Employee.Add(data);
            db.SaveChanges();
        }

        public void Edit(EmployeeVM emp)
        {
            var data = mapper.Map<Employee>(emp);
            db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var deletedObject = db.Employee.Find(id);

            db.Employee.Remove(deletedObject);

            UploadeFileHelper.RemoveFile("Photos/", deletedObject.PhotoName);
            UploadeFileHelper.RemoveFile("Docs/", deletedObject.CVName);

            db.SaveChanges();
        }

        // Refactor

        private EmployeeVM GetEmployeeById(int id)
        {
            return db.Employee.Where(a => a.Id == id)
            .Select(a => new EmployeeVM
            {
                Id = a.Id,
                Name = a.Name,
                Salary = a.Salary,
                Address = a.Address,
                HierDate = a.HierDate,
                IsActive = a.IsActive,
                Email = a.Email,
                Notes = a.Notes,
                DepartmentId = a.DepartmentId,
                DistrictId = a.DistrictId,
                // Upload Files
                PhotoName = a.PhotoName,
                CVName = a.CVName,
                DepartmentName = a.Department.DepartmentName,
                DistrictName = a.District.DistrictName

            }).FirstOrDefault();
        }

        private IQueryable<EmployeeVM> GetAllemps()
        {
            return db.Employee.Select(a => new EmployeeVM
            {
                Id = a.Id,
                Name = a.Name,
                Salary = a.Salary,
                Address = a.Address,
                HierDate = a.HierDate,
                IsActive = a.IsActive,
                Email = a.Email,
                Notes = a.Notes,
                DepartmentId = a.DepartmentId,
                DistrictId = a.DistrictId,
                // Upload Files
                PhotoName = a.PhotoName,
                CVName = a.CVName,
                DepartmentName = a.Department.DepartmentName,
                DistrictName = a.District.DistrictName

            });
        }
    }
}
