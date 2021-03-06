﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class ModuleService : IModuleService
    {

        private readonly IModuleRepository _moduleRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleService(IModuleRepository moduleRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._moduleRepository = moduleRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public void Create(Module module, string user = "")
        {
            module.coupePrincipe = "string";
            _moduleRepository.Insert(module);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Creation.ToString(),
                description = String.Format("Ajout d'un nouveau module"),
            });

        }

        public void Delete(int id, string user = "")
        {
            _moduleRepository.Delete(x => x.id == id);
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression du module_id = {0}", id),
            });
            
        }
    

        public Module Get(int id)
        {
            return _moduleRepository.GetById(id);
        }

        public IEnumerable<Module> DonneTous()
        {
            return _moduleRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Module module, string user = "")
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                description = String.Format("Mise à jour du module_id = {0}", module.id),
                action = Parametres.Action.Modification.ToString()
            });

            _moduleRepository.Update(module);
        }
    }

    public interface IModuleService : IService<Module>
    {
        

    }
}