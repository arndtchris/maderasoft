using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class TaxeRepository : RepositoryBase<Taxe>, ITaxeRepository
    {
        public TaxeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ITaxeRepository : IRepository<Taxe>
    {

    }
}