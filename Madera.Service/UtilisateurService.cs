using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (string.IsNullOrEmpty(utilisateur.password))
            {
                utilisateur.password = Crypte(Parametres.defaultPassword);
            }

            utilisateur.isFirstConnexion = true;
            utilisateur.dCreation = DateTime.Now;
            utilisateur.dConnexion = DateTime.MinValue;

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

        public void Update(Utilisateur utilisateur)
        {

            _utilisateurRepository.Update(utilisateur);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'utilisateur utilisateur_id = {0}", utilisateur.id),
            });
        }

        public string Crypte(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }

        public void ActiveUtilisateur(int id)
        {
            _utilisateurRepository.activeUtilisateur(id);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Activation du compte utilisateur utilisateur_id = {0}", id),
            });
        }

        public void DesactiveUtilisateur(int id)
        {
            _utilisateurRepository.desactiveUtilisateur(id);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Désactivation du compte utilisateur utilisateur_id = {0}", id),
            });
        }

        public void ResetPwd(int id)
        {
            _utilisateurRepository.resetPwd(id, Crypte(Parametres.defaultPassword));

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Réinitialisation du mot de passe du compte utilisateur utilisateur_id = {0}", id),
            });
        }

        public Utilisateur TrouveUtilisateur(string login, string pwd)
        {
            string cryptedPwd = Crypte(pwd);
            return _utilisateurRepository.Get(x => x.login == login && x.password == cryptedPwd);
        }
    }

    public interface IUtilisateurService : IService<Utilisateur>
    {
        void ActiveUtilisateur(int id);

        void DesactiveUtilisateur(int id);

        void ResetPwd(int id);

        string Crypte(string password);

        Utilisateur TrouveUtilisateur(string login, string pwd);
    }
}