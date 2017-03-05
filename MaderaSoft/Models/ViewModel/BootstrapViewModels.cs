using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Service;

namespace MaderaSoft.Models.ViewModel
{
    public class BootstrapDeleteModalViewModel
    {
        public int idToDelete { get; set; }

        public string message { get; set; }

        public string urlController { get; set; }

        public string method { get; set; }

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
        public List<List<object>> lesLignes { get; set; }
        public bool avecActionCrud { get; set; }
        public string typeObjet { get; set; }
        public string messageSiVide { get; set; }

        public BootstrapTableViewModel()
        {
            lesLignes = new List<List<object>>();
            messageSiVide = "";

            //ToDo : gérer ce boolean en fonction des droits de l'utilisateur connecté
            avecActionCrud = true;
        }
    }

    public class BootstrapButtonViewModel
    {
        public string href { get; set; }
        public Parametres.TypeBouton typeDeBouton { get; set; }
        public string libe { get; set; }
        public string cssClass { get; set; }
        public string tooltip { get; set; }
    }
}