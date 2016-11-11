using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public List<AdresseDTO> lesAdresses { get; set; }
        public AdresseDTO nouvelleAdresse { get; set; }

        public AdresseViewModel()
        {
            lesAdresses = new List<AdresseDTO>();
            nouvelleAdresse = new AdresseDTO();
        }
    }
}