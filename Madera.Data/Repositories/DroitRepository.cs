using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class DroitRepository : RepositoryBase<Droit>, IDroitRepository
    {
        public DroitRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IDroitRepository : IRepository<Droit>
    {

    }
}