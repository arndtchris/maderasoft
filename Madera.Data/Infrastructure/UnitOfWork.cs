using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Madera.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private MaderaEntities dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("--- Nouvelle instance de la bdd ---");
            System.Diagnostics.Debug.WriteLine("");
        }

        public MaderaEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init());  }
        }


        public void Commit()
        {
            DbContext.Commit();
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("--- Fermeture de l'instance de la bdd ---");
            System.Diagnostics.Debug.WriteLine("");
        }
    }
}