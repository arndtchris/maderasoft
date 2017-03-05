using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class UtilisateurConfiguration : EntityTypeConfiguration<Utilisateur>
    {
        public UtilisateurConfiguration()
        {
            ToTable("Utilisateur");
            HasKey<int>(a => a.id);
            Property(a => a.login).HasColumnType("varchar").HasMaxLength(20);
            Property(a => a.password).HasColumnType("varchar").HasMaxLength(200);
            Property<DateTime>(a => a.dConnexion).HasColumnType("datetime2");
            Property<DateTime>(a => a.dCreation).HasColumnType("datetime2");
            //Property<DateTime>(a => a.dConnexion).IsOptional().HasColumnType("datetime2");
            //Property<DateTime>(a => a.dCreation).IsOptional().HasColumnType("datetime2");
            Property<bool>(a => a.isActive);
            Property<bool>(a => a.isFirstConnexion);
            Property<bool>(a => a.isDeleted);
        }
    }
}