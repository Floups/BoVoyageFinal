using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Areas.Office.Models;
using BoVoyage.Models;
using BoVoyage.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BoVoyage.Areas.Office.Controllers
{
    [Area("Office")]
    [Authorize(Roles = "Admin")]
    public class ManagersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ManagersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Office/Managers
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            Dictionary<string, string> roles = new Dictionary<string, string>();
            foreach(var user in users)
            {
                var s = "";
                foreach (var role in await _userManager.GetRolesAsync(user))
                    s += " "+role;
                roles.Add(user.Id,s);
            }
            ViewBag.Roles = roles;
            return View(users);
        }

        // GET: Office/Managers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }
            string role = "";
            foreach (var roles in await _userManager.GetRolesAsync(manager))
                role += "" + roles;
            ViewBag.Roles = role;
            return View(manager);
        }

        // GET: Office/Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Office/Managers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,Email,Password")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = manager.Email, Email = manager.Email, PhoneNumber = manager.PhoneNumber, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, manager.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Manager");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        // GET: Office/Managers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Users.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Office/Managers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PhoneNumber,Email")] Manager manager)
        {
            if (id != manager.Id)
            {
                return NotFound();
            }
            try
            {
                var user = _context.Users.Find(id);
                user.Email = manager.Email;
                user.UserName = manager.Email;
                user.PhoneNumber = manager.PhoneNumber;
                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(manager.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(new IdentityUser() { Id = id, Email = manager.Email, PhoneNumber = manager.PhoneNumber });
                }
            }

            
        }

        // GET: Office/Managers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Office/Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var manager = await _context.Users.FindAsync(id);
            await _userManager.DeleteAsync(manager);
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
