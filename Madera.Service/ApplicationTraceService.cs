using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;

namespace Madera.Service
{
    public class ApplicationTraceService : IApplicationTraceService
    {
        /*
         * Une trace peut uniquement être crée depuis l'application. 
         * 
         */ 

        private readonly IApplicationTraceRepository applicationTraceRepository;
        private readonly IUnitOfWork unitOfWork;

        public ApplicationTraceService(IApplicationTraceRepository applicationTraceRepository, IUnitOfWork unitOfWork)
        {
            this.applicationTraceRepository = applicationTraceRepository;
            this.unitOfWork = unitOfWork;
        }

        public void create(ApplicationTrace trace)
        {
            applicationTraceRepository.Insert(trace);
        }

        public void save()
        {
            unitOfWork.Commit();
        }

    }

    public interface IApplicationTraceService
    {
        void save();
        void create(ApplicationTrace trace);
    }
}