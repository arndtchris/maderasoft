using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Models.DTO;
using MaderaSoft.Areas.RessourcesHumaines.Models.ViewModels;
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
        /// <summary>
        /// Permet de synthétiser l'ensemble des employés présents dans l'application au sein d'un tableau
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            EmployeIndexViewModel modelOut = new EmployeIndexViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();
            modelOut.tableauEmployes.typeObjet = "RessourcesHumaines/Employe";
            modelOut.tableauEmployes.avecActionCrud = true;
            modelOut.tableauEmployes.messageSiVide = "Aucun employé n'a été saisi dans l'application.";

            List<PEmployeTableauDTO> lesEmployes = Mapper.Map<List<Employe>, List<PEmployeTableauDTO>>(_employeService.GetEmployes().ToList());

            modelOut.tableauEmployes.lesLignes.Add(new List<object> {"", "Employé","Adresse",""});

            foreach(PEmployeTableauDTO employe in lesEmployes)
            {
                button = new BootstrapButtonViewModel {
                    href = Url.Action("Detail", "Employe", new { area = "RessourcesHumaines", id = employe.id}).ToString(),
                    cssClass = "",
                    libe = " ",
                    typeDeBouton = Parametres.TypeBouton.Detail
                };
             
                modelOut.tableauEmployes.lesLignes.Add(new List<object> { button, string.Format("{0} {1} {2}", employe.getCiv().ToUpperFirst(), employe.nom.ToUpperFirst(), employe.prenom.ToUpperFirst()), string.Format("{0} {1} {2} {3}",employe.adresse.numRue, employe.adresse.nomRue, employe.adresse.codePostal, employe.adresse.ville), employe.id.ToString() });
            }


            return View(modelOut);
        }

        /// <summary>
        /// Permet d'alimenter les informations nécéssaires à la génération d'une fenêtre modale de création/modification d'un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateModal()
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            CreateEmployeViewModel editEmploye = new CreateEmployeViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();

            modelOut.titreModal = "Ajout d'un employé";

            //On récupère la liste des services disponibles dans l'application
            editEmploye.lesServices = _donneListeService();

            //On récupère les niveaux de droits disponibles dans l'application
            editEmploye.lesDroits = _donneListeGroupeUtilisateur();

            //On récuère la liste des types d'employé
            editEmploye.lesTypesEmployes = _donneListeTypeEmploye();

            //On prépare le tableau récapitulant les affectations de l'employé
            editEmploye.lesAffectationsEmploye.lesLignes.Add(new List<object> {"", "Service", "Droit", "Activité principale"});

            modelOut.formulaireUrl = "~/Areas/RessourcesHumaines/Views/Employe/_CreateEmployePartial.cshtml";
            modelOut.objet = editEmploye;

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        /// <summary>
        /// Permet d'alimenter les informations nécessaires à la génération d'une modal d'édition d'un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet]
        public ActionResult EditModal(int id)
        {

            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            EditEmployeViewModel editEmploye = new EditEmployeViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();

            editEmploye.personne = Mapper.Map<Employe, EmployeDTO>(_employeService.GetEmploye(id));
            modelOut.titreModal = string.Format("Modification des informations de {0} {1} {2}", editEmploye.personne.getCiv(), editEmploye.personne.nom.ToUpperFirst(), editEmploye.personne.prenom.ToUpperFirst());


            //On récupère la liste des services disponibles dans l'application
            editEmploye.lesServices = _donneListeService();

            //On récupère les niveaux de droits disponibles dans l'application
            editEmploye.lesDroits = _donneListeGroupeUtilisateur();

            //On récuère la liste des types d'employés
            editEmploye.lesTypesEmployes = _donneListeTypeEmploye();

            //On prépare le tableau récapitulant les affectations de l'employé
            editEmploye.lesAffectationsEmploye.lesLignes.Add(new List<object> { "", "Service", "Droit", "Activité principale" });

            #region préparation du tableau récapitulatif des affectations

            if (editEmploye.personne.employe != null)
            {
                if (editEmploye.personne.employe.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in editEmploye.personne.employe.affectationServices)
                    {
                        button = new BootstrapButtonViewModel
                        {
                            href = Url.Action("Detail", "Employe", new { area = "RessourcesHumaines", id = editEmploye.personne.employe.id }).ToString(),
                            cssClass = "",
                            libe = " ",
                            typeDeBouton = Parametres.TypeBouton.Detail
                        };

                        editEmploye.lesAffectationsEmploye.lesLignes.Add(new List<object> { button, affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon() });
                    }
                }

            }

            #endregion

            modelOut.formulaireUrl = "~/Areas/RessourcesHumaines/Views/Employe/_EditEmployePartial.cshtml";
            modelOut.objet = editEmploye;

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

        }

        /// <summary>
        /// Permet de valider la création/modification d'un employé
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EmployeDTO personne)
        {
            AffectationService nouvelleAffectation = new AffectationService();
            Employe perso = new Employe();

            //On prépare la nouvelle affectation
            if(personne.serviceIdPourAffectation != 0 && personne.groupeIdPourAffectation != 0)
            {
                nouvelleAffectation.isPrincipal = personne.isAffecttionPrincipal;
                nouvelleAffectation.service = _serviceService.GetService(personne.serviceIdPourAffectation);
                nouvelleAffectation.groupe = _droitService.GetDroit(personne.groupeIdPourAffectation);
            }

            if (personne.id != 0)//update
            {
                try
                {
                    perso = Mapper.Map<EmployeDTO, Employe>(personne);
                    perso.typeEmploye = _temployeService.GetTEmploye(personne.typeEmploye.id);

                    _insertOrUpdateAffectation(ref perso, nouvelleAffectation);

                    _personneService.UpdatePersonne(perso);

                    FlashMessage.Confirmation("Employé mis à jour avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de mis à jour de l'employé");
                }
            }
            else//create
            {
                try
                {
                    perso = Mapper.Map<EmployeDTO, Employe>(personne);
                    perso.affectationServices.Add(nouvelleAffectation);

                    //On prépare le type d'employé
                    perso.typeEmploye = _temployeService.GetTEmploye(personne.typeEmploye.id);
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

        /// <summary>
        /// Permet d'alimenter les informations nécessaires à la génération d'une modale permettant de supprimer un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Pemret de supprimer un employé en fonction de son id
        /// </summary>
        /// <param name="idToDelete"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Permet d'afficher de manière détaillée l'ensemble des informations rattachés à un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            DetailEmployeViewModel modelOut = new DetailEmployeViewModel();

            /*if (id != 0)
            {
                modelOut.personne = Mapper.Map<Personne, PersonneDTO>(_personneService.GetPersonne(id));
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

            modelOut.lesServices.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            //On récupère les niveaux de droits disponibles dans l'application
            modelOut.lesDroits = _droitService.GetDroits().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();
            modelOut.lesDroits.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });


            modelOut.lesAffectationsEmploye.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale", "" });
            modelOut.lesAffectationsEmploye.typeObjet = "AffectationService";

            if (modelOut.personne.employe != null)
            {
                if (modelOut.personne.employe.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in modelOut.personne.employe.affectationServices)
                    {
                        modelOut.lesAffectationsEmploye.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon(), affectation.id });
                    }
                }

            }

            #endregion

            //On récupère les types d'employés disponibles dans l'application
            modelOut.lesTEmployes = _temployeService.GetTEmployes().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();*/

            return View(modelOut);
        }

        /// <summary>
        /// Permet de modifier les informations d'un employé depuis la vue détaillée
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Donne la liste des types d'employés en base de données
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> _donneListeTypeEmploye()
        {
            List<SelectListItem> lesTypesDEmployes = _temployeService.GetTEmployes().Select(
                    x => new SelectListItem()
                    {
                        Text = x.libe,
                        Value = x.id.ToString()
                    }
                ).ToList();
            lesTypesDEmployes.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            return lesTypesDEmployes;
        }

        /// <summary>
        /// Donne la liste des services disponibles en base de données
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        private List<SelectListItem> _donneListeService()
        {
            List<SelectListItem> lesServices = new List<SelectListItem>();

            //On récupère la liste des services disponibles dans l'application
            lesServices = _serviceService.GetServices().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();
            lesServices.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            return lesServices;
        }

        /// <summary>
        /// Donne la liste des groupes utilisateur disponibles en base de données
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        private List<SelectListItem> _donneListeGroupeUtilisateur()
        {
            List<SelectListItem> lesGroupes = new List<SelectListItem>();

            //On récupère la liste des services disponibles dans l'application
            lesGroupes = _droitService.GetDroits().Select(
                x => new SelectListItem()
                {
                    Text = x.libe,
                    Value = x.id.ToString()
                }
                ).ToList();
            lesGroupes.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            return lesGroupes;
        }

        /// <summary>
        /// Permet de mettre à jour les affectations d'un employé
        /// </summary>
        /// <param name="personne"></param>
        /// <param name="nouvelleAffectation"></param>
        private void _insertOrUpdateAffectation (ref Employe personne, AffectationService nouvelleAffectation)
        {
            if(nouvelleAffectation.service != null)
            {
                //On regarde si cet empolyé a déjà une affectation sur ce service
                if (personne.affectationServices.FirstOrDefault(x => x.service.id == nouvelleAffectation.service.id) != null)//On met à jour l'affectation
                {
                    personne.affectationServices.First(x => x.service.libe == nouvelleAffectation.service.libe).isPrincipal = nouvelleAffectation.isPrincipal;
                    personne.affectationServices.First(x => x.service.libe == nouvelleAffectation.service.libe).groupe = nouvelleAffectation.groupe;

                }
                else//On ajoute l'affectation
                {
                    personne.affectationServices.Add(nouvelleAffectation);
                }
            }

        }
    }
}