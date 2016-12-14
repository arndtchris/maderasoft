using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Models.ViewModel
{
    public class AffectationServiceViewModel
    {
        public AffectationServiceDTO affectationService { get; set; }
        public List<SelectListItem> lesServices { get; set; }
        public List<SelectListItem> lesDroits { get; set; }

        public AffectationServiceViewModel()
        {
            affectationService = new AffectationServiceDTO();
            lesServices = new List<SelectListItem>();
            lesDroits = new List<SelectListItem>();
        }
    }
}