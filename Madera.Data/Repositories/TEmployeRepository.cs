using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class TEmployeRepository : RepositoryBase<TEmploye>, ITEmployeRepository
    {
        public TEmployeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ITEmployeRepository : IRepository<TEmploye>
    {

    }
}