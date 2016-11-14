using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using MaderaSoft.Models.Bootstrap;

namespace MaderaSoft.Models
{
    public class AdresseDTO
    {
        public int AdresseID { get; set; }
        [DisplayName("Numéro")]
        public string numRue { get; set; }
        [DisplayName("Nom")]
        public string nomRue { get; set; }
        [DisplayName("Code postal")]
        public string codePostal { get; set; }
        [DisplayName("Ville")]
        public string ville { get; set; }
        [DisplayName("Pays")]
        public string pays { get; set; }

        public AdresseDTO()
        {
            
        }
    }

    public class AdresseViewModel
    {

        public BootstrapTableModel tableauAdresses { get; set; }

        public AdresseDTO nouvelleAdresse { get; set; }

        public AdresseViewModel()
        {
            nouvelleAdresse = new AdresseDTO();
            tableauAdresses = new BootstrapTableModel();
        }
    }
}