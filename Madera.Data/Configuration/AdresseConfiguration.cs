using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class AdresseConfiguration : EntityTypeConfiguration<Adresse>
    {
        public AdresseConfiguration()
        {
            ToTable("Adresse");
            HasKey<int>(a => a.AdresseID);
            Property(a => a.codePostal).HasColumnType("varchar").HasMaxLength(5);
            Property(a => a.nomRue).HasColumnType("varchar").HasMaxLength(150);
            Property(a => a.numRue).HasColumnType("varchar").HasMaxLength(10);
            Property(a => a.ville).HasColumnType("varchar").HasMaxLength(80);
            Property(a => a.pays).HasColumnType("varchar").HasMaxLength(80);
        }
    }
}