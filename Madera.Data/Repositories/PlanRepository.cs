using Madera.Data.Infrastructure;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Data.Repositories
{
    public class PlanRepository : RepositoryBase<Plan>, IPlanRepository
    {
        public PlanRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IPlanRepository : IRepository<Plan>
    {

    }
}