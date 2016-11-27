using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class AdresseDTO
    {

        public int AdresseID { get; set; }
        [DisplayName("Numéro du logement")]
        [Required(ErrorMessage = "Le numéro du logement est obligatoire.")]
        public string numRue { get; set; }
        [DisplayName("Nom de la rue")]
        [Required(ErrorMessage = "Le nom de la rue est obligatoire.")]
        public string nomRue { get; set; }
        [DisplayName("Code postal")]
        [Required(ErrorMessage = "Le code postal est obligatoire.")]
        [RegularExpression("^(F-)?((2[A|B])|[0-9]{2})[0-9]{3}$", ErrorMessage = "Le format du code postal n'est pas valide")]
        public string codePostal { get; set; }
        [DisplayName("Ville")]
        [Required(ErrorMessage = "La ville est obligatoire.")]
        public string ville { get; set; }
        [DisplayName("Pays")]
        [Required(ErrorMessage = "Le pays est obligatoire.")]
        public string pays { get; set; }

        public AdresseDTO()
        {

        }
    }
}