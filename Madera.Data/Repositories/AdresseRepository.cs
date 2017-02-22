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
        public AdresseRepository(IDbFactory dbFactory): base(dbFactory) { }

        public IEnumerable<Adresse> GetAdresses(string country)
        {
            return this.DbContext.Adresses.Where(c => String.Equals(c.pays.ToUpper(), country.ToUpper()));
        }

    }

    public interface IAdresseRepository : IRepository<Adresse>
    {
        IEnumerable<Adresse> GetAdresses(string country);


    }
}