using System;
using System.Collections.Generic;
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
            Property(a => a.password).HasColumnType("varchar").HasMaxLength(20);
            Property<DateTime>(a => a.dConnexion);
            Property<DateTime>(a => a.dCreation);
            Property<bool>(a => a.isActive);
            Property<bool>(a => a.isFirstConnexion);
        }
    }
}