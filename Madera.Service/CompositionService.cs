using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Service
{
    public class CompositionService : ICompositionService
    {

        private readonly ICompositionRepository _compositionRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public CompositionService(ICompositionRepository compositionRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._compositionRepository = compositionRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public void Create(Composition composition, string user = "")
        {
            _compositionRepository.Insert(composition);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Creation.ToString(),
                description = String.Format("Ajout d'un composant {0} au module {1}", composition.composant.libe, composition.module.libe),
            });

        }

        public void Delete(int id, string user = "")
        {
            _compositionRepository.Delete(x => x.id == id);
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un composant dans le module_id = {1}", id),
            });
            
        }


        public Composition Get(int id)
        {
            return _compositionRepository.GetById(id);
        }

        public IEnumerable<Composition> DonneTous()
        {
            return _compositionRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Composition composition, string user = "")
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                description = String.Format("Mise à jour du composant {0} dans le module {1}", composition.composant.libe, composition.module.libe),
                action = Parametres.Action.Modification.ToString()
            });

            _compositionRepository.Update(composition);
        }
    }

    public interface ICompositionService : IService<Composition>
    {


    }
}