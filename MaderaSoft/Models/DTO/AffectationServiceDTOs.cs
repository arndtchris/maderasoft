using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class AffectationServiceDTO
    {
        public int id { get; set; }

        [DisplayName("Service")]
        [Required(ErrorMessage = "Veuillez renseigner un service d'affectation")]
        public virtual ServiceDTO service { get; set; }
        [DisplayName("Groupe utilisateur")]
        [Required(ErrorMessage = "Veuillez renseigner un groupe utilisateur")]
        public virtual DroitDTO groupe { get; set; }
        [DisplayName("Affectation principale")]
        public Boolean isPrincipal { get; set; }

        public AffectationServiceDTO()
        {
            service = new ServiceDTO();
            groupe = new DroitDTO();
        }

        public string affectationPrincipaleOuiNon()
        {
            if (isPrincipal)
                return "Oui";
            else
                return "Non";
        }
    }
}