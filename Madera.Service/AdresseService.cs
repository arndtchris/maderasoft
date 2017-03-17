using System;
using System.Collections.Generic;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class AdresseService : IAdresseService
    {
        private readonly IAdresseRepository _adresseRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;


        public AdresseService(IAdresseRepository adresseRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._adresseRepository = adresseRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retourne toutes les adresses contenu en base de données
        /// </summary>
        /// <returns>IEnumerable<Adresse></returns>
        public IEnumerable<Adresse> DonneTous()
        {
            return _adresseRepository.GetAll();
        }

        /// <summary>
        /// Retourne une liste d'adresses correspondant au pays renseigné
        /// </summary>
        /// <param name="country"></param>
        /// <returns>IEnumerable<Adresse></returns>
        public IEnumerable<Adresse> GetAdressesByCountry(string country)
        {
            return _adresseRepository.GetAdresses(country);
        }


        /// <summary>
        /// Retourne l'adresse correspondante à l'id renseignée
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Adresse</returns>
        public Adresse Get(int id)
        {
            return _adresseRepository.GetById(id);
        }

        /// <summary>
        /// Insert une nouvelle adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void Create(Adresse adresse, string user = "")
        {
            _applicationTraceService.create(new ApplicationTrace{
                utilisateur = user,
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'une nouvelle adresse",
            });

            _adresseRepository.Insert(adresse);            
        }

        /// <summary>
        /// Met à jour une adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void Update(Adresse adresse, string user = "")
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'adresse adrs_id = {0}",adresse.id),
            });

            _adresseRepository.Update(adresse);      
        }

        /// <summary>
        /// Supprime l'adresse correspondante à l'id renseigné
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id, string user = "")
        {
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de l'adresse adrs_id = {0}", id),
            });
            _adresseRepository.Delete(x => x.id == id);
        }

        /// <summary>
        /// Permet de fermer et rendre effective une série de requêtes sql
        /// </summary>
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }

    //Définition des méthodes qui seront accessibles depuis la couche de présentation
    //A ce niveau on peut définir des méthodes AjouteAdresseEtDonneListe() qui va combiner plusieurs méthodes basiques du pattern Repository
    public interface IAdresseService : IService<Adresse>
    {
        IEnumerable<Adresse> GetAdressesByCountry(string country);
    }
}