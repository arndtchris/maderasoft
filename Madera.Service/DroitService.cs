using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class DroitService : IDroitService
    {
        private readonly IDroitRepository _droitRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public DroitService(IDroitRepository droitRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._droitRepository = droitRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public void Create(Droit droit)
        {
            _droitRepository.Insert(droit);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un nouveau droit utilisateur",
            });
        }

        public void Delete(int id)
        {
            _droitRepository.Delete(x => x.id == id);
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un type de droit utilisateur droit_id = {0}", id),
            });
        }

        public Droit Get(int id)
        {
            return _droitRepository.GetById(id);
        }

        public IEnumerable<Droit> DonneTous()
        {
            return _droitRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Droit droit)
        {
            _droitRepository.Update(droit);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un type de droit utilisateur droit_id = {0}", droit.id),
            });
        }
    }

    public interface IDroitService : IService<Droit>
    {

    }
}