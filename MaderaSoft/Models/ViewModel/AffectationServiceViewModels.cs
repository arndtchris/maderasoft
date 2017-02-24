using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Models.ViewModel
{
    public class CardAffectationServiceViewModel
    {
        public List<SelectListItem> lesServices { get; set; }

        public List<SelectListItem> lesDroits { get; set; }

        public NouvelleAffectationDTO nouvelleAffectation { get; set; }

        public List<AffectationServiceDTO> tableauAffectations { get; set; }

        public CardAffectationServiceViewModel()
        {
            lesServices = new List<SelectListItem>();
            lesDroits = new List<SelectListItem>();
            nouvelleAffectation = new NouvelleAffectationDTO();
            tableauAffectations = new List<AffectationServiceDTO>();
        }
    }
}