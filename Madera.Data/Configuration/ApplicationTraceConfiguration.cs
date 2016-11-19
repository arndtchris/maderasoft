using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class ApplicationTraceConfiguration : EntityTypeConfiguration<ApplicationTrace>
    {
        public ApplicationTraceConfiguration()
        {
            ToTable("ApplicationTrace");
            HasKey<int>(a => a.id);
            Property(a => a.description).HasColumnType("varchar").HasMaxLength(255);
            Property(a => a.utilisateur).HasColumnType("varchar").HasMaxLength(150);
            Property(a => a.action).HasColumnType("varchar").HasMaxLength(150);
            Property<DateTime>(a => a.date);
        }
    }
}