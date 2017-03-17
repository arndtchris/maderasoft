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

        public void Create(TEmploye TEmploye, string user = "")
        {
            _temployeRepository.Insert(TEmploye);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un type d'employé",
            });
        }

        public void Delete(int id, string user = "")
        {
            _temployeRepository.Delete(x => x.id == id);
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un type d'employé temploye_id = {0}", id),
            });
        }

        public TEmploye Get(int id)
        {
           return _temployeRepository.GetById(id);
        }

        public IEnumerable<TEmploye> DonneTous()
        {
            return _temployeRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(TEmploye TEmploye, string user = "")
        {

            _temployeRepository.Update(TEmploye);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un type d'employé temploye_id = {0}", TEmploye.id),
            });
        }
    }

    public interface ITEmployeService : IService<TEmploye>
    {

    }
}