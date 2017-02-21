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
            HasRequired<Employe>(x => x.employe).WithMany(x => x.affectationServices);
            HasRequired<Service>(x => x.service);
            HasRequired<Droit>(x => x.groupe);
            Property<bool>(a => a.isPrincipal);

        }
    }
}