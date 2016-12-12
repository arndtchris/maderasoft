using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Madera.Model;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Areas.RessourcesHumaines.Models.DTOs
{
    public class EmployeDTO
    {
        //public int id { get; set; }
        public Boolean isDeleted { get; set; }
        [DisplayName("Statut")]
        [Required(ErrorMessage = "Veuillez renseigner le statut de l'employé")]
        public virtual TEmployeDTO typeEmploye { get; set; }
        public virtual AffectationServiceDTO nouvelleAffectation { get; set; }
        public virtual List<AffectationServiceDTO> affectationServices { get; set; }
        //public virtual PersonneDTO personne { get; set; }

        public EmployeDTO()
        {
            typeEmploye = new TEmployeDTO();
            affectationServices = new List<AffectationServiceDTO>();
            //personne = new PersonneDTO();
        }
    }
}