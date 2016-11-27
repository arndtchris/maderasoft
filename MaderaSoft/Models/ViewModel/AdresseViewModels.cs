using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Models.ViewModel
{
    public class AdresseIndexViewModel
    {

        public BootstrapTableViewModel tableauAdresses { get; set; }

        public AdresseDTO nouvelleAdresse { get; set; }

        public AdresseIndexViewModel()
        {
            nouvelleAdresse = new AdresseDTO();
            tableauAdresses = new BootstrapTableViewModel();
        }
    }
}