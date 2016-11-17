using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class HistoriqueProjetConfiguration : EntityTypeConfiguration<HistoriqueProjet>
    {

        public HistoriqueProjetConfiguration()
        {
            ToTable("HistoriqueProjet");
            HasKey<int>(a => a.id);
            Property<DateTime>(a => a.date);
        }
    }
}