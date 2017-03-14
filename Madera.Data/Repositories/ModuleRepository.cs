using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Insert(Module entity)
        {
            if(entity.id == 0)
                base.Insert(entity);
        }
    }

    public interface IModuleRepository : IRepository<Module>
    {

    }
}