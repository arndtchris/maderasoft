﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Data.Infrastructure;
using Madera.Model;

namespace Madera.Data.Repositories
{
    public class GammeRepository : RepositoryBase<Gamme>, IGammeRepository
    {
        public GammeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IGammeRepository : IRepository<Gamme>
    {

    }
}