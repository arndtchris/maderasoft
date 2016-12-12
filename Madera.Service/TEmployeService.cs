using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class TEmployeService : ITEmployeService
    {
        private readonly ITEmployeRepository _temployeRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;


        public TEmployeService(ITEmployeRepository _temployeRepository, IUnitOfWork _unitOfWork, IApplicationTraceService _applicationTraceService)
        {
            this._temployeRepository = _temployeRepository;
            this._applicationTraceService = _applicationTraceService;
            this._unitOfWork = _unitOfWork;
        }

        public void CreateTEmploye(TEmploye TEmploye)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un type d'employé",
            });

            _temployeRepository.Insert(TEmploye);
        }

        public void DeleteTEmploye(int id)
        {
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un type d'employé temploye_id = {0}", id),
            });
            _temployeRepository.Delete(x => x.TEmployeId == id);
        }

        public TEmploye GetTEmploye(int id)
        {
           return _temployeRepository.GetById(id);
        }

        public IEnumerable<TEmploye> GetTEmployes()
        {
            return _temployeRepository.GetAll();
        }

        public void SaveTEmploye()
        {
            _unitOfWork.Commit();
        }

        public void UpdateTEmploye(TEmploye TEmploye)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un type d'employé temploye_id = {0}", TEmploye.TEmployeId),
            });

            _temployeRepository.Update(TEmploye);
        }
    }

    public interface ITEmployeService
    {
        IEnumerable<TEmploye> GetTEmployes();
        TEmploye GetTEmploye(int id);
        void CreateTEmploye(TEmploye TEmploye);
        void UpdateTEmploye(TEmploye TEmploye);
        void DeleteTEmploye(int id);
        void SaveTEmploye();
    }
}