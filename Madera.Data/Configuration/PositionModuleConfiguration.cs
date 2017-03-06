using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class PositionModuleConfiguration : EntityTypeConfiguration<PositionModule>
    {
        public PositionModuleConfiguration()
        {
            ToTable("PositionModule");
            HasKey<int>(a => a.id);
            HasRequired<Module>(a => a.module);
            HasRequired<Etage>(x => x.etage).WithMany(x => x.listPositionModule);
            Property<int>(x => x.x1);
            Property<int>(x => x.x2);
            Property<int>(x => x.y1);
            Property<int>(x => x.y2);
            Property(a => a.lineId).HasColumnType("varchar").HasMaxLength(30);
        }
    }
}