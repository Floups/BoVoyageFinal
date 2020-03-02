using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Models;
using Microsoft.AspNetCore.Authorization;

namespace BoVoyage.Areas.Office.Controllers
{
    [Area("Office")]
    [Authorize(Roles = "Admin, Manager")]
    public class ClientsController : Controller
    {
        private readonly BoVoyageContext _context;

        public ClientsController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Office/Clients
        public async Task<IActionResult> Index(string nom)
        {
            ViewBag.Nom = nom;

            IQueryable<BoVoyage.Models.Client> clients = _context.Client.Include(c => c.IdNavigation);

            if (nom != null)
                clients = clients.Where(c => c.IdNavigation.Nom.Contains(nom));

            var boVoyageContext = await clients.ToListAsync();
         
            return View( boVoyageContext);
        }

        // GET: Office/Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [Authorize(Roles = "Admin")]
        // GET: Office/Clients/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Personne, "Id", "Civilite");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Office/Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] BoVoyage.Models.Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Personne, "Id", "Civilite", client.Id);
            return View(client);
        }
        [Authorize(Roles = "Admin")]
        // GET: Office/Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Personne, "Id", "Civilite", client.Id);
            return View(client);
        }
        [Authorize(Roles = "Admin")]
        // POST: Office/Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] BoVoyage.Models.Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            ViewData["Id"] = new SelectList(_context.Personne, "Id", "Civilite", client.Id);
            return View(client);
        }
        [Authorize(Roles = "Admin")]
        // GET: Office/Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }
        [Authorize(Roles = "Admin")]
        // POST: Office/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
