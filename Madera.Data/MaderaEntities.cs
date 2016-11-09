using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Madera.Data.Configuration;
using Madera.Model;

namespace Madera.Data
{
    public class MaderaEntities : DbContext
    {
        public MaderaEntities() : base("MaderaEntities") { }

        public DbSet<Adresse> Adresses { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdresseConfiguration());
        }
    }
}