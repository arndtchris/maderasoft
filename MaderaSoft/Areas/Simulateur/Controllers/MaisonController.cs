using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MaderaSoft.Areas.Simulateur.Models.DTOs;
using MaderaSoft.Areas.Simulateur.Models.ViewModels;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;

namespace MaderaSoft.Areas.Simulateur.Controllers
{
    public class MaisonController : Controller
    {
        // GET: Simulateur/Maison
        public ActionResult Index()
        {
            return View();
        }

        // POST: Simulateur/Maison
        public ActionResult Edit()
        {
            return View();

        }

        [HttpPost]
        public ActionResult SavePlan(PlanDTO plan)
        {
            if (plan != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }

    }
}