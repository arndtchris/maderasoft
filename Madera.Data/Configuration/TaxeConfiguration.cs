using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class TaxeConfiguration : EntityTypeConfiguration<Taxe>
    {
        public TaxeConfiguration()
        {
            ToTable("Taxe");
            HasKey<int>(a => a.id);
            Property(a => a.libe).HasColumnType("varchar").HasMaxLength(50);
            Property<double>(a => a.pourcentage);
            Property<bool>(a => a.isReduction);
        }
    }
}