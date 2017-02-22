using Madera.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Madera.Data.Configuration
{
    public class EtageConfiguration : EntityTypeConfiguration<Etage>
    {
        public EtageConfiguration()
        {
            ToTable("Etage");
            HasKey<int>(a => a.id);
            HasRequired<Plan>(x => x.plan).WithMany(x => x.listEtages);
        }
    }
}