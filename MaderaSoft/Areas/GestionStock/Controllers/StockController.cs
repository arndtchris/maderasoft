﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Areas.GestionStock.Models.ViewModels;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using Vereyon.Web;

namespace MaderaSoft.Areas.GestionStock.Controllers
{
    public class StockController : Controller
    {
        private readonly IComposantService _composantService;
        private readonly IServiceService _serviceService;
        private readonly IGammeService _gammeService;
        private readonly IPersonneService _personneService;


        public StockController(IComposantService composantService, IServiceService serviceService, IGammeService gammeService, IPersonneService personneService)
        {
            this._composantService = composantService;
            this._serviceService = serviceService;
            this._gammeService = gammeService;
            this._personneService = personneService;
        }

        // GET: GestionStock/Stocks
        public ActionResult Index()
        {
            StockIndexViewModel modelOut = new StockIndexViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();
            modelOut.tableauComposants.typeObjet = "GestionStock/Stock";
            modelOut.tableauComposants.avecActionCrud = true;
            modelOut.tableauComposants.messageSiVide = "Aucun composant n'a été ajouté à l'application.";

            List<ComposantDTO> lesComposants = Mapper.Map<List<Composant>, List<ComposantDTO>>(_composantService.DonneTous().ToList());

            modelOut.tableauComposants.lesLignes.Add(new List<object> {"Nom Composant", "Quantité", "Gamme", "Prix fournisseur", "Nom fournisseur", "" });

            foreach (ComposantDTO composant in lesComposants)
            {
                //button = new BootstrapButtonViewModel
                //{
                //    href = Url.Action("Detail", "Composant", new { area = "GestionStock", id = composant.id }).ToString(),
                //    cssClass = "",
                //    libe = " ",
                //    typeDeBouton = Parametres.TypeBouton.Detail
                //};
                modelOut.tableauComposants.lesLignes.Add(new List<object> { composant.libe, composant.qteStock.ToString(), composant.gamme.libe.ToString(), composant.prixHT.ToString(), composant.fournisseur.nom, composant.id });
            }


            return View(modelOut);
        }

        [HttpGet]
        public ActionResult EditModal(int? id)
        {

            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            CreateStockViewModel editComposant = new CreateStockViewModel();
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
                editComposant.composant = Mapper.Map<Composant, ComposantDTO>(_composantService.Get(id.Value));
            }
            editComposant.lesGammes = _donneListeGammes();
            //On prépare le fournisseur
            List<Personne> listFrnsr = _personneService.DonneTousFournisseurs();
            editComposant.lesFournisseurs = _donneListeFournisseurs() ;
            modelOut.formulaireUrl = "~/Areas/GestionStock/Views/Stock/_EditStockPartial.cshtml";
            modelOut.titreModal = string.Format("Modification des informations du composant");
            modelOut.objet = editComposant;

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

        }

        private List<SelectListItem> _donneListeFournisseurs()
        {
            List<SelectListItem> lesFournisseurs = new List<SelectListItem>();

            //On récupère la liste des services disponibles dans l'application
            lesFournisseurs = _personneService.DonneTousFournisseurs().Select(
                x => new SelectListItem()
                {
                    Text = x.nom,
                    Value = x.id.ToString()
                }
                ).ToList();
            lesFournisseurs.Insert(0, new SelectListItem() { Text = "--- Sélectionnez ---", Value = "" });

            return lesFournisseurs;
        }

        private List<SelectListItem> _donneListeGammes()
        {
            List<SelectListItem> lesGammes = _gammeService.DonneTous().Select(
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
        public ActionResult Edit(ComposantDTO composant)
        {

            Composant cpst = new Composant();
            Personne frnsr = new Personne();

            if (composant.id != 0)//update
            {
                try
                {

                    cpst = _composantService.Get(composant.id);
                    cpst.libe = composant.libe;
                    cpst.prixHT = composant.prixHT;
                    cpst.qteStock = composant.qteStock;
                    //cpst.fournisseur = 
                    //cpst.gamme = _tmoduleService.Get(composant.gamme.id);
                    //mdl = Mapper.Map<ModuleDTO, Module>(module);
                    _composantService.Update(cpst, _donneNomPrenomUtilisateur());

                    FlashMessage.Confirmation("Module mis à jour avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de la mise à jour du composant");
                }
            }
            else
            {
                try
                {
                    cpst = Mapper.Map<ComposantDTO, Composant>(composant);


                    //On prépare le fournisseur
                    cpst.fournisseur = _personneService.Get(composant.fournisseur.id);
                    //On prépare la gamme
                    cpst.gamme = _gammeService.Get(composant.gamme.id);
                    
                    _composantService.Create(cpst, _donneNomPrenomUtilisateur());

                    FlashMessage.Confirmation("Module créé avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de l'ajout du composant");
                }

            }
            _composantService.Save();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            modelOut.typeObjet = "GestionStock/Stock";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'un composant";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer ce composant ?", method = "Delete", urlController = "Stock" };

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            try
            {
                FlashMessage.Confirmation("Suppression du composant");
                _composantService.Delete(idToDelete, _donneNomPrenomUtilisateur());
                _composantService.Save();
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