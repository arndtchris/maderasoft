using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Areas.RechercheDeveloppement.Models.DTOs;
using MaderaSoft.Areas.RechercheDeveloppement.Models.ViewModels;
using MaderaSoft.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace MaderaSoft.Areas.RechercheDeveloppement.Controllers
{
    public class ModuleController : Controller
    {
        // GET: GestionModule/Module

            private readonly IModuleService _moduleService;
            private readonly IServiceService _serviceService;
            private readonly ITModuleService _tmoduleService;


            public ModuleController(IModuleService moduleService, IServiceService serviceService, ITModuleService tmoduleService)
            {
                this._moduleService = moduleService;
                this._serviceService = serviceService;
                this._tmoduleService = tmoduleService;
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

                modelOut.tableauModules.lesLignes.Add(new List<object> { "", "Nom module", "Gamme", "" });

                foreach (ModuleDTO module in lesModules)
                {
                    button = new BootstrapButtonViewModel
                    {
                        href = Url.Action("Detail", "Module", new { area = "RechercheDeveloppement", id = module.id }).ToString(),
                        cssClass = "",
                        libe = " ",
                        typeDeBouton = Parametres.TypeBouton.Detail
                    };
                    modelOut.tableauModules.lesLignes.Add(new List<object> { button, module.libe, module.typeModule.libe , module.id });
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
            }


            editModule.lesGammes = _donneListeGammes();
                modelOut.formulaireUrl = "~/Areas/RechercheDeveloppement/Views/Module/_EditModulePartial.cshtml";
                modelOut.titreModal = string.Format("Modification des informations du module");
                modelOut.objet = editModule;

                return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

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
                    _moduleService.Update(mdl);

                    FlashMessage.Confirmation("Module mis à jour avec succès");
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
                    _moduleService.Create(mdl);

                        FlashMessage.Confirmation("Module créé avec succès");
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
                BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
                modelOut.typeObjet = "RechercheDeveloppement/Module";
                modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
                modelOut.titreModal = "Suppression d'un module";
                modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer ce module ?" };

                return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
            }

            [HttpPost]
            public ActionResult Delete(int idToDelete)
            {
                try
                {
                    FlashMessage.Confirmation("Suppression du module");
                    _moduleService.Delete(idToDelete);
                    _moduleService.Save();
                }
                catch (Exception)
                {
                    FlashMessage.Danger("Erreur lors de la suppression du module");
                    throw;
                }

                return RedirectToAction("Index");
            }
        }
    }

