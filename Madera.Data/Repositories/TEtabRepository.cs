using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class TEtabRepository : RepositoryBase<TEtab>, ITEtabRepository
    {
        public TEtabRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ITEtabRepository : IRepository<TEtab>
    {

    }
}