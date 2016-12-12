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
        private readonly ITEmployeService _temployeService;
        private readonly IServiceService _serviceService;
        private readonly IPersonneService _personneService;
        private readonly IAdresseService _adresseService;
        private readonly IDroitService _droitService;
        private readonly IAffectationServiceService _affectationService;

        public EmployeController(IEmployeService employeService, IServiceService serviceService, IPersonneService personneService, IAdresseService adresseService, IDroitService droitService, ITEmployeService temployeService, IAffectationServiceService affectationService)
        {
            this._employeService = employeService;
            this._serviceService = serviceService;
            this._adresseService = adresseService;
            this._personneService = personneService;
            this._droitService = droitService;
            this._temployeService = temployeService;
            this._affectationService = affectationService;
        }

        // GET: RessourcesHumaines/Employe
        public ActionResult Index()
        {
            EmployeIndexViewModel modelOut = new EmployeIndexViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();
            modelOut.tableauEmployes.typeObjet = "Employe";
            modelOut.tableauEmployes.avecActionCrud = true;

            List<PersonneDTO> lesEmployes = Mapper.Map<List<Personne>, List<PersonneDTO>>(_personneService.GetEmployes().ToList());

            modelOut.tableauEmployes.lesLignes.Add(new List<object> {"", "Nom", "Prénom","Adresse",""});

            foreach(PersonneDTO employe in lesEmployes)
            {
                button = new BootstrapButtonViewModel {
                    href = Url.Action("Detail", "Employe", new { area = "RessourcesHumaines", id = employe.id}).ToString(),
                    cssClass = "",
                    libe = " ",
                    typeDeBouton = Parametres.TypeBouton.Detail
                };
                modelOut.tableauEmployes.lesLignes.Add(new List<object> { button, employe.nom, employe.prenom, string.Format("{0} {1} {2} {3}",employe.adresse.numRue, employe.adresse.nomRue, employe.adresse.codePostal, employe.adresse.ville), employe.id.ToString() });
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
                editEmploye.personne = Mapper.Map<Personne, PersonneEmployeDTO>(_personneService.GetPersonne(id.Value));
                modelOut.titreModal = string.Format("Modification des informations de {0} {1} {2}", editEmploye.personne.getCiv(), editEmploye.personne.nom, editEmploye.personne.prenom);
            }else
            {
                modelOut.titreModal = "Edition d'un employé";
            }

            #region préparation des affectations

            //On récupère la liste des services disponibles dans l'application
            editEmploye.lesServices = _serviceService.GetServices().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();

            //On récupère les niveaux de droits disponibles dans l'application
            editEmploye.lesDroits = _droitService.GetDroits().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();


            editEmploye.lesAffectationsEmploye.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale"});

            if (editEmploye.personne.employe != null)
            {
                if (editEmploye.personne.employe.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in editEmploye.personne.employe.affectationServices)
                    {
                        editEmploye.lesAffectationsEmploye.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon()});
                    }
                }

            }


            #endregion

            editEmploye.lesTypesEmployes = _temployeService.GetTEmployes().Select(
                    x => new SelectListItem()
                    {
                        Text = x.libe,
                        Value = x.TEmployeId.ToString()
                    }
                ).ToList();

            modelOut.formulaireUrl = "~/Areas/RessourcesHumaines/Views/Employe/_EditEmployePartial.cshtml";
            

            modelOut.objet = editEmploye;

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

        }

        [HttpPost]
        public ActionResult Edit(PersonneEmployeDTO personne)
        {
            AffectationService nouvelleAffectation = new AffectationService();
            Personne perso = new Personne();

            if (personne.employe.id != 0)
            {
                try
                {
                    //On prépare la nouvelle affectation
                    nouvelleAffectation.isPrincipal = personne.employe.isAffecttionPrincipal;
                    nouvelleAffectation.service = _serviceService.GetService(personne.employe.serviceIdPourAffectation);
                    nouvelleAffectation.groupe = _droitService.GetDroit(personne.employe.groupeIdPourAffectation);

                    perso = _personneService.GetPersonne(personne.id);

                    perso.employe.affectationServices.Add(nouvelleAffectation);

                    //On prépare le type d'employé
                    perso.employe.typeEmploye = _temployeService.GetTEmploye(personne.employe.typeEmployeId);

                    _personneService.UpdatePersonne(perso);

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
                    perso = Mapper.Map<PersonneEmployeDTO, Personne>(personne);
                    _personneService.CreatePersonne(perso);
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

        //GET : RessourcesHumaines/Employe/Detail/1
        [HttpGet]
        public ActionResult Detail(int id)
        {
            DetailEmployeViewModel modelOut = new DetailEmployeViewModel();

            if (id != 0)
            {
                modelOut.personne = Mapper.Map<Personne, PersonneEmployeDTO>(_personneService.GetPersonne(id));
            }                
            else
            {
                FlashMessage.Danger("Cet identifiant ne correspond pas à celui d'un employé");
                return RedirectToAction("Index");
            }

            #region préparation des affectations

            //On récupère la liste des services disponibles dans l'application
            modelOut.lesServices = _serviceService.GetServices().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();

            //On récupère les niveaux de droits disponibles dans l'application
            modelOut.lesDroits = _droitService.GetDroits().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();


            modelOut.lesAffectationsEmploye.lesLignes.Add(new List<object> { "Service", "Droit", "Acctivité principale", "" });

            if(modelOut.personne.employe != null)
            {
                if(modelOut.personne.employe.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in modelOut.personne.employe.affectationServices)
                    {
                        modelOut.lesAffectationsEmploye.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.isPrincipal, affectation.id });
                    }
                }

            }


            #endregion


            //On récupère les types d'employés disponibles dans l'application
            modelOut.lesTEmployes = _temployeService.GetTEmployes().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.TEmployeId.ToString()
                }
                ).ToList();

            return View(modelOut);
        }

        [HttpPost]
        public ActionResult EditDetail(DetailEmployeViewModel modelIn)
        {
            var personne = Mapper.Map<PersonneDTO, Personne>(modelIn.personne);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            if (personne.id != 0)
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

        private void insertOrUpdateAffectation (List<AffectationService> affectations)
        {
            foreach(AffectationService affectation in affectations)
            {
                if (affectation.id != 0)//update
                {
                    _affectationService.UpdateAffectationService(affectation);
                }
                else//create
                {
                    _affectationService.CreateAffectationService(affectation);
                }
            }

            _affectationService.saveAffectationService();
        }

        private void insertOrUpdateEmploye(Employe employe)
        {
                if (employe.id != 0)//update
                {
                    _employeService.UpdateEmploye(employe);
                }
                else//create
                {
                _employeService.CreateEmploye(employe);
            }

            _employeService.saveEmploye();
        }
    }
}