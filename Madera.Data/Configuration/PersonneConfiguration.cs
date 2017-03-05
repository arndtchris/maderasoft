using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class PersonneConfiguration : EntityTypeConfiguration<Personne>
    {
        public PersonneConfiguration()
        {
            ToTable("Personne");
            HasKey<int>(a => a.id);
            HasRequired<Adresse>(x => x.adresse);
            HasOptional<Utilisateur>(x => x.utilisateur);
            Property(a => a.civ).HasColumnType("varchar").HasMaxLength(3);
            Property(a => a.nom).HasColumnType("varchar").HasMaxLength(80);
            Property(a => a.prenom).HasColumnType("varchar").HasMaxLength(80);
            Property(a => a.email).HasColumnType("varchar").HasMaxLength(150);
            Property(a => a.tel1).HasColumnType("varchar").HasMaxLength(10);
            Property(a => a.tel2).HasColumnType("varchar").HasMaxLength(10);
            Property<bool>(a => a.isClient);
            Property<bool>(a => a.isFournisseur);
            Property<bool>(a => a.isDeleted);
        }
    }
}