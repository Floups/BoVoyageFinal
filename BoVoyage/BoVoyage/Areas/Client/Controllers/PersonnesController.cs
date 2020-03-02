using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BoVoyage.Data;

namespace BoVoyage.Areas.Client.Controllers
{
    [Area("Client")]
    public class PersonnesController : Controller
    {
        private readonly BoVoyageContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _contextApp;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PersonnesController(BoVoyageContext context, UserManager<IdentityUser> userManager, ApplicationDbContext contextApp,SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _contextApp = contextApp;
            _signInManager = signInManager;
        }

        // GET: Client/Personnes/Details/5
        public async Task<IActionResult> Details()
        {
            var userMail = User.FindFirstValue(ClaimTypes.Name);
            if (userMail == null)
            {
                return NotFound();
            }

            IQueryable<Personne> pers = _context.Personne.Include(p => p.Client).ThenInclude(c => c.Dossierresa).ThenInclude(d => d.IdEtatDossierNavigation)
                                                          .Include(p=>p.Client).ThenInclude(c=>c.Dossierresa).ThenInclude(d=>d.IdVoyageNavigation).ThenInclude(v=>v.IdDestinationNavigation);

            var personne = await pers
                
                .Where(p => p.Email == userMail)
                .FirstOrDefaultAsync();


            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // GET: Client/Personnes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Personnes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypePers,Civilite,Nom,Prenom,Email,Telephone,Datenaissance")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personne);
        }

        // GET: Client/Personnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }
            return View(personne);
        }

        // POST: Client/Personnes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypePers,Civilite,Nom,Prenom,Email,Telephone,Datenaissance")] Personne personne)
        {
            if (id != personne.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personne);
                    await _context.SaveChangesAsync();
                    var user = await _contextApp.Users.Where(u=>u.Email==User.FindFirstValue(ClaimTypes.Name)).SingleOrDefaultAsync();
                    user.Email = personne.Email;
                    user.UserName = personne.Email;
                    user.PhoneNumber = personne.Telephone;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.SignInAsync(user, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonneExists(personne.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details");
            }
            return View(personne);
        }

        private bool PersonneExists(int id)
        {
            return _context.Personne.Any(e => e.Id == id);
        }
    }
}
