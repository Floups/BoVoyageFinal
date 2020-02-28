using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyage.Models
{
    public partial class Personne
    {
        public Personne()
        {
            Voyageur = new HashSet<Voyageur>();
        }

        public int Id { get; set; }
        [Display (Name = "Catégorie de personne")]
        public byte TypePers { get; set; }

        [Display(Name = "Civilité")]
        [Required (ErrorMessage ="Champ requis.")]
        public string Civilite { get; set; }

        [Required(ErrorMessage = "Champ requis."),StringLength(50)]
        public string Nom { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Champ requis."), StringLength(50)]
        public string Prenom { get; set; }

        [Display(Name = "Adresse e-mail")]
        [Required(ErrorMessage = "Champ requis."), EmailAddress (ErrorMessage ="Format incorrect.")]
        public string Email { get; set; }

        [Display(Name = "Téléphone")]
        [Phone]
        public string Telephone { get; set; }

        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime? Datenaissance { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }
    }
}
