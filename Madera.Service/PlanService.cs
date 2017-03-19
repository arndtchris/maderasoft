using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Service
{
    public class PlanService : IPlanService
    {

        private readonly IPlanRepository planRepository;
        private readonly IApplicationTraceService applicationTraceService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPositionModuleRepository positionModuleRepository;
        private readonly IEtageRepository etageRepository;
        private readonly IModuleRepository moduleRepository;


        public PlanService(IPlanRepository _planRepository, IUnitOfWork _unitOfWork, IApplicationTraceService _applicationTraceService, IPositionModuleRepository _positionModuleRepository, IEtageRepository _etageRepository, IModuleRepository _moduleRepository)
        {
            this.planRepository = _planRepository;
            this.applicationTraceService = _applicationTraceService;
            this.unitOfWork = _unitOfWork;
            this.positionModuleRepository = _positionModuleRepository;
            this.etageRepository = _etageRepository;
            this.moduleRepository = _moduleRepository;
        }

        public void Create(Plan item, string user = "")
        {
            planRepository.Insert(item);

            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un nouveau plan",
            });
        }

        public void Delete(int id, string user = "")
        {
            planRepository.Delete(x => x.id == id);

            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression du plan plan_id = {0}", id),
            });

        }

        public IEnumerable<Plan> DonneTous()
        {
            return planRepository.GetAll();
        }

        public Plan Get(int id)
        {
            return planRepository.GetById(id);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Plan item, string user = "")
        {

            foreach (Etage e in item.listEtages)
            {
                foreach (PositionModule p in e.listPositionModule)
                {
                    if(p.id != 0){
                        //p.module = moduleRepository.GetById(p.module.id);
                        positionModuleRepository.Update(p);
                    }else
                    {
                        //p.module = moduleRepository.GetById(p.module.id);
                        positionModuleRepository.Insert(p);
                    }
                }
            }

                planRepository.Update(item);

            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour du plan plan_id = {0}", item.id),
            });
        }
    }

    public interface IPlanService : IService<Plan>
    {
        /*
         * Pour ajouter des méthodes propres à cet objet, écrire le corps de ces méthodes dans cette interface 
         */
    }
}