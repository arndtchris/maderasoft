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

        public void CreateDroit(Droit droit)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un nouveau droit utilisateur",
            });

            _droitRepository.Insert(droit);
        }

        public void DeleteDroit(int id)
        {
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un type de droit utilisateur droit_id = {0}", id),
            });
            _droitRepository.Delete(x => x.id == id);
        }

        public Droit GetDroit(int id)
        {
            return _droitRepository.GetById(id);
        }

        public IEnumerable<Droit> GetDroits()
        {
            return _droitRepository.GetAll();
        }

        public void SaveDroit()
        {
            _unitOfWork.Commit();
        }

        public void UpdateDroit(Droit droit)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un type de droit utilisateur droit_id = {0}", droit.id),
            });

            _droitRepository.Update(droit);
        }
    }

    public interface IDroitService
    {
        IEnumerable<Droit> GetDroits();
        Droit GetDroit(int id);
        void CreateDroit(Droit droit);
        void UpdateDroit(Droit droit);
        void DeleteDroit(int id);
        void SaveDroit();
    }
}