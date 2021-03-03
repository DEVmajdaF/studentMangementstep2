using app1.Data;
using app1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app1.Controllers
{
   
    [Authorize(Roles ="Admin")]
    public class Admin : Controller
    {
        ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
      
        public Admin(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;


        }
        // GET: Admin

       
        public ActionResult Index()
        {

            var  m = _db.ApplicationUser.FromSqlRaw<ApplicationUser>("Select * From AspNetUsers").ToList();
            return View(m);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUser = await _db.ApplicationUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUser = await  _db.ApplicationUser.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            return View(aspNetUser);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(user);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool AspNetUserExists(string id)
        {
            throw new NotImplementedException();
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var student = _db.ApplicationUser.FirstOrDefault(m =>m.Id == id);
            if (student ==null) {
                return NotFound();
            }

            return View(student);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, ApplicationUser user )
        {
            var student = await _db.ApplicationUser.FindAsync(id);
           // var userinfo = _db.ApplicationUser.FirstOrDefault(m => m.Id == student.Id);
            _db.ApplicationUser.Remove(student);
           await _db.SaveChangesAsync();
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
                IdentityResult Result = await _roleManager.CreateAsync(identityRole);
                if (Result.Succeeded)
                {
                    return RedirectToAction("index", "Admin");
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

