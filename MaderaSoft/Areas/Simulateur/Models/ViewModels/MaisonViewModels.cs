using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Madera.Model;
//using MaderaSoft.Areas.Simulateur.Models.DTOs;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;

namespace MaderaSoft.Areas.Simulateur.Models.ViewModels
{
    public class IndexViewModel
    {
        public PlanDTO plan {get; set;}
        public List<SelectListItem> lesPlans { get; set; }

        public PlanViewModel PlanViewModel { get; set; }
        //public ChargePlanViewModel ChargePlanViewModel { get; set; }

        public IndexViewModel()
        {
            plan = new PlanDTO();
            lesPlans = new List<SelectListItem>();
            PlanViewModel = new PlanViewModel();
            //ChargePlanViewModel = new ChargePlanViewModel();
        }
    }

    public class PlanViewModel
    {
        public List<ModuleDTO> lesModules { get; set; }
        public Plan plan { get; set; }

        public PlanViewModel()
        {
            plan = new Plan();
            lesModules = new List<ModuleDTO>();
        }
    }

    /*public class ChargePlanViewModel
    {
        public Plan plan { get; set; }

        public ChargePlanViewModel()
        {
            plan = new Plan();
        }
    }*/

}