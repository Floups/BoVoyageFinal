using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Models;
using System.Xml.Serialization;
using System.IO;

namespace BoVoyage.Areas.Office.Controllers
{
    [Area("Office")]
    public class PersonnesController : Controller
    {
        private readonly BoVoyageContext _context;

        public PersonnesController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Office/Personnes
        public async Task<IActionResult> Index(int cat,string nom)
        {
            var dicoCat = new Dictionary<int, string>();
            dicoCat.Add(1,"Client");
            dicoCat.Add(2, "Voyageur");
            dicoCat.Add(3, "Contact");
            dicoCat.Add(4, "Utilisateur");

            var listeCat = await _context.Personne.ToListAsync();
            ViewBag.Categorie = new SelectList(dicoCat, "Key", "Value", cat);
            ViewBag.Nom = nom;
           

            IQueryable<Personne> personnes = _context.Personne;

            if(cat!=0)
                personnes = personnes.Where(p => p.TypePers==cat);
            if (nom != null)
                personnes = personnes.Where(p=>p.Nom.Contains(nom));
           
            var boVoyageContext = await personnes.ToListAsync();

            return View( boVoyageContext);
        }

        public FileResult TelechargerXML()
        {
            var listClients = _context.Personne.Where(p=>p.TypePers==2).ToList();
                Serialiser(listClients);

            string fileName = "Clients.xml";
            byte[] fileBytes = System.IO.File.ReadAllBytes($"wwwroot/XML/{fileName}");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        static void Serialiser(List<Personne> listClients)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Personne>),
                           new XmlRootAttribute("Clients"));

            using (var sw = new StreamWriter(@"wwwroot\XML\Clients.xml"))
            {
                serializer.Serialize(sw, listClients);
            }

        }

        // GET: Office/Personnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }


        // GET: Office/Personnes/Edit/5
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

        // POST: Office/Personnes/Edit/5
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
                return RedirectToAction(nameof(Index));
            }
            return View(personne);
        }

        // GET: Office/Personnes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // POST: Office/Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personne = await _context.Personne.FindAsync(id);
            _context.Personne.Remove(personne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonneExists(int id)
        {
            return _context.Personne.Any(e => e.Id == id);
        }
    }
}
