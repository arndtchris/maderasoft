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
        //public int id { get; set; }

        //public bool isDeleted { get; set; }

        [DisplayName("Affectation principale")]
        public bool isAffecttionPrincipal { get; set; }

        [DisplayName("Statut")]
        public virtual TEmployeDTO typeEmploye { get; set; }

        [DisplayName("Service")]
        public virtual int serviceIdPourAffectation { get; set; }

        [DisplayName("Groupe utilisateur")]
        public virtual int groupeIdPourAffectation { get; set; }

        public virtual List<AffectationServiceDTO> affectationServices { get; set; }

        public EmployeDTO()
        {
            affectationServices = new List<AffectationServiceDTO>();
            typeEmploye = new TEmployeDTO();
        }

    }

    /// <summary>
    /// Reprend les attributs avec des règles de validation spécifiques à la création d'un employé
    /// </summary>
    public class CreateEmployeDTO : EmployeDTO
    {

        [DisplayName("Statut")]
        [Required(ErrorMessage = "Veuillez renseigner un type d'employé")]
        public override TEmployeDTO typeEmploye { get; set; }

        [DisplayName("Service")]
        [Required(ErrorMessage = "Veuillez renseigner un service pour effectuer l'affectation")]
        public override int serviceIdPourAffectation { get; set; }

        [DisplayName("Groupe utilisateur")]
        [Required(ErrorMessage = "Veuillez renseigner un groupe utilisateur pour cette affectation")]
        public override int groupeIdPourAffectation { get; set; }

        public CreateEmployeDTO()
        {
            //typeEmploye = new TEmployeDTO();
        }
    }

    /// <summary>
    /// Reprend les attributs avec des règles de validation spécifiques à la modification d'un employé
    /// </summary>
    public class EditEmployeDTO : EmployeDTO
    {
        [DisplayName("Statut")]
        public override TEmployeDTO typeEmploye { get; set; }

        [DisplayName("Service")]
        public override int serviceIdPourAffectation { get; set; }

        [DisplayName("Groupe utilisateur")]
        public override int groupeIdPourAffectation { get; set; }

        public EditEmployeDTO()
        {
            //typeEmploye = new TEmployeDTO();
        }
    }
}