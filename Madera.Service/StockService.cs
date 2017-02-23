using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class StockService : IStockService
    {
        private readonly IComposantRepository _composantRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IComposantRepository composantRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._composantRepository = composantRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Composant> GetComposants()
        {
            return _composantRepository.GetAll();
        }

        public Composant GetUnComposant(int id)
        {
            return _composantRepository.GetById(id);
        }

        public void CreateComposant(Composant composant)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
            });

            _composantRepository.Insert(composant);
        }

        public void UpdateComposant(Composant composant)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
            });

            _composantRepository.Update(composant);
        }

        public void saveComposant()
        {
            _unitOfWork.Commit();
        }

        public void deleteComposant(int id)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression du composant_id = {0}", id),
            });
            _composantRepository.Delete(x => x.id == id);
        }
    }

    public interface IStockService
    {
        IEnumerable<Composant> GetComposants();
        Composant GetUnComposant(int id);
        void CreateComposant(Composant composant);
        void UpdateComposant(Composant composant);
        void saveComposant();
        void deleteComposant(int id);
    }
}