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
   
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _Role;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;
        public RoleController(RoleManager<IdentityRole> Role, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _Role = Role;
            _db = db;
            _userManager = userManager;
        }
        // GET: RoleController
        public ActionResult Index()
        {
            var roles = _Role.Roles.ToList();
            ViewBag.Roles = roles;
            return View(_Role.Roles.ToList());
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        


        // GET: RoleController/Edit/5
        public async Task<ActionResult> Edit(string  id)
        {
            var role = await _Role.FindByIdAsync(id);
       
            if(role==null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id,string name)
        {
            var role = await _Role.FindByIdAsync(id);
            role.Name = name;
            var isExist = await _Role.RoleExistsAsync(role.Name);
            if(isExist)
            {
                ViewBag.mgs = "This role is already exist";
                ViewBag.name = name;
               
            }
            var result = await _Role.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been updated successfully";
            }
            return View();
           
        }

        // GET: RoleController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var role = await _Role.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            var role = await _Role.FindByIdAsync(id);

            await _Role.DeleteAsync(role);

            return RedirectToAction(nameof(Index));

        }


        public ActionResult CreateRole(string id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(CreateRole model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult Result = await _Role.CreateAsync(identityRole);
                if (Result.Succeeded)
                {
                    return RedirectToAction("index", "Role");
                }

                foreach (IdentityError error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
    }
}
