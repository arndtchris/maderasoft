using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    /// <summary>
    /// DTO simple de la table Employe
    /// </summary>
    public class EmployeDTO : PersonneDTO
    {

        [DisplayName("Statut")]
        public virtual TEmployeDTO typeEmploye { get; set; }

        public virtual List<AffectationServiceDTO> affectationServices { get; set; }

        public EmployeDTO()
        {
            affectationServices = new List<AffectationServiceDTO>();
            typeEmploye = new TEmployeDTO();
        }

    }

    /// <summary>
    /// Reprend les attributs avec des règles de validation spécifiques à la création/modification d'un employé
    /// </summary>
    public class EditEmployeDTO : EmployeDTO
    {
        [DisplayName("Affectation principale")]
        public bool isAffecttionPrincipal { get; set; }

        [DisplayName("Service")]
        [Required(ErrorMessage = "Veuillez renseigner un service pour effectuer l'affectation")]
        public int serviceIdPourAffectation { get; set; }

        [DisplayName("Groupe utilisateur")]
        [Required(ErrorMessage = "Veuillez renseigner un groupe utilisateur pour cette affectation")]
        public int groupeIdPourAffectation { get; set; }

        public EditEmployeDTO()
        {

        }
    }
}