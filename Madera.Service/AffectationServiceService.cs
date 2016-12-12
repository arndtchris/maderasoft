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

        public void CreateAffectationService(AffectationService affectationService)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                //description = String.Format("Affectation de l'utilisateur {0} {1} au service {2} en tant que {3}", affectationService.groupe.libe, affectationService.employe.personne.nom, affectationService.service.libe, affectationService.groupe.libe),
            });

            _affectationServiceRepository.Insert(affectationService);
        }

        public void deleteAffectationService(int id)
        {
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'une affectation à un service adrs_id = {0}", id),
            });
            _affectationServiceRepository.Delete(x => x.id == id);
        }

        public AffectationService GetAffectationService(int id)
        {
            return _affectationServiceRepository.GetById(id);
        }

        public IEnumerable<AffectationService> GetAffectationServices()
        {
            return _affectationServiceRepository.GetAll();
        }

        public void saveAffectationService()
        {
            _unitOfWork.Commit();
        }

        public void UpdateAffectationService(AffectationService affectationService)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                //description = String.Format("Mise à jour de l'affectation de l'utilisateur {0} {1} au service {2} en tant que {3}", affectationService.groupe.libe, affectationService.employe.personne.nom, affectationService.service.libe, affectationService.groupe.libe),
            });

            _affectationServiceRepository.Update(affectationService);
        }
    }

    public interface IAffectationServiceService
    {
        IEnumerable<AffectationService> GetAffectationServices();
        AffectationService GetAffectationService(int id);
        void CreateAffectationService(AffectationService affectationService);
        void UpdateAffectationService(AffectationService affectationService);
        void saveAffectationService();
        void deleteAffectationService(int id);
    }
}