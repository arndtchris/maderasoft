using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.ViewModel
{
    public class BootstrapDeleteModalViewModel
    {
        public int idToDelete { get; set; }

        public string typeObjet { get; set; }

        public string message { get; set; }

        public BootstrapDeleteModalViewModel()
        {

        }
    }

    public class BootstrapModalViewModel
    {
        public string formulaireUrl { get; set; }

        public Object objet { get; set; }

        public int idToDelete { get; set; }

        public string typeObjet { get; set; }

        public string message { get; set; }

        public string titreModal { get; set; }

        public BootstrapModalViewModel()
        {
            objet = new object();
        }

    }

    public class BootstrapTableViewModel
    {
        public List<List<string>> lesLignes { get; set; }
        public bool avecActionCrud { get; set; }
        public string typeObjet { get; set; }

        public BootstrapTableViewModel()
        {
            lesLignes = new List<List<string>>();

            //ToDo : gérer ce boolean en fonction des droits de l'utilisateur connecté
            avecActionCrud = true;
        }
    }
}