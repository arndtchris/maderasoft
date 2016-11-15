using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;
using Vereyon.Web;

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
            return adresseRepository.GetAll().Where(c => String.Equals(c.pays.ToUpper(), country.ToUpper()));
        }


        /// <summary>
        /// Retourne l'adresse correspondante à l'id renseignée
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Adresse</returns>
        public Adresse GetAdresse(int id)
        {
            var adresse = adresseRepository.GetById(id);
            return adresse;
        }

        /// <summary>
        /// Insert une nouvelle adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void CreateAdresse(Adresse adresse)
        {

            try
            {
                FlashMessage.Confirmation("Adresse ajoutée avec succes");
                adresseRepository.Insert(adresse);
            }
            catch (Exception e)
            {
                FlashMessage.Danger("Erreur lors de la sauvegarde");
            }
            
        }

        /// <summary>
        /// Met à jour une adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void UpdateAdresse(Adresse adresse)
        {
            try
            {
                FlashMessage.Confirmation("Adresse mise à jour avec succes");
                adresseRepository.Update(adresse);
            }
            catch (Exception)
            {

                FlashMessage.Danger("Erreur lors de la mise à jour");
            }
            
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
            try
            {
                FlashMessage.Confirmation("Suppression de l'adresse");
                adresseRepository.Delete(this.GetAdresse(id));
            }
            catch (Exception)
            {
                FlashMessage.Danger("Erreur lors de la suppression de l'adresse");
                throw;
            }
            
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