using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class UtilisateurDTO
    {
        public string login { get; set; }
        public string password { get; set; }
        public DateTime dCreation { get; set; }
        public DateTime dConnexion { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

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
    }
}