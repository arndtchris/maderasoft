using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class UtilisateurDTO
    {
        [DisplayName("id")]
        public int id { get; set; }

        [DisplayName("Identifiant")]
        public string login { get; set; }

        [DisplayName("Mot de passe")]
        public string password { get; set; }

        [DisplayName("Date de création")]
        public DateTime dCreation { get; set; }

        [DisplayName("Date de dernier connexion")]
        public DateTime? dConnexion { get; set; }

        [DisplayName("Archivé")]
        public bool isActive { get; set; }

        [DisplayName("Supprimé")]
        public bool isDeleted { get; set; }

        public bool isFirstConnexion { get; set; }

        public string utilisateurActif()
        {
            if (this.isActive)
                return "Actif";
            else
                return "Archivé";
        }

        public string utilisateurSupprime()
        {
            if (this.isDeleted)
                return "Supprimé";
            else
                return "";
        }

        public UtilisateurDTO()
        {

        }

        public UtilisateurDTO(UtilisateurDTO util)
        {
            this.dConnexion = util.dConnexion;
            this.id = util.id;
            this.dCreation = util.dCreation;
            this.isActive = util.isActive;
            this.isDeleted = util.isDeleted;
            this.login = util.login;
            this.password = util.password;
        }
    }

    public class UtilisateurLoginDTO
    {
        public string login { get; set; }
        public string password { get; set; }
    }

    public class PersoPwdDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Veuillez saisir un nouveau mot de passe")]
        public string pwd1 { get; set; }

        [Required(ErrorMessage = "Veuillez ressaisir votre nouveau mot de passe")]
        [Compare("pwd1", ErrorMessage ="Les mots de passe saisies sont différents")]
        public string pwd2 { get; set; }

    }
}