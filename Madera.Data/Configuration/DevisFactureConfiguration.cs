using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class DevisFactureConfiguration : EntityTypeConfiguration<DevisFacture>
    {
        public DevisFactureConfiguration()
        {
            ToTable("DevisFacture");
            HasKey<int>(a => a.id);
            Property<bool>(a => a.isDeleted);
            Property<bool>(a => a.isSigned);
        }
    }
}