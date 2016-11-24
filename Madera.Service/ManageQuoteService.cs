using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Service
{
    public class ManageQuoteService : IManageQuoteService
    {
        private readonly IDevisFactureRepository devisRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork unitOfWork;


        public ManageQuoteService(IDevisFactureRepository devisRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this.devisRepository = devisRepository;
            this._applicationTraceService = applicationTraceService;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retourne toutes les adresses contenu en base de données
        /// </summary>
        /// <returns>IEnumerable<Adresse></returns>
        public IEnumerable<DevisFacture> GetLesDevis()
        {
            return devisRepository.GetAll();
        }
        /// <summary>
        /// Retourne l'adresse correspondante à l'id renseignée
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Adresse</returns>
        public DevisFacture GetUnDevis(int id)
        {
            return devisRepository.GetById(id);
        }


        /// <summary>
        /// Met à jour une adresse en base de données
        /// </summary>
        /// <param name="adresse"></param>
        public void UpdateDevis(DevisFacture devis)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'adresse adrs_id = {0}", devis.id),
            });

            devisRepository.Update(devis);
        }

        public void saveDevis()
        {
            unitOfWork.Commit();
        }

        /// <summary>
        /// Supprime l'adresse correspondante à l'id renseigné
        /// </summary>
        /// <param name="id"></param>
        public void deleteDevis(int id)
        {
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de l'adresse adrs_id = {0}", id),
            });
            devisRepository.Delete(x => x.id == id);
        }
    }

    //Définition des méthodes qui seront accessibles depuis la couche de présentation
    //A ce niveau on peut définir des méthodes AjouteAdresseEtDonneListe() qui va combiner plusieurs méthodes basiques du pattern Repository
    public interface IManageQuoteService
    {
        IEnumerable<DevisFacture> GetLesDevis();
        DevisFacture GetUnDevis(int id);
        void UpdateDevis(DevisFacture devis);
        void saveDevis();
        void deleteDevis(int id);
    }

}
