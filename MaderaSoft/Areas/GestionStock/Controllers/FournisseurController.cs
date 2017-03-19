using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Areas.GestionStock.Models.ViewModels;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace MaderaSoft.Areas.GestionStock.Controllers
{
    public class FournisseurController : Controller
    {
        private readonly IComposantService _composantService;
        private readonly IServiceService _serviceService;
        private readonly IGammeService _gammeService;
        private readonly IPersonneService _personneService;
        private readonly string _service;


        public FournisseurController(IComposantService composantService, IServiceService serviceService, IGammeService gammeService, IPersonneService personneService)
        {
            this._composantService = composantService;
            this._serviceService = serviceService;
            this._gammeService = gammeService;
            this._personneService = personneService;
            this._service = "Gestion des stocks";
        }
        // GET: GestionStock/Fournisseur
        public ActionResult Index()
        {
            Session["service"] = _service;
            FournisseurIndexViewModel modelOut = new FournisseurIndexViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();
            modelOut.tableauFournisseurs.typeObjet = "GestionStock/Fournisseur";
            modelOut.tableauFournisseurs.avecActionCrud = true;
            modelOut.tableauFournisseurs.messageSiVide = "Aucun fournisseur n'a été ajouté à l'application.";

            List<PersonneDTO> lesFournisseurs = Mapper.Map<List<Personne>, List<PersonneDTO>>(_personneService.DonneTousFournisseurs().ToList());

            modelOut.tableauFournisseurs.lesLignes.Add(new List<object> { "Nom", "Adresse", "Mail", ""});

            foreach ( PersonneDTO prs in lesFournisseurs)
            {
                modelOut.tableauFournisseurs.lesLignes.Add(new List<object> { prs.nom, prs.adresse.numRue.ToString() + " " + prs.adresse.nomRue + " " + prs.adresse.codePostal + " " + prs.adresse.ville, prs.email, prs.id });
            }

            return View(modelOut);

        }

        [HttpGet]
        public ActionResult EditModal(int? id)
        {
            Session["service"] = _service;
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            CreateFournisseurViewModel editFournisseur = new CreateFournisseurViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();

            //if (id.HasValue)
            //{
            //    editComposant.composant = Mapper.Map<Composant, StockDTO>(_composantService.GetUnComposant(id.Value));
            //    modelOut.titreModal = string.Format("Modification des informations d'un composant");
            //}
            //else
            //{
            //    modelOut.titreModal = "Edition d'un composant";
            //}

            if (id.HasValue)
            {
                editFournisseur.fournisseur = Mapper.Map<Personne, PersonneDTO>(_personneService.Get(id.Value));
                modelOut.titreModal = string.Format("Modification d'un fournisseur");
            }
            else
            {
                modelOut.titreModal = string.Format("Ajout d'un fournisseur");
            }

            modelOut.formulaireUrl = "~/Areas/GestionStock/Views/Fournisseur/_EditFournisseurPartial.cshtml";
            modelOut.objet = editFournisseur;

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

        }

        [HttpPost]
        public ActionResult Edit(PersonneDTO fournisseur)
        {

            Personne frnsr = new Personne();

            if (fournisseur.id != 0)//update
            {
                try
                {
                    frnsr = _personneService.Get(fournisseur.id);
                    frnsr.nom = fournisseur.nom;
                    frnsr.email = fournisseur.email;
                    frnsr.adresse.numRue = fournisseur.adresse.numRue;
                    frnsr.adresse.nomRue = fournisseur.adresse.nomRue;
                    frnsr.adresse.ville = fournisseur.adresse.ville;
                    frnsr.adresse.pays = fournisseur.adresse.pays;
                    frnsr.adresse.codePostal = fournisseur.adresse.codePostal;
                    _personneService.Update(frnsr, _donneNomPrenomUtilisateur());

                    FlashMessage.Confirmation("Fournisseur mis à jour avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de la mise à jour du fournisseur");
                }
            }
            else
            {
                try
                {
                    frnsr = Mapper.Map<PersonneDTO, Personne>(fournisseur);
                    frnsr.isFournisseur = true;
                    frnsr.isClient = false;

                    _personneService.Create(frnsr, _donneNomPrenomUtilisateur());

                    FlashMessage.Confirmation("Fournisseur créé avec succès.");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de l'ajout du fournisseur");
                }

            }
            _personneService.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            Session["service"] = _service;
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            modelOut.typeObjet = "GestionStock/Fournisseur";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'un fournisseur";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer ce fournisseur ?", method = "Delete", urlController = "Fournisseur" };

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            try
            {
                FlashMessage.Confirmation("Suppression du fournisseur");
                _personneService.Delete(idToDelete, _donneNomPrenomUtilisateur());
                _personneService.Save();
            }
            catch (Exception)
            {
                FlashMessage.Danger("Erreur lors de la suppression du composant");
                throw;
            }

            return RedirectToAction("Index");
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