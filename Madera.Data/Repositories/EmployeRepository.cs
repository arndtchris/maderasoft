using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class EmployeRepository : RepositoryBase<Employe>, IEmployeRepository
    {
        public EmployeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Insert(Employe entity)
        {
            entity.isDeleted = false;
            base.Insert(entity);
        }
    }

    public interface IEmployeRepository : IRepository<Employe>
    {

    }
}