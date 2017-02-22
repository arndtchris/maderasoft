using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Service
{
    public class PositionModuleService : IPositionModuleService
    {
        private readonly IPositionModuleRepository positionModuleRepository;
        private readonly IApplicationTraceService applicationTraceService;
        private readonly IUnitOfWork unitOfWork;

        public PositionModuleService(IPositionModuleRepository _positionModuleRepository, IUnitOfWork _unitOfWork, IApplicationTraceService _applicationTraceService)
        {
            this.positionModuleRepository = _positionModuleRepository;
            this.applicationTraceService = _applicationTraceService;
            this.unitOfWork = _unitOfWork;
        }

        public PositionModule Get(int id)
        {
            return positionModuleRepository.GetById(id);
        }

        public void Create(PositionModule item)
        {
            positionModuleRepository.Insert(item);

            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un élément position module",
            });
        }

        public void Update(PositionModule item)
        {
            positionModuleRepository.Update(item);

            applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un élément position module positionModule_id = {0}", item.id),
            });
        }

        public void Delete(int id)
        {
            positionModuleRepository.Delete(x => x.id == id);

            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un élément position module positionModule_id = {0}", id),
            });
        }

        public IEnumerable<PositionModule> DonneTous()
        {
            return positionModuleRepository.GetAll();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }

    public interface IPositionModuleService : IService<PositionModule>
    {
        /*
         * Pour ajouter des méthodes propres à cet objet, écrire le corps de ces méthodes dans cette interface 
         */
    }
}