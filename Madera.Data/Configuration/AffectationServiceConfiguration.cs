using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class AffectationServiceConfiguration : EntityTypeConfiguration<AffectationService>
    {
        public AffectationServiceConfiguration()
        {
            ToTable("AffectationService");
            HasKey<int>(a => a.id);
            Property<bool>(a => a.isPrincipal);

        }
    }
}