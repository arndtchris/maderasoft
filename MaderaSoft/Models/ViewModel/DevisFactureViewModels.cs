using MaderaSoft.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaderaSoft.Models.ViewModel
{
    public class DevisFactureIndexViewModel
    {
        public BootstrapTableViewModel tableauDevisFactures{ get; set; }

        public DevisFactureDTO nouveaudevis { get; set; }

        public DevisFactureIndexViewModel()
        {
            nouveaudevis = new DevisFactureDTO();
            tableauDevisFactures = new BootstrapTableViewModel();
        }
    }
}