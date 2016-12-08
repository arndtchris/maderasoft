using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Areas.RessourcesHumaines.Models.DTOs;
using MaderaSoft.Areas.RessourcesHumaines.Models.ViewModels;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using Vereyon.Web;

namespace MaderaSoft.Areas.RessourcesHumaines.Controllers
{
    public class EmployeController : Controller
    {
        private readonly IEmployeService _employeService;
        private readonly IServiceService _serviceService;
        private readonly IPersonneService _personneService;
        private readonly IAdresseService _adresseService;

        public EmployeController(IEmployeService employeService, IServiceService serviceService, IPersonneService personneService, IAdresseService adresseService )
        {
            this._employeService = employeService;
            this._serviceService = serviceService;
            this._adresseService = adresseService;
            this._personneService = personneService;

        }

        // GET: RessourcesHumaines/Employe
        public ActionResult Index()
        {
            EmployeIndexViewModel modelOut = new EmployeIndexViewModel();
            modelOut.tableauEmployes.typeObjet = "Employe";
            modelOut.tableauEmployes.avecActionCrud = true;

            List<PersonneDTO> lesEmployes = Mapper.Map<List<Personne>, List<PersonneDTO>>(_personneService.GetEmployes().ToList());

            modelOut.tableauEmployes.lesLignes.Add(new List<string> { "Nom", "Prénom","Adresse",""});

            foreach(PersonneDTO employe in lesEmployes)
            {
                modelOut.tableauEmployes.lesLignes.Add(new List<string> { employe.nom, employe.prenom, string.Format("{0} {1} {2} {3}",employe.adresse.numRue, employe.adresse.nomRue, employe.adresse.codePostal, employe.adresse.ville), employe.id.ToString() });
            }


            return View(modelOut);
        }

        [HttpGet]
        public ActionResult EditModal(int? id)
        {

            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            EditEmployeViewModel editEmploye = new EditEmployeViewModel();

            if (id.HasValue)
            {
                editEmploye.personne = Mapper.Map<Personne,PersonneDTO>(_personneService.GetPersonne(id.Value));
                modelOut.titreModal = string.Format("Modification des informations de {0} {1} {2}", editEmploye.personne.civ, editEmploye.personne.nom, editEmploye.personne.prenom);
            }else
            {
                modelOut.titreModal = "Edition d'un employé";
            }

            modelOut.formulaireUrl = "~/Areas/RessourcesHumaines/Views/Employe/_EditEmployePartial.cshtml";
            

            modelOut.objet = editEmploye;

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

        }

        [HttpPost]
        public ActionResult Edit(EmployeDTO modelIn)
        {
            var personne = Mapper.Map<PersonneDTO, Personne>(modelIn.personne);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            if(modelIn.personne.id != 0)
            {
                try
                {
                    _personneService.UpdatePersonne(personne);
                    FlashMessage.Confirmation("Employé mis à jour avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de mis à jour de l'employé");
                }
            }
            else
            {
                try
                {
                    _personneService.CreatePersonne(personne);
                    FlashMessage.Confirmation("Employé créé avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de l'ajout de l'employé");
                }
                
            }

            _personneService.savePersonne();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            modelOut.typeObjet = "RessourcesHumaines/Employe";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'un employé";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer cet employé ?" };

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            try
            {
                FlashMessage.Confirmation("Suppression de l'employé");
                _personneService.deletePersonne(idToDelete);
                _personneService.savePersonne();
            }
            catch (Exception)
            {
                FlashMessage.Danger("Erreur lors de la suppression de l'employé");
                throw;
            }

            return RedirectToAction("Index");
        }
    }
}