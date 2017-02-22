using Madera.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Madera.Data.Configuration
{
    public class PlanConfiguration : EntityTypeConfiguration<Plan>
    {
        public PlanConfiguration()
        {
            ToTable("Plan");
            HasKey<int>(a => a.id);
            Property<int>(a => a.largeur).IsRequired();
            Property<int>(a => a.longueur).IsRequired();
        }

    }
}