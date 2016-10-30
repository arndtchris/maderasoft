using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class AdresseRepository : RepositoryBase<Adresse>, IAdresseRepository
    {
        public AdresseRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

    }

    public interface IAdresseRepository : IRepository<Adresse>
    {
        
    }
}