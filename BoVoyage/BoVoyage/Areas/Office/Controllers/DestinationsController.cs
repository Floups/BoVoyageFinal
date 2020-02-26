using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BoVoyage.Areas.Office.Controllers
{
    [Area("Office")]
    public class DestinationsController : Controller
    {
        private readonly BoVoyageContext _context;

        public DestinationsController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Office/Destinations
        public async Task<IActionResult> Index()
        {
            var boVoyageContext = _context.Destination.Include(d => d.IdParenteNavigation);
            return View(await boVoyageContext.ToListAsync());
        }

        // GET: Office/Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destination
                .Include(d => d.IdParenteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // GET: Office/Destinations/Create
        public IActionResult Create()
        {
            ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom");
            return View();
        }

        // POST: Office/Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdParente,Nom,Niveau,Description,Photo")] Destination destination, List<IFormFile> photos)
        {
            if (ModelState.IsValid)
            {
                foreach (var photo in photos)
                {
                    if (photo.Length > 0)
                    {
                        using (var stream = System.IO.File.Create("wwwroot/img/" + photo.FileName))
                        {
                            await photo.CopyToAsync(stream);
                        }
                        var photoSql = new Photo() { IdDestination = destination.Id, NomFichier = photo.FileName };
                        destination.Photo.Add(photoSql);
                    }
                }
                _context.Add(destination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom", destination.IdParente);
            return View(destination);
        }

        // GET: Office/Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destination.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom", destination.IdParente);
            return View(destination);
        }

        // POST: Office/Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdParente,Nom,Niveau,Description")] Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinationExists(destination.Id))
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
            ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom", destination.IdParente);
            return View(destination);
        }

        // GET: Office/Destinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destination
                .Include(d => d.IdParenteNavigation).Include(d=>d.Voyage)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.voyage = destination.Voyage.Count();
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // POST: Office/Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destination = await _context.Destination.Include(d=>d.Voyage).SingleOrDefaultAsync(d=>d.Id==id);
            _context.Voyage.RemoveRange(destination.Voyage);
            _context.Destination.Remove(destination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinationExists(int id)
        {
            return _context.Destination.Any(e => e.Id == id);
        }
    }
}
