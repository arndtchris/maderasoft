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
    public class PersonneService : IPersonneService
    {
        private readonly IPersonneRepository _personneRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IAdresseService _adresseService;
        private readonly IEmployeService _employeService;
        private readonly IUtilisateurService _utilisateurService;
        private readonly IUnitOfWork _unitOfWork;
        private string _typePersonne = "";

        public PersonneService(IPersonneRepository personneRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService, IAdresseService adresseService, IEmployeService employeService, IUtilisateurService utilisateurService)
        {
            this._personneRepository = personneRepository;
            this._applicationTraceService = applicationTraceService;
            this._adresseService = adresseService;
            this._utilisateurService = utilisateurService;
            this._employeService = employeService;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Personne> DonneTous()
        {
            return _personneRepository.GetAll();
        }

        public Personne Get(int id)
        {
            return _personneRepository.GetById(id);
        }

        public void Create(Personne personne)
        {
            donneTypeDePersonne(ref this._typePersonne, personne);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = String.Format("Ajout de {0} {1} {2} en tant que {3} au sein de l'application", getCiv(personne.civ), personne.nom, personne.prenom, _typePersonne),
            });

            _personneRepository.Insert(personne);
        }

        public void Update(Personne personne)
        {
            donneTypeDePersonne(ref this._typePersonne, personne);

            //Si la personne possède une adresse, on doit également la mettre à jour
            //EntityFramework ne gère pas la mise à jour des enfants

            if(personne.adresse != null)
                _adresseService.Update(personne.adresse);

            _personneRepository.Update(personne);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de {0} {1} {2} en tant que {3}", getCiv(personne.civ), personne.nom, personne.prenom, _typePersonne),
            });
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _personneRepository.Delete(x => x.id == id);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de la personne personne_id = {0}", id),
            });

        }

        /// <summary>
        /// Donne le type de personne en fonction des attributs de l'objet
        /// </summary>
        /// <param name="_typePersonne"></param>
        /// <param name="perso"></param>
        private static void donneTypeDePersonne(ref string _typePersonne, Personne perso)
        {
            if (perso.isClient)
            {
                _typePersonne = "client";
            }
            else if (perso.isFournisseur)
            {
                _typePersonne = "fournisseur";
            }
            else if (!perso.isClient && !perso.isFournisseur)
            {
                _typePersonne = "employé";
            }
        }

        /// <summary>
        /// Donne la civilité de la personne en fonction de ses attributs
        /// </summary>
        /// <param name="civ"></param>
        /// <returns></returns>
        private static string getCiv(string civ)
        {

            if (civ != null)
            {
                if (civ == "1")
                {
                    return "Madame";
                }
                else
                {
                    return "Monsieur";
                }
            }
            else
            {
                return " ";
            }
        }

        public Personne TrouveUtilisateur(string login, string pwd)
        {
            string cryptedPwd = Crypte(pwd);
            return _personneRepository.Get(x => x.utilisateur.login == login && x.utilisateur.password == cryptedPwd);
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
    }

    public interface IPersonneService : IService<Personne>
    {
        Personne TrouveUtilisateur(string login, string pwd);

        string Crypte(string password);
    }
}