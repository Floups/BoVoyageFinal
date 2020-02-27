using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace BoVoyage.Areas.Client.Controllers
{
    [Area("Client")]
    public class VoyagesController : Controller
    {
        private readonly BoVoyageContext _context;

        public VoyagesController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Client/Voyages
        public async Task<IActionResult> Index(int? dest, decimal prixMin, decimal prixMax,DateTime dateMin, DateTime dateMax)
        {
            var listeDestinations = await _context.Destination.ToListAsync();
            ViewBag.Destinations = new SelectList(listeDestinations, "Id", "Nom",dest);
            ViewBag.PrixMin = prixMin;
            ViewBag.PrixMax = prixMax;
            
            IQueryable<Voyage> voyages = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo);
            
            if (dest != null && dest!=0)
                voyages = voyages.Where(v => v.IdDestinationNavigation.Id == dest);
            if (prixMin != 0 && prixMax == 0)
                voyages = voyages.Where(v => v.PrixHt >= prixMin);
            if (prixMin == 0 && prixMax != 0)
                voyages = voyages.Where(v => v.PrixHt <= prixMax);
            if (prixMin != 0 && prixMax != 0)
                voyages =  voyages.Where(v => v.PrixHt >= prixMin && v.PrixHt <= prixMax);
            if (dateMin != DateTime.MinValue && dateMax == DateTime.MinValue)
            {
                ViewBag.DateMin = dateMin.ToString("yyyy-MM-dd");
                voyages = voyages.Where(v => v.DateDepart >= dateMin);
            }
            if (dateMin == DateTime.MinValue && dateMax != DateTime.MinValue)
            {
                ViewBag.DateMax = dateMax.ToString("yyyy-MM-dd");
                voyages = voyages.Where(v => v.DateDepart <= dateMax);
            }
            if (dateMin != DateTime.MinValue && dateMax != DateTime.MinValue)
            {
                ViewBag.DateMin = dateMin.ToString("yyyy-MM-dd");
                ViewBag.DateMax = dateMax.ToString("yyyy-MM-dd");
                voyages = voyages.Where(v => v.DateDepart >= dateMin && v.DateDepart <= dateMax);
            }


            var listeVoyages = await voyages.ToListAsync();
            var photos = new Dictionary<int, string>();
            foreach (var item in listeVoyages)
            {
                Photo photo = item.IdDestinationNavigation.Photo.FirstOrDefault();
                if (photo == null)
                {
                    photos.Add(item.IdDestination, "no_result.jpg");
                }
                else
                {
                    if (!photos.ContainsKey(item.IdDestination))
                    {
                        photos.Add(item.IdDestination, photo.NomFichier);
                    }

                }
            }
            ViewBag.Photos = photos;

            return View(listeVoyages);
        }

        // GET: Client/Voyages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Le voyage demandé ne se trouve pas dans notre base de données.");
            }

            var voyage = await _context.Voyage
                .Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound("Le voyage demandé ne se trouve pas dans notre base de données.");
            }

            return View(voyage);
        }

        // GET: Client/Voyages/Create
        public IActionResult Create()
        {
            ViewData["IdDestination"] = new SelectList(_context.Destination, "Id", "Nom");
            return View();
        }

        // POST: Client/Voyages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDestination,DateDepart,DateRetour,PlacesDispo,PrixHt,Reduction,Descriptif")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voyage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDestination"] = new SelectList(_context.Destination, "Id", "Nom", voyage.IdDestination);
            return View(voyage);
        }

        // GET: Client/Voyages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyage.FindAsync(id);
            if (voyage == null)
            {
                return NotFound();
            }
            ViewData["IdDestination"] = new SelectList(_context.Destination, "Id", "Nom", voyage.IdDestination);
            return View(voyage);
        }

        // POST: Client/Voyages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDestination,DateDepart,DateRetour,PlacesDispo,PrixHt,Reduction,Descriptif")] Voyage voyage)
        {
            if (id != voyage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voyage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoyageExists(voyage.Id))
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
            ViewData["IdDestination"] = new SelectList(_context.Destination, "Id", "Nom", voyage.IdDestination);
            return View(voyage);
        }

        // GET: Client/Voyages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyage
                .Include(v => v.IdDestinationNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound();
            }

            return View(voyage);
        }

        // POST: Client/Voyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voyage = await _context.Voyage.FindAsync(id);
            _context.Voyage.Remove(voyage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoyageExists(int id)
        {
            return _context.Voyage.Any(e => e.Id == id);
        }
    }
}
