﻿using System;
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
        public byte TypePers { get; set; }
        [Required]
        public string Civilite { get; set; }
        [Required,StringLength(50)]
        public string Nom { get; set; }
        [Required, StringLength(50)]
        public string Prenom { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Telephone { get; set; }
        public DateTime? Datenaissance { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }
    }
}
