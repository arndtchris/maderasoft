using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Models.ViewModel
{
    public class UtilisateurIndexViewModel
    {
        public BootstrapTableViewModel tableauAdresses { get; set; }

        public UtilisateurDTO nouvelUtilisateur { get; set; }

        public UtilisateurIndexViewModel()
        {
            nouvelUtilisateur = new UtilisateurDTO();
            tableauAdresses = new BootstrapTableViewModel();
        }
    }
}