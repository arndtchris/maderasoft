using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Areas.GestionStock.Models.DTOs;
using MaderaSoft.Areas.GestionStock.Models.ViewModels;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using Vereyon.Web;

namespace MaderaSoft.Areas.GestionStock.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IServiceService _serviceService;


        public StockController(IStockService stockService, IServiceService serviceService)
        {
            this._stockService = stockService;
            this._serviceService = serviceService;
        }

        // GET: GestionStock/Stocks
        public ActionResult Index()
        {
            StockIndexViewModel modelOut = new StockIndexViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();
            modelOut.tableauComposants.typeObjet = "GestionStock/Stocks";
            modelOut.tableauComposants.avecActionCrud = true;
            modelOut.tableauComposants.messageSiVide = "Aucun composant n'a été ajouté à l'application.";

            List<StockDTO> lesComposants = Mapper.Map<List<Composant>, List<StockDTO>>(_stockService.GetComposants().ToList());

            modelOut.tableauComposants.lesLignes.Add(new List<object> { "", "Nom Composant", "Quantité", "Gamme", "Prix fournisseur", "Nom fournisseur", "" });

            foreach (StockDTO composant in lesComposants)
            {
                button = new BootstrapButtonViewModel
                {
                    href = Url.Action("Detail", "Composant", new { area = "GestionStock", id = composant.id }).ToString(),
                    cssClass = "",
                    libe = " ",
                    typeDeBouton = Parametres.TypeBouton.Detail
                };
                modelOut.tableauComposants.lesLignes.Add(new List<object> { button, composant.libe, composant.qteStock.ToString(), composant.gamme.libe.ToString(), composant.prixHT.ToString(), composant.fournisseur.login.ToString() });
            }


            return View(modelOut);
        }

        [HttpGet]
        public ActionResult CreateModal()
        {

            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            EditStockViewModel editComposant = new EditStockViewModel();
            BootstrapButtonViewModel button = new BootstrapButtonViewModel();

            //if (id.HasValue)
            //{
            //    editComposant.composant = Mapper.Map<Composant, StockDTO>(_stockService.GetUnComposant(id.Value));
            //    modelOut.titreModal = string.Format("Modification des informations d'un composant");
            //}
            //else
            //{
            //    modelOut.titreModal = "Edition d'un composant";
            //}


            modelOut.formulaireUrl = "~/Areas/GestionStock/Views/Stocks/_EditStockPartial.cshtml";
            modelOut.titreModal = string.Format("Modification des informations du composant");
            modelOut.objet = editComposant;

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

        }

        [HttpPost]
        public ActionResult Edit(StockDTO composant)
        {
            
            Composant cpst = new Composant();

                try
                {
                    cpst = _stockService.GetUnComposant(composant.id);
                    _stockService.UpdateComposant(cpst);

                    FlashMessage.Confirmation("Composant mis à jour avec succès");
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de mis à jour du composant");
                }
            

            _stockService.saveComposant();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            modelOut.typeObjet = "GestionStock/Stocks";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'un composant";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer ce composant ?" };

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            try
            {
                FlashMessage.Confirmation("Suppression du composant");
                _stockService.deleteComposant(idToDelete);
                _stockService.saveComposant();
            }
            catch (Exception)
            {
                FlashMessage.Danger("Erreur lors de la suppression du composant");
                throw;
            }

            return RedirectToAction("Index");
        }
    }
}