﻿using System;
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
using Vereyon.Web;

namespace MaderaSoft.Areas.ServiceCommercial.Controllers
{
    public class MaisonController : Controller
    {
        private readonly IPlanService _planService;
        private readonly IModuleService _moduleService;
        private readonly IEtageService _etageService;
        private readonly string _service;
        private readonly IApplicationTraceService _traceService;


        public MaisonController(IPlanService planService, IModuleService moduleService, IEtageService etageService, IApplicationTraceService traceService) {

            _planService = planService;
            _moduleService = moduleService;
            _etageService = etageService;
            _service = "Service commercial";
            _traceService = traceService;


        }

        // GET: Simulateur/Maison
        public ActionResult Index()
        {
            Session["service"] = _service;

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
        public ActionResult SavePlan(PlanDTO plan)
        {
            int idModule = 0;
           // List<Etage> etages = new List<Etage>();

            if(plan.id != 0)
            {

                PlanViewModel view = new PlanViewModel();

                Plan p = Mapper.Map<PlanDTO, Plan>(plan);

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
                int total = (1 + planReturn.largeur) * planReturn.longueur + (1 + planReturn.longueur) * planReturn.largeur;

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
                                pm.lineId = "lineLarg" + j + i + numEtage;
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

                FlashMessage.Confirmation("Mise à jour réussie");

                _traceService.create(new ApplicationTrace
                {
                    action = Parametres.Action.Modification.ToString(),
                    description = String.Format("Mise à jour du plan_id={0}", view.plan.id),
                    utilisateur = _donneNomPrenomUtilisateur()
                });

                return PartialView("~/Areas/ServiceCommercial/Views/Maison/_AffichePlan.cshtml", view);

            }
            else
            {
                if (plan != null)
                {

                    plan.nom = "test";

                    Plan planP = new Plan();
                    planP = Mapper.Map<PlanDTO, Plan>(plan);
                    //plan = new Plan();

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
                        _planService.Create(planP, _donneNomPrenomUtilisateur());
                        _planService.Save();

                        PlanViewModel view = new PlanViewModel();

                        Plan p = _planService.DonneTous().ToList().OrderByDescending(x => x.id).First();

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
                        int total = (1 + planReturn.largeur) * planReturn.longueur + (1 + planReturn.longueur) * planReturn.largeur;

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
                                        pm.lineId = "lineLarg" + j + i + numEtage;
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

                        FlashMessage.Confirmation("Sauvegarde réussie");
                        _traceService.create(new ApplicationTrace
                        {
                            action = Parametres.Action.Creation.ToString(),
                            description = String.Format("Création du plan_id={0}", view.plan.id),
                            utilisateur = _donneNomPrenomUtilisateur()
                        });
                        return PartialView("~/Areas/ServiceCommercial/Views/Maison/_AffichePlan.cshtml", view);
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

        private void _addOrUpdatePositionModule(ref Plan planOrigine, PositionModuleDTO position)
        {
            if(position.id != 0)//mise à jour
            {
                foreach(Etage et in planOrigine.listEtages)
                {
                    if(et.listPositionModule.FirstOrDefault(x => x.id == position.id && x.etage.id == position.etage.id) != null)
                    {
                        et.listPositionModule.First(x => x.id == position.id && x.etage.id == position.etage.id).module = _moduleService.Get(position.module.id);
                        et.listPositionModule.First(x => x.id == position.id && x.etage.id == position.etage.id).etage = _etageService.Get(position.etage.id);
                    }
                }
            }
            else//ajout
            {
                PositionModule newPos = new PositionModule();
                newPos.etage = _etageService.Get(position.etage.id);
                newPos.module = _moduleService.Get(position.module.id);
                newPos.x1 = position.x1;
                newPos.x2 = position.x2;
                newPos.y2 = position.y2;
                newPos.y1 = position.y1;
                newPos.lineId = position.lineId;

                if(planOrigine.listEtages.FirstOrDefault(x => x.id == newPos.etage.id) != null)
                    planOrigine.listEtages.First(x => x.id == newPos.etage.id).listPositionModule.Add(newPos);


            }
        }

        private string _donneNomPrenomUtilisateur()
        {
            EmployeDTO emp = (EmployeDTO)HttpContext.Session["utilisateur"];

            if (emp != null)
                return string.Format("{0} {1}", emp.nom.ToUpperFirst(), emp.prenom.ToUpperFirst());
            else
                return "";

        }
    }
}