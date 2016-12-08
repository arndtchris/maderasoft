using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Model;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Areas.RessourcesHumaines.Models.DTOs
{
    public class EmployeDTO
    {
        public int id { get; set; }
        public Boolean isDeleted { get; set; }
        public virtual TEmploye typeEmploye { get; set; }
        public virtual List<AffectationService> affectationServices { get; set; }
        public virtual PersonneDTO personne { get; set; }

        public EmployeDTO()
        {
            typeEmploye = new TEmploye();
            affectationServices = new List<AffectationService>();
            personne = new PersonneDTO();
        }
    }
}