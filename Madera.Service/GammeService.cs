using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class GammeService : IGammeService
    {
        private readonly IGammeRepository _gammeRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;


        public GammeService(IGammeRepository _gammeRepository, IUnitOfWork _unitOfWork, IApplicationTraceService _applicationTraceService)
        {
            this._gammeRepository = _gammeRepository;
            this._applicationTraceService = _applicationTraceService;
            this._unitOfWork = _unitOfWork;
        }

        public void Create(Gamme gamme)
        {
            _gammeRepository.Insert(gamme);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'une gamme",
            });
        }

        public void Delete(int id)
        {
            _gammeRepository.Delete(x => x.id == id);
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'une gamme gamme_id = {0}", id),
            });
        }

        public Gamme Get(int id)
        {
           return _gammeRepository.GetById(id);
        }

        public IEnumerable<Gamme> DonneTous()
        {
            return _gammeRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Gamme gamme)
        {

            _gammeRepository.Update(gamme);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un type d'une gamme gamme_id = {0}", gamme.id),
            });
        }
    }

    public interface IGammeService : IService<Gamme>
    {

    }
}