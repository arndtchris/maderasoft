using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Madera.Model;
using MaderaSoft.Models.ViewModel;

namespace MaderaSoft.Models.DTO
{
    public class DevisFactureDTO
    {

            [DisplayName("Numéro Devis")]
            public int id { get; set; }
            [DisplayName("Devis signé")]
            public Boolean isSigned { get; set; }
            [DisplayName("Devis supprimé")]
            public Boolean isDeleted { get; set; }
            [DisplayName("Numéro projet")]
            public virtual Projet projet { get; set; }
            [DisplayName("Référent")]
            public virtual Employe employe { get; set; }

        public DevisFactureDTO()
        {
        }
    }
}