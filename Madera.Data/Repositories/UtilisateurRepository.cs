using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class UtilisateurRepository : RepositoryBase<Utilisateur>, IUtilisateurRepository
    {
        public UtilisateurRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IUtilisateurRepository : IRepository<Utilisateur>
    {

    }
}