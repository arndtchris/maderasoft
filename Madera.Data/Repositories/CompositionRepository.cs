using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;
using System.Linq.Expressions;

namespace Madera.Data.Repositories
{
    public class CompositionRepository : RepositoryBase<Composition>, ICompositionRepository
    {
        public CompositionRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<Composition> GetCompositions(Expression<Func<Composition, bool>> where)
        {
            return this.GetAll(where);
        }
    }

    public interface ICompositionRepository : IRepository<Composition>
    {
        IEnumerable<Composition> GetCompositions(Expression<Func<Composition, bool>> where);

    }
}