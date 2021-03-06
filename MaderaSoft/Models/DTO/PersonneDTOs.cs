﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaderaSoft.Models.DTO
{
    /// <summary>
    /// DTO simple de la table PersonneDTO
    /// </summary>
    public class PersonneDTO
    {
        public int id { get; set; }

        public bool isDeleted { get; set; }

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
        public UtilisateurDTO utilisateur { get; set; }

        public string getCiv()
        {
            if (this.civ != null)
            {
                if (this.civ == "1")
                {
                    return "Madame";
                }
                else
                {
                    return "Monsieur";
                }
            }
            else
            {
                return " ";
            }
        }
        public PersonneDTO()
        {
            adresse = new AdresseDTO();
            utilisateur = new UtilisateurDTO();
        }
    }

    public class PEmployeTableauDTO
    {
        public int id { get; set; }
        public string civ { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public AdresseDTO adresse { get; set; }

        public string getCiv()
        {
            if (this.civ != null)
            {
                if (this.civ == "1")
                {
                    return "Mme";
                }
                else
                {
                    return "M";
                }
            }
            else
            {
                return " ";
            }
        }
        public PEmployeTableauDTO()
        {
            adresse = new AdresseDTO();
        }
    }

    public class PersonneSimpleDTO
    {
        public int id { get; set; }

        public bool isDeleted { get; set; }

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
    }
}