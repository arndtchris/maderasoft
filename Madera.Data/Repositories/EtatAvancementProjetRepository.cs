using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class EtatAvancementProjetRepository : RepositoryBase<EtatAvancementProjet>, IEtatAvancementProjetRepository
    {
        public EtatAvancementProjetRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IEtatAvancementProjetRepository : IRepository<EtatAvancementProjet>
    {

    }
}