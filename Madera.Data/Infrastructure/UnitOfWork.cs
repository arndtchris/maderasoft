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
        }

        public MaderaEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init());  }
        }


        public void Commit()
        {
            DbContext.Commit();
        }
    }
}