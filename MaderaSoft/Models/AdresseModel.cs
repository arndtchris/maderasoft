using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaderaSoft.Models.Bootstrap;

namespace MaderaSoft.Models
{
    public class AdresseDTO
    {
        public int AdresseID { get; set; }
        public string numRue { get; set; }
        public string nomRue { get; set; }
        public string codePostal { get; set; }
        public string ville { get; set; }
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