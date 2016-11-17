using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class CompositionRepository : RepositoryBase<Composition>, ICompositionRepository
    {
        public CompositionRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ICompositionRepository : IRepository<Composition>
    {

    }
}