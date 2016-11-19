using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class ProjetConfiguration : EntityTypeConfiguration<Projet>
    {
        public ProjetConfiguration()
        {
            ToTable("Projet");
            HasKey<int>(a => a.id);
            Property(a => a.libe).HasColumnType("varchar").HasMaxLength(150);
            Property<float>(a => a.prixHT);
            Property<float>(a => a.prixTotalTTC);
            Property<bool>(a => a.isDeleted);
            Property<bool>(a => a.isPaid);
         }
    }
}