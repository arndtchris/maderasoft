using Madera.Data.Infrastructure;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Data.Repositories
{
    public class PositionModuleRepository : RepositoryBase<PositionModule>, IPositionModuleRepository
    {
        public PositionModuleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IPositionModuleRepository : IRepository<PositionModule>
    {

    }
}