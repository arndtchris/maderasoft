using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class EmployeDTO
    {
        public int id { get; set; }
        public Boolean isDeleted { get; set; }

        [DisplayName("Statut")]
        //public int typeEmployeId { get; set; }
        public virtual TEmployeDTO typeEmploye { get; set; }

        [DisplayName("Service")]
        [Required(ErrorMessage = "Veuillez renseigner un service pour effectuer l'affectation")]
        public int serviceIdPourAffectation { get; set; }

        [DisplayName("Groupe utilisateur")]
        [Required(ErrorMessage = "Veuillez renseigner un groupe utilisateur pour cette affectation")]
        public int groupeIdPourAffectation { get; set; }

        [DisplayName("Affectation principale")]
        public bool isAffecttionPrincipal { get; set; }
        public virtual List<AffectationServiceDTO> affectationServices { get; set; }

        public EmployeDTO()
        {
            typeEmploye = new TEmployeDTO();
            affectationServices = new List<AffectationServiceDTO>();
        }
    }
}