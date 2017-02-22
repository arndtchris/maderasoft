using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class AffectationServiceService : IAffectationServiceService
    {
        private readonly IAffectationServiceRepository _affectationServiceRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public AffectationServiceService(IAffectationServiceRepository affectationServiceRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._affectationServiceRepository = affectationServiceRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public void Create(AffectationService affectationService)
        {

            _affectationServiceRepository.Insert(affectationService);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = String.Format("Ajout d'une nouvelle affectation au service {0} en tant que {1}", affectationService.service.libe, affectationService.groupe.libe),
            });
        }

        public void Delete(int id)
        {
            _affectationServiceRepository.Delete(x => x.id == id);

            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'une affectation à un service affec_id = {0}", id)
            });

        }

        public AffectationService Get(int id)
        {
            return _affectationServiceRepository.GetById(id);
        }

        public IEnumerable<AffectationService> DonneTous()
        {
            return _affectationServiceRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(AffectationService affectationService)
        {

            _affectationServiceRepository.Update(affectationService);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'affectation affec_id = {0} au service {1} en tant que {2}", affectationService.id, affectationService.service.libe, affectationService.groupe.libe)
            });
        }
    }

    public interface IAffectationServiceService : IService<AffectationService>
    {
        /*
         * Pour ajouter des méthodes propres à cet objet, écrire le corps de ces méthodes dans cette interface 
         */
    }
}