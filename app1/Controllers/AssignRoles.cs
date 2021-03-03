using app1.Data;
using app1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app1.Controllers
{
    public class AssignRoles : Controller
    {

        ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _Role;
         UserManager<ApplicationUser> _userManager;

        public AssignRoles(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _Role = roleManager;


        }
        // GET: HomeController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController/Create
        public ActionResult Assign()
        {
            ViewData["UserId"] = new SelectList(_db.ApplicationUser.ToList(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_Role.Roles.ToList(), "Name", "Name");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Assign(RoleUser roleUser)
        {
            var user = _db.ApplicationUser.FirstOrDefault(c => c.Id == roleUser.UserId);

            var role = await _userManager.AddToRoleAsync(user, roleUser.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "User Role Assign has been successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
