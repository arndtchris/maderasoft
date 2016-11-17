using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class EtabRepository : RepositoryBase<Etab>, IEtabRepository
    {
        public EtabRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IEtabRepository : IRepository<Etab>
    {

    }
}