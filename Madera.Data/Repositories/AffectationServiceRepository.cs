using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class AffectationServiceRepository : RepositoryBase<AffectationService>, IAffectationServiceRepository
    {
        public AffectationServiceRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IAffectationServiceRepository : IRepository<AffectationService>
    {
        
    }

}