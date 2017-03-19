using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Areas.RechercheDeveloppement.Models.ViewModels;
using MaderaSoft.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Areas.RechercheDeveloppement.Controllers
{
    public class ModuleController : Controller
    {
        // GET: GestionModule/Module

            private readonly IModuleService _moduleService;
            private readonly IServiceService _serviceService;
            private readonly ITModuleService _tmoduleService;
            private readonly IComposantService _composantService;
            private readonly ICompositionService _compositionService;
            private readonly string _service;


        public ModuleController(IModuleService moduleService, IServiceService serviceService, ITModuleService tmoduleService, IComposantService composantService, ICompositionService compositionService)
            {
                this._moduleService = moduleService;
                this._serviceService = serviceService;
                this._tmoduleService = tmoduleService;
                this._composantService = composantService;
                this._compositionService = compositionService;
                this._service = "Recherche et Développement";
            }

            // GET: GestionStock/Stocks
            public ActionResult Index()
            {
                ModuleIndexViewModel modelOut = new ModuleIndexViewModel();
                BootstrapButtonViewModel button = new BootstrapButtonViewModel();
                modelOut.tableauModules.typeObjet = "RechercheDeveloppement/Module";
                modelOut.tableauModules.avecActionCrud = true;
                modelOut.tableauModules.messageSiVide = "Aucun module n'a été ajouté à l'application.";

                List<ModuleDTO> lesModules = Mapper.Map<List<Module>, List<ModuleDTO>>(_moduleService.DonneTous().ToList());

                modelOut.tableauModules.lesLignes.Add(new List<object> { "", "Nom module", "Gamme", "Prix du module", "" });

                foreach (ModuleDTO module in lesModules)
                {
                    button = new BootstrapButtonViewModel
                    {
                        href = Url.Action("Detail", "Module", new { area = "RechercheDeveloppement", id = module.id }).ToString(),
                        cssClass = "",
                        libe = " ",
                        typeDeBouton = Parametres.TypeBouton.Detail
                    };
                    modelOut.tableauModules.lesLignes.Add(new List<object> { button, module.libe, module.typeModule.libe , module.prix.ToString(), module.id });
                }


                return View(modelOut);
            }

            [HttpGet]
            public ActionResult EditModal(int? id)
            {

                BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
                CreateModuleViewModel editModule = new CreateModuleViewModel();
                BootstrapButtonViewModel button = new BootstrapButtonViewModel();

            if (id.HasValue)
            {
                editModule.module = Mapper.Map<Module, ModuleDTO>(_moduleService.Get(id.Value));
                modelOut.titreModal = string.Format("Modification des informations du module");
            }
            else
            {
                modelOut.titreModal = string.Format("Ajout d'un module");
            }

            editModule.lesComposants = _donneListeComposants();
                editModule.lesGammes = _donneListeGammes();
                modelOut.formulaireUrl = "~/Areas/RechercheDeveloppement/Views/Module/_EditModulePartial.cshtml";
                
                modelOut.objet = editModule;

                return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

            }

        private List<SelectListItem> _donneListeComposants()
        {
            List<SelectListItem> lesComposants = _composantService.DonneTous().Select(
                    x => new SelectListItem()
                    {
                        Text = x.libe,
                        Value = x.id.ToString()
                    }
                ).ToList();
            lesComposants.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            return lesComposants;
        }
        private List<SelectListItem> _donneListeGammes()
        {
            List<SelectListItem> lesGammes = _tmoduleService.DonneTous().Select(
                    x => new SelectListItem()
                    {
                        Text = x.libe,
                        Value = x.id.ToString()
                    }
                ).ToList();
            lesGammes.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            return lesGammes;
        }

        [HttpPost]
            public ActionResult Edit(ModuleDTO module)
            {
                Module mdl = new Module();

            if (module.id != 0)//update
            {
                try
                {
                    
                    mdl = _moduleService.Get(module.id);
                    mdl.libe = module.libe;
                    mdl.prix = module.prix;
                    mdl.prix = Convert.ToDecimal(mdl.prix);
                    mdl.coupePrincipe = "string";
                    mdl.typeModule = _tmoduleService.Get(module.typeModule.id);
                    //mdl = Mapper.Map<ModuleDTO, Module>(module);
                    _moduleService.Update(mdl, _donneNomPrenomUtilisateur());

                    FlashMessage.Confirmation("Module mis à jour avec succès.");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de la mise à jour du module");
                }
            }
            else
                {
                    try
                    {
                    mdl = Mapper.Map<ModuleDTO, Module>(module);
                    
                    int test = mdl.typeModule.id;
                    string test2 = mdl.typeModule.libe;
                    int ind = mdl.id;

                    //On prépare le type de module
                    mdl.typeModule = _tmoduleService.Get(mdl.typeModule.id);
                    _moduleService.Create(mdl, _donneNomPrenomUtilisateur());

                        FlashMessage.Confirmation("Module créé avec succès. Pour ajouter des composants au module, cliquez sur Détail.");
                    }
                    catch (Exception e)
                    {
                        FlashMessage.Danger("Erreur lors de l'ajout du module");
                    }

                }
                _moduleService.Save();

                return RedirectToAction("Index");

            }

            [HttpGet]
            public ActionResult DeleteModal(int id)
            {
            string testid = id.ToString();
                BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
                modelOut.typeObjet = "RechercheDeveloppement/Module";
                modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
                modelOut.titreModal = "Suppression d'un module";
                modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer ce module ?", method = "Delete", urlController = "Module" };

                return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
            }

            [HttpPost]
            public ActionResult Delete(int idToDelete)
            {
                try
                {
                    FlashMessage.Confirmation("Suppression du module");
                    _moduleService.Delete(idToDelete, _donneNomPrenomUtilisateur());
                    _moduleService.Save();
                }
                catch (Exception)
                {
                    FlashMessage.Danger("Erreur lors de la suppression du module");
                    throw;
                }

                return RedirectToAction("Index");
            }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            //On renseigne le service courant pour adapater l'IHM en fonction des droits de l'utilisateur connecté
            Session["service"] = _service;

            DetailIndexViewModel modelOut = new DetailIndexViewModel();
            ModuleDTO mod = Mapper.Map<Module, ModuleDTO>(_moduleService.Get(id));

            #region préparation de la card module

            modelOut.cardModule.module = new ModuleDTO
            {
                id = mod.id,
                libe = mod.libe,
                typeModule = mod.typeModule
            };

            modelOut.cardModule.lesGammes = _donneListeGammes();

            #endregion

            #region préparation des infos composants

            //On prépare le tableau récapitulant les affectations de l'employé
            modelOut.cardComposant.tableauComposant.avecActionCrud = false;
            modelOut.cardComposant.tableauComposant.lesLignes.Add(new List<object> { "Composant", "Prix fournisseur", "Prix de vente", "Gamme", "" });
            List<Composition> composition = new List<Composition>();
            composition = _compositionService.DonneTousComposants(id);


            if (composition.Count != 0)
            {
                foreach (Composition cpst in composition)
                {

                    modelOut.cardComposant.tableauComposant.lesLignes.Add(new List<object> { cpst.composant.libe, cpst.composant.prixHT.ToString(), (cpst.composant.prixHT * (1 + (cpst.composant.gamme.pourcentageGamme/100))).ToString(), cpst.composant.gamme.libe, cpst.id });
             
                }
            }

            modelOut.cardComposant.lesComposants = _donneListeComposants();

            #endregion


            return View(modelOut);
        }

        [HttpPost]
        public ActionResult DetailComposant(int id, int idComposant)
        {
            CardComposantViewModel modelOut = new CardComposantViewModel();

            try
            {
                Module mdl = new Module();
                mdl = _moduleService.Get(id);
                Composant cpst = new Composant();
                cpst = _composantService.Get(idComposant);
                Composition cpstion = new Composition();
                cpstion.composant = cpst;
                cpstion.module = mdl;

                mdl.compositions.Add(cpstion);

                decimal prixAvecTaux = Convert.ToDecimal(cpst.prixHT * (1 + (cpst.gamme.pourcentageGamme / 100)));
                decimal prixtotal = Decimal.Add(mdl.prix , prixAvecTaux);
                mdl.prix = prixtotal;

                _moduleService.Update(mdl, _donneNomPrenomUtilisateur());
                _moduleService.Save();


                //On reconstruit le tableau récapitulant les composants de l'employé
                modelOut.tableauComposant.avecActionCrud = false;
                modelOut.tableauComposant.lesLignes.Add(new List<object> { "Composant", "Prix fournisseur", "Prix de vente", "Gamme", "" });
                List<Composition> composition1 = new List<Composition>();
                composition1 = _compositionService.DonneTousComposants(id);


                if (composition1.Count != 0)
                {
                    foreach (Composition cpst1 in composition1)
                    {

                        modelOut.tableauComposant.lesLignes.Add(new List<object> { cpst1.composant.libe, cpst1.composant.prixHT.ToString(), (cpst1.composant.prixHT * (1 + (cpst1.composant.gamme.pourcentageGamme / 100))).ToString(), cpst1.composant.gamme.libe, cpst1.id });

                    }
                }

                modelOut.lesComposants = _donneListeComposants();

                FlashMessage.Confirmation("Composant mis à jour avec succès");
            }
            catch (Exception e)
            {

                FlashMessage.Danger("Erreur lors de la mise à jour du composant");

                return PartialView("~/Areas/RechercheDeveloppement/Views/Module/_CardComposantPartial.cshtml", modelOut);
            }


            //List<Composition> composition = new List<Composition>();
            //composition = _compositionService.DonneTousComposants(id);
            //modelOut.lesTypesEmployes = _donneListeTypeEmploye();
            

            //modelOut.tableauComposant.lesLignes.Add(new List<object> { , module.typeModule.libe, module.prix.ToString(), module.id });


            return PartialView("~/Areas/RechercheDeveloppement/Views/Module/_CardComposantPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult DeleteComposantDetail(int idToDelete)
        {
            CardComposantViewModel modelOut = new CardComposantViewModel();
            CompositionDTO cpstion = new CompositionDTO();
            ModuleDTO mdl = new ModuleDTO();
            int idModule;
            decimal prixComposant;

            try
            {
                
                cpstion = Mapper.Map<Composition, CompositionDTO>(_compositionService.Get(idToDelete));
                prixComposant = Convert.ToDecimal(cpstion.composant.prixHT * (1 + (cpstion.composant.gamme.pourcentageGamme / 100)));
                _compositionService.Delete(idToDelete, _donneNomPrenomUtilisateur());
                _compositionService.Save();

                //On récupère les caractéristiques du module
                idModule = _compositionService.Get(idToDelete).module.id;
                mdl = Mapper.Map<Module, ModuleDTO>(_moduleService.Get(idModule));
                mdl.prix = Decimal.Subtract(mdl.prix, prixComposant);
                _moduleService.Update(Mapper.Map<ModuleDTO, Module>(mdl), _donneNomPrenomUtilisateur());
                _moduleService.Save();

                //On reconstruit le tableau récapitulant les composants de l'employé
                modelOut.tableauComposant.avecActionCrud = false;
                modelOut.tableauComposant.lesLignes.Add(new List<object> { "Composant", "Prix fournisseur", "Prix de vente", "Gamme", "" });
                List<Composition> composition1 = new List<Composition>();
                composition1 = _compositionService.DonneTousComposants(idModule);


                if (composition1.Count != 0)
                {
                    foreach (Composition cpst1 in composition1)
                    {

                        modelOut.tableauComposant.lesLignes.Add(new List<object> { cpst1.composant.libe, cpst1.composant.prixHT.ToString(), (cpst1.composant.prixHT * (1 + (cpst1.composant.gamme.pourcentageGamme / 100))).ToString(), cpst1.composant.gamme.libe, cpst1.id });

                    }
                }

                modelOut.lesComposants = _donneListeComposants();

                FlashMessage.Confirmation("Composant mis à jour avec succès");


            }
            catch (Exception e)
            {
                FlashMessage.Danger("Erreur lors de la mise à jour du composant");

                return PartialView("~/Areas/RechercheDeveloppement/Views/Module/_CardComposantPartial.cshtml", modelOut);


            }

            return PartialView("~/Areas/RechercheDeveloppement/Views/Module/_CardComposantPartial.cshtml", modelOut);
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

