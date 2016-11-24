using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MaderaSoft.Models.Bootstrap;
using Madera.Model;

namespace MaderaSoft.Models
{
    public class ManageQuoteViewModel
    {

        public class DevisFactureDTO
        {
            public int id { get; set; }
            [DisplayName("Numéro de devis")]
            public Boolean isSigned { get; set; }
            [DisplayName("Devis signé")]
            public Boolean isDeleted { get; set; }
            [DisplayName("Devis supprimé")]
            public virtual Projet projet { get; set; }
            [DisplayName("Numéro du projet")]
            public virtual Employe employe { get; set; }

    public DevisFactureDTO()
            {

            }
        }

        public BootstrapTableModel tableauQuote { get; set; }


        public ManageQuoteViewModel()
        {
            tableauQuote = new BootstrapTableModel();
        }
    }
}