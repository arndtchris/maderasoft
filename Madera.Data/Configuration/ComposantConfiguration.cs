using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Model;
using System.Data.Entity.ModelConfiguration;

namespace Madera.Data.Configuration
{
    public class ComposantConfiguration : EntityTypeConfiguration<Composant>
    {
        public ComposantConfiguration()
        {
            ToTable("Composant");
            HasKey<int>(a => a.id);
            Property<bool>(a => a.isDeleted);
            Property(a => a.libe).HasColumnType("varchar").HasMaxLength(150);
            Property<double>(a => a.prixHT);
            Property<int>(a => a.qteStock);

        }
    }
}