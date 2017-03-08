using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class DroitConfiguration : EntityTypeConfiguration<Droit>
    {
        public DroitConfiguration()
        {
            ToTable("Droit");
            HasKey<int>(a => a.id);
            Property(a => a.libe).HasColumnType("varchar").HasMaxLength(50);
            Property<bool>(a => a.delete);
            Property<bool>(a => a.update);
            Property<bool>(a => a.create);
            Property<bool>(a => a.read);
            Property<bool>(a => a.softDelete);
        }
    }
}