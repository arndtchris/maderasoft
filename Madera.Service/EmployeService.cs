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
        private readonly IAffectationServiceService _affectationServiceService;
        private readonly IAdresseService _adresseService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeService(IEmployeRepository employeRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService, IAffectationServiceService affectationServiceService, IAdresseService adresseService)
        {
            this._employeRepository = employeRepository;
            this._applicationTraceService = applicationTraceService;
            this._affectationServiceService = affectationServiceService;
            this._adresseService = adresseService;
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
            _employeRepository.Insert(employe);
        }

        public void UpdateEmploye(Employe employe)
        {

            //Si la personne possède une adresse, on doit également la mettre à jour
            //EntityFramework ne gère pas la mise à jour des enfants
            //L'adresse est obligatoire, on a pas beoin de vérifier le null
            _adresseService.Update(employe.adresse);

            if (employe.affectationServices.Count > 0)
            {
                foreach(AffectationService affec in employe.affectationServices)
                {
                    if(affec.id != 0)//on met à jour l'affection
                    {
                        _affectationServiceService.UpdateAffectationService(affec);
                    }
                    else//il faut créer l'affectation
                    {
                        if(affec.employe == null)
                        {
                            affec.employe = new Employe();
                            affec.employe.id = employe.id;
                        }

                        _affectationServiceService.CreateAffectationService(affec);
                    }
                }
            }

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