using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class TEmployeConfiguration : EntityTypeConfiguration<TEmploye>
    {

        public TEmployeConfiguration()
        {
            ToTable("TEmploye");
            HasKey<int>(a => a.id);
            Property(a => a.libe).HasColumnType("varchar").HasMaxLength(100);

        }

    }
}