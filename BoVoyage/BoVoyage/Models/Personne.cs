using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace BoVoyage.Models
{
    [XmlRoot("Clients")]
    public partial class Personne
    {

        public Personne()
        {
            Voyageur = new HashSet<Voyageur>();
        }
        [XmlIgnore]
        public int Id { get; set; }
        /// <summary>
        /// 1: client (forcément utilisateur)
        /// 2: voyageur
        /// 3: contact
        /// 4: utilisateur
        /// </summary>

        [XmlIgnore]
        [Display (Name = "Catégorie de personne")]
        public byte TypePers { get; set; }

        [XmlIgnore]
        [Display(Name = "Civilité")]
        [Required (ErrorMessage ="Champ requis.")]
        public string Civilite { get; set; }

        [XmlAttribute]
        [Required(ErrorMessage = "Champ requis."),StringLength(50)]
        public string Nom { get; set; }

        [XmlAttribute]
        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Champ requis."), StringLength(50)]
        public string Prenom { get; set; }

        [XmlAttribute]
        [Display(Name = "Adresse e-mail")]
        [Required(ErrorMessage = "Champ requis."), EmailAddress (ErrorMessage ="Format incorrect.")]
        public string Email { get; set; }

        [XmlIgnore]
        [Display(Name = "Téléphone")]
        [Phone]
        public string Telephone { get; set; }
        
        [XmlIgnore]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Champ requis.")]
        public DateTime? Datenaissance { get; set; }
        
        [XmlIgnore]
        public virtual Client Client { get; set; }

        [XmlIgnore]
        public virtual ICollection<Voyageur> Voyageur { get; set; }
    }
}
