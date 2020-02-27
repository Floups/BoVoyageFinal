using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoVoyage.Models
{
    public class ContactViewModel
    {
        [Required (ErrorMessage = "Champ requis."), 
            Display(Name ="Civilité")]
        public string Civilite { get; set; }
        
        [Required(ErrorMessage = "Champ requis."), MaxLength(50)]
        public string Nom { get; set; }
        
        [Required(ErrorMessage = "Champ requis."), MaxLength(50), Display(Name = "Prénom")]
        public string Prenom { get; set; }
       
        [Required(ErrorMessage = "Champ requis."),
            RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage ="Format de l'adresse e-mail incorrecte."),
            Display(Name = "Adresse e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Champ requis."), 
            Phone(ErrorMessage = "Format du numéro de téléphone incorrect."),
            Display(Name = "Téléphone")]
        public string Telephone { get; set; }
        
        [Required(ErrorMessage = "Champ requis."),
            Display(Name ="Sujet"), MaxLength(50)]
        public string SujetMessage { get; set; }
        [Required(ErrorMessage = "Champ requis."),
            MaxLength(300)]
        public string Message { get; set; }


    }
}
