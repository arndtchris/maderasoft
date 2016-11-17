using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class ModuleConfiguration : EntityTypeConfiguration<Module>
    {
        public ModuleConfiguration()
        {
            ToTable("Moule");
            HasKey<int>(a => a.id);
            Property(a => a.libe).HasColumnType("varchar").HasMaxLength(150);
            Property(a => a.coupePrincipe).HasColumnType("varchar");
        }
    }
}