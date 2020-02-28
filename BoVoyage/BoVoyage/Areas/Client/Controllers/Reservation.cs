using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Areas.Client.Models;
using BoVoyage.Models;
using BoVoyage.Controllers;

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
            
            //Prix de base pour l'utilisateur 
            double prixTva = (double)voyage.PrixHt;

            //On s'assure que la liste de voyageur en session est déjà instancié
            List<Personne> voyageurs = HttpContext.Session.Get<List<Personne>>("voyageurs") == null ? new List<Personne>() : HttpContext.Session.Get<List<Personne>>("voyageurs");
            
            List<double> prixParVoyageur = PrixParVoyageur((double)voyage.PrixHt, voyageurs);

            if (nbPersonnes > voyageurs.Count())
            {
                voyageurs.Add(new Personne());
            }

            foreach (var item in prixParVoyageur)
            {
                prixTva += item; 
                Tva(prixTva); 
            }
           

            ViewBag.Utilisateur = await _context.Personne.Where(p => p.Id == 4).FirstOrDefaultAsync();
            ViewBag.PrixParVoyageur = prixParVoyageur;
            ViewBag.PrixTva = prixTva;

            var voyagePersonnes = new VoyagePersonnesViewModel(voyage, voyageurs);

            return View(voyagePersonnes);
        }

        public IActionResult AjoutVoyageur(VoyagePersonnesViewModel voyagePersonnes, int idVoyage, int nbPersonnes)
        {
            //On récupère le voyage en cours
            var voyage = _context.Voyage.Where(v => v.Id == idVoyage).Include(v => v.IdDestinationNavigation).FirstOrDefault();
            voyagePersonnes.Voyage = voyage;

            //On récupère l'utilisateur
            ViewBag.Utilisateur = _context.Personne.Where(p => p.Id == 4).FirstOrDefault();

            //Prix de base pour l'utilisateur 
            double prixTva = (double)voyage.PrixHt;

            List<double> prixParVoyageur = PrixParVoyageur((double)voyage.PrixHt, voyagePersonnes.Voyageurs);

            foreach (var item in prixParVoyageur)
            {
                prixTva += item;
                Tva(prixTva);
            }
            
            ViewBag.PrixParVoyageur = prixParVoyageur;
            ViewBag.PrixTva = prixTva;

            if (ModelState.IsValid)
            {
                //On stock en session si le voyageur est valide
                HttpContext.Session.Set("voyageurs", voyagePersonnes.Voyageurs);
                return RedirectToAction(nameof(Index), new { idVoyage, nbPersonnes });
            }
            return View("Index", voyagePersonnes);
        }

        public IActionResult SupprimerVoyageur(int idVoyageur, int idVoyage, int nbPersonnes)
        {           

            var voyageurs = HttpContext.Session.Get<List<Personne>>("voyageurs");
            voyageurs.RemoveAt(idVoyageur);
            HttpContext.Session.Set("voyageurs", voyageurs);
            return RedirectToAction(nameof(Index), new { idVoyage, nbPersonnes = nbPersonnes-1 });
        }


        public List<double> PrixParVoyageur(double prixVoyage, List<Personne> voyageurs)
        {
            List<double> prixParVoyageur = new List<double>();

            foreach (var item in voyageurs)
            {
                if (item.Email != null)
                {

                    //Reduc de 60% pour - 12ans
                    if (item.Datenaissance > DateTime.Now.AddYears(-12))
                    {
                        prixParVoyageur.Add(prixVoyage * 0.4);
                    }
                    else
                    {
                        prixParVoyageur.Add(prixVoyage);
                    }
                }
            }
            return prixParVoyageur;
        }

        public double Tva(double totalHt)
        {
            return totalHt * 0.2;
        }

    }
}
