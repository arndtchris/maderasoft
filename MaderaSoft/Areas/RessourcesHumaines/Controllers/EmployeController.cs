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

            List<PEmployeTableauDTO> lesEmployes = Mapper.Map<List<Employe>, List<PEmployeTableauDTO>>(_employeService.DonneTous().ToList());

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

            if(id.HasValue)
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
            Employe perso = new Employe();

            //On prépare la nouvelle affectation
            if(personne.serviceIdPourAffectation != 0 && personne.groupeIdPourAffectation != 0)
            {
                nouvelleAffectation.isPrincipal = personne.isAffecttionPrincipal;
                nouvelleAffectation.service = _serviceService.Get(personne.serviceIdPourAffectation);
                nouvelleAffectation.groupe = _droitService.Get(personne.groupeIdPourAffectation);
            }

            if (personne.id != 0)//update
            {
                try
                {
                    perso = Mapper.Map<EditEmployeDTO, Employe>(personne);
                    perso.typeEmploye = _temployeService.Get(personne.typeEmploye.id);

                    _insertOrUpdateAffectation(ref perso, nouvelleAffectation);

                    _employeService.Update(perso);

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
                    perso = Mapper.Map<EditEmployeDTO, Employe>(personne);
                    perso.affectationServices.Add(nouvelleAffectation);

                    //On prépare le type d'employé
                    perso.typeEmploye = _temployeService.Get(personne.typeEmploye.id);
                    _employeService.Create(perso);

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
                _personneService.Delete(idToDelete);
                _personneService.Save();
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
        /*[HttpGet]
        public ActionResult Detail(int id)
        {
            DetailEmployeViewModel modelOut = new DetailEmployeViewModel();

            if (id != 0)
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
                ).ToList();

            return View(modelOut);
        }*/

        /// <summary>
        /// Permet de modifier les informations d'un employé depuis la vue détaillée
        /// </summary>
        /// <param name="modelIn"></param>
        /// <returns></returns>
        /*[HttpPost]
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
                    _personneService.Update(personne);
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
                    _personneService.Create(personne);
                    FlashMessage.Confirmation("Employé créé avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de l'ajout de l'employé");
                }

            }

            _personneService.Save();

            return RedirectToAction("Index");
        }*/

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
        /// <returns>List<SelectListItem></returns>
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
        /// <returns>List<SelectListItem></returns>
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