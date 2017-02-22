using Madera.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MaderaSoft.Models;
using AutoMapper;
using Madera.Model;
using Vereyon.Web;
using MaderaSoft.Models.ViewModel;
using MaderaSoft.Mapping;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Controllers
{
    public class DevisFactureController : Controller
    {
        /*
         * Toute la logique métier doit être centralisée dans la couche Service
         * Un controller doit se contenter d'appeler les différentes fonctions dont il a besoin depuis la couche service
         * Dans certains cas, des traitements peuvent être réalisée selon a complexité de l'affichage à réaliser
         * Ex : si un Model combine plusieurs objets de types différents.
         */


        private readonly IDevisFactureService devisfactureService;

        public DevisFactureController(IDevisFactureService devisfactureService)
        {
            this.devisfactureService = devisfactureService;
        }

        // GET: DevisFacture
        [HttpGet]
        public ActionResult Index()
        {
            DevisFactureIndexViewModel modelOut = new DevisFactureIndexViewModel();
            modelOut.tableauDevisFactures.typeObjet = "DevisFacture";
            
            /*
             * Pour transporter un minimum d'informationon récupère directement un model allégé (DTO) au lieu d'un Plain Object 
             */
            List<DevisFactureDTO> lesDevis = Mapper.Map<List<DevisFacture>, List<DevisFactureDTO>>(devisfactureService.DonneTous().ToList());

            //On initialise le première ligne du tableau qui permettra d'en construire l'entête
            /*modelOut.tableauDevisFactures.lesLignes.Add(new List<string> { "Numéro devis", "Devis signé", "Devis supprimé", "Numéro projet", "Référent", ""});

            //On rempli ensuite les autres lignes avec les données correspondantes
            foreach (DevisFactureDTO devisFactureDTO in lesDevis)
            {
                modelOut.tableauDevisFactures.lesLignes.Add(new List<string> { String.Format(devisFactureDTO.id.ToString(), devisFactureDTO.isSigned.ToString(), devisFactureDTO.isDeleted.ToString(), devisFactureDTO.projet.id.ToString(), devisFactureDTO.employe.id.ToString(), devisFactureDTO.id.ToString() )});
            }*/

            return View(modelOut);
        }

        //GET : renvoit un formulaire permettant de mofifier le devis correspondant à l'ID reçu
        [HttpGet]
        public ActionResult EditModal(int? id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();

            if (id.HasValue)//Si on id est transmis on reprend les valeurs du devis correspondant
                Mapper.Map<DevisFacture, DevisFactureDTO>(devisfactureService.Get(id.Value));
            else//Sinon on instancie un nouveau devis
                modelOut.objet = new DevisFactureDTO();

            modelOut.formulaireUrl = "~/Views/ManageQuote/Edit.cshtml";
            modelOut.titreModal = "Edition d'un devis";

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);

        }

        //POST : créé ou met à jour un devis
        [HttpPost]
        public ActionResult Edit(DevisFactureDTO modelIn)
        {
            //On vérifie la validité du model récupéré
            //Si le modèle n'est pas valide, on renvoit la vue avec les données reçus
            if (!ModelState.IsValid)
            {
                BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
                modelOut.objet = modelIn;
                modelOut.formulaireUrl = "~/Views/ManageQuote/Edit.cshtml";
                modelOut.titreModal = "Edition d'un devis";

                return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
            }

            //Ici DevisFactureModel est un DTO (Data Transfert Object) qui contient les données saisies dans le formulaire
            //Pour pouvoir sauvegarder ces données, il faut établir une correspondance entre les attributs de DevisFactureModel 
            //et la table DevisFacture, c'est ce qui est réalisé ici
            var devisATraiter = Mapper.Map<DevisFactureDTO, DevisFacture>(modelIn);

            if (devisATraiter.id != 0) //si notre objet contient déjà un ID, il s'agit d'une mise à jour
            {
                try
                {
                    FlashMessage.Confirmation("Devis mis à jour avec succès");
                    devisfactureService.Update(devisATraiter);

                    //Après avoir défini toutes les nouvelles entrées à réaliser en bdd, 
                    //on demande au Pattern UnitOfWork de réaliser les transactions nécessaire pour assurer la persistence des données
                    //En effet la méthode saveDevis() appelle unitOfWork.Commit();
                    devisfactureService.Save();
                }
                catch (Exception)
                {

                    FlashMessage.Danger("Erreur lors de la mise à jour");
                }
            }
            

            //On met en place une redirection pour afficher de nouveau l'ensemble des devis
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            modelOut.typeObjet = "Devis";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'un devis";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer ce devis ?" };

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            try
            {
                FlashMessage.Confirmation("Suppression du devis");
                devisfactureService.Delete(idToDelete);
                devisfactureService.Save();
            }
            catch (Exception)
            {
                FlashMessage.Danger("Erreur lors de la suppression du devis");
                throw;
            }

            return RedirectToAction("Index");
        }

    }

}
