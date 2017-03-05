using System;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class UtilisateurRepository : RepositoryBase<Utilisateur>, IUtilisateurRepository
    {
        public UtilisateurRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public void activeUtilisateur(int id)
        {
            Utilisateur util = this.dbSet.Find(id);
            base.DbContext.Entry(util).Property("isActive").CurrentValue = true;
        }

        public void desactiveUtilisateur(int id)
        {
            Utilisateur util = this.dbSet.Find(id);
            base.DbContext.Entry(util).Property("isActive").CurrentValue = false;
        }

        public override void Insert(Utilisateur entity)
        {
            entity.dCreation = DateTime.Now;
            entity.dConnexion = DateTime.Now;
            entity.isDeleted = false;
            entity.isActive = true;
            entity.isFirstConnexion = true;
            base.Insert(entity);
        }

        public void resetPwd(int id, string pwd)
        {
            Utilisateur util = this.dbSet.Find(id);
            base.DbContext.Entry(util).Property("password").CurrentValue = pwd;
        }

        public override void Update(Utilisateur entity)
        {

            base.Update(entity);
        }


    }

    public interface IUtilisateurRepository : IRepository<Utilisateur>
    {
        void activeUtilisateur(int id);
        void desactiveUtilisateur(int id);
        void resetPwd(int id,string pwd);
    }


}