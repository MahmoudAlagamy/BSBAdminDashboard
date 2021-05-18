using BSBAdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> userManager;

        public AdminController(RoleManager<IdentityRole> userManager)
        {
            this.userManager = userManager;
        }

        #region CRUD Grid Table
        public IActionResult Index()
        {
            var data = userManager.Roles;

            return View(data);
        }
        #endregion

        #region Create Role
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {

            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model.RoleName
                };

                var result = await userManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("CreateRole");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(model);
        }

        #endregion

        #region Edit Role
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await userManager.FindByIdAsync(Id);

            var data = new EditRoleVM()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var role = await userManager.FindByIdAsync(model.Id);
                role.Name = model.RoleName;
                var result = await userManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        #endregion

        #region Delete Role
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await userManager.FindByIdAsync(Id);

            var data = new DeleteRoleVM()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var role = await userManager.FindByIdAsync(model.Id);
                
                var result = await userManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        #endregion

    }
}
