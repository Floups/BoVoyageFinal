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
    public class DossierresasController : Controller
    {
        private readonly BoVoyageContext _context;

        public DossierresasController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Office/Dossierresas
        public async Task<IActionResult> Index()
        {
            var boVoyageContext = _context.Dossierresa.Include(d => d.IdClientNavigation).Include(d => d.IdEtatDossierNavigation).Include(d => d.IdVoyageNavigation);
            return View(await boVoyageContext.ToListAsync());
        }

        // GET: Office/Dossierresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossierresa = await _context.Dossierresa
                .Include(d => d.IdClientNavigation)
                .Include(d => d.IdEtatDossierNavigation)
                .Include(d => d.IdVoyageNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dossierresa == null)
            {
                return NotFound();
            }

            return View(dossierresa);
        }


        // GET: Office/Dossierresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossierresa = await _context.Dossierresa.FindAsync(id);
            if (dossierresa == null)
            {
                return NotFound();
            }
            ViewData["IdEtatDossier"] = new SelectList(_context.Etatdossier, "Id", "Libelle", dossierresa.IdEtatDossier);
            return View(dossierresa);
        }

        // POST: Office/Dossierresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, byte IdEtatDossier)
        {

            var dossier = await _context.Dossierresa.Include(d => d.IdVoyageNavigation).ThenInclude(v => v.Voyageur).SingleOrDefaultAsync(d => d.Id == id);
            if (IdEtatDossier == 3)
            {
                var voyage = dossier.IdVoyageNavigation;
                voyage.PlacesDispo += voyage.Voyageur.Count();
                _context.Voyage.Update(voyage);
            }
            else if (IdEtatDossier != 3 && dossier.IdEtatDossier == 3)
            {
                var voyage = dossier.IdVoyageNavigation;
                voyage.PlacesDispo -= voyage.Voyageur.Count();
                _context.Voyage.Update(voyage);
            }
            dossier.IdEtatDossier = IdEtatDossier;


            _context.Update(dossier);
            await _context.SaveChangesAsync();



            ViewData["IdEtatDossier"] = new SelectList(_context.Etatdossier, "Id", "Libelle", dossier.IdEtatDossier);
            return RedirectToAction(nameof(Index));
        }

        // GET: Office/Dossierresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossierresa = await _context.Dossierresa
                .Include(d => d.IdClientNavigation)
                .Include(d => d.IdEtatDossierNavigation)
                .Include(d => d.IdVoyageNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dossierresa == null)
            {
                return NotFound();
            }

            return View(dossierresa);
        }

        // POST: Office/Dossierresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dossierresa = await _context.Dossierresa.Include(d => d.IdVoyageNavigation).ThenInclude(v => v.Voyageur).SingleOrDefaultAsync(d => d.Id == id);
            if (dossierresa.IdEtatDossier != 3)
            {
                var voyage = dossierresa.IdVoyageNavigation;
                voyage.PlacesDispo += voyage.Voyageur.Count();
                _context.Voyage.Update(voyage);
            }
            _context.Dossierresa.Remove(dossierresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DossierresaExists(int id)
        {
            return _context.Dossierresa.Any(e => e.Id == id);
        }
    }
}
