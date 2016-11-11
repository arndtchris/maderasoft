using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Models;

namespace MaderaSoft.Controllers
{
    public class AdresseController : Controller
    {
        /*
         * Toute la logique métier doit être centralisée dans la couche Service
         * Un controller doit se contenter d'appeler les différentes fonctions dont il a besoin depuis la couche service
         * Dans certains cas, des traitements peuvent être réalisée selon a complexité de l'affichage à réaliser
         * Ex : si un Model combine plusieurs objets métiers.
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
            AdresseViewModel modelOut = new AdresseViewModel();

            /*
             * Pour transporter un minimum d'informationon récupère directement un model allégé (DTO) au lieu d'un Plain Object 
             */ 
            modelOut.lesAdresses = Mapper.Map<List<Adresse>, List<AdresseDTO>>(adresseService.GetAdresses().ToList());

            return View(modelOut);
        }

        //POST : créé ou met à jour une adresse
        [HttpPost]
        public ActionResult Edit(AdresseDTO modelIn)
        {
            //Ici AdresseModel est un DTO (Data Transfert Object) qui contient les données saisies dans le formulaire
            //Pour pouvoir sauvegarder ces données, il faut établir une correspondance entre les attributs d'AdresseModel 
            //et la table Adresse, c'est ce qui est réalisé ici
            var adresseATraiter = Mapper.Map<AdresseDTO, Adresse>(modelIn);

            if (adresseATraiter.AdresseID != 0)//si notre objet contient déjà un ID, il s'agit d'une mise à jour
            {
                adresseService.UpdateAdresse(adresseATraiter);
            }
            else//Si notre objet n'a pas d'ID, cela veut dire que nous devons créer une nouvelle adresse
            {
                //Une fois la correspondance effectuée, nous demandons au service adéquat de créer une nouvelle entrée
                adresseService.CreateAdresse(adresseATraiter);
            }


            //Après avoir défini toutes les nouvelles entrées à réaliser en bdd, 
            //on demande au Pattern UnitOfWork de réaliser les transactions nécessaire pour assurer la persistence des données
            //En effet la méthode saveAdresse() appelle unitOfWork.Commit();
            adresseService.saveAdresse();

            //On met en place une redirection pour afficher de nouveau l'ensemble des adresses
            return RedirectToAction("Index");
        }


    }
}