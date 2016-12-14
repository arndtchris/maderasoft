using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using Vereyon.Web;

namespace MaderaSoft.Controllers
{
    public class AffectationServiceController : Controller
    {

        private readonly IAffectationServiceService _affectationService;
        private readonly IServiceService _serviceService;
        private readonly IDroitService _droitService;

        public AffectationServiceController(IAffectationServiceService affectationService, IServiceService serviceService, IDroitService droitService)
        {
            this._affectationService = affectationService;
            this._serviceService = serviceService;
            this._droitService = droitService;
        }

        // GET: AffectationService
        /*public ActionResult Index()
        {
            return View();
        }*/

        [HttpGet]
        public ActionResult EditModal(int? id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            AffectationServiceViewModel editAffectationService = new AffectationServiceViewModel();

            editAffectationService.affectationService = Mapper.Map<AffectationService, AffectationServiceDTO>(_affectationService.GetAffectationService(id.Value));

            //On récupère la liste des services disponibles dans l'application
            editAffectationService.lesServices = _serviceService.GetServices().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();
            editAffectationService.lesServices.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            //On récupère les niveaux de droits disponibles dans l'application
            editAffectationService.lesDroits = _droitService.GetDroits().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();
            editAffectationService.lesDroits.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            modelOut.formulaireUrl = "~/Views/AffectationService/_EditAffectationServicePartial.cshtml";
            modelOut.objet = editAffectationService;
            modelOut.titreModal = "Modification d'une affectation";

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Edit(AffectationServiceViewModel modelIn)
        {
            AffectationService editAffectation = Mapper.Map<AffectationServiceDTO, AffectationService>(modelIn.affectationService);

            try
            {
                _affectationService.UpdateAffectationService(editAffectation);
                _affectationService.saveAffectationService();
                FlashMessage.Confirmation("L'affectation a été mise à jour avec succès");
            }
            catch (Exception e)
            {
                FlashMessage.Danger("Erreur lors de la mise à jour de l'affectation");

            }

            return RedirectToAction(Url.Action("Index","Employe", new { area = "RessourcesHumaines" }, null));
        }

        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            modelOut.typeObjet = "AffectationService";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'une affectation";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer cette affectation ?" };

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            try
            {
                FlashMessage.Confirmation("Suppression de l'affectation");
                _affectationService.deleteAffectationService(idToDelete);
                _affectationService.saveAffectationService();
            }
            catch (Exception)
            {
                FlashMessage.Danger("Erreur lors de la suppression de l'adresse");
            }

            return RedirectToAction(HttpContext.Request.UrlReferrer.ToString());
        }
    }
}