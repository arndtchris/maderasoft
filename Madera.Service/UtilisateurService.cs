using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class UtilisateurService : IUtilisateurService
    {

        private readonly IUtilisateurRepository _utilisateurRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public UtilisateurService(IUtilisateurRepository utilisateurRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._utilisateurRepository = utilisateurRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public void Create(Utilisateur utilisateur)
        {
            _utilisateurRepository.Insert(utilisateur);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un utilisateur",
            });
        }

        public void Delete(int id)
        {
            _utilisateurRepository.Delete(x => x.id == id);

            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de l'utilisateur utilisateur_id = {0}", id),
            });
        }

        public Utilisateur Get(int id)
        {
            return _utilisateurRepository.GetById(id);
        }

        public IEnumerable<Utilisateur> DonneTous()
        {
            return _utilisateurRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Utilisateur utilsateur)
        {
            _utilisateurRepository.Update(utilsateur);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'utilisateur utilisateur_id = {0}", utilsateur.id),
            });
        }
    }

    public interface IUtilisateurService : IService<Utilisateur>
    {

    }
}