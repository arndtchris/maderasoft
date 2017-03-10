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
        private readonly IModuleService _moduleService;

        public MaisonController(IPlanService planService, IModuleService moduleService) {

            _planService = planService;
            _moduleService = moduleService;

        }

        // GET: Simulateur/Maison
        public ActionResult Index()
        {

            IndexViewModel view = new IndexViewModel();
            view.lesPlans = _donnePlanBDD();
            view.PlanViewModel.lesModules = Mapper.Map<List<Module>, List<ModuleDTO>>(_moduleService.DonneTous().ToList());

            return View(view);
        }

        /// <summary>
        /// Donne la liste des groupes utilisateur disponibles en base de données
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> _donnePlanBDD()
        {
            List<SelectListItem> lesPlans = new List<SelectListItem>();

            //On récupère la liste des services disponibles dans l'application
            lesPlans = _planService.DonneTous().Select(
                x => new SelectListItem()
                {
                    Text = x.id.ToString(),
                    Value = x.id.ToString()
                }
                ).ToList();
            lesPlans.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            return lesPlans;
        }

        // POST: Simulateur/Maison
        public ActionResult Edit()
        {
            return View();

        }

        [HttpPost]
        //public ActionResult SavePlan()
        public ActionResult SavePlan(PlanDTO plan)
        {
           /* PlanDTO plan = new PlanDTO();
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
            });*/

            if (plan != null)
            {
               /* plan.largeur = 2;
                plan.largeur = 2;
                plan.nom = "test";
                plan.lesEtages.Add(new EtageDTO{
                    lesModules = pModule
                });*/

                Plan planP = new Plan();
                plan.nom = "test";
                planP=Mapper.Map<PlanDTO, Plan>(plan);
                try
                {
                    _planService.Create(planP);
                    _planService.Save();

                    //int i = 0;
                }
                catch(Exception e)
                {
                    throw (e);
                }
               // return RedirectToAction("Index");

                return Json("Success");
            }
            else
            {
                //return RedirectToAction("Index");
                return Json("An Error Has occoured");
            }
        }

        [HttpPost]
        public JsonResult GetPlan(int id)
        {
            try
            {
                List<ModuleDTO> lesModules = Mapper.Map<List<Module>, List<ModuleDTO>>(_moduleService.DonneTous().ToList());
                PlanDTO plan = Mapper.Map<Plan, PlanDTO>(_planService.Get(id));

                return Json(plan);
            }
            catch(Exception e)
            {
                return Json("An Error Has occoured");
            }
            
            
        }

    }
}