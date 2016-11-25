using System;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class UtilisateurRepository : RepositoryBase<Utilisateur>, IUtilisateurRepository
    {
        public UtilisateurRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Insert(Utilisateur entity)
        {
            entity.dCreation = DateTime.Now;
            entity.isDeleted = false;
            entity.isActive = true;
            entity.isFirstConnexion = true;
            entity.password = Crypte(entity.password);
            base.Insert(entity);
        }

        public override void Update(Utilisateur entity)
        {
            entity.password = Crypte(entity.password);
            base.Update(entity);
        }


    }

    public interface IUtilisateurRepository : IRepository<Utilisateur>
    {

    }


}