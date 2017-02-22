using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Service
{
    public class EtageService : IEtageService
    {

        private readonly IEtageRepository etageRepository;
        private readonly IApplicationTraceService applicationTraceService;
        private readonly IUnitOfWork unitOfWork;

        public EtageService(IEtageRepository _etageRepository, IUnitOfWork _unitOfWork, IApplicationTraceService _applicationTraceService)
        {
            this.etageRepository = _etageRepository;
            this.applicationTraceService = _applicationTraceService;
            this.unitOfWork = _unitOfWork;
        }

        public Etage Get(int id)
        {
            return etageRepository.GetById(id);
        }

        public void Create(Etage item)
        {
            etageRepository.Insert(item);

            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un étage",
            });
        }

        public void Update(Etage item)
        {
            etageRepository.Update(item);

            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un étage etage_id = {0}", item.id),
            });
        }

        public void Delete(int id)
        {
            etageRepository.Delete(x => x.id == id);

            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un étage etage_id = {0}", id),
            });
        }

        public IEnumerable<Etage> DonneTous()
        {
            return etageRepository.GetAll();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }

    public interface IEtageService : IService<Etage>
    {
        /*
         * Pour ajouter des méthodes propres à cet objet, écrire le corps de ces méthodes dans cette interface 
         */
    }
}