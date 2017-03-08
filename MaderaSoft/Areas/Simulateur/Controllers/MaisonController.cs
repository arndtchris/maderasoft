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

            //BootstrapTableViewModel modelOut = new BootstrapTableViewModel();
            List<ModuleDTO> lesModules = Mapper.Map<List<Module>, List<ModuleDTO>>(_moduleService.DonneTous().ToList());



            int i = 0;
           /* modelOut.avecActionCrud = false;
            modelOut.messageSiVide = "L'application ne contient pas encore de modules.";
            modelOut.lesLignes.Add(new List<object> { "Utilisateur", "Date", "Action", "Description" });


            foreach (ApplicationTraceDTO appTraceDTO in lesTraces)
            {
                modelOut.lesLignes.Add(new List<object> { appTraceDTO.utilisateur, appTraceDTO.date.ToString(), appTraceDTO.action, appTraceDTO.description });
            }


            return View(modelOut);*/



            return View(lesModules);
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
                planP=Mapper.Map<PlanDTO, Plan>(plan);
                try
                {
                    _planService.Create(planP);
                    _planService.Save();

                    //int i = 0;
                }
                catch(Exception e)
                {

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

    }
}