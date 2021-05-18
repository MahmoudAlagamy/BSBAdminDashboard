using BSBAdminDashboard.BL.Repository;
using BSBAdminDashboard.DAL.Database;
using BSBAdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using BSBAdminDashboard.BL.Interface;

namespace BSBAdminDashboard.Controllers
{
    public class DepartmentController : Controller
    {
        // to GET Data From database befor using interface and Repository
        //private DbContainer db = new DbContainer();

        // to GET Data From database After using interface and Repository

        //private DepartmentRep department = new DepartmentRep();

        // Edit Inistance

        // OR better way couse the constructor work firs when using class

        //private DepartmentRep department;
        //public DepartmentController()
        //{
        //    this.department = new DepartmentRep();
        //}

        // OR 2- Take instance for each user (Fast)
        // Inject Inestance from Startup.cs

        // this way called tightly Coupled ارباط باحكام
        //private readonly DepartmentRep department;

        // this way called Losely Coupled اربتباط ضعيف
        private readonly IDepartmentRep department;

        public DepartmentController(IDepartmentRep department) // DepartmentRep department => department = new DepartmentRep()
        {
            this.department = department;
        }

        public IActionResult Index()
        {
            //---------------------------------------

            var data = department.Get();
            return View(data);

            //---------------------------------------

            //var data = db.Department.Select(a => a);

            //return View(data);

            //---------------------------------------

            // The Bettr Way To Get data With Fill View Model
            //var data = db.Department.Select(a => new DepartmentVM
            //{
            //    Id = a.Id,
            //    DepartmentName = a.DepartmentName,
            //    DepartmentCode = a.DepartmentCode
            //});

            //return View(data);

            //---------------------------------------

            //// ViewData => Lik Object Don't Know The Type of Data
            //ViewData["x"] = "Hi I'm View Data";

            //// ViewBag => Lik Dynamic Know The Type of Data on run time to
            //ViewBag.y = "Hi I'm View Bag";

            //// TempData => Can Use to send data from action View to another view of another controller
            //TempData["z"] = "Hi I'm Temp Data";
            //return View();

            ////---------------------------------------

            //string[] arr = { "Mahmoud", "Ahmed", "Islam", "Samy" };
            //ViewBag.data = arr;

            //return View();

            //---------------------------------------

            //DepartmentVM dept1 = new DepartmentVM()
            //{
            //    Id = 1,
            //    DepartmentName = "HR",
            //    DepartmentCode = "A1020"
            //};
            //DepartmentVM dept2 = new DepartmentVM()
            //{
            //    Id = 2,
            //    DepartmentName = "IT",
            //    DepartmentCode = "B2020"
            //};
            //DepartmentVM dept3 = new DepartmentVM()
            //{
            //    Id = 3,
            //    DepartmentName = "Sales",
            //    DepartmentCode = "C3020"
            //};

            //List<DepartmentVM> DeptData = new List<DepartmentVM>();
            //DeptData.Add(dept1);
            //DeptData.Add(dept2);
            //DeptData.Add(dept3);

            //return View(DeptData);

            //var data = DeptData;
            //return View(data);

            ////OR

            //ViewBag.data = DeptData;
            //return View(ViewBag.data);

            //--------------------------------------------------

            //return RedirectToAction("Index", "Home");

            /* Go To Another Page Using RedirectToAction, Redirect
                - If Action In The Same Controller Just Use Action Name
                  return RedirectToAction("Index");
                
                - If Action In Another Controller Use Action Name and Controller Name
                  return RedirectToAction("Index", "Home");
             
                - OR To Call Singl HTML Page Alone
                  return Redirect("/Home/Index");
             */
        }

        // Create Data
        // Defult [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentVM dept) // name , code
        {
            try
            {
                // check data validation
                if (ModelState.IsValid)
                {
                    department.Add(dept);
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    return View(dept);
                }

            }
            catch (Exception ex)
            {
                // using System.Diagnostics;
                // this error mean i can't use EventLog on macOS just windows
                EventLog log = new EventLog();
                log.Source = "Admin Dashboard";
                log.WriteEntry(ex.Message, EventLogEntryType.Error);


                return View(dept); // return with data
            }

        }

        // Edit Data
        public IActionResult Edit(int id)
        {
            var data = department.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentVM dept)
        {
            try
            {
                // check data validation
                if (ModelState.IsValid)
                {
                    department.Edit(dept);
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    return View(dept);
                }

            }
            catch (Exception ex)
            {
                // using System.Diagnostics;
                // this error mean i can't use EventLog on macOS just windows
                EventLog log = new EventLog();
                log.Source = "Admin Dashboard";
                log.WriteEntry(ex.Message, EventLogEntryType.Error);


                return View(dept); // return with data

            }
        }

        // Delete Data
        public IActionResult Delete(int id)
        {
            var data = department.GetById(id);
            
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")] // run time name
        public IActionResult ConfirmDelete(int id) // compile time name
        {
            try
            {
                // check data validation
                    department.Delete(id);
                    return RedirectToAction("Index", "Department");
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
    }
}