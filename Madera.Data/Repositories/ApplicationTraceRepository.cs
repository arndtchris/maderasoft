using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Models.Models;

namespace Madera.Data.Repositories
{
    public class ApplicationTraceRepository : RepositoryBase<ApplicationTrace>, IApplicationTraceRepository
    {
        public ApplicationTraceRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IApplicationTraceRepository : IRepository<ApplicationTrace>
    {

    }
}