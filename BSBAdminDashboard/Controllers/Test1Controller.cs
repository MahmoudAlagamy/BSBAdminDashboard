using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.Controllers
{
    public class Test1Controller : Controller
    {
        public IActionResult Index()
        {
            // <!-- Inject In View index important --> 
            return View();
        }

        // Ajax Call
        [HttpPost] // post mean don't show in browser
        public JsonResult Display(string name)
        {
            var data = "Welcome : " + name;
            return Json (data);
        }
    }
}
