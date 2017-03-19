using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MaderaSoft.Areas.Simulateur.Models.DTOs;
using MaderaSoft.Areas.ServiceCommercial.Models.ViewModels;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using Madera.Service;
using Madera.Model;
using AutoMapper;

namespace MaderaSoft.Areas.ServiceCommercial.Controllers
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
        /*public ActionResult Edit()
        {
            return View();

        }*/

        [HttpPost]
        //public ActionResult SavePlan()
        public ActionResult SavePlan(int id, PlanDTO plan)
        {
            int idModule = 0;

            if(id != 0)
            {

            }else
            {
                if (plan != null)
                {

                    plan.nom = "test";

                    Plan planP = new Plan();
                    planP = Mapper.Map<PlanDTO, Plan>(plan);

                    foreach (Etage e in planP.listEtages)
                    {
                        foreach (PositionModule p in e.listPositionModule)
                        {
                            idModule = p.module.id;

                            p.module = new Module();
                            p.module = _moduleService.Get(idModule);
                        }

                        idModule = 0;
                    }

                    try
                    {
                        _planService.Create(planP);
                        _planService.Save();

                    }
                    catch (Exception e)
                    {
                        throw (e);
                    }

                    return Json("Success");
                }
                else
                {
                    //return RedirectToAction("Index");
                    return Json("An Error Has occoured");
                }
            }

            return Json("Success");
            
        }

        [HttpPost]
        public ActionResult GetPlan(int id)
        {
            try
            {

                PlanViewModel view = new PlanViewModel();

                Plan p = _planService.Get(id);

                //Création du nouveau plan
                Plan planReturn = new Plan();

                //Ajout des informations du plan par rapport à l'ancien
                planReturn.largeur = p.largeur;
                planReturn.longueur = p.longueur;
                planReturn.nom = p.nom;
                planReturn.id = p.id;

                int largeur = p.largeur;
                int longueur = p.longueur;

                //Création des listes de modules et étages pour le nouveau objet Plan
               
                List<Etage> listEtagesReturn = new List<Etage>();


                //Calcul nombre de trait dans la grille (ex: larg:3 long:7 = trait:52)
                int total = ( 1 + planReturn.largeur) * planReturn.longueur + ( 1 + planReturn.longueur) * planReturn.largeur;

                //Calcul nombre de trait horizontal
                int totalHorizontal = (1 + planReturn.largeur) * planReturn.longueur;
                //Calcul nombre de trait vertical
                int totalVerticale = (1 + planReturn.longueur) * planReturn.largeur;

                // intialisation valeur de départ
                int xGrille = 10;
                int yGrille = 10;

                int numEtage = 1;
                
                // boucle sur les étages du plan
                foreach (Etage etage in p.listEtages)
                {
                    //Création de l'objet Étage
                    Etage etageTemp = new Etage();
                    etageTemp.id = etage.id;
                    List<PositionModule> listPositionModuleReturn = new List<PositionModule>();
                    int taille = largeur + 1;
                    for (int i = 0; i < taille; i++)
                    {
                        for (int j = 0; j < longueur; j++)
                        {
                            PositionModule pm = new PositionModule();

                            int xAfter = xGrille + 40;
                            xGrille = xGrille + 1;

                            pm = etage.listPositionModule.FirstOrDefault(x => x.x1 == xGrille && x.x2 == xAfter && x.y1 == yGrille && x.y2 == yGrille);

                            if (pm == null)
                            {
                                pm = new PositionModule();
                                pm.x1 = xGrille;
                                pm.x2 = xAfter;
                                pm.y1 = yGrille;
                                pm.y2 = yGrille;
                                pm.lineId = "lineLong" + i + j + numEtage;
                                pm.module = null;
                                listPositionModuleReturn.Add(pm);
                            }
                            else
                            {
                                listPositionModuleReturn.Add(pm);
                            }

                            xGrille = xAfter;
                        }
                        yGrille = yGrille + 40;
                        xGrille = 10;
                    }

                    xGrille = 10;
                    yGrille = 10;
                    taille = longueur + 1;
                    for (int i = 0; i < taille; i++)
                    {
                        for (int j = 0; j < largeur; j++)
                        {

                            //Création de l'objet PositionModule
                            PositionModule pm = new PositionModule();

                            int yAfter = yGrille + 40;
                            yGrille = yGrille + 1;

                            pm = etage.listPositionModule.FirstOrDefault(x => x.x1 == xGrille && x.x2 == xGrille && x.y1 == yGrille && x.y2 == yAfter);

                            if (pm == null)
                            {
                                pm = new PositionModule();
                                pm.x1 = xGrille;
                                pm.x2 = xGrille;
                                pm.y1 = yGrille;
                                pm.y2 = yAfter;
                                pm.lineId = "lineLarg" +j+i+numEtage;
                                pm.module = null;
                                listPositionModuleReturn.Add(pm);
                            }
                            else
                            {
                                //Ajout dans l'objet PositionModule dans la listPositionModule
                                listPositionModuleReturn.Add(pm);
                            }
                            yGrille = yAfter;
                        }
                        xGrille = xGrille + 40;
                        yGrille = 10;
                    }

                    numEtage++;

                    xGrille = 10;
                    yGrille = 10;

                    //Ajout de la liste des modules dans l'objet Étage
                    etageTemp.listPositionModule = listPositionModuleReturn;
                    //Ajout de l'objet Étage dans la liste des étages
                    listEtagesReturn.Add(etageTemp);

                }
                //Ajout des étages dans le plan
                planReturn.listEtages = listEtagesReturn;

                
                view.plan = planReturn;
                view.lesModules = Mapper.Map<List<Module>, List<ModuleDTO>>(_moduleService.DonneTous().ToList());

                return PartialView("~/Areas/ServiceCommercial/Views/Maison/_AffichePlan.cshtml", view);
                //return Json(planReturn);
            }
            catch(Exception e)
            {
                return Json("An Error Has occoured");
            }
            
            
        }

    }
}