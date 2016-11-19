using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class ProjetRepository : RepositoryBase<Projet>, IProjetRepository
    {
        public ProjetRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Insert(Projet entity)
        {
            entity.isDeleted = false;
            entity.isPaid = false;
            base.Insert(entity);
        }
    }

    public interface IProjetRepository : IRepository<Projet>
    {

    }

}