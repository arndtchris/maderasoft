using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaderaSoft.Models.DTO
{
    public class PersonneDTO
    {
        public int id { get; set; }
        [DisplayName("Civilité")]
        [Required(ErrorMessage = "Veuillez renseigner une civilité")]
        public string civ { get; set; }
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Veuillez renseigner un nom")]
        public string nom { get; set; }
        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Veuillez renseigner un prénom")]
        public string prenom { get; set; }
        [DisplayName("Email")]
        //[RegularExpression("^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Cette adresse email n'est as valide")]
        public string email { get; set; }
        [DisplayName("Tel. portable")]
        public string tel1 { get; set; }
        [DisplayName("Tel. fixe")]
        public string tel2 { get; set; }
        public AdresseDTO adresse { get; set; }
        public List<SelectListItem> lesCivilites { get; set; }

        public PersonneDTO()
        {
            adresse = new AdresseDTO();
            lesCivilites = new List<SelectListItem> {
                new SelectListItem {Value = "",Text = "--- Sélectionnez ---" },
                new SelectListItem {Value = "1",Text = "Madame" },
                new SelectListItem {Value = "2",Text = "Monsieur" }
            };

        }
    }
}