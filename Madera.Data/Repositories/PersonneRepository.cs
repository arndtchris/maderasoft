using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class PersonneRepository : RepositoryBase<Personne>, IPersonneRepository
    {
        public PersonneRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<Personne> GetEmployes()
        {
            return this.DbContext.Personnes.Where(c => !c.isClient && !c.isFournisseur);
        }

        public IEnumerable<Personne> GetPersonnes(Expression<Func<Personne, bool>> where)
        {
            return this.GetAll(where);
        }

        public override void Insert(Personne entity)
        {
            entity.isDeleted = false;
            base.Insert(entity);
        }

        
    }

    public interface IPersonneRepository : IRepository<Personne>
    {
        IEnumerable<Personne> GetEmployes();
        IEnumerable<Personne> GetPersonnes(Expression<Func<Personne, bool>> where);


    }
}