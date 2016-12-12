using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class EmployeService : IEmployeService
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeService(IEmployeRepository employeRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._employeRepository = employeRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Employe> GetEmployes()
        {
            return _employeRepository.GetAll();
        }

        public Employe GetEmploye(int id)
        {
            return _employeRepository.GetById(id);
        }

        public void CreateEmploye(Employe employe)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                //description = String.Format("Création d'un nouvel employé {0} {1}", employe.personne.nom, employe.personne.prenom),
            });

            _employeRepository.Insert(employe);
        }

        public void UpdateEmploye(Employe employe)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                //description = String.Format("Mise à jour de l'employé {0} {1}", employe.personne.nom, employe.personne.prenom),
            });

            _employeRepository.Update(employe);
        }

        public void saveEmploye()
        {
            _unitOfWork.Commit();
        }

        public void deleteEmploye(int id)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de l'employé employé_id = {0}", id),
            });
            _employeRepository.Delete(x => x.id == id);
        }
    }

    public interface IEmployeService
    {
        IEnumerable<Employe> GetEmployes();
        Employe GetEmploye(int id);
        void CreateEmploye(Employe employe);
        void UpdateEmploye(Employe employe);
        void saveEmploye();
        void deleteEmploye(int id);
    }
}