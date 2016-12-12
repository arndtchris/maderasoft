using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class ComposantRepository : RepositoryBase<Composant>, IComposantRepository
    {
        public ComposantRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }



    public interface IComposantRepository : IRepository<Composant>
    {

    }
}