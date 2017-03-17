using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Model;
using System.Data.Entity.ModelConfiguration;

namespace Madera.Data.Configuration
{
    public class CompositionConfiguration : EntityTypeConfiguration<Composition>
    {
        public CompositionConfiguration()
        {
            ToTable("Composition");
            HasKey<int>(a => a.id);
            HasOptional<Composant>(a => a.composant);
            HasOptional<Module>(a => a.module).WithMany(x => x.compositions);

        }


    }
}