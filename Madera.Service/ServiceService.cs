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

        public void CreateService(Model.Service service)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Creation.ToString(),
                description = "Création d'un service",
            });

            _serviceRepository.Insert(service);
        }

        public void DeleteService(int id)
        {
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression d'un service service_id = {0}", id),
            });
            _serviceRepository.Delete(x => x.id == id);
        }

        public Model.Service GetService(int id)
        {
            return _serviceRepository.GetById(id);
        }

        public IEnumerable<Model.Service> GetServices()
        {
            return _serviceRepository.GetAll();
        }

        public void SaveService()
        {
            _unitOfWork.Commit();
        }

        public void UpdateService(Model.Service service)
        {
            _applicationTraceService.create(new ApplicationTrace
            {
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour d'un service service_id = {0}", service.id),
            });

            _serviceRepository.Update(service);
        }
    }

    public interface IServiceService
    {
        IEnumerable<Madera.Model.Service> GetServices();
        Madera.Model.Service GetService(int id);
        void CreateService(Madera.Model.Service service);
        void UpdateService(Madera.Model.Service service);
        void DeleteService(int id);
        void SaveService();
    }
}