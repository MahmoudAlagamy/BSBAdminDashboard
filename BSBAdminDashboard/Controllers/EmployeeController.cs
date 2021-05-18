using BSBAdminDashboard.BL.Interface;
using BSBAdminDashboard.Models;
using BSBAdminDashboard.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace BSBAdminDashboard.Controllers
{
    public class EmployeeController : Controller
    {
        // Loosly Coupled
        private readonly IEmployeeRep employee;
        private readonly IDepartmentRep department;
        private readonly ICountryRep country;
        private readonly ICityRep city;
        private readonly IDistrictRep ditrict;
        //private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        // Dependency Injection
        // employeeRep employee => employee = new employeeRep()
        public EmployeeController(IEmployeeRep employee, IDepartmentRep department, ICountryRep country, ICityRep city, IDistrictRep ditrict/*, IStringLocalizer<SharedResource> SharedLocalizer*/)
        {
            this.employee = employee;
            this.department = department;
            this.country = country;
            this.city = city;
            this.ditrict = ditrict;
            //sharedLocalizer = SharedLocalizer;
        }

        public IActionResult Index()
        {
            // Show & Return first 3 Employees 
            //var data = employee.Get().Skip(0).Take(3);

            //Show & Return All Employees
            var data = employee.Get();

            return View(data);
        }

        // Create Data
        // Defult [HttpGet]
        public IActionResult Create()
        {
            var data = department.Get();
            var countrydata = country.Get();
            ViewBag.DepartmentList = new SelectList(data, "Id", "DepartmentName");
            ViewBag.CountryList = new SelectList(countrydata, "Id", "CountryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM emp)
        {
            try
            {
                // check data validation
                if (ModelState.IsValid)
                {

                    #region Save Image befor Helper Folder move to EmploeeRep

                    //// Get Directory
                    //string PhotoPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos/";

                    //// Get File Name
                    //// Guid.NewGuid() use to not replace files have same name and givit random code
                    //string PhotoName = Guid.NewGuid() + Path.GetFileName(emp.PhotoUrl.FileName);

                    //// Merge The Directory With File Name
                    //// if forget '/' use 'Path.Combine'
                    //string FinalPath = Path.Combine(PhotoPath , PhotoName);

                    //// Save Your Files As Stream "Data Over Time"
                    //using (var stream = new FileStream(FinalPath, FileMode.Create))
                    //{
                    //    emp.PhotoUrl.CopyTo(stream);
                    //}

                    #endregion

                    #region Save CV befor Helper Folder move to EmploeeRep

                    //// Get Directory
                    //string CVPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/Docs/";

                    //// Get File Name
                    //// Guid.NewGuid() use to not replace files have same name and givit random code
                    //string CVName = Guid.NewGuid() + Path.GetFileName(emp.CVUrl.FileName);

                    //// Merge The Directory With File Name
                    //// if forget '/' use 'Path.Combine'
                    //string FinalCVPath = Path.Combine(CVPath, CVName);

                    //// Save Your Files As Stream "Data Over Time"
                    //using (var stream = new FileStream(FinalCVPath, FileMode.Create))
                    //{
                    //    emp.CVUrl.CopyTo(stream);
                    //}

                    #endregion

                    employee.Add(emp);
                    return RedirectToAction("Index", "Employee");
                }


                var data = department.Get();
                var countrydata = country.Get();

                ViewBag.CountryList = new SelectList(countrydata, "Id", "CountryName");
                ViewBag.DepartmentList = new SelectList(data, "Id", "DepartmentName");
                return View(emp);


            }
            catch (Exception ex)
            {
                // using System.Diagnostics;
                // this error mean i can't use EventLog on macOS just windows
                EventLog log = new EventLog();
                log.Source = "Admin Dashboard";
                log.WriteEntry(ex.Message, EventLogEntryType.Error);


                return View(emp); // return with data
            }

        }

        // Edit Data
        public IActionResult Edit(int id)
        {
            var data = employee.GetById(id);
            var Deptdata = department.Get();
            ViewBag.DepartmentList = new SelectList(Deptdata, "Id", "DepartmentName", data.DepartmentId);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeVM emp)
        {
            try
            {
                // check data validation
                if (ModelState.IsValid)
                {
                    employee.Edit(emp);
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    var Deptdata = department.Get();
                    ViewBag.DepartmentList = new SelectList(Deptdata, "Id", "DepartmentName", emp.DepartmentId);
                    return View(emp);
                }

            }
            catch (Exception ex)
            {
                // using System.Diagnostics;
                // this error mean i can't use EventLog on macOS just windows
                EventLog log = new EventLog();
                log.Source = "Admin Dashboard";
                log.WriteEntry(ex.Message, EventLogEntryType.Error);


                return View(emp); // return with data

            }
        }

        // Delete Data
        public IActionResult Delete(int id)
        {
            var data = employee.GetById(id);
            var Deptdata = department.Get();
            ViewBag.DepartmentList = new SelectList(Deptdata, "Id", "DepartmentName", data.DepartmentId);

            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")] // run time name
        public IActionResult ConfirmDelete(int id) // compile time name
        {
            try
            {
                // check data validation
                employee.Delete(id);
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                // using System.Diagnostics;
                // this error mean i can't use EventLog on macOS just windows
                EventLog log = new EventLog();
                log.Source = "Admin Dashboard";
                log.WriteEntry(ex.Message, EventLogEntryType.Error);


                return View(); // return with data

            }
        }

        public ActionResult Details(int id)
        {
            var data = employee.GetById(id);
            var Deptdata = department.Get();

            ViewBag.PhotoN = data.PhotoName;
            ViewBag.CvN = data.CVName;

            ViewBag.DeptName = new SelectList(Deptdata, "Id", "DepartmentName");

            return View(data);
        }

        // Ajax Call

        [HttpPost]
        public JsonResult GetCityDataByCountryId(int CntryId)
        {
            var data = city.Get().Where(a => a.CountryId == CntryId);

            return Json(data);
        }

        [HttpPost]
        public JsonResult GetDistrictDataByCityId(int CityId)
        {
            var data = ditrict.Get().Where(a => a.CityId == CityId);

            return Json(data);
        }

        //public IActionResult Arabic()
        //{
        //    return Redirect(Request.url);
        //}

    }
}
