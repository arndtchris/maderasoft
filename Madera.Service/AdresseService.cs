using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class AdresseService : IAdresseService
    {
        private readonly IAdresseRepository adresseRepository;
        private readonly IUnitOfWork unitOfWork;

        public AdresseService(IAdresseRepository adresseRepository, IUnitOfWork unitOfWork)
        {
            this.adresseRepository = adresseRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Adresse> GetAdresses()
        {
            return adresseRepository.GetAll();
        }

        public IEnumerable<Adresse> GetAdressesByCountry(string country)
        {
            return adresseRepository.GetAll().Where(c => c.pays == country);
        }

        public Adresse GetAdresse(int id)
        {
            var adresse = adresseRepository.GetById(id);
            return adresse;
        }

        public void CreateAdresse(Adresse adresse)
        {
            adresseRepository.Insert(adresse);
        }

        public void UpdateAdresse(Adresse adresse)
        {
            adresseRepository.Update(adresse);
        }
    }

    //Définition des méthodes qui seront accessibles depuis la couche de présentation
    //A ce niveau on peut définir des méthodes AjouteAdresseEtDonneListe() qui va combiner plusieurs méthodes basiques du pattern Repository
    public interface IAdresseService
    {
        IEnumerable<Adresse> GetAdresses();
        IEnumerable<Adresse> GetAdressesByCountry(string country);
        Adresse GetAdresse(int id);
        void CreateAdresse(Adresse adresse);
        void UpdateAdresse(Adresse adresse);
    }
}