using Madera.Data.Infrastructure;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Data.Repositories
{
    public class EtageRepository : RepositoryBase<Etage>, IEtageRepository
    {
        public EtageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IEtageRepository : IRepository<Etage>
    {

    }
}