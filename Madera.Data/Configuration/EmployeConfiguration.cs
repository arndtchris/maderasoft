using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data.Configuration
{
    public class EmployeConfiguration : EntityTypeConfiguration<Employe>
    {
        public EmployeConfiguration()
        {
            ToTable("Employe");
            HasKey<int>(a => a.id);
            HasRequired<TEmploye>(x => x.typeEmploye);
            Property<bool>(a => a.isDeleted);
        }
    }
}