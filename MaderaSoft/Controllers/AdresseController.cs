﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Models;
using MaderaSoft.Models.ViewModel;
using MaderaSoft.Models.DTO;
using Vereyon.Web;

namespace MaderaSoft.Controllers
{
    public class AdresseController : Controller
    {
        /*
         * Toute la logique métier doit être centralisée dans la couche Service
         * Un controller doit se contenter d'appeler les différentes fonctions dont il a besoin depuis la couche service
         * Dans certains cas, des traitements peuvent être réalisée selon a complexité de l'affichage à réaliser
         * Ex : si un Model combine plusieurs objets de types différents.
         */


        private readonly IAdresseService adresseService;

        public AdresseController(IAdresseService adresseService)
        {
            this.adresseService = adresseService;
        }

        // GET: Adresse
        [HttpGet]
        public ActionResult Index()
        {
            AdresseIndexViewModel modelOut = new AdresseIndexViewModel();
            modelOut.tableauAdresses.typeObjet = "Adresse";

            /*
             * Pour transporter un minimum d'informationon récupère directement un model allégé (DTO) au lieu d'un Plain Object 
             */ 
            List<AdresseDTO> lesAdresses = Mapper.Map<List<Adresse>, List<AdresseDTO>>(adresseService.DonneTous().ToList());

            //On initialise le première ligne du tableau qui permettra d'en construire l'entête
            modelOut.tableauAdresses.lesLignes.Add(new List<object> { "Rue", "Ville", "Code postal", "Pays",""});
            modelOut.tableauAdresses.messageSiVide = "Aucune adresse n'a été saisie.";

            //On rempli ensuite les autres lignes avec les données correspondantes
            foreach (AdresseDTO adresseDTO in lesAdresses)
            {
                modelOut.tableauAdresses.lesLignes.Add(new List<object> {String.Format(adresseDTO.numRue + " " + adresseDTO.nomRue), adresseDTO.ville, adresseDTO.codePostal,adresseDTO.pays,adresseDTO.id.ToString()});
            }

            return View(modelOut);
        }

        //GET : renvoit un formulaire permettant de mofifier l'adresse correspondante à l'ID reçu
        [HttpGet]
        public ActionResult EditModal(int? id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();

            if(id.HasValue)//Si on id est transmis on reprend les valeurs de l'adresse correspondante
                modelOut.objet = Mapper.Map<Adresse, AdresseDTO>(adresseService.Get(id.Value));
            else//Sinon on instanci une nouvelle adresse
                modelOut.objet = new AdresseDTO();

            modelOut.formulaireUrl = "~/Views/Adresse/Edit.cshtml";
            modelOut.titreModal = "Edition d'une adresse";

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml",modelOut);

        }

        //POST : créé ou met à jour une adresse
        [HttpPost]
        public ActionResult Edit(AdresseDTO modelIn)
        {
            //On vérifie la validité du model récupéré
            //Si le modèle n'est pas valide, on renvoit la vue avec les données reçus
            if (!ModelState.IsValid)
            {
                BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
                modelOut.objet = modelIn;
                modelOut.formulaireUrl = "~/Views/Adresse/Edit.cshtml";
                modelOut.titreModal = "Edition d'une adresse";

                return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
            }

            //Ici AdresseModel est un DTO (Data Transfert Object) qui contient les données saisies dans le formulaire
            //Pour pouvoir sauvegarder ces données, il faut établir une correspondance entre les attributs d'AdresseModel 
            //et la table Adresse, c'est ce qui est réalisé ici
            var adresseATraiter = Mapper.Map<AdresseDTO, Adresse>(modelIn);

            if (adresseATraiter.id != 0) //si notre objet contient déjà un ID, il s'agit d'une mise à jour
            {
                try
                {
                    FlashMessage.Confirmation("Adresse mise à jour avec succes");
                    adresseService.Update(adresseATraiter);

                    //Après avoir défini toutes les nouvelles entrées à réaliser en bdd, 
                    //on demande au Pattern UnitOfWork de réaliser les transactions nécessaire pour assurer la persistence des données
                    //En effet la méthode saveAdresse() appelle unitOfWork.Commit();
                    adresseService.Save();
                }
                catch (Exception)
                {

                    FlashMessage.Danger("Erreur lors de la mise à jour");
                }
            }
            else //Si notre objet n'a pas d'ID, cela veut dire que nous devons créer une nouvelle adresse
            {
                //Une fois la correspondance effectuée, nous demandons au service adéquat de créer une nouvelle entrée
                try
                {
                    FlashMessage.Confirmation("Adresse ajoutée avec succès");
                    adresseService.Create(adresseATraiter);

                    //Après avoir défini toutes les nouvelles entrées à réaliser en bdd, 
                    //on demande au Pattern UnitOfWork de réaliser les transactions nécessaire pour assurer la persistence des données
                    //En effet la méthode saveAdresse() appelle unitOfWork.Commit();
                    adresseService.Save();
                }
                catch (Exception e)
                {
                    FlashMessage.Danger("Erreur lors de la sauvegarde");
                }
            }

            //On met en place une redirection pour afficher de nouveau l'ensemble des adresses
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            BootstrapModalViewModel modelOut = new BootstrapModalViewModel();
            //modelOut.typeObjet = "Adresse";
            modelOut.formulaireUrl = "~/Views/Shared/_BootstrapDeleteModalPartial.cshtml";
            modelOut.titreModal = "Suppression d'une adresse";
            modelOut.objet = new BootstrapDeleteModalViewModel { idToDelete = id, message = "Etes vous sûr de vouloir supprimer cette adresse ?", method="Delete",urlController="Adresse"};

            return PartialView("~/Views/Shared/_BootstrapModalPartial.cshtml", modelOut);
        }

        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            try
            {
                FlashMessage.Confirmation("Suppression de l'adresse");
                adresseService.Delete(idToDelete);
                adresseService.Save();
            }
            catch (Exception)
            {
                FlashMessage.Danger("Erreur lors de la suppression de l'adresse");
            }

            return RedirectToAction("Index");
        }

    }
}