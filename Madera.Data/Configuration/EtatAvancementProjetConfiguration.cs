using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class EtatAvancementProjetConfiguration : EntityTypeConfiguration<EtatAvancementProjet>
    {
        public EtatAvancementProjetConfiguration()
        {
            ToTable("EtatAvancementProjet");
            HasKey<int>(a => a.id);
            Property(a => a.libe).HasColumnType("varchar").HasMaxLength(150);
            Property<double>(a => a.pourcentageADebloquer);
        }
    }
}