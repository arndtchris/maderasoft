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
            //HasRequired<Employe>(x => x.employe).WithMany(x => x.affectationServices).HasForeignKey( x => x.employeId);
            HasRequired<Employe>(x => x.employe).WithMany(x => x.affectationServices);
            HasOptional<Service>(x => x.service);
            HasOptional<Droit>(x => x.groupe);
            Property<bool>(a => a.isPrincipal);

        }
    }
}