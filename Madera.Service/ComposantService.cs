using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class ComposantService : IComposantService
    {
        private readonly IComposantRepository _composantRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public ComposantService(IComposantRepository composantRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._composantRepository = composantRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public Composant Get(int id)
        {
            return _composantRepository.GetById(id);
        }

        public void Create(Composant item, string user = "")
        {
            _composantRepository.Insert(item);
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Creation.ToString(),
                description = String.Format("Création du composant {0} composant_id = {1}", item.libe, item.id)
            });

            
        }

        public void Update(Composant item, string user = "")
        {
            _composantRepository.Update(item);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Modification du composant {0} composant_id = {1}", item.libe, item.id),
            });   
        }

        public void Delete(int id, string user = "")
        {
            _composantRepository.Delete(x => x.id == id);
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = user,
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression du composant_id = {0}", id),
            });
            
        }

        public IEnumerable<Composant> DonneTous()
        {
            return _composantRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }

    public interface IComposantService : IService<Composant>
    {
    //    IEnumerable<Composant> GetComposants();
    //    Composant GetUnComposant(int id);
    //    void CreateComposant(Composant composant);
    //    void UpdateComposant(Composant composant);
    //    void saveComposant();
    //    void deleteComposant(int id);
    }
}