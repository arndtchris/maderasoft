﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class AdresseService : IAdresseService
    {
        private readonly IAdresseRepository adresseRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork unitOfWork;


        public AdresseService(IAdresseRepository adresseRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this.adresseRepository = adresseRepository;
            this._applicationTraceService = applicationTraceService;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retourne toutes les adresses contenu en base de données
        /// </summary>
        /// <returns>IEnumerable<Adresse></returns>
        public IEnumerable<Adresse> GetAdresses()
        {
            return adresseRepository.GetAll();
        }

        /// <summary>
        /// Retourne une liste d'adresses correspondant au pays renseigné
        /// </summary>
        /// <param name="country"></param>
        /// <returns>IEnumerable<Adresse></returns>
        public IEnumerable<Adresse> GetAdressesByCountry(string country)
        {
            return adresseRepository.GetAdresses(country);
        }


        /// <summary>
        /// Retourne l'adresse correspondante à l'id renseignée
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Adresse</returns>
        public Adresse GetAdresse(int id)
        {
            return adresseRepository.GetById(id);
        }

        /// <summary>
        /// Insert une nouvelle adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void CreateAdresse(Adresse adresse)
        {

            _applicationTraceService.create(new ApplicationTrace{
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'une nouvelle adresse",
                date = DateTime.Now
            });

             adresseRepository.Insert(adresse);            
        }

        /// <summary>
        /// Met à jour une adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void UpdateAdresse(Adresse adresse)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'adresse adrs_id = {0}",adresse.AdresseID),
                date = DateTime.Now
            });

            adresseRepository.Update(adresse);      
        }

        public void saveAdresse()
        {
            unitOfWork.Commit();
        }

        /// <summary>
        /// Supprime l'adresse correspondante à l'id renseigné
        /// </summary>
        /// <param name="id"></param>
        public void deleteAdresse(int id)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de l'adresse adrs_id = {0}", id),
                date = DateTime.Now
            });
            adresseRepository.Delete(x => x.AdresseID == id);            
        }
    }

    //Définition des méthodes qui seront accessibles depuis la couche de présentation
    //A ce niveau on peut définir des méthodes AjouteAdresseEtDonneListe() qui va combiner plusieurs méthodes basiques du pattern Repository
    public interface IAdresseService
    {
        IEnumerable<Adresse> GetAdresses();
        IEnumerable<Adresse> GetAdressesByCountry(string country);
        Adresse GetAdresse(int id);
        void CreateAdresse(Adresse adresse);
        void UpdateAdresse(Adresse adresse);
        void saveAdresse();
        void deleteAdresse(int id);
    }
}