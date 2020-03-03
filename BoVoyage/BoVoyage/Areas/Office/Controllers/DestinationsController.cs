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
using Microsoft.AspNetCore.Authorization;

namespace BoVoyage.Areas.Office.Controllers
{
    [Area("Office")]
    [Authorize(Roles = "Admin, Manager")]
    public class DestinationsController : Controller
    {
        private const string ROOT = "wwwroot/img/";
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
        public IActionResult Create(bool voyage = false)
        {
            ViewBag.voyage = voyage;
            ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom");
            return View();
        }

        // POST: Office/Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdParente,Nom,Niveau,Description,Photo")] Destination destination, List<IFormFile> photos, bool voyage)
        {
            if (ModelState.IsValid)
            {
                foreach (var photo in photos)
                {
                    if (photo.Length > 0 && (photo.FileName.EndsWith(".png")) || (photo.FileName.EndsWith(".jpeg")) || (photo.FileName.EndsWith(".jpg")))
                    {
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        var FileExtension = Path.GetExtension(photo.FileName);
                        using (var stream = System.IO.File.Create(ROOT + myUniqueFileName + FileExtension))
                        {
                            await photo.CopyToAsync(stream);
                        }
                        var photoSql = new Photo() { IdDestination = destination.Id, NomFichier = myUniqueFileName + FileExtension };
                        destination.Photo.Add(photoSql);
                    }
                }
                _context.Add(destination);
                await _context.SaveChangesAsync();
                if (voyage)
                    return RedirectToAction("Create", "Voyages", new { id = destination.Id });
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

            var destination = await _context.Destination.Include(d => d.Photo).SingleOrDefaultAsync(d => d.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdParente,Nom,Niveau,Description")] Destination destination, List<IFormFile> photos)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var photo in photos)
                    {
                        if (photo.Length > 0 && (photo.FileName.EndsWith(".png")) || (photo.FileName.EndsWith(".jpeg")) || (photo.FileName.EndsWith(".jpg")))
                        {
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var FileExtension = Path.GetExtension(photo.FileName);
                            using (var stream = System.IO.File.Create(ROOT + myUniqueFileName + FileExtension))
                            {
                                await photo.CopyToAsync(stream);
                            }
                            var photoSql = new Photo() { IdDestination = destination.Id, NomFichier = myUniqueFileName + FileExtension };
                            destination.Photo.Add(photoSql);
                        }
                    }
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
                .Include(d => d.IdParenteNavigation).Include(d => d.Voyage)
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
            var destination = await _context.Destination.Include(d => d.Photo).SingleOrDefaultAsync(d => d.Id == id);
            foreach (var photo in destination.Photo)
            {
                if (System.IO.File.Exists(ROOT + photo.NomFichier))
                    System.IO.File.Delete(ROOT + photo.NomFichier);
                _context.Photo.Remove(photo);
            }
            _context.Voyage.RemoveRange(destination.Voyage);
            _context.Destination.Remove(destination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("DeletePhoto")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhoto(int id, int idDest)
        {
            if (id == 0)
            {
                var destination = await _context.Destination.Include(d => d.Photo).SingleOrDefaultAsync(d => d.Id == idDest);
                foreach (var photo in destination.Photo)
                {
                    if (System.IO.File.Exists(ROOT + photo.NomFichier))
                        System.IO.File.Delete(ROOT + photo.NomFichier);
                    _context.Photo.Remove(photo);
                }
            }
            else
            {
                var photo = await _context.Photo.FindAsync(id);
                if (System.IO.File.Exists(ROOT + photo.NomFichier))
                    System.IO.File.Delete(ROOT + photo.NomFichier);
                _context.Photo.Remove(photo);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = idDest });
        }
        private bool DestinationExists(int id)
        {
            return _context.Destination.Any(e => e.Id == id);
        }
    }
}
