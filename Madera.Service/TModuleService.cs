using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class TModuleService : ITModuleService
    {
        private readonly ITModuleRepository _tmoduleRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;


        public TModuleService(ITModuleRepository _tmoduleRepository, IUnitOfWork _unitOfWork, IApplicationTraceService _applicationTraceService)
        {
            this._tmoduleRepository = _tmoduleRepository;
            this._applicationTraceService = _applicationTraceService;
            this._unitOfWork = _unitOfWork;
        }

        public void Create(TModule TModule, string user = "")
        {
            _tmoduleRepository.Insert(TModule);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un type d'un type de module",
            });
        }

        public void Delete(int id, string user = "")
        {
            _tmoduleRepository.Delete(x => x.id == id);
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un type d'un type de module tmodule_id = {0}", id),
            });
        }

        public TModule Get(int id)
        {
           return _tmoduleRepository.GetById(id);
        }

        public IEnumerable<TModule> DonneTous()
        {
            return _tmoduleRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(TModule TModule, string user = "")
        {

            _tmoduleRepository.Update(TModule);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un type d'un type de module tmodule_id = {0}", TModule.id),
            });
        }
    }

    public interface ITModuleService : IService<TModule>
    {

    }
}