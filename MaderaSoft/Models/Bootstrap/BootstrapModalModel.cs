using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace MaderaSoft.Models.Bootstrap
{
    public class BootstrapModalModel
    {
        public string formulaireUrl { get; set; }

        public Object objet { get; set; }

        public int idToDelete { get; set; }

        public string typeObjet { get; set; }

        //public string typeObjet { get; set; }

        public string message { get; set; }

        public string titreModal { get; set; }

        public BootstrapModalModel()
        {
            objet = new object();
        }

    }
}