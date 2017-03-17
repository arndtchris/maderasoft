using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;


        public ServiceService(IServiceRepository serviceRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._serviceRepository = serviceRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }

        
        public void Create(Model.Service service, string user = "")
        {
            _serviceRepository.Insert(service);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un service",
            });
        }

        public void Delete(int id, string user = "")
        {
            _serviceRepository.Delete(x => x.id == id);
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un service service_id = {0}", id),
            });
        }

        public Model.Service Get(int id)
        {
            return _serviceRepository.GetById(id);
        }

        public IEnumerable<Model.Service> DonneTous()
        {
            return _serviceRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Model.Service service, string user = "")
        {
            _serviceRepository.Update(service);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un service service_id = {0}", service.id),
            });
        }
    }

    public interface IServiceService : IService<Madera.Model.Service>
    {

    }
}