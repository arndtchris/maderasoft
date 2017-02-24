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

        public virtual EmployeDTO employe { get; set; }

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
            employe = new EmployeDTO();
        }

        public string affectationPrincipaleOuiNon()
        {
            if (isPrincipal)
                return "Oui";
            else
                return "Non";
        }
    }

    public class NouvelleAffectationDTO
    {
        public int emplyeId { get; set; }

        [DisplayName("Affectation principale")]
        public bool isAffecttionPrincipal { get; set; }

        [DisplayName("Service")]
        [Required(ErrorMessage = "Veuillez renseigner un service pour effectuer l'affectation")]
        public int serviceIdPourAffectation { get; set; }

        [DisplayName("Groupe utilisateur")]
        [Required(ErrorMessage = "Veuillez renseigner un groupe utilisateur pour cette affectation")]
        public int groupeIdPourAffectation { get; set; }
    }
}