using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Madera.Service;
using MaderaSoft.Models.DTO;
using MaderaSoft.Areas.RessourcesHumaines.Models.ViewModels;
using MaderaSoft.Models.ViewModel;
using Vereyon.Web;
using Madera.Model;
using MaderaSoft.Models;
using System.Data.SqlTypes;

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
        private readonly IUtilisateurService _utilisateurService;

        public EmployeController(IEmployeService employeService, IServiceService serviceService, IPersonneService personneService, IAdresseService adresseService, IDroitService droitService, ITEmployeService temployeService, IAffectationServiceService affectationService, IUtilisateurService utilisateurService)
        {
            this._employeService = employeService;
            this._serviceService = serviceService;
            this._adresseService = adresseService;
            this._personneService = personneService;
            this._droitService = droitService;
            this._temployeService = temployeService;
            this._affectationService = affectationService;
            this._utilisateurService = utilisateurService;
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
            //modelOut.tableauEmployes.typeObjet = "RessourcesHumaines/Employe";
            modelOut.tableauEmployes.avecActionCrud = true;
            modelOut.tableauEmployes.messageSiVide = "Aucun employé n'a été saisi dans l'application.";

            List<PEmployeTableauDTO> lesEmployes = Mapper.Map<List<Employe>, List<PEmployeTableauDTO>>(_employeService.DonneTous().ToList());

            modelOut.tableauEmployes.lesLignes.Add(new List<object> { "", "Employé", "Adresse", "" });

            foreach (PEmployeTableauDTO employe in lesEmployes)
            {
                button = new BootstrapButtonViewModel
                {
                    href = Url.Action("Detail", "Employe", new { area = "RessourcesHumaines", id = employe.id }).ToString(),
                    cssClass = "",
                    libe = " ",
                    typeDeBouton = Parametres.TypeBouton.Detail,
                    tooltip = "Voir la fiche détaillée de cet employé"
                };

                modelOut.tableauEmployes.lesLignes.Add(new List<object> { button, string.Format("{0} {1} {2}", employe.getCiv().ToUpperFirst(), employe.nom.ToUpperFirst(), employe.prenom.ToUpperFirst()), string.Format("{0} {1} {2} {3}", employe.adresse.numRue, employe.adresse.nomRue, employe.adresse.codePostal, employe.adresse.ville), employe.id.ToString() });
            }


            return View(modelOut);
        }

        /// <summary>
        /// Permet d'alimenter les informations nécessaires à la génération d'une modal d'édition d'un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult EditModal(int? id)
        {

            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            EditEmployeViewModel editEmploye = new EditEmployeViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();

            if (id.HasValue)
            {
                editEmploye.personne = Mapper.Map<Employe, EditEmployeDTO>(_employeService.Get(id.Value));

                modelOut.titreModal = string.Format("Modification des informations de {0} {1} {2}", editEmploye.personne.getCiv(), editEmploye.personne.nom.ToUpperFirst(), editEmploye.personne.prenom.ToUpperFirst());

                #region préparation du tableau récapitulatif des affectations

                //On prépare le tableau récapitulant les affectations de l'employé
                editEmploye.lesAffectationsEmploye.lesLignes.Add(new List<object> { "", "Service", "Droit", "Activité principale" });

                if (editEmploye.personne != null)
                {
                    if (editEmploye.personne.affectationServices != null)
                    {
                        foreach (AffectationServiceDTO affectation in editEmploye.personne.affectationServices)
                        {
                            button = new BootstrapButtonViewModel
                            {
                                href = Url.Action("Detail", "Employe", new { area = "RessourcesHumaines", id = editEmploye.personne.id }).ToString(),
                                cssClass = "",
                                libe = " ",
                                typeDeBouton = Parametres.TypeBouton.Detail
                            };

                            editEmploye.lesAffectationsEmploye.lesLignes.Add(new List<object> { button, affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon() });
                        }
                    }

                }

                #endregion

            }
            else
            {
                modelOut.titreModal = "Ajout d'un employé";
            }

            //On récupère la liste des services disponibles dans l'application
            editEmploye.lesServices = _donneListeService();

            //On récupère les niveaux de droits disponibles dans l'application
            editEmploye.lesDroits = _donneListeGroupeUtilisateur();

            //On récuère la liste des types d'employés
            editEmploye.lesTypesEmployes = _donneListeTypeEmploye();

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
        public ActionResult Edit(EditEmployeDTO personne)
        {
            AffectationService nouvelleAffectation = new AffectationService();
            Employe employeOrigine = new Employe();

            //On prépare la nouvelle affectation
            if (personne.serviceIdPourAffectation != 0 && personne.groupeIdPourAffectation != 0)
            {
                nouvelleAffectation.isPrincipal = personne.isAffecttionPrincipal;
                nouvelleAffectation.service = _serviceService.Get(personne.serviceIdPourAffectation);
                nouvelleAffectation.groupe = _droitService.Get(personne.groupeIdPourAffectation);
            }

            if (personne.id != 0)//update
            {
                try
                {
                    employeOrigine = _employeService.Get(personne.id);

                    _insertOrUpdateAffectation(ref employeOrigine, nouvelleAffectation);

                    _employeService.Update(employeOrigine);
                    _employeService.Save();

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
                    employeOrigine = Mapper.Map<EditEmployeDTO, Employe>(personne);


                    employeOrigine.utilisateur.password = _utilisateurService.Crypte(Parametres.defaultPassword);
                    employeOrigine.affectationServices.Add(nouvelleAffectation);
                    employeOrigine.utilisateur.login = employeOrigine.nom.ToUpper() + '.' + employeOrigine.prenom.ToUpper().First();

                    //On prépare le type d'employé
                    employeOrigine.typeEmploye = _temployeService.Get(personne.typeEmploye.id);
                    _employeService.Create(employeOrigine);

                    FlashMessage.Confirmation("Employé créé avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de l'ajout de l'employé");
                }

            }
            _employeService.Save();

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
            //modelOut.typeObjet = "RessourcesHumaines/Employe";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'un employé";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer cet employé ?", method = "Delete", urlController = "Employe" };

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

                _employeService.Delete(idToDelete);
                _employeService.Save();
                FlashMessage.Confirmation("Suppression de l'employé");
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
            EmployeDTO emp = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(id));

            #region préparation de la card identité de l'employé

            modelOut.cardEmploye.employe = new EmployeSimpleDTO
            {
                id = emp.id,
                civ = emp.civ,
                nom = emp.nom,
                prenom = emp.prenom,
                email = emp.email,
                tel1 = emp.tel1,
                tel2 = emp.tel2,
                typeEmploye = emp.typeEmploye
            };

            //modelOut.cardEmploye.lesTypesEmployes = _donneListeTypeEmploye();

            #endregion

            #region préparation des infos utilisateur de l'employé

            modelOut.cardUtilisateur.utilisateur = emp.utilisateur;

            if (emp.utilisateur.isActive)
            {
                modelOut.cardUtilisateur.notifications.Add(new MaderaSoft.Models.Notification
                {
                    dureeNotification = Parametres.DureeNotification.Always,
                    message = "Le compte utilisateur est actif",
                    typeNotification = Parametres.TypeNotification.Information
                });

                if (emp.utilisateur.isFirstConnexion)
                {
                    modelOut.cardUtilisateur.notifications.Add(new MaderaSoft.Models.Notification
                    {
                        dureeNotification = Parametres.DureeNotification.Always,
                        message = "L'utilisateur doit changer le mot de passe par défaut",
                        typeNotification = Parametres.TypeNotification.Warning
                    });
                }
            }
            else
            {
                modelOut.cardUtilisateur.notifications.Add(new MaderaSoft.Models.Notification
                {
                    dureeNotification = Parametres.DureeNotification.Always,
                    message = "Le compte utilisateur est désactivé",
                    typeNotification = Parametres.TypeNotification.Warning
                });
            }



            #endregion

            #region préparation de la card des affectations de l'employé
            //les affectations de l'employé

            //On prépare le tableau récapitulant les affectations de l'employé
            modelOut.cardAffectations.tableauAffectations.avecActionCrud = false;
            modelOut.cardAffectations.tableauAffectations.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale", "" });

            if (emp.affectationServices != null)
            {
                foreach (AffectationServiceDTO affectation in emp.affectationServices)
                {

                    modelOut.cardAffectations.tableauAffectations.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon(), affectation.id });
                }
            }

            #region préparation des éléments utiles à la création d'une affectation

            modelOut.cardAffectations.lesDroits = _donneListeGroupeUtilisateur();

            modelOut.cardAffectations.lesServices = _donneListeService();

            modelOut.cardAffectations.nouvelleAffectation.emplyeId = id;


            #endregion

            #endregion

            #region préparation de la card adresse

            modelOut.adresse = emp.adresse;

            #endregion

            return View(modelOut);
        }

        /// <summary>
        /// Méthode utilisée depuis un appel Ajax pour mettre à jour les données relatives à l'identité d'un employé depuis sa fiche détaillée.
        /// </summary>
        /// <param name="employe"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DetailEmploye(EmployeSimpleDTO employe)
        {
            CardEmployeViewModel modelOut = new CardEmployeViewModel();

            try
            {
            _personneService.Update(Mapper.Map<PersonneSimpleDTO, Personne>(employe));
            _personneService.Save();

            FlashMessage.Confirmation("Employé mis à jour avec succès");
            }
            catch (Exception e)
            {
                modelOut.employe = employe;

                FlashMessage.Danger("Erreur lors de la mise à jour de l'employé");

                return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardEmployePartial.cshtml", modelOut);
            }

            modelOut.employe = employe;
            //modelOut.lesTypesEmployes = _donneListeTypeEmploye();

            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardEmployePartial.cshtml", modelOut);
        }

        /// <summary>
        /// Méthode utilisée depuis un appel Ajax pour mettre à jour les données relatives à l'adresse d'un employé depuis sa fiche détaillée.
        /// </summary>
        /// <param name="adresse"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DetailAdresse(AdresseDTO adresse)
        {
            AdresseDTO modelOut = new AdresseDTO();
            try
            {
                _adresseService.Update(Mapper.Map<AdresseDTO, Adresse>(adresse));
                _adresseService.Save();
                FlashMessage.Confirmation("Adresse mise à jour avec succès");
            }
            catch (Exception e)
            {
                modelOut = adresse;
                FlashMessage.Danger("Erreur lors de la mise à jour");
                return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardAdressePartial.cshtml", modelOut);
            }

            modelOut = adresse;

            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardAdressePartial.cshtml", modelOut);
        }

        /// <summary>
        /// Méthode utilisée depuis un appel Ajax pour ajouter une affectation à un employé depuis sa fiche détaillée.
        /// </summary>
        /// <param name="nouvelleAffectation"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DetailAjoutAffectation(NouvelleAffectationDTO nouvelleAffectation)
        {
            CardAffectationServiceViewModel modelOut = new CardAffectationServiceViewModel();
            AffectationService newAffectation = new AffectationService();
            Employe emp = new Employe();
            EmployeDTO emplo = new EmployeDTO();

            try
            {
                emp = _employeService.Get(nouvelleAffectation.emplyeId);

                newAffectation.groupe = _droitService.Get(nouvelleAffectation.groupeIdPourAffectation);
                newAffectation.service = _serviceService.Get(nouvelleAffectation.serviceIdPourAffectation);
                newAffectation.isPrincipal = nouvelleAffectation.isAffecttionPrincipal;


                _insertOrUpdateAffectation(ref emp, newAffectation);

                _employeService.Update(emp);
                _employeService.Save();


                FlashMessage.Confirmation("Ajout de l'affectation avec succès");

                #region tableau des affectations de l'employé

                //On récupère l'employé avecla dernière affectation
                emplo = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(nouvelleAffectation.emplyeId));

                //On prépare le tableau récapitulant les affectations de l'employé
                modelOut.tableauAffectations.avecActionCrud = false;
                modelOut.tableauAffectations.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale", "" });

                if (emplo.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in emplo.affectationServices)
                    {

                        modelOut.tableauAffectations.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon(), affectation.id });
                    }
                }

                #endregion

                modelOut.nouvelleAffectation.emplyeId = nouvelleAffectation.emplyeId;
                modelOut.lesDroits = _donneListeGroupeUtilisateur();
                modelOut.lesServices = _donneListeService();

                //On met à jour l'utilisateur en session, car lesa ffectations influes sur les éléments du menu de navigation
                EmployeDTO util = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(nouvelleAffectation.emplyeId));
                Session["utilisateur"] = util;


            }
            catch (Exception e)
            {
                emplo = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(nouvelleAffectation.emplyeId));

                #region tableau des affectations de l'employé

                //On prépare le tableau récapitulant les affectations de l'employé
                modelOut.tableauAffectations.avecActionCrud = false;
                modelOut.tableauAffectations.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale", "" });

                if (emplo.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in emplo.affectationServices)
                    {

                        modelOut.tableauAffectations.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon(), "" });
                    }
                }

                #endregion

                modelOut.nouvelleAffectation.emplyeId = nouvelleAffectation.emplyeId;
                modelOut.lesDroits = _donneListeGroupeUtilisateur();
                modelOut.lesServices = _donneListeService();

                FlashMessage.Danger("Erreur lors de la création de l'affectation");
            }



            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardAffectationPartial.cshtml", modelOut);
        }

        /// <summary>
        /// Méthode utilisée depuis un appel Ajax pour supprimer une affectation à un employé depuis sa ficche détaillée.
        /// </summary>
        /// <param name="idToDelete"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAffectationDetail(int idToDelete)
        {
            CardAffectationServiceViewModel modelOut = new CardAffectationServiceViewModel();
            EmployeDTO emp = new EmployeDTO();

            //On conserve l'id de l'employé correspondant pour recontruire ensuite l'affiche de ses affectations
            int idEmploye = _affectationService.Get(idToDelete).employe.id;

            try
            {
                emp = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(idEmploye));

                //Un employé doit avoir au moins une affectation
                if(emp.affectationServices.Count == 1)
                {
                    modelOut.notifications.Add(new Notification
                    {
                        dureeNotification = Parametres.DureeNotification.Always,
                        message = "Un employé doit avoir au moins une affectation",
                        typeNotification = Parametres.TypeNotification.Warning
                    });

                    //On reconstruit l'affichage
                    modelOut.tableauAffectations.avecActionCrud = false;
                    modelOut.tableauAffectations.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale", "" });

                    if (emp.affectationServices != null)
                    {
                        foreach (AffectationServiceDTO affectation in emp.affectationServices)
                        {

                            modelOut.tableauAffectations.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon(), affectation.id });
                        }
                    }

                    #region préparation des éléments utiles à la création d'une affectation

                    modelOut.lesDroits = _donneListeGroupeUtilisateur();

                    modelOut.lesServices = _donneListeService();

                    modelOut.nouvelleAffectation.emplyeId = emp.id;


                    #endregion

                    return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardAffectationPartial.cshtml", modelOut);
                }

                //On supprimer l'affectation
                _affectationService.Delete(idToDelete);
                _affectationService.Save();
                FlashMessage.Confirmation("Suppression de l'affectation avec succès");

                emp = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(idEmploye));

                //On reconstruit l'affichage
                modelOut.tableauAffectations.avecActionCrud = false;
                modelOut.tableauAffectations.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale", "" });

                if (emp.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in emp.affectationServices)
                    {

                        modelOut.tableauAffectations.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon(), affectation.id });
                    }
                }

                #region préparation des éléments utiles à la création d'une affectation

                modelOut.lesDroits = _donneListeGroupeUtilisateur();

                modelOut.lesServices = _donneListeService();

                modelOut.nouvelleAffectation.emplyeId = emp.id;


                #endregion

            }
            catch (Exception e)
            {
                FlashMessage.Danger("Erreur lors de la suppression de l'affectation");

                //On reconstruit l'affichage
                modelOut.tableauAffectations.avecActionCrud = false;
                modelOut.tableauAffectations.lesLignes.Add(new List<object> { "Service", "Droit", "Activité principale", "" });

                if (emp.affectationServices != null)
                {
                    foreach (AffectationServiceDTO affectation in emp.affectationServices)
                    {

                        modelOut.tableauAffectations.lesLignes.Add(new List<object> { affectation.service.libe, affectation.groupe.libe, affectation.affectationPrincipaleOuiNon(), affectation.id });
                    }
                }

                #region préparation des éléments utiles à la création d'une affectation

                modelOut.lesDroits = _donneListeGroupeUtilisateur();

                modelOut.lesServices = _donneListeService();

                modelOut.nouvelleAffectation.emplyeId = emp.id;


                #endregion

                return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardAffectationPartial.cshtml", modelOut);
            }

            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardAffectationPartial.cshtml", modelOut);
        }

        /// <summary>
        /// Donne la liste des types d'employés en base de données
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> _donneListeTypeEmploye()
        {
            List<SelectListItem> lesTypesDEmployes = _temployeService.DonneTous().Select(
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
        /// <returns></returns>
        private List<SelectListItem> _donneListeService()
        {
            List<SelectListItem> lesServices = new List<SelectListItem>();

            //On récupère la liste des services disponibles dans l'application
            lesServices = _serviceService.DonneTous().Select(
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
        /// <returns></returns>
        private List<SelectListItem> _donneListeGroupeUtilisateur()
        {
            List<SelectListItem> lesGroupes = new List<SelectListItem>();

            //On récupère la liste des services disponibles dans l'application
            lesGroupes = _droitService.DonneTous().Select(
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
        private void _insertOrUpdateAffectation(ref Employe personne, AffectationService nouvelleAffectation)
        {
            if (nouvelleAffectation.service != null)
            {
                //On regarde si cet employé a déjà une affectation sur ce service
                if (personne.affectationServices.FirstOrDefault(x => x.service.id == nouvelleAffectation.service.id) != null)//On met à jour l'affectation
                {
                    personne.affectationServices.First(x => x.service.libe == nouvelleAffectation.service.libe).isPrincipal = nouvelleAffectation.isPrincipal;
                    personne.affectationServices.First(x => x.service.libe == nouvelleAffectation.service.libe).groupe = nouvelleAffectation.groupe;

                }
                else//On ajoute l'affectation
                {
                    nouvelleAffectation.employe = personne;
                    personne.affectationServices.Add(nouvelleAffectation);
                }
            }

        }
    }
}