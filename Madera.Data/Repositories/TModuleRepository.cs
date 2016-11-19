using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class TModuleRepository : RepositoryBase<TModule>, ITModuleRepository
    {
        public TModuleRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ITModuleRepository : IRepository<TModule>
    {

    }
}