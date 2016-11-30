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


        public void CreateUtilisateur(Utilisateur utilisateur)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un utilisateur",
            });

            _utilisateurRepository.Insert(utilisateur);
        }

        public void DeleteUtilisateur(int id)
        {
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de l'utilisateur utilisateur_id = {0}", id),
            });
            _utilisateurRepository.Delete(x => x.id == id);
        }

        public Utilisateur GetUtilisateur(int id)
        {
            return _utilisateurRepository.GetById(id);
        }

        public IEnumerable<Utilisateur> GetUtilisateurs()
        {
            return _utilisateurRepository.GetAll();
        }

        public void SaveUtilisateur()
        {
            _unitOfWork.Commit();
        }

        public void UpdateUtilisateur(Utilisateur utilsateur)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de l'utilisateur utilisateur_id = {0}", utilsateur.id),
            });

            _utilisateurRepository.Update(utilsateur);
        }
    }

    public interface IUtilisateurService
    {
        IEnumerable<Utilisateur> GetUtilisateurs();
        Utilisateur GetUtilisateur(int id);
        void CreateUtilisateur(Utilisateur utilisateur);
        void UpdateUtilisateur(Utilisateur utilsateur);
        void DeleteUtilisateur(int id);
        void SaveUtilisateur();
    }
}