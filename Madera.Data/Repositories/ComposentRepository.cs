using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class ComposentRepository : RepositoryBase<Composent>, IComposentRepository
    {
        public ComposentRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }



    public interface IComposentRepository : IRepository<Composent>
    {

    }
}