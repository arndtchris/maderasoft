using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Personne> GetPersonnes()
        {
            return _personneRepository.GetAll();
        }

        public Personne GetPersonne(int id)
        {
            return _personneRepository.GetById(id);
        }

        public void CreatePersonne(Personne personne)
        {
            donneTypeDePersonne(ref this._typePersonne, personne);

            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                description = String.Format("Ajout de {0} {1} {2} en tant que {3} au sein de l'application", getCiv(personne.civ), personne.nom, personne.prenom, _typePersonne),
            });

            _personneRepository.Insert(personne);
        }

        public void UpdatePersonne(Personne personne)
        {
            donneTypeDePersonne(ref this._typePersonne, personne);

            //Si la personne possède une adresse, on doit également la mettre à jour
            //EntityFramework ne gère pas la mise à jour des enfants
            //L'adresse est obligatoire, on a pas beoin de vérifier le null
            _adresseService.Update(personne.adresse);

            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour de {0} {1} {2} en tant que {3}", getCiv(personne.civ), personne.nom, personne.prenom, _typePersonne),
            });

            _personneRepository.Update(personne);
        }

        public void savePersonne()
        {
            _unitOfWork.Commit();
        }

        public void deletePersonne(int id)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de la personne personne_id = {0}", id),
            });
            _personneRepository.Delete(x => x.id == id);
        }

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

        public IEnumerable<Personne> GetEmployes()
        {
            return _personneRepository.GetEmployes();
        }
    }

    public interface IPersonneService
    {
        IEnumerable<Personne> GetPersonnes();
        IEnumerable<Personne> GetEmployes();
        Personne GetPersonne(int id);
        void CreatePersonne(Personne personne);
        void UpdatePersonne(Personne personne);
        void savePersonne();
        void deletePersonne(int id);

    }
}