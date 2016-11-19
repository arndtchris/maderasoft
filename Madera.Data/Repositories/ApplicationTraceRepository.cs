using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class ApplicationTraceRepository : RepositoryBase<ApplicationTrace>, IApplicationTraceRepository
    {
        public ApplicationTraceRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Insert(ApplicationTrace entity)
        {
            entity.date = DateTime.Now;
            base.Insert(entity);
        }
    }

    public interface IApplicationTraceRepository : IRepository<ApplicationTrace>
    {

    }
}