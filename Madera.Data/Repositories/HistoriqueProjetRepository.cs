using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class HistoriqueProjetRepository : RepositoryBase<HistoriqueProjet>, IHistoriqueProjetRepository
    {
        public HistoriqueProjetRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Insert(HistoriqueProjet entity)
        {
            entity.date = DateTime.Now;
            base.Insert(entity);
        }

        public override void Update(HistoriqueProjet entity)
        {
            entity.date = DateTime.Now;
            base.Update(entity);
        }
    }

    public interface IHistoriqueProjetRepository : IRepository<HistoriqueProjet>
    {

    }
}