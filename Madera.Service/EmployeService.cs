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
        private readonly IUtilisateurService _utilisateurService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeService(IEmployeRepository employeRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService, IAffectationServiceService affectationServiceService, IAdresseService adresseService, IUtilisateurService utilisateurService)
        {
            this._employeRepository = employeRepository;
            this._applicationTraceService = applicationTraceService;
            this._affectationServiceService = affectationServiceService;
            this._adresseService = adresseService;
            this._utilisateurService = utilisateurService;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Employe> DonneTous()
        {
            return _employeRepository.GetAll();
        }

        public Employe Get(int id)
        {
            return _employeRepository.GetById(id);
        }

        public void Create(Employe employe)
        {
            _employeRepository.Insert(employe);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un employé",
            });
        }

        public void Update(Employe employe)
        {

            //Si la personne possède une adresse, on doit également la mettre à jour
            //EntityFramework ne gère pas la mise à jour des enfants
            if(employe.adresse != null)
                _adresseService.Update(employe.adresse);

            //L'utilisateur existe, on le met à jour
            if(employe.utilisateur.id != 0)
            {
                _utilisateurService.Update(employe.utilisateur);

            }else if(employe.utilisateur.id == 0 && string.IsNullOrEmpty(employe.utilisateur.login))//l'utilisateur n'existe pas, on le créé
            {
                _utilisateurService.Create(employe.utilisateur);
            }

            if (employe.affectationServices != null && employe.affectationServices.Count > 0)
            {
                foreach(AffectationService affec in employe.affectationServices)
                {
                    if(affec.id != 0)//on met à jour l'affection
                    {
                        _affectationServiceService.Update(affec);
                    }
                    else//il faut créer l'affectation
                    {
                        if(affec.employe == null)
                        {
                            affec.employe = new Employe();
                            affec.employe = employe;
                        }

                        _affectationServiceService.Create(affec);
                    }
                }
            }
            _employeRepository.Update(employe);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'employé employe_id = {0}", employe.id),
            });
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            Employe emp = this.Get(id);
            List<int> ids = new List<int>();

            foreach(AffectationService affec in emp.affectationServices)
            {
                ids.Add(affec.id);
            }

            foreach(int i in ids)
            {
                _affectationServiceService.Delete(i);
            }

            _adresseService.Delete(emp.adresse.id);

            _employeRepository.Delete(x => x.id == id);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de l'employé employe_id = {0}", id),
            });
        }
    }

    public interface IEmployeService : IService<Employe>
    {

    }
}