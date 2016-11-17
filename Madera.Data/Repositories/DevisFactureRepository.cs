using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class DevisFactureRepository : RepositoryBase<DevisFacture>, IDevisFactureRepository
    {
        public DevisFactureRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Insert(DevisFacture entity)
        {
            entity.isDeleted = false;
            entity.isSigned = false;
            base.Insert(entity);
        }
    }

    public interface IDevisFactureRepository : IRepository<DevisFacture>
    {

    }
}