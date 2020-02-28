using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Areas.Client.Models;
using BoVoyage.Models;

namespace BoVoyage.Areas.Client.Controllers
{
    [Area("Client")]
    public class Reservation : Controller
    {
        private readonly BoVoyageContext _context;

        public Reservation(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Client/Reservation
        public async Task<IActionResult> Index(int idVoyage, int nbPersonnes)
        {
            var voyage = await _context.Voyage.Where(v => v.Id == idVoyage).Include(v => v.IdDestinationNavigation).FirstOrDefaultAsync();
            List<Personne> voyageurs = new List<Personne>();
            for (int i = 0; i < nbPersonnes; i++)
            {
                voyageurs.Add(new Personne());
            }

            var voyagePersonnes = new VoyagePersonnesViewModel(voyage, voyageurs);

            return View(voyagePersonnes);
        }

        //public IActionResult AjoutVoyageur([Bind("Civilite", "Nom", "Prenom", "Datenaissance", "Email", "Telephone")] Personne personne, [FromQuery] int idVoyage, [FromQuery] int nbPersonnes, List<Personne> personnes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        personnes.Add(personne);

        //        return RedirectToAction(nameof(Index));
        //    }
        //}

        
        // GET: Client/Reservation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] VoyagePersonnesViewModel voyagePersonnesViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voyagePersonnesViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voyagePersonnesViewModel);
        }

        // GET: Client/Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyagePersonnesViewModel = await _context.VoyagePersonnesViewModel.FindAsync(id);
            if (voyagePersonnesViewModel == null)
            {
                return NotFound();
            }
            return View(voyagePersonnesViewModel);
        }

        // POST: Client/Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] VoyagePersonnesViewModel voyagePersonnesViewModel)
        {
            if (id != voyagePersonnesViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voyagePersonnesViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoyagePersonnesViewModelExists(voyagePersonnesViewModel.Id))
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
            return View(voyagePersonnesViewModel);
        }

        // GET: Client/Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyagePersonnesViewModel = await _context.VoyagePersonnesViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyagePersonnesViewModel == null)
            {
                return NotFound();
            }

            return View(voyagePersonnesViewModel);
        }

        // POST: Client/Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voyagePersonnesViewModel = await _context.VoyagePersonnesViewModel.FindAsync(id);
            _context.VoyagePersonnesViewModel.Remove(voyagePersonnesViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoyagePersonnesViewModelExists(int id)
        {
            return _context.VoyagePersonnesViewModel.Any(e => e.Id == id);
        }
    }
}
