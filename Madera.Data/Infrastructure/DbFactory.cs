using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        MaderaEntities dbContext;

        public MaderaEntities Init()
        {
            return dbContext ?? (dbContext = new MaderaEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}