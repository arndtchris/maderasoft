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


        public PlanService(IPlanRepository _planRepository, IUnitOfWork _unitOfWork, IApplicationTraceService _applicationTraceService)
        {
            this.planRepository = _planRepository;
            this.applicationTraceService = _applicationTraceService;
            this.unitOfWork = _unitOfWork;
        }

        public void Create(Plan item)
        {
            planRepository.Insert(item);

            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un nouveau plan",
            });
        }

        public void Delete(int id)
        {
            planRepository.Delete(x => x.id == id);

            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
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

        public void Update(Plan item)
        {
            planRepository.Update(item);

            applicationTraceService.create(new ApplicationTrace
            {
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