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
        private readonly IUnitOfWork unitOfWork;


        public AdresseService(IAdresseRepository adresseRepository, IUnitOfWork unitOfWork)
        {
            this.adresseRepository = adresseRepository;
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
             adresseRepository.Insert(adresse);            
        }

        /// <summary>
        /// Met à jour une adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void UpdateAdresse(Adresse adresse)
        {
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
            adresseRepository.Delete(this.GetAdresse(id));            
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