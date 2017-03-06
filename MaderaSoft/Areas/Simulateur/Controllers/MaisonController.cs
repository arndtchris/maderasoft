using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MaderaSoft.Areas.Simulateur.Models.DTOs;
using MaderaSoft.Areas.Simulateur.Models.ViewModels;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using Madera.Service;
using Madera.Model;
using AutoMapper;

namespace MaderaSoft.Areas.Simulateur.Controllers
{
    public class MaisonController : Controller
    {
        private readonly IPlanService _planService;

        public MaisonController(IPlanService planService) {

            _planService = planService;

        }

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

        [HttpGet]
        public ActionResult SavePlan()
        {
            PlanDTO plan = new PlanDTO();
            List<PositionModuleDTO> pModule = new List<PositionModuleDTO>();
            ModuleDTO mod = new ModuleDTO();
            mod.id = 1;
            pModule.Add(new PositionModuleDTO
            {
                lineId = 1,
                x1 = 1,
                x2 = 2,
                y1 = 1,
                y2 = 2,
                module = mod
            });

            if (plan != null)
            {
                plan.largeur = 2;
                plan.largeur = 2;
                plan.nom = "test";
                plan.lesEtages.Add(new EtageDTO{
                    lesModules = pModule
                });

                Plan planP = new Plan();
                planP=Mapper.Map<PlanDTO, Plan>(plan);
                try
                {
                    _planService.Create(planP);
                    _planService.Save();

                    int i = 0;
                }
                catch(Exception e)
                {

                }

                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }

    }
}